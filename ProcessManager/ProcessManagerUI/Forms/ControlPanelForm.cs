using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.DataObjects.Comparers;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;
using ProcessManager.Service.DataObjects;
using ProcessManager.Utilities;
using ProcessManagerUI.Controls.Nodes;
using ProcessManagerUI.Controls.Nodes.Support;
using ProcessManagerUI.Support;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Forms
{
	public partial class ControlPanelForm : Form, IProcessManagerEventHandler
	{
		private static readonly IDictionary<ControlPanelGrouping, string> _controlPanelGroupingDescriptions = new Dictionary<ControlPanelGrouping, string>()
			{
				{ ControlPanelGrouping.MachineGroupApplication, "Machine > Group > Application" },
				{ ControlPanelGrouping.GroupMachineApplication, "Group > Machine > Application" }
			};
		private static readonly IDictionary<DistributionGrouping, string> _distributionGroupingDescriptions = new Dictionary<DistributionGrouping, string>()
			{
				{ DistributionGrouping.MachineGroupApplicationMachine, "Machine > Group > Application > Machine" },
				//{ DistributionGrouping.MachineGroupMachine, "Machine > Group > Machine" }
			};
		private DateTime _formClosedAt;
		private readonly List<INode> _allNodes;
		private readonly List<IRootNode> _rootNodes;
		private readonly List<ControlPanelApplicationNode> _applicationNodes;
		private bool _controlPanelNodeLayoutSuspended;
		private bool _distributionNodeLayoutSuspended;

		public event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;

		public ControlPanelForm()
		{
			InitializeComponent();
			_formClosedAt = DateTime.MinValue;
			_allNodes = new List<INode>();
			_rootNodes = new List<IRootNode>();
			_applicationNodes = new List<ControlPanelApplicationNode>();
			_controlPanelNodeLayoutSuspended = false;
			_distributionNodeLayoutSuspended = false;
			ServiceHelper.Initialize(this);
		}

		#region Event raisers

		private bool RaiseConfigurationChangedEvent(Machine machine, string configurationHash)
		{
			if (ConfigurationChanged != null)
			{
				ConfigurationChanged(this, new MachineConfigurationHashEventArgs(machine, configurationHash));
				return true;
			}
			return false;
		}

		#endregion

		#region GUI events handlers

		#region Main form

		private void ControlPanelForm_Load(object sender, EventArgs e)
		{
			HideForm();
			ExtendGlass();
			Settings.Client.Load();

			foreach (ControlPanelGrouping grouping in _controlPanelGroupingDescriptions.Keys)
			{
				int index = comboBoxControlPanelGroupBy.Items.Add(new ComboBoxItem<ControlPanelGrouping>(_controlPanelGroupingDescriptions[grouping], grouping));
				if (Settings.Client.CP_SelectedGrouping == grouping.ToString())
					comboBoxControlPanelGroupBy.SelectedIndex = index;
			}
			if (comboBoxControlPanelGroupBy.SelectedIndex == -1)
				comboBoxControlPanelGroupBy.SelectedIndex = 0;

			foreach (DistributionGrouping grouping in _distributionGroupingDescriptions.Keys)
			{
				int index = comboBoxDistributionGroupBy.Items.Add(new ComboBoxItem<DistributionGrouping>(_distributionGroupingDescriptions[grouping], grouping));
				if (Settings.Client.D_SelectedGrouping == grouping.ToString())
					comboBoxDistributionGroupBy.SelectedIndex = index;
			}
			if (comboBoxDistributionGroupBy.SelectedIndex == -1)
				comboBoxDistributionGroupBy.SelectedIndex = 0;

			DisplaySelectedTabPage();
			UpdateControlPanelFilterAndLayout();

			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerInitializationCompleted += ServiceConnectionHandler_ServiceHandlerInitializationCompleted;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged += ServiceConnectionHandler_ServiceHandlerConnectionChanged;
			foreach (Machine machine in Settings.Client.Machines.Where(machine => !ConnectionStore.ConnectionCreated(machine)))
			{
				MachineConnection connection = ConnectionStore.CreateConnection(this, machine);
				connection.ServiceHandler.Initialize();
			}
		}

		private void ControlPanelForm_Deactivate(object sender, EventArgs e)
		{
			if (Opacity == 1)
				HideForm();
		}

		private void TabControlSection_SelectedIndexChanged(object sender, EventArgs e)
		{
			DisplaySelectedTabPage();
		}

		private void ComboBoxControlPanelGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxControlPanelGroupBy.SelectedIndex == -1) return;

			ControlPanelGrouping grouping = ((ComboBoxItem<ControlPanelGrouping>) comboBoxControlPanelGroupBy.Items[comboBoxControlPanelGroupBy.SelectedIndex]).Tag;
			Settings.Client.CP_SelectedGrouping = grouping.ToString();
			Settings.Client.Save(ClientSettingsType.States);

			LayoutControlPanelNodes();
		}

		private void ComboBoxControlPanelMachineFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxControlPanelMachineFilter.SelectedIndex == -1) return;

			Settings.Client.CP_SelectedFilterMachine = ((ComboBoxItem) comboBoxControlPanelMachineFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutControlPanelNodes();
		}

		private void ComboBoxControlPanelGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxControlPanelGroupFilter.SelectedIndex == -1) return;

			Settings.Client.CP_SelectedFilterGroup = ((ComboBoxItem) comboBoxControlPanelGroupFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutControlPanelNodes();
		}

		private void ComboBoxControlPanelApplicationFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxControlPanelApplicationFilter.SelectedIndex == -1) return;

			Settings.Client.CP_SelectedFilterApplication = ((ComboBoxItem) comboBoxControlPanelApplicationFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutControlPanelNodes();
		}

		private void LinkLabelControlPanelStartAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_rootNodes.ForEach(node => node.TakeAction(ActionType.Start));
		}

		private void LinkLabelControlPanelStopAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_rootNodes.ForEach(node => node.TakeAction(ActionType.Stop));
		}

		private void LinkLabelControlPanelRestartAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_rootNodes.ForEach(node => node.TakeAction(ActionType.Restart));
		}

		private void LinkLabelControlPanelExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_rootNodes.ForEach(node => node.ExpandAll(true));
		}

		private void LinkLabelControlPanelCollapseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_rootNodes.ForEach(node => node.ExpandAll(false));
		}

		private void LinkLabelOpenConfiguration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenConfigurationForm();
		}

		#endregion

		#region Notify icon

		private void NotifyIcon_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			if (Opacity == 1)
				HideForm();
		}

		private void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			if (Opacity == 0 && _formClosedAt.AddMilliseconds(500) < DateTime.Now)
				ShowForm();
		}

		#endregion

		#region System tray context menu

		private void ToolStripMenuItemSystemTrayConfiguration_Click(object sender, EventArgs e)
		{
			OpenConfigurationForm();
		}

		private void ToolStripMenuItemSystemTrayExit_Click(object sender, EventArgs e)
		{
			Settings.Client.Save(ClientSettingsType.States);
			Close();
			Environment.Exit(0);
		}

		#endregion

		#endregion

		#region Connection handler event handlers

		private void ServiceConnectionHandler_ServiceHandlerInitializationCompleted(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerInitializationCompleted), sender, e);
				return;
			}

			UpdateControlPanelFilterAndLayout();
		}

		private void ServiceConnectionHandler_ServiceHandlerConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerConnectionChanged), sender, e);
				return;
			}

			UpdateControlPanelFilterAndLayout();
		}

		#endregion

		#region Control panel node event handlers

		private void ControlPanelRootNode_SizeChanged(object sender, EventArgs e)
		{
			if (flowLayoutPanelControlPanelApplications.Controls.Count > 0)
				UpdateSize(_rootNodes.Select(node => node.Size).ToList());
		}

		private void ControlPanelNode_CheckedChanged(object sender, EventArgs e)
		{
			if (flowLayoutPanelControlPanelApplications.Controls.Count > 0)
			{
				int uncheckedCount = _rootNodes.Count(node => node.CheckState == CheckState.Unchecked);
				EnableActionLinks(uncheckedCount != _rootNodes.Count);
			}
		}

		private void ControlPanelNode_ActionTaken(object sender, ActionEventArgs e)
		{
			TakeApplicationAction((ApplicationAction) e.Action);
		}

		#endregion

		#region Configuration form event handlers

		private void ConfigurationForm_ConfigurationChanged(object sender, MachinesEventArgs e)
		{
			new Thread(UpdateControlPanelFilterAndLayout).Start();
		}

		#endregion

		#region Implementation of IProcessManagerEventHandler

		public void ProcessManagerServiceEventHandler_ApplicationStatusesChanged(object sender, ApplicationStatusesEventArgs e)
		{
			Logger.Add(LogType.Debug, "Received ApplicationStatusesChanged event: count = " + e.ApplicationStatuses.Count + e.ApplicationStatuses.Aggregate("", (x, y) => x + ", " + y.GroupID + " / " + y.ApplicationID));
			HandleApplicationStatusesChanged(e.ApplicationStatuses);
		}

		public void ProcessManagerServiceEventHandler_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e)
		{
			HandleConfigurationChanged(e.Machine, e.ConfigurationHash);
		}

		#endregion

		#region Helpers

		#region Handling Process Manager Service events

		private void HandleApplicationStatusesChanged(IEnumerable<ApplicationStatus> applicationStatuses)
		{
			new Thread(() => HandleApplicationStatusesChangedThread(applicationStatuses)).Start();
		}

		private void HandleApplicationStatusesChangedThread(IEnumerable<ApplicationStatus> applicationStatuses)
		{
			ApplyApplicationStatuses(applicationStatuses);
		}

		private void HandleConfigurationChanged(Machine machine, string configurationHash)
		{
			new Thread(() => HandleConfigurationChangedThread(machine, configurationHash)).Start();
		}

		private void HandleConfigurationChangedThread(Machine machine, string configurationHash)
		{
			if (!RaiseConfigurationChangedEvent(machine, configurationHash) && ConnectionStore.Connections[machine].Configuration.Hash != configurationHash)
				ReloadConfiguration(machine);
		}

		#endregion

		private void ShowForm()
		{
			Location = new Point(Math.Min(MousePosition.X - Size.Width / 2, Screen.PrimaryScreen.WorkingArea.Width - Size.Width - 8),
				Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 8 /* (isWindowsSeven ? 8 : 0) */);
			Opacity = 1;
			Show();
            try { Program.SetForegroundWindow(Handle); } catch { ; }
		}

		private void HideForm()
		{
			Hide();
			Opacity = 0;
			_formClosedAt = DateTime.Now;
		}

		private void ExtendGlass()
		{
			bool isGlassSupported = false;
			NativeMethods.DwmIsCompositionEnabled(ref isGlassSupported);
			if (isGlassSupported)
			{
				NativeMethods.Margins margins = new NativeMethods.Margins(0, 0, panelGlass.Height, 0);
				NativeMethods.DwmExtendFrameIntoClientArea(Handle, ref margins);
				Color keyColor = Color.FromArgb(255, 255, 0, 0);
				TransparencyKey = keyColor;
				panelGlass.BackColor = keyColor;
			}
		}

		private void DisplaySelectedTabPage()
		{
			if (tabControlSection.SelectedTab == tabPageControlPanel)
			{
				tableLayoutPanelControlPanel.Visible = true;
				tableLayoutPanelDistribution.Visible = false;
			}
			else if (tabControlSection.SelectedTab == tabPageDistribution)
			{
				tableLayoutPanelControlPanel.Visible = false;
				tableLayoutPanelDistribution.Visible = true;
			}
		}

		private void OpenConfigurationForm()
		{
			ConfigurationForm configurationForm = new ConfigurationForm();
			configurationForm.ConfigurationChanged += ConfigurationForm_ConfigurationChanged;
			configurationForm.Show();
		}

		private void ReloadConfiguration(Machine machine)
		{
			try
			{
				ConnectionStore.Connections[machine].Configuration = ConnectionStore.Connections[machine].ServiceHandler.Service.GetConfiguration().FromDTO();
				UpdateControlPanelFilterAndLayout();
			}
			catch (Exception ex)
			{
				Logger.Add("Failed to retrieve new machine configuration", ex);
			}
		}

		private static void TakeApplicationAction(ApplicationAction action)
		{
			try
			{
				if (action.Machine == null || action.Group == null || action.Application == null)
					throw new Exception("Incomplete ApplicationAction object");

				if (!ConnectionStore.ConnectionCreated(action.Machine))
					throw new Exception("No connection to machine " + action.Machine);

				ConnectionStore.Connections[action.Machine].ServiceHandler.Service.TakeApplicationAction(new DTOApplicationAction(action));
			}
			catch (Exception ex)
			{
				Logger.Add("Failed to take application action", ex);
			}
		}

		private void RetrieveAllApplicationStatuses()
		{
			new Thread(RetrieveAllApplicationStatusesThread).Start();
		}

		private void RetrieveAllApplicationStatusesThread()
		{
			List<ApplicationStatus> applicationStatuses = new List<ApplicationStatus>();
			foreach (Machine machine in Settings.Client.Machines)
			{
				try
				{
					if (!ConnectionStore.ConnectionCreated(machine))
						throw new Exception("No connection to machine " + machine);

					applicationStatuses.AddRange(ConnectionStore.Connections[machine].ServiceHandler.Service
						.GetAllApplicationStatuses().Select(x => x.FromDTO(machine)));
				}
				catch (Exception ex)
				{
					Logger.Add("Failed to retrieve all application statuses from machine " + machine, ex);
				}
			}
			ApplyApplicationStatuses(applicationStatuses);
		}

		private delegate void ApplyApplicationStatusesDelegate(IEnumerable<ApplicationStatus> applicationStatuses);

		private void ApplyApplicationStatuses(IEnumerable<ApplicationStatus> applicationStatuses)
		{
			if (InvokeRequired)
			{
				Invoke(new ApplyApplicationStatusesDelegate(ApplyApplicationStatuses), applicationStatuses);
				return;
			}

			lock (_applicationNodes)
			{
				foreach (ApplicationStatus applicationStatus in applicationStatuses)
				{
					ControlPanelApplicationNode applicationNode = _applicationNodes.FirstOrDefault(node =>
						node.Matches(applicationStatus.Machine.ID, applicationStatus.GroupID, applicationStatus.ApplicationID));

					if (applicationNode != null)
						applicationNode.Status = applicationStatus.Status;
				}
			}
		}

		private void UpdateControlPanelFilterAndLayout()
		{
			SuspendControlPanelNodeLayout();
			UpdateControlPanelFilter();
			ResumeControlPanelNodeLayout();
			LayoutControlPanelNodes();
		}

		private delegate void UpdateControlPanelFilterDelegate();

		private void UpdateControlPanelFilter()
		{
			if (InvokeRequired)
			{
				Invoke(new UpdateControlPanelFilterDelegate(UpdateControlPanelFilter));
				return;
			}

			string selectedMachineName = Settings.Client.CP_SelectedFilterMachine;
			comboBoxControlPanelMachineFilter.Items.Clear();
			comboBoxControlPanelMachineFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Machine machine in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer())
				.OrderBy(machine => machine.HostName))
			{
				int index = comboBoxControlPanelMachineFilter.Items.Add(new ComboBoxItem(machine.HostName));
				if (!string.IsNullOrEmpty(selectedMachineName) && machine.HostName.Equals(selectedMachineName, StringComparison.CurrentCultureIgnoreCase))
					comboBoxControlPanelMachineFilter.SelectedIndex = index;
			}

			string selectedGroupName = Settings.Client.CP_SelectedFilterGroup;
			comboBoxControlPanelGroupFilter.Items.Clear();
			comboBoxControlPanelGroupFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Group group in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.SelectMany(connection => connection.Configuration.Groups)
				.Distinct(new GroupEqualityComparer())
				.OrderBy(group => group.Name))
			{
				int index = comboBoxControlPanelGroupFilter.Items.Add(new ComboBoxItem(group.Name));
				if (!string.IsNullOrEmpty(selectedGroupName) && group.Name.Equals(selectedGroupName, StringComparison.CurrentCultureIgnoreCase))
					comboBoxControlPanelGroupFilter.SelectedIndex = index;
			}

			string selectedApplicationName = Settings.Client.CP_SelectedFilterApplication;
			comboBoxControlPanelApplicationFilter.Items.Clear();
			comboBoxControlPanelApplicationFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Application application in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.SelectMany(connection => connection.Configuration.Applications)
				.Distinct(new ApplicationEqualityComparer())
				.OrderBy(application => application.Name))
			{
				int index = comboBoxControlPanelApplicationFilter.Items.Add(new ComboBoxItem(application.Name));
				if (!string.IsNullOrEmpty(selectedApplicationName) && application.Name.Equals(selectedApplicationName, StringComparison.CurrentCultureIgnoreCase))
					comboBoxControlPanelApplicationFilter.SelectedIndex = index;
			}
		}

		private void SuspendControlPanelNodeLayout()
		{
			_controlPanelNodeLayoutSuspended = true;
		}

		private void ResumeControlPanelNodeLayout()
		{
			_controlPanelNodeLayoutSuspended = false;
		}

		private delegate void LayoutControlPanelNodesDelegate();

		private void LayoutControlPanelNodes()
		{
			if (_controlPanelNodeLayoutSuspended)
				return;

			if (InvokeRequired)
			{
				Invoke(new LayoutControlPanelNodesDelegate(LayoutControlPanelNodes));
				return;
			}

			//try{

			Settings.Client.Save(ClientSettingsType.States);
			ControlPanelGrouping grouping = ((ComboBoxItem<ControlPanelGrouping>) comboBoxControlPanelGroupBy.SelectedItem).Tag;

			lock (_applicationNodes)
			{
				flowLayoutPanelControlPanelApplications.Controls.Clear();
				_rootNodes.ForEach(node =>
					{
						node.SizeChanged -= ControlPanelRootNode_SizeChanged;
						node.CheckedChanged -= ControlPanelNode_CheckedChanged;
						node.ActionTaken -= ControlPanelNode_ActionTaken;
					});
				_allNodes.ForEach(node => node.Dispose());
				_allNodes.Clear();
				_rootNodes.Clear();
				_applicationNodes.Clear();

				var applications = ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.Where(connection => string.IsNullOrEmpty(Settings.Client.CP_SelectedFilterMachine) || connection.Machine.Equals(Settings.Client.CP_SelectedFilterMachine))
					.SelectMany(connection =>
						connection.Configuration.Groups
							.Where(group => string.IsNullOrEmpty(Settings.Client.CP_SelectedFilterGroup) || group.Equals(Settings.Client.CP_SelectedFilterGroup))
							.SelectMany(group => connection.Configuration.Applications
								.Where(application => group.Applications.Contains(application.ID))
								.Where(application => string.IsNullOrEmpty(Settings.Client.CP_SelectedFilterApplication) || application.Equals(Settings.Client.CP_SelectedFilterApplication))
								.Select(application => new
									{
										connection.Machine,
										Group = group,
										Application = application
									})))
					.ToList();

				switch (grouping)
				{
					case ControlPanelGrouping.MachineGroupApplication:
						{
							var machinesGroupsApplications = applications
								.GroupBy(a => a.Machine, (a, b) => new
									{
										Machine = a,
										Groups = b.GroupBy(c => c.Group, (c, d) => new
											{
												Group = c,
												Applications = d
											})
									}, new MachineEqualityComparer());

							_rootNodes.AddRange(machinesGroupsApplications.Select(machineGroupsApplications =>
								{
									IEnumerable<ControlPanelGroupNode> groupNodes = machineGroupsApplications.Groups.Select(groupApplications =>
										{
											IEnumerable<ControlPanelApplicationNode> applicationNodes = groupApplications.Applications.Select(application =>
												new ControlPanelApplicationNode(application.Application, application.Group.ID, application.Machine.ID)).ToList();
											_applicationNodes.AddRange(applicationNodes);
											return new ControlPanelGroupNode(groupApplications.Group, groupApplications.Applications.First().Machine.ID, applicationNodes, grouping);
										}).ToList(); // must make ToList() to ensure ApplicationNodes only are created once
									_allNodes.AddRange(groupNodes);
									return new ControlPanelMachineNode(machineGroupsApplications.Machine, null, groupNodes, grouping);
								}).ToList());
						}
						break;
					case ControlPanelGrouping.GroupMachineApplication:
						{
							var groupsMachinesApplications = applications
								.GroupBy(a => a.Group, (a, b) => new
									{
										Group = a,
										Machines = b.GroupBy(c => c.Machine, (c, d) => new
											{
												Machine = c,
												Applications = d
											})
									}, new GroupEqualityComparer());

							_rootNodes.AddRange(groupsMachinesApplications.Select(groupMachinesApplications =>
								{
									IEnumerable<ControlPanelMachineNode> machineNodes = groupMachinesApplications.Machines.Select(machineApplications =>
										{
											IEnumerable<ControlPanelApplicationNode> applicationNodes = machineApplications.Applications.Select(application =>
												new ControlPanelApplicationNode(application.Application, application.Group.ID, application.Machine.ID)).ToList();
											_applicationNodes.AddRange(applicationNodes);
											return new ControlPanelMachineNode(machineApplications.Machine, machineApplications.Applications.First().Group.ID, applicationNodes, grouping);
										}).ToList(); // must make ToList() to ensure ApplicationNodes only are created once
									_allNodes.AddRange(machineNodes);
									return new ControlPanelGroupNode(groupMachinesApplications.Group, null, machineNodes, grouping);
								}).ToList());
						}
						break;
				}

				_allNodes.AddRange(_rootNodes);
				_allNodes.AddRange(_applicationNodes);
				_rootNodes.ForEach(node =>
					{
						node.SizeChanged += ControlPanelRootNode_SizeChanged;
						node.CheckedChanged += ControlPanelNode_CheckedChanged;
						node.ActionTaken += ControlPanelNode_ActionTaken;
					});
			}

			if (_applicationNodes.Count > 0)
			{
				UpdateSize(_rootNodes.Select(node => node.LayoutNode()).ToList());

				_rootNodes.ForEach(node =>
					{
						node.ForceWidth(flowLayoutPanelControlPanelApplications.Size.Width);
						flowLayoutPanelControlPanelApplications.Controls.Add((UserControl) node);
					});

				Settings.Client.CP_CheckedNodes.ToList()
					.Select(id => _applicationNodes.FirstOrDefault(node => node.Matches(id)))
					.Where(node => node != null)
					.ToList().ForEach(node => node.Check(true));

				RetrieveAllApplicationStatuses();
			}
			else
			{
				UpdateSize(null);
			}

			labelControlPanelUnavailable.Visible = (_applicationNodes.Count == 0);
			//}
			//catch (Exception ex)
			//{
			//}
		}

		private void UpdateSize(List<Size> rootNodeSizes)
		{
			MinimumSize = MaximumSize = new Size(0, 0);

			if (rootNodeSizes != null)
			{
				int totalNodesHeight = rootNodeSizes.Sum(size => size.Height);
				int maxNodeWidth = rootNodeSizes.Max(size => size.Width);
				Size = new Size(Size.Width - flowLayoutPanelControlPanelApplications.Size.Width + maxNodeWidth,
					Size.Height - flowLayoutPanelControlPanelApplications.Size.Height + totalNodesHeight);
			}
			else
			{
				Size = new Size(415, 220);
			}

			MinimumSize = MaximumSize = Size;
			Location = new Point(Location.X, Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 8);
		}

		private void EnableActionLinks(bool enable)
		{
			linkLabelControlPanelStartAll.Enabled = enable;
			linkLabelControlPanelStopAll.Enabled = enable;
			linkLabelControlPanelRestartAll.Enabled = enable;
		}

		#endregion
	}
}

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
		private static readonly IDictionary<ProcessGrouping, string> _processGroupingDescriptions = new Dictionary<ProcessGrouping, string>()
			{
				{ ProcessGrouping.MachineGroupApplication, "Machine > Group > Application" },
				{ ProcessGrouping.GroupMachineApplication, "Group > Machine > Application" }
			};
		private static readonly IDictionary<DistributionGrouping, string> _distributionGroupingDescriptions = new Dictionary<DistributionGrouping, string>()
			{
				{ DistributionGrouping.MachineGroupApplicationMachine, "Machine > Group > Application > Machine" },
				//{ DistributionGrouping.MachineGroupMachine, "Machine > Group > Machine" }
			};
		private DateTime _formClosedAt;
		private readonly List<INode> _allProcessNodes;
		private readonly List<IRootNode> _processRootNodes;
		private readonly List<ProcessApplicationNode> _processApplicationNodes;
		private readonly List<INode> _allDistributionNodes;
		private readonly List<IRootNode> _distributionRootNodes;
		private readonly List<DistributionDestinationMachineNode> _distributionDestinationMachineNodes;
		private bool _processNodeLayoutSuspended;
		private bool _distributionNodeLayoutSuspended;

		public event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;

		public ControlPanelForm()
		{
			InitializeComponent();
			tabPageProcess.Tag = ControlPanelTab.Process;
			tabPageProcess.Text = tabPageProcess.Tag.ToString();
			tabPageDistribution.Tag = ControlPanelTab.Distribution;
			tabPageDistribution.Text = tabPageDistribution.Tag.ToString();
			_formClosedAt = DateTime.MinValue;
			_allProcessNodes = new List<INode>();
			_processRootNodes = new List<IRootNode>();
			_processApplicationNodes = new List<ProcessApplicationNode>();
			_allDistributionNodes = new List<INode>();
			_distributionRootNodes = new List<IRootNode>();
			_distributionDestinationMachineNodes = new List<DistributionDestinationMachineNode>();
			_processNodeLayoutSuspended = false;
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

			foreach (TabPage tabPage in tabControlSection.TabPages)
			{
				if (tabPage.Tag.ToString() == Settings.Client.CP_SelectedTab)
					tabControlSection.SelectedTab = tabPage;
			}

			foreach (ProcessGrouping grouping in _processGroupingDescriptions.Keys)
			{
				int index = comboBoxProcessGroupBy.Items.Add(new ComboBoxItem<ProcessGrouping>(_processGroupingDescriptions[grouping], grouping));
				if (Settings.Client.P_SelectedGrouping == grouping.ToString())
					comboBoxProcessGroupBy.SelectedIndex = index;
			}
			if (comboBoxProcessGroupBy.SelectedIndex == -1)
				comboBoxProcessGroupBy.SelectedIndex = 0;

			foreach (DistributionGrouping grouping in _distributionGroupingDescriptions.Keys)
			{
				int index = comboBoxDistributionGroupBy.Items.Add(new ComboBoxItem<DistributionGrouping>(_distributionGroupingDescriptions[grouping], grouping));
				if (Settings.Client.D_SelectedGrouping == grouping.ToString())
					comboBoxDistributionGroupBy.SelectedIndex = index;
			}
			if (comboBoxDistributionGroupBy.SelectedIndex == -1)
				comboBoxDistributionGroupBy.SelectedIndex = 0;

			DisplaySelectedTabPage();
			UpdateAllFiltersAndLayout();

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
			Settings.Client.CP_SelectedTab = tabControlSection.SelectedTab.Tag.ToString();
			Settings.Client.Save(ClientSettingsType.States);

			DisplaySelectedTabPage();
		}

		private void ComboBoxProcessGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProcessGroupBy.SelectedIndex == -1) return;

			ProcessGrouping grouping = ((ComboBoxItem<ProcessGrouping>) comboBoxProcessGroupBy.Items[comboBoxProcessGroupBy.SelectedIndex]).Tag;
			Settings.Client.P_SelectedGrouping = grouping.ToString();
			Settings.Client.Save(ClientSettingsType.States);

			LayoutProcessNodes();
		}

		private void ComboBoxProcessMachineFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProcessMachineFilter.SelectedIndex == -1) return;

			Settings.Client.P_SelectedFilterMachine = ((ComboBoxItem) comboBoxProcessMachineFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutProcessNodes();
		}

		private void ComboBoxProcessGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProcessGroupFilter.SelectedIndex == -1) return;

			Settings.Client.P_SelectedFilterGroup = ((ComboBoxItem) comboBoxProcessGroupFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutProcessNodes();
		}

		private void ComboBoxProcessApplicationFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProcessApplicationFilter.SelectedIndex == -1) return;

			Settings.Client.P_SelectedFilterApplication = ((ComboBoxItem) comboBoxProcessApplicationFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutProcessNodes();
		}

		private void LinkLabelProcessStartAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_processRootNodes.ForEach(node => node.TakeAction(ActionType.Start));
		}

		private void LinkLabelProcessStopAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_processRootNodes.ForEach(node => node.TakeAction(ActionType.Stop));
		}

		private void LinkLabelProcessRestartAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_processRootNodes.ForEach(node => node.TakeAction(ActionType.Restart));
		}

		private void LinkLabelProcessExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_processRootNodes.ForEach(node => node.ExpandAll(true));
		}

		private void LinkLabelProcessCollapseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_processRootNodes.ForEach(node => node.ExpandAll(false));
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

			UpdateAllFiltersAndLayout();
		}

		private void ServiceConnectionHandler_ServiceHandlerConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerConnectionChanged), sender, e);
				return;
			}

			UpdateAllFiltersAndLayout();
		}

		#endregion

		#region Node event handlers

		private void RootNode_SizeChanged(object sender, EventArgs e)
		{
			FlowLayoutPanel flowLayoutPanel;
			List<IRootNode> rootNodes;
			switch ((ControlPanelTab) tabControlSection.SelectedTab.Tag)
			{
				case ControlPanelTab.Process:
					flowLayoutPanel = flowLayoutPanelProcessApplications;
					rootNodes = _processRootNodes;
					break;
				case ControlPanelTab.Distribution:
					flowLayoutPanel = flowLayoutPanelDistributionDestinations;
					rootNodes = _distributionRootNodes;
					break;
				default:
					return;
			}

			if (flowLayoutPanel.Controls.Count > 0)
				UpdateSize(rootNodes.Select(node => node.Size).ToList(), flowLayoutPanel);
		}

		private void Node_CheckedChanged(object sender, EventArgs e)
		{
			FlowLayoutPanel flowLayoutPanel;
			List<IRootNode> rootNodes;
			switch ((ControlPanelTab) tabControlSection.SelectedTab.Tag)
			{
				case ControlPanelTab.Process:
					flowLayoutPanel = flowLayoutPanelProcessApplications;
					rootNodes = _processRootNodes;
					break;
				case ControlPanelTab.Distribution:
					flowLayoutPanel = flowLayoutPanelDistributionDestinations;
					rootNodes = _distributionRootNodes;
					break;
				default:
					return;
			}

			if (flowLayoutPanel.Controls.Count > 0)
			{
				int uncheckedCount = rootNodes.Count(node => node.CheckState == CheckState.Unchecked);
				EnableActionLinks((ControlPanelTab) tabControlSection.SelectedTab.Tag, uncheckedCount != rootNodes.Count);
			}
		}

		private void Node_ActionTaken(object sender, ActionEventArgs e)
		{
			TakeAction((ControlPanelTab) tabControlSection.SelectedTab.Tag, e.Action);
		}

		#endregion

		#region Configuration form event handlers

		private void ConfigurationForm_ConfigurationChanged(object sender, MachinesEventArgs e)
		{
			new Thread(UpdateAllFiltersAndLayout).Start();
		}

		#endregion

		#region Implementation of IProcessManagerEventHandler

		public void ProcessManagerServiceEventHandler_ProcessStatusesChanged(object sender, ProcessStatusesEventArgs e)
		{
			Logger.Add(LogType.Debug, "Received ProcessStatusesChanged event: count = " + e.ProcessStatuses.Count + e.ProcessStatuses.Aggregate("", (x, y) => x + ", " + y.GroupID + " / " + y.ApplicationID));
			HandleProcessStatusesChanged(e.ProcessStatuses);
		}

		public void ProcessManagerServiceEventHandler_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e)
		{
			HandleConfigurationChanged(e.Machine, e.ConfigurationHash);
		}

		#endregion

		#region Helpers

		#region Handling Process Manager Service events

		private void HandleProcessStatusesChanged(IEnumerable<ProcessStatus> processStatuses)
		{
			new Thread(() => HandleProcessStatusesChangedThread(processStatuses)).Start();
		}

		private void HandleProcessStatusesChangedThread(IEnumerable<ProcessStatus> processStatuses)
		{
			ApplyProcessStatuses(processStatuses);
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
			if (tabControlSection.SelectedTab == tabPageProcess)
			{
				tableLayoutPanelProcess.Visible = true;
				tableLayoutPanelDistribution.Visible = false;
			}
			else if (tabControlSection.SelectedTab == tabPageDistribution)
			{
				tableLayoutPanelProcess.Visible = false;
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
				UpdateAllFiltersAndLayout();
			}
			catch (Exception ex)
			{
				Logger.Add("Failed to retrieve new machine configuration", ex);
			}
		}

		private void UpdateAllFiltersAndLayout()
		{
			UpdateProcessFilterAndLayout();
			UpdateDistributionFilterAndLayout();
		}

		#region Process tab

		private void RetrieveAllProcessStatuses()
		{
			new Thread(RetrieveAllProcessStatusesThread).Start();
		}

		private void RetrieveAllProcessStatusesThread()
		{
			List<ProcessStatus> processStatuses = new List<ProcessStatus>();
			foreach (Machine machine in Settings.Client.Machines)
			{
				try
				{
					if (!ConnectionStore.ConnectionCreated(machine))
						throw new Exception("No connection to machine " + machine);

					processStatuses.AddRange(ConnectionStore.Connections[machine].ServiceHandler.Service
						.GetAllProcessStatuses().Select(x => x.FromDTO(machine)));
				}
				catch (Exception ex)
				{
					Logger.Add("Failed to retrieve all process statuses from machine " + machine, ex);
				}
			}
			ApplyProcessStatuses(processStatuses);
		}

		private delegate void ApplyProcessStatusesDelegate(IEnumerable<ProcessStatus> processStatuses);

		private void ApplyProcessStatuses(IEnumerable<ProcessStatus> processStatuses)
		{
			if (InvokeRequired)
			{
				Invoke(new ApplyProcessStatusesDelegate(ApplyProcessStatuses), processStatuses);
				return;
			}

			lock (_processApplicationNodes)
			{
				foreach (ProcessStatus processStatus in processStatuses)
				{
					ProcessApplicationNode applicationNode = _processApplicationNodes.FirstOrDefault(node =>
						node.Matches(processStatus.Machine.ID, processStatus.GroupID, processStatus.ApplicationID));

					if (applicationNode != null)
						applicationNode.Status = processStatus.Value;
				}
			}
		}

		private void UpdateProcessFilterAndLayout()
		{
			SuspendProcessNodeLayout();
			UpdateProcessFilter();
			ResumeProcessNodeLayout();
			LayoutProcessNodes();
		}

		private delegate void UpdateProcessFilterDelegate();

		private void UpdateProcessFilter()
		{
			if (InvokeRequired)
			{
				Invoke(new UpdateProcessFilterDelegate(UpdateProcessFilter));
				return;
			}

			string selectedMachineName = Settings.Client.P_SelectedFilterMachine;
			comboBoxProcessMachineFilter.Items.Clear();
			comboBoxProcessMachineFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Machine machine in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer())
				.OrderBy(machine => machine.HostName))
			{
				int index = comboBoxProcessMachineFilter.Items.Add(new ComboBoxItem(machine.HostName));
				if (!string.IsNullOrEmpty(selectedMachineName) && machine.HostName.Equals(selectedMachineName, StringComparison.CurrentCultureIgnoreCase))
					comboBoxProcessMachineFilter.SelectedIndex = index;
			}

			string selectedGroupName = Settings.Client.P_SelectedFilterGroup;
			comboBoxProcessGroupFilter.Items.Clear();
			comboBoxProcessGroupFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Group group in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.SelectMany(connection => connection.Configuration.Groups)
				.Distinct(new GroupEqualityComparer())
				.OrderBy(group => group.Name))
			{
				int index = comboBoxProcessGroupFilter.Items.Add(new ComboBoxItem(group.Name));
				if (!string.IsNullOrEmpty(selectedGroupName) && group.Name.Equals(selectedGroupName, StringComparison.CurrentCultureIgnoreCase))
					comboBoxProcessGroupFilter.SelectedIndex = index;
			}

			string selectedApplicationName = Settings.Client.P_SelectedFilterApplication;
			comboBoxProcessApplicationFilter.Items.Clear();
			comboBoxProcessApplicationFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Application application in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.SelectMany(connection => connection.Configuration.Applications)
				.Distinct(new ApplicationEqualityComparer())
				.OrderBy(application => application.Name))
			{
				int index = comboBoxProcessApplicationFilter.Items.Add(new ComboBoxItem(application.Name));
				if (!string.IsNullOrEmpty(selectedApplicationName) && application.Name.Equals(selectedApplicationName, StringComparison.CurrentCultureIgnoreCase))
					comboBoxProcessApplicationFilter.SelectedIndex = index;
			}
		}

		private void SuspendProcessNodeLayout()
		{
			_processNodeLayoutSuspended = true;
		}

		private void ResumeProcessNodeLayout()
		{
			_processNodeLayoutSuspended = false;
		}

		private delegate void LayoutProcessNodesDelegate();

		private void LayoutProcessNodes()
		{
			if (_processNodeLayoutSuspended)
				return;

			if (InvokeRequired)
			{
				Invoke(new LayoutProcessNodesDelegate(LayoutProcessNodes));
				return;
			}

			//try{

			Settings.Client.Save(ClientSettingsType.States);
			ProcessGrouping grouping = ((ComboBoxItem<ProcessGrouping>) comboBoxProcessGroupBy.SelectedItem).Tag;

			lock (_processApplicationNodes)
			{
				flowLayoutPanelProcessApplications.Controls.Clear();
				_processRootNodes.ForEach(node =>
					{
						node.SizeChanged -= RootNode_SizeChanged;
						node.CheckedChanged -= Node_CheckedChanged;
						node.ActionTaken -= Node_ActionTaken;
					});
				_allProcessNodes.ForEach(node => node.Dispose());
				_allProcessNodes.Clear();
				_processRootNodes.Clear();
				_processApplicationNodes.Clear();

				var applications = ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.Where(connection => string.IsNullOrEmpty(Settings.Client.P_SelectedFilterMachine) || connection.Machine.Equals(Settings.Client.P_SelectedFilterMachine))
					.SelectMany(connection =>
						connection.Configuration.Groups
							.Where(group => string.IsNullOrEmpty(Settings.Client.P_SelectedFilterGroup) || group.Equals(Settings.Client.P_SelectedFilterGroup))
							.SelectMany(group => connection.Configuration.Applications
								.Where(application => group.Applications.Contains(application.ID))
								.Where(application => string.IsNullOrEmpty(Settings.Client.P_SelectedFilterApplication) || application.Equals(Settings.Client.P_SelectedFilterApplication))
								.Select(application => new
									{
										connection.Machine,
										Group = group,
										Application = application
									})))
					.ToList();

				switch (grouping)
				{
					case ProcessGrouping.MachineGroupApplication:
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

							_processRootNodes.AddRange(machinesGroupsApplications.Select(machineGroupsApplications =>
								{
									IEnumerable<ProcessGroupNode> groupNodes = machineGroupsApplications.Groups.Select(groupApplications =>
										{
											IEnumerable<ProcessApplicationNode> applicationNodes = groupApplications.Applications.Select(application =>
												new ProcessApplicationNode(application.Application, application.Group.ID, application.Machine.ID)).ToList();
											_processApplicationNodes.AddRange(applicationNodes);
											return new ProcessGroupNode(groupApplications.Group, groupApplications.Applications.First().Machine.ID, applicationNodes, grouping);
										}).ToList(); // must make ToList() to ensure ApplicationNodes only are created once
									_allProcessNodes.AddRange(groupNodes);
									return new ProcessMachineNode(machineGroupsApplications.Machine, null, groupNodes, grouping);
								}).ToList());
						}
						break;
					case ProcessGrouping.GroupMachineApplication:
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

							_processRootNodes.AddRange(groupsMachinesApplications.Select(groupMachinesApplications =>
								{
									IEnumerable<ProcessMachineNode> machineNodes = groupMachinesApplications.Machines.Select(machineApplications =>
										{
											IEnumerable<ProcessApplicationNode> applicationNodes = machineApplications.Applications.Select(application =>
												new ProcessApplicationNode(application.Application, application.Group.ID, application.Machine.ID)).ToList();
											_processApplicationNodes.AddRange(applicationNodes);
											return new ProcessMachineNode(machineApplications.Machine, machineApplications.Applications.First().Group.ID, applicationNodes, grouping);
										}).ToList(); // must make ToList() to ensure ApplicationNodes only are created once
									_allProcessNodes.AddRange(machineNodes);
									return new ProcessGroupNode(groupMachinesApplications.Group, null, machineNodes, grouping);
								}).ToList());
						}
						break;
				}

				_allProcessNodes.AddRange(_processRootNodes);
				_allProcessNodes.AddRange(_processApplicationNodes);
				_processRootNodes.ForEach(node =>
					{
						node.SizeChanged += RootNode_SizeChanged;
						node.CheckedChanged += Node_CheckedChanged;
						node.ActionTaken += Node_ActionTaken;
					});
			}

			if (_processApplicationNodes.Count > 0)
			{
				UpdateSize(_processRootNodes.Select(node => node.LayoutNode()).ToList(), flowLayoutPanelProcessApplications);

				_processRootNodes.ForEach(node =>
					{
						node.ForceWidth(flowLayoutPanelProcessApplications.Size.Width);
						flowLayoutPanelProcessApplications.Controls.Add((UserControl) node);
					});

				Settings.Client.P_CheckedNodes.ToList()
					.Select(id => _processApplicationNodes.FirstOrDefault(node => node.Matches(id)))
					.Where(node => node != null)
					.ToList().ForEach(node => node.Check(true));

				RetrieveAllProcessStatuses();
			}
			else
			{
				UpdateSize(null, null);
			}

			labelProcessUnavailable.Visible = (_processApplicationNodes.Count == 0);
			//}
			//catch (Exception ex)
			//{
			//}
		}

		#endregion

		#region Distribution tab

		private void UpdateDistributionFilterAndLayout()
		{
			SuspendDistributionNodeLayout();
			UpdateDistributionFilter();
			ResumeDistributionNodeLayout();
			LayoutDistributionNodes();
		}

		private delegate void UpdateDistributionFilterDelegate();

		private void UpdateDistributionFilter()
		{
			if (InvokeRequired)
			{
				Invoke(new UpdateDistributionFilterDelegate(UpdateDistributionFilter));
				return;
			}

			//string selectedMachineName = Settings.Client.P_SelectedFilterMachine;
			//comboBoxProcessMachineFilter.Items.Clear();
			//comboBoxProcessMachineFilter.Items.Add(new ComboBoxItem(string.Empty));

			//foreach (Machine machine in ConnectionStore.Connections.Values
			//	.Where(connection => connection.Configuration != null)
			//	.Select(connection => connection.Machine)
			//	.Distinct(new MachineEqualityComparer())
			//	.OrderBy(machine => machine.HostName))
			//{
			//	int index = comboBoxProcessMachineFilter.Items.Add(new ComboBoxItem(machine.HostName));
			//	if (!string.IsNullOrEmpty(selectedMachineName) && machine.HostName.Equals(selectedMachineName, StringComparison.CurrentCultureIgnoreCase))
			//		comboBoxProcessMachineFilter.SelectedIndex = index;
			//}

			//string selectedGroupName = Settings.Client.P_SelectedFilterGroup;
			//comboBoxProcessGroupFilter.Items.Clear();
			//comboBoxProcessGroupFilter.Items.Add(new ComboBoxItem(string.Empty));

			//foreach (Group group in ConnectionStore.Connections.Values
			//	.Where(connection => connection.Configuration != null)
			//	.SelectMany(connection => connection.Configuration.Groups)
			//	.Distinct(new GroupEqualityComparer())
			//	.OrderBy(group => group.Name))
			//{
			//	int index = comboBoxProcessGroupFilter.Items.Add(new ComboBoxItem(group.Name));
			//	if (!string.IsNullOrEmpty(selectedGroupName) && group.Name.Equals(selectedGroupName, StringComparison.CurrentCultureIgnoreCase))
			//		comboBoxProcessGroupFilter.SelectedIndex = index;
			//}

			//string selectedApplicationName = Settings.Client.P_SelectedFilterApplication;
			//comboBoxProcessApplicationFilter.Items.Clear();
			//comboBoxProcessApplicationFilter.Items.Add(new ComboBoxItem(string.Empty));

			//foreach (Application application in ConnectionStore.Connections.Values
			//	.Where(connection => connection.Configuration != null)
			//	.SelectMany(connection => connection.Configuration.Applications)
			//	.Distinct(new ApplicationEqualityComparer())
			//	.OrderBy(application => application.Name))
			//{
			//	int index = comboBoxProcessApplicationFilter.Items.Add(new ComboBoxItem(application.Name));
			//	if (!string.IsNullOrEmpty(selectedApplicationName) && application.Name.Equals(selectedApplicationName, StringComparison.CurrentCultureIgnoreCase))
			//		comboBoxProcessApplicationFilter.SelectedIndex = index;
			//}
		}

		private void SuspendDistributionNodeLayout()
		{
			_distributionNodeLayoutSuspended = true;
		}

		private void ResumeDistributionNodeLayout()
		{
			_distributionNodeLayoutSuspended = false;
		}

		private delegate void LayoutDistributionNodesDelegate();

		private void LayoutDistributionNodes()
		{
			if (_distributionNodeLayoutSuspended)
				return;

			if (InvokeRequired)
			{
				Invoke(new LayoutDistributionNodesDelegate(LayoutDistributionNodes));
				return;
			}

			//try{

			Settings.Client.Save(ClientSettingsType.States);
			DistributionGrouping grouping = DistributionGrouping.MachineGroupApplicationMachine;
				//((ComboBoxItem<DistributionGrouping>) comboBoxDistributionGroupBy.SelectedItem).Tag;

			lock (_distributionDestinationMachineNodes)
			{
				flowLayoutPanelDistributionDestinations.Controls.Clear();
				_distributionRootNodes.ForEach(node =>
					{
						node.SizeChanged -= RootNode_SizeChanged;
						node.CheckedChanged -= Node_CheckedChanged;
						node.ActionTaken -= Node_ActionTaken;
					});
				_allDistributionNodes.ForEach(node => node.Dispose());
				_allDistributionNodes.Clear();
				_distributionRootNodes.Clear();
				_distributionDestinationMachineNodes.Clear();

				var destinationMachines = ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.Where(connection => string.IsNullOrEmpty(Settings.Client.D_SelectedFilterMachine) || connection.Machine.Equals(Settings.Client.D_SelectedFilterMachine))
					.SelectMany(connection =>
						connection.Configuration.Groups
							.Where(group => string.IsNullOrEmpty(Settings.Client.D_SelectedFilterGroup) || group.Equals(Settings.Client.D_SelectedFilterGroup))
							.SelectMany(group => connection.Configuration.Applications
								.Where(application => group.Applications.Contains(application.ID))
								.Where(application => string.IsNullOrEmpty(Settings.Client.D_SelectedFilterApplication) || application.Equals(Settings.Client.D_SelectedFilterApplication))
								.SelectMany(application => ConnectionStore.Connections.Values
									.Select(destination => new
										{
											SourceMachine = connection.Machine,
											Group = group,
											Application = application,
											DestinationMachine = destination.Machine
										}))))
					.ToList();

				switch (grouping)
				{
					case DistributionGrouping.MachineGroupApplicationMachine:
						{
							var machinesGroupsApplicationsMachines = destinationMachines
								.GroupBy(a => a.SourceMachine, (a, b) => new
									{
										SourceMachine = a,
										Groups = b.GroupBy(c => c.Group, (c, d) => new
											{
												Group = c,
												Applications = d.GroupBy(e => e.Application, (e, f) => new
													{
														Application = e,
														DestinationMachines = f
													})
											})
									}, new MachineEqualityComparer());

							_distributionRootNodes.AddRange(machinesGroupsApplicationsMachines.Select(machineGroupsApplicationsMachines =>
								{
									IEnumerable<DistributionGroupNode> groupNodes = machineGroupsApplicationsMachines.Groups.Select(groupApplicationsMachines =>
										{
											IEnumerable<DistributionApplicationNode> applicationNodes = groupApplicationsMachines.Applications.Select(applicationMachines =>
												{
													IEnumerable<DistributionDestinationMachineNode> destinationMachineNodes = applicationMachines.DestinationMachines.Select(machine =>
														new DistributionDestinationMachineNode(machine.DestinationMachine, machine.Application.ID, machine.Group.ID, machine.SourceMachine.ID)).ToList();
													_distributionDestinationMachineNodes.AddRange(destinationMachineNodes);
													return new DistributionApplicationNode(applicationMachines.Application, destinationMachineNodes, grouping);
												}).ToList(); // must make ToList() to ensure DestinationMachineNodes only are created once
											_allDistributionNodes.AddRange(applicationNodes);
											return new DistributionGroupNode(groupApplicationsMachines.Group, null, applicationNodes, grouping);
										}).ToList(); // must make ToList() to ensure ApplicationNodes only are created once
									_allDistributionNodes.AddRange(groupNodes);
									return new DistributionSourceMachineNode(machineGroupsApplicationsMachines.SourceMachine, null, groupNodes, grouping);
								}).ToList());
						}
						break;
				}

				_allDistributionNodes.AddRange(_distributionRootNodes);
				_allDistributionNodes.AddRange(_distributionDestinationMachineNodes);
				_distributionRootNodes.ForEach(node =>
					{
						node.SizeChanged += RootNode_SizeChanged;
						node.CheckedChanged += Node_CheckedChanged;
						node.ActionTaken += Node_ActionTaken;
					});
			}

			if (_distributionDestinationMachineNodes.Count > 0)
			{
				UpdateSize(_distributionRootNodes.Select(node => node.LayoutNode()).ToList(), flowLayoutPanelDistributionDestinations);

				_distributionRootNodes.ForEach(node =>
					{
						node.ForceWidth(flowLayoutPanelDistributionDestinations.Size.Width);
						flowLayoutPanelDistributionDestinations.Controls.Add((UserControl) node);
					});

				Settings.Client.D_CheckedNodes.ToList()
					.Select(id => _distributionDestinationMachineNodes.FirstOrDefault(node => node.Matches(id)))
					.Where(node => node != null)
					.ToList().ForEach(node => node.Check(true));

				//RetrieveAllProcessStatuses();
			}
			else
			{
				UpdateSize(null, null);
			}

			labelDistributionUnavailable.Visible = (_distributionDestinationMachineNodes.Count == 0);
			//}
			//catch (Exception ex)
			//{
			//}
		}

		#endregion

		private void UpdateSize(List<Size> rootNodeSizes, FlowLayoutPanel flowLayoutPanel)
		{
			MinimumSize = MaximumSize = new Size(0, 0);

			if (rootNodeSizes != null)
			{
				int totalNodesHeight = rootNodeSizes.Sum(size => size.Height);
				int maxNodeWidth = rootNodeSizes.Max(size => size.Width);
				Size = new Size(Size.Width - flowLayoutPanel.Size.Width + maxNodeWidth,
					Size.Height - flowLayoutPanel.Size.Height + totalNodesHeight);
			}
			else
			{
				Size = new Size(415, 220);
			}

			MinimumSize = MaximumSize = Size;
			Location = new Point(Location.X, Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 8);
		}

		private void EnableActionLinks(ControlPanelTab tab, bool enable)
		{
			switch (tab)
			{
				case ControlPanelTab.Process:
					linkLabelProcessStartAll.Enabled = enable;
					linkLabelProcessStopAll.Enabled = enable;
					linkLabelProcessRestartAll.Enabled = enable;
					break;
				case ControlPanelTab.Distribution:
					linkLabelDistributionDistributeAll.Enabled = enable;
					break;
			}
		}

		private static void TakeAction(ControlPanelTab tab, IAction action)
		{
			try
			{
				switch (tab)
				{
					case ControlPanelTab.Process:
						{
							ProcessAction processAction = action as ProcessAction;

							if (processAction == null)
								return;

							if (processAction.Machine == null || processAction.Group == null || processAction.Application == null)
								throw new Exception("Incomplete ProcessAction object");

							if (!ConnectionStore.ConnectionCreated(processAction.Machine))
								throw new Exception("No connection to machine " + processAction.Machine);

							ConnectionStore.Connections[processAction.Machine].ServiceHandler.Service.TakeProcessAction(new DTOProcessAction(processAction));
						}
						break;
					case ControlPanelTab.Distribution:
						{
							DistributionAction distributionAction = action as DistributionAction;

							if (distributionAction == null)
								return;

						}
						break;
				}
			}
			catch (Exception ex)
			{
				Logger.Add("Failed to take action", ex);
			}
		}

		#endregion
	}
}

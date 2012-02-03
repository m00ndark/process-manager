using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;
using ProcessManager.Service.DataObjects;
using ProcessManager.Utilities;
using ProcessManagerUI.Controls.Nodes;
using ProcessManagerUI.Controls.Nodes.Support;
using ProcessManagerUI.Support;

namespace ProcessManagerUI.Forms
{
	public enum Grouping
	{
		MachineGroupApplication,
		GroupMachineApplication
	}

	public partial class ControlPanelForm : Form, IProcessManagerEventHandler
	{
		private static readonly IDictionary<Grouping, string> _groupingDescriptions = new Dictionary<Grouping, string>()
			{
				{ Grouping.MachineGroupApplication, "Machine > Group > Application" },
				{ Grouping.GroupMachineApplication, "Group > Machine > Application" }
			};
		private DateTime _formClosedAt;
		private readonly List<IControlPanelNode> _allNodes;
		private readonly List<IControlPanelRootNode> _rootNodes;
		private readonly List<ApplicationNode> _applicationNodes;

		public event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;

		public ControlPanelForm()
		{
			InitializeComponent();
			_formClosedAt = DateTime.MinValue;
			_allNodes = new List<IControlPanelNode>();
			_rootNodes = new List<IControlPanelRootNode>();
			_applicationNodes = new List<ApplicationNode>();
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
			Settings.Client.Load();

			foreach (Grouping grouping in _groupingDescriptions.Keys)
			{
				int index = comboBoxGroupBy.Items.Add(new ComboBoxItem<Grouping>(_groupingDescriptions[grouping], grouping));
				if (Settings.Client.CP_SelectedGrouping == grouping.ToString())
					comboBoxGroupBy.SelectedIndex = index;
			}
			if (comboBoxGroupBy.SelectedIndex == -1)
				comboBoxGroupBy.SelectedIndex = 0;

			LayoutNodes();

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

		private void ComboBoxGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxGroupBy.SelectedIndex == -1) return;

			Grouping grouping = ((ComboBoxItem<Grouping>) comboBoxGroupBy.Items[comboBoxGroupBy.SelectedIndex]).Tag;
			Settings.Client.CP_SelectedGrouping = grouping.ToString();
			Settings.Client.Save(ClientSettingsType.States);
		}

		private void ComboBoxMachineFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.Client.Save(ClientSettingsType.States);
		}

		private void ComboBoxGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.Client.Save(ClientSettingsType.States);
		}

		private void ComboBoxApplicationFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.Client.Save(ClientSettingsType.States);
		}

		private void LinkLabelStartAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_rootNodes.ForEach(node => node.TakeAction(ApplicationActionType.Start));
		}

		private void LinkLabelStopAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_rootNodes.ForEach(node => node.TakeAction(ApplicationActionType.Stop));
		}

		private void LinkLabelRestartAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_rootNodes.ForEach(node => node.TakeAction(ApplicationActionType.Restart));
		}

		private void LinkLabelExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_rootNodes.ForEach(node => node.ExpandAll(true));
		}

		private void LinkLabelCollapseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_rootNodes.ForEach(node => node.ExpandAll(false));
		}

		private void LinkLabelOpenConfiguration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			new ConfigurationForm(this).Show();
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
			new ConfigurationForm(this).Show();
		}

		private void ToolStripMenuItemSystemTrayExit_Click(object sender, EventArgs e)
		{
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

			LayoutNodes();
		}

		private void ServiceConnectionHandler_ServiceHandlerConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerConnectionChanged), sender, e);
				return;
			}

			LayoutNodes();
		}

		#endregion

		#region Control panel node event handlers

		private void ControlPanelRootNode_SizeChanged(object sender, EventArgs e)
		{
			if (flowLayoutPanelApplications.Controls.Count > 0)
				UpdateSize(_rootNodes.Select(node => node.Size).ToList());
		}

		private void ControlPanelNode_CheckedChanged(object sender, EventArgs e)
		{
			if (flowLayoutPanelApplications.Controls.Count > 0)
			{
				int uncheckedCount = _rootNodes.Count(node => node.CheckState == CheckState.Unchecked);
				EnableActionLinks(uncheckedCount != _rootNodes.Count);
			}
		}

		private void ControlPanelNode_ActionTaken(object sender, ApplicationActionEventArgs e)
		{
			TakeAction(e.Action);
		}

		#endregion

		#region Implementation of IProcessManagerEventHandler

		public void ProcessManagerServiceEventHandler_ApplicationStatusesChanged(object sender, ApplicationStatusesEventArgs e)
		{
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

		private void ReloadConfiguration(Machine machine)
		{
			try
			{
				ConnectionStore.Connections[machine].Configuration = ConnectionStore.Connections[machine].ServiceHandler.Service.GetConfiguration().FromDTO();
				LayoutNodes();
			}
			catch (Exception ex)
			{
				Logger.Add("Failed to retrieve new machine configuration", ex);
			}
		}

		private static void TakeAction(ApplicationAction action)
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
					ApplicationNode applicationNode = _applicationNodes.FirstOrDefault(node => node.ID == applicationStatus.ApplicationID
						&& node.GroupID == applicationStatus.GroupID && node.MachineID == applicationStatus.Machine.ID);

					if (applicationNode != null)
						applicationNode.Status = applicationStatus.Status;
				}
			}
		}

		private void LayoutNodes()
		{
			//try{

			lock (_applicationNodes)
			{
				flowLayoutPanelApplications.Controls.Clear();
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

				var machinesGroupsApplications = ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.SelectMany(connection =>
						connection.Configuration.Groups.SelectMany(group =>
							connection.Configuration.Applications
								.Where(application => group.Applications.Contains(application.ID))
								.Select(application => new
									{
										connection.Machine,
										Group = group,
										Application = application
									})))
					.GroupBy(a => a.Machine, (a, b) => new
						{
							Machine = a,
							Groups = b.GroupBy(c => c.Group, (c, d) => new
								{
									Group = c,
									Applications = d.Select(e => e.Application)
								})
						});

				_rootNodes.AddRange(machinesGroupsApplications.Select(machineGroupsApplications =>
					{
						IEnumerable<GroupNode> groupNodes = machineGroupsApplications.Groups.Select(groupApplications =>
							{
								IEnumerable<ApplicationNode> applicationNodes = groupApplications.Applications.Select(application =>
									new ApplicationNode(application, groupApplications.Group.ID, machineGroupsApplications.Machine.ID)).ToList();
								_applicationNodes.AddRange(applicationNodes);
								return new GroupNode(groupApplications.Group, applicationNodes);
							}).ToList(); // must make ToList() to ensure ApplicationNodes only are created once
						_allNodes.AddRange(groupNodes);
						return new MachineNode(machineGroupsApplications.Machine, groupNodes);
					}).ToList());

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
						node.ForceWidth(flowLayoutPanelApplications.Size.Width);
						flowLayoutPanelApplications.Controls.Add((UserControl) node);
					});

				RetrieveAllApplicationStatuses();
			}
			else
			{
				UpdateSize(null);
			}

			labelUnavailable.Visible = (_applicationNodes.Count == 0);
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
				Size = new Size(Size.Width - flowLayoutPanelApplications.Size.Width + maxNodeWidth,
					Size.Height - flowLayoutPanelApplications.Size.Height + totalNodesHeight);
			}
			else
			{
				Size = new Size(415, 180);
			}

			MinimumSize = MaximumSize = Size;
			Location = new Point(Location.X, Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 8);
		}

		private void EnableActionLinks(bool enable)
		{
			linkLabelStartAll.Enabled = enable;
			linkLabelStopAll.Enabled = enable;
			linkLabelRestartAll.Enabled = enable;
		}

		#endregion
	}
}

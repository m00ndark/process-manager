using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;
using ProcessManager.Utilities;
using ProcessManagerUI.Controls.Nodes;
using ProcessManagerUI.Support;
using ProcessManagerUI.Utilities;
using Application = ProcessManager.DataObjects.Application;

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
		private readonly List<ApplicationNode> _applicationNodes;

		public event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;

		public ControlPanelForm()
		{
			InitializeComponent();
			_formClosedAt = DateTime.MinValue;
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

		private void HandleApplicationStatusesChanged(List<ApplicationStatus> applicationStatuses)
		{
			new Thread(() => HandleApplicationStatusesChangedThread(applicationStatuses)).Start();
		}

		private void HandleApplicationStatusesChangedThread(List<ApplicationStatus> applicationStatuses)
		{

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

		private void LayoutNodes()
		{
			flowLayoutPanelApplications.Controls.Clear();
			_applicationNodes.Clear();

			var machinesGroupsApplications = ConnectionStore.Connections.Values.SelectMany(connection =>
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

			List<IControlPanelNode> rootNodes = machinesGroupsApplications.Select(machineGroupsApplications =>
				new MachineNode(machineGroupsApplications.Machine,
					machineGroupsApplications.Groups.Select(groupApplications =>
						{
							IEnumerable<ApplicationNode> applicationNodes = groupApplications.Applications.Select(application => new ApplicationNode(application));
							_applicationNodes.AddRange(applicationNodes);
							return new GroupNode(groupApplications.Group, applicationNodes);
						}))).Cast<IControlPanelNode>().ToList();

			if (_applicationNodes.Count > 0)
			{
				List<Size> rootNodeSizes = rootNodes.Select(node => node.LayoutNode()).ToList();
				int totalNodesHeight = rootNodeSizes.Sum(size => size.Height);
				int maxNodeWidth = rootNodeSizes.Max(size => size.Width);

				Size = new Size(Size.Width - flowLayoutPanelApplications.Size.Width + maxNodeWidth,
					Size.Height - flowLayoutPanelApplications.Size.Height + totalNodesHeight);

				// todo: MinimumSize, MaximumSize ...

				rootNodes.ForEach(node => flowLayoutPanelApplications.Controls.Add((UserControl) node));
			}

			labelUnavailable.Visible = (_applicationNodes.Count == 0);
		}

		#endregion
	}
}

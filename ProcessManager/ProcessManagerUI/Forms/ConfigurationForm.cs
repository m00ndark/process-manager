using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;
using ProcessManagerUI.Utilities;

namespace ProcessManagerUI.Forms
{
	public partial class ConfigurationForm : Form
	{
		private readonly IProcessManagerEventHandler _processManagerEventHandler;
		private readonly Timer _initTimer;
		private Panel _currentPanel;
		private bool _hasUnsavedConfiguration;

		public ConfigurationForm(IProcessManagerEventHandler processManagerEventHandler)
		{
			InitializeComponent();
			_processManagerEventHandler = processManagerEventHandler;
			_currentPanel = null;
			_hasUnsavedConfiguration = false;
			_initTimer = new Timer() { Enabled = false, Interval = 100 };
			_initTimer.Tick += InitTimer_Tick;
		}

		#region Timer event handlers

		private void InitTimer_Tick(object sender, EventArgs e)
		{
			_initTimer.Stop();
			ConnectMachines();
			SelectInitialMachine();
		}

		#endregion

		#region GUI event handlers

		private void ConfigurationForm_Load(object sender, EventArgs e)
		{
			Settings.Client.Load();
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerInitializationCompleted += ServiceConnectionHandler_ServiceHandlerInitializationCompleted;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged += ServiceConnectionHandler_ServiceHandlerConnectionChanged;
			ShowAllControls();
			ClearAllControls();
			PopulateMachinesComboBox(false);
			TreeNode setupNode = new TreeNode("Setup") { Name = "Setup" };
			TreeNode groupsNode = new TreeNode("Groups") { Name = "Groups", Tag = panelGroups };
			TreeNode applicationsNode = new TreeNode("Applications") { Name = "Applications", Tag = panelApplications };
			TreeNode pluginsNode = new TreeNode("Plugins") { Name = "Plugins", Tag = panelPlugins };
			treeViewConfiguration.Nodes.Add(setupNode);
			treeViewConfiguration.Nodes.Add(pluginsNode);
			setupNode.Nodes.Add(groupsNode);
			setupNode.Nodes.Add(applicationsNode);
			treeViewConfiguration.ExpandAll();
			TreeNode[] matches =  treeViewConfiguration.Nodes.Find(Settings.Client.CFG_SelectedConfigurationSection, true);
			treeViewConfiguration.SelectedNode = (matches.Length > 0 ? matches[0] : groupsNode);
			_initTimer.Start();
		}

		private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerInitializationCompleted -= ServiceConnectionHandler_ServiceHandlerInitializationCompleted;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged -= ServiceConnectionHandler_ServiceHandlerConnectionChanged;
			Settings.Client.CFG_SelectedConfigurationSection =
				(treeViewConfiguration.SelectedNode != null ? treeViewConfiguration.SelectedNode.Name : Settings.Client.Defaults.SELECTED_CONFIGURATION_SECTION);
			Settings.Client.Save(ClientSettingsType.States);
		}

		private void ComboBoxMachines_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxMachines.SelectedIndex > -1)
			{
				Machine machine = (Machine) comboBoxMachines.SelectedItem;
				Settings.Client.CFG_SelectedHostName = machine.HostName;
				PopulateAllControls();
			}
		}

		private void ButtonMachines_Click(object sender, EventArgs e)
		{
			MachinesForm machinesForm = new MachinesForm((Machine) comboBoxMachines.SelectedItem);
			machinesForm.ShowDialog(this);
			if (machinesForm.MachinesChanged)
			{
				ConnectMachines();
				PopulateMachinesComboBox();
			}
		}

		private void TreeViewConfiguration_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (_currentPanel != null)
				_currentPanel.Visible = false;

			_currentPanel = (Panel) e.Node.Tag;

			if (_currentPanel != null)
				_currentPanel.Visible = true;
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			SaveConfiguration();
			Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			if (_hasUnsavedConfiguration)
				if (Messenger.ShowWarningQuestion("Configuration has been changed", "Would you like to discard any changes?") == DialogResult.No)
					return;
			Close();
		}

		private void ButtonApply_Click(object sender, EventArgs e)
		{
			SaveConfiguration();
		}

		#endregion

		#region Service handler event handlers

		private void ServiceConnectionHandler_ServiceHandlerInitializationCompleted(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerInitializationCompleted), sender, e);
				return;
			}

			Machine machine = Settings.Client.Machines.FirstOrDefault(x => x.ServiceHandler == e.ServiceHandler);
			if (machine != null)
			{
				if (e.Status == ProcessManagerServiceHandlerStatus.Disconnected && e.Exception != null)
				{
					Messenger.ShowError("Failed to connect to machine", "Could not connect to Process Manager service at " + machine.HostName, e.Exception.Message);
				}
				else if (e.Status == ProcessManagerServiceHandlerStatus.Connected && comboBoxMachines.SelectedItem == machine)
				{
					PopulateAllControls();
				}
			}
		}

		private void ServiceConnectionHandler_ServiceHandlerConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerConnectionChanged), sender, e);
				return;
			}

			Machine machine = Settings.Client.Machines.FirstOrDefault(x => x.ServiceHandler == e.ServiceHandler);
			if (machine != null && comboBoxMachines.SelectedItem == machine)
			{
				if (e.Status == ProcessManagerServiceHandlerStatus.Disconnected)
				{
					ClearAllControls();
					ShowAllControls(false);
					Messenger.ShowWarning("Connection lost", "The connection to the selected machine was lost.");
				}
				else if (e.Status == ProcessManagerServiceHandlerStatus.Connected)
				{
					PopulateAllControls();
				}
			}
		}

		#endregion

		#region Helpers

		private void ConnectMachines()
		{
			if (!Settings.Client.Machines.Any(machine => machine.ServiceHandler == null))
				return;

			Worker.Do("Connecting to machines...", () =>
				{
					foreach (Machine machine in Settings.Client.Machines.Where(machine => machine.ServiceHandler == null))
					{
						ProcessManagerServiceConnectionHandler.Instance.CreateServiceHandler(_processManagerEventHandler, machine);
						machine.ServiceHandler.Initialize();
					}
				});
			// todo: not here but anyways.. progress bar task dialog does not hide buttons!!
			WaitForConfiguration((Machine) comboBoxMachines.SelectedItem);
		}

		private void PopulateMachinesComboBox(bool enableAutoSelect = true)
		{
			Machine localhost = new Machine(Settings.Constants.LOCALHOST);
			if (!Settings.Client.Machines.Contains(localhost))
			{
				Settings.Client.Machines.Insert(0, localhost);
				Settings.Client.Save(ClientSettingsType.Machines);
			}

			Machine selectedMachine = new Machine(Settings.Client.CFG_SelectedHostName);
			if (!Settings.Client.Machines.Contains(selectedMachine))
				selectedMachine = localhost;

			comboBoxMachines.Items.Clear();
			foreach (Machine machine in Settings.Client.Machines)
			{
				int index = comboBoxMachines.Items.Add(machine);
				if (enableAutoSelect && machine.Equals(selectedMachine))
					comboBoxMachines.SelectedIndex = index;
			}
		}

		private void SelectInitialMachine()
		{
			Machine selectedMachine = new Machine(Settings.Client.CFG_SelectedHostName);
			if (!Settings.Client.Machines.Contains(selectedMachine))
				selectedMachine = new Machine(Settings.Constants.LOCALHOST);

			int index = comboBoxMachines.Items.IndexOf(selectedMachine);
			comboBoxMachines.SelectedIndex = (index == -1 ? 0 : index);
		}

		private void ShowAllControls(bool show = true)
		{
			labelMachineNotAvailable.Visible = !show;
			if (show)
				labelMachineNotAvailable.SendToBack();
			else
				labelMachineNotAvailable.BringToFront();
		}

		private void ClearAllControls()
		{
			// groups
			listViewGroups.Items.Clear();
			textBoxGroupName.Text = string.Empty;
			textBoxGroupPath.Text = string.Empty;
			listViewGroupApplications.Items.Clear();
			// applications
			listViewApplications.Items.Clear();
			textBoxApplicationName.Text = string.Empty;
			textBoxApplicationRelativePath.Text = string.Empty;
			textBoxApplicationArguments.Text = string.Empty;
			// plugins
			listViewPlugins.Items.Clear();
			labelPluginNameValue.Text = string.Empty;
			labelPluginDescriptionValue.Text = string.Empty;
		}

		private void PopulateAllControls()
		{
			ClearAllControls();
			Machine selectedMachine = (Machine) comboBoxMachines.SelectedItem;
			WaitForConfiguration(selectedMachine);
			if (selectedMachine.Configuration != null)
			{
				selectedMachine.Configuration.Groups.ForEach(group => listViewGroups.Items.Add(new ListViewItem(group.Name) { Tag = group }));
				selectedMachine.Configuration.Applications.ForEach(application => listViewApplications.Items.Add(new ListViewItem(application.Name) { Tag = application }));
			}
			ShowAllControls(selectedMachine.Configuration != null);
		}

		private void WaitForConfiguration(Machine machine)
		{
			if (machine != null && machine.Configuration == null)
				Worker.WaitFor("Retrieving configuration...", () => (machine.ServiceHandler.Status == ProcessManagerServiceHandlerStatus.Disconnected
					|| machine.ServiceHandler.Status == ProcessManagerServiceHandlerStatus.Connected && machine.Configuration != null));
		}

		private void SaveConfiguration()
		{
			if (_hasUnsavedConfiguration)
			{
				//Settings.Client.Save(); // skip??
				// todo: save server side config
				_hasUnsavedConfiguration = false;
			}
		}

		#endregion
	}
}

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
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Forms
{
	public partial class ConfigurationForm : Form
	{
		private readonly IProcessManagerEventHandler _processManagerEventHandler;
		private readonly Timer _initTimer;
		private Panel _currentPanel;
		private Group _selectedGroup;
		private Application _selectedApplication;
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

		#region Main

		private void ConfigurationForm_Load(object sender, EventArgs e)
		{
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerInitializationCompleted += ServiceConnectionHandler_ServiceHandlerInitializationCompleted;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged += ServiceConnectionHandler_ServiceHandlerConnectionChanged;
			ShowAllControls(false);
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

		#region Groups

		private void ListViewGroups_SelectedIndexChanged(object sender, EventArgs e)
		{
			Machine machine = (Machine) comboBoxMachines.SelectedItem;
			if (machine == null) return;

			UpdateSelectedGroup();
			if (listViewGroups.SelectedItems.Count == 0)
			{
				panelGroup.Visible = false;
			}
			else
			{
				_selectedGroup = ((Group) listViewGroups.SelectedItems[0].Tag);
				textBoxGroupName.Text = _selectedGroup.Name;
				textBoxGroupPath.Text = _selectedGroup.Path;
				listViewGroupApplications.Items.Clear();
				listViewGroupApplications.Items.AddRange(_selectedGroup.Applications
					.Select(id => ConnectionStore.Connections[machine].Configuration.Applications.FirstOrDefault(x => x.ID == id))
					.Where(application => application != null)
					.Select(application => new ListViewItem(application.Name) { Tag = application.ID })
					.ToArray());
				panelGroup.Visible = true;
			}
		}

		private void ButtonAddGroup_Click(object sender, EventArgs e)
		{
			Machine machine = (Machine) comboBoxMachines.SelectedItem;
			if (machine == null) return;

			UpdateSelectedGroup();
			_hasUnsavedConfiguration = true;
			string groupName = GetFirstAvailableDefaultName(ConnectionStore.Connections[machine].Configuration.Groups.Select(group => group.Name).ToList(), "Group");
			_selectedGroup = new Group(groupName);
			ConnectionStore.Connections[machine].Configuration.Groups.Add(_selectedGroup);
			textBoxGroupName.Text = _selectedGroup.Name;
			textBoxGroupPath.Text = _selectedGroup.Path;
			listViewGroupApplications.Items.Clear();
			ListViewItem item = listViewGroups.Items.Add(new ListViewItem(_selectedGroup.Name) { Tag = _selectedGroup });
			item.Selected = true;
			panelGroup.Visible = true;
			EnableControls();
			textBoxGroupName.Focus();
		}

		private void ButtonRemoveGroup_Click(object sender, EventArgs e)
		{
			Machine machine = (Machine) comboBoxMachines.SelectedItem;
			if (machine == null) return;

			if (listViewGroups.SelectedItems.Count > 0)
			{
				_hasUnsavedConfiguration = true;
				_selectedGroup = ((Group) listViewGroups.SelectedItems[0].Tag);
				ConnectionStore.Connections[machine].Configuration.Groups.Remove(_selectedGroup);
				listViewGroups.Items.Remove(listViewGroups.SelectedItems[0]);
				_selectedGroup = null;
				EnableControls();
			}
		}

		#endregion

		#endregion

		#region Service handler event handlers

		private void ServiceConnectionHandler_ServiceHandlerInitializationCompleted(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerInitializationCompleted), sender, e);
				return;
			}

			Machine machine = ConnectionStore.Connections.Values.Where(x => x.ServiceHandler == e.ServiceHandler).Select(x => x.Machine).FirstOrDefault();
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

			Machine machine = ConnectionStore.Connections.Values.Where(x => x.ServiceHandler == e.ServiceHandler).Select(x => x.Machine).FirstOrDefault();
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
			ConnectionStore.Connections.Keys.Where(machine => !Settings.Client.Machines.Contains(machine)).ToList().ForEach(ConnectionStore.RemoveConnection);

			if (Settings.Client.Machines.Any(machine => !ConnectionStore.ConnectionCreated(machine)))
			{
				Worker.Do("Connecting to machines...", () =>
					{
						foreach (Machine machine in Settings.Client.Machines.Where(machine => !ConnectionStore.ConnectionCreated(machine)))
						{
							MachineConnection connection = ConnectionStore.CreateConnection(_processManagerEventHandler, machine);
							connection.ServiceHandler.Initialize();
						}
					});
			}

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
			if (ConnectionStore.Connections[selectedMachine].Configuration != null)
			{
				ConnectionStore.Connections[selectedMachine].Configuration.Groups.ForEach(group => listViewGroups.Items.Add(new ListViewItem(group.Name) { Tag = group }));
				ConnectionStore.Connections[selectedMachine].Configuration.Applications.ForEach(application => listViewApplications.Items.Add(new ListViewItem(application.Name) { Tag = application }));
			}
			ShowAllControls(ConnectionStore.Connections[selectedMachine].Configuration != null);
		}

		private void WaitForConfiguration(Machine machine)
		{
			if (machine != null && ConnectionStore.Connections[machine].Configuration == null)
				Worker.WaitFor("Retrieving configuration...", () => (ConnectionStore.Connections[machine].ServiceHandler.Status == ProcessManagerServiceHandlerStatus.Disconnected
					|| ConnectionStore.Connections[machine].ServiceHandler.Status == ProcessManagerServiceHandlerStatus.Connected && ConnectionStore.Connections[machine].Configuration != null));
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

		private string GetFirstAvailableDefaultName(List<string> existingNames, string nameTemplate)
		{
			int tryNo = 1;
			nameTemplate = nameTemplate.Trim() + " ";
			string name = nameTemplate + tryNo;

			while (existingNames.Contains(name))
				name = nameTemplate + (++tryNo);

			return name;
		}

		private void UpdateSelectedGroup()
		{
			if (_selectedGroup != null)
			{
				if (GroupChanged())
				{
					_selectedGroup.Name = textBoxGroupName.Text;
					_selectedGroup.Path = textBoxGroupPath.Text;
					_selectedGroup.Applications.Clear();
					_selectedGroup.Applications.AddRange(listViewGroupApplications.Items.Cast<ListViewItem>().Select(x => (Guid) x.Tag));
					ListViewItem item = listViewGroups.Items.Cast<ListViewItem>().First(x => x.Tag == _selectedGroup);
					item.Text = _selectedGroup.Name;
					listViewGroups.Sort();
				}
				textBoxGroupName.Text = _selectedGroup.Name;
				EnableControls();
			}
		}

		private bool GroupChanged()
		{
			bool groupChanged = false;
			if (_selectedGroup != null && !string.IsNullOrEmpty(textBoxGroupName.Text))
			{
				int equalApplicationsCount = _selectedGroup.Applications.Intersect(listViewGroupApplications.Items.Cast<ListViewItem>().Select(x => (Guid) x.Tag)).Count();
				groupChanged = (_selectedGroup.Name != textBoxGroupName.Text || _selectedGroup.Path != textBoxGroupPath.Text
					|| equalApplicationsCount != _selectedGroup.Applications.Count || equalApplicationsCount != listViewGroupApplications.Items.Count);
				_hasUnsavedConfiguration |= groupChanged;
			}
			return groupChanged;
		}

		private void EnableControls(bool enable = true)
		{
			buttonApply.Enabled = (enable && _hasUnsavedConfiguration);
		}

		#endregion
	}
}

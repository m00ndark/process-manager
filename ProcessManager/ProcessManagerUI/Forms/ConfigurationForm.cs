using System;
using System.Collections.Generic;
using System.IO;
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
using ProcessManagerUI.Utilities;
using Application = ProcessManager.DataObjects.Application;
using Timer = System.Windows.Forms.Timer;

namespace ProcessManagerUI.Forms
{
	public partial class ConfigurationForm : Form
	{
		private readonly Timer _initTimer;
		private Panel _currentPanel;
		private Machine _selectedMachine;
		private Group _selectedGroup;
		private Application _selectedApplication;
		private Distribution _selectedDistribution;
		private bool _disableTextChangedEvents;

		public event EventHandler<MachinesEventArgs> ConfigurationChanged;

		public ConfigurationForm()
		{
			InitializeComponent();
			ServiceHelper.ProcessManagerEventHandler.ConfigurationChanged += ProcessManagerEventHandler_ConfigurationChanged;
			_currentPanel = null;
			_selectedMachine = null;
			_selectedGroup = null;
			_selectedApplication = null;
			_selectedDistribution = null;
			_disableTextChangedEvents = false;
			_initTimer = new Timer() { Enabled = false, Interval = 100 };
			_initTimer.Tick += InitTimer_Tick;
		}

		#region Properties

		public bool HasUnsavedConfiguration
		{
			get { return ConnectionStore.Connections.Values.Any(connection => connection.ConfigurationModified); }
		}

		#endregion

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
			TreeNode distributionsNode = new TreeNode("Distributions") { Name = "Distributions", Tag = panelDistributions };
			TreeNode pluginsNode = new TreeNode("Plugins") { Name = "Plugins", Tag = panelPlugins };
			treeViewConfiguration.Nodes.Add(setupNode);
			treeViewConfiguration.Nodes.Add(distributionsNode);
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
			ServiceHelper.ProcessManagerEventHandler.ConfigurationChanged -= ProcessManagerEventHandler_ConfigurationChanged;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerInitializationCompleted -= ServiceConnectionHandler_ServiceHandlerInitializationCompleted;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged -= ServiceConnectionHandler_ServiceHandlerConnectionChanged;
			Settings.Client.CFG_SelectedConfigurationSection =
				(treeViewConfiguration.SelectedNode != null ? treeViewConfiguration.SelectedNode.Name : Settings.Client.Defaults.SELECTED_CONFIGURATION_SECTION);
			Settings.Client.Save(ClientSettingsType.States);
		}

		private void ComboBoxMachines_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateSelections();
			if (comboBoxMachines.SelectedIndex > -1)
			{
				_selectedMachine = (Machine) comboBoxMachines.SelectedItem;
				Settings.Client.CFG_SelectedHostName = _selectedMachine.HostName;
				PopulateAllControls();
			}
			else
				_selectedMachine = null;
		}

		private void ButtonMachines_Click(object sender, EventArgs e)
		{
			MachinesForm machinesForm = new MachinesForm(_selectedMachine);
			machinesForm.MachinesChanged += MachinesForm_MachinesChanged;
			machinesForm.ShowDialog(this);
			machinesForm.MachinesChanged -= MachinesForm_MachinesChanged;
			if (machinesForm.AnyMachinesChanged)
			{
				if (_selectedMachine != null && !Settings.Client.Machines.Contains(_selectedMachine))
					_selectedMachine = null;

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
			if (SaveConfiguration())
				Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			if (HasUnsavedConfiguration)
			{
				if (Messenger.ShowWarningQuestion("Configuration has been changed", "Would you like to discard any changes?") == DialogResult.No)
				{
					DialogResult = DialogResult.None;
					return;
				}
				ServiceHelper.ReloadConfiguration();
			}
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
			if (_selectedMachine == null) return;

			if (listViewGroups.SelectedItems.Count == 0)
			{
				panelGroup.Visible = false;
			}
			else
			{
				_disableTextChangedEvents = true;
				_selectedGroup = ((Group) listViewGroups.SelectedItems[0].Tag);
				textBoxGroupName.Text = _selectedGroup.Name;
				textBoxGroupPath.Text = _selectedGroup.Path;
				listViewGroupApplications.Items.Clear();
				listViewGroupApplications.Items.AddRange(_selectedGroup.Applications
					.Select(id => ConnectionStore.Connections[_selectedMachine].Configuration.Applications.FirstOrDefault(x => x.ID == id))
					.Where(application => application != null)
					.Select(application => new ListViewItem(application.Name) { Tag = application.ID })
					.ToArray());
				_disableTextChangedEvents = false;
				EnableControls();
				panelGroup.Visible = true;
			}
		}

		private void ButtonAddGroup_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			UpdateSelectedGroup();
			string groupName = GetFirstAvailableDefaultName(
				ConnectionStore.Connections[_selectedMachine].Configuration.Groups.Select(group => group.Name).ToList(), "Group");
			_selectedGroup = new Group(groupName);
			ConnectionStore.Connections[_selectedMachine].Configuration.Groups.Add(_selectedGroup);
			ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
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
			if (_selectedMachine == null) return;

			if (listViewGroups.SelectedItems.Count > 0)
			{
				_selectedGroup = (Group) listViewGroups.SelectedItems[0].Tag;
				ConnectionStore.Connections[_selectedMachine].Configuration.Groups.Remove(_selectedGroup);
				ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
				listViewGroups.Items.Remove(listViewGroups.SelectedItems[0]);
				_selectedGroup = null;
				EnableControls();
			}
		}

		private void TextBoxGroupName_TextChanged(object sender, EventArgs e)
		{
			if (!_disableTextChangedEvents)
			{
				GroupChanged();
				EnableControls();
			}
		}

		private void TextBoxGroupName_Leave(object sender, EventArgs e)
		{
			UpdateSelectedGroup();
		}

		private void TextBoxGroupPath_TextChanged(object sender, EventArgs e)
		{
			if (!_disableTextChangedEvents)
			{
				GroupChanged();
				EnableControls();
			}
		}

		private void TextBoxGroupPath_MouseLeave(object sender, EventArgs e)
		{
			UpdateSelectedGroup();
		}

		private void ButtonBrowseGroupPath_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
				{
					Description = "Select a root folder for this group...",
					SelectedPath = textBoxGroupPath.Text
				};
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				textBoxGroupPath.Text = folderBrowserDialog.SelectedPath;
				UpdateSelectedGroup();
			}
		}

		private void ListViewGroupApplications_SelectedIndexChanged(object sender, EventArgs e)
		{
			EnableControls();
		}

		private void ButtonAddGroupApplication_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			Picker.ShowMenu(buttonAddGroupApplication, GetNonIncludedGroupApplications(), ContextMenu_AddGroupApplication_ApplicationClicked);
		}

		private void ButtonRemoveGroupApplication_Click(object sender, EventArgs e)
		{
			if (listViewGroupApplications.SelectedItems.Count > 0)
			{
				listViewGroupApplications.Items.Remove(listViewGroupApplications.SelectedItems[0]);
				UpdateSelectedGroup();
				EnableControls();
			}
		}

		private void ButtonCopyGroupApplications_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null || _selectedGroup == null) return;

			Picker.ShowMenu(buttonCopyGroupApplications, GetAllGroups(true), ContextMenu_CopyGroupApplications_GroupClicked);
		}

		#endregion

		#region Applications

		private void ListViewApplications_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			if (listViewApplications.SelectedItems.Count == 0)
			{
				panelApplication.Visible = false;
			}
			else
			{
				_disableTextChangedEvents = true;
				_selectedApplication = ((Application) listViewApplications.SelectedItems[0].Tag);
				textBoxApplicationName.Text = _selectedApplication.Name;
				textBoxApplicationRelativePath.Text = _selectedApplication.RelativePath;
				textBoxApplicationArguments.Text = _selectedApplication.Arguments;
				_disableTextChangedEvents = false;
				EnableControls();
				panelApplication.Visible = true;
			}
		}

		private void ButtonAddApplication_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			UpdateSelectedApplication();
			string applicationName = GetFirstAvailableDefaultName(
				ConnectionStore.Connections[_selectedMachine].Configuration.Applications.Select(application => application.Name).ToList(), "Application");
			_selectedApplication = new Application(applicationName);
			ConnectionStore.Connections[_selectedMachine].Configuration.Applications.Add(_selectedApplication);
			ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
			textBoxApplicationName.Text = _selectedApplication.Name;
			textBoxApplicationRelativePath.Text = _selectedApplication.RelativePath;
			textBoxApplicationArguments.Text = _selectedApplication.Arguments;
			ListViewItem item = listViewApplications.Items.Add(new ListViewItem(_selectedApplication.Name) { Tag = _selectedApplication });
			item.Selected = true;
			panelApplication.Visible = true;
			EnableControls();
			textBoxApplicationName.Focus();
		}

		private void ButtonRemoveApplication_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			if (listViewApplications.SelectedItems.Count > 0)
			{
				_selectedApplication = (Application) listViewApplications.SelectedItems[0].Tag;
				ConnectionStore.Connections[_selectedMachine].Configuration.Applications.Remove(_selectedApplication);
				ConnectionStore.Connections[_selectedMachine].Configuration.Groups.ForEach(group => group.Applications.Remove(_selectedApplication.ID));
				ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
				listViewApplications.Items.Remove(listViewApplications.SelectedItems[0]);
				listViewGroupApplications.Items.Cast<ListViewItem>()
					.Where(item => (Guid) item.Tag == _selectedApplication.ID).ToList()
					.ForEach(item => listViewGroupApplications.Items.Remove(item));
				_selectedApplication = null;
				EnableControls();
			}
		}

		private void TextBoxApplicationName_TextChanged(object sender, EventArgs e)
		{
			if (!_disableTextChangedEvents)
			{
				ApplicationChanged();
				EnableControls();
			}
		}

		private void TextBoxApplicationName_Leave(object sender, EventArgs e)
		{
			UpdateSelectedApplication();
		}

		private void TextBoxApplicationRelativePath_TextChanged(object sender, EventArgs e)
		{
			if (!_disableTextChangedEvents)
			{
				ApplicationChanged();
				EnableControls();
			}
		}

		private void TextBoxApplicationRelativePath_Leave(object sender, EventArgs e)
		{
			UpdateSelectedApplication();
		}

		private void ButtonBrowseApplicationRelativePath_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			Picker.ShowMenu(buttonBrowseApplicationRelativePath, GetAllGroups(false), ContextMenu_BrowseApplicationRelativePath_GroupClicked);
		}

		private void TextBoxApplicationArguments_TextChanged(object sender, EventArgs e)
		{
			if (!_disableTextChangedEvents)
			{
				ApplicationChanged();
				EnableControls();
			}
		}

		private void TextBoxApplicationArguments_Leave(object sender, EventArgs e)
		{
			UpdateSelectedApplication();
		}

		#endregion

		#region Distributions

		private void ListViewDistributions_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			if (listViewDistributions.SelectedItems.Count == 0)
			{
				panelDistribution.Visible = false;
			}
			else
			{
				_disableTextChangedEvents = true;
				_selectedDistribution = ((Distribution) listViewDistributions.SelectedItems[0].Tag);
				textBoxDistributionName.Text = _selectedDistribution.Name;
				textBoxDistributionDestination.Text = _selectedDistribution.DestinationPath;
				listViewDistributionSources.Items.Clear();
				listViewDistributionSources.Items.AddRange(_selectedDistribution.Sources
					.Select(source => new ListViewItem(source.Path) { Tag = source })
					.ToArray());
				_disableTextChangedEvents = false;
				EnableControls();
				panelDistribution.Visible = true;
			}
		}

		private void ButtonAddDistribution_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			UpdateSelectedDistribution();
			string distributionName = GetFirstAvailableDefaultName(
				ConnectionStore.Connections[_selectedMachine].Configuration.Distributions.Select(distribution => distribution.Name).ToList(), "Distribution");
			_selectedDistribution = new Distribution(distributionName);
			ConnectionStore.Connections[_selectedMachine].Configuration.Distributions.Add(_selectedDistribution);
			ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
			textBoxDistributionName.Text = _selectedDistribution.Name;
			textBoxDistributionDestination.Text = _selectedDistribution.DestinationPath;
			listViewDistributionSources.Items.Clear();
			ListViewItem item = listViewDistributions.Items.Add(new ListViewItem(_selectedDistribution.Name) { Tag = _selectedDistribution });
			item.Selected = true;
			panelDistribution.Visible = true;
			EnableControls();
			textBoxDistributionName.Focus();
		}

		private void ButtonRemoveDistribution_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			if (listViewDistributions.SelectedItems.Count > 0)
			{
				_selectedDistribution = (Distribution) listViewDistributions.SelectedItems[0].Tag;
				ConnectionStore.Connections[_selectedMachine].Configuration.Distributions.Remove(_selectedDistribution);
				ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
				listViewDistributions.Items.Remove(listViewDistributions.SelectedItems[0]);
				_selectedDistribution = null;
				EnableControls();
			}
		}

		private void TextBoxDistributionName_TextChanged(object sender, EventArgs e)
		{
			if (!_disableTextChangedEvents)
			{
				DistributionChanged();
				EnableControls();
			}
		}

		private void TextBoxDistributionName_Leave(object sender, EventArgs e)
		{
			UpdateSelectedDistribution();
		}

		private void ButtonAddDistributionSource_Click(object sender, EventArgs e)
		{
		}

		private void ButtonRemoveDistributionSource_Click(object sender, EventArgs e)
		{

		}

		private void TextBoxDistributionDestination_TextChanged(object sender, EventArgs e)
		{
			if (!_disableTextChangedEvents)
			{
				DistributionChanged();
				EnableControls();
			}
		}

		private void TextBoxDistributionDestination_Leave(object sender, EventArgs e)
		{
			UpdateSelectedDistribution();
		}

		private void ButtonBrowseDistributionDestinationPath_Click(object sender, EventArgs e)
		{

		}

		#endregion

		#endregion

		#region Picker event handlers

		private void ContextMenu_AddGroupApplication_ApplicationClicked(Application application)
		{
			listViewGroupApplications.Items.Add(new ListViewItem(application.Name) { Tag = application.ID });
			UpdateSelectedGroup();
			EnableControls();
		}

		private void ContextMenu_CopyGroupApplications_GroupClicked(Group group)
		{
			if (_selectedMachine == null) return;

			listViewGroupApplications.Items.Clear();
			listViewGroupApplications.Items.AddRange(group.Applications
				.Select(id => ConnectionStore.Connections[_selectedMachine].Configuration.Applications.FirstOrDefault(x => x.ID == id))
				.Where(application => application != null)
				.Select(application => new ListViewItem(application.Name) { Tag = application.ID })
				.ToArray());
			UpdateSelectedGroup();
			EnableControls();
		}

		private void ContextMenu_BrowseApplicationRelativePath_GroupClicked(Group group)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Title = "Select an application",
					InitialDirectory = (string.IsNullOrEmpty(textBoxApplicationRelativePath.Text) ? group.Path
						: Path.GetDirectoryName(Path.Combine(group.Path, textBoxApplicationRelativePath.Text.Trim(Path.DirectorySeparatorChar)))),
					FileName = (string.IsNullOrEmpty(textBoxApplicationRelativePath.Text) ? string.Empty : Path.GetFileName(textBoxApplicationRelativePath.Text)),
					Filter = "Applications (*.exe)|*.exe|All files (*.*)|*.*",
					CheckFileExists = true
				};
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				if (!openFileDialog.FileName.StartsWith(group.Path, StringComparison.CurrentCultureIgnoreCase))
					Messenger.ShowError("Invalid application path", "The selected application's path must start with the selected group's path; " + group.Path);
				else
				{
					textBoxApplicationRelativePath.Text = openFileDialog.FileName.Substring(group.Path.TrimEnd(Path.DirectorySeparatorChar).Length);
					UpdateSelectedApplication();
				}
			}
		}

		#endregion

		#region Process manager event handlers

		private void ProcessManagerEventHandler_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<MachineConfigurationHashEventArgs>(ProcessManagerEventHandler_ConfigurationChanged), sender, e);
				return;
			}

			if (ConnectionStore.Connections[e.Machine].Configuration.Hash != e.ConfigurationHash)
			{
				Messenger.ShowWarning("Configuration changed", "The configuration for " + e.Machine + " was changed from another client."
					+ " Configuration will be reloaded to reflect those changes.");

				ServiceHelper.ReloadConfiguration(e.Machine);
			}
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

			Machine machine = ConnectionStore.Connections.Values.Where(x => x.ServiceHandler == e.ServiceHandler).Select(x => x.Machine).FirstOrDefault();
			if (machine != null)
			{
				if (e.Status == ProcessManagerServiceHandlerStatus.Disconnected && e.Exception != null)
				{
					Messenger.ShowError("Failed to connect to machine", "Could not connect to Process Manager service at " + machine.HostName, e.Exception);
				}
				else if (e.Status == ProcessManagerServiceHandlerStatus.Connected && _selectedMachine == machine)
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
			if (machine != null && _selectedMachine == machine)
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

		#region Machines form event handlers

		private void MachinesForm_MachinesChanged(object sender, EventArgs e)
		{
			RaiseConfigurationChangedEvent(null);
		}

		#endregion

		#region Event raisers

		private void RaiseConfigurationChangedEvent(List<Machine> machines)
		{
			if (ConfigurationChanged != null)
				ConfigurationChanged(this, new MachinesEventArgs(machines));
		}

		#endregion

		#region Helpers

		#region Machines

		private void ConnectMachines()
		{
			ServiceHelper.ConnectMachines();
			ServiceHelper.WaitForConfiguration(_selectedMachine);
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

		#endregion

		#region Configuration

		private bool SaveConfiguration()
		{
			UpdateSelections();
			if (HasUnsavedConfiguration)
			{
				// validate
				if (!ValidateGroups() || !ValidateApplications() || !ValidateDistributions())
					return false;

				List<Machine> modifiedMachines = ConnectionStore.Connections.Values
					.Where(connection => connection.ConfigurationModified)
					.Select(connection => connection.Machine)
					.ToList();

				// save
				if (!ServiceHelper.SaveConfiguration())
					return false;

				RaiseConfigurationChangedEvent(modifiedMachines);

				EnableControls();
			}
			return true;
		}

		#endregion

		#region Groups

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
			if (_selectedMachine != null && _selectedGroup != null && !string.IsNullOrEmpty(textBoxGroupName.Text))
			{
				int equalApplicationsCount = _selectedGroup.Applications.Intersect(listViewGroupApplications.Items.Cast<ListViewItem>().Select(x => (Guid) x.Tag)).Count();
				groupChanged = (_selectedGroup.Name != textBoxGroupName.Text || _selectedGroup.Path != textBoxGroupPath.Text
					|| equalApplicationsCount != _selectedGroup.Applications.Count || equalApplicationsCount != listViewGroupApplications.Items.Count);
				ConnectionStore.Connections[_selectedMachine].ConfigurationModified |= groupChanged;
			}
			return groupChanged;
		}

		private IEnumerable<Application> GetNonIncludedGroupApplications()
		{
			if (_selectedMachine == null) return new List<Application>();

			return ConnectionStore.Connections[_selectedMachine].Configuration.Applications
				.Except(ConnectionStore.Connections[_selectedMachine].Configuration.Applications
					.Where(x => listViewGroupApplications.Items.Cast<ListViewItem>().Any(y => (Guid) y.Tag == x.ID)));
		}

		private IEnumerable<Group> GetAllGroups(bool exceptCurrent)
		{
			if (_selectedMachine == null) return new List<Group>();

			List<Group> groups = ConnectionStore.Connections[_selectedMachine].Configuration.Groups;
			return (exceptCurrent ? (_selectedGroup == null ? new List<Group>() : groups.Except(new List<Group>() { _selectedGroup })) : groups);
		}

		private static bool ValidateGroups()
		{
			IDictionary<Machine, Dictionary<Group, List<string>>> invalidGroups =
				ConnectionStore.Connections.Values
					.Where(connection => connection.ConfigurationModified)
					.Select(connection => new
						{
							Machine = connection.Machine,
							Groups = connection.Configuration.Groups
								.Select(group => new
									{
										Group = group,
										Messages = GroupIsValid(group).ToList()
									})
								.Where(x => x.Messages.Count > 0)
								.ToList()
						})
					.Where(x => x.Groups.Count > 0)
					.ToDictionary(x => x.Machine, x => x.Groups.ToDictionary(y => y.Group, y => y.Messages));
			if (invalidGroups.Count > 0)
			{
				int invalidGroupCount = invalidGroups.SelectMany(x => x.Value).Count();
				Messenger.ShowError("Group" + (invalidGroupCount == 1 ? string.Empty : "s") + " invalid",
					"One or more group property invalid. See details for more information.",
					invalidGroups.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + Environment.NewLine
						+ y.Value.SelectMany(z => z.Value, (a, b) => new { Group = a.Key, Message = b })
							.Aggregate(string.Empty, (a, b) => a + Environment.NewLine + y.Key + " > " + b.Group + ": " + b.Message).Trim()).Trim());
				return false;
			}
			return true;
		}

		private static IEnumerable<string> GroupIsValid(Group group)
		{
			if (string.IsNullOrEmpty(group.Name))
				yield return "Name missing";
			if (string.IsNullOrEmpty(group.Path))
				yield return "Path missing";
			else if (!Path.IsPathRooted(group.Path))
				yield return "Path must be rooted";
		}

		#endregion

		#region Applications

		private void UpdateSelectedApplication()
		{
			if (_selectedApplication != null)
			{
				if (ApplicationChanged())
				{
					_selectedApplication.Name = textBoxApplicationName.Text;
					_selectedApplication.RelativePath = textBoxApplicationRelativePath.Text;
					_selectedApplication.Arguments = textBoxApplicationArguments.Text;
					ListViewItem item = listViewApplications.Items.Cast<ListViewItem>().First(x => x.Tag == _selectedApplication);
					item.Text = _selectedApplication.Name;
					listViewApplications.Sort();
				}
				textBoxApplicationName.Text = _selectedApplication.Name;
				EnableControls();
			}
		}

		private bool ApplicationChanged()
		{
			bool applicationChanged = false;
			if (_selectedMachine != null && _selectedApplication != null && !string.IsNullOrEmpty(textBoxApplicationName.Text))
			{
				applicationChanged = (_selectedApplication.Name != textBoxApplicationName.Text
					|| _selectedApplication.RelativePath != textBoxApplicationRelativePath.Text
					|| _selectedApplication.Arguments != textBoxApplicationArguments.Text);
				ConnectionStore.Connections[_selectedMachine].ConfigurationModified |= applicationChanged;
			}
			return applicationChanged;
		}

		private static bool ValidateApplications()
		{
			IDictionary<Machine, Dictionary<Application, List<string>>> invalidApplications =
				ConnectionStore.Connections.Values
					.Where(connection => connection.ConfigurationModified)
					.Select(connection => new
					{
						Machine = connection.Machine,
						Applications = connection.Configuration.Applications
							.Select(application => new
							{
								Application = application,
								Messages = ApplicationIsValid(application).ToList()
							})
							.Where(x => x.Messages.Count > 0)
							.ToList()
					})
					.Where(x => x.Applications.Count > 0)
					.ToDictionary(x => x.Machine, x => x.Applications.ToDictionary(y => y.Application, y => y.Messages));
			if (invalidApplications.Count > 0)
			{
				int invalidApplicationCount = invalidApplications.SelectMany(x => x.Value).Count();
				Messenger.ShowError("Application" + (invalidApplicationCount == 1 ? string.Empty : "s") + " invalid",
					"One or more application property invalid. See details for more information.",
					invalidApplications.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + Environment.NewLine
						+ y.Value.SelectMany(z => z.Value, (a, b) => new { Application = a.Key, Message = b })
							.Aggregate(string.Empty, (a, b) => a + Environment.NewLine + y.Key + " > " + b.Application + ": " + b.Message).Trim()).Trim());
				return false;
			}
			return true;
		}

		private static IEnumerable<string> ApplicationIsValid(Application application)
		{
			if (string.IsNullOrEmpty(application.Name))
				yield return "Name missing";
			if (string.IsNullOrEmpty(application.RelativePath))
				yield return "Relative path missing";
			else if (Path.IsPathRooted(application.RelativePath.TrimStart(Path.DirectorySeparatorChar)))
				yield return "Relative path can not be rooted";
		}

		#endregion

		#region Distributions

		private void UpdateSelectedDistribution()
		{
			if (_selectedDistribution != null)
			{
				if (DistributionChanged())
				{
					_selectedDistribution.Name = textBoxDistributionName.Text;
					_selectedDistribution.DestinationPath = textBoxDistributionDestination.Text;
					_selectedDistribution.Sources.Clear();
					_selectedDistribution.Sources.AddRange(listViewDistributionSources.Items.Cast<ListViewItem>().Select(x => (DistributionSource) x.Tag));
					ListViewItem item = listViewDistributions.Items.Cast<ListViewItem>().First(x => x.Tag == _selectedDistribution);
					item.Text = _selectedDistribution.Name;
					listViewDistributions.Sort();
				}
				textBoxDistributionName.Text = _selectedDistribution.Name;
				EnableControls();
			}
		}

		private bool DistributionChanged()
		{
			bool distributionChanged = false;
			if (_selectedMachine != null && _selectedDistribution != null && !string.IsNullOrEmpty(textBoxDistributionName.Text))
			{
				int equalSourcesCount = _selectedDistribution.Sources.Intersect(listViewDistributionSources.Items
					.Cast<ListViewItem>().Select(x => (DistributionSource) x.Tag), new IDObjectEqualityComparer<DistributionSource>()).Count();
				distributionChanged = (_selectedDistribution.Name != textBoxDistributionName.Text
					|| _selectedDistribution.DestinationPath != textBoxDistributionDestination.Text
					|| equalSourcesCount != _selectedDistribution.Sources.Count || equalSourcesCount != listViewDistributionSources.Items.Count);
				ConnectionStore.Connections[_selectedMachine].ConfigurationModified |= distributionChanged;
			}
			return distributionChanged;
		}

		private static bool ValidateDistributions()
		{
			IDictionary<Machine, Dictionary<Distribution, List<string>>> invalidDistributions =
				ConnectionStore.Connections.Values
					.Where(connection => connection.ConfigurationModified)
					.Select(connection => new
					{
						Machine = connection.Machine,
						Distributions = connection.Configuration.Distributions
							.Select(distribution => new
							{
								Distribution = distribution,
								Messages = DistributionIsValid(distribution).ToList()
							})
							.Where(x => x.Messages.Count > 0)
							.ToList()
					})
					.Where(x => x.Distributions.Count > 0)
					.ToDictionary(x => x.Machine, x => x.Distributions.ToDictionary(y => y.Distribution, y => y.Messages));
			if (invalidDistributions.Count > 0)
			{
				int invalidDistributionCount = invalidDistributions.SelectMany(x => x.Value).Count();
				Messenger.ShowError("Distribution" + (invalidDistributionCount == 1 ? string.Empty : "s") + " invalid",
					"One or more distribution property invalid. See details for more information.",
					invalidDistributions.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + Environment.NewLine
						+ y.Value.SelectMany(z => z.Value, (a, b) => new { Distribution = a.Key, Message = b })
							.Aggregate(string.Empty, (a, b) => a + Environment.NewLine + y.Key + " > " + b.Distribution + ": " + b.Message).Trim()).Trim());
				return false;
			}
			return true;
		}

		private static IEnumerable<string> DistributionIsValid(Distribution distribution)
		{
			if (string.IsNullOrEmpty(distribution.Name))
				yield return "Name missing";
			if (string.IsNullOrEmpty(distribution.DestinationPath))
				yield return "Destination path missing";
			else if (!Path.IsPathRooted(distribution.DestinationPath))
				yield return  "Destination path must be rooted";

			// TODO: validate distribution sources?
		}

		#endregion

		private static string GetFirstAvailableDefaultName(ICollection<string> existingNames, string nameTemplate)
		{
			nameTemplate = nameTemplate.Trim() + " ";
			int tryNo = 0; string name;

			while (existingNames.Contains(name = nameTemplate + (++tryNo))) {}

			return name;
		}

		private void UpdateSelections()
		{
			UpdateSelectedGroup();
			UpdateSelectedApplication();
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
			_selectedGroup = null;
			panelGroup.Visible = false;
			textBoxGroupName.Text = string.Empty;
			textBoxGroupPath.Text = string.Empty;
			listViewGroupApplications.Items.Clear();
			// applications
			listViewApplications.Items.Clear();
			_selectedApplication = null;
			panelApplication.Visible = false;
			textBoxApplicationName.Text = string.Empty;
			textBoxApplicationRelativePath.Text = string.Empty;
			textBoxApplicationArguments.Text = string.Empty;
			// plugins
			listViewPlugins.Items.Clear();
			panelPlugin.Visible = false;
			labelPluginNameValue.Text = string.Empty;
			labelPluginDescriptionValue.Text = string.Empty;
		}

		private void PopulateAllControls()
		{
			ClearAllControls();
			ServiceHelper.WaitForConfiguration(_selectedMachine);
			listViewGroups.Items.Clear();
			listViewApplications.Items.Clear();
			listViewPlugins.Items.Clear();
			if (_selectedMachine != null && ConnectionStore.Connections[_selectedMachine].Configuration != null)
			{
				ConnectionStore.Connections[_selectedMachine].Configuration.Groups.ForEach(group => listViewGroups.Items.Add(new ListViewItem(group.Name) { Tag = group }));
				ConnectionStore.Connections[_selectedMachine].Configuration.Applications.ForEach(application => listViewApplications.Items.Add(new ListViewItem(application.Name) { Tag = application }));
			}
			ShowAllControls(_selectedMachine != null && ConnectionStore.Connections[_selectedMachine].Configuration != null);
		}

		private void EnableControls(bool enable = true)
		{
			buttonApply.Enabled = (enable && HasUnsavedConfiguration);
			buttonAddGroupApplication.Enabled = (enable && GetNonIncludedGroupApplications().Count() > 0);
			buttonRemoveGroupApplication.Enabled = (enable && listViewGroupApplications.SelectedItems.Count > 0);
			buttonCopyGroupApplications.Enabled = (enable && GetAllGroups(true).Count() > 0);
		}

		#endregion

		private void buttonRemoveDistributionSource_Click(object sender, EventArgs e)
		{

		}

		private void buttonAddDistributionSource_Click(object sender, EventArgs e)
		{

		}

		private void buttonBrowseDistributionDestinationPath_Click(object sender, EventArgs e)
		{

		}
	}
}

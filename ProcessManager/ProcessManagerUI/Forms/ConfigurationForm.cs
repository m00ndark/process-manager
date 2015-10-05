using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.DataObjects.Comparers;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;
using ProcessManagerUI.Support;
using ProcessManagerUI.Utilities;
using Application = ProcessManager.DataObjects.Application;
using Timer = System.Windows.Forms.Timer;

namespace ProcessManagerUI.Forms
{
	public partial class ConfigurationForm : Form
	{
		private readonly Timer _initTimer;
		private readonly Func<MacrosForm> _getMacrosForm;
		private readonly Action<MacrosForm> _setMacrosForm;
		private Panel _currentPanel;
		private Machine _selectedMachine;
		private Group _selectedGroup;
		private Application _selectedApplication;
		private bool _disableChangedEvents;

		public event EventHandler<MachinesEventArgs> ConfigurationChanged;
		public event EventHandler MacrosChanged;

		public ConfigurationForm()
		{
			InitializeComponent();
			ServiceHelper.ProcessManagerEventHandler.ConfigurationChanged += ProcessManagerEventHandler_ConfigurationChanged;
			_getMacrosForm = null;
			_setMacrosForm = null;
			_currentPanel = null;
			_selectedMachine = null;
			_selectedGroup = null;
			_selectedApplication = null;
			_disableChangedEvents = false;
			_initTimer = new Timer() { Enabled = false, Interval = 100 };
			_initTimer.Tick += InitTimer_Tick;
		}

		public ConfigurationForm(Func<MacrosForm> getMacrosForm, Action<MacrosForm> setMacrosForm) : this()
		{
			_getMacrosForm = getMacrosForm;
			_setMacrosForm = setMacrosForm;
		}

		#region Properties

		public bool HasUnsavedConfiguration { get { return ConnectionStore.Connections.Values.Any(connection => connection.ConfigurationModified); } }

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

			foreach (SuccessExitCode successExitCode in Enum.GetValues(typeof(SuccessExitCode)))
				comboBoxApplicationSuccessExitCode.Items.Add(new ComboBoxItem<SuccessExitCode>(successExitCode));

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

		private void ConfigurationForm_Enter(object sender, EventArgs e)
		{
			if (Settings.Client.UserOwnsControlPanel && Settings.Client.KeepControlPanelTopMost)
				TopMost = true;
		}

		private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			ServiceHelper.ProcessManagerEventHandler.ConfigurationChanged -= ProcessManagerEventHandler_ConfigurationChanged;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerInitializationCompleted -= ServiceConnectionHandler_ServiceHandlerInitializationCompleted;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged -= ServiceConnectionHandler_ServiceHandlerConnectionChanged;
			Settings.Client.CFG_SelectedConfigurationSection = treeViewConfiguration.SelectedNode?.Name ?? Settings.Client.Defaults.SELECTED_CONFIGURATION_SECTION;
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

		private void ButtonMacros_Click(object sender, EventArgs e)
		{
			MacrosForm macrosForm = _getMacrosForm();
			if (macrosForm != null)
			{
				try { NativeMethods.SetForegroundWindow(macrosForm.Handle); } catch { ; }
				return;
			}

			_setMacrosForm(macrosForm = new MacrosForm());
			if (Settings.Client.UserOwnsControlPanel && Settings.Client.KeepControlPanelTopMost)
				macrosForm.TopMost = true;
			macrosForm.FormClosed += MacrosForm_FormClosed;
			macrosForm.MacrosChanged += MacrosForm_MacrosChanged;
			macrosForm.Show();
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
			if (Save())
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
			Save();
		}

		#endregion

		#region Groups

		private void ListViewGroups_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			if (listViewGroups.SelectedItems.Count != 1)
			{
				labelNoGroupSelected.Text = listViewGroups.SelectedItems.Count == 0 ? "No group selected" : "Multiple groups selected";
				panelGroup.Visible = false;
			}
			else
			{
				_disableChangedEvents = true;
				_selectedGroup = ((Group) listViewGroups.SelectedItems[0].Tag);
				textBoxGroupName.Text = _selectedGroup.Name;
				textBoxGroupPath.Text = _selectedGroup.Path;
				listViewGroupApplications.Items.Clear();
				listViewGroupApplications.Items.AddRange(_selectedGroup.Applications
					.Select(id => ConnectionStore.Connections[_selectedMachine].Configuration.Applications.FirstOrDefault(x => x.ID == id))
					.Where(application => application != null)
					.Select(application => new ListViewItem(application.Name) { Tag = application.ID })
					.ToArray());
				_disableChangedEvents = false;
				panelGroup.Visible = true;
			}
			EnableControls();
		}

		private void ButtonAddGroup_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			AddGroup(false);
		}

		private void ButtonCopyGroup_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			if (listViewGroups.SelectedItems.Count == 1)
				AddGroup(true);
		}

		private void ButtonRemoveGroup_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			if (listViewGroups.SelectedItems.Count > 0)
			{
				foreach (ListViewItem item in listViewGroups.SelectedItems)
				{
					ConnectionStore.Connections[_selectedMachine].Configuration.Groups.Remove((Group) item.Tag);
					listViewGroups.Items.Remove(item);
				}
				ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
				_selectedGroup = null;
				EnableControls();
			}
		}

		private void TextBoxGroupName_TextChanged(object sender, EventArgs e)
		{
			if (_disableChangedEvents) return;

			GroupChanged();
			EnableControls();
		}

		private void TextBoxGroupName_Leave(object sender, EventArgs e)
		{
			UpdateSelectedGroup();
		}

		private void TextBoxGroupPath_TextChanged(object sender, EventArgs e)
		{
			if (_disableChangedEvents) return;

			GroupChanged();
			EnableControls();
		}

		private void TextBoxGroupPath_MouseLeave(object sender, EventArgs e)
		{
			UpdateSelectedGroup();
		}

		private void ButtonBrowseGroupPath_Click(object sender, EventArgs e)
		{
			FileSystemBrowserForm fileSystemBrowser = new FileSystemBrowserForm(_selectedMachine)
				{
					Description = "Select a root folder for this group...",
					SelectedPath = textBoxGroupPath.Text,
					BrowserMode = FileSystemBrowserForm.Mode.Folder
				};

			if (fileSystemBrowser.ShowDialog(this) != DialogResult.OK)
				return;

			textBoxGroupPath.Text = fileSystemBrowser.SelectedPath;
			UpdateSelectedGroup();
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
			if (listViewGroupApplications.SelectedItems.Count == 0) return;

			listViewGroupApplications.Items.Remove(listViewGroupApplications.SelectedItems[0]);
			UpdateSelectedGroup();
			EnableControls();
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

			if (listViewApplications.SelectedItems.Count != 1)
			{
				labelNoApplicationSelected.Text = listViewApplications.SelectedItems.Count == 0 ? "No application selected" : "Multiple applications selected";
				panelApplication.Visible = false;
			}
			else
			{
				_disableChangedEvents = true;
				_selectedApplication = ((Application) listViewApplications.SelectedItems[0].Tag);
				textBoxApplicationName.Text = _selectedApplication.Name;
				textBoxApplicationRelativePath.Text = _selectedApplication.RelativePath;
				textBoxApplicationArguments.Text = _selectedApplication.Arguments;
				checkBoxApplicationWaitForExit.Checked = _selectedApplication.WaitForExit;
				comboBoxApplicationSuccessExitCode.SelectedItem = GetSuccessExitCodeItem(_selectedApplication.SuccessExitCode);
				checkBoxDistributionOnly.Checked = _selectedApplication.DistributionOnly;
				DisplayDistributionSourceCount();
				_disableChangedEvents = false;
				panelApplication.Visible = true;
			}
			EnableControls();
		}

		private void ButtonAddApplication_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			AddApplication(false);
		}

		private void ButtonCopyApplication_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			if (listViewApplications.SelectedItems.Count == 1)
				AddApplication(true);
		}

		private void ButtonRemoveApplication_Click(object sender, EventArgs e)
		{
			if (_selectedMachine == null) return;

			if (listViewApplications.SelectedItems.Count == 0) return;

			foreach (ListViewItem item in listViewApplications.SelectedItems)
			{
				Application application = (Application) item.Tag;
				ConnectionStore.Connections[_selectedMachine].Configuration.Applications.Remove(application);
				ConnectionStore.Connections[_selectedMachine].Configuration.Groups.ForEach(group => @group.Applications.Remove(application.ID));
				listViewApplications.Items.Remove(item);
				listViewGroupApplications.Items.Cast<ListViewItem>()
					.Where(x => (Guid) x.Tag == application.ID).ToList()
					.ForEach(x => listViewGroupApplications.Items.Remove(x));
			}
			ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
			_selectedApplication = null;
			EnableControls();
		}

		private void TextBoxApplicationName_TextChanged(object sender, EventArgs e)
		{
			if (_disableChangedEvents) return;

			ApplicationChanged();
			EnableControls();
		}

		private void TextBoxApplicationName_Leave(object sender, EventArgs e)
		{
			UpdateSelectedApplication();
		}

		private void TextBoxApplicationRelativePath_TextChanged(object sender, EventArgs e)
		{
			if (_disableChangedEvents) return;

			ApplicationChanged();
			EnableControls();
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
			if (_disableChangedEvents) return;

			ApplicationChanged();
			EnableControls();
		}

		private void TextBoxApplicationArguments_Leave(object sender, EventArgs e)
		{
			UpdateSelectedApplication();
		}

		private void CheckBoxApplicationWaitForExit_CheckedChanged(object sender, EventArgs e)
		{
			if (_disableChangedEvents) return;

			UpdateSelectedApplication();
		}

		private void ComboBoxApplicationSuccessExitCode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_disableChangedEvents) return;

			UpdateSelectedApplication();
		}

		private void ButtonEditDistributionSources_Click(object sender, EventArgs e)
		{
			DistributionSourcesForm distributionSourcesForm = new DistributionSourcesForm(_selectedMachine, _selectedApplication, () => GetAllGroups(false));
			if (distributionSourcesForm.ShowDialog(this) != DialogResult.OK)
				return;

			if (!distributionSourcesForm.DistributionSourcesChanged)
				return;

			if (_selectedMachine == null)
				return;

			_selectedApplication.Sources.Where(x => distributionSourcesForm.DistributionSources.All(y => y.ID != x.ID))
				.ToList().ForEach(x => _selectedApplication.Sources.Remove(x));

			distributionSourcesForm.DistributionSources
				.Select(x => new
					{
						New = x,
						Old = _selectedApplication.Sources.FirstOrDefault(y => y.ID == x.ID)
					})
				.Where(x => x.Old != null)
				.ToList()
				.ForEach(x => x.New.CopyTo(x.Old));

			distributionSourcesForm.DistributionSources.Where(x => _selectedApplication.Sources.All(y => y.ID != x.ID))
				.ToList().ForEach(x => _selectedApplication.Sources.Add(x));

			DisplayDistributionSourceCount();

			ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
			EnableControls();
		}

		private void CheckBoxDistributionOnly_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxDistributionOnly.Checked)
			{
				textBoxApplicationRelativePath.Text = string.Empty;
				textBoxApplicationArguments.Text = string.Empty;
				checkBoxApplicationWaitForExit.Checked = false;
				comboBoxApplicationSuccessExitCode.SelectedItem = GetSuccessExitCodeItem(SuccessExitCode.Any);
			}

			if (_disableChangedEvents) return;

			UpdateSelectedApplication();
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
			FileSystemBrowserForm fileSystemBrowser = new FileSystemBrowserForm(_selectedMachine)
				{
					Description = "Select an application...",
					SelectedPath = Path.Combine(group.Path, textBoxApplicationRelativePath.Text.Trim(Path.DirectorySeparatorChar)),
					Filter = "Applications (*.exe)|*.exe|All files (*.*)|*.*",
					BrowserMode = FileSystemBrowserForm.Mode.File
				};

			if (fileSystemBrowser.ShowDialog(this) != DialogResult.OK)
				return;

			if (!fileSystemBrowser.SelectedPath.StartsWith(@group.Path, StringComparison.CurrentCultureIgnoreCase))
				Messenger.ShowError("Invalid application path", $"The selected application's path must start with the selected group's path; {@group.Path}");
			else
			{
				textBoxApplicationRelativePath.Text = fileSystemBrowser.SelectedPath.Substring(@group.Path.TrimEnd(Path.DirectorySeparatorChar).Length);
				UpdateSelectedApplication();
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
				Messenger.ShowWarning("Configuration changed", $"The configuration for {e.Machine} was changed from another client."
					+ " Configuration will be reloaded to reflect those changes.");

				ServiceHelper.ReloadConfiguration(e.Machine);
				UpdateSelections();
				PopulateAllControls();
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
					Messenger.ShowError("Failed to connect to machine", $"Could not connect to Process Manager service at {machine.HostName}", e.Exception);
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
			UpdateSelections();
			PopulateAllControls();
			RaiseConfigurationChangedEvent(null);
		}

		#endregion

		#region Macros form event handlers

		private void MacrosForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			MacrosForm macrosForm = _getMacrosForm();
			if (macrosForm == null)
				return;

			macrosForm.FormClosed -= MacrosForm_FormClosed;
			macrosForm.MacrosChanged -= MacrosForm_MacrosChanged;
			_setMacrosForm(null);
		}

		private void MacrosForm_MacrosChanged(object sender, EventArgs e)
		{
			RaiseMacrosChangedEvent();
		}

		#endregion

		#region Event raisers

		private void RaiseConfigurationChangedEvent(List<Machine> machines)
		{
			ConfigurationChanged?.Invoke(this, new MachinesEventArgs(machines));
		}

		private void RaiseMacrosChangedEvent()
		{
			MacrosChanged?.Invoke(this, EventArgs.Empty);
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
			if (!Settings.Client.Machines.Contains(Settings.Constants.LocalMachine))
			{
				Settings.Client.Machines.Insert(0, Settings.Constants.LocalMachine);
				Settings.Client.Save(ClientSettingsType.Machines);
			}

			Machine selectedMachine = new Machine(Settings.Client.CFG_SelectedHostName);
			if (!Settings.Client.Machines.Contains(selectedMachine))
				selectedMachine = Settings.Constants.LocalMachine;

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
				selectedMachine = Settings.Constants.LocalMachine;

			int index = comboBoxMachines.Items.IndexOf(selectedMachine);
			comboBoxMachines.SelectedIndex = (index == -1 ? 0 : index);
		}

		#endregion

		#region Groups

		private void AddGroup(bool makeCopy)
		{
			UpdateSelectedGroup();
			string groupName = GetFirstAvailableDefaultName(
				ConnectionStore.Connections[_selectedMachine].Configuration.Groups.Select(group => group.Name).ToList(),
					makeCopy ? _selectedGroup.Name : "Group");
			_selectedGroup = makeCopy ? _selectedGroup.Copy(groupName) : new Group(groupName);
			ConnectionStore.Connections[_selectedMachine].Configuration.Groups.Add(_selectedGroup);
			ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
			textBoxGroupName.Text = _selectedGroup.Name;
			textBoxGroupPath.Text = _selectedGroup.Path;
			listViewGroupApplications.Items.Clear();
			ListViewItem item = listViewGroups.Items.Add(new ListViewItem(_selectedGroup.Name) { Tag = _selectedGroup });
			listViewGroups.SelectedItems.Clear();
			item.Selected = true;
			panelGroup.Visible = true;
			EnableControls();
			textBoxGroupName.Focus();
		}

		private void UpdateSelectedGroup()
		{
			if (_selectedGroup == null) return;

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

		private bool GroupChanged()
		{
			if (_selectedMachine == null || _selectedGroup == null || string.IsNullOrEmpty(textBoxGroupName.Text))
				return false;

			int equalApplicationsCount = _selectedGroup.Applications.Intersect(listViewGroupApplications.Items.Cast<ListViewItem>().Select(x => (Guid) x.Tag)).Count();
			bool groupChanged = (_selectedGroup.Name != textBoxGroupName.Text || _selectedGroup.Path != textBoxGroupPath.Text
				|| equalApplicationsCount != _selectedGroup.Applications.Count || equalApplicationsCount != listViewGroupApplications.Items.Count);
			ConnectionStore.Connections[_selectedMachine].ConfigurationModified |= groupChanged;
			return groupChanged;
		}

		private IEnumerable<Application> GetNonIncludedGroupApplications()
		{
			if (_selectedMachine == null || !ConnectionStore.ConfigurationAvailable(_selectedMachine))
				return new List<Application>();

			return ConnectionStore.Connections[_selectedMachine].Configuration.Applications
				.Except(ConnectionStore.Connections[_selectedMachine].Configuration.Applications
					.Where(x => listViewGroupApplications.Items.Cast<ListViewItem>().Any(y => (Guid) y.Tag == x.ID)));
		}

		private IEnumerable<Group> GetAllGroups(bool exceptCurrent)
		{
			if (_selectedMachine == null || !ConnectionStore.ConfigurationAvailable(_selectedMachine))
				return new List<Group>();

			List<Group> groups = ConnectionStore.Connections[_selectedMachine].Configuration.Groups;
			return (exceptCurrent ? (_selectedGroup == null ? new List<Group>() : groups.Except(new List<Group>() { _selectedGroup })) : groups);
		}

		private static bool ValidateGroups()
		{
			IDictionary<Machine, Dictionary<Group, string>> nonUniqueGroups =
				ConnectionStore.Connections.Values
					.Where(connection => connection.ConfigurationModified)
					.Select(connection => new
						{
							connection.Machine,
							Groups = connection.Configuration.Groups
								.GroupBy(group => group, new GroupEqualityComparer())
								.Where(x => x.Count() > 1)
								.Select(x => new
									{
										Group = x.Key,
										Message = $"{x.Count()} groups"
									})
								.ToList()
						})
					.Where(x => x.Groups.Count > 0)
					.ToDictionary(x => x.Machine, x => x.Groups.ToDictionary(y => y.Group, y => y.Message));
			if (nonUniqueGroups.Count > 0)
			{
				Messenger.ShowError("Group names not unique",
					"Two or more groups have the same name. See details for more information.",
					nonUniqueGroups.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + Environment.NewLine
						+ y.Value.Aggregate(string.Empty, (a, b) => a + Environment.NewLine + y.Key + " > " + b.Key + ": " + b.Value).Trim()).Trim());
				return false;
			}

			IDictionary<Machine, Dictionary<Group, List<string>>> invalidGroups =
				ConnectionStore.Connections.Values
					.Where(connection => connection.ConfigurationModified)
					.Select(connection => new
						{
							connection.Machine,
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
				Messenger.ShowError($"Group{(invalidGroupCount == 1 ? string.Empty : "s")} invalid",
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

		private void AddApplication(bool makeCopy)
		{
			UpdateSelectedApplication();
			string applicationName = GetFirstAvailableDefaultName(
				ConnectionStore.Connections[_selectedMachine].Configuration.Applications.Select(application => application.Name).ToList(),
					makeCopy ? _selectedApplication.Name : "Application");
			Application previousApplication = _selectedApplication;
			_selectedApplication = makeCopy ? _selectedApplication.Copy(applicationName) : new Application(applicationName);
			ConnectionStore.Connections[_selectedMachine].Configuration.Applications.Add(_selectedApplication);
			if (makeCopy)
			{
				ConnectionStore.Connections[_selectedMachine].Configuration.Groups
					.Where(group => group.Applications.Contains(previousApplication.ID))
					.ToList()
					.ForEach(group => group.Applications.Add(_selectedApplication.ID));
			}
			ConnectionStore.Connections[_selectedMachine].ConfigurationModified = true;
			textBoxApplicationName.Text = _selectedApplication.Name;
			textBoxApplicationRelativePath.Text = _selectedApplication.RelativePath;
			textBoxApplicationArguments.Text = _selectedApplication.Arguments;
			checkBoxApplicationWaitForExit.Checked = _selectedApplication.WaitForExit;
			comboBoxApplicationSuccessExitCode.SelectedItem = GetSuccessExitCodeItem(_selectedApplication.SuccessExitCode);
			checkBoxDistributionOnly.Checked = _selectedApplication.DistributionOnly;
			DisplayDistributionSourceCount();
			ListViewItem item = listViewApplications.Items.Add(new ListViewItem(_selectedApplication.Name) { Tag = _selectedApplication });
			listViewApplications.SelectedItems.Clear();
			item.Selected = true;

			if (makeCopy && listViewGroupApplications.Items.Cast<ListViewItem>().Any(x => (Guid) x.Tag == previousApplication.ID))
				listViewGroupApplications.Items.Add(new ListViewItem(_selectedApplication.Name) { Tag = _selectedApplication.ID });

			panelApplication.Visible = true;
			EnableControls();
			textBoxApplicationName.Focus();
		}

		private void UpdateSelectedApplication()
		{
			if (_selectedApplication == null) return;

			if (ApplicationChanged())
			{
				_selectedApplication.Name = textBoxApplicationName.Text;
				_selectedApplication.RelativePath = textBoxApplicationRelativePath.Text;
				_selectedApplication.Arguments = textBoxApplicationArguments.Text;
				_selectedApplication.WaitForExit = checkBoxApplicationWaitForExit.Checked;
				_selectedApplication.SuccessExitCode = GetSelectedSuccessExitCode();
				_selectedApplication.DistributionOnly = checkBoxDistributionOnly.Checked;
				ListViewItem item = listViewApplications.Items.Cast<ListViewItem>().First(x => x.Tag == _selectedApplication);
				item.Text = _selectedApplication.Name;
				listViewApplications.Sort();
			}
			textBoxApplicationName.Text = _selectedApplication.Name;
			EnableControls();
		}

		private bool ApplicationChanged()
		{
			if (_selectedMachine == null || _selectedApplication == null || string.IsNullOrEmpty(textBoxApplicationName.Text))
				return false;

			bool applicationChanged = (_selectedApplication.Name != textBoxApplicationName.Text
				|| _selectedApplication.RelativePath != textBoxApplicationRelativePath.Text
				|| _selectedApplication.Arguments != textBoxApplicationArguments.Text
				|| _selectedApplication.WaitForExit != checkBoxApplicationWaitForExit.Checked
				|| _selectedApplication.SuccessExitCode != GetSelectedSuccessExitCode()
				|| _selectedApplication.DistributionOnly != checkBoxDistributionOnly.Checked);
			ConnectionStore.Connections[_selectedMachine].ConfigurationModified |= applicationChanged;
			return applicationChanged;
		}

		private static bool ValidateApplications()
		{
			IDictionary<Machine, Dictionary<Application, string>> nonUniqueApplications =
				ConnectionStore.Connections.Values
					.Where(connection => connection.ConfigurationModified)
					.Select(connection => new
						{
							connection.Machine,
							Applications = connection.Configuration.Applications
								.GroupBy(application => application, new ApplicationEqualityComparer())
								.Where(x => x.Count() > 1)
								.Select(x => new
									{
										Application = x.Key,
										Message = $"{x.Count()} applications"
									})
								.ToList()
						})
					.Where(x => x.Applications.Count > 0)
					.ToDictionary(x => x.Machine, x => x.Applications.ToDictionary(y => y.Application, y => y.Message));
			if (nonUniqueApplications.Count > 0)
			{
				Messenger.ShowError("Application names not unique",
					"Two or more applications have the same name. See details for more information.",
					nonUniqueApplications.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + Environment.NewLine
						+ y.Value.Aggregate(string.Empty, (a, b) => a + Environment.NewLine + y.Key + " > " + b.Key + ": " + b.Value).Trim()).Trim());
				return false;
			}

			IDictionary<Machine, Dictionary<Application, List<string>>> invalidApplications =
				ConnectionStore.Connections.Values
					.Where(connection => connection.ConfigurationModified)
					.Select(connection => new
						{
							connection.Machine,
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
				Messenger.ShowError($"Application{(invalidApplicationCount == 1 ? string.Empty : "s")} invalid",
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
			if (!application.DistributionOnly)
			{
				if (string.IsNullOrEmpty(application.RelativePath))
					yield return "Relative path missing";
				else if (Path.IsPathRooted(application.RelativePath.TrimStart(Path.DirectorySeparatorChar)))
					yield return "Relative path can not be rooted";
			}
		}

		private void DisplayDistributionSourceCount()
		{
			labelDistributionSourcesCount.Text = _selectedApplication.Sources.Count
				+ " source" + (_selectedApplication.Sources.Count == 1 ? string.Empty : "s");
		}

		private SuccessExitCode GetSelectedSuccessExitCode()
		{
			return comboBoxApplicationSuccessExitCode.SelectedIndex > 0 ? ((ComboBoxItem<SuccessExitCode>) comboBoxApplicationSuccessExitCode.SelectedItem).Tag : SuccessExitCode.Any;
		}

		private ComboBoxItem<SuccessExitCode> GetSuccessExitCodeItem(SuccessExitCode successExitCode)
		{
			return comboBoxApplicationSuccessExitCode.Items.Cast<ComboBoxItem<SuccessExitCode>>().FirstOrDefault(x => x.Tag == successExitCode);
		}

		#endregion

		private bool Save()
		{
			UpdateSelections();
			return SaveConfiguration();
		}

		private bool SaveConfiguration()
		{
			if (HasUnsavedConfiguration)
			{
				// validate
				if (!ValidateGroups() || !ValidateApplications())
					return false;

				List<Machine> modifiedMachines = ConnectionStore.Connections.Values
					.Where(connection => connection.ConfigurationModified)
					.Select(connection => connection.Machine)
					.ToList();

				// save
				if (!ServiceHelper.TrySaveConfiguration())
					return false;

				RaiseConfigurationChangedEvent(modifiedMachines);

				EnableControls();
			}
			return true;
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
			Group previouslySeletedGroup = _selectedGroup;
			Application previouslySeletedApplication = _selectedApplication;
			ClearAllControls();
			ServiceHelper.WaitForConfiguration(_selectedMachine);
			listViewGroups.Items.Clear();
			listViewApplications.Items.Clear();
			listViewPlugins.Items.Clear();
			if (_selectedMachine != null && ConnectionStore.ConfigurationAvailable(_selectedMachine))
			{
				ConnectionStore.Connections[_selectedMachine].Configuration.Groups.ForEach(group => listViewGroups.Items.Add(new ListViewItem(group.Name) { Tag = group }));
				ConnectionStore.Connections[_selectedMachine].Configuration.Applications.ForEach(application => listViewApplications.Items.Add(new ListViewItem(application.Name) { Tag = application }));
				if (previouslySeletedGroup != null)
				{
					ListViewItem groupItem = listViewGroups.Items.Cast<ListViewItem>().FirstOrDefault(item => previouslySeletedGroup.Equals(item.Tag));
					if (groupItem != null)
						groupItem.Selected = true;
				}
				if (previouslySeletedApplication != null)
				{
					ListViewItem applicationItem = listViewApplications.Items.Cast<ListViewItem>().FirstOrDefault(item => previouslySeletedApplication.Equals(item.Tag));
					if (applicationItem != null)
						applicationItem.Selected = true;
				}
			}
			ShowAllControls(_selectedMachine != null && ConnectionStore.ConfigurationAvailable(_selectedMachine));
			EnableControls();
		}

		private void EnableControls(bool enable = true)
		{
			buttonApply.Enabled = (enable && HasUnsavedConfiguration);

			buttonCopyGroup.Enabled = (enable && listViewGroups.SelectedItems.Count == 1);
			buttonRemoveGroup.Enabled = (enable && listViewGroups.SelectedItems.Count > 0);
			buttonAddGroupApplication.Enabled = (enable && GetNonIncludedGroupApplications().Any());
			buttonRemoveGroupApplication.Enabled = (enable && listViewGroupApplications.SelectedItems.Count > 0);
			buttonCopyGroupApplications.Enabled = (enable && GetAllGroups(true).Any());

			buttonCopyApplication.Enabled = (enable && listViewApplications.SelectedItems.Count == 1);
			buttonRemoveApplication.Enabled = (enable && listViewApplications.SelectedItems.Count > 0);
			labelApplicationRelativePath.Enabled = (enable && _selectedApplication != null && !_selectedApplication.DistributionOnly);
			labelApplicationArguments.Enabled = (enable && _selectedApplication != null && !_selectedApplication.DistributionOnly);
			textBoxApplicationRelativePath.Enabled = (enable && _selectedApplication != null && !_selectedApplication.DistributionOnly);
			textBoxApplicationArguments.Enabled = (enable && _selectedApplication != null && !_selectedApplication.DistributionOnly);
			buttonBrowseApplicationRelativePath.Enabled = (enable && _selectedApplication != null && !_selectedApplication.DistributionOnly);
			labelApplicationWaitForExit.Enabled = (enable && _selectedApplication != null && !_selectedApplication.DistributionOnly);
			checkBoxApplicationWaitForExit.Enabled = (enable && _selectedApplication != null && !_selectedApplication.DistributionOnly);
			labelApplicationSuccessExitCode.Enabled = (enable && _selectedApplication != null && !_selectedApplication.DistributionOnly && checkBoxApplicationWaitForExit.Checked);
			comboBoxApplicationSuccessExitCode.Enabled = (enable && _selectedApplication != null && !_selectedApplication.DistributionOnly && checkBoxApplicationWaitForExit.Checked);
		}

		#endregion

		public static string GetFirstAvailableDefaultName(ICollection<string> existingNames, string nameTemplate)
		{
			int tryNo = 0; string name; bool foundNo = false;
			int lastIndex = nameTemplate.LastIndexOf(' ');
			if (lastIndex != -1 && lastIndex < nameTemplate.Length - 1)
			{
				int existingNo;
				if (int.TryParse(nameTemplate.Substring(lastIndex + 1), out existingNo))
				{
					tryNo = existingNo;
					foundNo = true;
				}
			}
			nameTemplate = (foundNo ? nameTemplate.Substring(0, lastIndex) : nameTemplate.Trim()) + " ";
			while (existingNames.Contains(name = nameTemplate + (++tryNo))) { }
			return name;
		}
	}
}

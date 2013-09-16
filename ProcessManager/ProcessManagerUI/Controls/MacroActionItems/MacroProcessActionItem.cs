using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.DataObjects.Comparers;
using ProcessManagerUI.Controls.MacroActionItems.Support;
using ProcessManagerUI.Support;
using ProcessManagerUI.Utilities;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Controls.MacroActionItems
{
	public partial class MacroProcessActionItem : UserControl, IMacroActionItem
	{
		private const string DEFAULT_MACHINE = "Machine...";
		private const string DEFAULT_GROUP = "Group...";
		private const string DEFAULT_APPLICATION = "Application...";

		private List<Machine> _selectedMachines;
		private Group _selectedGroup;
		private List<Application> _selectedApplications;

		public MacroProcessActionItem()
		{
			InitializeComponent();
			ActionBundle = null;
		}

		public MacroProcessActionItem(MacroActionBundle actionBundle) : this()
		{
            if (actionBundle.Type != MacroActionType.Start && actionBundle.Type != MacroActionType.Stop && actionBundle.Type != MacroActionType.Restart)
                throw new ArgumentException("Invalid process action type");

			ActionBundle = actionBundle;
		}

		#region Properties

		public MacroActionBundle ActionBundle { get; private set; }

		private List<Machine> SelectedMachines
		{
			get { return _selectedMachines; }
			set
			{
				_selectedMachines = value != null && value.Count > 0 ? value : null;
				UpdateLinkLabelMachines();
				UpdateSelectedGroup();
			}
		}

		private Group SelectedGroup
		{
			get { return _selectedGroup; }
			set
			{
				_selectedGroup = value;
				UpdateLinkLabelGroup();
				UpdateSelectedApplications();
			}
		}

		private List<Application> SelectedApplications
		{
			get { return _selectedApplications; }
			set
			{
				_selectedApplications = value != null && value.Count > 0 ? value : null;
				UpdateLinkLabelApplications();
			}
		}

		private bool HasValidSelections
		{
			get { return SelectedMachines != null && SelectedGroup != null && SelectedApplications != null; }
		}

		#endregion

		#region GUI event handlers

		private void MacroDistributionActionItem_Load(object sender, EventArgs e)
		{
			if (ActionBundle == null)
				throw new InvalidOperationException();

			_selectedMachines = ActionBundle.Actions
				.Select(macroAction => ((MacroProcessAction) macroAction).MachineID)
				.Select(machineID => ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.Select(connection => connection.Machine)
					.FirstOrDefault(machine => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(machine, machineID)))
				.Where(machine => machine != null)
				.Distinct(new MachineEqualityComparer())
				.OrderBy(machine => machine.HostName)
				.ToList();

			_selectedGroup = ActionBundle.Actions
				.Select(macroAction => ((MacroProcessAction) macroAction).GroupID)
				.Select(groupID => SelectedMachines
					.SelectMany(machine => ConnectionStore.Connections.Values
						.First(connection => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(connection.Machine, machine))
						.Configuration
						.Groups)
					.FirstOrDefault(group => ProcessManager.DataObjects.Comparers.Comparer<Group>.IDObjectsEqual(group, groupID)))
				.FirstOrDefault(group => group != null);

			_selectedApplications = ActionBundle.Actions
				.Select(macroAction => ((MacroProcessAction) macroAction).ApplicationID)
				.Select(applicationID => SelectedMachines
					.SelectMany(machine => ConnectionStore.Connections.Values
						.First(connection => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(connection.Machine, machine))
						.Configuration
						.Applications)
					.FirstOrDefault(application => ProcessManager.DataObjects.Comparers.Comparer<Application>.IDObjectsEqual(application, applicationID)))
				.Where(application => application != null)
				.Distinct(new ApplicationEqualityComparer())
				.ToList();

			UpdateControls();
		}

		private void LinkLabelMachines_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			IEnumerable<Tuple<Machine, bool>> machines = ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer())
				.Select(machine => new Tuple<Machine, bool>(machine, SelectedMachines != null
					&& SelectedMachines.Any(x => Comparer.MachinesEqual(machine, x))));

			Picker.ShowMultiSelectMenu(linkLabelMachines, machines, ContextMenu_SelectMachines_MenuClosed);
		}

		private void LinkLabelGroup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			IEnumerable<Group> groups = SelectedMachines
				.Select(machine => ConnectionStore.Connections.Values
					.First(connection => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(connection.Machine, machine))
					.Configuration)
				.SelectMany(configuration => configuration.Groups)
				.Distinct(new GroupEqualityComparer());

			Picker.ShowMenu(linkLabelGroup, groups, ContextMenu_SelectGroup_GroupClicked);
		}

		private void LinkLabelApplications_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			IEnumerable<Tuple<Application, bool>> applications = SelectedMachines
				.Select(machine => ConnectionStore.Connections.Values
					.First(connection => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(connection.Machine, machine))
					.Configuration)
				.SelectMany(configuration => configuration
					.Applications
					.Where(application => configuration
						.Groups
						.Where(group => Comparer.GroupsEqual(group, SelectedGroup))
						.SelectMany(group => group.Applications)
						.Any(applicationID => ProcessManager.DataObjects.Comparers.Comparer<Application>.IDObjectsEqual(application, applicationID))))
				.Distinct(new ApplicationEqualityComparer())
				.Select(application => new Tuple<Application, bool>(application, SelectedApplications != null
					&& SelectedApplications.Any(x => Comparer.ApplicationsEqual(application, x))));

			Picker.ShowMultiSelectMenu(linkLabelApplications, applications, ContextMenu_SelectApplications_MenuClosed);
		}

		//private void ComboBoxMachine_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	if (comboBoxMachine.SelectedIndex > -1)
		//	{
		//		Machine sourceMachine = ((ComboBoxItem<Machine>) comboBoxMachine.Items[comboBoxMachine.SelectedIndex]).Tag;
		//		ActionBundle.MachineID = (sourceMachine != null ? sourceMachine.ID : Guid.Empty);
		//	}
		//	else
		//		ActionBundle.MachineID = Guid.Empty;

		//	if (ActionBundle.MachineID == Guid.Empty)
		//		ClearGroupsComboBox();
		//	else
		//		FillGroupsComboBox();
		//}

		//private void ComboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	if (comboBoxGroup.SelectedIndex > -1)
		//	{
		//		Group group = ((ComboBoxItem<Group>) comboBoxGroup.Items[comboBoxGroup.SelectedIndex]).Tag;
		//		ActionBundle.GroupID = (group != null ? group.ID : Guid.Empty);
		//	}
		//	else
		//		ActionBundle.GroupID = Guid.Empty;

		//	if (ActionBundle.GroupID == Guid.Empty)
		//		ClearApplicationsComboBox();
		//	else
		//		FillApplicationsComboBox();
		//}

		//private void ComboBoxApplication_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	if (comboBoxApplication.SelectedIndex > -1)
		//	{
		//		Application application = ((ComboBoxItem<Application>) comboBoxApplication.Items[comboBoxApplication.SelectedIndex]).Tag;
		//		ActionBundle.ApplicationID = (application != null ? application.ID : Guid.Empty);
		//	}
		//	else
		//		ActionBundle.ApplicationID = Guid.Empty;
		//}

		#endregion

		#region Picker event handlers

		private void ContextMenu_SelectMachines_MenuClosed(List<Machine> machines)
		{
			SelectedMachines = machines;
			UpdateMacroActionBundle();
		}

		private void ContextMenu_SelectGroup_GroupClicked(Group group)
		{
			SelectedGroup = group;
			UpdateMacroActionBundle();
		}

		private void ContextMenu_SelectApplications_MenuClosed(List<Application> application)
		{
			SelectedApplications = application;
			UpdateMacroActionBundle();
		}

		#endregion

		public void SetWidth(int width)
		{
			Width = width;
		}

		#region Helpers

		private void UpdateSelectedMachines()
		{
			SelectedMachines = ActionBundle.Actions
				.Select(macroAction => ((MacroProcessAction) macroAction).MachineID)
				.Select(machineID => ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.Select(connection => connection.Machine)
					.FirstOrDefault(machine => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(machine, machineID)))
				.Where(machine => machine != null)
				.Distinct(new MachineEqualityComparer())
				.OrderBy(machine => machine.HostName)
				.ToList();
		}

		private void UpdateSelectedGroup()
		{
			if (SelectedGroup == null)
				return;

			SelectedGroup = SelectedMachines == null ? null : SelectedMachines
				.SelectMany(machine => ConnectionStore.Connections.Values
					.First(connection => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(connection.Machine, machine))
					.Configuration
					.Groups)
				.FirstOrDefault(group => Comparer.GroupsEqual(group, SelectedGroup));
		}

		private void UpdateSelectedApplications()
		{
			if (SelectedApplications == null)
				return;

			SelectedApplications = SelectedMachines == null ? null : SelectedApplications
				.Select(selectedApplication => SelectedMachines
					.Select(machine => ConnectionStore.Connections.Values
						.First(connection => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(connection.Machine, machine))
						.Configuration)
					.SelectMany(configuration => configuration
						.Applications
						.Where(application => configuration
							.Groups
							.Where(group => Comparer.GroupsEqual(group, SelectedGroup))
							.SelectMany(group => group.Applications)
							.Any(appID => ProcessManager.DataObjects.Comparers.Comparer<Application>.IDObjectsEqual(application, appID))))
					.FirstOrDefault(application => Comparer.ApplicationsEqual(application, selectedApplication)))
				.Where(application => application != null)
				.Distinct(new ApplicationEqualityComparer())
				.ToList();
		}

		private void UpdateControls()
		{
			UpdateLinkLabelMachines();
			UpdateLinkLabelGroup();
			UpdateLinkLabelApplications();
		}

		private void UpdateLinkLabelMachines()
		{
			linkLabelMachines.Text = _selectedMachines != null
				? string.Join(", ", _selectedMachines.Select(machine => machine.HostName))
				: DEFAULT_MACHINE;
			linkLabelGroup.Enabled = _selectedMachines != null;
		}

		private void UpdateLinkLabelGroup()
		{
			linkLabelGroup.Text = _selectedGroup != null ? _selectedGroup.Name : DEFAULT_GROUP;
			linkLabelApplications.Enabled = _selectedGroup != null;
		}

		private void UpdateLinkLabelApplications()
		{
			linkLabelApplications.Text = _selectedApplications != null
				? string.Join(", ", _selectedApplications.Select(application => application.Name))
				: DEFAULT_APPLICATION;
		}

		private void UpdateMacroActionBundle()
		{
			ActionBundle.Actions.Clear();
			if (HasValidSelections)
				ActionBundle.Actions.AddRange(SelectedMachines.SelectMany(machine =>
					SelectedApplications.Select(application =>
						new MacroProcessAction(ActionBundle.Type, machine.ID, SelectedGroup.ID, application.ID))));
		}

		//private void ClearMachinesComboBox()
		//{
		//	comboBoxMachine.Items.Clear();
		//	ClearGroupsComboBox();
		//}

		//private void FillMachinesComboBox()
		//{
		//	ClearMachinesComboBox();
		//	comboBoxMachine.Items.Add(new ComboBoxItem<Machine>(string.Empty));
		//	foreach (Machine machine in ConnectionStore.Connections.Values
		//		.Where(connection => connection.Configuration != null)
		//		.Select(connection => connection.Machine)
		//		.Distinct(new MachineEqualityComparer())
		//		.OrderBy(machine => machine.HostName))
		//	{
		//		int index = comboBoxMachine.Items.Add(new ComboBoxItem<Machine>(machine));
		//		if (ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(machine, ActionBundle.MachineID))
		//			comboBoxMachine.SelectedIndex = index;
		//	}
		//}

		//private void ClearGroupsComboBox()
		//{
		//	comboBoxGroup.Items.Clear();
		//	ClearApplicationsComboBox();
		//}

		//private void FillGroupsComboBox()
		//{
		//	ClearGroupsComboBox();
		//	comboBoxGroup.Items.Add(new ComboBoxItem<Group>(string.Empty));
		//	Configuration configuration = ConnectionStore.Connections.Values
		//		.First(connection => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(connection.Machine, ActionBundle.MachineID))
		//		.Configuration;
		//	foreach (Group group in configuration
		//		.Groups
		//		.Distinct(new GroupEqualityComparer())
		//		.OrderBy(group => group.Name))
		//	{
		//		int index = comboBoxGroup.Items.Add(new ComboBoxItem<Group>(group));
		//		if (ProcessManager.DataObjects.Comparers.Comparer<Group>.IDObjectsEqual(group, ActionBundle.GroupID))
		//			comboBoxGroup.SelectedIndex = index;
		//	}
		//}

		//private void ClearApplicationsComboBox()
		//{
		//	comboBoxApplication.Items.Clear();
		//}

		//private void FillApplicationsComboBox()
		//{
		//	ClearApplicationsComboBox();
		//	comboBoxApplication.Items.Add(new ComboBoxItem<Application>(string.Empty));
		//	Configuration configuration = ConnectionStore.Connections.Values
		//		.First(connection => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(connection.Machine, ActionBundle.MachineID))
		//		.Configuration;
		//	foreach (Application application in configuration
		//		.Applications
		//		.Where(application => configuration.Groups.First(group => ProcessManager.DataObjects.Comparers.Comparer<Group>.IDObjectsEqual(group, ActionBundle.GroupID)).Applications.Contains(application.ID))
		//		.Distinct(new ApplicationEqualityComparer())
		//		.OrderBy(application => application.Name))
		//	{
		//		int index = comboBoxApplication.Items.Add(new ComboBoxItem<Application>(application));
		//		if (ProcessManager.DataObjects.Comparers.Comparer<Application>.IDObjectsEqual(application, ActionBundle.ApplicationID))
		//			comboBoxApplication.SelectedIndex = index;
		//	}
		//}

		#endregion
	}
}

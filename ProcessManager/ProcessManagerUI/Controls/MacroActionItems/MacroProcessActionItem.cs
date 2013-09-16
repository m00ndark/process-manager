using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.DataObjects.Comparers;
using ProcessManagerUI.Controls.MacroActionItems.Support;
using ProcessManagerUI.Utilities;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Controls.MacroActionItems
{
	public partial class MacroProcessActionItem : UserControl, IMacroActionItem
	{
		private const string DEFAULT_MACHINE = "Machines...";
		private const string DEFAULT_GROUP = "Group...";
		private const string DEFAULT_APPLICATION = "Applications...";

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

		public event EventHandler MacroActionItemChanged;

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
				.ToList();

			_selectedGroup = ActionBundle.Actions
				.Select(macroAction => ((MacroProcessAction) macroAction).GroupID)
				.Select(groupID => ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.SelectMany(connection => connection.Configuration.Groups)
					.FirstOrDefault(group => ProcessManager.DataObjects.Comparers.Comparer<Group>.IDObjectsEqual(group, groupID)))
				.FirstOrDefault(group => group != null);

			_selectedApplications = ActionBundle.Actions
				.Select(macroAction => ((MacroProcessAction) macroAction).ApplicationID)
				.Select(applicationID => ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.SelectMany(connection => connection.Configuration.Applications)
					.FirstOrDefault(application => ProcessManager.DataObjects.Comparers.Comparer<Application>.IDObjectsEqual(application, applicationID)))
				.Where(application => application != null)
				.Distinct(new ApplicationEqualityComparer())
				.ToList();

			UpdateSelections();
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

		#region Event raisers

		private void RaiseMacroActionItemChangedEvent()
		{
			if (MacroActionItemChanged != null)
				MacroActionItemChanged(this, EventArgs.Empty);
		}

		#endregion

		public void SetWidth(int width)
		{
			Width = width;
		}

		#region Helpers

		private void UpdateSelections()
		{
			UpdateSelectedMachines();
			UpdateSelectedGroup();
			UpdateSelectedApplications();
		}

		private void UpdateSelectedMachines()
		{
			if (SelectedMachines != null)
			{
				SelectedMachines = SelectedMachines
					.Select(selectedMachine => ConnectionStore.Connections.Values
						.Where(connection => connection.Configuration != null)
						.Select(connection => connection.Machine)
						.FirstOrDefault(machine => Comparer.MachinesEqual(machine, selectedMachine)))
					.Where(machine => machine != null)
					.Distinct(new MachineEqualityComparer())
					.ToList();
			}
			UpdateLinkLabelMachines();
		}

		private void UpdateSelectedGroup()
		{
			if (SelectedGroup != null)
			{
				SelectedGroup = SelectedMachines == null ? null : SelectedMachines
					.SelectMany(machine => ConnectionStore.Connections.Values
						.First(connection => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(connection.Machine, machine))
						.Configuration
						.Groups)
					.FirstOrDefault(group => Comparer.GroupsEqual(group, SelectedGroup));
			}
			UpdateLinkLabelGroup();
		}

		private void UpdateSelectedApplications()
		{
			if (SelectedApplications != null)
			{
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
			UpdateLinkLabelApplications();
		}

		private void UpdateLinkLabelMachines()
		{
			linkLabelMachines.Text = _selectedMachines != null
				? string.Join(", ", _selectedMachines.OrderBy(machine => machine.HostName).Select(machine => machine.HostName))
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
				? string.Join(", ", _selectedApplications.OrderBy(machine => machine.Name).Select(application => application.Name))
				: DEFAULT_APPLICATION;
		}

		private void UpdateMacroActionBundle()
		{
			ActionBundle.Actions.Clear();
			if (HasValidSelections)
				ActionBundle.Actions.AddRange(SelectedMachines.SelectMany(machine =>
					SelectedApplications.Select(application =>
						new MacroProcessAction(ActionBundle.Type, machine.ID, SelectedGroup.ID, application.ID))));

			RaiseMacroActionItemChangedEvent();
		}

		#endregion
	}
}

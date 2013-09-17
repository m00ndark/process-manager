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
	public partial class MacroDistributionActionItem : UserControl, IMacroActionItem
	{
		private const string DEFAULT_SOURCE_MACHINE = "Machine...";
		private const string DEFAULT_GROUP = "Group...";
		private const string DEFAULT_APPLICATION = "Applications...";
		private const string DEFAULT_DESTINATION_MACHINE = "Machines...";

		private Machine _selectedSourceMachine;
		private Group _selectedGroup;
		private List<Application> _selectedApplications;
		private List<Machine> _selectedDestinationMachines;

		public MacroDistributionActionItem()
		{
			InitializeComponent();
			ActionBundle = null;
		}

		public MacroDistributionActionItem(MacroActionBundle actionBundle) : this()
		{
			if (actionBundle.Type != MacroActionType.Distribute)
				throw new ArgumentException("Invalid distribution action type");

			ActionBundle = actionBundle;
		}

		public event EventHandler MacroActionItemChanged;

		#region Properties

		public MacroActionBundle ActionBundle { get; private set; }

		private Machine SelectedSourceMachine
		{
			get { return _selectedSourceMachine; }
			set
			{
				_selectedSourceMachine = value;
				UpdateLinkLabelSourceMachine();
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

		private List<Machine> SelectedDestinationMachines
		{
			get { return _selectedDestinationMachines; }
			set
			{
				_selectedDestinationMachines = value != null && value.Count > 0 ? value : null;
				UpdateLinkLabelDestinationMachines();
			}
		}

		private bool HasValidSelections
		{
			get { return SelectedSourceMachine != null && SelectedGroup != null && SelectedApplications != null && SelectedDestinationMachines != null; }
		}

		#endregion

		#region GUI event handlers

		private void MacroDistributionActionItem_Load(object sender, EventArgs e)
		{
			if (ActionBundle == null)
				throw new InvalidOperationException();

			_selectedSourceMachine = ActionBundle.Actions
				.Select(macroAction => ((MacroDistributionAction) macroAction).SourceMachineID)
				.Select(machineID => ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.Select(connection => connection.Machine)
					.FirstOrDefault(machine => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(machine, machineID)))
				.FirstOrDefault(machine => machine != null);

			_selectedGroup = ActionBundle.Actions
				.Select(macroAction => ((MacroDistributionAction) macroAction).GroupID)
				.Select(groupID => ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.SelectMany(connection => connection.Configuration.Groups)
					.FirstOrDefault(group => ProcessManager.DataObjects.Comparers.Comparer<Group>.IDObjectsEqual(group, groupID)))
				.FirstOrDefault(group => group != null);

			_selectedApplications = ActionBundle.Actions
				.Select(macroAction => ((MacroDistributionAction) macroAction).ApplicationID)
				.Select(applicationID => ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.SelectMany(connection => connection.Configuration.Applications)
					.FirstOrDefault(application => ProcessManager.DataObjects.Comparers.Comparer<Application>.IDObjectsEqual(application, applicationID)))
				.Where(application => application != null)
				.Distinct(new ApplicationEqualityComparer())
				.ToList();

			_selectedDestinationMachines = ActionBundle.Actions
				.Select(macroAction => ((MacroDistributionAction) macroAction).DestinationMachineID)
				.Select(machineID => ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.Select(connection => connection.Machine)
					.FirstOrDefault(machine => ProcessManager.DataObjects.Comparers.Comparer<Machine>.IDObjectsEqual(machine, machineID)))
				.Where(machine => machine != null)
				.Distinct(new MachineEqualityComparer())
				.ToList();

			UpdateSelections();
		}

		private void LinkLabelSourceMachine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			IEnumerable<Machine> machines = ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer());

			Picker.ShowMenu(linkLabelSourceMachine, machines, ContextMenu_SelectSourceMachine_MachineClicked);
		}

		private void LinkLabelGroup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			IEnumerable<Group> groups = ConnectionStore.Connections[SelectedSourceMachine].Configuration.Groups;

			Picker.ShowMenu(linkLabelGroup, groups, ContextMenu_SelectGroup_GroupClicked);
		}

		private void LinkLabelApplications_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			IEnumerable<Tuple<Application, bool>> applications = ConnectionStore.Connections[SelectedSourceMachine]
				.Configuration
				.Applications
					.Where(application => ConnectionStore.Connections[SelectedSourceMachine].Configuration
						.Groups
						.Where(group => Comparer.GroupsEqual(group, SelectedGroup))
						.SelectMany(group => group.Applications)
						.Any(applicationID => ProcessManager.DataObjects.Comparers.Comparer<Application>.IDObjectsEqual(application, applicationID)))
				.Distinct(new ApplicationEqualityComparer())
				.Select(application => new Tuple<Application, bool>(application, SelectedApplications != null
					&& SelectedApplications.Any(x => Comparer.ApplicationsEqual(application, x))));

			Picker.ShowMultiSelectMenu(linkLabelApplications, applications, ContextMenu_SelectApplications_MenuClosed);
		}

		private void LinkLabelDestinationMachines_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			IEnumerable<Tuple<Machine, bool>> machines = ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer())
				.Select(machine => new Tuple<Machine, bool>(machine, SelectedDestinationMachines != null
					&& SelectedDestinationMachines.Any(x => Comparer.MachinesEqual(machine, x))));

			Picker.ShowMultiSelectMenu(linkLabelDestinationMachines, machines, ContextMenu_SelectDestinationMachines_MenuClosed);
		}

		#endregion

		#region Picker event handlers

		private void ContextMenu_SelectSourceMachine_MachineClicked(Machine machine)
		{
			SelectedSourceMachine = machine;
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

		private void ContextMenu_SelectDestinationMachines_MenuClosed(List<Machine> machines)
		{
			SelectedDestinationMachines = machines;
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
			UpdateSelectedSourceMachine();
			UpdateSelectedGroup();
			UpdateSelectedApplications();
			UpdateSelectedDestinationMachines();
		}

		private void UpdateSelectedSourceMachine()
		{
			if (SelectedSourceMachine != null)
			{
				SelectedSourceMachine = ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.Select(connection => connection.Machine)
					.FirstOrDefault(machine => Comparer.MachinesEqual(machine, SelectedSourceMachine));
			}
			UpdateLinkLabelSourceMachine();
		}

		private void UpdateSelectedGroup()
		{
			if (SelectedGroup != null)
			{
				SelectedGroup = SelectedSourceMachine == null || !ConnectionStore.ConfigurationAvailable(SelectedSourceMachine)
					? null
					: ConnectionStore.Connections[SelectedSourceMachine].Configuration.Groups
						.FirstOrDefault(group => Comparer.GroupsEqual(group, SelectedGroup));
			}
			UpdateLinkLabelGroup();
		}

		private void UpdateSelectedApplications()
		{
			if (SelectedApplications != null)
			{
				SelectedApplications = SelectedSourceMachine == null || !ConnectionStore.ConfigurationAvailable(SelectedSourceMachine)
					? null
					: SelectedApplications
						.Select(selectedApplication => ConnectionStore.Connections[SelectedSourceMachine].Configuration
							.Applications
							.Where(application => ConnectionStore.Connections[SelectedSourceMachine].Configuration
								.Groups
								.Where(group => Comparer.GroupsEqual(group, SelectedGroup))
								.SelectMany(group => group.Applications)
								.Any(appID => ProcessManager.DataObjects.Comparers.Comparer<Application>.IDObjectsEqual(application, appID)))
							.FirstOrDefault(application => Comparer.ApplicationsEqual(application, selectedApplication)))
						.Where(application => application != null)
						.Distinct(new ApplicationEqualityComparer())
						.ToList();
			}
			UpdateLinkLabelApplications();
		}

		private void UpdateSelectedDestinationMachines()
		{
			if (SelectedDestinationMachines != null)
			{
				SelectedDestinationMachines = SelectedDestinationMachines
					.Select(selectedMachine => ConnectionStore.Connections.Values
						.Where(connection => connection.Configuration != null)
						.Select(connection => connection.Machine)
						.FirstOrDefault(machine => Comparer.MachinesEqual(machine, selectedMachine)))
					.Where(machine => machine != null)
					.Distinct(new MachineEqualityComparer())
					.ToList();
			}
			UpdateLinkLabelDestinationMachines();
		}

		private void UpdateLinkLabelSourceMachine()
		{
			linkLabelSourceMachine.Text = _selectedSourceMachine != null ? _selectedSourceMachine.HostName : DEFAULT_SOURCE_MACHINE;
			linkLabelGroup.Enabled = _selectedSourceMachine != null;
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

		private void UpdateLinkLabelDestinationMachines()
		{
			linkLabelDestinationMachines.Text = _selectedDestinationMachines != null
				? string.Join(", ", _selectedDestinationMachines.OrderBy(machine => machine.HostName).Select(machine => machine.HostName))
				: DEFAULT_DESTINATION_MACHINE;
		}

		private void UpdateMacroActionBundle()
		{
			ActionBundle.Actions.Clear();
			if (HasValidSelections)
				ActionBundle.Actions.AddRange(SelectedApplications.SelectMany(application =>
					SelectedDestinationMachines.Select(destinationMachine =>
						new MacroDistributionAction(ActionBundle.Type, SelectedSourceMachine.ID, SelectedGroup.ID, application.ID, destinationMachine.ID))));

			RaiseMacroActionItemChangedEvent();
		}

		#endregion
	}
}

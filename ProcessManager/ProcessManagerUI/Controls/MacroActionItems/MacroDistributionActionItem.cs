using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.DataObjects.Comparers;
using ProcessManagerUI.Controls.MacroActionItems.Support;
using ProcessManagerUI.Support;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Controls.MacroActionItems
{
	public partial class MacroDistributionActionItem : UserControl, IMacroActionItem
	{
		public MacroDistributionActionItem()
		{
			InitializeComponent();
			MacroDistributionAction = null;
		}

		public MacroDistributionActionItem(MacroDistributionAction macroDistributionAction) : this()
		{
			MacroDistributionAction = macroDistributionAction;
		}

		#region Properties

		public MacroDistributionAction MacroDistributionAction { get; private set; }

		#endregion

		#region GUI event handlers

		private void MacroDistributionActionItem_Load(object sender, EventArgs e)
		{
			if (MacroDistributionAction == null)
				throw new InvalidOperationException();

			FillSourceMachinesComboBox();
		}

		private void ComboBoxSourceMachine_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxSourceMachine.SelectedIndex > -1)
			{
				Machine sourceMachine = ((ComboBoxItem<Machine>) comboBoxSourceMachine.Items[comboBoxSourceMachine.SelectedIndex]).Tag;
				MacroDistributionAction.SourceMachineID = (sourceMachine != null ? sourceMachine.ID : Guid.Empty);
			}
			else
				MacroDistributionAction.SourceMachineID = Guid.Empty;

			if (MacroDistributionAction.SourceMachineID == Guid.Empty)
				ClearGroupsComboBox();
			else
				FillGroupsComboBox();
		}

		private void ComboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxGroup.SelectedIndex > -1)
			{
				Group group = ((ComboBoxItem<Group>) comboBoxGroup.Items[comboBoxGroup.SelectedIndex]).Tag;
				MacroDistributionAction.GroupID = (group != null ? group.ID : Guid.Empty);
			}
			else
				MacroDistributionAction.GroupID = Guid.Empty;

			if (MacroDistributionAction.GroupID == Guid.Empty)
				ClearApplicationsComboBox();
			else
				FillApplicationsComboBox();
		}

		private void ComboBoxApplication_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxApplication.SelectedIndex > -1)
			{
				Application application = ((ComboBoxItem<Application>) comboBoxApplication.Items[comboBoxApplication.SelectedIndex]).Tag;
				MacroDistributionAction.ApplicationID = (application != null ? application.ID : Guid.Empty);
			}
			else
				MacroDistributionAction.ApplicationID = Guid.Empty;

			if (MacroDistributionAction.ApplicationID == Guid.Empty)
				ClearDestinationMachinesComboBox();
			else
				FillDestinationMachinesComboBox();
		}

		private void ComboBoxDestinationMachine_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDestinationMachine.SelectedIndex > -1)
			{
				Machine destinationMachine = ((ComboBoxItem<Machine>) comboBoxDestinationMachine.Items[comboBoxDestinationMachine.SelectedIndex]).Tag;
				MacroDistributionAction.DestinationMachineID = (destinationMachine != null ? destinationMachine.ID : Guid.Empty);
			}
			else
				MacroDistributionAction.DestinationMachineID = Guid.Empty;
		}

		#endregion

		public void SetWidth(int width)
		{
			Width = width;
		}

		#region Helpers

		private void ClearSourceMachinesComboBox()
		{
			comboBoxSourceMachine.Items.Clear();
			ClearGroupsComboBox();
		}

		private void FillSourceMachinesComboBox()
		{
			ClearSourceMachinesComboBox();
			comboBoxSourceMachine.Items.Add(new ComboBoxItem<Machine>(string.Empty));
			foreach (Machine machine in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer())
				.OrderBy(machine => machine.HostName))
			{
				int index = comboBoxSourceMachine.Items.Add(new ComboBoxItem<Machine>(machine));
				if (Comparer<Machine>.IDObjectsEqual(machine, MacroDistributionAction.SourceMachineID))
					comboBoxSourceMachine.SelectedIndex = index;
			}
		}

		private void ClearGroupsComboBox()
		{
			comboBoxGroup.Items.Clear();
			ClearApplicationsComboBox();
		}

		private void FillGroupsComboBox()
		{
			ClearGroupsComboBox();
			comboBoxGroup.Items.Add(new ComboBoxItem<Group>(string.Empty));
			Configuration sourceConfiguration = ConnectionStore.Connections.Values
				.First(connection => Comparer<Machine>.IDObjectsEqual(connection.Machine, MacroDistributionAction.SourceMachineID))
				.Configuration;
			foreach (Group group in sourceConfiguration
				.Groups
				.Distinct(new GroupEqualityComparer())
				.OrderBy(group => group.Name))
			{
				int index = comboBoxGroup.Items.Add(new ComboBoxItem<Group>(group));
				if (Comparer<Group>.IDObjectsEqual(group, MacroDistributionAction.GroupID))
					comboBoxGroup.SelectedIndex = index;
			}
		}

		private void ClearApplicationsComboBox()
		{
			comboBoxApplication.Items.Clear();
			ClearDestinationMachinesComboBox();
		}

		private void FillApplicationsComboBox()
		{
			ClearApplicationsComboBox();
			comboBoxApplication.Items.Add(new ComboBoxItem<Application>(string.Empty));
			Configuration sourceConfiguration = ConnectionStore.Connections.Values
				.First(connection => Comparer<Machine>.IDObjectsEqual(connection.Machine, MacroDistributionAction.SourceMachineID))
				.Configuration;
			foreach (Application application in sourceConfiguration
				.Applications
				.Where(application => sourceConfiguration.Groups.First(group => Comparer<Group>.IDObjectsEqual(group, MacroDistributionAction.GroupID)).Applications.Contains(application.ID))
				.Where(application => application.Sources.Count > 0)
				.Distinct(new ApplicationEqualityComparer())
				.OrderBy(application => application.Name))
			{
				int index = comboBoxApplication.Items.Add(new ComboBoxItem<Application>(application));
				if (Comparer<Application>.IDObjectsEqual(application, MacroDistributionAction.ApplicationID))
					comboBoxApplication.SelectedIndex = index;
			}
		}

		private void ClearDestinationMachinesComboBox()
		{
			comboBoxDestinationMachine.Items.Clear();
		}

		private void FillDestinationMachinesComboBox()
		{
			ClearDestinationMachinesComboBox();
			comboBoxDestinationMachine.Items.Add(new ComboBoxItem<Machine>(string.Empty));
			MachineConnection sourceConnection = ConnectionStore.Connections.Values
				.First(connection => Comparer<Machine>.IDObjectsEqual(connection.Machine, MacroDistributionAction.SourceMachineID));
			Group sourceGroup = sourceConnection.Configuration.Groups
				.First(group => Comparer<Group>.IDObjectsEqual(group, MacroDistributionAction.GroupID));
			foreach (Machine machine in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Where(connection => !Comparer.MachinesEqual(connection.Machine, sourceConnection.Machine))
				.Where(connection => connection.Configuration.Groups.Any(group => Comparer.GroupsEqual(group, sourceGroup)))
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer())
				.OrderBy(machine => machine.HostName))
			{
				int index = comboBoxDestinationMachine.Items.Add(new ComboBoxItem<Machine>(machine));
				if (Comparer<Machine>.IDObjectsEqual(machine, MacroDistributionAction.DestinationMachineID))
					comboBoxDestinationMachine.SelectedIndex = index;
			}
		}

		#endregion
	}
}

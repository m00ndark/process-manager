using System;
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
	public partial class MacroProcessActionItem : UserControl, IMacroActionItem
	{
		public MacroProcessActionItem()
		{
			InitializeComponent();
			MacroProcessAction = null;
		}

		public MacroProcessActionItem(MacroProcessAction macroProcessAction) : this()
		{
			MacroProcessAction = macroProcessAction;
		}

		#region Properties

		public MacroProcessAction MacroProcessAction { get; private set; }

		#endregion

		#region GUI event handlers

		private void MacroDistributionActionItem_Load(object sender, EventArgs e)
		{
			if (MacroProcessAction == null)
				throw new InvalidOperationException();

			FillMachinesComboBox();
		}

		private void ComboBoxMachine_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxMachine.SelectedIndex > -1)
			{
				Machine sourceMachine = ((ComboBoxItem<Machine>) comboBoxMachine.Items[comboBoxMachine.SelectedIndex]).Tag;
				MacroProcessAction.MachineID = (sourceMachine != null ? sourceMachine.ID : Guid.Empty);
			}
			else
				MacroProcessAction.MachineID = Guid.Empty;

			if (MacroProcessAction.MachineID == Guid.Empty)
				ClearGroupsComboBox();
			else
				FillGroupsComboBox();
		}

		private void ComboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxGroup.SelectedIndex > -1)
			{
				Group group = ((ComboBoxItem<Group>) comboBoxGroup.Items[comboBoxGroup.SelectedIndex]).Tag;
				MacroProcessAction.GroupID = (group != null ? group.ID : Guid.Empty);
			}
			else
				MacroProcessAction.GroupID = Guid.Empty;

			if (MacroProcessAction.GroupID == Guid.Empty)
				ClearApplicationsComboBox();
			else
				FillApplicationsComboBox();
		}

		private void ComboBoxApplication_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxApplication.SelectedIndex > -1)
			{
				Application application = ((ComboBoxItem<Application>) comboBoxApplication.Items[comboBoxApplication.SelectedIndex]).Tag;
				MacroProcessAction.ApplicationID = (application != null ? application.ID : Guid.Empty);
			}
			else
				MacroProcessAction.ApplicationID = Guid.Empty;
		}

		#endregion

		public void SetWidth(int width)
		{
			Width = width;
		}

		#region Helpers

		private void ClearMachinesComboBox()
		{
			comboBoxMachine.Items.Clear();
			ClearGroupsComboBox();
		}

		private void FillMachinesComboBox()
		{
			ClearMachinesComboBox();
			comboBoxMachine.Items.Add(new ComboBoxItem<Machine>(string.Empty));
			foreach (Machine machine in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer())
				.OrderBy(machine => machine.HostName))
			{
				int index = comboBoxMachine.Items.Add(new ComboBoxItem<Machine>(machine));
				if (Comparer<Machine>.IDObjectsEqual(machine, MacroProcessAction.MachineID))
					comboBoxMachine.SelectedIndex = index;
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
			Configuration configuration = ConnectionStore.Connections.Values
				.First(connection => Comparer<Machine>.IDObjectsEqual(connection.Machine, MacroProcessAction.MachineID))
				.Configuration;
			foreach (Group group in configuration
				.Groups
				.Distinct(new GroupEqualityComparer())
				.OrderBy(group => group.Name))
			{
				int index = comboBoxGroup.Items.Add(new ComboBoxItem<Group>(group));
				if (Comparer<Group>.IDObjectsEqual(group, MacroProcessAction.GroupID))
					comboBoxGroup.SelectedIndex = index;
			}
		}

		private void ClearApplicationsComboBox()
		{
			comboBoxApplication.Items.Clear();
		}

		private void FillApplicationsComboBox()
		{
			ClearApplicationsComboBox();
			comboBoxApplication.Items.Add(new ComboBoxItem<Application>(string.Empty));
			Configuration configuration = ConnectionStore.Connections.Values
				.First(connection => Comparer<Machine>.IDObjectsEqual(connection.Machine, MacroProcessAction.MachineID))
				.Configuration;
			foreach (Application application in configuration
				.Applications
				.Where(application => configuration.Groups.First(group => Comparer<Group>.IDObjectsEqual(group, MacroProcessAction.GroupID)).Applications.Contains(application.ID))
				.Distinct(new ApplicationEqualityComparer())
				.OrderBy(application => application.Name))
			{
				int index = comboBoxApplication.Items.Add(new ComboBoxItem<Application>(application));
				if (Comparer<Application>.IDObjectsEqual(application, MacroProcessAction.ApplicationID))
					comboBoxApplication.SelectedIndex = index;
			}
		}

		#endregion
	}
}

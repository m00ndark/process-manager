using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManagerUI.Utilities;

namespace ProcessManagerUI.Forms
{
	public partial class MachinesForm : Form
	{
		private readonly Machine _initiallySelectedMachine;
		private Machine _selectedMachine;
		private bool _hasUnsavedChanges;

		public MachinesForm(Machine initiallySelectedMachine)
		{
			InitializeComponent();
			_initiallySelectedMachine = initiallySelectedMachine;
			_selectedMachine = null;
			_hasUnsavedChanges = false;
			MachinesChanged = _hasUnsavedChanges;
		}

		#region Properties

		public bool MachinesChanged { get; private set; }

		#endregion

		#region GUI event handlers

		private void MachinesForm_Load(object sender, EventArgs e)
		{
			foreach (Machine machine in Settings.Client.Machines)
			{
				ListViewItem item = listViewMachines.Items.Add(new ListViewItem(machine.HostName) { Tag = machine });
				if (_initiallySelectedMachine != null && _initiallySelectedMachine == machine)
					item.Selected = true;
			}
		}

		private void ListViewMachines_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateSelectedMachine();
			if (listViewMachines.SelectedItems.Count == 0)
			{
				panelMachine.Visible = false;
			}
			else
			{
				_selectedMachine = ((Machine) listViewMachines.SelectedItems[0].Tag);
				textBoxMachineHostName.Text = _selectedMachine.HostName;
				panelMachine.Visible = true;
			}
		}

		private void ButtonAddMachine_Click(object sender, EventArgs e)
		{
			UpdateSelectedMachine();
			MachinesChanged = _hasUnsavedChanges = true;
			_selectedMachine = new Machine();
			Settings.Client.Machines.Add(_selectedMachine);
			textBoxMachineHostName.Text = _selectedMachine.HostName;
			ListViewItem item = listViewMachines.Items.Add(new ListViewItem(_selectedMachine.HostName) { Tag = _selectedMachine });
			item.Selected = true;
			panelMachine.Visible = true;
			EnableControls();
			textBoxMachineHostName.Focus();
		}

		private void ButtonRemoveMachine_Click(object sender, EventArgs e)
		{
			if (listViewMachines.SelectedItems.Count > 0)
			{
				MachinesChanged = _hasUnsavedChanges = true;
				_selectedMachine = ((Machine) listViewMachines.SelectedItems[0].Tag);
				Settings.Client.Machines.Remove(_selectedMachine);
				listViewMachines.Items.Remove(listViewMachines.SelectedItems[0]);
				_selectedMachine = null;
				EnableControls();
			}
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			SaveMachines();
			Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			if (MachinesChanged)
				if (Messenger.ShowWarningQuestion("Machines has been changed", "Would you like to discard any changes?") == DialogResult.No)
					return;
			Close();
		}

		private void ButtonApply_Click(object sender, EventArgs e)
		{
			SaveMachines();
		}

		#endregion

		#region Helpers

		private void UpdateSelectedMachine()
		{
			if (_selectedMachine != null)
			{
				bool machineChanged = (_selectedMachine.HostName != textBoxMachineHostName.Text);
				_hasUnsavedChanges |= machineChanged;
				MachinesChanged |= machineChanged;
				_selectedMachine.HostName = textBoxMachineHostName.Text;
				if (machineChanged)
				{
					ListViewItem item = listViewMachines.Items.Cast<ListViewItem>().First(x => x.Tag == _selectedMachine);
					item.Text = _selectedMachine.HostName;
					listViewMachines.Sort();
				}
				EnableControls();
			}
		}

		private void SaveMachines()
		{
			UpdateSelectedMachine();
			if (_hasUnsavedChanges)
			{
				// todo: make machines distinct before saving and possible update GUI
				//List<Machine> duplicateMachines = Settings.Client.Machines.Where(x => Settings.Client.Machines.Count(y => y.Equals(x)) > 1).ToList();
				//List<Machine> distincts = duplicateMachines.Distinct(EqualityComparer<Machine>.Default).ToList();
				//List<Machine> left = duplicateMachines.ToList().Except(distincts, EqualityComparer<object>.Default).Cast<Machine>().ToList();
				//left.ForEach(x => Settings.Client.Machines.Remove(x));
				Settings.Client.Save(ClientSettingsType.Machines);
				_hasUnsavedChanges = false;
				EnableControls();
			}
		}

		private void EnableControls(bool enable = true)
		{
			buttonApply.Enabled = (enable && _hasUnsavedChanges);
		}

		#endregion
	}
}

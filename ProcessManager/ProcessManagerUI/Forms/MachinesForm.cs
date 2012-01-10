﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.Service.Client;
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

		private void TextBoxMachineHostName_TextChanged(object sender, EventArgs e)
		{
			MachineChanged();
			EnableControls();
		}

		private void ButtonValidateMachine_Click(object sender, EventArgs e)
		{
			try
			{
				Machine machine = new Machine(textBoxMachineHostName.Text);
				using (ProcessManagerServiceHandler serviceHandler = new ProcessManagerServiceHandler(machine))
				{
					serviceHandler.Service.Ping();
				}
				Messenger.ShowInformation("Machine connection validated", "Connection to a Process Manager service on the specified machine was successfully established.");
			}
			catch (Exception ex)
			{
				Messenger.ShowError("Machine connection validation failed", "Connection to a Process Manager service on the specified machine could not be established.", ex.Message);
				ProcessManager.Utilities.Logger.Add(ex.Message, ex.InnerException);
			}
		}

		private void ButtonCopyMachineSetup_Click(object sender, EventArgs e)
		{

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
				if (MachineChanged())
				{
					_selectedMachine.HostName = textBoxMachineHostName.Text;
					ListViewItem item = listViewMachines.Items.Cast<ListViewItem>().First(x => x.Tag == _selectedMachine);
					item.Text = _selectedMachine.HostName;
					listViewMachines.Sort();
				}
				textBoxMachineHostName.Text = _selectedMachine.HostName;
				EnableControls();
			}
		}

		private bool MachineChanged()
		{
			bool machineChanged = false;
			if (_selectedMachine != null && !string.IsNullOrEmpty(textBoxMachineHostName.Text))
			{
				machineChanged = (_selectedMachine.HostName != textBoxMachineHostName.Text);
				_hasUnsavedChanges |= machineChanged;
				MachinesChanged |= machineChanged;
			}
			return machineChanged;
		}

		private void SaveMachines()
		{
			UpdateSelectedMachine();
			if (_hasUnsavedChanges)
			{
				var duplicateMachines = Settings.Client.Machines
					.Distinct()
					.Select(machine => new { Machine = machine, Count = Settings.Client.Machines.Count(x => x.Equals(machine)) })
					.Where(x => x.Count > 1);
				foreach (var x in duplicateMachines.ToList())
				{
					for (int i = 0; i < x.Count - 1; i++)
					{
						ListViewItem item = listViewMachines.Items.Cast<ListViewItem>().First(y => x.Machine.Equals(y.Tag));
						Settings.Client.Machines.Remove(x.Machine);
						listViewMachines.Items.Remove(item);
					}
				}
				Machine defaultMachine = Settings.Client.Machines.FirstOrDefault(machine => machine.HostName == Machine.DEFAULT_HOST_NAME);
				if (defaultMachine != null)
				{
					ListViewItem item = listViewMachines.Items.Cast<ListViewItem>().First(y => defaultMachine.Equals(y.Tag));
					Settings.Client.Machines.Remove(defaultMachine);
					listViewMachines.Items.Remove(item);
				}
				Settings.Client.Save(ClientSettingsType.Machines);
				_hasUnsavedChanges = false;
				EnableControls();
			}
		}

		private void EnableControls(bool enable = true)
		{
			buttonApply.Enabled = (enable && _hasUnsavedChanges);
			buttonValidateMachine.Enabled = (enable && !string.IsNullOrEmpty(textBoxMachineHostName.Text));
			buttonCopyMachineSetup.Enabled = (enable && !_hasUnsavedChanges && !string.IsNullOrEmpty(textBoxMachineHostName.Text));
		}

		#endregion
	}
}

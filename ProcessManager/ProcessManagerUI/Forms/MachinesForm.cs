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

namespace ProcessManagerUI.Forms
{
	public partial class MachinesForm : Form
	{
		#region MachineWrapper class

		public class MachineWrapper
		{
			public MachineWrapper(Machine machine)
			{
				Machine = machine;
			}

			public Machine Machine { get; private set; }

			public override string ToString()
			{
				return "From " + Machine;
			}
		}

		#endregion

		private readonly Machine _initiallySelectedMachine;
		private Machine _selectedMachine;
		private bool _hasUnsavedChanges;

		public event EventHandler<MachinesEventArgs> MachinesChanged;

		public MachinesForm(Machine initiallySelectedMachine)
		{
			InitializeComponent();
			_initiallySelectedMachine = initiallySelectedMachine;
			_selectedMachine = null;
			_hasUnsavedChanges = false;
			AnyMachinesChanged = _hasUnsavedChanges;
		}

		#region Properties

		public bool AnyMachinesChanged { get; private set; }

		#endregion

		#region GUI event handlers

		private void MachinesForm_Load(object sender, EventArgs e)
		{
			foreach (Machine machine in Settings.Client.Machines)
			{
				ListViewItem item = listViewMachines.Items.Add(new ListViewItem(machine.HostName) { Tag = machine });
				if (_initiallySelectedMachine != null && _initiallySelectedMachine.Equals(machine))
					item.Selected = true;
			}
		}

		private void ListViewMachines_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewMachines.SelectedItems.Count == 0)
			{
				panelMachine.Visible = false;
			}
			else
			{
				_selectedMachine = ((Machine) listViewMachines.SelectedItems[0].Tag);
				textBoxMachineHostName.Text = _selectedMachine.HostName;
				panelMachine.Visible = true;
				EnableControls();
			}
		}

		private void ButtonAddMachine_Click(object sender, EventArgs e)
		{
			UpdateSelectedMachine();
			AnyMachinesChanged = _hasUnsavedChanges = true;
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
				AnyMachinesChanged = _hasUnsavedChanges = true;
				_selectedMachine = (Machine) listViewMachines.SelectedItems[0].Tag;
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

		private void TextBoxMachineHostName_Leave(object sender, EventArgs e)
		{
			UpdateSelectedMachine();
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
				Messenger.ShowError("Machine connection validation failed", "Connection to a Process Manager service on the specified machine could not be established.", ex);
			}
		}

		private void ButtonCopyMachineSetup_Click(object sender, EventArgs e)
		{
			if (_selectedMachine != null)
				Picker.ShowMenu(buttonCopyMachineSetup, new[] { ConfigurationParts.All, ConfigurationParts.Groups, ConfigurationParts.Applications },
					Settings.Client.Machines.Where(machine => !machine.Equals(_selectedMachine)).Select(machine => new MachineWrapper(machine)), ContextMenu_CopyMachineSetup_MachineClicked);
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			if (SaveMachines())
				Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			if (_hasUnsavedChanges)
			{
				if (Messenger.ShowWarningQuestion("Machines have been changed", "Would you like to discard any changes?") == DialogResult.No)
				{
					DialogResult = DialogResult.None;
					return;
				}
				Settings.Client.Load(ClientSettingsType.Machines);
			}
			Close();
		}

		private void ButtonApply_Click(object sender, EventArgs e)
		{
			SaveMachines();
		}

		#endregion

		#region Picker event handlers

		private void ContextMenu_CopyMachineSetup_MachineClicked(ConfigurationParts configurationPart, MachineWrapper machineWrapper)
		{
			if (_selectedMachine != null && machineWrapper != null)
				CopyConfiguration(configurationPart, machineWrapper.Machine, _selectedMachine);
		}

		#endregion

		#region Event raisers

		private void RaiseMachinesChangedEvent(Machine machine)
		{
			RaiseMachinesChangedEvent(new List<Machine>() { machine });
		}

		private void RaiseMachinesChangedEvent(List<Machine> machines = null)
		{
			if (MachinesChanged != null)
				MachinesChanged(this, new MachinesEventArgs(machines));
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
				AnyMachinesChanged |= machineChanged;
			}
			return machineChanged;
		}

		private bool SaveMachines()
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

				List<Machine> invalidMachines = new List<Machine>(Settings.Client.Machines.Where(machine => !MachineIsValid(machine)));
				if (invalidMachines.Count > 0)
				{
					Messenger.ShowError("Machine" + (invalidMachines.Count == 1 ? string.Empty : "s") + " invalid", "Host name invalid for "
						+ invalidMachines.Aggregate(string.Empty, (x, y) => x + ", " + y).Trim(", ".ToCharArray()));
					return false;
				}

				Settings.Client.Save(ClientSettingsType.Machines);
				_hasUnsavedChanges = false;

				RaiseMachinesChangedEvent();
				EnableControls();
			}
			return true;
		}

		private static bool MachineIsValid(Machine machine)
		{
			return ConnectionStore.MachineIsValid(machine);
		}

		private void CopyConfiguration(ConfigurationParts configurationPart, Machine sourceMachine, Machine destinationMachine)
		{
			ServiceHelper.ConnectMachines();
			ServiceHelper.WaitForConfiguration(sourceMachine, destinationMachine);

			if (ConnectionStore.Connections[sourceMachine].ServiceHandler.Status != ProcessManagerServiceHandlerStatus.Connected)
			{
				Messenger.ShowError("Machine disconnected", "Could not establish a connection to Process Manager service at " + sourceMachine);
				return;
			}

			if (ConnectionStore.Connections[destinationMachine].ServiceHandler.Status != ProcessManagerServiceHandlerStatus.Connected)
			{
				Messenger.ShowError("Machine disconnected", "Could not establish a connection to Process Manager service at " + destinationMachine);
				return;
			}

			if (configurationPart == ConfigurationParts.All)
			{
				if (Messenger.ShowWarningQuestion("Confirm setup copy", "Are you sure you want to copy the setup from "
					+ sourceMachine + " to " + destinationMachine + ", overwriting the existing setup?") == DialogResult.No)
				{
					return;
				}
			}
			else
			{
				if (Messenger.ShowWarningQuestion("Confirm setup copy", "Are you sure you want to copy the " + configurationPart.ToString().ToLower() + " setup from "
					+ sourceMachine + " to " + destinationMachine + ", overwriting the existing " + configurationPart.ToString().ToLower() + "?") == DialogResult.No)
				{
					return;
				}
			}

			Configuration configurationBackup = ConnectionStore.Connections[destinationMachine].Configuration;

			ConnectionStore.Connections[destinationMachine].Configuration = ConnectionStore.Connections[sourceMachine].Configuration.CopyTo(configurationBackup.Clone(), configurationPart);
			ConnectionStore.Connections[destinationMachine].ConfigurationModified = true;

			if (!ServiceHelper.SaveConfiguration())
				ConnectionStore.Connections[destinationMachine].Configuration = configurationBackup;
			else
				RaiseMachinesChangedEvent(destinationMachine);
		}

		private void EnableControls(bool enable = true)
		{
			buttonApply.Enabled = (enable && _hasUnsavedChanges);
			buttonRemoveMachine.Enabled = (enable && listViewMachines.SelectedItems.Count > 0 && !listViewMachines.SelectedItems[0].Tag.Equals(Settings.Constants.LocalMachine));
			textBoxMachineHostName.ReadOnly = (listViewMachines.SelectedItems.Count > 0 && listViewMachines.SelectedItems[0].Tag.Equals(Settings.Constants.LocalMachine));
			buttonValidateMachine.Enabled = (enable && !string.IsNullOrEmpty(textBoxMachineHostName.Text));
			buttonCopyMachineSetup.Enabled = (enable && !_hasUnsavedChanges && !string.IsNullOrEmpty(textBoxMachineHostName.Text));
		}

		#endregion
	}
}

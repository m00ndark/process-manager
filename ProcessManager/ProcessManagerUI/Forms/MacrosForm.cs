﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.DataObjects.Comparers;
using ProcessManagerUI.Controls.MacroActionItems;
using ProcessManagerUI.Utilities;

namespace ProcessManagerUI.Forms
{
	public partial class MacrosForm : Form
	{
		private const int SCROLLBAR_WIDTH = 20;
		private Macro _selectedMacro;
		private readonly List<MacroActionItem> _macroActionItems;
		private bool _disableTextChangedEvents;
		private bool _hasUnsavedChanges;

		public event EventHandler MacrosChanged;

		public MacrosForm()
		{
			InitializeComponent();
			_selectedMacro = null;
			_macroActionItems = new List<MacroActionItem>();
			_disableTextChangedEvents = false;
			_hasUnsavedChanges = false;
			AnyMachinesChanged = _hasUnsavedChanges;
		}

		#region Properties

		public bool AnyMachinesChanged { get; private set; }

		#endregion

		#region GUI event handlers

		private void MachinesForm_Load(object sender, EventArgs e)
		{
			Settings.Client.Macros.ForEach(macro => listViewMacros.Items.Add(new ListViewItem(macro.Name) { Tag = macro }));
		}

		private void ListViewMacros_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewMacros.SelectedItems.Count == 0)
			{
				panelMacro.Visible = false;
			}
			else
			{
				_disableTextChangedEvents = true;
				_selectedMacro = ((Macro) listViewMacros.SelectedItems[0].Tag);
				textBoxMacroName.Text = _selectedMacro.Name;
				LayoutMacroActionItems();
				_disableTextChangedEvents = false;
				panelMacro.Visible = true;
				EnableControls();
			}
		}

		private void ButtonAddMacro_Click(object sender, EventArgs e)
		{
			UpdateSelectedMacro();
			AnyMachinesChanged = _hasUnsavedChanges = true;
			string macroName = ConfigurationForm.GetFirstAvailableDefaultName(
				Settings.Client.Macros.Select(macro => macro.Name).ToList(), "Macro");
			_selectedMacro = new Macro(macroName);
			Settings.Client.Macros.Add(_selectedMacro);
			textBoxMacroName.Text = _selectedMacro.Name;
			ListViewItem item = listViewMacros.Items.Add(new ListViewItem(_selectedMacro.Name) { Tag = _selectedMacro });
			item.Selected = true;
			panelMacro.Visible = true;
			EnableControls();
			textBoxMacroName.Focus();
		}

		private void ButtonRemoveMacro_Click(object sender, EventArgs e)
		{
			if (listViewMacros.SelectedItems.Count > 0)
			{
				AnyMachinesChanged = _hasUnsavedChanges = true;
				_selectedMacro = (Macro) listViewMacros.SelectedItems[0].Tag;
				Settings.Client.Macros.Remove(_selectedMacro);
				listViewMacros.Items.Remove(listViewMacros.SelectedItems[0]);
				_selectedMacro = null;
				EnableControls();
			}
		}

		private void TextBoxMacroName_TextChanged(object sender, EventArgs e)
		{
			if (!_disableTextChangedEvents)
			{
				MacroChanged();
				EnableControls();
			}
		}

		private void TextBoxMacroName_Leave(object sender, EventArgs e)
		{
			UpdateSelectedMacro();
		}

		private void ButtonAddMacroAction_Click(object sender, EventArgs e)
		{
			AddMacroActionItem();
			UpdateSelectedMacro();
		}

		private void FlowLayoutPanelMacroActions_Resize(object sender, EventArgs e)
		{
			foreach (MacroActionItem macroActionItem in _macroActionItems)
				macroActionItem.SetWidth(flowLayoutPanelMacroActions.Width - SCROLLBAR_WIDTH);
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			if (SaveMacros())
				Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			if (_hasUnsavedChanges)
			{
				if (Messenger.ShowWarningQuestion("Macros have been changed", "Would you like to discard any changes?") == DialogResult.No)
				{
					DialogResult = DialogResult.None;
					return;
				}
				Settings.Client.Load(ClientSettingsType.Macros);
			}
			Close();
		}

		private void ButtonApply_Click(object sender, EventArgs e)
		{
			SaveMacros();
		}

		#endregion

		#region Picker event handlers

		//private void ContextMenu_CopyMachineSetup_MachineClicked(ConfigurationParts configurationPart, MachineWrapper machineWrapper)
		//{
		//	if (_selectedMacro != null && machineWrapper != null)
		//		CopyConfiguration(configurationPart, machineWrapper.Machine, _selectedMacro);
		//}

		#endregion

		#region Macro item event handlers

		private void MacroActionItem_MacroActionItemRemoved(object sender, EventArgs e)
		{
			RemoveMacroActionItem((MacroActionItem) sender);
			UpdateSelectedMacro();
		}

		#endregion

		#region Event raisers

		private void RaiseMacrosChangedEvent()
		{
			if (MacrosChanged != null)
				MacrosChanged(this, new EventArgs());
		}

		#endregion

		#region Helpers

		private void UpdateSelectedMacro()
		{
			if (_selectedMacro != null)
			{
				if (MacroChanged())
				{
					_selectedMacro.Name = textBoxMacroName.Text;
					_selectedMacro.Actions.Clear();
					_macroActionItems.ForEach(macroActionItem => _selectedMacro.Actions.Add(macroActionItem.Action));
					ListViewItem item = listViewMacros.Items.Cast<ListViewItem>().First(x => x.Tag == _selectedMacro);
					item.Text = _selectedMacro.Name;
					listViewMacros.Sort();
				}
				textBoxMacroName.Text = _selectedMacro.Name;
				EnableControls();
			}
		}

		private void ClearMacroActionItems()
		{
			_macroActionItems.Clear();
			flowLayoutPanelMacroActions.Controls.Clear();
			_macroActionItems.ForEach(item =>
				{
					item.MacroActionItemRemoved -= MacroActionItem_MacroActionItemRemoved;
					item.Dispose();
				});
		}

		private MacroActionItem CreateMacroActionItem(IMacroAction action = null)
		{
			MacroActionItem macroActionItem = new MacroActionItem(action);
			macroActionItem.MacroActionItemRemoved += MacroActionItem_MacroActionItemRemoved;
			macroActionItem.SetWidth(flowLayoutPanelMacroActions.Width - SCROLLBAR_WIDTH);
			_macroActionItems.Add(macroActionItem);
			return macroActionItem;
		}

		private void AddMacroActionItem(IMacroAction action = null)
		{
			flowLayoutPanelMacroActions.Controls.Add(CreateMacroActionItem(action));
		}

		private void RemoveMacroActionItem(MacroActionItem macroActionItem)
		{
			_macroActionItems.Remove(macroActionItem);
			flowLayoutPanelMacroActions.Controls.Remove(macroActionItem);
			macroActionItem.MacroActionItemRemoved -= MacroActionItem_MacroActionItemRemoved;
			macroActionItem.Dispose();
		}

		private void LayoutMacroActionItems()
		{
			ClearMacroActionItems();
			if (_selectedMacro == null) return;
			_selectedMacro.Actions.ForEach(action => CreateMacroActionItem(action));
			flowLayoutPanelMacroActions.Controls.AddRange(_macroActionItems.Cast<Control>().ToArray());
		}

		private bool MacroChanged()
		{
			bool macroChanged = false;
			if (_selectedMacro != null && !string.IsNullOrEmpty(textBoxMacroName.Text))
			{
				int equalActionsCount = _selectedMacro.Actions.Intersect(_macroActionItems.Select(item => item.Action)).Count();
				macroChanged = (_selectedMacro.Name != textBoxMacroName.Text
					|| equalActionsCount != _selectedMacro.Actions.Count || equalActionsCount != _macroActionItems.Count);
				_hasUnsavedChanges |= macroChanged;
				AnyMachinesChanged |= macroChanged;
			}
			return macroChanged;
		}

		private static bool ValidateMacros()
		{
			IDictionary<Macro, string> nonUniqueMacros = Settings.Client.Macros
				.GroupBy(macro => macro, new MacroEqualityComparer())
				.Where(x => x.Count() > 1)
				.Select(x => new
					{
						Macro = x.Key,
						Message = x.Count() + " macros"
					})
				.ToDictionary(x => x.Macro, x => x.Message);
			if (nonUniqueMacros.Count > 0)
			{
				Messenger.ShowError("Macro names not unique",
					"Two or more macros have the same name. See details for more information.",
					nonUniqueMacros.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + y.Key + ": " + y.Value).Trim());
				return false;
			}

			IDictionary<Macro, List<string>> invalidMacros = Settings.Client.Macros
				.Select(macro => new
					{
						Macro = macro,
						Messages = MacroIsValid(macro).ToList()
					})
				.Where(x => x.Messages.Count > 0)
				.ToDictionary(x => x.Macro, x => x.Messages);
			if (invalidMacros.Count > 0)
			{
				int invalidMacroCount = invalidMacros.SelectMany(x => x.Value).Count();
				Messenger.ShowError("Macro" + (invalidMacroCount == 1 ? string.Empty : "s") + " invalid",
					"One or more macro property invalid. See details for more information.",
					invalidMacros.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + Environment.NewLine + y.Value.Select(z => new { Macro = y.Key, Message = z })
						.Aggregate(string.Empty, (a, b) => a + Environment.NewLine + b.Macro + ": " + b.Message).Trim()).Trim());
				return false;
			}
			return true;
		}

		private static IEnumerable<string> MacroIsValid(Macro macro)
		{
			if (string.IsNullOrEmpty(macro.Name))
				yield return "Name missing";
		}

		private bool SaveMacros()
		{
			_macroActionItems
				.Where(macroActionItem => macroActionItem.Action == null || !macroActionItem.Action.IsValid)
				.ToList()
				.ForEach(RemoveMacroActionItem);

			UpdateSelectedMacro();

			if (_hasUnsavedChanges)
			{
				// validate
				if (!ValidateMacros())
					return false;

				// save
				Settings.Client.Save(ClientSettingsType.Macros);
				_hasUnsavedChanges = false;

				RaiseMacrosChangedEvent();
				EnableControls();
			}
			return true;
		}

		private void EnableControls(bool enable = true)
		{
			Machine localhost = new Machine(Settings.Constants.LOCALHOST);
			buttonApply.Enabled = (enable && _hasUnsavedChanges);
			buttonRemoveMacro.Enabled = (enable && listViewMacros.SelectedItems.Count > 0 && !listViewMacros.SelectedItems[0].Tag.Equals(localhost));
			textBoxMacroName.ReadOnly = (listViewMacros.SelectedItems.Count > 0 && listViewMacros.SelectedItems[0].Tag.Equals(localhost));
		}

		#endregion
	}
}

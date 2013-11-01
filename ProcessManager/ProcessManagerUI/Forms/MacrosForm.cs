using System;
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
			EnableControls();
		}

		private void ListViewMacros_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewMacros.SelectedItems.Count != 1)
			{
				labelNoMacroSelected.Text = listViewMacros.SelectedItems.Count == 0 ? "No macro selected" : "Multiple macros selected";
				panelMacro.Visible = false;
			}
			else
			{
				_disableTextChangedEvents = true;
				_selectedMacro = ((Macro) listViewMacros.SelectedItems[0].Tag);
				textBoxMacroName.Text = _selectedMacro.Name;
				LayoutMacroActionItems();
				_disableTextChangedEvents = false;
				SuspendLayout();
				panelMacro.Visible = true;
				ResumeLayout(true);
			}
			EnableControls();
		}

		private void ButtonAddMacro_Click(object sender, EventArgs e)
		{
			AddMacro(false);
		}

		private void ButtonCopyMacro_Click(object sender, EventArgs e)
		{
			if (listViewMacros.SelectedItems.Count == 1)
				AddMacro(true);
		}

		private void ButtonRemoveMacro_Click(object sender, EventArgs e)
		{
			if (listViewMacros.SelectedItems.Count > 0)
			{
				AnyMachinesChanged = _hasUnsavedChanges = true;
				foreach (ListViewItem item in listViewMacros.SelectedItems)
				{
					Settings.Client.Macros.Remove((Macro) item.Tag);
					listViewMacros.Items.Remove(item);
				}
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

		private void MacroActionItem_MacroActionItemChanged(object sender, EventArgs e)
		{
			UpdateSelectedMacro();
		}

		private void MacroActionItem_MacroActionItemRemoved(object sender, EventArgs e)
		{
			RemoveMacroActionItem((MacroActionItem) sender);
			UpdateSelectedMacro();
		}

		private void MacroActionItem_MacroActionItemMovedUp(object sender, EventArgs e)
		{
			MoveMacroActionItem((MacroActionItem) sender, true);
			UpdateSelectedMacro();
		}

		private void MacroActionItem_MacroActionItemMovedDown(object sender, EventArgs e)
		{
			MoveMacroActionItem((MacroActionItem) sender, false);
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

		private void AddMacro(bool makeCopy)
		{
			UpdateSelectedMacro();
			AnyMachinesChanged = _hasUnsavedChanges = true;
			string macroName = ConfigurationForm.GetFirstAvailableDefaultName(
				Settings.Client.Macros.Select(macro => macro.Name).ToList(), makeCopy ? _selectedMacro.Name : "Macro");
			_selectedMacro = makeCopy ? _selectedMacro.Copy(macroName) : new Macro(macroName);
			Settings.Client.Macros.Add(_selectedMacro);
			textBoxMacroName.Text = _selectedMacro.Name;
			ListViewItem item = listViewMacros.Items.Add(new ListViewItem(_selectedMacro.Name) { Tag = _selectedMacro });
			listViewMacros.SelectedItems.Clear();
			item.Selected = true;
			panelMacro.Visible = true;
			EnableControls();
			textBoxMacroName.Focus();
		}

		private void UpdateSelectedMacro()
		{
			if (_selectedMacro != null)
			{
				if (MacroChanged())
				{
					_selectedMacro.Name = textBoxMacroName.Text;
					_selectedMacro.ActionBundles.Clear();
					_macroActionItems.ForEach(macroActionItem => _selectedMacro.ActionBundles.Add(
						macroActionItem.ActionBundle != null ? macroActionItem.ActionBundle.Clone() : null));
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
					item.MacroActionItemChanged -= MacroActionItem_MacroActionItemChanged;
					item.MacroActionItemRemoved -= MacroActionItem_MacroActionItemRemoved;
					item.MacroActionItemMovedUp -= MacroActionItem_MacroActionItemMovedUp;
					item.MacroActionItemMovedDown -= MacroActionItem_MacroActionItemMovedDown;
					item.Dispose();
				});
		}

		private MacroActionItem CreateMacroActionItem(MacroActionBundle actionBundle = null)
		{
			MacroActionItem macroActionItem = new MacroActionItem(actionBundle != null ? actionBundle.Clone() : null);
			macroActionItem.MacroActionItemChanged += MacroActionItem_MacroActionItemChanged;
			macroActionItem.MacroActionItemRemoved += MacroActionItem_MacroActionItemRemoved;
			macroActionItem.MacroActionItemMovedUp += MacroActionItem_MacroActionItemMovedUp;
			macroActionItem.MacroActionItemMovedDown += MacroActionItem_MacroActionItemMovedDown;
			macroActionItem.SetWidth(flowLayoutPanelMacroActions.Width - SCROLLBAR_WIDTH);
			_macroActionItems.Add(macroActionItem);
			return macroActionItem;
		}

		private void AddMacroActionItem(MacroActionBundle actionBundle = null)
		{
			flowLayoutPanelMacroActions.Controls.Add(CreateMacroActionItem(actionBundle));
		}

		private void RemoveMacroActionItem(MacroActionItem macroActionItem)
		{
			_macroActionItems.Remove(macroActionItem);
			flowLayoutPanelMacroActions.Controls.Remove(macroActionItem);
			macroActionItem.MacroActionItemChanged -= MacroActionItem_MacroActionItemChanged;
			macroActionItem.MacroActionItemRemoved -= MacroActionItem_MacroActionItemRemoved;
			macroActionItem.MacroActionItemMovedUp -= MacroActionItem_MacroActionItemMovedUp;
			macroActionItem.MacroActionItemMovedDown -= MacroActionItem_MacroActionItemMovedDown;
			macroActionItem.Dispose();
		}

		private void MoveMacroActionItem(MacroActionItem macroActionItem, bool up)
		{
			int index = flowLayoutPanelMacroActions.Controls.IndexOf(macroActionItem);
			if (up && index > 0)
				flowLayoutPanelMacroActions.Controls.SetChildIndex(macroActionItem, index - 1);
			else if (!up && index < flowLayoutPanelMacroActions.Controls.Count - 1)
				flowLayoutPanelMacroActions.Controls.SetChildIndex(macroActionItem, index + 1);

			index = _macroActionItems.IndexOf(macroActionItem);
			if (up && index > 0)
			{
				_macroActionItems.RemoveAt(index);
				_macroActionItems.Insert(index - 1, macroActionItem);
			}
			else if (!up && index < _macroActionItems.Count - 1)
			{
				_macroActionItems.RemoveAt(index);
				_macroActionItems.Insert(index + 1, macroActionItem);
			}
		}

		private void LayoutMacroActionItems()
		{
			ClearMacroActionItems();
			if (_selectedMacro == null) return;
			_selectedMacro.ActionBundles.ForEach(actionBundle => CreateMacroActionItem(actionBundle));
			flowLayoutPanelMacroActions.Controls.AddRange(_macroActionItems.Cast<Control>().ToArray());
		}

		private bool MacroChanged()
		{
			bool macroChanged = false;
			if (_selectedMacro != null && !string.IsNullOrEmpty(textBoxMacroName.Text))
			{
				macroChanged = (_selectedMacro.Name != textBoxMacroName.Text
					|| !_selectedMacro.ActionBundles.SequenceEqual(_macroActionItems.Select(item => item.ActionBundle)));
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
				.Where(macroActionItem => macroActionItem.ActionBundle == null || !macroActionItem.ActionBundle.IsValid)
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
			buttonApply.Enabled = (enable && _hasUnsavedChanges);
			buttonCopyMacro.Enabled = (enable && listViewMacros.SelectedItems.Count == 1);
			buttonRemoveMacro.Enabled = (enable && listViewMacros.SelectedItems.Count > 0);
		}

		#endregion
	}
}

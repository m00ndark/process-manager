using System;
using System.Windows.Forms;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.MacroActionItems.Support;
using ProcessManagerUI.Support;

namespace ProcessManagerUI.Controls.MacroActionItems
{
	public partial class MacroActionItem : UserControl
	{
		private IMacroActionItem _macroActionItem;
		private IMacroAction _action;

		public MacroActionItem()
		{
			InitializeComponent();
			_macroActionItem = null;
			Action = null;
		}

		public MacroActionItem(IMacroAction action) : this()
		{
			Action = action;
		}

		public event EventHandler MacroActionItemRemoved;

		#region Properties

		public IMacroAction Action
		{
			get { return _action; }
			private set
			{
				_action = value;
				if (_action is MacroProcessAction)
					MacroProcessAction = (MacroProcessAction) _action;
				else if (_action is MacroDistributionAction)
					MacroDistributionAction = (MacroDistributionAction) _action;
			}
		}

		private MacroProcessAction MacroProcessAction { get; set; }
		private MacroDistributionAction MacroDistributionAction { get; set; }

		#endregion

		#region GUI event handlers

		private void MacroActionItem_Load(object sender, EventArgs e)
		{
			comboBoxActionTypes.Items.Add(new ComboBoxItem<ActionType>(string.Empty));
			foreach (ActionType actionType in Enum.GetValues(typeof(ActionType)))
			{
				int index = comboBoxActionTypes.Items.Add(new ComboBoxItem<ActionType>(actionType));
				if (Action != null && Action.Type == actionType)
					comboBoxActionTypes.SelectedIndex = index;
			}
		}

		private void buttonRemove_Click(object sender, EventArgs e)
		{
			RaiseMacroActionItemRemovedEvent();
		}

		private void ComboBoxActionTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxActionTypes.SelectedIndex > -1)
			{
				if (_macroActionItem != null)
				{
					_macroActionItem.Dispose();
					panelAction.Controls.Remove((Control) _macroActionItem);
					_macroActionItem = null;
				}

				ComboBoxItem<ActionType> comboBoxItem = (ComboBoxItem<ActionType>) comboBoxActionTypes.Items[comboBoxActionTypes.SelectedIndex];

				if (comboBoxItem.Text == string.Empty)
				{
					Action = null;
					panelAction.Controls.Clear();
					labelDivider.Visible = false;
					return;
				}

				ActionType actionType = comboBoxItem.Tag;

				switch (actionType)
				{
					case ActionType.Start:
					case ActionType.Stop:
					case ActionType.Restart:
						Action = MacroProcessAction ?? new MacroProcessAction(actionType);
						//_actionItem = new 
						break;
					case ActionType.Distribute:
						Action = MacroDistributionAction ?? new MacroDistributionAction(actionType);
						_macroActionItem = new MacroDistributionActionItem(MacroDistributionAction);
						break;
				}

				if (_macroActionItem != null)
				{
					labelDivider.Visible = true;
					_macroActionItem.SetWidth(panelAction.Width);
					panelAction.Controls.Add((Control) _macroActionItem);
				}
			}
		}

		#endregion

		#region Event raisers

		private void RaiseMacroActionItemRemovedEvent()
		{
			if (MacroActionItemRemoved != null)
				MacroActionItemRemoved(this, EventArgs.Empty);
		}

		#endregion

		public void SetWidth(int width)
		{
			Width = width;
			if (_macroActionItem != null)
				_macroActionItem.SetWidth(panelAction.Width);
		}
	}
}

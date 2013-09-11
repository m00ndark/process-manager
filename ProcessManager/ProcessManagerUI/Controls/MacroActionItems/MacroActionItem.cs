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
			    if (_action == null)
			    {
			        MacroProcessAction = null;
			        MacroDistributionAction = null;
                    MacroWaitAction = null;
			    }
			    else if (_action is MacroProcessAction)
			        MacroProcessAction = (MacroProcessAction) _action;
			    else if (_action is MacroDistributionAction)
			        MacroDistributionAction = (MacroDistributionAction) _action;
			    else if (_action is MacroWaitAction)
			        MacroWaitAction = (MacroWaitAction) _action;
			}
		}

		private MacroProcessAction MacroProcessAction { get; set; }
		private MacroDistributionAction MacroDistributionAction { get; set; }
		private MacroWaitAction MacroWaitAction { get; set; }

		#endregion

		#region GUI event handlers

		private void MacroActionItem_Load(object sender, EventArgs e)
		{
			comboBoxActionTypes.Items.Add(new ComboBoxItem<ActionType>(string.Empty));
            foreach (MacroActionType actionType in Enum.GetValues(typeof(MacroActionType)))
			{
                int index = comboBoxActionTypes.Items.Add(new ComboBoxItem<MacroActionType>(actionType));
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

				ComboBoxItem<MacroActionType> comboBoxItem = (ComboBoxItem<MacroActionType>) comboBoxActionTypes.Items[comboBoxActionTypes.SelectedIndex];

				if (comboBoxItem.Text == string.Empty)
				{
					Action = null;
					panelAction.Controls.Clear();
					labelDivider.Visible = false;
					return;
				}

                MacroActionType actionType = comboBoxItem.Tag;

				switch (actionType)
				{
                    case MacroActionType.Start:
                    case MacroActionType.Stop:
                    case MacroActionType.Restart:
						Action = MacroProcessAction ?? new MacroProcessAction(actionType);
                        _macroActionItem = new MacroProcessActionItem(MacroProcessAction);
						break;
                    case MacroActionType.Distribute:
						Action = MacroDistributionAction ?? new MacroDistributionAction(actionType);
						_macroActionItem = new MacroDistributionActionItem(MacroDistributionAction);
						break;
                    case MacroActionType.Wait:
                        Action = MacroWaitAction ?? new MacroWaitAction(actionType);
                        _macroActionItem = new MacroWaitActionItem(MacroWaitAction);
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

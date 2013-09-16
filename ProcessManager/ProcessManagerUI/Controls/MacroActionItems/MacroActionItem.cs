using System;
using System.Linq;
using System.Windows.Forms;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.MacroActionItems.Support;
using ProcessManagerUI.Utilities;

namespace ProcessManagerUI.Controls.MacroActionItems
{
	public partial class MacroActionItem : UserControl
    {
        private IMacroActionItem _macroActionItem;
		private MacroActionBundle _actionBundle;

		public MacroActionItem()
		{
			InitializeComponent();
			_macroActionItem = null;
			ActionBundle = null;
		}

		public MacroActionItem(MacroActionBundle actionBundle) : this()
		{
			ActionBundle = actionBundle;
		}

		public event EventHandler MacroActionItemChanged;
		public event EventHandler MacroActionItemRemoved;
		public event EventHandler MacroActionItemMovedUp;
		public event EventHandler MacroActionItemMovedDown;

		#region Properties

		public MacroActionBundle ActionBundle
		{
			get { return _actionBundle; }
			private set
			{
				_actionBundle = value;
			    if (_actionBundle == null)
			    {
			        ProcessActionBundle = null;
			        DistributionActionBundle = null;
                    WaitActionBundle = null;
			    }
			    else
			    {
				    switch (_actionBundle.Type)
				    {
						case MacroActionType.Start:
						case MacroActionType.Stop:
						case MacroActionType.Restart:
						    ProcessActionBundle = _actionBundle;
						    break;
						case MacroActionType.Distribute:
						    DistributionActionBundle = _actionBundle;
						    break;
						case MacroActionType.Wait:
						    WaitActionBundle = _actionBundle;
						    break;
				    }
			    }
			}
		}

		private MacroActionBundle ProcessActionBundle { get; set; }
		private MacroActionBundle DistributionActionBundle { get; set; }
		private MacroActionBundle WaitActionBundle { get; set; }

		#endregion

		#region GUI event handlers

		private void MacroActionItem_Load(object sender, EventArgs e)
		{
			if (ActionBundle != null)
				SelectActionType(ActionBundle.Type);
		}

		private void ButtonRemove_Click(object sender, EventArgs e)
		{
			RaiseMacroActionItemRemovedEvent();
		}

		private void ButtonMoveUp_Click(object sender, EventArgs e)
		{
			RaiseMacroActionItemMovedUpEvent();
		}

		private void ButtonMoveDown_Click(object sender, EventArgs e)
		{
			RaiseMacroActionItemMovedDownEvent();
		}

		private void LinkLabelActionType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Picker.ShowMenu(linkLabelActionType, Enum.GetValues(typeof(MacroActionType)).Cast<MacroActionType>(), ContextMenu_SelectActionType_ActionTypeClicked);
		}

		#endregion

		#region Macro action item event handlers

		private void MacroActionItem_MacroActionItemChanged(object sender, EventArgs e)
		{
			RaiseMacroActionItemChangedEvent();
		}

		#endregion

		#region Picker event handlers

		private void ContextMenu_SelectActionType_ActionTypeClicked(MacroActionType actionType)
		{
			SelectActionType(actionType);
			RaiseMacroActionItemChangedEvent();
		}

		#endregion

		#region Event raisers

		private void RaiseMacroActionItemChangedEvent()
		{
			if (MacroActionItemChanged != null)
				MacroActionItemChanged(this, EventArgs.Empty);
		}

		private void RaiseMacroActionItemRemovedEvent()
		{
			if (MacroActionItemRemoved != null)
				MacroActionItemRemoved(this, EventArgs.Empty);
		}

		private void RaiseMacroActionItemMovedUpEvent()
		{
			if (MacroActionItemMovedUp != null)
				MacroActionItemMovedUp(this, EventArgs.Empty);
		}

		private void RaiseMacroActionItemMovedDownEvent()
		{
			if (MacroActionItemMovedDown != null)
				MacroActionItemMovedDown(this, EventArgs.Empty);
		}

		#endregion

		public void SetWidth(int width)
		{
			Width = width;
			if (_macroActionItem != null)
				_macroActionItem.SetWidth(panelAction.Width);
		}

		#region Helpers

		private void SelectActionType(MacroActionType actionType)
		{
			if (_macroActionItem != null)
			{
				_macroActionItem.MacroActionItemChanged -= MacroActionItem_MacroActionItemChanged;
				_macroActionItem.Dispose();
				panelAction.Controls.Remove((Control) _macroActionItem);
				_macroActionItem = null;
			}

			linkLabelActionType.Text = actionType.ToString();

			switch (actionType)
			{
				case MacroActionType.Start:
				case MacroActionType.Stop:
				case MacroActionType.Restart:
					ActionBundle = ProcessActionBundle ?? new MacroActionBundle(actionType);
					_macroActionItem = new MacroProcessActionItem(ProcessActionBundle);
					break;
				case MacroActionType.Distribute:
					ActionBundle = DistributionActionBundle ?? new MacroActionBundle(actionType);
					_macroActionItem = new MacroDistributionActionItem(DistributionActionBundle);
					break;
				case MacroActionType.Wait:
					ActionBundle = WaitActionBundle ?? new MacroActionBundle(actionType);
					_macroActionItem = new MacroWaitActionItem(WaitActionBundle);
					break;
			}

			if (_macroActionItem != null)
			{
				labelSeparator.Visible = true;
				_macroActionItem.MacroActionItemChanged += MacroActionItem_MacroActionItemChanged;
				_macroActionItem.SetWidth(panelAction.Width);
				panelAction.Controls.Add((Control) _macroActionItem);
			}
		}

		#endregion
	}
}

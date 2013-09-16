using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.MacroActionItems.Support;
using ProcessManagerUI.Utilities;

namespace ProcessManagerUI.Controls.MacroActionItems
{
	public partial class MacroWaitActionItem : UserControl, IMacroActionItem
    {
		#region MacroActionWaitForEventWrapper class

		private class MacroActionWaitForEventWrapper
		{
			public MacroActionWaitForEventWrapper(MacroActionWaitForEvent waitForEvent, bool capitalized = true)
			{
				WaitForEvent = waitForEvent;
				Capitalized = capitalized;
			}

			public MacroActionWaitForEvent WaitForEvent { get; private set; }
			private bool Capitalized { get; set; }

			public override string ToString()
			{
				switch (WaitForEvent)
				{
					case MacroActionWaitForEvent.Timeout:
						return Capitalized ? "For Timeout" : "For timeout";
					case MacroActionWaitForEvent.PreviousActionsCompleted:
						return Capitalized ? "For Previous Actions Completed" : "For previous actions completed";
					default:
						throw new InvalidOperationException();
				}
			}
		}

		#endregion

		private const string DEFAULT_WAIT_FOR_EVENT = "For event...";

		private MacroActionWaitForEvent? _selectedWaitForEvent;
		private bool discardNumericUpDownValueChangedEvents;

		public MacroWaitActionItem()
		{
			InitializeComponent();
			discardNumericUpDownValueChangedEvents = false;
			ActionBundle = null;
		}

		public MacroWaitActionItem(MacroActionBundle actionBundle) : this()
		{
			if (actionBundle.Type != MacroActionType.Wait)
				throw new ArgumentException("Invalid wait action type");

			ActionBundle = actionBundle;
		}

		public event EventHandler MacroActionItemChanged;

		#region Properties

		public MacroActionBundle ActionBundle { get; private set; }

		private MacroActionWaitForEvent? SelectedWaitForEvent
		{
			get { return _selectedWaitForEvent; }
			set
			{
				_selectedWaitForEvent = value;
				UpdateLinkLabelWaitForEvent();
			}
		}

		private int TimeoutMilliseconds { get; set; }

		private bool HasValidSelections
		{
			get { return SelectedWaitForEvent != null; }
		}

		#endregion

		#region GUI event handlers

		private void MacroDistributionActionItem_Load(object sender, EventArgs e)
		{
			if (ActionBundle == null)
				throw new InvalidOperationException();

			if (ActionBundle.Actions.Any())
			{
				MacroWaitAction macroAction = (MacroWaitAction) ActionBundle.Actions[0];
				_selectedWaitForEvent = macroAction.WaitForEvent;
				TimeoutMilliseconds = macroAction.TimeoutMilliseconds;
			}
			else
			{
				SelectedWaitForEvent = null;
				TimeoutMilliseconds = 0;
			}

			discardNumericUpDownValueChangedEvents = true;
			UpdateLinkLabelWaitForEvent();
			discardNumericUpDownValueChangedEvents = false;
		}

		private void LinkLabelWaitForEvent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			IEnumerable<MacroActionWaitForEventWrapper> waitForEvents = Enum.GetValues(typeof(MacroActionWaitForEvent))
				.Cast<MacroActionWaitForEvent>()
				.Select(waitForEvent => new MacroActionWaitForEventWrapper(waitForEvent));

			Picker.ShowMenu(linkLabelWaitForEvent, waitForEvents, ContextMenu_SelectWaitForEvent_WaitForEventClicked);
		}

        private void NumericUpDownTimeoutMilliseconds_ValueChanged(object sender, EventArgs e)
        {
	        if (discardNumericUpDownValueChangedEvents)
		        return;

			TimeoutMilliseconds = (int) numericUpDownTimeoutMilliseconds.Value;
			UpdateMacroActionBundle();
        }

		#endregion

		#region Picker event handlers

		private void ContextMenu_SelectWaitForEvent_WaitForEventClicked(MacroActionWaitForEventWrapper waitForEventWrapper)
		{
			SelectedWaitForEvent = waitForEventWrapper.WaitForEvent;
			UpdateMacroActionBundle();
		}

		#endregion

		#region Event raisers

		private void RaiseMacroActionItemChangedEvent()
		{
			if (MacroActionItemChanged != null)
				MacroActionItemChanged(this, EventArgs.Empty);
		}

		#endregion

		public void SetWidth(int width)
		{
			Width = width;
		}

		#region Helpers

		private void UpdateLinkLabelWaitForEvent()
		{
			linkLabelWaitForEvent.Text = _selectedWaitForEvent != null
				? new MacroActionWaitForEventWrapper(_selectedWaitForEvent.Value, false).ToString()
				: DEFAULT_WAIT_FOR_EVENT;
			ShowWaitForTimeoutPanel(_selectedWaitForEvent != null && _selectedWaitForEvent.Value == MacroActionWaitForEvent.Timeout);
		}

		private void UpdateMacroActionBundle()
		{
			ActionBundle.Actions.Clear();
			if (HasValidSelections)
				ActionBundle.Actions.Add(new MacroWaitAction(ActionBundle.Type, SelectedWaitForEvent.Value, TimeoutMilliseconds));

			RaiseMacroActionItemChangedEvent();
		}

        private void ShowWaitForTimeoutPanel(bool show)
        {
            panelTimeout.Visible = show;
            if (show)
                numericUpDownTimeoutMilliseconds.Value = TimeoutMilliseconds;
		}

		#endregion
	}
}

using System;
using System.Windows.Forms;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.MacroActionItems.Support;
using ProcessManagerUI.Support;

namespace ProcessManagerUI.Controls.MacroActionItems
{
	public partial class MacroWaitActionItem : UserControl, IMacroActionItem
    {
        #region MacroActionWaitForEventWrapper class

        private class MacroActionWaitForEventWrapper
        {
            public MacroActionWaitForEventWrapper(MacroActionWaitForEvent waitForEvent)
            {
                WaitForEvent = waitForEvent;
            }

            public MacroActionWaitForEvent WaitForEvent { get; private set; }

            public override string ToString()
            {
                switch (WaitForEvent)
                {
                    case MacroActionWaitForEvent.Timeout:
                        return "For Timeout";
                    case MacroActionWaitForEvent.PreviousActionsCompleted:
                        return "For Previous Actions Completed";
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        #endregion

        public MacroWaitActionItem()
		{
			InitializeComponent();
			MacroWaitAction = null;
		}

		public MacroWaitActionItem(MacroWaitAction macroWaitAction) : this()
		{
			MacroWaitAction = macroWaitAction;
		}

		#region Properties

        public MacroWaitAction MacroWaitAction { get; private set; }

		#endregion

		#region GUI event handlers

		private void MacroDistributionActionItem_Load(object sender, EventArgs e)
		{
			if (MacroWaitAction == null)
				throw new InvalidOperationException();

            FillWaitForEventComboBox();
		}

		private void ComboBoxWaitForEvent_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxWaitForEvent.SelectedIndex > -1)
			{
                MacroActionWaitForEventWrapper waitForEventWrapper = ((ComboBoxItem<MacroActionWaitForEventWrapper>) comboBoxWaitForEvent.Items[comboBoxWaitForEvent.SelectedIndex]).Tag;
				MacroWaitAction.WaitForEvent = (waitForEventWrapper != null ? (MacroActionWaitForEvent?) waitForEventWrapper.WaitForEvent : null);
			}
			else
				MacroWaitAction.WaitForEvent = null;

            ShowWaitForTimeoutPanel(MacroWaitAction.WaitForEvent.HasValue && MacroWaitAction.WaitForEvent.Value == MacroActionWaitForEvent.Timeout);
		}

        private void NumericUpDownTimeoutMilliseconds_ValueChanged(object sender, EventArgs e)
        {
            MacroWaitAction.TimeoutMilliseconds = (int) numericUpDownTimeoutMilliseconds.Value;
        }

		#endregion

		public void SetWidth(int width)
		{
			Width = width;
		}

		#region Helpers

        private void ClearWaitForEventComboBox()
		{
			comboBoxWaitForEvent.Items.Clear();
		}

        private void FillWaitForEventComboBox()
		{
            ClearWaitForEventComboBox();
            comboBoxWaitForEvent.Items.Add(new ComboBoxItem<MacroActionWaitForEventWrapper>(string.Empty));
            foreach (MacroActionWaitForEvent waitForEvent in Enum.GetValues(typeof(MacroActionWaitForEvent)))
            {
                int index = comboBoxWaitForEvent.Items.Add(new ComboBoxItem<MacroActionWaitForEventWrapper>(new MacroActionWaitForEventWrapper(waitForEvent)));
                if (waitForEvent == MacroWaitAction.WaitForEvent)
                    comboBoxWaitForEvent.SelectedIndex = index;
            }
		}

        private void ShowWaitForTimeoutPanel(bool show)
        {
            panelTimeout.Visible = show;
            if (show)
                numericUpDownTimeoutMilliseconds.Value = MacroWaitAction.TimeoutMilliseconds;
		}

		#endregion
	}
}

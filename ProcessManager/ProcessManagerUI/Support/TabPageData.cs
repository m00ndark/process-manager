using ProcessManager;

namespace ProcessManagerUI.Support
{
	public class TabPageData
	{
		public TabPageData(ControlPanelTab controlPanelTab)
		{
			ControlPanelTab = controlPanelTab;
			Invalidate();
		}

		#region Properties

		public ControlPanelTab ControlPanelTab { get; private set; }
		public bool Invalidated { get; private set; }

		#endregion

		public void Validate()
		{
			Invalidated = false;
		}

		public void Invalidate()
		{
			Invalidated = true;
		}

		public override string ToString()
		{
			return ControlPanelTab.ToString();
		}
	}
}

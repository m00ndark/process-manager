using ProcessManager;

namespace ProcessManagerUI.Support
{
	public class TabPageData
	{
		public TabPageData(ControlPanelTab controlPanelTab)
		{
			ControlPanelTab = controlPanelTab;
			Initialized = false;
		}

		#region Properties

		public ControlPanelTab ControlPanelTab { get; private set; }
		public bool Initialized { get; set; }

		#endregion

		public override string ToString()
		{
			return ControlPanelTab.ToString();
		}
	}
}

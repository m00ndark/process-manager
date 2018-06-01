using System;

namespace ProcessManagerUI.Controls.MacroActionItems.Support
{
	interface IMacroActionItem : IDisposable
	{
		event EventHandler MacroActionItemChanged;

		void SetWidth(int width);
	}
}

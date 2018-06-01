using System;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes;

namespace ProcessManagerUI
{
	public interface IControlPanel : IProcessManagerEventHandler
	{
		bool TakeAction(IAction action);
		void ApplyMacroActionState(Guid macroID, Guid macroActionID, MacroActionState state);
	}
}

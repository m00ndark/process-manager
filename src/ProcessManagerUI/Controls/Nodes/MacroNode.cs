using System;
using System.Collections.Generic;
using System.Linq;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class MacroNode : BaseMacroRootNode
	{
        public MacroNode(Macro macro, IEnumerable<IMacroNode> childNodes)
			: base(childNodes, !Settings.Client.M_CollapsedNodes.Contains(macro.ID))
		{
			Macro = macro;
			//BackColor = Color.FromArgb(255, 208, 160);
		}

		#region Properties

        public Macro Macro { get; }

		public override Guid ID => Macro.ID;
		protected override string NodeName => Macro.Name;

		#endregion

		public override void TakeAction(ActionType type)
		{
			MacroAction macroAction = new MacroAction(type, Macro);
			macroAction.Actions.AddRange(GetCheckedLeafNodes()
				.Where(node => node is MacroActionNode)
				.Cast<MacroActionNode>()
				.Select(node => node.MacroAction));
			RaiseActionTakenEvent(macroAction);
		}

		protected override void UpdateMacroAction(MacroAction action)
		{
			action.Macro = Macro;
		}
	}
}

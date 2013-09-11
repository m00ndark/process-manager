using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class MacroNode : BaseMacroRootNode
	{
        public MacroNode(Macro macro, IEnumerable<INode> childNodes)
			: base(childNodes, !Settings.Client.M_CollapsedNodes.Contains(macro.ID))
		{
			Macro = macro;
			//BackColor = Color.FromArgb(255, 208, 160);
		}

		#region Properties

        public Macro Macro { get; private set; }

		public override Guid ID { get { return Macro.ID; } }
        protected override string NodeName { get { return Macro.Name; } }

		#endregion

		public override void TakeAction(ActionType type)
		{
			MacroAction macroAction = new MacroAction(type, Macro);
			macroAction.Actions.AddRange(ChildNodes
				.Where(node => node.CheckState == CheckState.Checked)
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

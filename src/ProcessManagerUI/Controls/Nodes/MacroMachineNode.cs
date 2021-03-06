﻿using System;
using System.Collections.Generic;
using System.Linq;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;
using ToolComponents.Core;

namespace ProcessManagerUI.Controls.Nodes
{
	public class MacroMachineNode : BaseMacroRootNode
	{
		private readonly Guid _id;
		private readonly string _nodeName;

		public MacroMachineNode(Guid machineID, MacroActionType type, Macro macro, IEnumerable<IMacroNode> childNodes)
			: this(MakeID(macro.ID, machineID), machineID, type, macro, childNodes) { }

		private MacroMachineNode(Guid id, Guid machineID, MacroActionType type, Macro macro, IEnumerable<IMacroNode> childNodes)
			: base(childNodes, !Settings.Client.M_CollapsedNodes.Contains(id))
		{
			if (type == MacroActionType.Wait)
				throw new ArgumentException("Invalid action type for macro machine node");

			_id = id;
			MachineID = machineID;
			Type = type;
			Macro = macro;
			_nodeName = GetNodeName();
			//BackColor = Color.FromArgb(255, 208, 160);
		}

		#region Properties

        public Macro Macro { get; }
		public Guid MachineID { get; }
		public MacroActionType Type { get; }

		public override Guid ID => _id;
		protected override string NodeName => _nodeName;

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
			// nothing to update with
		}

		#region Helpers

		private static Guid MakeID(Guid macroID, Guid machineID)
		{
			return Cryptographer.CreateGuid(macroID.ToString() + machineID);
		}

		private string GetNodeName()
		{
			Machine machine = Settings.Client.Machines.FirstOrDefault(x => x.ID == MachineID);
			return machine == null
				? null
				: (Type == MacroActionType.Distribute ? "Distribute to " : $"{Type} at ") + machine.HostName;
		}

		#endregion
	}
}

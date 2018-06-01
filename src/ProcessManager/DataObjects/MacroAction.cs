using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects
{
	public class MacroAction : IAction
	{
		public MacroAction(ActionType type, Macro macro)
		{
			if (type != ActionType.Play)
				throw new ArgumentException("Invalid macro action type");

			Type = type;
			Macro = macro;
			Actions = new List<IMacroAction>();
		}

		public MacroAction(ActionType type, IMacroAction action) : this(type)
		{
			Actions.Add(action);
		}

		public MacroAction(ActionType type) : this(type, macro: null) {}

		#region Properties

		public ActionType Type { get; }
		public Macro Macro { get; set; }
		public List<IMacroAction> Actions { get; }

		#endregion
	}
}

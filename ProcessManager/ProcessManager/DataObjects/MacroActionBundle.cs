using System.Collections.Generic;
using System.Linq;

namespace ProcessManager.DataObjects
{
	public class MacroActionBundle
	{
		public MacroActionBundle(MacroActionType type)
		{
			Type = type;
			Actions = new List<IMacroAction>();
		}

		#region Properties

		public MacroActionType Type { get; private set; }
		public List<IMacroAction> Actions { get; private set; }

		public bool IsValid { get { return Actions.Aggregate(Actions.Count > 0, (allValid, action) => allValid && action.IsValid); } }

		#endregion
	}
}

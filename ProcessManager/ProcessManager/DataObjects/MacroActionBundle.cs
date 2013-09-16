using System.Collections.Generic;
using System.Linq;
using ProcessManager.DataObjects.Comparers;

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

		#region Equality

		public override bool Equals(object obj)
		{
			MacroActionBundle macroActionBundle = obj as MacroActionBundle;
			return (macroActionBundle != null
				&& macroActionBundle.Type == Type
				&& macroActionBundle.Actions.SequenceEqual(Actions));
		}

		public override int GetHashCode()
		{
			return Actions.Aggregate(Type.GetHashCode(), (hashCode, action) => hashCode ^ (action != null ? action.GetHashCode() : 0) * 397);
		}

		#endregion
	}
}

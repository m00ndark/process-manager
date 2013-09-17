using System;
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

		private MacroActionBundle(MacroActionBundle macroActionBundle)
		{
			Type = macroActionBundle.Type;
			Actions = new List<IMacroAction>(macroActionBundle.Actions);
		}

		#region Properties

		public MacroActionType Type { get; private set; }
		public List<IMacroAction> Actions { get; private set; }

		public bool IsValid { get { return Actions.Aggregate(Actions.Count > 0, (allValid, action) => allValid && action.IsValid); } }

		#endregion

		public void ChangeActionType(MacroActionType actionType)
		{
			if (Type == MacroActionType.Distribute || Type == MacroActionType.Wait)
				throw new InvalidOperationException("Cannot change action type of distribution and wait macro actions");

			if (actionType == MacroActionType.Distribute || actionType == MacroActionType.Wait)
				throw new ArgumentException("Cannot change action type of process macro actions to distribute and wait");

			Type = actionType;
			Actions.ForEach(action => action.ChangeActionType(actionType));
		}

		public MacroActionBundle Copy()
		{
			return new MacroActionBundle(this);
		}

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

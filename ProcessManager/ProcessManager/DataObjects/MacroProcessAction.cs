using System;

namespace ProcessManager.DataObjects
{
	public class MacroProcessAction : IMacroAction
	{
		public MacroProcessAction(ActionType type)
		{
			Type = type;
			MachineID = Guid.Empty;
			GroupID = Guid.Empty;
			ApplicationID = Guid.Empty;
		}

		#region Properties

		public ActionType Type { get; private set; }
		public Guid MachineID { get; set; }
		public Guid GroupID { get; set; }
		public Guid ApplicationID { get; set; }

		public bool GotAnyID { get { return MachineID != Guid.Empty; } }

		#endregion

		#region Equality

		public override bool Equals(object obj)
		{
			MacroProcessAction macroProcessAction = obj as MacroProcessAction;
			return (macroProcessAction != null
				&& macroProcessAction.Type == Type
				&& macroProcessAction.MachineID == MachineID
				&& macroProcessAction.GroupID == GroupID
				&& macroProcessAction.ApplicationID == ApplicationID);
		}

		public override int GetHashCode()
		{
			return (Type.ToString() + MachineID + GroupID + ApplicationID).GetHashCode();
		}

		#endregion
	}
}

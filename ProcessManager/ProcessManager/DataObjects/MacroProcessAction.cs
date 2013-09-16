﻿using System;

namespace ProcessManager.DataObjects
{
	public class MacroProcessAction : IMacroAction
	{
		public MacroProcessAction(MacroActionType type, Guid machineID, Guid groupID, Guid applicationID)
			: this(Guid.NewGuid(), type, machineID, groupID, applicationID) { }

        public MacroProcessAction(Guid id, MacroActionType type, Guid machineID, Guid groupID, Guid applicationID)
		{
            if (type != MacroActionType.Start && type != MacroActionType.Stop && type != MacroActionType.Restart)
                throw new ArgumentException("Invalid process action type");

			ID = id;
            Type = type;
	        MachineID = machineID;
	        GroupID = groupID;
	        ApplicationID = applicationID;
		}

		#region Properties

		public Guid ID { get; private set; }
		public MacroActionType Type { get; private set; }
		public Guid MachineID { get; private set; }
		public Guid GroupID { get; private set; }
		public Guid ApplicationID { get; private set; }

		public bool IsValid { get { return MachineID != Guid.Empty && GroupID != Guid.Empty && ApplicationID != Guid.Empty; } }

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
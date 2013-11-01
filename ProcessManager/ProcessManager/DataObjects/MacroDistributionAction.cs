using System;

namespace ProcessManager.DataObjects
{
	public class MacroDistributionAction : IMacroAction
	{
		public MacroDistributionAction(MacroActionType type, Guid sourceMachineID, Guid groupID, Guid applicationID, Guid destinationMachineID)
			: this(Guid.NewGuid(), type, sourceMachineID, groupID, applicationID, destinationMachineID) {}

		public MacroDistributionAction(Guid id, MacroActionType type, Guid sourceMachineID, Guid groupID, Guid applicationID, Guid destinationMachineID)
		{
            if (type != MacroActionType.Distribute)
                throw new ArgumentException("Invalid distribution action type");

	        ID = id;
            Type = type;
			SourceMachineID = sourceMachineID;
			GroupID = groupID;
			ApplicationID = applicationID;
			DestinationMachineID = destinationMachineID;
		}

		#region Properties

		public Guid ID { get; private set; }
        public MacroActionType Type { get; private set; }
		public Guid SourceMachineID { get; private set; }
		public Guid GroupID { get; private set; }
		public Guid ApplicationID { get; private set; }
		public Guid DestinationMachineID { get; private set; }

		public bool IsValid { get { return SourceMachineID != Guid.Empty && GroupID != Guid.Empty && ApplicationID != Guid.Empty && DestinationMachineID != Guid.Empty; } }

		#endregion

		public IMacroAction Copy()
		{
			return new MacroDistributionAction(Guid.NewGuid(), Type, SourceMachineID, GroupID, ApplicationID, DestinationMachineID);
		}

		public void ChangeActionType(MacroActionType actionType)
		{
			throw new InvalidOperationException("Cannot change action type of macro wait action");
		}

		#region Equality

		public override bool Equals(object obj)
		{
			MacroDistributionAction macroDistributionAction = obj as MacroDistributionAction;
			return (macroDistributionAction != null
				&& macroDistributionAction.Type == Type
				&& macroDistributionAction.SourceMachineID == SourceMachineID
				&& macroDistributionAction.GroupID == GroupID
				&& macroDistributionAction.ApplicationID == ApplicationID
				&& macroDistributionAction.DestinationMachineID == DestinationMachineID);
		}

		public override int GetHashCode()
		{
			return (Type.ToString() + SourceMachineID + GroupID + ApplicationID + DestinationMachineID).GetHashCode();
		}

		#endregion
	}
}

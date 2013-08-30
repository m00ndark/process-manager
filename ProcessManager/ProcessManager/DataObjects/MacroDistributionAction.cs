using System;

namespace ProcessManager.DataObjects
{
	public class MacroDistributionAction : IMacroAction
	{
		public MacroDistributionAction(ActionType type)
		{
			Type = type;
			SourceMachineID = Guid.Empty;
			GroupID = Guid.Empty;
			ApplicationID = Guid.Empty;
			DestinationMachineID = Guid.Empty;
		}

		#region Properties

		public ActionType Type { get; private set; }
		public Guid SourceMachineID { get; set; }
		public Guid GroupID { get; set; }
		public Guid ApplicationID { get; set; }
		public Guid DestinationMachineID { get; set; }

		public bool GotAnyID { get { return SourceMachineID != Guid.Empty; } }

		#endregion

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

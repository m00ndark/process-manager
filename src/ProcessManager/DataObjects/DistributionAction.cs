using System;

namespace ProcessManager.DataObjects
{
	public class DistributionAction : IAction
	{
		public DistributionAction(ActionType type) : this(type, null) { }

		public DistributionAction(ActionType type, Machine destinationMachine) : this(type, null, null, null, destinationMachine) { }

		public DistributionAction(ActionType type, Machine sourceMachine, Group group, Application application, Machine destinationMachine)
		{
			if (type != ActionType.Distribute)
				throw new ArgumentException("Invalid distribution action type");

			Type = type;
			SourceMachine = sourceMachine;
			Group = group;
			Application = application;
			DestinationMachine = destinationMachine;
		}

		#region Properties

		public ActionType Type { get; }
		public Machine SourceMachine { get; set; }
		public Group Group { get; set; }
		public Application Application { get; set; }
		public Machine DestinationMachine { get; set; }

		#endregion
	}
}

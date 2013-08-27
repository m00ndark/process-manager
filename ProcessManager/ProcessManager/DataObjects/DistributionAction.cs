using System;

namespace ProcessManager.DataObjects
{
	public class DistributionAction : IAction
	{
		public DistributionAction(ActionType type, Machine destinationMachine)
		{
			if (type != ActionType.Distribute)
				throw new ArgumentException("Invalid distribution action type");

			Type = type;
			DestinationMachine = destinationMachine;
		}

		public DistributionAction(ActionType type, Machine sourceMachine, Group group, Application application, Machine destinationMachine) : this(type, destinationMachine)
		{
			SourceMachine = sourceMachine;
			Group = group;
			Application = application;
		}

		#region Properties

		public ActionType Type { get; private set; }
		public Machine SourceMachine { get; set; }
		public Group Group { get; set; }
		public Application Application { get; set; }
		public Machine DestinationMachine { get; set; }

		#endregion
	}
}

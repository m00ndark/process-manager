using System.Collections.Generic;

namespace ProcessManager.DataObjects
{
	public class DistributionWork
	{
		public DistributionWork(ActionType type, Group group, Application application, Machine destinationMachine)
		{
			Type = type;
			Group = group;
			Application = application;
			DestinationMachine = destinationMachine;
			Files = new List<DistributionFile>();
		}

		#region Properties

		public ActionType Type { get; private set; }
		public Group Group { get; private set; }
		public Application Application { get; private set; }
		public Machine DestinationMachine { get; private set; }
		public List<DistributionFile> Files { get; set; }

		#endregion
	}
}

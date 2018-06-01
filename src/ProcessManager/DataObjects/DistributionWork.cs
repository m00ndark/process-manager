using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects
{
	public class DistributionWork
	{
		public DistributionWork(ActionType type, Machine sourceMachine, Group @group, Application application, Machine destinationMachine, Guid clientId)
		{
			Type = type;
			SourceMachine = sourceMachine;
			Group = group;
			Application = application;
			DestinationMachine = destinationMachine;
			ClientId = clientId;
			Files = new List<DistributionFile>();
		}

		public ActionType Type { get; }
		public Machine SourceMachine { get; }
		public Group Group { get; }
		public Application Application { get; }
		public Machine DestinationMachine { get; }
		public Guid ClientId { get; }
		public List<DistributionFile> Files { get; set; }
	}
}

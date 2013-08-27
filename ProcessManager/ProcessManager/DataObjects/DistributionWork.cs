using System.Collections.Generic;
using ProcessManager.Service.Common;

namespace ProcessManager.DataObjects
{
	public class DistributionWork
	{
		public DistributionWork(ActionType type, Machine sourceMachine, Group @group, Application application, Machine destinationMachine, IProcessManagerServiceEventHandler caller)
		{
			Type = type;
			SourceMachine = sourceMachine;
			Group = group;
			Application = application;
			DestinationMachine = destinationMachine;
			Caller = caller;
			Files = new List<DistributionFile>();
		}

		#region Properties

		public ActionType Type { get; private set; }
		public Machine SourceMachine { get; private set; }
		public Group Group { get; private set; }
		public Application Application { get; private set; }
		public Machine DestinationMachine { get; private set; }
		public IProcessManagerServiceEventHandler Caller { get; private set; }
		public List<DistributionFile> Files { get; set; }

		#endregion
	}
}

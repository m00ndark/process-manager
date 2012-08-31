namespace ProcessManager.DataObjects
{
	public class DistributionAction : IAction
	{
		public DistributionAction(Machine destinationMachine)
		{
			DestinationMachine = destinationMachine;
		}

		#region Properties

		public Machine SourceMachine { get; set; }
		public Group SourceGroup { get; set; }
		public Application SourceApplication { get; set; }
		public Machine DestinationMachine { get; set; }

		#endregion
	}
}

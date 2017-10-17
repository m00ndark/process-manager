using System;
using ProcessManager.DataObjects;

namespace ProcessManager.EventArguments
{
	public class DistributionResultEventArgs : EventArgs
	{
		public DistributionResultEventArgs(DistributionResult distributionResult, Guid clientId = default(Guid))
		{
			DistributionResult = distributionResult;
			ClientId = clientId;
		}

		public DistributionResult DistributionResult { get; }
		public Guid ClientId { get; }
	}
}

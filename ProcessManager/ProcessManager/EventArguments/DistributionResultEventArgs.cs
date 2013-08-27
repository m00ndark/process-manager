using System;
using ProcessManager.DataObjects;
using ProcessManager.Service.Common;

namespace ProcessManager.EventArguments
{
	public class DistributionResultEventArgs : EventArgs
	{
		public DistributionResultEventArgs(DistributionResult distributionResult, IProcessManagerServiceEventHandler caller = null)
		{
			DistributionResult = distributionResult;
			Caller = caller;
		}

		#region Properties

		public DistributionResult DistributionResult { get; private set; }
		public IProcessManagerServiceEventHandler Caller { get; set; }

		#endregion
	}
}

using System;

namespace ProcessManager.Exceptions
{
	public class DistributionActionException : Exception
	{
		public DistributionActionException(string message) : base(message) {}
		public DistributionActionException(string message, Exception innerException) : base(message, innerException) {}
	}
}

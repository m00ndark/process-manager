using System;

namespace ProcessManager.Exceptions
{
	public class ProcessActionException : Exception
	{
		public ProcessActionException(string message) : base(message) {}
		public ProcessActionException(string message, Exception innerException) : base(message, innerException) {}
	}
}

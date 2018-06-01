using System;

namespace ProcessManager.Exceptions
{
	public class DistributeFileException : Exception
	{
		public DistributeFileException(string message) : base(message) {}
		public DistributeFileException(string message, Exception innerException) : base(message, innerException) {}
	}
}

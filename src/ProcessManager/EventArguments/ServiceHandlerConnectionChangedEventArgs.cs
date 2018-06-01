using System;
using ProcessManager.Service.Client;

namespace ProcessManager.EventArguments
{
	public class ServiceHandlerConnectionChangedEventArgs : EventArgs
	{
		public ServiceHandlerConnectionChangedEventArgs(ProcessManagerServiceHandler serviceHandler, ProcessManagerServiceHandlerStatus status)
			: this(serviceHandler, status, null) {}

		public ServiceHandlerConnectionChangedEventArgs(ProcessManagerServiceHandler serviceHandler, ProcessManagerServiceHandlerStatus status, Exception exception)
		{
			ServiceHandler = serviceHandler;
			Status = status;
			Exception = exception;
		}

		#region Properties

		public ProcessManagerServiceHandler ServiceHandler { get; private set; }
		public ProcessManagerServiceHandlerStatus Status { get; private set; }
		public Exception Exception { get; private set; }

		#endregion
	}
}

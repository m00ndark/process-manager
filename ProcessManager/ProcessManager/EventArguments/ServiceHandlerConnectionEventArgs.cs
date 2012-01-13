using System;
using ProcessManager.Service.Client;

namespace ProcessManager.EventArguments
{
	public class ServiceHandlerConnectionEventArgs : EventArgs
	{
		public ServiceHandlerConnectionEventArgs(ProcessManagerServiceHandler serviceHandler)
		{
			ServiceHandler = serviceHandler;
		}

		#region Properties

		public ProcessManagerServiceHandler ServiceHandler { get; set; }

		#endregion
	}
}

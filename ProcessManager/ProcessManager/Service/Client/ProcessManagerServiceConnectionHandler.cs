using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;

namespace ProcessManager.Service.Client
{
	public class ProcessManagerServiceConnectionHandler
	{
		private static volatile ProcessManagerServiceConnectionHandler _instance;
		private static readonly object _lock = new object();

		public event EventHandler<ServiceHandlerConnectionChangedEventArgs> ServiceHandlerInitializationCompleted;
		public event EventHandler<ServiceHandlerConnectionChangedEventArgs> ServiceHandlerConnectionChanged;

		private ProcessManagerServiceConnectionHandler() { }

		#region Properties

		public static ProcessManagerServiceConnectionHandler Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock)
					{
						if (_instance == null)
							_instance = new ProcessManagerServiceConnectionHandler();
					}
				}
				return _instance;
			}
		}

		#endregion

		public void CreateServiceHandler(IProcessManagerEventHandler processManagerEventHandler, Machine machine)
		{
			machine.ServiceHandler = new ProcessManagerServiceHandler(processManagerEventHandler, machine);
			machine.ServiceHandler.InitializationCompleted += ServiceHandler_InitializationCompleted;
			machine.ServiceHandler.ConnectionChanged += ServiceHandler_ConnectionChanged;
		}

		#region Service handler event handlers

		private void ServiceHandler_InitializationCompleted(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			try
			{
				ModifyMachineConfiguration(e.ServiceHandler, e.Status == ProcessManagerServiceHandlerStatus.Connected);

				RaiseServiceHandlerInitializationCompletedEvent(e.ServiceHandler, e.ServiceHandler.Status, e.Exception);
			}
			catch (Exception ex)
			{
				RaiseServiceHandlerInitializationCompletedEvent(e.ServiceHandler, e.ServiceHandler.Status, ex);
			}
		}

		private void ServiceHandler_ConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			try
			{
				ModifyMachineConfiguration(e.ServiceHandler, e.Status == ProcessManagerServiceHandlerStatus.Connected);

				RaiseServiceHandlerConnectionChangedEvent(e.ServiceHandler, e.ServiceHandler.Status, e.Exception);
			}
			catch (Exception ex)
			{
				RaiseServiceHandlerConnectionChangedEvent(e.ServiceHandler, e.ServiceHandler.Status, ex);
			}
		}

		#endregion

		#region Event raisers

		private void RaiseServiceHandlerInitializationCompletedEvent(ProcessManagerServiceHandler serviceHandler, ProcessManagerServiceHandlerStatus status, Exception exception = null)
		{
			if (ServiceHandlerInitializationCompleted != null)
				ServiceHandlerInitializationCompleted(this, new ServiceHandlerConnectionChangedEventArgs(serviceHandler, status, exception));
		}

		private void RaiseServiceHandlerConnectionChangedEvent(ProcessManagerServiceHandler serviceHandler, ProcessManagerServiceHandlerStatus status, Exception exception = null)
		{
			if (ServiceHandlerConnectionChanged != null)
				ServiceHandlerConnectionChanged(this, new ServiceHandlerConnectionChangedEventArgs(serviceHandler, status, exception));
		}

		#endregion

		private void ModifyMachineConfiguration(ProcessManagerServiceHandler serviceHandler, bool retrieve)
		{
			Machine machine = Settings.Client.Machines.FirstOrDefault(x => x.ServiceHandler == serviceHandler);
			if (machine != null)
			{
				try
				{
					machine.Configuration = (retrieve ? machine.ServiceHandler.Service.GetConfiguration().FromDTO() : null);
				}
				catch { ; }
			}
		}
	}
}

using System;
using System.Linq;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ToolComponents.Core.Logging;

namespace ProcessManager.Service.Client
{
	public class ProcessManagerServiceConnectionHandler
	{
		private static volatile ProcessManagerServiceConnectionHandler _instance;
		private static readonly object _lock = new object();

		public event EventHandler<ServiceHandlerConnectionChangedEventArgs> ServiceHandlerInitializationCompleted;
		public event EventHandler<ServiceHandlerConnectionChangedEventArgs> ServiceHandlerConnectionChanged;

		private ProcessManagerServiceConnectionHandler() {}

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

		public ProcessManagerServiceHandler CreateServiceHandler(IProcessManagerEventHandler processManagerEventHandler, Machine machine)
		{
			ProcessManagerServiceHandler serviceHandler = new ProcessManagerServiceHandler(processManagerEventHandler, machine);
			serviceHandler.InitializationCompleted += ServiceHandler_InitializationCompleted;
			serviceHandler.ConnectionChanged += ServiceHandler_ConnectionChanged;
			return serviceHandler;
		}

		#region Service handler event handlers

		private void ServiceHandler_InitializationCompleted(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			try
			{
				Logger.Add($"{e.ServiceHandler.Machine}: Initialization completed: status = {e.ServiceHandler.Status}", e.Exception);

				ModifyMachineConfiguration(e.ServiceHandler, e.Status == ProcessManagerServiceHandlerStatus.Connected);

				RaiseServiceHandlerInitializationCompletedEvent(e.ServiceHandler, e.ServiceHandler.Status, e.Exception);
			}
			catch (Exception ex)
			{
				Logger.Add($"{e.ServiceHandler.Machine}: Initialization completed, exception occurred: status = {e.ServiceHandler.Status}", ex);
				RaiseServiceHandlerInitializationCompletedEvent(e.ServiceHandler, e.ServiceHandler.Status, ex);
			}
		}

		private void ServiceHandler_ConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			try
			{
				Logger.Add($"{e.ServiceHandler.Machine}: Connection changed: status = {e.ServiceHandler.Status}", e.Exception);

				ModifyMachineConfiguration(e.ServiceHandler, e.Status == ProcessManagerServiceHandlerStatus.Connected);

				RaiseServiceHandlerConnectionChangedEvent(e.ServiceHandler, e.ServiceHandler.Status, e.Exception);
			}
			catch (Exception ex)
			{
				Logger.Add($"{e.ServiceHandler.Machine}: Connection changed, exception occurred: status = {e.ServiceHandler.Status}", ex);
				RaiseServiceHandlerConnectionChangedEvent(e.ServiceHandler, e.ServiceHandler.Status, ex);
			}
		}

		#endregion

		#region Event raisers

		private void RaiseServiceHandlerInitializationCompletedEvent(ProcessManagerServiceHandler serviceHandler, ProcessManagerServiceHandlerStatus status, Exception exception = null)
		{
			ServiceHandlerInitializationCompleted?.Invoke(this, new ServiceHandlerConnectionChangedEventArgs(serviceHandler, status, exception));
		}

		private void RaiseServiceHandlerConnectionChangedEvent(ProcessManagerServiceHandler serviceHandler, ProcessManagerServiceHandlerStatus status, Exception exception = null)
		{
			ServiceHandlerConnectionChanged?.Invoke(this, new ServiceHandlerConnectionChangedEventArgs(serviceHandler, status, exception));
		}

		#endregion

		private static void ModifyMachineConfiguration(ProcessManagerServiceHandler serviceHandler, bool retrieve)
		{
			MachineConnection machineConnection = ConnectionStore.Connections.Values.FirstOrDefault(x => x.ServiceHandler == serviceHandler);

			if (machineConnection == null)
				return;

			try
			{
				machineConnection.Configuration = (retrieve ? machineConnection.ServiceHandler.Service.GetConfiguration().FromDTO() : null);
			}
			catch (Exception ex)
			{
				Logger.Add("Failed to retrieve machine configuration", ex);
			}
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
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
			MachineConnection machineConnection = ConnectionStore.Connections.Values.FirstOrDefault(x => x.ServiceHandler == serviceHandler);
			if (machineConnection != null)
			{
				try
				{
					machineConnection.Configuration = (retrieve ? machineConnection.ServiceHandler.Service.GetConfiguration().FromDTO() : null);
				}
				catch { ; }
			}
		}
	}
}
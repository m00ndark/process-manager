using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using ProcessManager.DataObjects;
using ProcessManager.Service.Common;
using ProcessManager.Utilities;

namespace ProcessManager.Service.Client
{
	public class ProcessManagerServiceHandler : IDisposable
	{
		private readonly Machine _machine;
		private readonly ProcessManagerServiceEventHandler _processManagerServiceEventHandler;
		private readonly IProcessManagerEventHandler _processManagerEventHandler;
		private ProcessManagerServiceClient _processManagerServiceClient;
		private Thread _connectionWatcherThread;

		public ProcessManagerServiceHandler(IProcessManagerEventHandler processManagerEventHandler, Machine machine)
		{
			_machine = machine;
			_processManagerEventHandler = processManagerEventHandler;
			_processManagerServiceEventHandler = new ProcessManagerServiceEventHandler();
			_processManagerServiceEventHandler.ApplicationStatusesChanged += _processManagerEventHandler.ProcessManagerServiceEventHandler_ApplicationStatusesChanged;
			_connectionWatcherThread = new Thread(ConnectionWatcher);
			SetupClient();
			_connectionWatcherThread.Start();
		}

		#region Properties

		public IProcessManagerService Service { get { return _processManagerServiceClient; } }

		#endregion

		#region Implementation of IDisposable

		public void Dispose()
		{
			if (_connectionWatcherThread != null)
			{
				_connectionWatcherThread.Abort();
				_connectionWatcherThread = null;
				try
				{
					_processManagerServiceClient.Unsubscribe();
					_processManagerServiceClient.Abort();
				}
				catch { ; }
				_processManagerServiceEventHandler.ApplicationStatusesChanged -= _processManagerEventHandler.ProcessManagerServiceEventHandler_ApplicationStatusesChanged;
			}
		}

		#endregion

		#region Connection setup and watcher

		private void SetupClient()
		{
			Binding binding = new NetNamedPipeBinding();
			EndpointAddress endpointAddress = new EndpointAddress("net.pipe://" + _machine.HostName + "/ProcessManagerService");
			InstanceContext context = new InstanceContext(_processManagerServiceEventHandler);
			_processManagerServiceClient = new ProcessManagerServiceClient(context, binding, endpointAddress);
		}

		private void ConnectionWatcher()
		{
			try
			{
				while (true)
				{
					if (_processManagerServiceClient.State == CommunicationState.Faulted)
						_processManagerServiceClient.Abort();

					if (_processManagerServiceClient.State == CommunicationState.Closed)
						SetupClient();

					if (_processManagerServiceClient.State == CommunicationState.Created)
						try { _processManagerServiceClient.Subscribe(); } catch { ; }

					Thread.Sleep(1000);
				}
			}
			catch (ThreadAbortException) { /* exit */ }
			catch (Exception ex)
			{
				Logger.Add("Connection watcher thread for host " + _machine.HostName + " exited due to an unexpected exception", ex);
			}
		}

		#endregion
	}
}

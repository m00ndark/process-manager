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
		private readonly bool _isSubscribing;
		private readonly ProcessManagerServiceEventHandler _processManagerServiceEventHandler;
		private readonly IProcessManagerEventHandler _processManagerEventHandler;
		private ProcessManagerServiceClient _processManagerServiceClient;
		private Thread _connectionWatcherThread;
		
		public ProcessManagerServiceHandler(Machine machine) : this(null, machine) { }

		public ProcessManagerServiceHandler(IProcessManagerEventHandler processManagerEventHandler, Machine machine)
		{
			_machine = machine;
			_processManagerEventHandler = processManagerEventHandler;
			_isSubscribing = (_processManagerEventHandler != null);
			_processManagerServiceEventHandler = new ProcessManagerServiceEventHandler();
			SetupClient();
			if (_isSubscribing)
			{
				_processManagerServiceEventHandler.ApplicationStatusesChanged += _processManagerEventHandler.ProcessManagerServiceEventHandler_ApplicationStatusesChanged;
				_connectionWatcherThread = new Thread(ConnectionWatcher);
				_connectionWatcherThread.Start();
			}
			else
			{
				_processManagerServiceClient.Register(false);
			}
		}

		#region Properties

		public IProcessManagerServiceOperator Service { get { return _processManagerServiceClient; } }

		#endregion

		#region Implementation of IDisposable

		public void Dispose()
		{
			if (_isSubscribing)
			{
				if (_connectionWatcherThread != null)
				{
					_connectionWatcherThread.Abort();
					_connectionWatcherThread = null;
				}
				_processManagerServiceEventHandler.ApplicationStatusesChanged -= _processManagerEventHandler.ProcessManagerServiceEventHandler_ApplicationStatusesChanged;
			}
			if (_processManagerServiceClient != null)
			{
				try
				{
					_processManagerServiceClient.Unregister();
					_processManagerServiceClient.Abort();
				}
				catch { ; }
			}
		}

		#endregion

		#region Connection setup and watcher

		private void SetupClient()
		{
			NetTcpBinding binding = new NetTcpBinding() { Security = { Mode = SecurityMode.None } };
			EndpointAddress endpointAddress = new EndpointAddress("net.tcp://" + _machine.HostName + "/ProcessManagerService");
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
						try { _processManagerServiceClient.Register(true); } catch { ; }

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

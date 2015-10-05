using System;
using System.ServiceModel;
using System.Threading;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Common;
using ToolComponents.Core.Logging;

namespace ProcessManager.Service.Client
{
	public enum ProcessManagerServiceHandlerStatus
	{
		Uninitialized,
		Connected,
		Disconnected
	}

	public class ProcessManagerServiceHandler : IDisposable
	{
		private readonly bool _isSubscribing;
		private readonly ProcessManagerServiceEventHandler _processManagerServiceEventHandler;
		private readonly IProcessManagerEventHandler _processManagerEventHandler;
		private ProcessManagerServiceClient _processManagerServiceClient;
		private Thread _connectionWatcherThread;

		public event EventHandler<ServiceHandlerConnectionChangedEventArgs> InitializationCompleted;
		public event EventHandler<ServiceHandlerConnectionChangedEventArgs> ConnectionChanged;

		public ProcessManagerServiceHandler(Machine machine) : this(null, machine) { }

		public ProcessManagerServiceHandler(IProcessManagerEventHandler processManagerEventHandler, Machine machine)
		{
			Machine = machine;
			Status = ProcessManagerServiceHandlerStatus.Uninitialized;
			_processManagerEventHandler = processManagerEventHandler;
			_isSubscribing = (_processManagerEventHandler != null);
			_processManagerServiceEventHandler = new ProcessManagerServiceEventHandler(Machine);
			SetupClient();
			if (_isSubscribing)
			{
				_processManagerServiceEventHandler.ProcessStatusesChanged += _processManagerEventHandler.ProcessManagerServiceEventHandler_ProcessStatusesChanged;
				_processManagerServiceEventHandler.ConfigurationChanged += _processManagerEventHandler.ProcessManagerServiceEventHandler_ConfigurationChanged;
				_processManagerServiceEventHandler.DistributionCompleted += _processManagerEventHandler.ProcessManagerServiceEventHandler_DistributionCompleted;
			}
		}

		#region Properties

		public Machine Machine { get; }
		public ProcessManagerServiceHandlerStatus Status { get; private set; }
		public IProcessManagerServiceOperator Service => _processManagerServiceClient;

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
				_processManagerServiceEventHandler.ProcessStatusesChanged -= _processManagerEventHandler.ProcessManagerServiceEventHandler_ProcessStatusesChanged;
				_processManagerServiceEventHandler.ConfigurationChanged -= _processManagerEventHandler.ProcessManagerServiceEventHandler_ConfigurationChanged;
				_processManagerServiceEventHandler.DistributionCompleted -= _processManagerEventHandler.ProcessManagerServiceEventHandler_DistributionCompleted;
			}
			if (_processManagerServiceClient != null)
			{
				try
				{
					_processManagerServiceClient.Unregister();
					_processManagerServiceClient.Abort();
				}
				catch { ; }
				_processManagerServiceClient = null;
			}
			InitializationCompleted = null;
			ConnectionChanged = null;
		}

		#endregion

		#region Event raisers

		private void RaiseInitializationCompletedEvent(Exception exception = null)
		{
			InitializationCompleted?.Invoke(this, new ServiceHandlerConnectionChangedEventArgs(this, Status, exception));
		}

		private void RaiseConnectionChangedEvent()
		{
			ConnectionChanged?.Invoke(this, new ServiceHandlerConnectionChangedEventArgs(this, Status));
		}

		#endregion

		#region Initialization and setup

		public void Initialize()
		{
			if (_isSubscribing)
			{
				_connectionWatcherThread = new Thread(ConnectionWatcher);
				_connectionWatcherThread.Start();
			}
			else
			{
				_processManagerServiceClient.Register(false);
				Status = ProcessManagerServiceHandlerStatus.Connected;
			}
		}

		private void SetupClient()
		{
			NetTcpBinding binding = new NetTcpBinding()
				{
					MaxBufferSize = Constants.MAX_MESSAGE_SIZE,
					MaxBufferPoolSize = Constants.MAX_MESSAGE_SIZE * 2,
					MaxReceivedMessageSize = Constants.MAX_MESSAGE_SIZE,
					Security = { Mode = SecurityMode.None }
				};
			EndpointAddress endpointAddress = new EndpointAddress(new Uri($"net.tcp://{Machine.HostName}/ProcessManagerService"));
			InstanceContext context = new InstanceContext(_processManagerServiceEventHandler);
			_processManagerServiceClient = new ProcessManagerServiceClient(context, binding, endpointAddress);
		}

		public static bool HostNameValid(Machine machine)
		{
			UriHostNameType hostNameType = Uri.CheckHostName(machine.HostName);
			return (hostNameType == UriHostNameType.Dns || hostNameType == UriHostNameType.IPv4 || hostNameType == UriHostNameType.IPv6);
		}

		#endregion

		#region Connection watcher

		private void ConnectionWatcher()
		{
			try
			{
				bool connectionLost = false, connectionAttempted = false;
				while (true)
				{
					if (_processManagerServiceClient.State == CommunicationState.Faulted)
					{
						Logger.Add(LogType.Debug, $"{Machine}: ConnectionWatcher: state = Faulted");
						_processManagerServiceClient.Abort();
					}

					if (_processManagerServiceClient.State == CommunicationState.Closed)
					{
						Logger.Add(LogType.Debug, $"{Machine}: ConnectionWatcher: state = Closed");
						SetupClient();
					}

					if (_processManagerServiceClient.State == CommunicationState.Created)
					{
						try
						{
							Logger.Add(LogType.Debug, $"{Machine}: ConnectionWatcher: state = Created");
							_processManagerServiceClient.Register(true);
							Status = ProcessManagerServiceHandlerStatus.Connected;
							if (!connectionAttempted)
								RaiseInitializationCompletedEvent();
							else if (connectionLost)
							{
								connectionLost = false;
								RaiseConnectionChangedEvent();
							}
						}
						catch (Exception ex)
						{
							Status = ProcessManagerServiceHandlerStatus.Disconnected;
							if (!connectionAttempted)
							{
								connectionLost = true;
								RaiseInitializationCompletedEvent(ex);
							}
							else if (!connectionLost)
							{
								connectionLost = true;
								RaiseConnectionChangedEvent();
							}
						}
						connectionAttempted = true;
					}

					Thread.Sleep(1000);
				}
			}
			catch (ThreadAbortException) { /* exit */ }
			catch (Exception ex)
			{
				Logger.Add($"Connection watcher thread for host {Machine.HostName} exited due to an unexpected exception", ex);
			}
		}

		#endregion
	}
}

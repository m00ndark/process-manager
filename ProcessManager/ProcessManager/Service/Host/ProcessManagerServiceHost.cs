using System;
using System.ServiceModel;
using ProcessManager.Service.Common;

namespace ProcessManager.Service.Host
{
	internal static class ProcessManagerServiceHost
	{
		private static ServiceHost _serviceHost = null;
		private static ProcessManagerService _processManagerService = null;
		private static IProcessManagerEventProvider _processManagerEventProvider;

		public static void Open(IProcessManagerEventProvider processManagerEventProvider)
		{
			if (_serviceHost == null || _serviceHost.State == CommunicationState.Closed || _serviceHost.State == CommunicationState.Faulted)
			{
				_processManagerService = new ProcessManagerService();
				_processManagerEventProvider = processManagerEventProvider;
				_processManagerEventProvider.ApplicationStatusesChanged += _processManagerService.ProcessManagerEventProvider_ApplicationStatusesChanged;
				_processManagerEventProvider.ConfigurationChanged += _processManagerService.ProcessManagerEventProvider_ConfigurationChanged;
				NetTcpBinding binding = new NetTcpBinding()
					{
						MaxBufferSize = Constants.MAX_MESSAGE_SIZE,
						MaxBufferPoolSize = Constants.MAX_MESSAGE_SIZE * 2,
						MaxReceivedMessageSize = Constants.MAX_MESSAGE_SIZE,
						Security = { Mode = SecurityMode.None }
					};
				_serviceHost = new ServiceHost(_processManagerService, new Uri("net.tcp://" + Environment.MachineName + "/ProcessManagerService"));
				_serviceHost.AddServiceEndpoint(typeof(IProcessManagerService), binding, string.Empty);
				_serviceHost.Open();
			}
		}

		public static void Close()
		{
			if (_serviceHost != null)
			{
				_serviceHost.Close();
				_serviceHost = null;
				_processManagerEventProvider.ApplicationStatusesChanged -= _processManagerService.ProcessManagerEventProvider_ApplicationStatusesChanged;
				_processManagerEventProvider.ConfigurationChanged -= _processManagerService.ProcessManagerEventProvider_ConfigurationChanged;
				_processManagerService = null;
			}
		}
	}
}

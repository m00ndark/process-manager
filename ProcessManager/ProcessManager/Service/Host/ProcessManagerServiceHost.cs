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
				_processManagerEventProvider = processManagerEventProvider;
				_processManagerEventProvider.ApplicationStatusesChanged += _processManagerService.ProcessManagerEventProvider_ApplicationStatusesChanged;
				_processManagerService = new ProcessManagerService();
				_serviceHost = new ServiceHost(_processManagerService, new Uri("net.pipe://" + Environment.MachineName + "/ProcessManagerService"));
				_serviceHost.AddServiceEndpoint(typeof(IProcessManagerService), new NetNamedPipeBinding(), string.Empty);
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
				_processManagerService = null;
			}
		}
	}
}

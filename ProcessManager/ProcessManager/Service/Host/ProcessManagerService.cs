using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ProcessManager.EventArguments;
using ProcessManager.Service.Common;
using ProcessManager.Service.DataObjects;

namespace ProcessManager.Service.Host
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	internal class ProcessManagerService : IProcessManagerService
	{
		private readonly IDictionary<IProcessManagerServiceEventHandler, bool> _clients;

		public ProcessManagerService()
		{
			_clients = new Dictionary<IProcessManagerServiceEventHandler, bool>();
		}

		#region Implementation of IProcessManagerService

		public void Register(bool subscribe)
		{
			_clients.Add(OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>(), subscribe);
		}

		public void Unregister()
		{
			IProcessManagerServiceEventHandler caller = OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>();
			_clients.Where(x => (x.Key == caller)).ToList().ForEach(x => _clients.Remove(x));
		}

		#endregion

		#region Implementation of IProcessManagerServiceOperator

		public void Ping()
		{
			// do nothing
		}

		#endregion

		#region Service event handlers

		public void ProcessManagerEventProvider_ApplicationStatusesChanged(object sender, ApplicationStatusesEventArgs e)
		{
			List<IProcessManagerServiceEventHandler> faultedClients = new List<IProcessManagerServiceEventHandler>();
			foreach (IProcessManagerServiceEventHandler client in _clients.Where(x => x.Value).Select(x => x.Key))
			{
				try
				{
					client.ServiceEvent_ApplicationStatusesChanged(e.ApplicationStatuses.Select(x => new DTOApplicationStatus(x)).ToList());
				}
				catch
				{
					faultedClients.Add(client);
				}
			}
			faultedClients.ForEach(client => _clients.Remove(client));
		}

		#endregion
	}
}

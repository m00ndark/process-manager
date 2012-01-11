﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ProcessManager.EventArguments;
using ProcessManager.Service.Common;
using ProcessManager.Service.DataObjects;
using ProcessManager.Utilities;

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
			string clientAddress = ((RemoteEndpointMessageProperty) OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
			Logger.Add("Client at " + clientAddress + " registered" + (subscribe ? " as subscriber" : string.Empty));
		}

		public void Unregister()
		{
			IProcessManagerServiceEventHandler caller = OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>();
			_clients.Where(x => (x.Key == caller)).ToList().ForEach(x => _clients.Remove(x));
			string clientAddress = ((RemoteEndpointMessageProperty) OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
			Logger.Add("Client at " + clientAddress + " unregistered");
		}

		#endregion

		#region Implementation of IProcessManagerServiceOperator

		public void Ping()
		{
			// do nothing
		}

		public DTOConfiguration GetConfiguration()
		{
			return new DTOConfiguration(ProcessManager.GetConfiguration());
		}

		public void SetConfiguration(DTOConfiguration configuration)
		{
			ProcessManager.SetConfiguration(configuration.FromDTO());
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
			RemoveFaultedClients(faultedClients);
		}

		#endregion

		private void RemoveFaultedClients(List<IProcessManagerServiceEventHandler> faultedClients)
		{
			faultedClients.ForEach(client => _clients.Remove(client));
			if (faultedClients.Count > 0)
				Logger.Add(LogType.Error, "Removed " + faultedClients.Count + " faulted clients");
		}
	}
}

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
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
	internal class ProcessManagerService : IProcessManagerService
	{
		private readonly IDictionary<IProcessManagerServiceEventHandler, bool> _clients;
		private readonly IDictionary<IProcessManagerServiceEventHandler, InstanceContext> _clientsInstances = new Dictionary<IProcessManagerServiceEventHandler, InstanceContext>();

		public ProcessManagerService()
		{
			_clients = new Dictionary<IProcessManagerServiceEventHandler, bool>();
		}

		#region Implementation of IProcessManagerService

		public void Register(bool subscribe)
		{
			_clients.Add(OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>(), subscribe);
			_clientsInstances.Add(OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>(), OperationContext.Current.InstanceContext);
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
			Logger.Add(LogType.Debug, "Ping call received");
		}

		public DTOConfiguration GetConfiguration()
		{
			Logger.Add(LogType.Debug, "GetConfiguration call received");
			return new DTOConfiguration(ProcessManager.Instance.GetConfiguration());
		}

		public void SetConfiguration(DTOConfiguration configuration)
		{
			Logger.Add(LogType.Debug, "SetConfiguration call received");
			ProcessManager.Instance.SetConfiguration(configuration.FromDTO());
		}

		public List<DTOApplicationStatus> GetAllApplicationStatuses()
		{
			Logger.Add(LogType.Debug, "GetAllApplicationStatuses call received");
			return ProcessManager.Instance.GetAllApplicationStatuses().Select(x => new DTOApplicationStatus(x)).ToList();
		}

		public void TakeApplicationAction(DTOApplicationAction action)
		{
			Logger.Add(LogType.Debug, "TakeApplicationAction call received: action = " + action.Type + ", " + action.GroupID + " / " + action.ApplicationID);
			ProcessManager.Instance.TakeApplicationAction(action.GroupID, action.ApplicationID, action.Type);
		}

		public List<DTOFileSystemDrive> GetFileSystemDrives()
		{
			Logger.Add(LogType.Debug, "GetFileSystemDrives call received");
			return ProcessManager.Instance.GetFileSystemDrives().Select(x => new DTOFileSystemDrive(x)).ToList();
		}

		public List<DTOFileSystemEntry> GetFileSystemEntries(string path)
		{
			Logger.Add(LogType.Debug, "GetFileSystemEntries call received: path = " + path);
			return ProcessManager.Instance.GetFileSystemEntries(path).Select(x => new DTOFileSystemEntry(x)).ToList();
		}

		#endregion

		#region Service event handlers

		public void ProcessManagerEventProvider_ApplicationStatusesChanged(object sender, ApplicationStatusesEventArgs e)
		{
			Logger.Add("ProcessManagerEventProvider_ApplicationStatusesChanged: client count = " + _clients.Count);
			List<IProcessManagerServiceEventHandler> faultedClients = new List<IProcessManagerServiceEventHandler>();
			foreach (IProcessManagerServiceEventHandler client in _clients.Where(x => x.Value).Select(x => x.Key))
			{
				try
				{
					Logger.Add(LogType.Debug, "InstanceContext.State = " + _clientsInstances[client].State);
					Logger.Add(LogType.Debug, "Sending ApplicationStatusesChanged event: thread id = " + System.Threading.Thread.CurrentThread.ManagedThreadId + ", count = " + e.ApplicationStatuses.Count + e.ApplicationStatuses.Aggregate("", (x, y) => x + ", " + y.GroupID + " / " + y.ApplicationID));
					client.ServiceEvent_ApplicationStatusesChanged(e.ApplicationStatuses.Select(x => new DTOApplicationStatus(x)).ToList());
				}
				catch (Exception ex)
				{
					Logger.Add("Failed to send ApplicationStatusesChanged event: thread id = " + System.Threading.Thread.CurrentThread.ManagedThreadId + ", count = " + e.ApplicationStatuses.Count + e.ApplicationStatuses.Aggregate("", (x, y) => x + ", " + y.GroupID + " / " + y.ApplicationID), ex);
					faultedClients.Add(client);
				}
			}
			RemoveFaultedClients(faultedClients);
		}

		public void ProcessManagerEventProvider_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e)
		{
			List<IProcessManagerServiceEventHandler> faultedClients = new List<IProcessManagerServiceEventHandler>();
			foreach (IProcessManagerServiceEventHandler client in _clients.Where(x => x.Value).Select(x => x.Key))
			{
				try
				{
					Logger.Add(LogType.Debug, "Sending ConfigurationChanged event: count = " + e.ConfigurationHash);
					client.ServiceEvent_ConfigurationChanged(e.ConfigurationHash);
				}
				catch (Exception ex)
				{
					Logger.Add("Failed to send ApplicationStatusesChanged event: count = " + e.ConfigurationHash, ex);
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

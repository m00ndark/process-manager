﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ProcessManager.DataObjects;
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
		//private readonly IDictionary<IProcessManagerServiceEventHandler, InstanceContext> _clientsInstances = new Dictionary<IProcessManagerServiceEventHandler, InstanceContext>();

		public ProcessManagerService()
		{
			_clients = new Dictionary<IProcessManagerServiceEventHandler, bool>();
		}

		#region Properties

		private IEnumerable<IProcessManagerServiceEventHandler> SubscribingClients
		{
			get { lock (_clients) return _clients.Where(x => x.Value).Select(x => x.Key).ToList(); }
		}

		#endregion

		#region Implementation of IProcessManagerService

		public void Register(bool subscribe)
		{
			lock (_clients)
				_clients.Add(OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>(), subscribe);

			//_clientsInstances.Add(OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>(), OperationContext.Current.InstanceContext);
			string clientAddress = ((RemoteEndpointMessageProperty) OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
			Logger.Add("Client at " + clientAddress + " registered" + (subscribe ? " as subscriber" : string.Empty));
		}

		public void Unregister()
		{
			IProcessManagerServiceEventHandler caller = OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>();

			lock (_clients)
				_clients.Where(x => (x.Key == caller)).ToList().ForEach(x => _clients.Remove(x));

			string clientAddress = ((RemoteEndpointMessageProperty) OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
			Logger.Add("Client at " + clientAddress + " unregistered");
		}

		#endregion

		#region Implementation of IProcessManagerServiceOperator

		public void Ping()
		{
			Logger.Add(LogType.Verbose, "Ping call received");
			// do nothing
		}

		public DTOConfiguration GetConfiguration()
		{
			Logger.Add(LogType.Verbose, "GetConfiguration call received");
			return new DTOConfiguration(ProcessManager.Instance.GetConfiguration());
		}

		public void SetConfiguration(DTOConfiguration configuration)
		{
			Logger.Add(LogType.Verbose, "SetConfiguration call received");
			ProcessManager.Instance.SetConfiguration(configuration.FromDTO());
		}

		public List<DTOProcessStatus> GetAllProcessStatuses()
		{
			Logger.Add(LogType.Verbose, "GetAllProcessStatuses call received");
			return ProcessManager.Instance.GetAllProcessStatuses().Select(x => new DTOProcessStatus(x)).ToList();
		}

		public DTOProcessActionResult TakeProcessAction(DTOProcessAction processAction)
		{
			Logger.Add(LogType.Verbose, "TakeProcessAction call received: action = " + processAction.Type + ", " + processAction.GroupID + " / " + processAction.ApplicationID);
			ProcessActionResult processActionResult = ProcessManager.Instance.TakeProcessAction(processAction.GroupID, processAction.ApplicationID, processAction.Type);
			return new DTOProcessActionResult(processActionResult);
		}

		public bool TakeDistributionAction(DTODistributionAction distributionAction)
		{
			Logger.Add(LogType.Verbose, "TakeDistributionAction call received: action = " + distributionAction.Type + ", " + distributionAction.SourceMachineHostName + " / " + distributionAction.GroupID + " / " + distributionAction.ApplicationID + " / " + distributionAction.DestinationMachineHostName);
			IProcessManagerServiceEventHandler caller = OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>();
			return ProcessManager.Instance.TakeDistributionAction(distributionAction.SourceMachineHostName, distributionAction.GroupID, distributionAction.ApplicationID, distributionAction.DestinationMachineHostName, distributionAction.Type, caller);
		}

		public List<DTOFileSystemDrive> GetFileSystemDrives()
		{
			Logger.Add(LogType.Verbose, "GetFileSystemDrives call received");
			return ProcessManager.Instance.GetFileSystemDrives().Select(x => new DTOFileSystemDrive(x)).ToList();
		}

		public List<DTOFileSystemEntry> GetFileSystemEntries(string path, string filter)
		{
			Logger.Add(LogType.Verbose, "GetFileSystemEntries call received: path = " + path + ", filter = " + filter);
			return ProcessManager.Instance.GetFileSystemEntries(path, filter).Select(x => new DTOFileSystemEntry(x)).ToList();
		}

		public bool DistributeFile(DTODistributionFile distributionFile)
		{
			Logger.Add(LogType.Verbose, "DistributeFile call received: relative path = " + distributionFile.RelativePath + ", destination group id = " + distributionFile.DestinationGroupID + ", content length = " + distributionFile.Content.Length);
			return ProcessManager.Instance.DistributeFile(distributionFile.FromDTO());
		}

		#endregion

		#region Service event handlers

		public void ProcessManagerEventProvider_ProcessStatusesChanged(object sender, ProcessStatusesEventArgs e)
		{
			List<IProcessManagerServiceEventHandler> faultedClients = new List<IProcessManagerServiceEventHandler>();
			foreach (IProcessManagerServiceEventHandler client in SubscribingClients)
			{
				try
				{
					Logger.Add(LogType.Verbose, "Sending ProcessStatusesChanged event: count = " + e.ProcessStatuses.Count + e.ProcessStatuses.Aggregate("", (x, y) => x + ", " + y.GroupID + " / " + y.ApplicationID));
					client.ServiceEvent_ProcessStatusesChanged(e.ProcessStatuses.Select(x => new DTOProcessStatus(x)).ToList());
				}
				catch (Exception ex)
				{
					Logger.Add("Failed to send ProcessStatusesChanged event: count = " + e.ProcessStatuses.Count + e.ProcessStatuses.Aggregate("", (x, y) => x + ", " + y.GroupID + " / " + y.ApplicationID), ex);
					faultedClients.Add(client);
				}
			}
			RemoveFaultedClients(faultedClients);
		}

		public void ProcessManagerEventProvider_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e)
		{
			List<IProcessManagerServiceEventHandler> faultedClients = new List<IProcessManagerServiceEventHandler>();
			foreach (IProcessManagerServiceEventHandler client in SubscribingClients)
			{
				try
				{
					Logger.Add(LogType.Verbose, "Sending ConfigurationChanged event: hash = " + e.ConfigurationHash);
					client.ServiceEvent_ConfigurationChanged(e.ConfigurationHash);
				}
				catch (Exception ex)
				{
					Logger.Add("Failed to send ConfigurationChanged event: hash = " + e.ConfigurationHash, ex);
					faultedClients.Add(client);
				}
			}
			RemoveFaultedClients(faultedClients);
		}

		public void ProcessManagerEventProvider_DistributionCompleted(object sender, DistributionResultEventArgs e)
		{
			List<IProcessManagerServiceEventHandler> faultedClients = new List<IProcessManagerServiceEventHandler>();
			lock (_clients)
			{
				IProcessManagerServiceEventHandler client = _clients.Keys.FirstOrDefault(x => x == e.Caller);
				if (client != null)
				{
					try
					{
						Logger.Add(LogType.Verbose, "Sending DistributionCompleted event: action = " + e.DistributionResult.Type + ", " + e.DistributionResult.SourceMachineHostName + " / " + e.DistributionResult.GroupID + " / " + e.DistributionResult.ApplicationID + " / " + e.DistributionResult.DestinationMachineHostName);
						client.ServiceEvent_DistributionCompleted(new DTODistributionResult(e.DistributionResult));
					}
					catch (Exception ex)
					{
						Logger.Add("Failed to send DistributionCompleted event: action = " + e.DistributionResult.Type + ", " + e.DistributionResult.SourceMachineHostName + " / " + e.DistributionResult.GroupID + " / " + e.DistributionResult.ApplicationID + " / " + e.DistributionResult.DestinationMachineHostName, ex);
						faultedClients.Add(client);
					}
				}
			}
			RemoveFaultedClients(faultedClients);
		}

		#endregion

		private void RemoveFaultedClients(List<IProcessManagerServiceEventHandler> faultedClients)
		{
			lock (_clients)
				faultedClients.ForEach(client => _clients.Remove(client));

			if (faultedClients.Count > 0)
				Logger.Add(LogType.Error, "Removed " + faultedClients.Count + " faulted clients");
		}
	}
}

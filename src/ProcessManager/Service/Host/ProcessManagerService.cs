using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Common;
using ProcessManager.Service.DataObjects;
using ToolComponents.Core.Logging;

namespace ProcessManager.Service.Host
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
	internal class ProcessManagerService : IProcessManagerService
	{
		private class ConnectedClient
		{
			public ConnectedClient(IProcessManagerServiceEventHandler caller, bool subscribe)
			{
				Id = Guid.NewGuid();
				Caller = caller;
				Subscribe = subscribe;
			}

			public Guid Id { get; }
			public IProcessManagerServiceEventHandler Caller { get; }
			public bool Subscribe { get; }
		}

		private readonly IDictionary<IProcessManagerServiceEventHandler, ConnectedClient> _clients;

		public ProcessManagerService()
		{
			_clients = new Dictionary<IProcessManagerServiceEventHandler, ConnectedClient>();
		}

		#region Properties

		private IEnumerable<ConnectedClient> SubscribingClients
		{
			get { lock (_clients) return _clients.Values.Where(x => x.Subscribe).ToList(); }
		}

		#endregion

		#region Implementation of IProcessManagerService

		public void Register(bool subscribe)
		{
			ConnectedClient client = new ConnectedClient(GetCaller(), subscribe);

			lock (_clients)
				_clients.Add(client.Caller, client);

			string clientAddress = ((RemoteEndpointMessageProperty) OperationContext.Current
				.IncomingMessageProperties[RemoteEndpointMessageProperty.Name])
				.Address;

			Logger.Add($"Client at {clientAddress} registered{(subscribe ? " as subscriber" : string.Empty)}");
		}

		public void Unregister()
		{
			IProcessManagerServiceEventHandler caller = GetCaller();

			lock (_clients)
				_clients.Where(x => x.Key == caller).ToList().ForEach(x => _clients.Remove(x));

			string clientAddress = ((RemoteEndpointMessageProperty) OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
			Logger.Add($"Client at {clientAddress} unregistered");
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

		public void ActivateProcessStatusNotifications()
		{
			Logger.Add(LogType.Verbose, "ActivateProcessStatusNotifications call received");

			ConnectedClient client = GetClient();

			if (client != null)
				ProcessManager.Instance.ActivateProcessStatusNotifications(client.Id);
		}

		public void DeactivateProcessStatusNotifications()
		{
			Logger.Add(LogType.Verbose, "DeactivateProcessStatusNotifications call received");

			ConnectedClient client = GetClient();

			if (client != null)
				ProcessManager.Instance.DeactivateProcessStatusNotifications(client.Id);
		}

		public DTOProcessActionResult TakeProcessAction(DTOProcessAction processAction)
		{
			Logger.Add(LogType.Verbose, $"TakeProcessAction call received: action = {processAction.Type}, {processAction.GroupID} / {processAction.ApplicationID}");
			ProcessActionResult processActionResult = ProcessManager.Instance.TakeProcessAction(processAction.GroupID, processAction.ApplicationID, processAction.Type);
			return new DTOProcessActionResult(processActionResult);
		}

		public DTODistributionActionResult TakeDistributionAction(DTODistributionAction distributionAction)
		{
			Logger.Add(LogType.Verbose, $"TakeDistributionAction call received: action = {distributionAction.Type}," +
				$" {distributionAction.SourceMachineHostName} / {distributionAction.GroupID} / {distributionAction.ApplicationID} / {distributionAction.DestinationMachineHostName}");

			ConnectedClient client = GetClient();

			if (client == null)
				return null;

			DistributionActionResult distributionActionResult = ProcessManager.Instance.TakeDistributionAction(
				distributionAction.SourceMachineHostName,
				distributionAction.GroupID,
				distributionAction.ApplicationID,
				distributionAction.DestinationMachineHostName,
				distributionAction.Type,
				client.Id);

			return new DTODistributionActionResult(distributionActionResult);
		}

		public List<DTOFileSystemDrive> GetFileSystemDrives()
		{
			Logger.Add(LogType.Verbose, "GetFileSystemDrives call received");
			return ProcessManager.Instance.GetFileSystemDrives().Select(x => new DTOFileSystemDrive(x)).ToList();
		}

		public List<DTOFileSystemEntry> GetFileSystemEntries(string path, string filter)
		{
			Logger.Add(LogType.Verbose, $"GetFileSystemEntries call received: path = {path}, filter = {filter}");
			return ProcessManager.Instance.GetFileSystemEntries(path, filter).Select(x => new DTOFileSystemEntry(x)).ToList();
		}

		public DTODistributeFileResult DistributeFile(DTODistributionFile distributionFile)
		{
			Logger.Add(LogType.Verbose, $"DistributeFile call received: relative path = {distributionFile.RelativePath}, destination group id = {distributionFile.DestinationGroupID}, content length = {distributionFile.Content.Length}");
			DistributeFileResult distributeFileResult = ProcessManager.Instance.DistributeFile(distributionFile.FromDTO());
			return new DTODistributeFileResult(distributeFileResult);
		}

		#endregion

		#region Service event handlers

		public void ProcessManagerEventProvider_ProcessStatusesChanged(object sender, ProcessStatusesEventArgs e)
		{
			List<ConnectedClient> faultedClients = new List<ConnectedClient>();
			foreach (ConnectedClient client in e.ClientIds.Select(GetClientById))
			{
				try
				{
					Logger.Add(LogType.Verbose, $"Sending ProcessStatusesChanged event: count = {e.ProcessStatuses.Count}{e.ProcessStatuses.Aggregate("", (x, y) => $"{x}, {y.GroupID} / {y.ApplicationID}")}");
					client.Caller.ServiceEvent_ProcessStatusesChanged(e.ProcessStatuses.Select(x => new DTOProcessStatus(x)).ToList());
				}
				catch (Exception ex)
				{
					Logger.Add($"Failed to send ProcessStatusesChanged event: count = {e.ProcessStatuses.Count}{e.ProcessStatuses.Aggregate("", (x, y) => $"{x}, {y.GroupID} / {y.ApplicationID}")}", ex);
					faultedClients.Add(client);
				}
			}
			RemoveFaultedClients(faultedClients);
		}

		public void ProcessManagerEventProvider_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e)
		{
			List<ConnectedClient> faultedClients = new List<ConnectedClient>();
			foreach (ConnectedClient client in SubscribingClients)
			{
				try
				{
					Logger.Add(LogType.Verbose, $"Sending ConfigurationChanged event: hash = {e.ConfigurationHash}");
					client.Caller.ServiceEvent_ConfigurationChanged(e.ConfigurationHash);
				}
				catch (Exception ex)
				{
					Logger.Add($"Failed to send ConfigurationChanged event: hash = {e.ConfigurationHash}", ex);
					faultedClients.Add(client);
				}
			}
			RemoveFaultedClients(faultedClients);
		}

		public void ProcessManagerEventProvider_DistributionCompleted(object sender, DistributionResultEventArgs e)
		{
			List<ConnectedClient> faultedClients = new List<ConnectedClient>();
			ConnectedClient client = GetClientById(e.ClientId);
			if (client != null)
			{
				try
				{
					Logger.Add(LogType.Verbose, $"Sending DistributionCompleted event: action = {e.DistributionResult.Type}, {e.DistributionResult.SourceMachineHostName} / {e.DistributionResult.GroupID} / {e.DistributionResult.ApplicationID} / {e.DistributionResult.DestinationMachineHostName}");
					client.Caller.ServiceEvent_DistributionCompleted(new DTODistributionResult(e.DistributionResult));
				}
				catch (Exception ex)
				{
					Logger.Add($"Failed to send DistributionCompleted event: action = {e.DistributionResult.Type}, {e.DistributionResult.SourceMachineHostName} / {e.DistributionResult.GroupID} / {e.DistributionResult.ApplicationID} / {e.DistributionResult.DestinationMachineHostName}", ex);
					faultedClients.Add(client);
				}
			}
			RemoveFaultedClients(faultedClients);
		}

		#endregion

		private static IProcessManagerServiceEventHandler GetCaller()
		{
			return OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>();
		}

		private ConnectedClient GetClient()
		{
			lock (_clients)
				return _clients.TryGetValue(GetCaller(), out ConnectedClient client) ? client : null;
		}

		private ConnectedClient GetClientById(Guid clientId)
		{
			lock (_clients)
				return _clients.Values.FirstOrDefault(client => client.Id == clientId);
		}

		private void RemoveFaultedClients(List<ConnectedClient> faultedClients)
		{
			lock (_clients)
				faultedClients.ForEach(client => _clients.Remove(client.Caller));

			if (faultedClients.Count > 0)
				Logger.Add(LogType.Error, $"Removed {faultedClients.Count} faulted clients");
		}
	}
}

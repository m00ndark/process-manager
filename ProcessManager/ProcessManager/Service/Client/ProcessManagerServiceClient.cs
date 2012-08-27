using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ProcessManager.Service.Common;
using ProcessManager.Service.DataObjects;

namespace ProcessManager.Service.Client
{
	public class ProcessManagerServiceClient : DuplexClientBase<IProcessManagerService>, IProcessManagerService
	{
		public ProcessManagerServiceClient(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
			: base(callbackInstance, binding, remoteAddress) {}

		#region Implementation of IProcessManagerService

		public void Register(bool subscribe)
		{
			Channel.Register(subscribe);
		}

		public void Unregister()
		{
			Channel.Unregister();
		}

		#endregion

		#region Implementation of IProcessManagerServiceOperator

		public void Ping()
		{
			Channel.Ping();
		}

		public DTOConfiguration GetConfiguration()
		{
			return Channel.GetConfiguration();
		}

		public void SetConfiguration(DTOConfiguration configuration)
		{
			Channel.SetConfiguration(configuration);
		}

		public List<DTOApplicationStatus> GetAllApplicationStatuses()
		{
			return Channel.GetAllApplicationStatuses();
		}

		public void TakeApplicationAction(DTOApplicationAction action)
		{
			Channel.TakeApplicationAction(action);
		}

		public List<DTOFileSystemDrive> GetFileSystemDrives()
		{
			return Channel.GetFileSystemDrives();
		}

		public List<DTOFileSystemEntry> GetFileSystemEntries(string path, string filter = null)
		{
			return Channel.GetFileSystemEntries(path, filter);
		}

		#endregion
	}
}

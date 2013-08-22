using System.Collections.Generic;
using System.ServiceModel;
using ProcessManager.Service.DataObjects;

namespace ProcessManager.Service.Common
{
	[ServiceContract]
	public interface IProcessManagerServiceOperator
	{
		[OperationContract]
		void Ping();

		[OperationContract]
		DTOConfiguration GetConfiguration();

		[OperationContract]
		void SetConfiguration(DTOConfiguration configuration);

		[OperationContract]
		List<DTOProcessStatus> GetAllProcessStatuses();

		[OperationContract]
		void TakeProcessAction(DTOProcessAction processAction);

		[OperationContract]
		List<DTOFileSystemDrive> GetFileSystemDrives();

		[OperationContract]
		List<DTOFileSystemEntry> GetFileSystemEntries(string path, string filter = null);
	}
}

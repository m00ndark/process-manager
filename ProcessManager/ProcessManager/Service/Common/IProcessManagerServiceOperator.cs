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
		List<DTOApplicationStatus> GetAllApplicationStatuses();

		[OperationContract]
		void TakeApplicationAction(DTOApplicationAction action);

		[OperationContract]
		List<DTOFileSystemDrive> GetFileSystemDrives();

		[OperationContract]
		List<DTOFileSystemEntry> GetFileSystemEntries(string path);
	}
}

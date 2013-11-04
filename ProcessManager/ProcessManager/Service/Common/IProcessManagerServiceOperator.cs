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
		DTOProcessActionResult TakeProcessAction(DTOProcessAction processAction);

		[OperationContract]
		DTODistributionActionResult TakeDistributionAction(DTODistributionAction distributionAction);

		[OperationContract]
		List<DTOFileSystemDrive> GetFileSystemDrives();

		[OperationContract]
		List<DTOFileSystemEntry> GetFileSystemEntries(string path, string filter = null);

		[OperationContract]
		DTODistributeFileResult DistributeFile(DTODistributionFile distributionFile);
	}
}

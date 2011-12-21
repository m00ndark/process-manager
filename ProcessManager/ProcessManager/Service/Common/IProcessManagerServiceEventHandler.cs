using System.Collections.Generic;
using System.ServiceModel;
using ProcessManager.Service.DataObjects;

namespace ProcessManager.Service.Common
{
	public interface IProcessManagerServiceEventHandler
	{
		[OperationContract(IsOneWay = true)]
		void ServiceEvent_ApplicationStatusesChanged(List<DTOApplicationStatus> applicationStatuses);
	}
}

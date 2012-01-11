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
	}
}

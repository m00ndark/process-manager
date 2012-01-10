using System.ServiceModel;

namespace ProcessManager.Service.Common
{
	[ServiceContract]
	public interface IProcessManagerServiceOperator
	{
		[OperationContract]
		void Ping();
	}
}

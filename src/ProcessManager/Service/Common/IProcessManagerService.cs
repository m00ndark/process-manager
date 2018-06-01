using System.ServiceModel;

namespace ProcessManager.Service.Common
{
	[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IProcessManagerServiceEventHandler))]
	public interface IProcessManagerService : IProcessManagerServiceOperator
	{
		[OperationContract(IsInitiating = true)]
		void Register(bool subscribe);

		[OperationContract(IsTerminating = true)]
		void Unregister();
	}
}

using System.ServiceModel;
using System.ServiceModel.Channels;
using ProcessManager.Service.Common;

namespace ProcessManager.Service.Client
{
	public class ProcessManagerServiceClient : DuplexClientBase<IProcessManagerService>, IProcessManagerService
	{
		public ProcessManagerServiceClient(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
			: base(callbackInstance, binding, remoteAddress) {}

		#region Implementation of IProcessManagerService

		public void Subscribe()
		{
			Channel.Subscribe();
		}

		public void Unsubscribe()
		{
			Channel.Unsubscribe();
		}

		#endregion
	}
}

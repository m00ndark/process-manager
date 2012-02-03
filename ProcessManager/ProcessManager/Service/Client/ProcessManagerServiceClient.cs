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

		public void TakeApplicationAction(DTOApplicationAction action)
		{
			Channel.TakeApplicationAction(action);
		}

		#endregion
	}
}

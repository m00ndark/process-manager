using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ProcessManager.EventArguments;
using ProcessManager.Service.Common;
using ProcessManager.Service.DataObjects;

namespace ProcessManager.Service.Host
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	internal class ProcessManagerService : IProcessManagerService
	{
		private readonly List<IProcessManagerServiceEventHandler> _subscribers;

		public ProcessManagerService()
		{
			_subscribers = new List<IProcessManagerServiceEventHandler>();
		}

		#region Implementation of IProcessManagerService

		public void Subscribe()
		{
			_subscribers.Add(OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>());
		}

		public void Unsubscribe()
		{
			IProcessManagerServiceEventHandler caller = OperationContext.Current.GetCallbackChannel<IProcessManagerServiceEventHandler>();
			_subscribers.RemoveAll(subscriber => (subscriber == caller));
		}

		#endregion

		public void ProcessManagerEventProvider_ApplicationStatusesChanged(object sender, ApplicationStatusesEventArgs e)
		{
			List<IProcessManagerServiceEventHandler> faultedSubscribers = new List<IProcessManagerServiceEventHandler>();
			foreach (IProcessManagerServiceEventHandler subscriber in _subscribers)
			{
				try
				{
					subscriber.ServiceEvent_ApplicationStatusesChanged(e.ApplicationStatuses.Select(x => new DTOApplicationStatus(x)).ToList());
				}
				catch
				{
					faultedSubscribers.Add(subscriber);
				}
			}
			faultedSubscribers.ForEach(subscriber => _subscribers.Remove(subscriber));
		}
	}
}

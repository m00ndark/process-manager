using System;
using System.Collections.Generic;
using System.Linq;
using ProcessManager.EventArguments;
using ProcessManager.Service.Common;
using ProcessManager.Service.DataObjects;

namespace ProcessManager.Service.Client
{
	public class ProcessManagerServiceEventHandler : IProcessManagerServiceEventHandler
	{
		#region Events

		public event EventHandler<ApplicationStatusesEventArgs> ApplicationStatusesChanged;

		#endregion

		#region Event raisers

		private void RaiseApplicationStatusesChangedEvent(IEnumerable<DTOApplicationStatus> applicationStatuses)
		{
			if (ApplicationStatusesChanged != null)
				ApplicationStatusesChanged(this, new ApplicationStatusesEventArgs(applicationStatuses.Select(x => x.FromDTO()).ToList()));
		}

		#endregion

		#region Implementation of IProcessManagerServiceEventHandler

		public void ServiceEvent_ApplicationStatusesChanged(List<DTOApplicationStatus> applicationStatuses)
		{
			RaiseApplicationStatusesChangedEvent(applicationStatuses);
		}

		#endregion
	}
}

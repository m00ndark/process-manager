using System;
using System.Collections.Generic;
using ProcessManager.DataObjects;

namespace ProcessManager.EventArguments
{
	public class ApplicationStatusesEventArgs : EventArgs
	{
		public ApplicationStatusesEventArgs(List<ApplicationStatus> applicationStatuses)
		{
			ApplicationStatuses = applicationStatuses;
		}

		public List<ApplicationStatus> ApplicationStatuses { get; private set; }
	}
}

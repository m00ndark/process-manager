using System;
using System.Collections.Generic;
using ProcessManager.DataObjects;

namespace ProcessManager.EventArguments
{
	public class ProcessStatusesEventArgs : EventArgs
	{
		public ProcessStatusesEventArgs(List<ProcessStatus> processStatuses, Guid[] clientIds = null)
		{
			ProcessStatuses = processStatuses;
			ClientIds = clientIds;
		}

		public List<ProcessStatus> ProcessStatuses { get; private set; }
		public Guid[] ClientIds { get; }
	}
}

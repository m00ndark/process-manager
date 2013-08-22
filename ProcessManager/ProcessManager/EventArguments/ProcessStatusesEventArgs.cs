using System;
using System.Collections.Generic;
using ProcessManager.DataObjects;

namespace ProcessManager.EventArguments
{
	public class ProcessStatusesEventArgs : EventArgs
	{
		public ProcessStatusesEventArgs(List<ProcessStatus> processStatuses)
		{
			ProcessStatuses = processStatuses;
		}

		public List<ProcessStatus> ProcessStatuses { get; private set; }
	}
}

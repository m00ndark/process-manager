using System;
using System.Collections.Generic;
using ProcessManager.DataObjects;

namespace ProcessManager.EventArguments
{
	public class MachinesEventArgs : EventArgs
	{
		public MachinesEventArgs(List<Machine> machines)
		{
			Machines = machines;
		}

		#region Properties

		public List<Machine> Machines { get; private set; }

		#endregion
	}
}

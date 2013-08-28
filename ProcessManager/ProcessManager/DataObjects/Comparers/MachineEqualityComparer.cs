using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class MachineEqualityComparer : IEqualityComparer<Machine>
	{
		public bool Equals(Machine machine, string hostName)
		{
			return (machine != null && machine.HostName != null && hostName != null && machine.HostName.Equals(hostName, StringComparison.CurrentCultureIgnoreCase));
		}

		public bool Equals(Machine x, Machine y)
		{
			return (y != null && Equals(x, y.HostName));
		}

		public int GetHashCode(Machine obj)
		{
			return (obj != null && obj.HostName != null ? obj.HostName.ToLower().GetHashCode() : 0);
		}
	}
}

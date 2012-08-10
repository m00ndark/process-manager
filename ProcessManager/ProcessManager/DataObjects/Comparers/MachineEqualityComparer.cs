using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class MachineEqualityComparer : IEqualityComparer<Machine>
	{
		public bool Equals(Machine x, Machine y)
		{
			return (x != null && y != null && x.HostName.Equals(y.HostName, StringComparison.CurrentCultureIgnoreCase));
		}

		public int GetHashCode(Machine obj)
		{
			return (obj != null && obj.HostName != null ? obj.HostName.ToLower().GetHashCode() : 0);
		}
	}
}

using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class ApplicationEqualityComparer : IEqualityComparer<Application>
	{
		public bool Equals(Application x, Application y)
		{
			return (x != null && y != null && x.Name.Equals(y.Name, StringComparison.CurrentCultureIgnoreCase));
		}

		public int GetHashCode(Application obj)
		{
			return (obj != null && obj.Name != null ? obj.Name.ToLower().GetHashCode() : 0);
		}
	}
}

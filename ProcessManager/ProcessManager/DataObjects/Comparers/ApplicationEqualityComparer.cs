using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class ApplicationEqualityComparer : IEqualityComparer<Application>
	{
		public bool Equals(Application application, string name)
		{
			return (application?.Name != null && name != null && application.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
		}

		public bool Equals(Application x, Application y)
		{
			return (y != null && Equals(x, y.Name));
		}

		public int GetHashCode(Application obj)
		{
			return obj?.Name?.ToLower().GetHashCode() ?? 0;
		}
	}
}

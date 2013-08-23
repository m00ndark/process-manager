using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class ApplicationEqualityComparer : IEqualityComparer<Application>
	{
		public bool Equals(Application application, string applicationName)
		{
			return (application != null && application.Name.Equals(applicationName, StringComparison.CurrentCultureIgnoreCase));
		}

		public bool Equals(Application x, Application y)
		{
			return (y != null && Equals(x, y.Name));
		}

		public int GetHashCode(Application obj)
		{
			return (obj != null && obj.Name != null ? obj.Name.ToLower().GetHashCode() : 0);
		}
	}
}

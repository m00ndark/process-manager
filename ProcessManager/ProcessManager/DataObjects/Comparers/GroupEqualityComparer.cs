using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class GroupEqualityComparer : IEqualityComparer<Group>
	{
		public bool Equals(Group x, Group y)
		{
			return (x != null && y != null && x.Name.Equals(y.Name, StringComparison.CurrentCultureIgnoreCase));
		}

		public int GetHashCode(Group obj)
		{
			return (obj != null && obj.Name != null ? obj.Name.ToLower().GetHashCode() : 0);
		}
	}
}

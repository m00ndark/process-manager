using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class GroupEqualityComparer : IEqualityComparer<Group>
	{
		public bool Equals(Group group, string name)
		{
			return (group != null && group.Name != null && name != null && group.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
		}

		public bool Equals(Group x, Group y)
		{
			return (y != null && Equals(x, y.Name));
		}

		public int GetHashCode(Group obj)
		{
			return (obj != null && obj.Name != null ? obj.Name.ToLower().GetHashCode() : 0);
		}
	}
}

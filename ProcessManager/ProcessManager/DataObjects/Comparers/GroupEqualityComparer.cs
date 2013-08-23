using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class GroupEqualityComparer : IEqualityComparer<Group>
	{
		public bool Equals(Group group, string groupName)
		{
			return (group != null && group.Name.Equals(groupName, StringComparison.CurrentCultureIgnoreCase));
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

using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class IDObjectEqualityComparer<T> : IEqualityComparer<T> where T : class, IIDObject
	{
		public bool Equals(T x, T y)
		{
			return x != null && y != null && x.ID == y.ID;
		}

		public int GetHashCode(T obj)
		{
			return obj.ID.ToString().GetHashCode();
		}
	}
}

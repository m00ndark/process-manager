using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class IDObjectEqualityComparer<T> : IEqualityComparer<T> where T : class, IIDObject
	{
		public bool Equals(T x, Guid id)
		{
			return x != null && x.ID == id;
		}

		public bool Equals(T x, T y)
		{
			return y != null && Equals(x, y.ID);
		}

		public int GetHashCode(T obj)
		{
			return obj.ID.ToString().GetHashCode();
		}
	}
}

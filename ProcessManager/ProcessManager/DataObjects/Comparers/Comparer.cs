using System;

namespace ProcessManager.DataObjects.Comparers
{
	public static class Comparer
	{
		private static readonly MachineEqualityComparer _machineEqualityComparer = new MachineEqualityComparer();
		private static readonly GroupEqualityComparer _groupEqualityComparer = new GroupEqualityComparer();
		private static readonly ApplicationEqualityComparer _applicationEqualityComparer = new ApplicationEqualityComparer();
		private static readonly DistributionFileEqualityComparer _distributionFileEqualityComparer = new DistributionFileEqualityComparer();
		private static readonly MacroEqualityComparer _macroEqualityComparer = new MacroEqualityComparer();

		public static bool MachinesEqual(Machine x, Machine y)
		{
			return _machineEqualityComparer.Equals(x, y);
		}

		public static bool MachinesEqual(Machine x, string hostName)
		{
			return _machineEqualityComparer.Equals(x, hostName);
		}

		public static int GetHashCode(Machine x)
		{
			return _machineEqualityComparer.GetHashCode(x);
		}

		public static bool GroupsEqual(Group x, Group y)
		{
			return _groupEqualityComparer.Equals(x, y);
		}

		public static bool GroupsEqual(Group x, string name)
		{
			return _groupEqualityComparer.Equals(x, name);
		}

		public static int GetHashCode(Group x)
		{
			return _groupEqualityComparer.GetHashCode(x);
		}

		public static bool ApplicationsEqual(Application x, Application y)
		{
			return _applicationEqualityComparer.Equals(x, y);
		}

		public static bool ApplicationsEqual(Application x, string name)
		{
			return _applicationEqualityComparer.Equals(x, name);
		}

		public static int GetHashCode(Application x)
		{
			return _applicationEqualityComparer.GetHashCode(x);
		}

		public static bool DistributionFilesEqual(DistributionFile x, DistributionFile y)
		{
			return _distributionFileEqualityComparer.Equals(x, y);
		}

		public static int GetHashCode(DistributionFile x)
		{
			return _distributionFileEqualityComparer.GetHashCode(x);
		}

		public static bool MacrosEqual(Macro x, Macro y)
		{
			return _macroEqualityComparer.Equals(x, y);
		}

		public static bool MacrosEqual(Macro x, string name)
		{
			return _macroEqualityComparer.Equals(x, name);
		}

		public static int GetHashCode(Macro x)
		{
			return _macroEqualityComparer.GetHashCode(x);
		}
	}

	public static class Comparer<T> where T : class, IIDObject
	{
		private static readonly IDObjectEqualityComparer<T> _idObjectEqualityComparer = new IDObjectEqualityComparer<T>();

		public static bool IDObjectsEqual(T x, Guid id)
		{
			return _idObjectEqualityComparer.Equals(x, id);
		}

		public static bool IDObjectsEqual(T x, T y)
		{
			return _idObjectEqualityComparer.Equals(x, y);
		}
	}
}

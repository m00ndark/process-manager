using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class MacroEqualityComparer : IEqualityComparer<Macro>
	{
		public bool Equals(Macro macro, string name)
		{
			return (macro != null && macro.Name != null && name != null && macro.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
		}

		public bool Equals(Macro x, Macro y)
		{
			return (y != null && Equals(x, y.Name));
		}

		public int GetHashCode(Macro obj)
		{
			return (obj != null && obj.Name != null ? obj.Name.ToLower().GetHashCode() : 0);
		}
	}
}

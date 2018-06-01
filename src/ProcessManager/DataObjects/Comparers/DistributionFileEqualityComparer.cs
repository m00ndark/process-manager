using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects.Comparers
{
	public class DistributionFileEqualityComparer : IEqualityComparer<DistributionFile>
	{
		public bool Equals(DistributionFile x, DistributionFile y)
		{
			return x != null && y != null && x.RelativePath.Equals(y.RelativePath, StringComparison.CurrentCultureIgnoreCase);
		}

		public int GetHashCode(DistributionFile obj)
		{
			return obj?.RelativePath?.ToLower().GetHashCode() ?? 0;
		}
	}
}

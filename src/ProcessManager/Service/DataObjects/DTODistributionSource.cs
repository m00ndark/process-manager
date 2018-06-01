using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTODistributionSource
	{
		public DTODistributionSource(DistributionSource distributionSource)
		{
			ID = distributionSource.ID;
			Path = distributionSource.Path;
			Filter = distributionSource.Filter;
			Recursive = distributionSource.Recursive;
			Inclusive = distributionSource.Inclusive;
		}

		#region Properties

		[DataMember]
		public Guid ID { get; set; }

		[DataMember]
		public string Path { get; set; }

		[DataMember]
		public string Filter { get; set; }

		[DataMember]
		public bool Recursive { get; set; }

		[DataMember]
		public bool Inclusive { get; set; }

		#endregion

		public DistributionSource FromDTO()
		{
			return new DistributionSource() { ID = ID, Path = Path, Filter = Filter, Recursive = Recursive, Inclusive = Inclusive };
		}
	}
}

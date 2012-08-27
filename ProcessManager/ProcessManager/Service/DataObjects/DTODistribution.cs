using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTODistribution
	{
		public DTODistribution(Distribution distribution)
		{
			ID = distribution.ID;
			Name = distribution.Name;
			Sources = new List<DTODistributionSource>();
			Sources.AddRange(distribution.Sources.Select(source => new DTODistributionSource(source)));
			DestinationPath = distribution.DestinationPath;
		}

		#region Properties

		[DataMember]
		public Guid ID { get; private set; }

		[DataMember]
		public string Name { get; private set; }

		[DataMember]
		public List<DTODistributionSource> Sources { get; private set; }

		[DataMember]
		public string DestinationPath { get; private set; }

		#endregion

		public Distribution FromDTO()
		{
			Distribution distribution = new Distribution() { ID = ID, Name = Name, DestinationPath = DestinationPath };
			distribution.Sources.AddRange(Sources.Select(source => source.FromDTO()));
			return distribution;
		}
	}
}

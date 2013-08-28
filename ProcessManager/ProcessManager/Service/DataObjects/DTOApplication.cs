using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOApplication
	{
		public DTOApplication(Application application)
		{
			ID = application.ID;
			Name = application.Name;
			RelativePath = application.RelativePath;
			Arguments = application.Arguments;
			DistributionOnly = application.DistributionOnly;
			Sources = new List<DTODistributionSource>();
			Sources.AddRange(application.Sources.Select(source => new DTODistributionSource(source)));
		}

		#region Properties

		[DataMember]
		public Guid ID { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string RelativePath { get; set; }

		[DataMember]
		public string Arguments { get; set; }

		[DataMember]
		public bool DistributionOnly { get; set; }

		[DataMember]
		public List<DTODistributionSource> Sources { get; private set; }

		#endregion

		public Application FromDTO()
		{
			Application application = new Application() { ID = ID, Name = Name, RelativePath = RelativePath, Arguments = Arguments, DistributionOnly = DistributionOnly };
			application.Sources.AddRange(Sources.Select(source => source.FromDTO()));
			return application;
		}
	}
}

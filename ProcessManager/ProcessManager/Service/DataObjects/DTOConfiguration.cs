using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOConfiguration
	{
		public DTOConfiguration(Configuration configuration)
		{
			Hash = configuration.Hash;
			Groups = new List<DTOGroup>();
			Applications = new List<DTOApplication>();
			Distributions = new List<DTODistribution>();
			Groups.AddRange(configuration.Groups.Select(group => new DTOGroup(group)));
			Applications.AddRange(configuration.Applications.Select(application => new DTOApplication(application)));
			Distributions.AddRange(configuration.Distributions.Select(distribution => new DTODistribution(distribution)));
		}

		#region Properties

		[DataMember]
		public string Hash { get; private set; }

		[DataMember]
		public List<DTOGroup> Groups { get; private set; }

		[DataMember]
		public List<DTOApplication> Applications { get; private set; }

		[DataMember]
		public List<DTODistribution> Distributions { get; private set; }

		#endregion

		public Configuration FromDTO()
		{
			Configuration configuration = new Configuration(Hash);
			configuration.Groups.AddRange(Groups.Select(group => group.FromDTO()));
			configuration.Applications.AddRange(Applications.Select(application => application.FromDTO()));
			configuration.Distributions.AddRange(Distributions.Select(distribution => distribution.FromDTO()));
			return configuration;
		}
	}
}

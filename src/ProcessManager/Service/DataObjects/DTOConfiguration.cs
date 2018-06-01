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
			Groups.AddRange(configuration.Groups.Select(group => new DTOGroup(group)));
			Applications.AddRange(configuration.Applications.Select(application => new DTOApplication(application)));
		}

		#region Properties

		[DataMember]
		public string Hash { get; private set; }

		[DataMember]
		public List<DTOGroup> Groups { get; private set; }

		[DataMember]
		public List<DTOApplication> Applications { get; private set; }

		#endregion

		public Configuration FromDTO()
		{
			Configuration configuration = new Configuration(Hash);
			configuration.Groups.AddRange(Groups.Select(group => group.FromDTO()));
			configuration.Applications.AddRange(Applications.Select(application => application.FromDTO()));
			return configuration;
		}
	}
}

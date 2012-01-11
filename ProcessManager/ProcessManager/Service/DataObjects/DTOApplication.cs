using System;
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

		#endregion

		public Application FromDTO()
		{
			return new Application() { ID = ID, Name = Name, RelativePath = RelativePath, Arguments = Arguments };
		}
	}
}

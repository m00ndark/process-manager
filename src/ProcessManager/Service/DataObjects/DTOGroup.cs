using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOGroup
	{
		public DTOGroup(Group group)
		{
			ID = group.ID;
			Name = group.Name;
			Path = group.Path;
			Applications = group.Applications.ToList();
		}

		#region Properties

		[DataMember]
		public Guid ID { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Path { get; set; }

		[DataMember]
		public List<Guid> Applications { get; private set; }

		#endregion

		public Group FromDTO()
		{
			Group group = new Group() { ID = ID, Name = Name, Path = Path };
			group.Applications.AddRange(Applications);
			return group;
		}
	}
}

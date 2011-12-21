using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOApplicationStatus
	{
		public DTOApplicationStatus(ApplicationStatus applicationStatus)
		{
			GroupID = applicationStatus.GroupID;
			ApplicationID = applicationStatus.ApplicationID;
			IsRunning = applicationStatus.IsRunning;
		}

		#region Properties

		[DataMember]
		public Guid GroupID { get; private set; }

		[DataMember]
		public Guid ApplicationID { get; private set; }

		[DataMember]
		public bool IsRunning { get; private set; }

		#endregion

		public ApplicationStatus FromDTO()
		{
			return new ApplicationStatus(GroupID, ApplicationID, IsRunning);
		}
	}
}

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
			Status = applicationStatus.Status;
		}

		#region Properties

		[DataMember]
		public Guid GroupID { get; private set; }

		[DataMember]
		public Guid ApplicationID { get; private set; }

		[DataMember]
		public ApplicationStatusValue Status { get; private set; }

		#endregion

		public ApplicationStatus FromDTO(Machine machine)
		{
			return new ApplicationStatus(machine, GroupID, ApplicationID, Status);
		}
	}
}

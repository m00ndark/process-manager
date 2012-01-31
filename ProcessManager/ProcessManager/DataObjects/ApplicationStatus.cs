using System;

namespace ProcessManager.DataObjects
{
	public enum ApplicationStatusValue
	{
		Unknown,
		Stopped,
		Running
	}

	public class ApplicationStatus
	{
		public ApplicationStatus(Guid groupID, Guid applicationID, ApplicationStatusValue status) : this(null, groupID, applicationID, status) { }

		public ApplicationStatus(Machine machine, Guid groupID, Guid applicationID, ApplicationStatusValue status)
		{
			Machine = machine;
			GroupID = groupID;
			ApplicationID = applicationID;
			Status = status;
		}

		#region Properties

		public Machine Machine { get; private set; }
		public Guid GroupID { get; private set; }
		public Guid ApplicationID { get; private set; }
		public ApplicationStatusValue Status { get; private set; }

		#endregion
	}
}

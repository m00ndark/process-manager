using System;

namespace ProcessManager.DataObjects
{
	public class ApplicationStatus
	{
		public ApplicationStatus(Guid groupID, Guid applicationID, bool isRunning)
		{
			GroupID = groupID;
			ApplicationID = applicationID;
			IsRunning = isRunning;
		}

		#region Properties

		public Guid GroupID { get; set; }
		public Guid ApplicationID { get; set; }
		public bool IsRunning { get; set; }

		#endregion
	}
}

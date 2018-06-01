using System;

namespace ProcessManager.DataObjects
{
	public enum ProcessStatusValue
	{
		Unknown,
		Stopped,
		Running,
		ActionError
	}

	public class ProcessStatus
	{
		public ProcessStatus(Guid groupID, Guid applicationID, ProcessStatusValue status) : this(null, groupID, applicationID, status) { }

		public ProcessStatus(Machine machine, Guid groupID, Guid applicationID, ProcessStatusValue value)
		{
			Machine = machine;
			GroupID = groupID;
			ApplicationID = applicationID;
			Value = value;
		}

		#region Properties

		public Machine Machine { get; private set; }
		public Guid GroupID { get; private set; }
		public Guid ApplicationID { get; private set; }
		public ProcessStatusValue Value { get; private set; }

		#endregion
	}
}

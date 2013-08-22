using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOProcessStatus
	{
		public DTOProcessStatus(ProcessStatus processStatus)
		{
			GroupID = processStatus.GroupID;
			ApplicationID = processStatus.ApplicationID;
			Value = processStatus.Value;
		}

		#region Properties

		[DataMember]
		public Guid GroupID { get; private set; }

		[DataMember]
		public Guid ApplicationID { get; private set; }

		[DataMember]
		public ProcessStatusValue Value { get; private set; }

		#endregion

		public ProcessStatus FromDTO(Machine machine)
		{
			return new ProcessStatus(machine, GroupID, ApplicationID, Value);
		}
	}
}

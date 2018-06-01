using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOProcessAction
	{
		public DTOProcessAction(ProcessAction processAction)
		{
			GroupID = processAction.Group.ID;
			ApplicationID = processAction.Application.ID;
			Type = processAction.Type;
		}

		#region Properties

		[DataMember]
		public Guid GroupID { get; private set; }

		[DataMember]
		public Guid ApplicationID { get; private set; }

		[DataMember]
		public ActionType Type { get; private set; }

		#endregion
	}
}

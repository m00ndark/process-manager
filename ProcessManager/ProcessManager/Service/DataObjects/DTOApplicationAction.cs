using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOApplicationAction
	{
		public DTOApplicationAction(ApplicationAction action)
		{
			GroupID = action.Group.ID;
			ApplicationID = action.Application.ID;
			Type = action.Type;
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

using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTODistributionAction
	{
		public DTODistributionAction(DistributionAction distributionAction)
		{
			GroupID = distributionAction.Group.ID;
			ApplicationID = distributionAction.Application.ID;
			DestinationMachineHostName = distributionAction.DestinationMachine.HostName;
			Type = distributionAction.Type;
		}

		#region Properties

		[DataMember]
		public Guid GroupID { get; private set; }

		[DataMember]
		public Guid ApplicationID { get; private set; }

		[DataMember]
		public string DestinationMachineHostName { get; private set; }

		[DataMember]
		public ActionType Type { get; private set; }

		#endregion
	}
}

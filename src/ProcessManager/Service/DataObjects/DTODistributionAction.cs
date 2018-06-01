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
			Type = distributionAction.Type;
			SourceMachineHostName = distributionAction.SourceMachine.HostName;
			GroupID = distributionAction.Group.ID;
			ApplicationID = distributionAction.Application.ID;
			DestinationMachineHostName = distributionAction.DestinationMachine.HostName;
		}

		#region Properties

		[DataMember]
		public ActionType Type { get; private set; }

		[DataMember]
		public string SourceMachineHostName { get; private set; }

		[DataMember]
		public Guid GroupID { get; private set; }

		[DataMember]
		public Guid ApplicationID { get; private set; }

		[DataMember]
		public string DestinationMachineHostName { get; private set; }

		#endregion
	}
}

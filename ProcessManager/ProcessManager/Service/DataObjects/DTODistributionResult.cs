using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTODistributionResult
	{
		public DTODistributionResult(DistributionResult distributionResult)
		{
			Type = distributionResult.Type;
			SourceMachineHostName = distributionResult.SourceMachineHostName;
			GroupID = distributionResult.GroupID;
			ApplicationID = distributionResult.ApplicationID;
			DestinationMachineHostName = distributionResult.DestinationMachineHostName;
			Result = distributionResult.Result;
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

		[DataMember]
		public DistributionResultValue Result { get; private set; }

		#endregion

		public DistributionResult FromDTO()
		{
			return new DistributionResult(Type, SourceMachineHostName, GroupID, ApplicationID, DestinationMachineHostName, Result);
		}
	}
}

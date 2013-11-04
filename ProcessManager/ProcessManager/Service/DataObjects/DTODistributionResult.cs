using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTODistributionResult
	{
		public DTODistributionResult(DistributionResult distributionActionResult)
		{
			Type = distributionActionResult.Type;
			SourceMachineHostName = distributionActionResult.SourceMachineHostName;
			GroupID = distributionActionResult.GroupID;
			ApplicationID = distributionActionResult.ApplicationID;
			DestinationMachineHostName = distributionActionResult.DestinationMachineHostName;
			ErrorMessage = distributionActionResult.ErrorMessage;
			Result = distributionActionResult.Result;
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
		public string ErrorMessage { get; private set; }

		[DataMember]
		public DistributionResultValue Result { get; private set; }

		#endregion

		public DistributionResult FromDTO()
		{
			return new DistributionResult(Type, SourceMachineHostName, GroupID, ApplicationID, DestinationMachineHostName, Result, ErrorMessage);
		}
	}
}

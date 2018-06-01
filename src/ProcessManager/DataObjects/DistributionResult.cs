using System;

namespace ProcessManager.DataObjects
{
	public enum DistributionResultValue
	{
		Success,
		Failure
	}

	public class DistributionResult
	{
		public DistributionResult(ActionType type, string sourceMachineHostName, Guid groupID, Guid applicationID,
			string destinationMachineHostName, DistributionResultValue result, string errorMessage)
		{
			Type = type;
			SourceMachineHostName = sourceMachineHostName;
			GroupID = groupID;
			ApplicationID = applicationID;
			DestinationMachineHostName = destinationMachineHostName;
			ErrorMessage = errorMessage;
			Result = result;
		}

		public DistributionResult(DistributionWork distributionWork, DistributionResultValue result, string errorMessage = null)
			: this(distributionWork.Type, distributionWork.SourceMachine.HostName, distributionWork.Group.ID, distributionWork.Application.ID,
				distributionWork.DestinationMachine.HostName, result, errorMessage)
		{}

		#region Properties

		public ActionType Type { get; private set; }
		public string SourceMachineHostName { get; set; }
		public Guid GroupID { get; private set; }
		public Guid ApplicationID { get; private set; }
		public string DestinationMachineHostName { get; private set; }
		public string ErrorMessage { get; private set; }
		public DistributionResultValue Result { get; private set; }

		#endregion
	}
}

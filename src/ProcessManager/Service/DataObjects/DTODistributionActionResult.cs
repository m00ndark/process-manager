using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTODistributionActionResult
	{
		public DTODistributionActionResult(DistributionActionResult distributionActionResult)
		{
			Type = distributionActionResult.Type;
			ErrorMessage = distributionActionResult.ErrorMessage;
		}

		#region Properties

		[DataMember]
		public ActionType Type { get; private set; }

		[DataMember]
		public string ErrorMessage { get; private set; }

		#endregion

		public DistributionActionResult FromDTO()
		{
			return new DistributionActionResult(Type, ErrorMessage);
		}
	}
}

using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTODistributeFileResult
	{
		public DTODistributeFileResult(DistributeFileResult distributeFileResult)
		{
			RelativePath = distributeFileResult.RelativePath;
			DestinationGroupID = distributeFileResult.DestinationGroupID;
			ErrorMessage = distributeFileResult.ErrorMessage;
		}

		#region Properties

		[DataMember]
		public string RelativePath { get; private set; }

		[DataMember]
		public Guid DestinationGroupID { get; private set; }

		[DataMember]
		public string ErrorMessage { get; private set; }

		#endregion

		public DistributeFileResult FromDTO()
		{
			return new DistributeFileResult(RelativePath, DestinationGroupID, ErrorMessage);
		}
	}
}

using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTODistributionFile
	{
		public DTODistributionFile(DistributionFile distributionFile)
		{
			RelativePath = distributionFile.RelativePath;
			DestinationGroupID = distributionFile.DestinationGroupID;
			Content = distributionFile.Content;
		}

		#region Properties

		[DataMember]
		public string RelativePath { get; private set; }

		[DataMember]
		public Guid DestinationGroupID { get; set; }

		[DataMember]
		public byte[] Content { get; private set; }

		#endregion

		public DistributionFile FromDTO()
		{
			return new DistributionFile(RelativePath, DestinationGroupID, Content);
		}
	}
}

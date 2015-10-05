using System;

namespace ProcessManager.DataObjects
{
	public class DistributeFileResult
	{
		public DistributeFileResult(string relativePath, Guid destinationGroupID, string errorMessage)
		{
			RelativePath = relativePath;
			DestinationGroupID = destinationGroupID;
			ErrorMessage = errorMessage;
		}

		#region Properties

		public bool Success => ErrorMessage == null;

		public string RelativePath { get; private set; }
		public Guid DestinationGroupID { get; private set; }
		public string ErrorMessage { get; }

		#endregion
	}
}

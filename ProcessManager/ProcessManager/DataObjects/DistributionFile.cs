using System;
using System.Diagnostics;

namespace ProcessManager.DataObjects
{
	[DebuggerDisplay("{RelativePath}, IsDistributed={IsDistributed}")]
	public class DistributionFile
	{
		private bool _isDistributed;

		public DistributionFile(string relativePath, Guid destinationGroupID, byte[] content)
		{
			RelativePath = relativePath;
			DestinationGroupID = destinationGroupID;
			Content = content;
			IsDistributed = false;
		}

		public DistributionFile(string relativePath) : this(relativePath, Guid.Empty, null) {}

		#region Properties

		public string RelativePath { get; private set; }
		public Guid DestinationGroupID { get; set; }
		public byte[] Content { get; set; }
		public bool IsDistributed
		{
			get { return _isDistributed; }
			set
			{
				_isDistributed = value;
				if (_isDistributed) Content = null;
			}
		}

		#endregion
	}
}

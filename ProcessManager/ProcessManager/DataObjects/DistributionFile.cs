using System;
using System.Diagnostics;

namespace ProcessManager.DataObjects
{
	[DebuggerDisplay("{RelativePath}, IsDistributed={IsDistributed}")]
	public class DistributionFile
	{
		private bool _isDistributed;

		public DistributionFile(string relativePath)
		{
			RelativePath = relativePath;
			DestinationApplicationID = Guid.Empty;
			Content = null;
			IsDistributed = false;
		}

		#region Properties

		public string RelativePath { get; private set; }
		public Guid DestinationApplicationID { get; set; }
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

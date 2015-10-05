using System;
using System.Diagnostics;
using ProcessManager.DataObjects.Comparers;

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

		public string RelativePath { get; }
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

		#region Equality

		public override bool Equals(object obj)
		{
			DistributionFile distributionFile = obj as DistributionFile;
			return (distributionFile != null && Comparer.DistributionFilesEqual(this, distributionFile));
		}

		public override int GetHashCode()
		{
			return Comparer.GetHashCode(this);
		}

		#endregion

		#region ToString

		public override string ToString()
		{
			return RelativePath;
		}

		#endregion
	}
}

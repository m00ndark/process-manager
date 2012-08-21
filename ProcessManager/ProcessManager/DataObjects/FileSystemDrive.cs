using System;
using System.Collections.Generic;

namespace ProcessManager.DataObjects
{
	public enum FileSystemDriveType
	{
		Unknown = 0,
		RemovableDisk = 2,
		LocalDisk = 3,
		NetworkDrive = 4,
		CompactDisc = 5
	}

	public class FileSystemDrive : IFileSystemEntry
	{
		private static readonly IDictionary<FileSystemDriveType, string> _typeDescriptions = new Dictionary<FileSystemDriveType, string>()
			{
				{ FileSystemDriveType.Unknown, string.Empty },
				{ FileSystemDriveType.RemovableDisk, "Removable Disk" },
				{ FileSystemDriveType.LocalDisk, "Local Disk" },
				{ FileSystemDriveType.NetworkDrive, "Network Drive" },
				{ FileSystemDriveType.CompactDisc, "Compact Disc" }
			};

		public FileSystemDrive(string name, string label, FileSystemDriveType type)
		{
			Name = name;
			IsFolder = true;
			Label = label;
			Type = type;
		}

		#region Properties

		public string Name { get; set; }
		public bool IsFolder { get; private set; }
		public string Label { get; set; }
		public FileSystemDriveType Type { get; set; }

		#endregion

		#region Equality

		public bool Equals(string name)
		{
			return name != null && name.Equals(Name, StringComparison.CurrentCultureIgnoreCase);
		}

		#endregion

		public string GetTypeDescription()
		{
			return _typeDescriptions[Type];
		}
	}
}

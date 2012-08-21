using System;

namespace ProcessManager.DataObjects
{
	public class FileSystemEntry : IFileSystemEntry
	{
		public FileSystemEntry(string name, bool isFolder, DateTime modifiedDate)
			: this(name, isFolder, modifiedDate, 0) {}

		public FileSystemEntry(string name, bool isFolder, DateTime modifiedDate, long bytes)
		{
			Name = name;
			IsFolder = isFolder;
			Bytes = bytes;
			ModifiedDate = modifiedDate;
		}

		#region Properties

		public string Name { get; set; }
		public bool IsFolder { get; set; }
		public long Bytes { get; set; }
		public DateTime ModifiedDate { get; set; }

		#endregion

		#region Equality

		public bool Equals(string name)
		{
			return name != null && name.Equals(Name, StringComparison.CurrentCultureIgnoreCase);
		}

		#endregion
	}
}

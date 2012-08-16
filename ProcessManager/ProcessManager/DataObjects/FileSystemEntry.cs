namespace ProcessManager.DataObjects
{
	public class FileSystemEntry
	{
		public FileSystemEntry(string entry, bool isFolder)
		{
			Entry = entry;
			IsFolder = isFolder;
		}

		#region Properties

		public string Entry { get; set; }
		public bool IsFolder { get; set; }

		#endregion
	}
}

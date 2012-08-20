namespace ProcessManager.DataObjects
{
	public class FileSystemEntry : IFileSystemEntry
	{
		public FileSystemEntry(string name, bool isFolder)
		{
			Name = name;
			IsFolder = isFolder;
		}

		#region Properties

		public string Name { get; set; }
		public bool IsFolder { get; set; }

		#endregion
	}
}

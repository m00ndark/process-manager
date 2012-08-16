using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOFileSystemEntry
	{
		public DTOFileSystemEntry(FileSystemEntry fileSystemEntry)
		{
			Entry = fileSystemEntry.Entry;
			IsFolder = fileSystemEntry.IsFolder;
		}

		#region Properties

		[DataMember]
		public string Entry { get; set; }

		[DataMember]
		public bool IsFolder { get; set; }

		#endregion

		public FileSystemEntry FromDTO()
		{
			return new FileSystemEntry(Entry, IsFolder);
		}
	}
}

using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOFileSystemDrive
	{
		public DTOFileSystemDrive(FileSystemDrive fileSystemDrive)
		{
			Name = fileSystemDrive.Name;
			Label = fileSystemDrive.Label;
			Type = fileSystemDrive.Type;
		}

		#region Properties

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Label { get; set; }

		[DataMember]
		public FileSystemDriveType Type { get; set; }

		#endregion

		public FileSystemDrive FromDTO()
		{
			return new FileSystemDrive(Name, Label, Type);
		}
	}
}

﻿using System;
using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOFileSystemEntry
	{
		public DTOFileSystemEntry(FileSystemEntry fileSystemEntry)
		{
			Entry = fileSystemEntry.Name;
			IsFolder = fileSystemEntry.IsFolder;
			Bytes = fileSystemEntry.Bytes;
			ModifiedDate = fileSystemEntry.ModifiedDate;
		}

		#region Properties

		[DataMember]
		public string Entry { get; set; }

		[DataMember]
		public bool IsFolder { get; set; }

		[DataMember]
		public long Bytes { get; set; }

		[DataMember]
		public DateTime ModifiedDate { get; set; }

		#endregion

		public FileSystemEntry FromDTO()
		{
			return new FileSystemEntry(Entry, IsFolder, ModifiedDate, Bytes);
		}
	}
}

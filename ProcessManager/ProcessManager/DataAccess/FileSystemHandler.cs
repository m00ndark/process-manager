using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.DataAccess
{
	public static class FileSystemHandler
	{
		private const string FILE_BACKUP_EXTENSION = ".bak";

		public static IEnumerable<string> GetDrives()
		{
			return Directory.GetLogicalDrives();
		}

		public static IEnumerable<FileSystemEntry> GetFileSystemEntries(string path)
		{
			if (!Directory.Exists(path))
				yield break;

			foreach (string entry in Directory.GetDirectories(path))
				yield return new FileSystemEntry(entry, true);

			foreach (string entry in Directory.GetFiles(path))
				yield return new FileSystemEntry(entry, false);
		}

		public static void CreateDirectory(string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
		}

		private static void MakeFileBackup(string filePath)
		{
			if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0) return;
			string backupFilePath = filePath + FILE_BACKUP_EXTENSION;
			File.Copy(filePath, backupFilePath, true);
		}

		#region Serialization

		public static void Serialize(string filePath, IXmlSerializable obj)
		{
			MakeFileBackup(filePath);
			XmlSerializer serializer = new XmlSerializer(obj.GetType());
			using (StreamWriter writer = new StreamWriter(filePath))
				serializer.Serialize(writer, obj);
		}

		public static T Deserialize<T>(string filePath) where T : IXmlSerializable
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (StreamReader reader = new StreamReader(filePath))
				return (T) serializer.Deserialize(reader);
		}

		#endregion
	}
}

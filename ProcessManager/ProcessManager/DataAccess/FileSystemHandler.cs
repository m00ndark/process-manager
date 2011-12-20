using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProcessManager.DataAccess
{
	public static class FileSystemHandler
	{
		private const string FILE_BACKUP_EXTENSION = ".bak";

		#region Directories

		public static void CreateDirectory(string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
		}

		#endregion

		#region Files

		#region Serialization

		public static void Serialize(string filePath, IXmlSerializable obj)
		{
			MakeBackup(filePath);
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

		private static void MakeBackup(string filePath)
		{
			if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0) return;
			string backupFilePath = filePath + FILE_BACKUP_EXTENSION;
			File.Copy(filePath, backupFilePath, true);
		}

		#endregion
	}
}

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.DataAccess
{
	public static class FileSystemHandler
	{
		private const string FILE_BACKUP_EXTENSION = ".bak";

		public static IEnumerable<FileSystemDrive> GetDrives()
		{
			DataTable drivesTable = WMIHandler.GetTable("Win32_LogicalDisk", new[] { "Name", "DriveType", "ProviderName", "VolumeName" });
			foreach (DataRow row in drivesTable.Rows)
			{
				int driveType = int.Parse(row["DriveType"].ToString());
				FileSystemDriveType type = (Enum.IsDefined(typeof(FileSystemDriveType), driveType) ? (FileSystemDriveType) driveType : FileSystemDriveType.Unknown);
				string providerName = (row["ProviderName"] != null ? row["ProviderName"].ToString().TrimEnd(Path.DirectorySeparatorChar) : string.Empty);
				string volumeName = (row["VolumeName"] != null ? row["VolumeName"].ToString() : string.Empty);
				string label = string.Empty;
				if (type == FileSystemDriveType.NetworkDrive && !string.IsNullOrEmpty(providerName))
				{
					string labelPart = Path.GetFileName(providerName);
					if (labelPart != null)
						label = labelPart + " (" + providerName.Substring(0, providerName.Length - labelPart.Length).TrimEnd(Path.DirectorySeparatorChar) + ")";
				}
				else
					label = (!string.IsNullOrEmpty(volumeName) ? volumeName : string.Empty);

				yield return new FileSystemDrive(row["Name"].ToString(), label, type);
			}
		}

		public static IEnumerable<FileSystemEntry> GetFileSystemEntries(string path)
		{
			string[] directories = new string[0], files = new string[0];

			try
			{
				if (!Directory.Exists(path))
					yield break;

				directories = Directory.GetDirectories(path);
				files = Directory.GetFiles(path);
			}
			catch { ; }

			foreach (string entry in directories)
			{
				DirectoryInfo info = new DirectoryInfo(entry);
				yield return new FileSystemEntry(Path.GetFileName(entry), true, info.LastWriteTime);
			}

			foreach (string entry in files)
			{
				FileInfo info = new FileInfo(entry);
				yield return new FileSystemEntry(Path.GetFileName(entry), false, info.LastWriteTime, info.Length);
			}
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

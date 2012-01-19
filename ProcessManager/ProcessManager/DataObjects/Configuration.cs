using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ProcessManager.DataAccess;
using ProcessManager.Utilities;

namespace ProcessManager.DataObjects
{
	public class Configuration : IXmlSerializable
	{
		private static readonly object _serializationLock = new object();
		private static readonly string _appDataFolder;
		private static readonly string _configFilePath;

		static Configuration()
		{
			_appDataFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
				Settings.Service.Defaults.COMPANY_FOLDER_NAME), Settings.Service.Defaults.APPLICATION_FOLDER_NAME);

			FileSystemHandler.CreateDirectory(_appDataFolder);

			_configFilePath = Path.Combine(_appDataFolder, Settings.Service.Read("ConfigurationFileName"));
		}

		public Configuration() : this(null) {}

		public Configuration(string hash)
		{
			Hash = hash;
			Groups = new List<Group>();
			Applications = new List<Application>();
			if (Hash == null) UpdateHash();
		}

		#region Properties

		public string Hash { get; private set; }
		public List<Group> Groups { get; private set; }
		public List<Application> Applications { get; private set; }

		#endregion

		public void UpdateHash()
		{
			Hash = Cryptographer.CreateSHA512Hash(Guid.NewGuid().ToString());
		}

		public static Configuration Read()
		{
			if (!File.Exists(_configFilePath))
				return new Configuration();

			lock (_serializationLock)
			{
				Configuration configuration = FileSystemHandler.Deserialize<Configuration>(_configFilePath);
				configuration.UpdateHash();
				return configuration;
			}
		}

		public static void Write(Configuration configuration)
		{
			lock (_serializationLock)
			{
				FileSystemHandler.Serialize(_configFilePath, configuration);
			}
		}

		#region Implementation of IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.IsStartElement())
				reader.ReadStartElement("Configuration");

			if (reader.IsEmptyElement)
				return;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Configuration")
					break;

				if (reader.NodeType == XmlNodeType.Element && reader.Name == "Groups")
				{
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Groups")
							break;

						if (reader.NodeType == XmlNodeType.Element && reader.Name == "Group")
							Groups.Add(new Group(reader));
					}
				}

				if (reader.NodeType == XmlNodeType.Element && reader.Name == "Applications")
				{
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Applications")
							break;

						if (reader.NodeType == XmlNodeType.Element && reader.Name == "Application")
							Applications.Add(new Application(reader));
					}
				}
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Groups");
			foreach (Group group in Groups)
				group.WriteXml(writer);
			writer.WriteEndElement();

			writer.WriteStartElement("Applications");
			foreach (Application application in Applications)
				application.WriteXml(writer);
			writer.WriteEndElement();
		}

		#endregion
	}
}

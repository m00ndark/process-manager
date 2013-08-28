using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects.Comparers;
using ProcessManager.Utilities;

namespace ProcessManager.DataObjects
{
	[Flags]
	public enum ConfigurationParts
	{
		None = 0,
		All = 3,
		Groups = 1,
		Applications = 2
	}

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

		public Configuration Clone()
		{
			Configuration configuration = new Configuration();
			configuration.Groups.AddRange(Groups.Select(group => group.Clone()));
			configuration.Applications.AddRange(Applications.Select(application => application.Clone()));
			return configuration;
		}

		public Configuration CopyTo(Configuration configuration, ConfigurationParts configurationParts)
		{
			List<Application> oldApplications = configuration.Applications;

			if ((configurationParts & ConfigurationParts.Groups) != 0)
			{
				configuration.Groups = new List<Group>();
				configuration.Groups.AddRange(Groups.Select(group => group.Clone()));
			}

			if ((configurationParts & ConfigurationParts.Applications) != 0)
			{
				configuration.Applications = new List<Application>();
				configuration.Applications.AddRange(Applications.Select(application => application.Clone()));
			}

			if (configurationParts == ConfigurationParts.All)
			{
				configuration.Groups.ForEach(group => group.ID = Guid.NewGuid());
				configuration.Applications.ForEach(application =>
				{
					application.Sources.ForEach(source => source.ID = Guid.NewGuid());
					Guid oldID = application.ID;
					application.ID = Guid.NewGuid();
					configuration.Groups.ForEach(group =>
					{
						if (group.Applications.Contains(oldID))
						{
							group.Applications.Remove(oldID);
							group.Applications.Add(application.ID);
						}
					});
				});
			}
			else if ((configurationParts & ConfigurationParts.Groups) != 0)
			{
				configuration.Groups.ForEach(group => group.ID = Guid.NewGuid());
				if ((configurationParts & ConfigurationParts.Applications) == 0)
				{
					foreach (Group group in configuration.Groups)
					{
						List<Guid> toRemove = new List<Guid>(), toAdd = new List<Guid>();
						foreach (Application application in group.Applications.Select(applicationID => Applications.First(application => application.ID == applicationID)))
						{
							toRemove.Add(application.ID);
							Application existingApplication = configuration.Applications.FirstOrDefault(x => Comparer.ApplicationsEqual(x, application));
							if (existingApplication != null)
								toAdd.Add(existingApplication.ID);
						}
						toRemove.ForEach(id => group.Applications.Remove(id));
						toAdd.ForEach(id => group.Applications.Add(id));
					}
				}
			}
			else if ((configurationParts & ConfigurationParts.Applications) != 0)
			{
				configuration.Applications.ForEach(application =>
					{
						application.Sources.ForEach(source => source.ID = Guid.NewGuid());
						if ((configurationParts & ConfigurationParts.Groups) != 0)
						{
							Guid oldID = application.ID;
							application.ID = Guid.NewGuid();
							configuration.Groups.ForEach(group =>
								{
									if (group.Applications.Contains(oldID))
									{
										group.Applications.Remove(oldID);
										group.Applications.Add(application.ID);
									}
								});
						}
						else
						{
							Application oldApplication = oldApplications.FirstOrDefault(x => Comparer.ApplicationsEqual(x, application));
							if (oldApplication != null)
							{
								application.ID = oldApplication.ID;
							}
							else
							{
								List<Group> referringGroups = Groups.Where(group => group.Applications.Any(applicationID => applicationID == application.ID)).ToList();
								application.ID = Guid.NewGuid();
								configuration.Groups.Where(group => referringGroups.Any(refGroup => Comparer.GroupsEqual(refGroup, group))).ToList().ForEach(group => group.Applications.Add(application.ID));
							}
						}
					});
			}
			return configuration;
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

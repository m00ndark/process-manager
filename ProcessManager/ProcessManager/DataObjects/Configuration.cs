using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects.Comparers;
using ToolComponents.Core;

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
				Settings.Service.Defaults.COMPANY_NAME), Settings.Service.Defaults.APPLICATION_NAME);

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
			Hash = Cryptographer.CreateSha512Hash();
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
			IDictionary<Guid, Guid> applicationIDMappings = new Dictionary<Guid, Guid>();
			List<Group> oldGroups = configuration.Groups;
			List<Application> oldApplications = configuration.Applications;

			if ((configurationParts & ConfigurationParts.Groups) != 0)
			{
				configuration.Groups = new List<Group>(Groups.Select(group => group.Clone()));
				configuration.Groups.ForEach(group =>
					{
						Group oldGroup = oldGroups.FirstOrDefault(x => Comparer.GroupsEqual(x, group));
						group.ID = oldGroup?.ID ?? Guid.NewGuid();
					});
			}

			if ((configurationParts & ConfigurationParts.Applications) != 0)
			{
				configuration.Applications = new List<Application>(Applications.Select(application => application.Clone()));
				configuration.Applications.ForEach(application =>
					{
						Application oldApplication = oldApplications.FirstOrDefault(x => Comparer.ApplicationsEqual(x, application));
						Guid updatedApplicationID = oldApplication?.ID ?? Guid.NewGuid();
						applicationIDMappings.Add(application.ID, updatedApplicationID);
						application.ID = updatedApplicationID;
						application.Sources.ForEach(source => source.ID = Guid.NewGuid());
					});
			}

			// update application references of groups to match existing applications
			if (configurationParts == ConfigurationParts.All)
			{
				foreach (Group group in configuration.Groups)
				{
					List<Guid> toRemove = new List<Guid>(), toAdd = new List<Guid>();
					foreach (Guid applicationID in group.Applications.Where(applicationID => applicationIDMappings.ContainsKey(applicationID)))
					{
						toRemove.Add(applicationID);
						toAdd.Add(applicationIDMappings[applicationID]);
					}
					toRemove.ForEach(id => group.Applications.Remove(id));
					toAdd.ForEach(id => group.Applications.Add(id));
				}				
			}
			else if (configurationParts == ConfigurationParts.Groups)
			{
				foreach (Group group in configuration.Groups)
				{
					List<Guid> toRemove = new List<Guid>(), toAdd = new List<Guid>();
					foreach (Application sourceApplication in group.Applications.Select(applicationID => Applications.First(application => application.ID == applicationID)))
					{
						toRemove.Add(sourceApplication.ID);
						Application existingApplication = configuration.Applications.FirstOrDefault(application => Comparer.ApplicationsEqual(application, sourceApplication));
						if (existingApplication != null)
							toAdd.Add(existingApplication.ID);
					}
					toRemove.ForEach(id => group.Applications.Remove(id));
					toAdd.ForEach(id => group.Applications.Add(id));
				}
			}
			else if (configurationParts == ConfigurationParts.Applications)
			{
				// remove references to applications that do not exist anymore
				foreach (Group group in configuration.Groups)
				{
					group.Applications
						.Select(applicationID => oldApplications.First(oldApplication => oldApplication.ID == applicationID))
						.Where(oldApplication => !configuration.Applications.Any(x => Comparer.ApplicationsEqual(x, oldApplication)))
						.ToList()
						.ForEach(oldApplication => group.Applications.Remove(oldApplication.ID));
				}
				// add references to new application based on source references
				foreach (Application application in configuration.Applications.Where(application => oldApplications.All(oldApplication => oldApplication.ID != application.ID)))
				{
					Guid sourceApplicationID = applicationIDMappings
						.Where(x => x.Value == application.ID)
						.Select(x => x.Key)
						.First();
					List<Group> referringSourceGroups = Groups
						.Where(group => group.Applications.Any(applicationID => applicationID == sourceApplicationID))
						.ToList();
					configuration.Groups
						.Where(group => referringSourceGroups.Any(refGroup => Comparer.GroupsEqual(refGroup, group)))
						//.Where(group => group.Applications.All(applicationID => applicationID != application.ID))
						.ToList()
						.ForEach(group => group.Applications.Add(application.ID));
				}
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

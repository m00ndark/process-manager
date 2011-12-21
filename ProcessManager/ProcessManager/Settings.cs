using System;
using System.Collections.Generic;
using System.Configuration;
using ProcessManager.DataAccess;

namespace ProcessManager
{
	public static class Settings
	{
		#region Service class

		public static class Service
		{
			#region Defaults class

			public static class Defaults
			{
				public const string COMPANY_FOLDER_NAME = "QlikTech";
				public const string APPLICATION_FOLDER_NAME = "ProcessManager";
				public const string CONFIGURATION_FILE_NAME = "processmanager_config.xml";
				public const string LOG_FOLDER_NAME = "Logs";
				public const string LOG_FILE_NAME = "processmanager_%date%.log";
				public const int STATUS_UPDATE_INTERVAL = 500;
			}

			#endregion

			private static readonly IDictionary<string, string> _defaultValues;

			static Service()
			{
				_defaultValues = new Dictionary<string, string>()
					{
						{ "ConfigurationFileName", Defaults.CONFIGURATION_FILE_NAME },
						{ "LogFolderName", Defaults.LOG_FOLDER_NAME },
						{ "LogFileName", Defaults.LOG_FILE_NAME },
						{ "StatusUpdateInterval", Defaults.STATUS_UPDATE_INTERVAL.ToString() }
					};
			}

			public static T Read<T>(string settingName) where T : struct
			{
				return (T) Convert.ChangeType(Read(settingName), typeof(T));
			}

			public static string Read(string settingName)
			{
				string defaultValue = (_defaultValues.ContainsKey(settingName) ? _defaultValues[settingName] : string.Empty);
				return Read(settingName, defaultValue);
			}

			public static T Read<T>(string settingName, string defaultValue) where T : struct
			{
				return (T) Convert.ChangeType(Read(settingName, defaultValue), typeof(T));
			}

			public static string Read(string settingName, string defaultValue)
			{
				return ConfigurationManager.AppSettings[settingName] ?? defaultValue;
			}
		}

		#endregion

		#region Client class

		public static class Client
		{
			#region Defaults class

			public static class Defaults
			{
				public const string START_WITH_WINDOWS = "False";
			}

			#endregion

			static Client()
			{
				StartWithWindows = bool.Parse(Defaults.START_WITH_WINDOWS);
			}

			#region Properties

			public static bool StartWithWindows { get; set; }

			#endregion

			public static void Load()
			{
				RegistryHandler.LoadClientSettings();
			}

			public static void Load(ClientSettingsType clientSettingsType)
			{
				RegistryHandler.LoadClientSettings(clientSettingsType);
			}

			public static void Save()
			{
				RegistryHandler.SaveClientSettings();
			}

			public static void Save(ClientSettingsType clientSettingsType)
			{
				RegistryHandler.SaveClientSettings(clientSettingsType);
			}
		} 

		#endregion
	}
}

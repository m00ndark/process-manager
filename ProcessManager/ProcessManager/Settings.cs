using System;
using System.Collections.Generic;
using System.Configuration;

namespace ProcessManager
{
	public static class Settings
	{
		#region Defaults class

		public static class Defaults
		{
			public const string COMPANY_FOLDER_NAME = "QlikTech";
			public const string APPLICATION_FOLDER_NAME = "ProcessManager";
			public const string CONFIGURATION_FILE_NAME = "processmanager_config.xml";
			public const string LOG_FOLDER_NAME = "Logs";
			public const string LOG_FILE_NAME = "processmanager_%date%.log";
		}

		#endregion

		private static readonly IDictionary<string, string> _defaultValues;

		static Settings()
		{
			_defaultValues = new Dictionary<string, string>()
				{
					{ "ConfigurationFileName", Defaults.CONFIGURATION_FILE_NAME },
					{ "LogFolderName", Defaults.LOG_FOLDER_NAME },
					{ "LogFileName", Defaults.LOG_FILE_NAME }
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
}

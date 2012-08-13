using System;
using System.Collections.Generic;
using System.Configuration;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;

namespace ProcessManager
{
	public enum ControlPanelGrouping
	{
		MachineGroupApplication,
		GroupMachineApplication
	}

	public static class Settings
	{
		#region Constants class

		public static class Constants
		{
			public const string APPLICATION_NAME = "Process Manager";
			public const string LOCALHOST = "localhost";
		}

		#endregion

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
				public const string SELECTED_HOST_NAME = Constants.LOCALHOST;
				public const string SELECTED_CONFIGURATION_SECTION = "";
				public const string SELECTED_GROUPING = "";
				public const string SELECTED_FILTER_MACHINE = "";
				public const string SELECTED_FILTER_GROUP = "";
				public const string SELECTED_FILTER_APPLICATION = "";
			}

			#endregion

			static Client()
			{
				Machines = new List<Machine>();
				StartWithWindows = bool.Parse(Defaults.START_WITH_WINDOWS);
				CFG_SelectedHostName = Defaults.SELECTED_HOST_NAME;
				CFG_SelectedConfigurationSection = Defaults.SELECTED_CONFIGURATION_SECTION;
				CP_SelectedGrouping = Defaults.SELECTED_GROUPING;
				CP_SelectedFilterMachine = Defaults.SELECTED_FILTER_MACHINE;
				CP_SelectedFilterGroup = Defaults.SELECTED_FILTER_GROUP;
				CP_SelectedFilterApplication = Defaults.SELECTED_FILTER_APPLICATION;
				CP_CheckedNodes = new List<Guid>();
				CP_CollapsedNodes = new Dictionary<ControlPanelGrouping, List<Guid>>()
					{
						{ ControlPanelGrouping.MachineGroupApplication, new List<Guid>() },
						{ ControlPanelGrouping.GroupMachineApplication, new List<Guid>() }
					};
			}

			#region Properties

			public static List<Machine> Machines { get; private set; }
			public static bool StartWithWindows { get; set; }
			public static string CFG_SelectedHostName { get; set; }
			public static string CFG_SelectedConfigurationSection { get; set; }
			public static string CP_SelectedGrouping { get; set; }
			public static string CP_SelectedFilterMachine { get; set; }
			public static string CP_SelectedFilterGroup { get; set; }
			public static string CP_SelectedFilterApplication { get; set; }
			public static List<Guid> CP_CheckedNodes { get; private set; }
			public static IDictionary<ControlPanelGrouping, List<Guid>> CP_CollapsedNodes { get; private set; }

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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;

namespace ProcessManager
{
	public enum ControlPanelTab
	{
		Process,
		Distribution,
		Macro
	}

	public enum ProcessGrouping
	{
		MachineGroupApplication,
		GroupMachineApplication
	}

	public enum DistributionGrouping
	{
		MachineGroupApplicationMachine,
		GroupMachineApplicationMachine
	}

	public static class Settings
	{
		#region Constants class

		public static class Constants
		{
			public const string APPLICATION_NAME = "Process Manager";
			public const string LOCALHOST = "localhost";

			public static readonly Machine LocalMachine = new Machine(LOCALHOST);
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
				public const int DISTRIBUTION_CONNECTION_CLEAN_INTERVAL = 1000;
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
						{ "StatusUpdateInterval", Defaults.STATUS_UPDATE_INTERVAL.ToString(CultureInfo.InvariantCulture) },
						{ "DistributionConnectionCleanInterval", Defaults.DISTRIBUTION_CONNECTION_CLEAN_INTERVAL.ToString(CultureInfo.InvariantCulture) }
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
				public const string USER_OWNS_CONTROL_PANEL = "False";
				public const string KEEP_CONTROL_PANEL_TOP_MOST = "True";
				public const string SELECTED_HOST_NAME = Constants.LOCALHOST;
				public const string SELECTED_CONFIGURATION_SECTION = "";
				public const string SELECTED_TAB = "";
				public const string SELECTED_PROCESS_GROUPING = "";
				public const string SELECTED_PROCESS_FILTER_MACHINE = "";
				public const string SELECTED_PROCESS_FILTER_GROUP = "";
				public const string SELECTED_PROCESS_FILTER_APPLICATION = "";
				public const string SELECTED_DISTRIBUTION_GROUPING = "";
				public const string SELECTED_DISTRIBUTION_FILTER_SOURCE_MACHINE = "";
				public const string SELECTED_DISTRIBUTION_FILTER_GROUP = "";
				public const string SELECTED_DISTRIBUTION_FILTER_APPLICATION = "";
				public const string SELECTED_DISTRIBUTION_FILTER_DESTINATION_MACHINE = "";
			}

			#endregion

			static Client()
			{
				Machines = new MachineList();
				Macros = new List<Macro>();
				StartWithWindows = bool.Parse(Defaults.START_WITH_WINDOWS);
				UserOwnsControlPanel = bool.Parse(Defaults.USER_OWNS_CONTROL_PANEL);
				KeepControlPanelTopMost = bool.Parse(Defaults.KEEP_CONTROL_PANEL_TOP_MOST);
				CFG_SelectedHostName = Defaults.SELECTED_HOST_NAME;
				CFG_SelectedConfigurationSection = Defaults.SELECTED_CONFIGURATION_SECTION;
				CP_SelectedTab = Defaults.SELECTED_TAB;
				P_SelectedGrouping = Defaults.SELECTED_PROCESS_GROUPING;
				P_SelectedFilterMachine = Defaults.SELECTED_PROCESS_FILTER_MACHINE;
				P_SelectedFilterGroup = Defaults.SELECTED_PROCESS_FILTER_GROUP;
				P_SelectedFilterApplication = Defaults.SELECTED_PROCESS_FILTER_APPLICATION;
				P_CheckedNodes = new List<Guid>();
				P_CollapsedNodes = new Dictionary<ProcessGrouping, List<Guid>>()
					{
						{ ProcessGrouping.MachineGroupApplication, new List<Guid>() },
						{ ProcessGrouping.GroupMachineApplication, new List<Guid>() }
					};
				D_SelectedGrouping = Defaults.SELECTED_DISTRIBUTION_GROUPING;
				D_SelectedFilterSourceMachine = Defaults.SELECTED_DISTRIBUTION_FILTER_SOURCE_MACHINE;
				D_SelectedFilterGroup = Defaults.SELECTED_DISTRIBUTION_FILTER_GROUP;
				D_SelectedFilterApplication = Defaults.SELECTED_DISTRIBUTION_FILTER_APPLICATION;
				D_SelectedFilterDestinationMachine = Defaults.SELECTED_DISTRIBUTION_FILTER_DESTINATION_MACHINE;
				D_CheckedNodes = new List<Guid>();
				D_CollapsedNodes = new Dictionary<DistributionGrouping, List<Guid>>()
					{
						{ DistributionGrouping.MachineGroupApplicationMachine, new List<Guid>() },
						{ DistributionGrouping.GroupMachineApplicationMachine, new List<Guid>() }
					};
				M_CheckedNodes = new List<Guid>();
                M_CollapsedNodes = new List<Guid>();
			}

			#region Properties

			public static MachineList Machines { get; private set; }
			public static List<Macro> Macros { get; private set; }
			public static bool StartWithWindows { get; set; }
			public static bool UserOwnsControlPanel { get; set; }
			public static bool KeepControlPanelTopMost { get; set; }
			public static string CFG_SelectedHostName { get; set; }
			public static string CFG_SelectedConfigurationSection { get; set; }
			public static string CP_SelectedTab { get; set; }
			public static string P_SelectedGrouping { get; set; }
			public static string P_SelectedFilterMachine { get; set; }
			public static string P_SelectedFilterGroup { get; set; }
			public static string P_SelectedFilterApplication { get; set; }
			public static List<Guid> P_CheckedNodes { get; private set; }
			public static IDictionary<ProcessGrouping, List<Guid>> P_CollapsedNodes { get; private set; }
			public static string D_SelectedGrouping { get; set; }
			public static string D_SelectedFilterSourceMachine { get; set; }
			public static string D_SelectedFilterGroup { get; set; }
			public static string D_SelectedFilterApplication { get; set; }
			public static string D_SelectedFilterDestinationMachine { get; set; }
			public static List<Guid> D_CheckedNodes { get; private set; }
			public static IDictionary<DistributionGrouping, List<Guid>> D_CollapsedNodes { get; private set; }
			public static List<Guid> M_CheckedNodes { get; private set; }
			public static List<Guid> M_CollapsedNodes { get; private set; }

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

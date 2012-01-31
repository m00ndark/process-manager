using System;
using System.Linq;
using Microsoft.Win32;
using ProcessManager.DataObjects;

namespace ProcessManager.DataAccess
{
	public enum	ClientSettingsType
	{
		Machines,
		Options,
		States
	}

	public static class RegistryHandler
	{
		private const string APPLICATION_REGISTRY_KEY = @"SOFTWARE\QlikTech\ProcessManager";
		private const string WINDOWS_RUN_REGISTRY_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

		public static void LoadClientSettings()
		{
			foreach (string clientSettingsType in Enum.GetNames(typeof(ClientSettingsType)))
			{
				LoadClientSettings((ClientSettingsType) Enum.Parse(typeof(ClientSettingsType), clientSettingsType));
			}
		}

		public static void LoadClientSettings(ClientSettingsType clientSettingsType)
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey(APPLICATION_REGISTRY_KEY, false);
			if (key == null) return;
			switch (clientSettingsType)
			{
				case ClientSettingsType.Machines:
					Settings.Client.Machines.Clear();
					RegistryKey machinesKey = key.OpenSubKey("Machines", false);
					if (machinesKey != null)
					{
						RegistryKey machineKey = machinesKey.OpenSubKey("Machine " + Settings.Client.Machines.Count.ToString("00"), false);
						while (machineKey != null)
						{
							string hostName = (string) machineKey.GetValue("Host Name", "<unknown>");
							Settings.Client.Machines.Add(new Machine(hostName));
							machineKey.Close();
							machineKey = machinesKey.OpenSubKey("Machine " + Settings.Client.Machines.Count.ToString("00"), false);
						}
						machinesKey.Close();
					}
					break;

				case ClientSettingsType.Options:
					RegistryKey optionsKey = key.OpenSubKey("Options", false);
					if (optionsKey != null)
					{
						Settings.Client.StartWithWindows = bool.Parse((string) optionsKey.GetValue("Start With Windows", Settings.Client.Defaults.START_WITH_WINDOWS));
						optionsKey.Close();
					}
					break;

				case ClientSettingsType.States:
					RegistryKey statesKey = key.OpenSubKey("States", false);
					if (statesKey != null)
					{
						Settings.Client.CFG_SelectedHostName = (string) statesKey.GetValue("CFG Selected Host Name", Settings.Client.Defaults.SELECTED_HOST_NAME);
						Settings.Client.CFG_SelectedConfigurationSection = (string) statesKey.GetValue("CFG Selected Configuration Section", Settings.Client.Defaults.SELECTED_CONFIGURATION_SECTION);
						Settings.Client.CP_SelectedGrouping = (string) statesKey.GetValue("CP Selected Grouping", Settings.Client.Defaults.SELECTED_GROUPING);
						statesKey.Close();
					}
					break;
			}
			key.Close();
		}

		public static void SaveClientSettings()
		{
			foreach (string clientSettingsType in Enum.GetNames(typeof(ClientSettingsType)))
			{
				SaveClientSettings((ClientSettingsType) Enum.Parse(typeof(ClientSettingsType), clientSettingsType));
			}
		}

		public static void SaveClientSettings(ClientSettingsType clientSettingsType)
		{
			RegistryKey key = Registry.LocalMachine.CreateSubKey(APPLICATION_REGISTRY_KEY);
			if (key == null) return;
			switch (clientSettingsType)
			{
				case ClientSettingsType.Machines:
					RegistryKey machinesKey = key.CreateSubKey("Machines");
					if (machinesKey != null)
					{
						string[] subKeyNames = machinesKey.GetSubKeyNames();
						foreach (string subKeyName in subKeyNames.Where(subKeyName => subKeyName.StartsWith("Machine ")))
						{
							machinesKey.DeleteSubKeyTree(subKeyName);
						}
						for (int i = 0; i < Settings.Client.Machines.Count; i++)
						{
							RegistryKey machineKey = machinesKey.CreateSubKey("Machine " + i.ToString("00"));
							if (machineKey == null) continue;
							machineKey.SetValue("Host Name", Settings.Client.Machines[i].HostName);
							machineKey.Close();
						}
						machinesKey.Close();
					}
					break;

				case ClientSettingsType.Options:
					RegistryKey optionsKey = key.CreateSubKey("Options");
					if (optionsKey != null)
					{
						optionsKey.SetValue("Start With Windows", Settings.Client.StartWithWindows.ToString());
						optionsKey.Close();
					}
					break;

				case ClientSettingsType.States:
					RegistryKey statesKey = key.CreateSubKey("States");
					if (statesKey != null)
					{
						statesKey.SetValue("CFG Selected Host Name", Settings.Client.CFG_SelectedHostName);
						statesKey.SetValue("CFG Selected Configuration Section", Settings.Client.CFG_SelectedConfigurationSection);
						statesKey.SetValue("CP Selected Grouping", Settings.Client.CP_SelectedGrouping);
						statesKey.Close();
					}
					break;
			}
			key.Close();
		}

		public static void SetWindowsStartupTrigger(string executablePath)
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey(WINDOWS_RUN_REGISTRY_KEY, true);
			if (key == null) return;
			if (string.IsNullOrEmpty(executablePath))
				key.DeleteValue("Process Manager", false);
			else
				key.SetValue("Process Manager", "\"" + executablePath + "\"", RegistryValueKind.String);
			key.Close();
		}
	}
}

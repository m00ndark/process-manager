using System;
using Microsoft.Win32;

namespace ProcessManager.DataAccess
{
	public enum	ClientSettingsType
	{
		Machines,
		StartWithWindows
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
			if (key != null)
			{
				switch (clientSettingsType)
				{
					case ClientSettingsType.StartWithWindows:
						Settings.Client.StartWithWindows = bool.Parse((string) key.GetValue("Start With Windows", Settings.Client.Defaults.START_WITH_WINDOWS));
						break;
				}
				key.Close();
			}
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
			if (key != null)
			{
				switch (clientSettingsType)
				{
					case ClientSettingsType.StartWithWindows:
						key.SetValue("Start With Windows", Settings.Client.StartWithWindows.ToString());
						break;
				}
				key.Close();
			}
		}

		public static void SetWindowsStartupTrigger(string executablePath)
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey(WINDOWS_RUN_REGISTRY_KEY, true);
			if (key != null)
			{
				if (string.IsNullOrEmpty(executablePath))
					key.DeleteValue("Process Manager", false);
				else
					key.SetValue("Process Manager", "\"" + executablePath + "\"", RegistryValueKind.String);
				key.Close();
			}
		}
	}
}

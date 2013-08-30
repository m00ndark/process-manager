using System;
using System.Linq;
using Microsoft.Win32;
using ProcessManager.DataObjects;

namespace ProcessManager.DataAccess
{
	public enum	ClientSettingsType
	{
		Machines,
		Macros,
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

				case ClientSettingsType.Macros:
					Settings.Client.Macros.Clear();
					RegistryKey macrosKey = key.OpenSubKey("Macros", false);
					if (macrosKey != null)
					{
						foreach (string macroID in macrosKey.GetSubKeyNames())
						{
							RegistryKey macroKey = macrosKey.OpenSubKey(macroID, false);
							if (macroKey == null) continue;
							string name = (string) macroKey.GetValue("Name", "<unknown>");
							Macro macro = new Macro(Guid.Parse(macroID), name);
							try
							{
								RegistryKey actionKey = macroKey.OpenSubKey("Action " + macro.Actions.Count.ToString("00"), false);
								while (actionKey != null)
								{
									IMacroAction action = null;
									string actionTypeStr = (string) actionKey.GetValue("Action Type");
									if (actionTypeStr == null) break;
									ActionType actionType = (ActionType) Enum.Parse(typeof(ActionType), actionTypeStr);
									switch (actionType)
									{
										case ActionType.Start:
										case ActionType.Stop:
										case ActionType.Restart:
											string processMachineID = (string) actionKey.GetValue("Machine ID");
											string processGroupID = (string) actionKey.GetValue("Group ID");
											string processApplicationID = (string) actionKey.GetValue("Application ID");
											action = new MacroProcessAction(actionType)
												{
													MachineID = Guid.Parse(processMachineID),
													GroupID = Guid.Parse(processGroupID),
													ApplicationID = Guid.Parse(processApplicationID)
												};
											break;
										case ActionType.Distribute:
											string distributionSourceMachineID = (string) actionKey.GetValue("Source Machine ID");
											string distributionGroupID = (string) actionKey.GetValue("Group ID");
											string distributionApplicationID = (string) actionKey.GetValue("Application ID");
											string distributionDestinationMachineID = (string) actionKey.GetValue("Destination Machine ID");
											action = new MacroDistributionAction(actionType)
												{
													SourceMachineID = Guid.Parse(distributionSourceMachineID),
													GroupID = Guid.Parse(distributionGroupID),
													ApplicationID = Guid.Parse(distributionApplicationID),
													DestinationMachineID = Guid.Parse(distributionDestinationMachineID)
												};
											break;
									}
									if (action == null) break;
									macro.Actions.Add(action);
									actionKey.Close();
									actionKey = macroKey.OpenSubKey("Action " + macro.Actions.Count.ToString("00"), false);
								}
							}
							catch
							{
								continue;
							}
							Settings.Client.Macros.Add(macro);
							macroKey.Close();
						}
						macrosKey.Close();
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
						Settings.Client.CP_SelectedTab = (string) statesKey.GetValue("CP Selected Tab", Settings.Client.Defaults.SELECTED_TAB);
						Settings.Client.P_SelectedGrouping = (string) statesKey.GetValue("P Selected Grouping", Settings.Client.Defaults.SELECTED_PROCESS_GROUPING);
						Settings.Client.P_SelectedFilterMachine = (string) statesKey.GetValue("P Selected Filter Machine", Settings.Client.Defaults.SELECTED_PROCESS_FILTER_MACHINE);
						Settings.Client.P_SelectedFilterGroup = (string) statesKey.GetValue("P Selected Filter Group", Settings.Client.Defaults.SELECTED_PROCESS_FILTER_GROUP);
						Settings.Client.P_SelectedFilterApplication = (string) statesKey.GetValue("P Selected Filter Application", Settings.Client.Defaults.SELECTED_PROCESS_FILTER_APPLICATION);
						Settings.Client.P_CheckedNodes.Clear();
						RegistryKey checkedNodesKey = statesKey.OpenSubKey("P Checked Nodes", false);
						if (checkedNodesKey != null)
						{
							string nodeID = (string) checkedNodesKey.GetValue("Node " + Settings.Client.P_CheckedNodes.Count.ToString("00"));
							while (nodeID != null)
							{
								Settings.Client.P_CheckedNodes.Add(new Guid(nodeID));
								nodeID = (string) checkedNodesKey.GetValue("Node " + Settings.Client.P_CheckedNodes.Count.ToString("00"));
							}
							checkedNodesKey.Close();
						}
						Enum.GetValues(typeof(ProcessGrouping)).Cast<ProcessGrouping>().ToList().ForEach(grouping => Settings.Client.P_CollapsedNodes[grouping].Clear());
						RegistryKey collapsedNodesKey = statesKey.OpenSubKey("P Collapsed Nodes", false);
						if (collapsedNodesKey != null)
						{
							foreach (ProcessGrouping grouping in Enum.GetValues(typeof(ProcessGrouping)))
							{
								RegistryKey groupingKey = collapsedNodesKey.OpenSubKey(grouping.ToString(), false);
								if (groupingKey != null)
								{
									string nodeID = (string) groupingKey.GetValue("Node " + Settings.Client.P_CollapsedNodes[grouping].Count.ToString("00"));
									while (nodeID != null)
									{
										Settings.Client.P_CollapsedNodes[grouping].Add(new Guid(nodeID));
										nodeID = (string) groupingKey.GetValue("Node " + Settings.Client.P_CollapsedNodes[grouping].Count.ToString("00"));
									}
									groupingKey.Close();
								}
							}
							collapsedNodesKey.Close();
						}
						Settings.Client.D_SelectedGrouping = (string) statesKey.GetValue("D Selected Grouping", Settings.Client.Defaults.SELECTED_DISTRIBUTION_GROUPING);
						Settings.Client.D_SelectedFilterSourceMachine = (string) statesKey.GetValue("D Selected Filter Source Machine", Settings.Client.Defaults.SELECTED_DISTRIBUTION_FILTER_SOURCE_MACHINE);
						Settings.Client.D_SelectedFilterGroup = (string) statesKey.GetValue("D Selected Filter Group", Settings.Client.Defaults.SELECTED_DISTRIBUTION_FILTER_GROUP);
						Settings.Client.D_SelectedFilterApplication = (string) statesKey.GetValue("D Selected Filter Application", Settings.Client.Defaults.SELECTED_DISTRIBUTION_FILTER_APPLICATION);
						Settings.Client.D_SelectedFilterDestinationMachine = (string) statesKey.GetValue("D Selected Filter Destination Machine", Settings.Client.Defaults.SELECTED_DISTRIBUTION_FILTER_DESTINATION_MACHINE);
						Settings.Client.D_CheckedNodes.Clear();
						checkedNodesKey = statesKey.OpenSubKey("D Checked Nodes", false);
						if (checkedNodesKey != null)
						{
							string nodeID = (string) checkedNodesKey.GetValue("Node " + Settings.Client.D_CheckedNodes.Count.ToString("00"));
							while (nodeID != null)
							{
								Settings.Client.D_CheckedNodes.Add(new Guid(nodeID));
								nodeID = (string) checkedNodesKey.GetValue("Node " + Settings.Client.D_CheckedNodes.Count.ToString("00"));
							}
							checkedNodesKey.Close();
						}
						Enum.GetValues(typeof(DistributionGrouping)).Cast<DistributionGrouping>().ToList().ForEach(grouping => Settings.Client.D_CollapsedNodes[grouping].Clear());
						collapsedNodesKey = statesKey.OpenSubKey("D Collapsed Nodes", false);
						if (collapsedNodesKey != null)
						{
							foreach (DistributionGrouping grouping in Enum.GetValues(typeof(DistributionGrouping)))
							{
								RegistryKey groupingKey = collapsedNodesKey.OpenSubKey(grouping.ToString(), false);
								if (groupingKey != null)
								{
									string nodeID = (string) groupingKey.GetValue("Node " + Settings.Client.D_CollapsedNodes[grouping].Count.ToString("00"));
									while (nodeID != null)
									{
										Settings.Client.D_CollapsedNodes[grouping].Add(new Guid(nodeID));
										nodeID = (string) groupingKey.GetValue("Node " + Settings.Client.D_CollapsedNodes[grouping].Count.ToString("00"));
									}
									groupingKey.Close();
								}
							}
							collapsedNodesKey.Close();
						}
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
						foreach (string subKeyName in machinesKey.GetSubKeyNames().Where(subKeyName => subKeyName.StartsWith("Machine ")))
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

				case ClientSettingsType.Macros:
					RegistryKey macrosKey = key.CreateSubKey("Macros");
					if (macrosKey != null)
					{
						foreach (string subKeyName in macrosKey.GetSubKeyNames())
						{
							macrosKey.DeleteSubKeyTree(subKeyName);
						}
						foreach (Macro macro in Settings.Client.Macros)
						{
							RegistryKey macroKey = macrosKey.CreateSubKey(macro.ID.ToString());
							if (macroKey == null) continue;
							macroKey.SetValue("Name", macro.Name);
							for (int i = 0; i < macro.Actions.Count; i++)
							{
								RegistryKey actionKey = macroKey.CreateSubKey("Action " + i.ToString("00"));
								actionKey.SetValue("Action Type", macro.Actions[i].Type.ToString());
								if (macro.Actions[i] is MacroProcessAction)
								{
									MacroProcessAction macroProcessAction = (MacroProcessAction) macro.Actions[i];
									actionKey.SetValue("Machine ID", macroProcessAction.MachineID);
									actionKey.SetValue("Group ID", macroProcessAction.GroupID);
									actionKey.SetValue("Application ID", macroProcessAction.ApplicationID);
								}
								else if (macro.Actions[i] is MacroDistributionAction)
								{
									MacroDistributionAction macroDistributionAction = (MacroDistributionAction) macro.Actions[i];
									actionKey.SetValue("Source Machine ID", macroDistributionAction.SourceMachineID);
									actionKey.SetValue("Group ID", macroDistributionAction.GroupID);
									actionKey.SetValue("Application ID", macroDistributionAction.ApplicationID);
									actionKey.SetValue("Destination Machine ID", macroDistributionAction.DestinationMachineID);
								}
								actionKey.Close();
							}
							macroKey.Close();
						}
						macrosKey.Close();
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
						statesKey.SetValue("CP Selected Tab", Settings.Client.CP_SelectedTab);
						statesKey.SetValue("P Selected Grouping", Settings.Client.P_SelectedGrouping);
						statesKey.SetValue("P Selected Filter Machine", Settings.Client.P_SelectedFilterMachine);
						statesKey.SetValue("P Selected Filter Group", Settings.Client.P_SelectedFilterGroup);
						statesKey.SetValue("P Selected Filter Application", Settings.Client.P_SelectedFilterApplication);
						RegistryKey checkedNodesKey = statesKey.CreateSubKey("P Checked Nodes");
						if (checkedNodesKey != null)
						{
							foreach (string valueName in checkedNodesKey.GetValueNames().Where(valueName => valueName.StartsWith("Node ")))
							{
								checkedNodesKey.DeleteValue(valueName);
							}
							for (int i = 0; i < Settings.Client.P_CheckedNodes.Count; i++)
							{
								checkedNodesKey.SetValue("Node " + i.ToString("00"), Settings.Client.P_CheckedNodes[i].ToString());
							}
							checkedNodesKey.Close();
						}
						RegistryKey collapsedNodesKey = statesKey.CreateSubKey("P Collapsed Nodes");
						if (collapsedNodesKey != null)
						{
							foreach (ProcessGrouping grouping in Enum.GetValues(typeof(ProcessGrouping)))
							{
								RegistryKey groupingKey = collapsedNodesKey.CreateSubKey(grouping.ToString());
								if (groupingKey != null)
								{
									foreach (string valueName in groupingKey.GetValueNames().Where(valueName => valueName.StartsWith("Node ")))
									{
										groupingKey.DeleteValue(valueName);
									}
									for (int i = 0; i < Settings.Client.P_CollapsedNodes[grouping].Count; i++)
									{
										groupingKey.SetValue("Node " + i.ToString("00"), Settings.Client.P_CollapsedNodes[grouping][i].ToString());
									}
									groupingKey.Close();
								}
							}
							collapsedNodesKey.Close();
						}
						statesKey.SetValue("D Selected Grouping", Settings.Client.D_SelectedGrouping);
						statesKey.SetValue("D Selected Filter Source Machine", Settings.Client.D_SelectedFilterSourceMachine);
						statesKey.SetValue("D Selected Filter Group", Settings.Client.D_SelectedFilterGroup);
						statesKey.SetValue("D Selected Filter Application", Settings.Client.D_SelectedFilterApplication);
						statesKey.SetValue("D Selected Filter Destination Machine", Settings.Client.D_SelectedFilterDestinationMachine);
						checkedNodesKey = statesKey.CreateSubKey("D Checked Nodes");
						if (checkedNodesKey != null)
						{
							foreach (string valueName in checkedNodesKey.GetValueNames().Where(valueName => valueName.StartsWith("Node ")))
							{
								checkedNodesKey.DeleteValue(valueName);
							}
							for (int i = 0; i < Settings.Client.D_CheckedNodes.Count; i++)
							{
								checkedNodesKey.SetValue("Node " + i.ToString("00"), Settings.Client.D_CheckedNodes[i].ToString());
							}
							checkedNodesKey.Close();
						}
						collapsedNodesKey = statesKey.CreateSubKey("D Collapsed Nodes");
						if (collapsedNodesKey != null)
						{
							foreach (DistributionGrouping grouping in Enum.GetValues(typeof(DistributionGrouping)))
							{
								RegistryKey groupingKey = collapsedNodesKey.CreateSubKey(grouping.ToString());
								if (groupingKey != null)
								{
									foreach (string valueName in groupingKey.GetValueNames().Where(valueName => valueName.StartsWith("Node ")))
									{
										groupingKey.DeleteValue(valueName);
									}
									for (int i = 0; i < Settings.Client.D_CollapsedNodes[grouping].Count; i++)
									{
										groupingKey.SetValue("Node " + i.ToString("00"), Settings.Client.D_CollapsedNodes[grouping][i].ToString());
									}
									groupingKey.Close();
								}
							}
							collapsedNodesKey.Close();
						}
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

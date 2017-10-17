using System;
using System.Collections.Generic;
using System.Linq;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.Service.Client;
using ProcessManager.Service.DataObjects;
using ProcessManagerUI.Utilities;

namespace ProcessManagerUI
{
	public static class ServiceHelper
	{
		static ServiceHelper()
		{
			ProcessManagerEventHandler = null;
		}

		#region Properties

		public static IProcessManagerEventHandler ProcessManagerEventHandler { get; private set; }
		public static bool IsInitialized => ProcessManagerEventHandler != null;

		#endregion

		public static void Initialize(IProcessManagerEventHandler processManagerEventHandler)
		{
			ProcessManagerEventHandler = processManagerEventHandler;
		}

		public static void ConnectMachines()
		{
			if (!IsInitialized)
				throw new InvalidOperationException("ServiceHelper not initialized");

			ConnectionStore.Connections.Keys.Where(machine => !Settings.Client.Machines.Contains(machine)).ToList().ForEach(ConnectionStore.RemoveConnection);

			if (Settings.Client.Machines.Any(machine => !ConnectionStore.ConnectionCreated(machine)))
			{
				Worker.Do("Connecting to machines...", () =>
					{
						foreach (Machine machine in Settings.Client.Machines.Where(machine => !ConnectionStore.ConnectionCreated(machine)))
						{
							MachineConnection connection = ConnectionStore.CreateConnection(ProcessManagerEventHandler, machine);
							connection.ServiceHandler.Initialize();
						}
					});
			}
		}

		public static void WaitForConfiguration(params Machine[] machines)
		{
			WaitForConfiguration(machines.ToList());
		}

		public static void WaitForConfiguration(List<Machine> machines)
		{
			if (machines != null && machines.Any(machine => machine != null && !ConnectionStore.ConfigurationAvailable(machine)))
				Worker.WaitFor("Retrieving configuration...", () => machines.All(machine => ConnectionStore.Connections[machine].ServiceHandler.Status == ProcessManagerServiceHandlerStatus.Disconnected
					|| ConnectionStore.ConfigurationAvailable(machine)));
		}

		public static void ReloadConfiguration(Machine machine = null)
		{
			Worker.Do("Retrieving configuration...", () =>
				{
					foreach (MachineConnection connection in ConnectionStore.Connections.Values.Where(x => machine == null || machine.Equals(x.Machine)))
					{
						try { connection.Configuration = connection.ServiceHandler.Service.GetConfiguration().FromDTO(); } catch { ; }
					}
				});
		}

		public static bool TrySaveConfiguration()
		{
			return !SaveConfiguration().Any();
		}

		public static Machine[] SaveConfiguration()
		{
			IDictionary<Machine, Exception> exceptions = new Dictionary<Machine, Exception>();
			Worker.Do("Saving configuration...", () =>
				{
					foreach (MachineConnection connection in ConnectionStore.Connections.Values.Where(connection => connection.ConfigurationModified))
					{
						try
						{
							connection.Configuration.UpdateHash();
							connection.ServiceHandler.Service.SetConfiguration(new DTOConfiguration(connection.Configuration));
							connection.ConfigurationModified = false;
						}
						catch (Exception ex)
						{
							exceptions.Add(connection.Machine, ex);
						}
					}
				});

			if (exceptions.Count == 0)
				return new Machine[0];

			Messenger.ShowError("Failed to save configuration",
				"Could not save configuration for " + exceptions.Aggregate(string.Empty, (x, y) => x + ", " + y.Key).Trim(", ".ToCharArray()),
				exceptions.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + Environment.NewLine + y.Value.Message).Trim());

			return exceptions.Keys.ToArray();
		}
	}
}

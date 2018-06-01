using System.Collections.Generic;
using ProcessManager.DataObjects;
using ProcessManager.Service.Client;

namespace ProcessManager
{
	public static class ConnectionStore
	{
		static ConnectionStore()
		{
			Connections = new Dictionary<Machine, MachineConnection>();
		}

		#region Properties

		public static IDictionary<Machine, MachineConnection> Connections { get; }

		#endregion

		public static bool ConnectionCreated(Machine machine)
		{
			return machine != null && Connections.ContainsKey(machine) && Connections[machine].ServiceHandler != null && Connections[machine].ServiceHandler.Status == ProcessManagerServiceHandlerStatus.Connected;
		}

		public static bool ConfigurationAvailable(Machine machine)
		{
			return ConnectionCreated(machine) && Connections[machine].Configuration != null;
		}

		public static MachineConnection CreateConnection(IProcessManagerEventHandler processManagerEventHandler, Machine machine)
		{
			ProcessManagerServiceHandler serviceHandler = ProcessManagerServiceConnectionHandler.Instance.CreateServiceHandler(processManagerEventHandler, machine);
			MachineConnection machineConnection = new MachineConnection(machine, serviceHandler);
			Connections[machine] = machineConnection;
			return machineConnection;
		}

		public static void RemoveConnection(Machine machine)
		{
			if (!Connections.ContainsKey(machine))
				return;

			Connections[machine].ServiceHandler.Dispose();
			Connections.Remove(machine);
		}

		public static bool MachineIsValid(Machine machine)
		{
			return ProcessManagerServiceHandler.HostNameValid(machine);
		}
	}
}

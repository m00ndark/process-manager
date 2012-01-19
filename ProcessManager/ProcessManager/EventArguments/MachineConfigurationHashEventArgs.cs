using System;
using ProcessManager.DataObjects;

namespace ProcessManager.EventArguments
{
	public class MachineConfigurationHashEventArgs : EventArgs
	{
		public MachineConfigurationHashEventArgs(string configurationHash) : this(null, configurationHash) { }

		public MachineConfigurationHashEventArgs(Machine machine, string configurationHash)
		{
			Machine = machine;
			ConfigurationHash = configurationHash;
		}

		#region Properties

		public Machine Machine { get; private set; }
		public string ConfigurationHash { get; private set; }

		#endregion
	}
}

using System;
using ProcessManager.EventArguments;

namespace ProcessManager
{
	public interface IProcessManagerEventHandler
	{
		event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;

		void ProcessManagerServiceEventHandler_ApplicationStatusesChanged(object sender, ApplicationStatusesEventArgs e);
		void ProcessManagerServiceEventHandler_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e);
	}
}

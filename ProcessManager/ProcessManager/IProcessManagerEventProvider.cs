using System;
using ProcessManager.EventArguments;

namespace ProcessManager
{
	public interface IProcessManagerEventProvider
	{
		event EventHandler<ProcessStatusesEventArgs> ProcessStatusesChanged;
		event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;
	}
}

using System;
using ProcessManager.EventArguments;

namespace ProcessManager
{
	public interface IProcessManagerEventProvider
	{
		event EventHandler<ApplicationStatusesEventArgs> ApplicationStatusesChanged;
		event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;
	}
}

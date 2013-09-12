using System;
using ProcessManager.EventArguments;

namespace ProcessManager
{
	public interface IProcessManagerEventHandler
	{
		event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;
		event EventHandler<DistributionResultEventArgs> DistributionCompleted;

		void ProcessManagerServiceEventHandler_ProcessStatusesChanged(object sender, ProcessStatusesEventArgs e);
		void ProcessManagerServiceEventHandler_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e);
		void ProcessManagerServiceEventHandler_DistributionCompleted(object sender, DistributionResultEventArgs e);
	}
}

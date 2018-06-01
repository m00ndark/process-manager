using System;
using System.Collections.Generic;
using System.Linq;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Common;
using ProcessManager.Service.DataObjects;

namespace ProcessManager.Service.Client
{
	public class ProcessManagerServiceEventHandler : IProcessManagerServiceEventHandler
	{
		public ProcessManagerServiceEventHandler(Machine machine)
		{
			Machine = machine;
		}

		#region Properties

		public Machine Machine { get; }

		#endregion

		#region Events

		public event EventHandler<ProcessStatusesEventArgs> ProcessStatusesChanged;
		public event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;
		public event EventHandler<DistributionResultEventArgs> DistributionCompleted;

		#endregion

		#region Event raisers

		private void RaiseProcessStatusesChangedEvent(IEnumerable<DTOProcessStatus> processStatuses)
		{
			ProcessStatusesChanged?.Invoke(this, new ProcessStatusesEventArgs(processStatuses.Select(x => x.FromDTO(Machine)).ToList()));
		}

		private void RaiseConfigurationChangedEvent(string configurationHash)
		{
			ConfigurationChanged?.Invoke(this, new MachineConfigurationHashEventArgs(Machine, configurationHash));
		}

		private void RaiseDistributionCompletedEvent(DTODistributionResult distributionResult)
		{
			DistributionCompleted?.Invoke(this, new DistributionResultEventArgs(distributionResult.FromDTO()));
		}

		#endregion

		#region Implementation of IProcessManagerServiceEventHandler

		public void ServiceEvent_ProcessStatusesChanged(List<DTOProcessStatus> processStatuses)
		{
			RaiseProcessStatusesChangedEvent(processStatuses);
		}

		public void ServiceEvent_ConfigurationChanged(string configurationHash)
		{
			RaiseConfigurationChangedEvent(configurationHash);
		}

		public void ServiceEvent_DistributionCompleted(DTODistributionResult distributionResult)
		{
			RaiseDistributionCompletedEvent(distributionResult);
		}

		#endregion
	}
}

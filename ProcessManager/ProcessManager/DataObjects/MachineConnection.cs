using System;
using System.Collections.Generic;
using ProcessManager.Service.Client;

namespace ProcessManager.DataObjects
{
	public class MachineConnection
	{
		public MachineConnection(Machine machine, ProcessManagerServiceHandler serviceHandler)
		{
			Machine = machine;
			ServiceHandler = serviceHandler;
			Configuration = null;
			ApplicationStatuses = null;
		}

		#region Properties

		public Machine Machine { get; private set; }
		public ProcessManagerServiceHandler ServiceHandler { get; set; }
		public Configuration Configuration { get; set; }
		public Dictionary<Guid, Dictionary<Guid, ApplicationStatus>> ApplicationStatuses { get; set; }

		#endregion
	}
}

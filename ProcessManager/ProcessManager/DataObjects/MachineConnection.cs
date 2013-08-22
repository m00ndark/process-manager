using System;
using System.Collections.Generic;
using ProcessManager.Service.Client;

namespace ProcessManager.DataObjects
{
	public class MachineConnection
	{
		private Configuration _configuration;

		public MachineConnection(Machine machine, ProcessManagerServiceHandler serviceHandler)
		{
			Machine = machine;
			ServiceHandler = serviceHandler;
			Configuration = null;
			ProcessStatuses = null;
		}

		#region Properties

		public Machine Machine { get; private set; }
		public ProcessManagerServiceHandler ServiceHandler { get; set; }
		public bool ConfigurationModified { get; set; }
		public Dictionary<Guid, Dictionary<Guid, ProcessStatus>> ProcessStatuses { get; set; }

		public Configuration Configuration
		{
			get { return _configuration; }
			set
			{
				_configuration = value;
				ConfigurationModified = false;
			}
		}

		#endregion
	}
}

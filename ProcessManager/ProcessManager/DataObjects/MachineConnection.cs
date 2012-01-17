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
			ApplicationStatuses = null;
		}

		#region Properties

		public Machine Machine { get; private set; }
		public ProcessManagerServiceHandler ServiceHandler { get; set; }
		public bool ConfigurationModified { get; set; }
		public Dictionary<Guid, Dictionary<Guid, ApplicationStatus>> ApplicationStatuses { get; set; }

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

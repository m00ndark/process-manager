using System;
using System.Collections.Generic;
using ProcessManager.Service.Client;

namespace ProcessManager.DataObjects
{
	public class Machine
	{
		public const string DEFAULT_HOST_NAME = "<host-name>";

		public Machine() : this(DEFAULT_HOST_NAME) {}

		public Machine(string hostName)
		{
			HostName = hostName;
			Configuration = null;
			ApplicationStatuses = null;
			ServiceHandler = null;
		}

		#region Properties

		// defining properties
		public string HostName { get; set; }

		// optional properties
		public Configuration Configuration { get; set; }
		public Dictionary<Guid, Dictionary<Guid, ApplicationStatus>> ApplicationStatuses { get; set; }
		public ProcessManagerServiceHandler ServiceHandler { get; set; }

		#endregion

		#region Equality

		public override bool Equals(object obj)
		{
			Machine machine = obj as Machine;
			return (machine != null && machine.HostName != null && HostName != null
				&& machine.HostName.Equals(HostName, StringComparison.CurrentCultureIgnoreCase));
		}

		public override int GetHashCode()
		{
			return (HostName != null ? HostName.ToLower().GetHashCode() : 0);
		}

		#endregion

		#region ToString

		public override string ToString()
		{
			return HostName;
		}

		#endregion
	}
}

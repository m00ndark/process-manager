using System;
using ProcessManager.Utilities;

namespace ProcessManager.DataObjects
{
	public class Machine : IIDObject
	{
		public const string DEFAULT_HOST_NAME = "<host-name>";

		public Machine() : this(DEFAULT_HOST_NAME) {}

		public Machine(string hostName)
		{
			ID = Cryptographer.CreateGUID(hostName);
			HostName = hostName;
		}

		#region Properties

		public Guid ID { get; private set; }
		public string HostName { get; set; }

		#endregion

		#region Equality

		public bool Equals(string hostName)
		{
			return (HostName != null && hostName != null && hostName.Equals(HostName, StringComparison.CurrentCultureIgnoreCase));
		}

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

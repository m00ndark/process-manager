using System;

namespace ProcessManager.DataObjects
{
	public class Machine
	{
		public Machine(string hostName)
		{
			HostName = hostName;
		}

		public string HostName { get; private set; }

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
	}
}

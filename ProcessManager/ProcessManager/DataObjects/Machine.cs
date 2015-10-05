using System;
using ProcessManager.DataObjects.Comparers;
using ToolComponents.Core;

namespace ProcessManager.DataObjects
{
	public class Machine : IIDObject
	{
		public const string DEFAULT_HOST_NAME = "<host-name>";

		private string _hostName;

		public Machine() : this(DEFAULT_HOST_NAME) {}

		public Machine(string hostName)
		{
			HostName = hostName;
		}

		#region Properties

		public Guid ID { get; private set; }

		public string HostName
		{
			get { return _hostName; }
			set
			{
				_hostName = value;
				ID = Cryptographer.CreateGuid(_hostName);
			}
		}

		#endregion

		#region Equality

		public bool Equals(string hostName)
		{
			return Comparer.MachinesEqual(this, hostName);
		}

		public override bool Equals(object obj)
		{
			Machine machine = obj as Machine;
			return (machine != null && Comparer.MachinesEqual(this, machine));
		}

		public override int GetHashCode()
		{
			return Comparer.GetHashCode(this);
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

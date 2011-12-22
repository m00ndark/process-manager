namespace ProcessManager.DataObjects
{
	public class Machine
	{
		public Machine(string hostName)
		{
			HostName = hostName;
		}

		public string HostName { get; private set; }
	}
}

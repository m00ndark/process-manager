using System;
using System.ServiceProcess;
using ProcessManager.Utilities;

namespace ProcessManagerService
{
	internal static class Program
	{
		private static void Main()
		{
			Logger.LogSource = LogSource.Server;
			if (Environment.CommandLine.Contains("-debug"))
			{
				ProcessManager.ProcessManager.Instance.Start();
			}
			else
			{
				ServiceBase[] servicesToRun = new ServiceBase[] { new ProcessManagerService() };
				ServiceBase.Run(servicesToRun);
			}
		}
	}
}

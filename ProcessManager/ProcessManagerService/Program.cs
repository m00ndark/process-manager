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
				new ProcessManager.ProcessManager().Start();
			}
			else
			{
				ServiceBase[] servicesToRun = new ServiceBase[] { new ProcessManagerService() };
				ServiceBase.Run(servicesToRun);
			}
		}
	}
}

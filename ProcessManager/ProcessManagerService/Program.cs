using System;
using System.ServiceProcess;

namespace ProcessManagerService
{
	internal static class Program
	{
		private static void Main()
		{
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

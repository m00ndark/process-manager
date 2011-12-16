using System.ServiceProcess;

namespace ProcessManagerService
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		private static void Main()
		{
			ServiceBase[] servicesToRun = new ServiceBase[] { new ProcessManagerService() };
			ServiceBase.Run(servicesToRun);
		}
	}
}

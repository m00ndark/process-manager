using System;
using ProcessManager;
using ToolComponents.Core;
using ToolComponents.Core.Logging;

namespace ProcessManagerService
{
	internal static class Program
	{
		internal const string SERVICE_NAME = "ProcessManagerService";

		private static void Main()
		{
			Logger.Initialize(Settings.Service.Defaults.APPLICATION_NAME, DefaultSourcedLogEntry.Headers, LogSource.Server, Settings.Service.Defaults.LOG_TYPE_MIN_LEVEL);

			try
			{
				ServiceStartupSequence.Run<ProcessManagerService>(SERVICE_NAME, ProcessManager.ProcessManager.Instance.Start);
			}
			catch (Exception ex)
			{
				Logger.Add("Fatal error", ex);
			}

			Logger.Flush();
		}
	}
}

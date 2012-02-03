using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using ProcessManager.DataObjects;
using ProcessManager.Utilities;

namespace ProcessManager.DataAccess
{
	public static class ProcessHandler
	{
		public static List<string> GetProcesses(List<Application> applications)
		{
			return applications.SelectMany(application => GetProcesses(application).Select(process => process.MainModule.FileName)).ToList();
		}

		public static void Start(Group group, Application application)
		{
			Start(group, application, false);
		}

		private static void Start(Group group, Application application, bool waitUntilStopped)
		{
			if (waitUntilStopped)
				WaitForProcessToTerminate(group, application);

			if (ProcessExists(group, application))
				return;

			string fullPath = Path.Combine(group.Path, application.RelativePath.TrimStart('\\'));
			Process process = new Process() { StartInfo = { FileName = fullPath, Arguments = application.Arguments } };
			try
			{
				process.Start();
			}
			catch (Exception ex)
			{
				Logger.Add("Failed to start process with path " + fullPath, ex);
			}
		}

		public static bool Stop(Group group, Application application)
		{
			bool success = true;
			foreach (Process process in GetProcesses(group, application))
			{
				try
				{
					process.Kill();
				}
				catch (Exception ex)
				{
					success = false;
					Logger.Add("Failed to kill process with path " + process.MainModule.FileName, ex);
				}
			}
			return success;
		}

		public static void Restart(Group group, Application application)
		{
			if (Stop(group, application))
				Start(group, application, true);
		}

		private static void WaitForProcessToTerminate(Group group, Application application)
		{
			DateTime timeout = DateTime.Now.AddSeconds(30);
			while (DateTime.Now < timeout && ProcessExists(group, application))
				Thread.Sleep(200);
		}

		private static bool ProcessExists(Group group, Application application)
		{
			return (GetProcesses(group, application).Count() > 0);
		}

		private static IEnumerable<Process> GetProcesses(Group group, Application application)
		{
			string fullPath = Path.GetDirectoryName(Path.Combine(group.Path, application.RelativePath.TrimStart('\\')));
			return GetProcesses(application)
				.Select(process => new
					{
						Process = process,
						Path = Path.GetDirectoryName(process.MainModule.FileName)
					})
				.Where(x => x.Path != null && x.Path.Equals(fullPath, StringComparison.CurrentCultureIgnoreCase))
				.Select(x => x.Process);
		}

		private static IEnumerable<Process> GetProcesses(Application application)
		{
			return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(application.RelativePath));
		}
	}
}

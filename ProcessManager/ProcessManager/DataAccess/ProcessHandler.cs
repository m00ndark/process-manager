using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using ProcessManager.DataObjects;
using ProcessManager.Exceptions;
using ProcessManager.Utilities;

namespace ProcessManager.DataAccess
{
	public static class ProcessHandler
	{
		private const int WAIT_FOR_EXIT_TIMEOUT = 60000;

		public static void Start(Group group, Application application)
		{
			Start(group, application, false);
		}

		private static void Start(Group group, Application application, bool waitUntilStopped)
		{
			if (waitUntilStopped)
				WaitForProcessToTerminate(group, application);

			if (ProcessExists(group, application))
				throw new ProcessActionException("Could not start process as it is already running");

			string fullPath = Path.Combine(group.Path, application.RelativePath.TrimStart('\\'));
			Process process = new Process() { StartInfo = { FileName = fullPath, Arguments = application.Arguments } };
			try
			{
				process.Start();
			}
			catch (Exception ex)
			{
				Logger.AddAndThrow<ProcessActionException>("Failed to start process with path " + fullPath, ex);
			}

			if (!application.WaitForExit)
				return;

			if (process.WaitForExit(WAIT_FOR_EXIT_TIMEOUT))
			{
				switch (application.SuccessExitCode)
				{
					case SuccessExitCode.Zero:
						if (process.ExitCode != 0) throw new ProcessActionException("Process exit code was " + process.ExitCode + " (expected zero)");
						break;
					case SuccessExitCode.Positive:
						if (process.ExitCode < 0) throw new ProcessActionException("Process exit code was " + process.ExitCode + " (expected positive)");
						break;
					case SuccessExitCode.Negative:
						if (process.ExitCode >= 0) throw new ProcessActionException("Process exit code was " + process.ExitCode + " (expected negative)");
						break;
				}
			}
			else
				throw new ProcessActionException("Process did not exit within " + WAIT_FOR_EXIT_TIMEOUT + " milliseconds");
		}

		public static void Stop(Group group, Application application)
		{
			foreach (Process process in GetProcesses(group, application))
			{
				try
				{
					process.Kill();
				}
				catch (Exception ex)
				{
					Logger.AddAndThrow<ProcessActionException>("Failed to kill process with path " + process.GetPathName(), ex);
				}
			}
		}

		public static void Restart(Group group, Application application)
		{
			Stop(group, application);
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
			return GetProcesses(group, application).Any();
		}

		private static IEnumerable<Process> GetProcesses(Group group, Application application)
		{
			string fullPath = Path.GetDirectoryName(Path.Combine(group.Path, application.RelativePath.TrimStart('\\')));
			return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(application.RelativePath))
				.Select(process => new
					{
						Process = process,
						Path = Path.GetDirectoryName(process.GetPathName())
					})
				.Where(x => x.Path != null && x.Path.Equals(fullPath, StringComparison.CurrentCultureIgnoreCase))
				.Select(x => x.Process);
		}

		public static List<string> GetProcesses()
		{
			return GetProcessesUsingWMI().ToList();
		}

		private static IEnumerable<string> GetProcessesUsingWMI()
		{
			const string WMI_QUERY_STRING = "SELECT ExecutablePath FROM Win32_Process";
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(WMI_QUERY_STRING))
			{
				using (ManagementObjectCollection results = searcher.Get())
				{
					foreach (ManagementObject obj in results.Cast<ManagementObject>().Where(obj => obj["ExecutablePath"] != null))
					{
						yield return obj["ExecutablePath"].ToString();
					}
				}
			}
		}
	}
}

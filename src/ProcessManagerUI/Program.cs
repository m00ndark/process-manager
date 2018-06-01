using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManagerUI.Forms;
using ProcessManagerUI.Support;
using ToolComponents.Core.Logging;

namespace ProcessManagerUI
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
            bool createdNew;
			using (new Mutex(true, "ProcessManager", out createdNew))
			{
				if (createdNew)
				{
					Logger.Initialize(Settings.Service.Defaults.APPLICATION_NAME, DefaultSourcedLogEntry.Headers, LogSource.Client, Settings.Service.Defaults.LOG_TYPE_MIN_LEVEL);

					if (Environment.CommandLine.Contains("-migratesettings"))
					{
						RegistryHandler.MigrateSettings();

						if (Settings.Client.StartWithWindows)
							RegistryHandler.SetWindowsStartupTrigger(Application.ExecutablePath);

						Logger.Add("Settings migrated");
					}
					else
					{
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						Application.Run(new ControlPanelForm());
					}
					Logger.Flush();
				}
				else
				{
					Process currentProcess = Process.GetCurrentProcess();
					Process process = Process.GetProcessesByName(currentProcess.ProcessName).FirstOrDefault(x => x.Id != currentProcess.Id);
					if (process != null) try { NativeMethods.SetForegroundWindow(process.MainWindowHandle); } catch { ; }
				}
			}
		}
	}
}

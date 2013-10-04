using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.Utilities;
using ProcessManagerUI.Forms;
using ProcessManagerUI.Support;

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
					Logger.LogSource = LogSource.Client;

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

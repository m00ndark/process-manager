using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
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
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new ControlPanelForm());
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

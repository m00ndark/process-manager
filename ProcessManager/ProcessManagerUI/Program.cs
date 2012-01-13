using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ProcessManager.Utilities;
using ProcessManagerUI.Forms;

namespace ProcessManagerUI
{
	static class Program
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		[STAThread]
		static void Main()
		{
            bool createdNew;
			using (Mutex mutex = new Mutex(true, "ProcessManager", out createdNew))
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
					if (process != null) try { SetForegroundWindow(process.MainWindowHandle); } catch { ; }
				}
			}
		}
	}
}

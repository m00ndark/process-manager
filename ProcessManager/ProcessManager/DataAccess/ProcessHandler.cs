using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProcessManager.DataAccess
{
	public static class ProcessHandler
	{
		public static List<string> GetProcesses(List<string> processNames)
		{
			return processNames.SelectMany(processName => Process.GetProcessesByName(processName).Select(process => process.MainModule.FileName)).ToList();
		}
	}
}

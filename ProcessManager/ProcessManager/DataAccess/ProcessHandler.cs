using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

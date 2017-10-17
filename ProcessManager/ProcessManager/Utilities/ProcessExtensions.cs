using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ProcessManager.Utilities
{
	public static class ProcessExtensions
	{
		#region Interops

		[DllImport("psapi.dll")]
		private static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpFilename, uint nSize);

		[DllImport("psapi.dll")]
		private static extern uint GetProcessImageFileName(IntPtr hProcess, StringBuilder lpImageFileName, uint nSize);

		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

		[DllImport("kernel32.dll")]
		private static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, StringBuilder lpExeName, ref uint lpdwSize);

		[DllImport("kernel32.dll")]
		private static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, uint uuchMax);

		[DllImport("kernel32.dll")]
		private static extern bool CloseHandle(IntPtr hObject);

		[Flags]
		private enum ProcessAccessFlags : uint
		{
			Read = 0x10, // PROCESS_VM_READ
			QueryInformation = 0x400 // PROCESS_QUERY_INFORMATION
		}

		#endregion

		private const uint PATH_BUFFER_SIZE = 512; // plenty big enough

		public static string GetPathName(this Process process)
		{
			uint processID = (uint) process.Id;
			StringBuilder pathBuffer = new StringBuilder((int) PATH_BUFFER_SIZE);

			// Try the GetModuleFileName method first since it's the fastest. 
			// May return ACCESS_DENIED (due to VM_READ flag) if the process is not owned by the current user.
			// Will fail if we are compiled as x86 and we're trying to open a 64 bit process...not allowed.
			IntPtr hProcess = OpenProcess(ProcessAccessFlags.QueryInformation | ProcessAccessFlags.Read, false, processID);
			if (hProcess != IntPtr.Zero)
			{
				try
				{
					if (GetModuleFileNameEx(hProcess, IntPtr.Zero, pathBuffer, PATH_BUFFER_SIZE) > 0)
					{
						return pathBuffer.ToString();
					}
				}
				finally
				{
					CloseHandle(hProcess);
				}
			}

			hProcess = OpenProcess(ProcessAccessFlags.QueryInformation, false, processID);
			if (hProcess != IntPtr.Zero)
			{
				try
				{
					// Try this method for Vista or higher operating systems
					uint size = PATH_BUFFER_SIZE;
					if (Environment.OSVersion.Version.Major >= 6 && QueryFullProcessImageName(hProcess, 0, pathBuffer, ref size) && size > 0)
					{
						return pathBuffer.ToString();
					}

					// Try the GetProcessImageFileName method
					if (GetProcessImageFileName(hProcess, pathBuffer, PATH_BUFFER_SIZE) > 0)
					{
						string dosPath = pathBuffer.ToString();
						foreach (string drive in Environment.GetLogicalDrives())
						{
							if (QueryDosDevice(drive.TrimEnd('\\'), pathBuffer, PATH_BUFFER_SIZE) > 0)
							{
								if (dosPath.StartsWith(pathBuffer.ToString()))
								{
									return drive + dosPath.Remove(0, pathBuffer.Length);
								}
							}
						}
					}
				}
				finally
				{
					CloseHandle(hProcess);
				}
			}

			return string.Empty;
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.Utilities;

namespace ProcessManager
{
	public class ProcessManager
	{
		private Thread _mainThread;
		private IDictionary<Guid, IDictionary<Guid, ApplicationStatus>> _applicationStatuses;

		public ProcessManager()
		{
			_applicationStatuses = new Dictionary<Guid, IDictionary<Guid, ApplicationStatus>>();
		}

		#region Properties

		public bool IsRunning { get { return (_mainThread != null); } }

		#endregion

		public void Start()
		{
			if (!IsRunning)
			{
				_mainThread = new Thread(MainThread);
				_mainThread.Start();
			}
		}

		public void ShutDown()
		{
			if (IsRunning)
			{
				_mainThread.Abort();
				_mainThread = null;
			}
		}

		private void MainThread()
		{
			EnterMainLoop();
		}

		private void EnterMainLoop()
		{
			try
			{
				while (true)
				{
					Configuration configuration = Configuration.Read();

					List<string> processNames = configuration.Applications.Select(application => Path.GetFileNameWithoutExtension(application.RelativePath)).ToList();
					List<string> runningProcesses = ProcessHandler.GetProcesses(processNames);
					List<string> applicationFilePaths = configuration.Groups
						.Select(group => new
							{
								RootPath = group.Path,
								RelativePaths = configuration.Applications.Where(application => group.Applications.Contains(application.ID)).Select(application => application.RelativePath)
							})
						.SelectMany(x => x.RelativePaths.Select(relativePath => Path.Combine(x.RootPath, relativePath)))
						.ToList();


					Thread.Sleep(Settings.Read<int>("StatusUpdateInterval"));
				}
			}
			catch (ThreadAbortException)
			{
				Logger.Add("Exiting..");
			}
			catch (Exception ex)
			{
				Logger.Add("Fatal exception in main loop, dying....", ex);
			}
		}
	}
}

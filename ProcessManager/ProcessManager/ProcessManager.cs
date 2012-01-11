using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Host;
using ProcessManager.Utilities;

namespace ProcessManager
{
	public class ProcessManager : IProcessManagerEventProvider
	{
		private Thread _mainThread;
		private Dictionary<Guid, Dictionary<Guid, ApplicationStatus>> _applicationStatuses;

		public ProcessManager()
		{
			_mainThread = null;
			_applicationStatuses = new Dictionary<Guid, Dictionary<Guid, ApplicationStatus>>();
		}

		#region Events

		public event EventHandler<ApplicationStatusesEventArgs> ApplicationStatusesChanged;

		#endregion

		#region Properties

		public bool IsRunning { get { return (_mainThread != null); } }

		#endregion

		#region Event raisers

		private void RaiseApplicationStatusesChangedEvent(List<ApplicationStatus> applicationStatuses)
		{
			if (ApplicationStatusesChanged != null)
				ApplicationStatusesChanged(this, new ApplicationStatusesEventArgs(applicationStatuses));
		}

		#endregion

		#region Operations

		public static Configuration GetConfiguration()
		{
			return Configuration.Read();
		}

		public static void SetConfiguration(Configuration configuration)
		{
			Configuration.Write(configuration);
		}

		#endregion

		#region Start, shut down, main thread and loop

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
			}
		}

		private void MainThread()
		{
			try
			{
				ProcessManagerServiceHost.Open(this);

				Thread.Sleep(2000);

				EnterMainLoop();

				ProcessManagerServiceHost.Close();

				Logger.Add("ABORTING -- Shut down completed, signing off");
			}
			catch (Exception ex)
			{
				Logger.Add("Fatal exception in main thread, dying...", ex);
			}
			finally
			{
				// indicate that we are no longer running
				_mainThread = null;
			}
		}

		private void EnterMainLoop()
		{
			try
			{
				while (true)
				{
					try
					{
						Configuration configuration = Configuration.Read();

						if (configuration.Applications.Count > 0 && configuration.Groups.Sum(group => group.Applications.Count) > 0)
						{
							List<string> processNames = configuration.Applications.Select(application => Path.GetFileNameWithoutExtension(application.RelativePath)).ToList();
							List<string> runningProcesses = ProcessHandler.GetProcesses(processNames);

							var applicationsStatusList = configuration.Groups
								.SelectMany(group => configuration.Applications
									.Where(application => group.Applications.Contains(application.ID))
									.Select(application => new
										{
											Group = group,
											Application = application,
											Path = Path.Combine(group.Path, application.RelativePath.TrimStart('\\'))
										}))
								.Select(x => new
									{
										x.Group,
										x.Application,
										ApplicationStatus = new ApplicationStatus(x.Group.ID, x.Application.ID,
											runningProcesses.Any(runningProcess => runningProcess.Equals(x.Path, StringComparison.CurrentCultureIgnoreCase)))
									})
								.ToList();

							List<ApplicationStatus> changedApplicationStatuses;
							lock (_applicationStatuses)
							{
								changedApplicationStatuses = applicationsStatusList
									.Where(x => !_applicationStatuses.ContainsKey(x.Group.ID) || !_applicationStatuses[x.Group.ID].ContainsKey(x.Application.ID)
										|| _applicationStatuses[x.Group.ID][x.Application.ID].IsRunning != x.ApplicationStatus.IsRunning)
									.Select(x => x.ApplicationStatus)
									.ToList();

								_applicationStatuses = applicationsStatusList
									.GroupBy(x => x.Group.ID)
									.ToDictionary(x => x.Key, x => x.ToDictionary(y => y.Application.ID, y => y.ApplicationStatus));
							}

							if (changedApplicationStatuses.Count > 0)
								RaiseApplicationStatusesChangedEvent(changedApplicationStatuses);
						}
					}
					catch (ThreadAbortException) { throw; }
					catch (Exception ex)
					{
						Logger.Add("An unexpected error occurred in main loop", ex);
					}

					Thread.Sleep(Settings.Service.Read<int>("StatusUpdateInterval"));
				}
			}
			catch (ThreadAbortException)
			{
				Logger.Add("ABORTING -- Exiting main loop...");
			}
			catch (Exception ex)
			{
				Logger.Add("Fatal exception in main loop, dying....", ex);
			}
		}

		#endregion
	}
}

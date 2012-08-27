using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.DataObjects;
using ProcessManager.Service.Host;
using ProcessManager.Utilities;

namespace ProcessManager
{
	public class ProcessManager : IProcessManagerEventProvider
	{
		private static volatile ProcessManager _instance;
		private static readonly object _lock = new object();

		private Thread _mainThread;
		private Dictionary<Guid, Dictionary<Guid, ApplicationStatus>> _applicationStatuses;

		private ProcessManager()
		{
			_mainThread = null;
			_applicationStatuses = new Dictionary<Guid, Dictionary<Guid, ApplicationStatus>>();
		}

		#region Events

		public event EventHandler<ApplicationStatusesEventArgs> ApplicationStatusesChanged;
		public event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;

		#endregion

		#region Properties

		public static ProcessManager Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock)
					{
						if (_instance == null)
							_instance = new ProcessManager();
					}
				}
				return _instance;
			}
		}

		public bool IsRunning { get { return (_mainThread != null); } }

		#endregion

		#region Event raisers

		private void RaiseApplicationStatusesChangedEvent(List<ApplicationStatus> applicationStatuses)
		{
			if (ApplicationStatusesChanged != null)
				ApplicationStatusesChanged(this, new ApplicationStatusesEventArgs(applicationStatuses));
		}

		private void RaiseConfigurationChangedEvent(Configuration configuration)
		{
			if (ConfigurationChanged != null)
				ConfigurationChanged(this, new MachineConfigurationHashEventArgs(configuration.Hash));
		}

		#endregion

		#region Operations

		public Configuration GetConfiguration()
		{
			return Configuration.Read();
		}

		public void SetConfiguration(Configuration configuration)
		{
			Configuration.Write(configuration);
			RaiseConfigurationChangedEvent(configuration);
		}

		public List<ApplicationStatus> GetAllApplicationStatuses()
		{
			lock (_applicationStatuses)
				return _applicationStatuses.Values.SelectMany(x => x.Values).ToList();
		}

		public void TakeApplicationAction(Guid groupID, Guid applicationID, ApplicationActionType type)
		{
			Configuration configuration = Configuration.Read();
			Group group = configuration.Groups.FirstOrDefault(x => x.ID == groupID);
			Application application = configuration.Applications.FirstOrDefault(x => x.ID == applicationID);

			if (group == null)
			{
				Logger.Add(LogType.Error, "Application " + type + ": Could not find group with ID " + groupID);
				return;
			}

			if (application == null)
			{
				Logger.Add(LogType.Error, "Application " + type + ": Could not find application with ID " + applicationID);
				return;
			}

			switch (type)
			{
				case ApplicationActionType.Start:
					ProcessHandler.Start(group, application);
					break;
				case ApplicationActionType.Stop:
					ProcessHandler.Stop(group, application);
					break;
				case ApplicationActionType.Restart:
					ProcessHandler.Restart(group, application);
					break;
			}
		}

		public List<FileSystemDrive> GetFileSystemDrives()
		{
			return FileSystemHandler.GetDrives().ToList();
		}

		public List<FileSystemEntry> GetFileSystemEntries(string path, string filter)
		{
			return FileSystemHandler.GetFileSystemEntries(path, filter).ToList();
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
							List<string> runningProcesses = ProcessHandler.GetProcesses(configuration.Applications);

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
											runningProcesses.Any(runningProcess => runningProcess.Equals(x.Path, StringComparison.CurrentCultureIgnoreCase))
												? ApplicationStatusValue.Running : ApplicationStatusValue.Stopped)
									})
								.ToList();

							List<ApplicationStatus> changedApplicationStatuses;
							lock (_applicationStatuses)
							{
								changedApplicationStatuses = applicationsStatusList
									.Where(x => !_applicationStatuses.ContainsKey(x.Group.ID) || !_applicationStatuses[x.Group.ID].ContainsKey(x.Application.ID)
										|| _applicationStatuses[x.Group.ID][x.Application.ID].Status != x.ApplicationStatus.Status)
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

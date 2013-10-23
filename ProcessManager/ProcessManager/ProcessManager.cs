using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Exceptions;
using ProcessManager.Service.Common;
using ProcessManager.Service.Host;
using ProcessManager.Utilities;

namespace ProcessManager
{
	public class ProcessManager : IProcessManagerEventProvider
	{
		private static volatile ProcessManager _instance;
		private static readonly object _lock = new object();

		private Thread _mainThread;
		private volatile bool _shutDownRequested;
		private Dictionary<Guid, Dictionary<Guid, ProcessStatus>> _processStatuses;

		private ProcessManager()
		{
			_mainThread = null;
			_shutDownRequested = false;
			_processStatuses = new Dictionary<Guid, Dictionary<Guid, ProcessStatus>>();
			Logger.LogTypeMinLevel = Settings.Service.Read<LogType>("LogTypeMinLevel");
			DistributionWorker.Instance.DistributionCompleted += DistributionWorker_DistributionCompleted;
		}

		#region Events

		public event EventHandler<ProcessStatusesEventArgs> ProcessStatusesChanged;
		public event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;
		public event EventHandler<DistributionResultEventArgs> DistributionCompleted;

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

		private void RaiseProcessStatusesChangedEvent(List<ProcessStatus> processStatuses)
		{
			if (ProcessStatusesChanged != null)
				ProcessStatusesChanged(this, new ProcessStatusesEventArgs(processStatuses));
		}

		private void RaiseConfigurationChangedEvent(Configuration configuration)
		{
			if (ConfigurationChanged != null)
				ConfigurationChanged(this, new MachineConfigurationHashEventArgs(configuration.Hash));
		}

		private void RaiseDistributionCompletedEvent(DistributionResult distributionResult, IProcessManagerServiceEventHandler caller)
		{
			if (DistributionCompleted != null)
				DistributionCompleted(this, new DistributionResultEventArgs(distributionResult, caller));
		}

		#endregion

		#region Distribution worker event handlers

		private void DistributionWorker_DistributionCompleted(object sender, DistributionResultEventArgs e)
		{
			RaiseDistributionCompletedEvent(e.DistributionResult, e.Caller);
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

		public List<ProcessStatus> GetAllProcessStatuses()
		{
			lock (_processStatuses)
				return _processStatuses.Values.SelectMany(x => x.Values).ToList();
		}

		public ProcessActionResult TakeProcessAction(Guid groupID, Guid applicationID, ActionType type)
		{
			string errorMessage = null;

			try
			{
				Configuration configuration = Configuration.Read();
				Group group = configuration.Groups.FirstOrDefault(x => x.ID == groupID);
				Application application = configuration.Applications.FirstOrDefault(x => x.ID == applicationID);

				if (group == null)
					Logger.AddAndThrow<ProcessActionException>(LogType.Error, "Application " + type + ": Could not find group with ID " + groupID);

				if (application == null)
					Logger.AddAndThrow<ProcessActionException>(LogType.Error, "Application " + type + ": Could not find application with ID " + applicationID);

				switch (type)
				{
					case ActionType.Start:
						ProcessHandler.Start(group, application);
						break;
					case ActionType.Stop:
						ProcessHandler.Stop(group, application);
						break;
					case ActionType.Restart:
						ProcessHandler.Restart(group, application);
						break;
					default:
						Logger.AddAndThrow<ProcessActionException>(LogType.Error, "Process action type invalid: " + type);
						break;
				}
			}
			catch (ProcessActionException ex)
			{
				errorMessage = ex.Message;
			}

			return new ProcessActionResult(type, errorMessage,
				_processStatuses.ContainsKey(groupID) && _processStatuses[groupID].ContainsKey(applicationID) ? _processStatuses[groupID][applicationID] : null);
		}

		public bool TakeDistributionAction(string sourceMachineHostName, Guid groupID, Guid applicationID, string destinationMachineHostName, ActionType type, IProcessManagerServiceEventHandler caller)
		{
			Configuration configuration = Configuration.Read();
			Group group = configuration.Groups.FirstOrDefault(x => x.ID == groupID);
			Application application = configuration.Applications.FirstOrDefault(x => x.ID == applicationID);

			if (string.IsNullOrEmpty(sourceMachineHostName))
			{
				Logger.Add(LogType.Error, "Application " + type + ": Missing source machine host name");
				return false;
			}

			if (group == null)
			{
				Logger.Add(LogType.Error, "Application " + type + ": Could not find group with ID " + groupID);
				return false;
			}

			if (application == null)
			{
				Logger.Add(LogType.Error, "Application " + type + ": Could not find application with ID " + applicationID);
				return false;
			}

			if (string.IsNullOrEmpty(destinationMachineHostName))
			{
				Logger.Add(LogType.Error, "Application " + type + ": Missing destination machine host name");
				return false;
			}

			DistributionWorker.Instance.AddWork(new DistributionWork(type, new Machine(sourceMachineHostName), group, application, new Machine(destinationMachineHostName), caller));
			return true;
		}

		public List<FileSystemDrive> GetFileSystemDrives()
		{
			return FileSystemHandler.GetFileSystemDrives().ToList();
		}

		public List<FileSystemEntry> GetFileSystemEntries(string path, string filter)
		{
			return FileSystemHandler.GetFileSystemEntries(path, filter, SearchOption.TopDirectoryOnly).ToList();
		}

		public bool DistributeFile(DistributionFile distributionFile)
		{
			Configuration configuration = Configuration.Read();
			Group group = configuration.Groups.FirstOrDefault(x => x.ID == distributionFile.DestinationGroupID);

			if (group == null)
			{
				Logger.Add(LogType.Error, "Distribute file: Could not find group with ID " + distributionFile.DestinationGroupID);
				return false;
			}

			string filePath = Path.Combine(group.Path, distributionFile.RelativePath.Trim(Path.DirectorySeparatorChar));

			try
			{
				FileSystemHandler.PutFileContent(filePath, distributionFile.Content);
				return true;
			}
			catch (Exception ex)
			{
				Logger.Add("Distribute file: Failed to persist file content of " + filePath, ex);
				return false;
			}
		}

		#endregion

		#region Start, shut down, main thread and loop

		public void Start()
		{
			if (IsRunning)
				return;

			_shutDownRequested = false;
			_mainThread = new Thread(MainThread);
			_mainThread.Start();
		}

		public void ShutDown()
		{
			if (IsRunning)
				_shutDownRequested = true;
		}

		private void MainThread()
		{
			try
			{
				Logger.Add("STARTUP -- Initializing...");

				DistributionWorker.Instance.Start();

				ProcessManagerServiceHost.Open(this);

				Thread.Sleep(2000);

				try
				{
					EnterMainLoop();
				}
				finally
				{
					ProcessManagerServiceHost.Close();

					DistributionWorker.Instance.ShutDown();
				}

				Logger.Add("SHUTDOWN -- Shut down completed, signing off");
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
				Logger.Add("STARTUP -- Entering main loop...");

				while (!_shutDownRequested)
				{
					try
					{
						Configuration configuration = Configuration.Read();

						if (configuration.Applications.Count > 0 && configuration.Groups.Sum(group => group.Applications.Count) > 0)
						{
							List<string> runningProcesses = ProcessHandler.GetProcesses(configuration.Applications);

							var processesStatusList = configuration.Groups
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
										Status = new ProcessStatus(x.Group.ID, x.Application.ID,
											runningProcesses.Any(runningProcess => runningProcess.Equals(x.Path, StringComparison.CurrentCultureIgnoreCase))
												? ProcessStatusValue.Running : ProcessStatusValue.Stopped)
									})
								.ToList();

							List<ProcessStatus> changedProcessStatuses;
							lock (_processStatuses)
							{
								changedProcessStatuses = processesStatusList
									.Where(x => !_processStatuses.ContainsKey(x.Group.ID) || !_processStatuses[x.Group.ID].ContainsKey(x.Application.ID)
										|| _processStatuses[x.Group.ID][x.Application.ID].Value != x.Status.Value)
									.Select(x => x.Status)
									.ToList();

								_processStatuses = processesStatusList
									.GroupBy(x => x.Group.ID)
									.ToDictionary(x => x.Key, x => x.ToDictionary(y => y.Application.ID, y => y.Status));
							}

							if (changedProcessStatuses.Count > 0)
								RaiseProcessStatusesChangedEvent(changedProcessStatuses);
						}
					}
					catch (Exception ex)
					{
						Logger.Add("An unexpected error occurred in main loop", ex);
					}

					Thread.Sleep(Settings.Service.Read<int>("StatusUpdateInterval"));
				}

				Logger.Add("SHUTDOWN -- Exiting main loop...");
			}
			catch (Exception ex)
			{
				Logger.Add("Fatal exception in main loop, dying....", ex);
			}
		}

		#endregion
	}
}

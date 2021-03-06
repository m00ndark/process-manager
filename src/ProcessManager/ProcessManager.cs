﻿using System;
using System.Collections.Concurrent;
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
using ToolComponents.Core.Logging;

namespace ProcessManager
{
	public class ProcessManager : IProcessManagerEventProvider
	{
		private static volatile ProcessManager _instance;
		private static readonly object _lock = new object();

		private Thread _mainThread;
		private volatile bool _shutDownRequested;
		private Dictionary<Guid, Dictionary<Guid, ProcessStatus>> _processStatuses;
		private readonly ConcurrentDictionary<Guid, bool> _processStatusListeners;

		private ProcessManager()
		{
			_mainThread = null;
			_shutDownRequested = false;
			_processStatuses = new Dictionary<Guid, Dictionary<Guid, ProcessStatus>>();
			_processStatusListeners = new ConcurrentDictionary<Guid, bool>();
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

		public bool IsRunning => _mainThread != null;

		#endregion

		#region Event raisers

		private void RaiseProcessStatusesChangedEvent(List<ProcessStatus> processStatuses)
		{
			ProcessStatusesChanged?.Invoke(this, new ProcessStatusesEventArgs(processStatuses, _processStatusListeners.Where(x => x.Value).Select(x => x.Key).ToArray()));
		}

		private void RaiseConfigurationChangedEvent(Configuration configuration)
		{
			ConfigurationChanged?.Invoke(this, new MachineConfigurationHashEventArgs(configuration.Hash));
		}

		private void RaiseDistributionCompletedEvent(DistributionResult distributionResult, Guid clientId)
		{
			DistributionCompleted?.Invoke(this, new DistributionResultEventArgs(distributionResult, clientId));
		}

		#endregion

		#region Distribution worker event handlers

		private void DistributionWorker_DistributionCompleted(object sender, DistributionResultEventArgs e)
		{
			RaiseDistributionCompletedEvent(e.DistributionResult, e.ClientId);
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

		public void ActivateProcessStatusNotifications(Guid clientId)
		{
			_processStatusListeners[clientId] = true;
		}

		public void DeactivateProcessStatusNotifications(Guid clientId)
		{
			_processStatusListeners[clientId] = false;
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
					Logger.AddAndThrow<ProcessActionException>(LogType.Error, $"Application {type}: Could not find group with ID {groupID}");

				if (application == null)
					Logger.AddAndThrow<ProcessActionException>(LogType.Error, $"Application {type}: Could not find application with ID {applicationID}");

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
						Logger.AddAndThrow<ProcessActionException>(LogType.Error, $"Process action type invalid: {type}");
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

		public DistributionActionResult TakeDistributionAction(string sourceMachineHostName, Guid groupID, Guid applicationID,
			string destinationMachineHostName, ActionType type, Guid clientId)
		{
			string errorMessage = null;

			try
			{
				Configuration configuration = Configuration.Read();
				Group group = configuration.Groups.FirstOrDefault(x => x.ID == groupID);
				Application application = configuration.Applications.FirstOrDefault(x => x.ID == applicationID);

				if (string.IsNullOrEmpty(sourceMachineHostName))
					Logger.AddAndThrow<DistributionActionException>(LogType.Error, $"Application {type}: Missing source machine host name");

				if (group == null)
					Logger.AddAndThrow<DistributionActionException>(LogType.Error, $"Application {type}: Could not find group with ID {groupID}");

				if (application == null)
					Logger.AddAndThrow<DistributionActionException>(LogType.Error, $"Application {type}: Could not find application with ID {applicationID}");

				if (string.IsNullOrEmpty(destinationMachineHostName))
					Logger.AddAndThrow<DistributionActionException>(LogType.Error, $"Application {type}: Missing destination machine host name");

				DistributionWorker.Instance.AddWork(new DistributionWork(type, new Machine(sourceMachineHostName), group, application, new Machine(destinationMachineHostName), clientId));
			}
			catch (DistributionActionException ex)
			{
				errorMessage = ex.Message;
			}

			return new DistributionActionResult(type, errorMessage);
		}

		public List<FileSystemDrive> GetFileSystemDrives()
		{
			return FileSystemHandler.GetFileSystemDrives().ToList();
		}

		public List<FileSystemEntry> GetFileSystemEntries(string path, string filter)
		{
			return FileSystemHandler.GetFileSystemEntries(path, filter, SearchOption.TopDirectoryOnly).ToList();
		}

		public DistributeFileResult DistributeFile(DistributionFile distributionFile)
		{
			string errorMessage = null;

			try
			{
				Configuration configuration = Configuration.Read();
				Group group = configuration.Groups.FirstOrDefault(x => x.ID == distributionFile.DestinationGroupID);

				if (group == null)
					Logger.AddAndThrow<DistributeFileException>(LogType.Error, $"Could not find group with ID {distributionFile.DestinationGroupID}");

				string filePath = Path.Combine(group.Path, distributionFile.RelativePath.Trim(Path.DirectorySeparatorChar));

				try
				{
					FileSystemHandler.PutFileContent(filePath, distributionFile.Content);
				}
				catch (Exception ex)
				{
					Logger.AddAndThrow<DistributeFileException>($"Failed to persist file content of {filePath}", ex);
				}
			}
			catch (DistributeFileException ex)
			{
				errorMessage = ex.Message;
			}

			return new DistributeFileResult(distributionFile.RelativePath, distributionFile.DestinationGroupID, errorMessage);
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
					if (_processStatusListeners.Any(x => x.Value))
					{
						try
						{
							Configuration configuration = Configuration.Read();

							if (configuration.Applications.Count > 0 && configuration.Groups.Sum(group => group.Applications.Count) > 0)
							{
								List<string> runningProcesses = ProcessHandler.GetProcesses();

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

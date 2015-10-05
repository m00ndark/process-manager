using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.DataObjects.Comparers;
using ProcessManager.EventArguments;
using ProcessManager.Exceptions;
using ProcessManager.Service.Client;
using ProcessManager.Service.Common;
using ProcessManager.Service.DataObjects;
using ToolComponents.Core.Logging;

namespace ProcessManager
{
	public class DistributionWorker : IProcessManagerEventHandler
	{
		private static volatile DistributionWorker _instance;
		private static readonly object _lock = new object();

		private Thread _distributionConnectionManagementThread;
		private volatile bool _shutDownRequested;
		private readonly List<DistributionWork> _pendingDistributionWork;

		private DistributionWorker()
		{
			_distributionConnectionManagementThread = null;
			_shutDownRequested = false;
			_pendingDistributionWork = new List<DistributionWork>();
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerInitializationCompleted += ServiceConnectionHandler_ServiceHandlerInitializationCompleted;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged += ServiceConnectionHandler_ServiceHandlerConnectionChanged;
		}

		public event EventHandler<DistributionResultEventArgs> DistributionCompleted;

		#region Properties

		public static DistributionWorker Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock)
					{
						if (_instance == null)
							_instance = new DistributionWorker();
					}
				}
				return _instance;
			}
		}

		public bool IsRunning => (_distributionConnectionManagementThread != null);

		#endregion

		#region Event raisers

		private void RaiseDistributionCompletedEvent(DistributionResult distributionResult, IProcessManagerServiceEventHandler caller)
		{
			DistributionCompleted?.Invoke(this, new DistributionResultEventArgs(distributionResult, caller));
		}

		#endregion

		public void AddWork(DistributionWork work)
		{
			Task.Run(() => AddWorkThread(work));
		}

		#region Implementation of IProcessManagerEventHandler

		public event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;

		public void ProcessManagerServiceEventHandler_ProcessStatusesChanged(object sender, ProcessStatusesEventArgs e) { }

		public void ProcessManagerServiceEventHandler_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e) { }

		public void ProcessManagerServiceEventHandler_DistributionCompleted(object sender, DistributionResultEventArgs e) { }

		#endregion

		#region Connection handler event handlers

		private void ServiceConnectionHandler_ServiceHandlerInitializationCompleted(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (e.Status == ProcessManagerServiceHandlerStatus.Connected)
			{
				Task.Run(() => DistributeSourceFilesThread(e.ServiceHandler.Machine));
			}
			else
			{
				ConnectionStore.RemoveConnection(e.ServiceHandler.Machine);
			}
		}

		private void ServiceConnectionHandler_ServiceHandlerConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			// do nothing?
		}

		#endregion

		#region Distribution connection management

		public void Start()
		{
			if (IsRunning)
				return;

			_shutDownRequested = false;
			_distributionConnectionManagementThread = new Thread(DistributionConnectionManagementThread);
			_distributionConnectionManagementThread.Start();
		}

		public void ShutDown()
		{
			if (!IsRunning)
				return;

			_shutDownRequested = true;
			while (IsRunning)
				Thread.Sleep(10);
		}

		private void DistributionConnectionManagementThread()
		{
			try
			{
				Logger.Add("STARTUP -- Starting distribution connection management thread...");

				while (!_shutDownRequested)
				{
					try
					{
						lock (_pendingDistributionWork)
						{
							ConnectionStore.Connections.Keys
								.Where(machine => !_pendingDistributionWork.Any(work => Comparer.MachinesEqual(work.DestinationMachine, machine)))
								.ToList()
								.ForEach(ConnectionStore.RemoveConnection);
						}

						List<Machine> machinesToConnect;
						lock (_pendingDistributionWork)
						{
							machinesToConnect = _pendingDistributionWork
								.Select(work => work.DestinationMachine)
								.Distinct(new MachineEqualityComparer())
								.Where(destinationMachine => !ConnectionStore.Connections.ContainsKey(destinationMachine))
								.ToList();
						}

						machinesToConnect.ForEach(destinationMachine =>
							{
								MachineConnection connection = ConnectionStore.CreateConnection(this, destinationMachine);
								connection.ServiceHandler.Initialize();
							});
					}
					catch (Exception ex)
					{
						Logger.Add("An unexpected error occurred in distribution connection management thread", ex);
					}

					Thread.Sleep(Settings.Service.Read<int>("DistributionConnectionCleanInterval"));
				}

				Logger.Add("SHUTDOWN -- Shutting down distribution connection management thread...");
			}
			catch (Exception ex)
			{
				Logger.Add("Fatal exception in distribution connection management thread, dying....", ex);
			}
			finally
			{
				// indicate that we are no longer running
				_distributionConnectionManagementThread = null;
			}
		}

		#endregion

		#region Helpers

		private void AddWorkThread(DistributionWork work)
		{
			try
			{
				switch (work.Type)
				{
					case ActionType.Distribute:
						{
							var sourceFiles = work.Application.Sources
								.Select(source => new
									{
										Source = source,
										FullPath = Path.Combine(work.Group.Path, source.Path.Trim(Path.DirectorySeparatorChar))
									})
								.Select(x => new
									{
										x.Source,
										Files = FileSystemHandler.IsFile(x.FullPath) ? new List<DistributionFile>() { new DistributionFile(x.Source.Path) } :
											FileSystemHandler.GetFileSystemEntries(x.FullPath, x.Source.Filter, x.Source.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
												.Where(entry => !entry.IsFolder)
												.Select(entry => new DistributionFile(Path.Combine(x.Source.Path, entry.Name)))
												.ToList()
									})
								.ToList();

							IEnumerable<DistributionFile> inclusiveSourceFiles = sourceFiles.Where(x => x.Source.Inclusive).SelectMany(x => x.Files);
							IEnumerable<DistributionFile> exclusiveSourceFiles = sourceFiles.Where(x => !x.Source.Inclusive).SelectMany(x => x.Files);
							work.Files = inclusiveSourceFiles.Except(exclusiveSourceFiles, new DistributionFileEqualityComparer()).ToList();
						}
						break;
				}
				
				Logger.Add(LogType.Verbose, $"Adding distribution work to queue: destination machine = {work.DestinationMachine}, files = {work.Files.Count}");

				lock (_pendingDistributionWork)
					_pendingDistributionWork.Add(work);
			}
			catch (ThreadAbortException) { }
			catch (Exception ex)
			{
				Logger.Add("An unexpected error occurred while preparing to add distribution work, aborting..", ex);
			}
		}

		private void DistributeSourceFilesThread(Machine machine)
		{
			List<DistributionWork> processedDistributionWork = new List<DistributionWork>();
			while (ConnectionStore.ConnectionCreated(machine))
			{
				DistributionWork work;

				lock (_pendingDistributionWork)
				{
					work = _pendingDistributionWork
						.Where(pendingWork => Comparer.MachinesEqual(pendingWork.DestinationMachine, machine))
						.FirstOrDefault(pendingWork => !processedDistributionWork.Contains(pendingWork));
				}

				if (work == null)
				{
					Thread.Sleep(10);
					continue;
				}

				processedDistributionWork.Add(work);

				Task.Run(() =>
					{
						try
						{
							try
							{
								Group destinationGroup = ConnectionStore.Connections[work.DestinationMachine].Configuration.Groups
									.FirstOrDefault(group => Comparer.GroupsEqual(group, work.Group));

								if (destinationGroup == null)
									Logger.AddAndThrow<DistributionActionException>(LogType.Warning, $"Could not distribute application {work.Application.Name} in group {work.Group.Name}" +
										$" to destination machine {work.DestinationMachine.HostName}. Destination machine does not contain a matching group.");

								List<string> errorMessages = new List<string>();
								foreach (DistributionFile file in work.Files)
								{
									file.DestinationGroupID = destinationGroup.ID;
									file.Content = FileSystemHandler.GetFileContent(Path.Combine(work.Group.Path, file.RelativePath.Trim(Path.DirectorySeparatorChar)));

									DistributeFileResult result = ConnectionStore.Connections[work.DestinationMachine].ServiceHandler.Service.DistributeFile(new DTODistributionFile(file)).FromDTO();

									try
									{
										if (result.Success)
											Logger.Add(LogType.Verbose, $"Distribution of file to {work.DestinationMachine.HostName} succeeded: {file.RelativePath}, {file.Content.Length} bytes");
										else
											Logger.AddAndThrow<DistributionActionException>(LogType.Error, $"Distribution of file to {work.DestinationMachine.HostName} failed: {Path.GetFileName(file.RelativePath)} | {result.ErrorMessage}");
									}
									catch (DistributionActionException ex)
									{
										errorMessages.Add(ex.Message);
									}

									file.IsDistributed = true;
								}

								if (errorMessages.Any())
									throw new DistributionActionException(string.Join(Environment.NewLine, errorMessages.Select(x => x.Replace(" | ", Environment.NewLine + "\t"))));

								RaiseDistributionCompletedEvent(new DistributionResult(work, DistributionResultValue.Success), work.Caller);
							}
							catch (DistributionActionException) { throw; }
							catch (ThreadAbortException) { }
							catch (Exception ex)
							{
								Logger.AddAndThrow<DistributionActionException>("An unexpected error occurred while distributing source files, aborting..", ex);
							}
							finally
							{
								lock (_pendingDistributionWork)
									_pendingDistributionWork.Remove(work);
							}
						}
						catch (DistributionActionException ex)
						{
							RaiseDistributionCompletedEvent(new DistributionResult(work, DistributionResultValue.Failure, ex.Message), work.Caller);
						}
					});
			}
		}

		#endregion
	}
}

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
using ProcessManager.Service.Client;
using ProcessManager.Service.Common;
using ProcessManager.Service.DataObjects;
using ProcessManager.Utilities;

namespace ProcessManager
{
	public class DistributionWorker : IDisposable, IProcessManagerEventHandler
	{
		private static volatile DistributionWorker _instance;
		private static readonly object _lock = new object();

		private Thread _distributionConnectionManagementThread;
		private readonly List<DistributionWork> _pendingDistributionWork;

		private DistributionWorker()
		{
			_distributionConnectionManagementThread = null;
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

		#endregion

		#region Event raisers

		private void RaiseDistributionCompletedEvent(DistributionResult distributionResult, IProcessManagerServiceEventHandler caller)
		{
			if (DistributionCompleted != null)
				DistributionCompleted(this, new DistributionResultEventArgs(distributionResult, caller));
		}

		#endregion

		public void Initialize()
		{
			StartDistributionConnectionManagementThread();
		}

		public void Dispose()
		{
			StopDistributionConnectionManagementThread();
		}

		public void AddWork(DistributionWork work)
		{
			Task.Factory.StartNew(() => AddWorkThread(work));
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
				Task.Factory.StartNew(() => DistributeSourceFilesThread(e.ServiceHandler.Machine));
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

		private void StartDistributionConnectionManagementThread()
		{
			if (_distributionConnectionManagementThread != null)
				return;

			_distributionConnectionManagementThread = new Thread(DistributionConnectionManagementThread);
			_distributionConnectionManagementThread.Start();
		}

		private void StopDistributionConnectionManagementThread()
		{
			if (_distributionConnectionManagementThread != null)
				_distributionConnectionManagementThread.Abort();
		}

		private void DistributionConnectionManagementThread()
		{
			try
			{
				int cleanInterval = Settings.Service.Read<int>("DistributionConnectionCleanInterval");
				while (true)
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
					catch (ThreadAbortException) { throw; }
					catch (Exception ex)
					{
						Logger.Add("An unexpected error occurred in distribution connection management thread", ex);
					}

					Thread.Sleep(cleanInterval);
				}
			}
			catch (ThreadAbortException)
			{
				Logger.Add("ABORTING -- Shutting down distribution connection management thread...");
			}
			catch (Exception ex)
			{
				Logger.Add("Fatal exception in distribution connection management thread, dying....", ex);
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
				
				Logger.Add(LogType.Verbose, "Adding distribution work to queue: destination machine = " + work.DestinationMachine + ", files = " + work.Files.Count);

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

				Task.Factory.StartNew(() =>
					{
						try
						{
							Group destinationGroup = ConnectionStore.Connections[work.DestinationMachine].Configuration.Groups
								.FirstOrDefault(group => Comparer.GroupsEqual(group, work.Group));

							if (destinationGroup == null)
							{
								Logger.Add(LogType.Warning, "Could not distribute application " + work.Application.Name + " in group " + work.Group.Name
									+ " to destination machine " + work.DestinationMachine.HostName + ". Destination machine does not contain a matching group.");
								RaiseDistributionCompletedEvent(new DistributionResult(work, DistributionResultValue.Failure), work.Caller);
								return;
							}

							bool totalSuccess = true;
							foreach (DistributionFile file in work.Files)
							{
								file.DestinationGroupID = destinationGroup.ID;
								file.Content = FileSystemHandler.GetFileContent(Path.Combine(work.Group.Path, file.RelativePath.Trim(Path.DirectorySeparatorChar)));

								bool success = ConnectionStore.Connections[work.DestinationMachine].ServiceHandler.Service.DistributeFile(new DTODistributionFile(file));

								Logger.Add(LogType.Verbose, "Distribution of file to " + work.DestinationMachine.HostName + " " + (success ? "succeeded" : "failed") + ": " + file.RelativePath + ", " + file.Content.Length + " bytes");

								file.IsDistributed = true;
								totalSuccess &= success;
							}

							RaiseDistributionCompletedEvent(new DistributionResult(work, totalSuccess ? DistributionResultValue.Success : DistributionResultValue.Failure), work.Caller);
						}
						catch (ThreadAbortException) {}
						catch (Exception ex)
						{
							Logger.Add("An unexpected error occurred while distributing source files, aborting..", ex);
							RaiseDistributionCompletedEvent(new DistributionResult(work, DistributionResultValue.Failure), work.Caller);
						}
						finally
						{
							lock (_pendingDistributionWork)
								_pendingDistributionWork.Remove(work);
						}
					});
			}
		}

		#endregion
	}
}

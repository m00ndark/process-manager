using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.DataObjects.Comparers;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;
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
			new Thread(() => AddWorkThread(work)).Start();
		}

		#region Implementation of IProcessManagerEventHandler

		public event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;

		public void ProcessManagerServiceEventHandler_ProcessStatusesChanged(object sender, ProcessStatusesEventArgs e) { }

		public void ProcessManagerServiceEventHandler_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e) { }

		#endregion

		#region Connection handler event handlers

		private void ServiceConnectionHandler_ServiceHandlerInitializationCompleted(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (e.Status == ProcessManagerServiceHandlerStatus.Connected)
			{
				foreach (DistributionWork work in _pendingDistributionWork.Where(work => work.DestinationMachine == e.ServiceHandler.Machine))
					new Thread(() => DistributeSourceFiles(work)).Start();
			}
		}

		private void ServiceConnectionHandler_ServiceHandlerConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			
		}

		#endregion

		#region Distribution connection management

		private void StartDistributionConnectionManagementThread()
		{
			if (_distributionConnectionManagementThread == null)
			{
				_distributionConnectionManagementThread = new Thread(DistributionConnectionManagementThread);
				_distributionConnectionManagementThread.Start();
			}
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
				while (true)
				{
					try
					{
						foreach (MachineConnection connection in ConnectionStore.Connections.Values)
						{
							if (!_pendingDistributionWork.Any(work => work.DestinationMachine == connection.Machine))
								ConnectionStore.RemoveConnection(connection.Machine);
						}

						foreach (Machine destinationMachine in _pendingDistributionWork.Select(work => work.DestinationMachine).Distinct(new MachineEqualityComparer()))
						{
							if (!ConnectionStore.Connections.ContainsKey(destinationMachine))
							{
								MachineConnection connection = ConnectionStore.CreateConnection(this, destinationMachine);
								connection.ServiceHandler.Initialize();
							}
						}
					}
					catch (ThreadAbortException) { throw; }
					catch (Exception ex)
					{
						Logger.Add("An unexpected error occurred in distribution connection management thread", ex);
					}

					Thread.Sleep(Settings.Service.Read<int>("DistributionConnectionCleanInterval"));
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
				
				_pendingDistributionWork.Add(work);
			}
			catch (ThreadAbortException) { }
			catch (Exception ex)
			{
				Logger.Add("An unexpected error occurred while preparing to add distribution work, aborting..", ex);
			}
		}

		private void DistributeSourceFiles(DistributionWork work)
		{
			try
			{
				ApplicationEqualityComparer applicationEqualityComparer = new ApplicationEqualityComparer();
				Application destinationApplication = ConnectionStore.Connections[work.DestinationMachine].Configuration.Applications
					.FirstOrDefault(application => applicationEqualityComparer.Equals(application, work.Application));
				
				if (destinationApplication == null)
				{
					Logger.Add(LogType.Warning, "Could not distribute application " + work.Application.Name + " to destination machine "
						+ work.DestinationMachine.HostName + ". Destination machine does not contain a matching application.");
					return;
				}

				foreach (DistributionFile file in work.Files)
				{
					file.DestinationApplicationID = destinationApplication.ID;
					file.Content = FileSystemHandler.GetFileContent(Path.Combine(work.Group.Path, file.RelativePath.Trim(Path.DirectorySeparatorChar)));

					// todo: send file here

					file.IsDistributed = true;
				}
			}
			catch (ThreadAbortException) { }
			catch (Exception ex)
			{
				Logger.Add("An unexpected error occurred while distributing source files, aborting..", ex);
			}
			finally
			{
				_pendingDistributionWork.Remove(work);
			}
		}

		#endregion
	}
}

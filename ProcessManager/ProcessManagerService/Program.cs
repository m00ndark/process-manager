using System;
using System.Collections;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceProcess;
using System.Threading;
using ProcessManager.Utilities;

namespace ProcessManagerService
{
	internal static class Program
	{
		internal const string SERVICE_NAME = "ProcessManagerService";

		private static void Main()
		{
			Logger.LogSource = LogSource.Server;
			try
			{
				string[] args = Environment.CommandLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();

				if (args.Contains("-stop"))
				{
					StopService();

					if (ShouldExitAfter("-stop", ref args))
						return;
				}

				if (args.Contains("-uninstall"))
				{
					UninstallService();

					if (ShouldExitAfter("-uninstall", ref args))
						return;
				}

				if (args.Contains("-install"))
				{
					InstallService();

					if (ShouldExitAfter("-install", ref args))
						return;
				}

				if (args.Contains("-standalone"))
				{
					// this process will keep running
					ProcessManager.ProcessManager.Instance.Start();
				}
				else if (args.Contains("-start"))
				{
					// another process will be started
					StartService();
				}
				else
				{
					// this process will keep running
					ServiceBase[] servicesToRun = new ServiceBase[] { new ProcessManagerService() };
					ServiceBase.Run(servicesToRun);
				}
			}
			catch (Exception ex)
			{
				Logger.Add("Fatal error", ex);
			}
			Logger.Flush();
		}

		private static bool ShouldExitAfter(string flag, ref string[] args)
		{
			args = args.Except(new[] { flag }).ToArray();
			return !args.Any();
		}

		private static void StartService()
		{
			ServiceController serviceController = ServiceController.GetServices().FirstOrDefault(sc => sc.ServiceName == SERVICE_NAME);

			if (serviceController == null || serviceController.Status != ServiceControllerStatus.Stopped)
				return;

			serviceController.Start();
			WaitForServiceStatus(serviceController, ServiceControllerStatus.Running);
		}

		private static void StopService()
		{
			ServiceController serviceController = ServiceController.GetServices().FirstOrDefault(sc => sc.ServiceName == SERVICE_NAME);

			if (serviceController == null || serviceController.Status != ServiceControllerStatus.Running)
				return;

			serviceController.Stop();
			WaitForServiceStatus(serviceController, ServiceControllerStatus.Stopped);
		}

		private static void WaitForServiceStatus(ServiceController serviceController, ServiceControllerStatus status)
		{
			while (serviceController.Status != status)
			{
				Thread.Sleep(100);
				serviceController.Refresh();
			}
		}

		private static void InstallService()
		{
			if (ServiceController.GetServices().Any(sc => sc.ServiceName == SERVICE_NAME))
				return;

			AssemblyInstaller assemblyInstaller = new AssemblyInstaller(Assembly.GetExecutingAssembly(), null) { UseNewContext = true };
			IDictionary savedState = new Hashtable();
			assemblyInstaller.Install(savedState);
			assemblyInstaller.Commit(savedState);
			DataContractSerializer serializer = new DataContractSerializer(typeof(IDictionary));
			using (FileStream fileStream = File.Create(GetServiceInstallerSavedStateFileName()))
				serializer.WriteObject(fileStream, savedState);
		}

		private static void UninstallService()
		{
			if (ServiceController.GetServices().All(sc => sc.ServiceName != SERVICE_NAME))
				return;

			IDictionary savedState = null;
			string stateFilePath = GetServiceInstallerSavedStateFileName();
			if (File.Exists(stateFilePath))
			{
				DataContractSerializer serializer = new DataContractSerializer(typeof(IDictionary));
				using (FileStream fileStream = File.OpenRead(stateFilePath))
					savedState = (IDictionary) serializer.ReadObject(fileStream);
			}
			AssemblyInstaller assemblyInstaller = new AssemblyInstaller(Assembly.GetExecutingAssembly(), null) { UseNewContext = true };
			assemblyInstaller.Uninstall(savedState);
		}

		private static string GetServiceInstallerSavedStateFileName()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			return Path.Combine(Path.GetDirectoryName(executingAssembly.Location), Path.GetFileNameWithoutExtension(executingAssembly.Location) + ".InstallState");
		}
	}
}

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
					ServiceController serviceController = ServiceController.GetServices().FirstOrDefault(sc => sc.ServiceName == SERVICE_NAME);
					if (serviceController != null && serviceController.Status == ServiceControllerStatus.Running)
					{
						serviceController.Stop();
						WaitForServiceStatus(serviceController, ServiceControllerStatus.Stopped);
					}

					if (!args.Except(new[] { "-stop" }).Any())
						return;
				}
				if (args.Contains("-uninstall"))
				{
					if (ServiceController.GetServices().Any(sc => sc.ServiceName == SERVICE_NAME))
						UninstallService();

					if (!args.Except(new [] { "-uninstall" }).Any())
						return;
				}
				if (args.Contains("-install"))
				{
					if (ServiceController.GetServices().All(sc => sc.ServiceName != SERVICE_NAME))
						InstallService();

					if (!args.Except(new[] { "-install" }).Any())
						return;
				}
				if (args.Contains("-standalone"))
				{
					ProcessManager.ProcessManager.Instance.Start();
				}
				else if (args.Contains("-start"))
				{
					ServiceController serviceController = ServiceController.GetServices().FirstOrDefault(sc => sc.ServiceName == SERVICE_NAME);
					if (serviceController != null && serviceController.Status == ServiceControllerStatus.Stopped)
					{
						serviceController.Start();
						WaitForServiceStatus(serviceController, ServiceControllerStatus.Running);
					}
				}
				else
				{
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

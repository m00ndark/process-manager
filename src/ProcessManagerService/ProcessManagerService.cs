using System.ServiceProcess;

namespace ProcessManagerService
{
	public partial class ProcessManagerService : ServiceBase
	{
		public ProcessManagerService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			if (!ProcessManager.ProcessManager.Instance.IsRunning)
				ProcessManager.ProcessManager.Instance.Start();
		}

		protected override void OnStop()
		{
			if (ProcessManager.ProcessManager.Instance.IsRunning)
				ProcessManager.ProcessManager.Instance.ShutDown();
		}
	}
}

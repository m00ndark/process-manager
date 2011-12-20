using System.ServiceProcess;

namespace ProcessManagerService
{
	public partial class ProcessManagerService : ServiceBase
	{
		private readonly ProcessManager.ProcessManager _processManager;

		public ProcessManagerService()
		{
			InitializeComponent();
			_processManager = new ProcessManager.ProcessManager();
		}

		protected override void OnStart(string[] args)
		{
			if (!_processManager.IsRunning)
				_processManager.Start();
		}

		protected override void OnStop()
		{
			if (_processManager.IsRunning)
				_processManager.ShutDown();
		}
	}
}

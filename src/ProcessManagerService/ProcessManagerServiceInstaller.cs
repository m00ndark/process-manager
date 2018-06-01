using System.ComponentModel;
using System.Configuration.Install;

namespace ProcessManagerService
{
	[RunInstaller(true)]
	public partial class ProcessManagerServiceInstaller : Installer
	{
		public ProcessManagerServiceInstaller()
		{
			InitializeComponent();
		}
	}
}

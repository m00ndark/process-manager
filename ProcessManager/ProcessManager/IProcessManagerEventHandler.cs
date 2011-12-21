using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessManager.EventArguments;

namespace ProcessManager
{
	public interface IProcessManagerEventHandler
	{
		void ProcessManagerServiceEventHandler_ApplicationStatusesChanged(object sender, ApplicationStatusesEventArgs e);
	}
}

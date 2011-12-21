using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ProcessManager.Service.Common
{
	[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IProcessManagerServiceEventHandler))]
	public interface IProcessManagerService
	{
		[OperationContract(IsInitiating = true)]
		void Subscribe();

		[OperationContract(IsTerminating = true)]
		void Unsubscribe();


	}
}

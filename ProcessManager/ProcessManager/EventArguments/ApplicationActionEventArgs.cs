using System;
using ProcessManager.DataObjects;

namespace ProcessManager.EventArguments
{
	public class ApplicationActionEventArgs : EventArgs
	{
		public ApplicationActionEventArgs(ApplicationAction action)
		{
			Action = action;
		}

		#region Properties

		public ApplicationAction Action { get; private set; }

		#endregion
	}
}

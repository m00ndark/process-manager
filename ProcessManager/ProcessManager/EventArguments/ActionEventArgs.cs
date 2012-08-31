using System;
using ProcessManager.DataObjects;

namespace ProcessManager.EventArguments
{
	public class ActionEventArgs : EventArgs
	{
		public ActionEventArgs(IAction action)
		{
			Action = action;
		}

		#region Properties

		public IAction Action { get; private set; }

		#endregion
	}
}

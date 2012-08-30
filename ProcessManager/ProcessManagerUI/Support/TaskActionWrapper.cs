using System;
using System.ComponentModel;
using System.Diagnostics;
using ProcessManager.Utilities;

namespace ProcessManagerUI.Support
{
	public static class TaskActionWrapper
	{
		public static void Do(Action action)
		{
			try
			{
				if (action != null)
					action();
			}
			catch (InvalidAsynchronousStateException) { /*  */ }
			catch (Exception ex)
			{
				Logger.Add("Unhandled exception:", ex);
				Debug.Assert(false);
			}
		}
	}
}

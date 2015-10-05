using System;
using System.ComponentModel;
using System.Diagnostics;
using ToolComponents.Core.Logging;

namespace ProcessManagerUI.Support
{
	public static class TaskActionWrapper
	{
		public static void Do(Action action)
		{
			try
			{
				action?.Invoke();
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

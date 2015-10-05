using System;
using System.Threading;
using System.Threading.Tasks;
using ProcessManager;
using ProcessManagerUI.Support;
using ToolComponents.Core.Logging;

namespace ProcessManagerUI.Utilities
{
	public class Worker
	{
		private Worker()
		{
			ID = Guid.NewGuid();
			ExecuteCompleted = false;
		}

		#region Properties

		private Guid ID { get; }
		private bool ExecuteCompleted { get; set; }

		#endregion

		private void ExceuteDo(Action work)
		{
			try
			{
				work();
			}
			catch (Exception ex)
			{
				Logger.Add("Work generated unhandled exception", ex);
			}
			ExecuteCompleted = true;
		}

		private void ExceuteWaitFor(Func<bool> signal)
		{
			try
			{
				while (!signal()) Thread.Sleep(200);
			}
			catch (Exception ex)
			{
				Logger.Add("Signal generated unhandled exception", ex);
			}
			ExecuteCompleted = true;
		}

		private void Tick(int ticks)
		{
			if (ExecuteCompleted)
				TaskDialog.Close(ID);
		}

		public static void Do(string message, Action work)
		{
			Worker worker = new Worker();
			Task.Run(() => worker.ExceuteDo(work));
			Wait(message, worker);
		}

		public static void WaitFor(Func<bool> signal)
		{
			WaitFor(null, signal);
		}

		public static void WaitFor(string message, Func<bool> signal)
		{
			if (signal()) return;
			Worker worker = new Worker();
			Task.Run(() => worker.ExceuteWaitFor(signal));
			Wait(message, worker);
		}

		private static void Wait(string message, Worker worker)
		{
			if (TaskDialog.IsPlatformSupported && message != null)
				TaskDialog.Show(worker.ID, Settings.Constants.APPLICATION_NAME, message, string.Empty, null, 0, worker.Tick);

			while (!worker.ExecuteCompleted)
				Thread.Sleep(200);
		}
	}
}

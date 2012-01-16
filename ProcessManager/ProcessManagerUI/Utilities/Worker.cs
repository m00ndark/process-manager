using System;
using System.Threading;
using ProcessManager;
using ProcessManagerUI.Support;

namespace ProcessManagerUI.Utilities
{
	public class Worker
	{
		private readonly int _threadID;
		private bool _executeCompleted;

		private Worker()
		{
			_threadID = Thread.CurrentThread.ManagedThreadId;
			_executeCompleted = false;
		}

		private void ExceuteDo(object inParam)
		{
			Action work = (Action) inParam;
			work();
			_executeCompleted = true;
		}

		private void ExceuteWaitFor(object inParam)
		{
			Func<bool> signal = (Func<bool>) inParam;
			while (!signal()) Thread.Sleep(200);
			_executeCompleted = true;
		}

		private void Tick(int ticks)
		{
			if (_executeCompleted)
				TaskDialog.Close(_threadID);
		}

		public static void Do(string message, Action work)
		{
			Worker worker = new Worker();
			new Thread(worker.ExceuteDo).Start(work);
			Wait(message, worker);
		}

		public static void WaitFor(string message, Func<bool> signal)
		{
			if (signal()) return;
			Worker worker = new Worker();
			new Thread(worker.ExceuteWaitFor).Start(signal);
			Wait(message, worker);
		}

		private static void Wait(string message, Worker worker)
		{
			if (TaskDialog.IsPlatformSupported)
			{
				TaskDialog.Show(Settings.Constants.APPLICATION_NAME, message, string.Empty, null, 0, worker.Tick);
			}

			while (!worker._executeCompleted)
				Thread.Sleep(200);
		}
	}
}

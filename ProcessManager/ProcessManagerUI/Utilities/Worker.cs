﻿using System;
using System.Threading;
using ProcessManager;
using ProcessManagerUI.Support;

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

		private Guid ID { get; set; }
		private bool ExecuteCompleted { get; set; }

		#endregion

		private void ExceuteDo(object inParam)
		{
			Action work = (Action) inParam;
			work();
			ExecuteCompleted = true;
		}

		private void ExceuteWaitFor(object inParam)
		{
			Func<bool> signal = (Func<bool>) inParam;
			while (!signal()) Thread.Sleep(200);
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
				TaskDialog.Show(worker.ID, Settings.Constants.APPLICATION_NAME, message, string.Empty, null, 0, worker.Tick);

			while (!worker.ExecuteCompleted)
				Thread.Sleep(200);
		}
	}
}
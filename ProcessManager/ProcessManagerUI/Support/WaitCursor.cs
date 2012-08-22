using System;
using System.Windows.Forms;

namespace ProcessManagerUI.Support
{
	public class WaitCursor : IDisposable
	{
		private static int _instanceCounter = 0;
		private static readonly object _lock = new object();

		private readonly Action<Cursor> _setCursor;

		public WaitCursor(Action<Cursor> customSetCursor = null)
		{
			_setCursor = customSetCursor ?? (cursor => { Cursor.Current = cursor; });

			lock (_lock)
			{
				if (_instanceCounter == 0)
					_setCursor(Cursors.WaitCursor);

				_instanceCounter++;
			}
		}

		public void Dispose()
		{
			lock (_lock)
			{
				_instanceCounter--;

				if (_instanceCounter == 0)
					_setCursor(Cursors.Default);
			}
		}
	}
}

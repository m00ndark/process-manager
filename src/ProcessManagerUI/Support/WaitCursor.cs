using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProcessManagerUI.Support
{
	public class WaitCursor : IDisposable
	{
		private static readonly object _lock = new object();
		private static readonly IDictionary<object, int> _instanceCounters = new Dictionary<object, int>();

		private readonly Form _owner;
		private readonly Action<Cursor> _setCursor;

		public WaitCursor(Form owner, Action<Cursor> customSetCursor = null)
		{
			_owner = owner;
			_setCursor = customSetCursor ?? (cursor => { Cursor.Current = cursor; });

			lock (_lock)
			{
				if (!_instanceCounters.ContainsKey(_owner))
					_instanceCounters.Add(_owner, 0);

				if (_instanceCounters[_owner] == 0)
					_setCursor(Cursors.WaitCursor);

				_instanceCounters[_owner]++;
			}
		}

		public void Dispose()
		{
			lock (_lock)
			{
				_instanceCounters[_owner]--;

				if (_instanceCounters[_owner] == 0)
				{
					_setCursor(Cursors.Default);
					_instanceCounters.Remove(_owner);
				}
			}
		}
	}
}

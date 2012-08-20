using System;
using System.Windows.Forms;

namespace ProcessManagerUI.Support
{
	public class WaitCursor : IDisposable
	{
		public WaitCursor()
		{
			Cursor.Current = Cursors.WaitCursor;
		}

		public void Dispose()
		{
			Cursor.Current = Cursors.Default;
		}
	}
}

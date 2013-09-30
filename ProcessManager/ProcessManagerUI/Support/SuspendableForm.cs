using System.Collections.Generic;
using System.Windows.Forms;

namespace ProcessManagerUI.Support
{
	public abstract class SuspendableForm<T> : Form
	{
		private readonly IDictionary<T, bool> _uiSuspended;

		protected SuspendableForm()
		{
			_uiSuspended = new Dictionary<T, bool>();
		}

		public void Suspend(T key)
		{
			_uiSuspended[key] = true;
		}

		public void Resume(T key)
		{
			_uiSuspended[key] = false;
		}

		public bool IsSuspended(T key)
		{
			return _uiSuspended[key];
		}
	}
}

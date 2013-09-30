using System;
using System.Collections.Generic;

namespace ProcessManagerUI.Support
{
	public class UISuspension<T>
	{
		private readonly IDictionary<T, bool> _uiSuspended;

		public UISuspension()
		{
			_uiSuspended = new Dictionary<T, bool>();
			if (typeof(T).IsEnum)
			{
				foreach (T value in Enum.GetValues(typeof(T)))
					_uiSuspended[value] = false;
			}
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

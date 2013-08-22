using System;

namespace ProcessManager.DataObjects
{
	public class ProcessAction : IAction
	{
		public ProcessAction(ActionType type, Application application)
		{
			if (type == ActionType.Distribute)
				throw new ArgumentException("Invalid process action type");

			Type = type;
			Application = application;
		}

		#region Properties

		public ActionType Type { get; private set; }
		public Machine Machine { get; set; }
		public Group Group { get; set; }
		public Application Application { get; set; }

		#endregion
	}
}

using System;

namespace ProcessManager.DataObjects
{
	public class ProcessAction : IAction
	{
		public ProcessAction(ActionType type, Application application)
		{
			if (type != ActionType.Start && type != ActionType.Stop && type != ActionType.Restart)
				throw new ArgumentException("Invalid process action type");

			Type = type;
			Machine = null;
			Group = null;
			Application = application;
		}

		public ProcessAction(ActionType type) : this(type, null) {}

		#region Properties

		public ActionType Type { get; private set; }
		public Machine Machine { get; set; }
		public Group Group { get; set; }
		public Application Application { get; set; }

		#endregion
	}
}

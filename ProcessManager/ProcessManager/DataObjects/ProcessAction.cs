using System;

namespace ProcessManager.DataObjects
{
	public class ProcessAction : IAction
	{
		public ProcessAction(ActionType type) : this(type, null) { }

		public ProcessAction(ActionType type, Application application) : this(type, null, null, application) { }

		public ProcessAction(ActionType type, Machine machine, Group group, Application application)
		{
			if (type != ActionType.Start && type != ActionType.Stop && type != ActionType.Restart)
				throw new ArgumentException("Invalid process action type");

			Type = type;
			Machine = machine;
			Group = group;
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

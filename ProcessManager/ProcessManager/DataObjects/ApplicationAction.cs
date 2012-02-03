namespace ProcessManager.DataObjects
{
	public enum ApplicationActionType
	{
		Start,
		Stop,
		Restart
	}

	public class ApplicationAction
	{
		public ApplicationAction(ApplicationActionType type, Application application)
		{
			Type = type;
			Application = application;
		}

		#region Properties

		public ApplicationActionType Type { get; private set; }
		public Machine Machine { get; set; }
		public Group Group { get; set; }
		public Application Application { get; set; }

		#endregion
	}
}

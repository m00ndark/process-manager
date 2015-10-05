namespace ProcessManager.DataObjects
{
	public class ProcessActionResult
	{
		public ProcessActionResult(ActionType type, string errorMessage, ProcessStatus status)
		{
			Type = type;
			ErrorMessage = errorMessage;
			Status = status;
		}

		#region Properties

		public bool Success => ErrorMessage == null;

		public ActionType Type { get; private set; }
		public string ErrorMessage { get; }
		public ProcessStatus Status { get; private set; }

		#endregion
	}
}

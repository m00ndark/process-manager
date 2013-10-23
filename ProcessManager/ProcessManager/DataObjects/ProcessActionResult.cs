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

		public bool Success { get { return ErrorMessage == null; } }

		public ActionType Type { get; private set; }
		public string ErrorMessage { get; private set; }
		public ProcessStatus Status { get; private set; }

		#endregion
	}
}

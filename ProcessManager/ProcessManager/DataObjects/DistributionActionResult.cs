namespace ProcessManager.DataObjects
{
	public class DistributionActionResult
	{
		public DistributionActionResult(ActionType type, string errorMessage)
		{
			Type = type;
			ErrorMessage = errorMessage;
		}

		#region Properties

		public bool Success => ErrorMessage == null;

		public ActionType Type { get; private set; }
		public string ErrorMessage { get; }

		#endregion
	}
}

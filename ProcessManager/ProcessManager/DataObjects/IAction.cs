namespace ProcessManager.DataObjects
{
	public enum ActionType
	{
		Start,
		Stop,
		Restart,
		Distribute
	}

	public interface IAction {}
}

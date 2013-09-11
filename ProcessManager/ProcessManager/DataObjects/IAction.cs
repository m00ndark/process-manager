namespace ProcessManager.DataObjects
{
	public enum ActionType
	{
		Start,
		Stop,
		Restart,
		Distribute,
		Play
	}

	public interface IAction
	{
		ActionType Type { get; }
	}
}

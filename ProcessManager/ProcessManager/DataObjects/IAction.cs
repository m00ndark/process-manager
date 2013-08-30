namespace ProcessManager.DataObjects
{
	public enum ActionType
	{
		Start,
		Stop,
		Restart,
		Distribute
	}

	public interface IAction
	{
		ActionType Type { get; }
	}

	public interface IMacroAction : IAction
	{
		bool GotAnyID { get; }
	}
}

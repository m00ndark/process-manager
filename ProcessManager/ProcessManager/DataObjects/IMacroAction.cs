namespace ProcessManager.DataObjects
{
    public enum MacroActionType
    {
        Start,
        Stop,
        Restart,
        Distribute,
        Wait
    }

    public enum MacroActionWaitForEvent
    {
        Timeout,
        PreviousActionCompleted
    }

    public interface IMacroAction
    {
        MacroActionType Type { get; }
        bool IsValid { get; }
    }
}

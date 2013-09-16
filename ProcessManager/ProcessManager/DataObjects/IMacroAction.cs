using System;

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
        PreviousActionsCompleted
    }

    public interface IMacroAction
    {
		Guid ID { get; }
        MacroActionType Type { get; }
        bool IsValid { get; }
    }
}

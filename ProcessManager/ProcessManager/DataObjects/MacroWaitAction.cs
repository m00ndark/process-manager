using System;

namespace ProcessManager.DataObjects
{
    public class MacroWaitAction : IMacroAction
    {
		public MacroWaitAction(MacroActionType type, MacroActionWaitForEvent waitForEvent, int timeoutMilliseconds = 0)
			: this(Guid.NewGuid(), type, waitForEvent, timeoutMilliseconds) {}

		public MacroWaitAction(Guid id, MacroActionType type, MacroActionWaitForEvent waitForEvent, int timeoutMilliseconds = 0)
        {
            if (type != MacroActionType.Wait)
                throw new ArgumentException("Invalid wait action type");

			ID = id;
			Type = type;
            WaitForEvent = waitForEvent;
            TimeoutMilliseconds = timeoutMilliseconds;
        }

        #region Properties

	    public Guid ID { get; }
        public MacroActionType Type { get; }
        public MacroActionWaitForEvent WaitForEvent { get; }
        public int TimeoutMilliseconds { get; }

        public bool IsValid => true;

	    #endregion

	    public IMacroAction Copy()
	    {
			return new MacroWaitAction(Guid.NewGuid(), Type, WaitForEvent, TimeoutMilliseconds);
	    }

	    public void ChangeActionType(MacroActionType actionType)
		{
			throw new InvalidOperationException("Cannot change action type of macro wait action");
		}

        #region Equality

	    public override bool Equals(object obj)
        {
            MacroWaitAction macroWaitAction = obj as MacroWaitAction;
            return (macroWaitAction != null
                && macroWaitAction.Type == Type
                && macroWaitAction.WaitForEvent == WaitForEvent
                && macroWaitAction.TimeoutMilliseconds == TimeoutMilliseconds);
        }

        public override int GetHashCode()
        {
            return (Type.ToString() + WaitForEvent + TimeoutMilliseconds).GetHashCode();
        }

        #endregion
    }
}

using System;

namespace ProcessManager.DataObjects
{
    public class MacroWaitAction : IMacroAction
    {
		public MacroWaitAction(MacroActionType type) : this(Guid.NewGuid(), type) {}

        public MacroWaitAction(Guid id, MacroActionType type)
        {
            if (type != MacroActionType.Wait)
                throw new ArgumentException("Invalid wait action type");

			ID = id;
			Type = type;
            WaitForEvent = null;
            TimeoutMilliseconds = 0;
        }

        #region Properties

	    public Guid ID { get; private set; }
        public MacroActionType Type { get; private set; }
        public MacroActionWaitForEvent? WaitForEvent { get; set; }
        public int TimeoutMilliseconds { get; set; }

        public bool IsValid { get { return WaitForEvent.HasValue; } }

        #endregion

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

using System;
using System.Collections.Generic;
using ProcessManager.DataObjects.Comparers;

namespace ProcessManager.DataObjects
{
	public class Macro
	{
		public Macro(string name) : this(Guid.NewGuid(), name) {}

		public Macro(Guid id, string name) : this()
		{
			ID = id;
			Name = name;
		}

		private Macro()
		{
			ActionBundles = new List<MacroActionBundle>();
		}

		#region Properties

		public Guid ID { get; private set; }
		public string Name { get; set; }
		public List<MacroActionBundle> ActionBundles { get; private set; }

		#endregion

		#region Equality

		public bool Equals(string name)
		{
			return Comparer.MacrosEqual(this, name);
		}

		public override bool Equals(object obj)
		{
			Macro macro = obj as Macro;
			return macro != null && Comparer.MacrosEqual(this, macro);
		}

		public override int GetHashCode()
		{
			return Comparer.GetHashCode(this);
		}

		#endregion

		#region ToString

		public override string ToString()
		{
			return Name;
		}

		#endregion
	}
}

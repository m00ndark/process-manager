using System;
using System.Collections.Generic;
using System.Linq;
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
		public List<MacroActionBundle> ActionBundles { get; }

		#endregion

		public Macro Clone(bool cloneBundles = true)
		{
			Macro macro = new Macro() { ID = ID, Name = Name };
			if (cloneBundles) macro.ActionBundles.AddRange(ActionBundles.Select(bundle => bundle.Clone()));
			return macro;
		}

		public Macro Copy(string name = null)
		{
			Macro macro = Clone(false);
			macro.ID = Guid.NewGuid();
			if (name != null) macro.Name = name;
			macro.ActionBundles.AddRange(ActionBundles.Select(bundle => bundle.Copy()));
			return macro;
		}

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

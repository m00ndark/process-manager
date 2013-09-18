using System;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public interface IMacroNode : INode
	{
		MacroActionState State { get; set; }

		event EventHandler StateChanged;
	}
}

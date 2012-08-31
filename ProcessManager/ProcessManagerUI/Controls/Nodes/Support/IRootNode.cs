using System;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public interface IRootNode : INode
	{
		event EventHandler SizeChanged;

		void ExpandAll(bool expanded);
	}
}

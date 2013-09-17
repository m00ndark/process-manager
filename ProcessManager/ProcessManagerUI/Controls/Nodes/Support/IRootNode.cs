using System;
using System.Collections.Generic;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public interface IRootNode : INode
	{
		event EventHandler SizeChanged;

		void ExpandAll(bool expanded);
	}
}

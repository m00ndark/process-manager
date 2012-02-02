using System;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public interface IControlPanelRootNode : IControlPanelNode
	{
		event EventHandler SizeChanged;

		void ExpandAll(bool expanded);
	}
}

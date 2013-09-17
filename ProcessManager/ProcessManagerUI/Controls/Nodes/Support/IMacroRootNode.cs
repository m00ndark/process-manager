using System.Collections.Generic;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public interface IMacroRootNode : IRootNode
	{
		IEnumerable<INode> GetCheckedLeafNodes();
	}
}

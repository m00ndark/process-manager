using System.Collections.Generic;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public interface IMacroRootNode : IRootNode, IMacroNode
	{
		IEnumerable<IMacroNode> GetCheckedLeafNodes();
	}
}

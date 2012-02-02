using System;
using System.Collections.Generic;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class GroupNode : BaseRootNode
	{
		public GroupNode(Group group, IEnumerable<IControlPanelNode> childNodes) : base(childNodes)
		{
			Group = group;
			//BackColor = Color.FromArgb(255, 208, 160);
		}

		#region Properties

		public Group Group { get; private set; }

		public override Guid ID { get { return Group.ID; } }
		protected override string NodeName { get { return Group.Name; } }

		#endregion
	}
}

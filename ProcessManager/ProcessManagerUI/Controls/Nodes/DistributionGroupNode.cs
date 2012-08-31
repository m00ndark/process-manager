using System;
using System.Collections.Generic;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class DistributionGroupNode : BaseDistributionRootNode
	{
		private DistributionGroupNode(Group group, IEnumerable<INode> childNodes, DistributionGrouping grouping)
			: base(childNodes, grouping, !Settings.Client.D_CollapsedNodes[grouping].Contains(group.ID))
		{
			Group = group;
			//BackColor = Color.FromArgb(255, 208, 160);
		}

		#region Properties

		public Group Group { get; private set; }

		public override Guid ID { get { return Group.ID; } }
		protected override string NodeName { get { return Group.Name; } }

		#endregion

		#region Helpers

		protected override void UpdateDistributionAction(DistributionAction action)
		{
			action.SourceGroup = Group;
		}

		#endregion
	}
}

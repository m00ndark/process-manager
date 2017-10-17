using System;
using System.Collections.Generic;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class DistributionGroupNode : BaseDistributionRootNode
	{
		private readonly Guid _id;

		public DistributionGroupNode(Group group, Guid? machineID, IEnumerable<INode> childNodes, DistributionGrouping grouping)
			: this(machineID.HasValue ? MakeID(@group.ID, machineID.Value) : @group.ID, group, childNodes, grouping) { }

		private DistributionGroupNode(Guid id, Group group, IEnumerable<INode> childNodes, DistributionGrouping grouping)
			: base(childNodes, grouping, !Settings.Client.D_CollapsedNodes[grouping].Contains(group.ID))
		{
			_id = id;
			Group = group;
			//BackColor = Color.FromArgb(255, 208, 160);
		}

		#region Properties

		public Group Group { get; }

		public override Guid ID => _id;
		protected override string NodeName => Group.Name;

		#endregion

		#region Helpers

		protected override void UpdateDistributionAction(DistributionAction action)
		{
			action.Group = Group;
		}

		#endregion
	}
}

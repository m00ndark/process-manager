using System;
using System.Collections.Generic;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class GroupNode : BaseRootNode
	{
		private readonly Guid _id;

		public GroupNode(Group group, Guid? machineID, IEnumerable<IControlPanelNode> childNodes, ControlPanelGrouping grouping)
			: this((machineID.HasValue ? MakeID(group.ID, machineID.Value) : group.ID), group, childNodes, grouping) { }

		private GroupNode(Guid id, Group group, IEnumerable<IControlPanelNode> childNodes, ControlPanelGrouping grouping)
			: base(childNodes, grouping, !Settings.Client.CP_CollapsedNodes[grouping].Contains(id))
		{
			_id = id;
			Group = group;
			//BackColor = Color.FromArgb(255, 208, 160);
		}

		#region Properties

		public Group Group { get; private set; }

		public override Guid ID { get { return _id; } }
		protected override string NodeName { get { return Group.Name; } }

		#endregion

		#region Helpers

		protected override void UpdateApplicationAction(ApplicationAction action)
		{
			action.Group = Group;
		}

		#endregion
	}
}

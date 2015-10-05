using System;
using System.Collections.Generic;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class ProcessGroupNode : BaseProcessRootNode
	{
		private readonly Guid _id;

		public ProcessGroupNode(Group group, Guid? machineID, IEnumerable<INode> childNodes, ProcessGrouping grouping)
			: this((machineID.HasValue ? MakeID(group.ID, machineID.Value) : group.ID), group, childNodes, grouping) { }

		private ProcessGroupNode(Guid id, Group group, IEnumerable<INode> childNodes, ProcessGrouping grouping)
			: base(childNodes, grouping, !Settings.Client.P_CollapsedNodes[grouping].Contains(id))
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

		protected override void UpdateProcessAction(ProcessAction action)
		{
			action.Group = Group;
		}

		#endregion
	}
}

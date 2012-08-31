using System;
using System.Collections.Generic;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class DistributionApplicationNode : BaseDistributionRootNode
	{
		private readonly Guid _id;

		public DistributionApplicationNode(Machine machine, Guid? groupID, IEnumerable<INode> childNodes, ControlPanelGrouping grouping)
			: this((groupID.HasValue ? MakeID(machine.ID, groupID.Value) : machine.ID), machine, childNodes, grouping) { }

		private DistributionApplicationNode(Guid id, Machine machine, IEnumerable<INode> childNodes, ControlPanelGrouping grouping)
			: base(childNodes, grouping, !Settings.Client.CP_CollapsedNodes[grouping].Contains(id))
		{
			_id = id;
			Machine = machine;
			//BackColor = Color.FromArgb(255, 224, 192);
		}

		#region Properties

		public Machine Machine { get; private set; }

		public override Guid ID { get { return _id; } }
		protected override string NodeName { get { return Machine.HostName; } }

		#endregion

		#region Helpers

		protected override void UpdateApplicationAction(ApplicationAction action)
		{
			action.Machine = Machine;
		}

		#endregion
	}
}

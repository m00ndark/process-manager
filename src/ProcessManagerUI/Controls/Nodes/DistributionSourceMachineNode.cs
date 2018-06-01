using System;
using System.Collections.Generic;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class DistributionSourceMachineNode : BaseDistributionRootNode
	{
		private readonly Guid _id;

		public DistributionSourceMachineNode(Machine machine, Guid? groupID, IEnumerable<INode> childNodes, DistributionGrouping grouping)
			: this(groupID.HasValue ? MakeID(machine.ID, groupID.Value) : machine.ID, machine, childNodes, grouping) { }

		private DistributionSourceMachineNode(Guid id, Machine machine, IEnumerable<INode> childNodes, DistributionGrouping grouping)
			: base(childNodes, grouping, !Settings.Client.D_CollapsedNodes[grouping].Contains(machine.ID))
		{
			_id = id;
			Machine = machine;
			//BackColor = Color.FromArgb(255, 224, 192);
		}

		#region Properties

		public Machine Machine { get; }

		public override Guid ID => _id;
		protected override string NodeName => Machine.HostName;

		#endregion

		#region Helpers

		protected override void UpdateDistributionAction(DistributionAction action)
		{
			action.SourceMachine = Machine;
		}

		#endregion
	}
}

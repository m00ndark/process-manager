using System;
using System.Collections.Generic;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class DistributionSourceMachineNode : BaseDistributionRootNode
	{
		private DistributionSourceMachineNode(Machine machine, IEnumerable<INode> childNodes, DistributionGrouping grouping)
			: base(childNodes, grouping, !Settings.Client.D_CollapsedNodes[grouping].Contains(machine.ID))
		{
			Machine = machine;
			//BackColor = Color.FromArgb(255, 224, 192);
		}

		#region Properties

		public Machine Machine { get; private set; }

		public override Guid ID { get { return Machine.ID; } }
		protected override string NodeName { get { return Machine.HostName; } }

		#endregion

		#region Helpers

		protected override void UpdateDistributionAction(DistributionAction action)
		{
			action.SourceMachine = Machine;
		}

		#endregion
	}
}

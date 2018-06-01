using System;
using System.Collections.Generic;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class ProcessMachineNode : BaseProcessRootNode
	{
		private readonly Guid _id;

		public ProcessMachineNode(Machine machine, Guid? groupID, IEnumerable<INode> childNodes, ProcessGrouping grouping)
			: this(groupID.HasValue ? MakeID(machine.ID, groupID.Value) : machine.ID, machine, childNodes, grouping) { }

		private ProcessMachineNode(Guid id, Machine machine, IEnumerable<INode> childNodes, ProcessGrouping grouping)
			: base(childNodes, grouping, !Settings.Client.P_CollapsedNodes[grouping].Contains(id))
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

		protected override void UpdateProcessAction(ProcessAction action)
		{
			action.Machine = Machine;
		}

		#endregion
	}
}

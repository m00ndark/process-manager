using System;
using System.Collections.Generic;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class MachineNode : BaseRootNode
	{
		public MachineNode(Machine machine, IEnumerable<IControlPanelNode> childNodes) : base(childNodes)
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

		protected override void UpdateApplicationAction(ApplicationAction action)
		{
			action.Machine = Machine;
		}

		#endregion
	}
}

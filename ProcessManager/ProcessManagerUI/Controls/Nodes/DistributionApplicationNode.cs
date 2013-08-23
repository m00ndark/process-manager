using System;
using System.Collections.Generic;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public class DistributionApplicationNode : BaseDistributionRootNode
	{
		public DistributionApplicationNode(Application application, IEnumerable<INode> childNodes, DistributionGrouping grouping)
			: base(childNodes, grouping, !Settings.Client.D_CollapsedNodes[grouping].Contains(application.ID))
		{
			Application = application;
			//BackColor = Color.FromArgb(255, 224, 192);
		}

		#region Properties

		public Application Application { get; private set; }

		public override Guid ID { get { return Application.ID; } }
		protected override string NodeName { get { return Application.Name; } }

		#endregion

		#region Helpers

		protected override void UpdateDistributionAction(DistributionAction action)
		{
			action.Application = Application;
		}

		#endregion
	}
}

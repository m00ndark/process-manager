using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProcessManager.DataObjects;

namespace ProcessManagerUI.Controls.Nodes
{
	public partial class MachineNode : UserControl, IControlPanelNode
	{
		public MachineNode(Machine machine, IEnumerable<IControlPanelNode> childNodes)
		{
			InitializeComponent();
			Machine = machine;
			ChildNodes = new List<IControlPanelNode>(childNodes);
		}

		#region Properties

		public Machine Machine { get; private set; }
		public List<IControlPanelNode> ChildNodes { get; private set; }

		public Guid ID { get { return Machine.ID; } }

		#endregion

		public Size LayoutNode()
		{
			List<Size> childSizes = ChildNodes.Select(node => node.LayoutNode()).ToList();
			int totalChildrenHeight = childSizes.Sum(size => size.Height);
			int maxChildWidth = childSizes.Max(size => size.Width);
			labelMachineName.Text = Machine.HostName;
			Size = new Size(Math.Max(Size.Width - flowLayoutPanel.Size.Width + maxChildWidth, 
				Size.Width - tableLayoutPanel.GetColumnWidths()[0] + labelMachineName.Size.Width),
				Size.Height - flowLayoutPanel.Size.Height + totalChildrenHeight);
			ChildNodes.ForEach(node => flowLayoutPanel.Controls.Add((UserControl) node));
			return Size;
		}
	}
}

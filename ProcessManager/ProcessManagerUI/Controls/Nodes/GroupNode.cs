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
	public partial class GroupNode : UserControl, IControlPanelNode
	{
		public GroupNode(Group group, IEnumerable<IControlPanelNode> childNodes)
		{
			InitializeComponent();
			Group = group;
			ChildNodes = new List<IControlPanelNode>(childNodes);
		}

		#region Properties

		public Group Group { get; private set; }
		public List<IControlPanelNode> ChildNodes { get; set; }

		public Guid ID { get { return Group.ID; } }

		#endregion

		public Size LayoutNode()
		{
			List<Size> childSizes = ChildNodes.Select(node => node.LayoutNode()).ToList();
			int totalChildrenHeight = childSizes.Sum(size => size.Height);
			int maxChildWidth = childSizes.Max(size => size.Width);
			labelGroupName.Text = Group.Name;
			Size = new Size(Math.Max(Size.Width - flowLayoutPanel.Size.Width + maxChildWidth,
				Size.Width - tableLayoutPanel.GetColumnWidths()[0] + labelGroupName.Size.Width),
				Size.Height - flowLayoutPanel.Size.Height + totalChildrenHeight);
			ChildNodes.ForEach(node => flowLayoutPanel.Controls.Add((UserControl) node));
			return Size;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public abstract class BaseRootNode : UserControl, IControlPanelRootNode
	{
		private bool _disableEvents;
		private Size _childrenSize;
		private bool _expanded;

		public event EventHandler CheckedChanged;

		protected BaseRootNode(IEnumerable<IControlPanelNode> childNodes)
		{
			ChildNodes = new List<IControlPanelNode>(childNodes);
			_disableEvents = false;
			_childrenSize = new Size(0, 0);
			_expanded = true;
			ChildNodes.ForEach(node => node.CheckedChanged += ControlPanelNode_CheckedChanged);
			ChildNodes.Select(node => node as IControlPanelRootNode).Where(node => node != null).ToList()
				.ForEach(node => node.SizeChanged += ControlPanelRootNode_SizeChanged);
		}

		#region Properties

		public List<IControlPanelNode> ChildNodes { get; private set; }

		public bool Expanded
		{
			get { return _expanded; }
			private set
			{
				_expanded = value;
				ApplyExpandedCollapsed();
			}
		}

		public CheckState CheckState { get { return CheckBox.CheckState; } }

		public abstract Guid ID { get; }
		protected abstract string NodeName { get; }
		protected abstract CheckBox CheckBox { get; }
		protected abstract FlowLayoutPanel FlowLayoutPanel { get; }
		protected abstract Label NameLabel { get; }
		protected abstract PictureBox ExpandCollapsePictureBox { get; }
		protected abstract TableLayoutPanel TableLayoutPanel { get; }
		protected abstract LinkLabel StartLinkLabel { get; }
		protected abstract LinkLabel StopLinkLabel { get; }
		protected abstract LinkLabel RestartLinkLabel { get; }

		#endregion

		#region GUI event handlers

		protected void Handle_PictureBoxExpandCollapse_MouseDown()
		{
			Expanded = !Expanded;
		}

		protected void Handle_CheckBoxSelected_CheckedChanged()
		{
			if (!_disableEvents)
				RaiseCheckedChangedEvent();

			EnableActionLinks(CheckState != CheckState.Unchecked);

			if (CheckState != CheckState.Indeterminate)
				ChildNodes.ForEach(node => node.Check(CheckState == CheckState.Checked));
		}

		protected void Handle_LinkLabelStart_LinkClicked()
		{
			Start();
		}

		protected void Handle_LinkLabelStop_LinkClicked()
		{
			Stop();
		}

		protected void Handle_LinkLabelRestart_LinkClicked()
		{
			Restart();
		}

		#endregion

		#region Control panel node event handlers

		protected void ControlPanelRootNode_SizeChanged(object sender, EventArgs e)
		{
			if (FlowLayoutPanel.Controls.Count > 0)
			{
				_childrenSize.Height = ChildNodes.Select(node => node.Size).Sum(size => size.Height);
				ApplyExpandedCollapsed();
			}
		}

		protected void ControlPanelNode_CheckedChanged(object sender, EventArgs e)
		{
			int checkedCount = ChildNodes.Count(node => node.CheckState == CheckState.Checked);
			int uncheckedCount = ChildNodes.Count(node => node.CheckState == CheckState.Unchecked);
			CheckBox.CheckState = (checkedCount == ChildNodes.Count ? CheckState.Checked
				: (uncheckedCount == ChildNodes.Count ? CheckState.Unchecked : CheckState.Indeterminate));
		}

		#endregion

		#region Implementation of IControlPanelRootNode

		public void ExpandAll(bool expanded)
		{
			ChildNodes.Select(node => node as IControlPanelRootNode).Where(node => node != null).ToList().ForEach(node => node.ExpandAll(expanded));
			Expanded = expanded;
		}

		#endregion

		#region Implementation of IControlPanelNode

		public Size LayoutNode()
		{
			List<Size> childSizes = ChildNodes.Select(node => node.LayoutNode()).ToList();
			_childrenSize.Height = childSizes.Sum(size => size.Height);
			_childrenSize.Width = childSizes.Max(size => size.Width);
			NameLabel.Text = NodeName;
			ApplyExpandedCollapsed();
			ChildNodes.ForEach(node => FlowLayoutPanel.Controls.Add((UserControl) node));
			return Size;
		}

		public void ForceWidth(int width)
		{
			Size = new Size(width, Size.Height);
			ChildNodes.ForEach(node => node.ForceWidth(FlowLayoutPanel.Size.Width));
		}

		public void Check(bool @checked)
		{
			_disableEvents = true;
			CheckBox.Checked = @checked;
			_disableEvents = false;
		}

		public void Start()
		{
			ChildNodes.ForEach(node => node.Start());
		}

		public void Stop()
		{
			ChildNodes.ForEach(node => node.Stop());
		}

		public void Restart()
		{
			ChildNodes.ForEach(node => node.Restart());
		}

		#endregion

		#region Event raisers

		private void RaiseCheckedChangedEvent()
		{
			if (CheckedChanged != null)
				CheckedChanged(this, new EventArgs());
		}

		#endregion

		#region Helpers

		private void ApplyExpandedCollapsed()
		{
			ExpandCollapsePictureBox.Image = (Expanded ? Properties.Resources.expanded_16 : Properties.Resources.collapsed_16);
			Size = new Size(Math.Max(Size.Width - FlowLayoutPanel.Size.Width + _childrenSize.Width,
				Size.Width - (int) TableLayoutPanel.ColumnStyles[0].Width + NameLabel.Size.Width),
				(Expanded ? Size.Height - FlowLayoutPanel.Size.Height + _childrenSize.Height : TableLayoutPanel.Size.Height));
			TableLayoutPanel.ColumnStyles[0].Width = NameLabel.Size.Width;
		}

		private void EnableActionLinks(bool enable)
		{
			StartLinkLabel.Enabled = enable;
			StopLinkLabel.Enabled = enable;
			RestartLinkLabel.Enabled = enable;
		}

		#endregion
	}
}

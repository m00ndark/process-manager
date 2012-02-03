using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public partial class BaseRootNode : UserControl, IControlPanelRootNode
	{
		private bool _disableEvents;
		private Size _childrenSize;
		private bool _expanded;

		public event EventHandler CheckedChanged;
		public event EventHandler<ApplicationActionEventArgs> ActionTaken;

		protected BaseRootNode(IEnumerable<IControlPanelNode> childNodes)
		{
			InitializeComponent();
			_disableEvents = false;
			_childrenSize = new Size(0, 0);
			_expanded = true;
			ChildNodes = new List<IControlPanelNode>(childNodes);
			ChildNodes.Select(node => node as IControlPanelRootNode).Where(node => node != null).ToList()
				.ForEach(node => node.SizeChanged += ControlPanelRootNode_SizeChanged);
			ChildNodes.ForEach(node =>
				{
					node.CheckedChanged += ControlPanelNode_CheckedChanged;
					node.ActionTaken += ControlPanelNode_ActionTaken;
				});
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

		public CheckState CheckState { get { return checkBoxSelected.CheckState; } }

		public virtual Guid ID { get { throw new InvalidOperationException("Class must be inherited!"); } }
		protected virtual string NodeName { get { throw new InvalidOperationException("Class must be inherited!"); } }

		#endregion

		#region GUI event handlers

		private void PictureBoxExpandCollapse_MouseDown(object sender, MouseEventArgs e)
		{
			Expanded = !Expanded;
		}

		private void CheckBoxSelected_CheckedChanged(object sender, EventArgs e)
		{
			if (!_disableEvents)
				RaiseCheckedChangedEvent();

			EnableActionLinks(CheckState != CheckState.Unchecked);

			if (CheckState != CheckState.Indeterminate)
				ChildNodes.ForEach(node => node.Check(CheckState == CheckState.Checked));
		}

		private void LinkLabelStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			TakeAction(ApplicationActionType.Start);
		}

		private void LinkLabelStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			TakeAction(ApplicationActionType.Stop);
		}

		private void LinkLabelRestart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			TakeAction(ApplicationActionType.Restart);
		}

		#endregion

		#region Control panel node event handlers

		private void ControlPanelRootNode_SizeChanged(object sender, EventArgs e)
		{
			if (flowLayoutPanel.Controls.Count > 0)
			{
				_childrenSize.Height = ChildNodes.Select(node => node.Size).Sum(size => size.Height);
				ApplyExpandedCollapsed();
			}
		}

		private void ControlPanelNode_CheckedChanged(object sender, EventArgs e)
		{
			int checkedCount = ChildNodes.Count(node => node.CheckState == CheckState.Checked);
			int uncheckedCount = ChildNodes.Count(node => node.CheckState == CheckState.Unchecked);
			checkBoxSelected.CheckState = (checkedCount == ChildNodes.Count ? CheckState.Checked
				: (uncheckedCount == ChildNodes.Count ? CheckState.Unchecked : CheckState.Indeterminate));
		}

		private void ControlPanelNode_ActionTaken(object sender, ApplicationActionEventArgs e)
		{
			ApplicationAction action = e.Action;
			UpdateApplicationAction(action);
			RaiseActionTakenEvent(action);
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
			labelNodeName.Text = NodeName;
			ApplyExpandedCollapsed();
			ChildNodes.ForEach(node => flowLayoutPanel.Controls.Add((UserControl) node));
			return Size;
		}

		public void ForceWidth(int width)
		{
			Size = new Size(width, Size.Height);
			ChildNodes.ForEach(node => node.ForceWidth(flowLayoutPanel.Size.Width));
		}

		public void Check(bool @checked)
		{
			_disableEvents = true;
			checkBoxSelected.Checked = @checked;
			_disableEvents = false;
		}

		public void TakeAction(ApplicationActionType type)
		{
			ChildNodes.ForEach(node => node.TakeAction(type));
		}

		#endregion

		#region Event raisers

		private void RaiseCheckedChangedEvent()
		{
			if (CheckedChanged != null)
				CheckedChanged(this, new EventArgs());
		}

		private void RaiseActionTakenEvent(ApplicationAction action)
		{
			if (ActionTaken != null)
				ActionTaken(this, new ApplicationActionEventArgs(action));
		}

		#endregion

		#region Helpers

		private void ApplyExpandedCollapsed()
		{
			pictureBoxExpandCollapse.Image = (Expanded ? Properties.Resources.expanded_16 : Properties.Resources.collapsed_16);
			Size = new Size(Math.Max(Size.Width - flowLayoutPanel.Size.Width + _childrenSize.Width,
				Size.Width - (int) tableLayoutPanel.ColumnStyles[0].Width + labelNodeName.Size.Width),
				(Expanded ? Size.Height - flowLayoutPanel.Size.Height + _childrenSize.Height : tableLayoutPanel.Size.Height));
			tableLayoutPanel.ColumnStyles[0].Width = labelNodeName.Size.Width;
		}

		private void EnableActionLinks(bool enable)
		{
			linkLabelStart.Enabled = enable;
			linkLabelStop.Enabled = enable;
			linkLabelRestart.Enabled = enable;
		}

		protected virtual void UpdateApplicationAction(ApplicationAction action)
		{
			throw new InvalidOperationException("Class must be inherited!");
		}

		#endregion
	}
}

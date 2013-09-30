using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public partial class BaseMacroRootNode : UserControl, IMacroRootNode
	{
		private bool _ignoreCheckedChangedEvents;
		private Size _childrenSize;
		private bool _expanded;
		private MacroActionState _state;

		public event EventHandler CheckedChanged;
		public event EventHandler<ActionEventArgs> ActionTaken;
		public event EventHandler StateChanged;

		protected BaseMacroRootNode(IEnumerable<IMacroNode> childNodes, bool expanded)
		{
			InitializeComponent();
			_ignoreCheckedChangedEvents = false;
			_childrenSize = new Size(0, 0);
			_expanded = expanded;
			_state = MacroActionState.Unknown;
			ChildNodes = new List<IMacroNode>(childNodes);
			ChildNodes.Where(node => node is IRootNode).Cast<IRootNode>().ToList()
				.ForEach(node => node.SizeChanged += RootNode_SizeChanged);
			ChildNodes.ForEach(node =>
				{
					node.CheckedChanged += Node_CheckedChanged;
					node.ActionTaken += Node_ActionTaken;
					node.StateChanged += MacroNode_StateChanged;
				});
		}

		#region Properties

		public List<IMacroNode> ChildNodes { get; private set; }

		public bool Expanded
		{
			get { return _expanded; }
			private set
			{
				_expanded = value;
				ApplyExpandedCollapsed();
			}
		}

		public MacroActionState State
		{
			get { return _state; }
			set
			{
				_state = value;
				ApplyState();
				RaiseStateChangedEvent();
			}
		}

		public bool IsComplete { get { return NodeName != null && ChildNodes.Aggregate(true, (areComplete, node) => areComplete && node.IsComplete); } }
		public CheckState CheckState { get { return checkBoxSelected.CheckState; } }

		public virtual Guid ID { get { throw new InvalidOperationException("Class must be inherited!"); } }
		protected virtual string NodeName { get { throw new InvalidOperationException("Class must be inherited!"); } }

		#endregion

		#region GUI event handlers

		private void PictureBoxExpandCollapse_MouseDown(object sender, MouseEventArgs e)
		{
			Expanded = !Expanded;

			if (Expanded)
				Settings.Client.M_CollapsedNodes.Remove(ID);
			else if (!Settings.Client.M_CollapsedNodes.Contains(ID))
				Settings.Client.M_CollapsedNodes.Add(ID);
		}

		private void CheckBoxSelected_CheckStateChanged(object sender, EventArgs e)
		{
			RaiseCheckedChangedEvent();

			EnableActionLinks(CheckState != CheckState.Unchecked);

			if (CheckState != CheckState.Indeterminate)
			{
				_ignoreCheckedChangedEvents = true;
				ChildNodes.ForEach(node => node.Check(CheckState == CheckState.Checked));
				_ignoreCheckedChangedEvents = false;
			}
		}

		private void LinkLabelPlay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			TakeAction(ActionType.Play);
		}

		#endregion

		#region Node event handlers

		private void RootNode_SizeChanged(object sender, EventArgs e)
		{
			if (flowLayoutPanel.Controls.Count > 0)
			{
				_childrenSize.Height = ChildNodes.Select(node => node.Size).Sum(size => size.Height);
				ApplyExpandedCollapsed();
			}
		}

		private void Node_CheckedChanged(object sender, EventArgs e)
		{
			if (_ignoreCheckedChangedEvents) return;
			int checkedCount = ChildNodes.Count(node => node.CheckState == CheckState.Checked);
			int uncheckedCount = ChildNodes.Count(node => node.CheckState == CheckState.Unchecked);
			checkBoxSelected.CheckState = (checkedCount == ChildNodes.Count ? CheckState.Checked
				: (uncheckedCount == ChildNodes.Count ? CheckState.Unchecked : CheckState.Indeterminate));
		}

		private void Node_ActionTaken(object sender, ActionEventArgs e)
		{
			MacroAction action = (MacroAction) e.Action;
			UpdateMacroAction(action);
			RaiseActionTakenEvent(action);
		}

		private void MacroNode_StateChanged(object sender, EventArgs e)
		{
			State = ChildNodes
				.Where(childNode => childNode.CheckState != CheckState.Unchecked)
				.Aggregate(MacroActionState.Unknown, (actionState, childNode) =>
					childNode.State > actionState ? childNode.State : actionState);
		}

		#endregion

		#region Implementation of IRootNode

		public void ExpandAll(bool expanded)
		{
			Expanded = expanded;
			ChildNodes.Select(node => node as IRootNode).Where(node => node != null).ToList().ForEach(node => node.ExpandAll(expanded));
		}

		public IEnumerable<IMacroNode> GetCheckedLeafNodes()
		{
			foreach (IMacroNode childNode in ChildNodes)
			{
				IMacroRootNode rootNode = childNode as IMacroRootNode;
				if (rootNode != null)
				{
					foreach (IMacroNode node in rootNode.GetCheckedLeafNodes())
						yield return node;
				}
				else if (childNode.CheckState == CheckState.Checked)
					yield return childNode;
			}
		}

		#endregion

		#region Implementation of INode

		public new void Dispose()
		{
			foreach (IMacroNode node in ChildNodes)
			{
				IRootNode rootNode = node as IRootNode;
				if (rootNode != null)
					rootNode.SizeChanged -= RootNode_SizeChanged;

				node.CheckedChanged -= Node_CheckedChanged;
				node.ActionTaken -= Node_ActionTaken;
				node.StateChanged -= MacroNode_StateChanged;

				node.Dispose();
			}
			base.Dispose();
		}

		public Size LayoutNode()
		{
			List<Size> childSizes = ChildNodes.Select(node => node.LayoutNode()).ToList();
			_childrenSize.Height = childSizes.Sum(size => size.Height);
			_childrenSize.Width = childSizes.Max(size => size.Width);
			labelNodeName.Text = NodeName;
			ApplyExpandedCollapsed();
			ApplyState();
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
			checkBoxSelected.Checked = @checked;
		}

		public virtual void TakeAction(ActionType type)
		{
			throw new InvalidOperationException("Class must be inherited!");
		}

		#endregion

		#region Event raisers

		private void RaiseCheckedChangedEvent()
		{
			if (CheckedChanged != null)
				CheckedChanged(this, EventArgs.Empty);
		}

		protected void RaiseActionTakenEvent(IAction action)
		{
			if (ActionTaken != null)
				ActionTaken(this, new ActionEventArgs(action));
		}

		private void RaiseStateChangedEvent()
		{
			if (StateChanged != null)
				StateChanged(this, EventArgs.Empty);
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
			linkLabelPlay.Enabled = enable;
		}

		protected virtual void UpdateMacroAction(MacroAction action)
		{
			throw new InvalidOperationException("Class must be inherited!");
		}

		private void ApplyState()
		{
			switch (State)
			{
				case MacroActionState.Ongoing:
					pictureBoxStatus.Image = Properties.Resources.macro_ongoing_16;
					break;
				case MacroActionState.Success:
					pictureBoxStatus.Image = Properties.Resources.macro_success_16;
					break;
				case MacroActionState.Failure:
					pictureBoxStatus.Image = Properties.Resources.macro_failure_16;
					break;
				default:
					pictureBoxStatus.Image = Properties.Resources.macro_unknown_16;
					break;
			}
		}

		#endregion
	}
}

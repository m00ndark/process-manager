﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ToolComponents.Core;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public partial class BaseProcessRootNode : UserControl, IRootNode
	{
		private bool _ignoreCheckedChangedEvents;
		private Size _childrenSize;
		private readonly ProcessGrouping _grouping;
		private bool _expanded;

		public event EventHandler CheckedChanged;
		public event EventHandler<ActionEventArgs> ActionTaken;

		protected BaseProcessRootNode(IEnumerable<INode> childNodes, ProcessGrouping grouping, bool expanded)
		{
			InitializeComponent();
			_ignoreCheckedChangedEvents = false;
			_childrenSize = new Size(0, 0);
			_grouping = grouping;
			_expanded = expanded;
			ChildNodes = new List<INode>(childNodes);
			ChildNodes.Where(node => node is IRootNode).Cast<IRootNode>().ToList()
				.ForEach(node => node.SizeChanged += RootNode_SizeChanged);
			ChildNodes.ForEach(node =>
				{
					node.CheckedChanged += Node_CheckedChanged;
					node.ActionTaken += Node_ActionTaken;
				});
		}

		#region Properties

		public List<INode> ChildNodes { get; }

		public bool Expanded
		{
			get { return _expanded; }
			private set
			{
				_expanded = value;
				ApplyExpandedCollapsed();
			}
		}

		public CheckState CheckState => checkBoxSelected.CheckState;

		public virtual Guid ID { get { throw new InvalidOperationException("Class must be inherited!"); } }
		protected virtual string NodeName { get { throw new InvalidOperationException("Class must be inherited!"); } }

		#endregion

		#region GUI event handlers

		private void PictureBoxExpandCollapse_MouseDown(object sender, MouseEventArgs e)
		{
			Expanded = !Expanded;

			if (Expanded)
				Settings.Client.P_CollapsedNodes[_grouping].Remove(ID);
			else if (!Settings.Client.P_CollapsedNodes[_grouping].Contains(ID))
				Settings.Client.P_CollapsedNodes[_grouping].Add(ID);
		}

		private void CheckBoxSelected_CheckStateChanged(object sender, EventArgs e)
		{
			RaiseCheckedChangedEvent();

			EnableActionLinks(CheckState != CheckState.Unchecked);

			if (CheckState == CheckState.Indeterminate)
				return;

			_ignoreCheckedChangedEvents = true;
			ChildNodes.ForEach(node => node.Check(CheckState == CheckState.Checked));
			_ignoreCheckedChangedEvents = false;
		}

		private void LinkLabelStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			TakeAction(ActionType.Start);
		}

		private void LinkLabelStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			TakeAction(ActionType.Stop);
		}

		private void LinkLabelRestart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			TakeAction(ActionType.Restart);
		}

		#endregion

		#region Node event handlers

		private void RootNode_SizeChanged(object sender, EventArgs e)
		{
			if (flowLayoutPanel.Controls.Count == 0)
				return;

			_childrenSize.Height = ChildNodes.Select(node => node.Size).Sum(size => size.Height);
			ApplyExpandedCollapsed();
		}

		private void Node_CheckedChanged(object sender, EventArgs e)
		{
			if (_ignoreCheckedChangedEvents) return;
			int checkedCount = ChildNodes.Count(node => node.CheckState == CheckState.Checked);
			int uncheckedCount = ChildNodes.Count(node => node.CheckState == CheckState.Unchecked);
			checkBoxSelected.CheckState = checkedCount == ChildNodes.Count ? CheckState.Checked
				: (uncheckedCount == ChildNodes.Count ? CheckState.Unchecked : CheckState.Indeterminate);
		}

		private void Node_ActionTaken(object sender, ActionEventArgs e)
		{
			ProcessAction action = (ProcessAction) e.Action;
			UpdateProcessAction(action);
			RaiseActionTakenEvent(action);
		}

		#endregion

		#region Implementation of IRootNode

		public void ExpandAll(bool expanded)
		{
			Expanded = expanded;
			ChildNodes.Select(node => node as IRootNode).Where(node => node != null).ToList().ForEach(node => node.ExpandAll(expanded));
		}

		#endregion

		#region Implementation of INode

		public new void Dispose()
		{
			foreach (INode node in ChildNodes)
			{
				IRootNode rootNode = node as IRootNode;
				if (rootNode != null)
					rootNode.SizeChanged -= RootNode_SizeChanged;

				node.CheckedChanged -= Node_CheckedChanged;
				node.ActionTaken -= Node_ActionTaken;

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

		public void TakeAction(ActionType type)
		{
			ChildNodes.ForEach(node => node.TakeAction(type));
		}

		#endregion

		#region Event raisers

		private void RaiseCheckedChangedEvent()
		{
			CheckedChanged?.Invoke(this, EventArgs.Empty);
		}

		private void RaiseActionTakenEvent(ProcessAction action)
		{
			ActionTaken?.Invoke(this, new ActionEventArgs(action));
		}

		#endregion

		#region Helpers

		protected static Guid MakeID(Guid machineID, Guid groupID)
		{
			return Cryptographer.CreateGuid(machineID.ToString() + groupID.ToString());
		}

		private void ApplyExpandedCollapsed()
		{
			pictureBoxExpandCollapse.Image = Expanded ? Properties.Resources.expanded_16 : Properties.Resources.collapsed_16;
			Size = new Size(Math.Max(Size.Width - flowLayoutPanel.Size.Width + _childrenSize.Width,
				Size.Width - (int) tableLayoutPanel.ColumnStyles[0].Width + labelNodeName.Size.Width),
				Expanded ? Size.Height - flowLayoutPanel.Size.Height + _childrenSize.Height : tableLayoutPanel.Size.Height);
			tableLayoutPanel.ColumnStyles[0].Width = labelNodeName.Size.Width;
		}

		private void EnableActionLinks(bool enable)
		{
			linkLabelStart.Enabled = enable;
			linkLabelStop.Enabled = enable;
			linkLabelRestart.Enabled = enable;
		}

		protected virtual void UpdateProcessAction(ProcessAction action)
		{
			throw new InvalidOperationException("Class must be inherited!");
		}

		#endregion
	}
}

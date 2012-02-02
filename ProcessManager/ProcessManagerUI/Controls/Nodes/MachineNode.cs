using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public partial class MachineNode : BaseRootNode
	{
		public MachineNode(Machine machine, IEnumerable<IControlPanelNode> childNodes) : base(childNodes)
		{
			InitializeComponent();
			Machine = machine;
			//BackColor = Color.FromArgb(255, 224, 192);
		}

		#region Properties

		public Machine Machine { get; private set; }

		public override Guid ID { get { return Machine.ID; } }
		protected override string NodeName { get { return Machine.HostName; } }
		protected override CheckBox CheckBox { get { return checkBoxSelected; } }
		protected override FlowLayoutPanel FlowLayoutPanel { get { return flowLayoutPanel; } }
		protected override Label NameLabel { get { return labelMachineName; } }
		protected override PictureBox ExpandCollapsePictureBox { get { return pictureBoxExpandCollapse; } }
		protected override TableLayoutPanel TableLayoutPanel { get { return tableLayoutPanel; } }
		protected override LinkLabel StartLinkLabel { get { return linkLabelStart; } }
		protected override LinkLabel StopLinkLabel { get { return linkLabelStop; } }
		protected override LinkLabel RestartLinkLabel { get { return linkLabelRestart; } }

		#endregion

		#region GUI event handlers

		private void PictureBoxExpandCollapse_MouseDown(object sender, MouseEventArgs e)
		{
			Handle_PictureBoxExpandCollapse_MouseDown();
		}

		private void CheckBoxSelected_CheckedChanged(object sender, EventArgs e)
		{
			Handle_CheckBoxSelected_CheckedChanged();
		}

		private void LinkLabelStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Handle_LinkLabelStart_LinkClicked();
		}

		private void LinkLabelStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Handle_LinkLabelStop_LinkClicked();
		}

		private void LinkLabelRestart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Handle_LinkLabelRestart_LinkClicked();
		}

		#endregion
	}
}

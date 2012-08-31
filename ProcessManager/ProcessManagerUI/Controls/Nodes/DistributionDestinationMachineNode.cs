using System;
using System.Drawing;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Utilities;
using ProcessManagerUI.Controls.Nodes.Support;

namespace ProcessManagerUI.Controls.Nodes
{
	public partial class DistributionDestinationMachineNode : UserControl, INode
	{
		private readonly Guid _id;

		public event EventHandler CheckedChanged;
		public event EventHandler<ActionEventArgs> ActionTaken;

		public DistributionDestinationMachineNode(Machine destinationMachine, Guid sourceApplicationID, Guid sourceGroupID, Guid sourceMachineID)
		{
			InitializeComponent();
			DestinationMachine = destinationMachine;
			SourceApplicationID = sourceApplicationID;
			SourceGroupID = sourceGroupID;
			SourceMachineID = sourceMachineID;
			_id = MakeID(SourceMachineID, SourceGroupID, SourceApplicationID, DestinationMachine.ID);
			//BackColor = Color.FromArgb(255, 192, 128);
		}

		#region Properties

		public Machine DestinationMachine { get; private set; }
		public Guid SourceApplicationID { get; private set; }
		public Guid SourceGroupID { get; private set; }
		public Guid SourceMachineID { get; private set; }

		public Guid ID { get { return _id; } }
		public CheckState CheckState { get { return checkBoxSelected.CheckState; } }

		#endregion

		#region GUI event handlers

		private void CheckBoxSelected_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkBoxSelected.Checked)
				Settings.Client.D_CheckedNodes.Remove(ID);
			else if (!Settings.Client.D_CheckedNodes.Contains(ID))
				Settings.Client.D_CheckedNodes.Add(ID);

			RaiseCheckedChangedEvent();
		}

		private void LinkLabelDistribute_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			RaiseActionTakenEvent(new DistributionAction(DestinationMachine));
		}

		#endregion

		#region Implementation of INode

		public Size LayoutNode()
		{
			labelMachineName.Text = DestinationMachine.HostName;
			Size = new Size(labelMachineName.Location.X + labelMachineName.Size.Width, Size.Height);
			return Size;
		}

		public void ForceWidth(int width)
		{
			Size = new Size(width, Size.Height);
		}

		public void Check(bool @checked)
		{
			checkBoxSelected.Checked = @checked;
		}

		public void TakeAction(ActionType type)
		{
			if (type != ActionType.Distribute)
				throw new ArgumentException("Invalid action type");

			if (checkBoxSelected.Checked)
				RaiseActionTakenEvent(new DistributionAction(DestinationMachine));
		}

		#endregion

		#region Event raisers

		private void RaiseCheckedChangedEvent()
		{
			if (CheckedChanged != null)
				CheckedChanged(this, new EventArgs());
		}

		private void RaiseActionTakenEvent(IAction action)
		{
			if (ActionTaken != null)
				ActionTaken(this, new ActionEventArgs(action));
		}

		#endregion

		#region Helpers

		private static Guid MakeID(Guid sourceMachineID, Guid sourceGroupID, Guid sourceApplicationID, Guid destinationMachineID)
		{
			return Cryptographer.CreateGUID(sourceMachineID.ToString() + sourceGroupID.ToString()
				+ sourceApplicationID.ToString() + destinationMachineID.ToString());
		}

		#endregion

		public bool Matches(Guid sourceMachineID, Guid sourceGroupID, Guid sourceApplicationID, Guid destinationMachineID)
		{
			return Matches(MakeID(sourceMachineID, sourceGroupID, sourceApplicationID, destinationMachineID));
		}

		public bool Matches(Guid id)
		{
			return (ID == id);
		}
	}
}

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
	public enum DistributionState
	{
		Unknown,
		Ongoing,
		Success,
		Failure
	}

	public partial class DistributionDestinationMachineNode : UserControl, INode
	{
		private readonly Guid _id;
		private DistributionState _state;
		private string _message;

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
			_state = DistributionState.Unknown;
			_message = null;
			//BackColor = Color.FromArgb(255, 192, 128);
		}

		#region Properties

		public Machine DestinationMachine { get; private set; }
		public Guid SourceApplicationID { get; private set; }
		public Guid SourceGroupID { get; private set; }
		public Guid SourceMachineID { get; private set; }

		public DistributionState State
		{
			get { return _state; }
			set
			{
				_state = value;
				ApplyState();
			}
		}

		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				ApplyMessage();
			}
		}

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
			State = DistributionState.Ongoing;
			Message = null;
			RaiseActionTakenEvent(new DistributionAction(ActionType.Distribute, DestinationMachine));
		}

		#endregion

		#region Implementation of INode

		public Size LayoutNode()
		{
			labelMachineName.Text = DestinationMachine.HostName;
			Size = new Size(labelMachineName.Location.X + labelMachineName.Size.Width, Size.Height);
			ApplyState();
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
			{
				State = DistributionState.Ongoing;
				Message = null;
				RaiseActionTakenEvent(new DistributionAction(ActionType.Distribute, DestinationMachine));
			}
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

		private void ApplyState()
		{
			switch (State)
			{
				case DistributionState.Ongoing:
					pictureBoxStatus.Image = Properties.Resources.distribution_ongoing_16;
					break;
				case DistributionState.Success:
					pictureBoxStatus.Image = Properties.Resources.distribution_success_16;
					break;
				case DistributionState.Failure:
					pictureBoxStatus.Image = Properties.Resources.distribution_failure_16;
					break;
				default:
					pictureBoxStatus.Image = Properties.Resources.distribution_unknown_16;
					break;
			}
		}

		private void ApplyMessage()
		{
			toolTip.SetToolTip(pictureBoxStatus, _message);
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

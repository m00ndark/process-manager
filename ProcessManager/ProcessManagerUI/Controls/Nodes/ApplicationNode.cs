using System;
using System.Drawing;
using System.Windows.Forms;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManagerUI.Controls.Nodes.Support;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Controls.Nodes
{
	public partial class ApplicationNode : UserControl, IControlPanelNode
	{
		private ApplicationStatusValue _status;
		private bool _disabledEvents;

		public event EventHandler CheckedChanged;
		public event EventHandler<ApplicationActionEventArgs> ActionTaken;

		public ApplicationNode(Application application, Guid groupID, Guid machineID)
		{
			InitializeComponent();
			Application = application;
			GroupID = groupID;
			MachineID = machineID;
			_status = ApplicationStatusValue.Unknown;
			_disabledEvents = false;
			//BackColor = Color.FromArgb(255, 192, 128);
		}

		#region Properties

		public Application Application { get; private set; }
		public Guid GroupID { get; private set; }
		public Guid MachineID { get; private set; }
		
		public ApplicationStatusValue Status
		{
			get { return _status; }
			set
			{
				_status = value;
				ApplyStatus();
			}
		}

		public Guid ID { get { return Application.ID; } }
		public CheckState CheckState { get { return checkBoxSelected.CheckState; } }

		#endregion

		#region GUI event handlers

		private void CheckBoxSelected_CheckedChanged(object sender, EventArgs e)
		{
			if (!_disabledEvents)
				RaiseCheckedChangedEvent();
		}

		private void LinkLabelStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DoTakeAction(ApplicationActionType.Start);
		}

		private void LinkLabelStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DoTakeAction(ApplicationActionType.Stop);
		}

		private void LinkLabelRestart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DoTakeAction(ApplicationActionType.Restart);
		}

		#endregion

		#region Implementation of IControlPanelNode

		public Size LayoutNode()
		{
			labelApplicationName.Text = Application.Name;
			Size = new Size(labelApplicationName.Location.X + labelApplicationName.Size.Width, Size.Height);
			ApplyStatus();
			return Size;
		}

		public void ForceWidth(int width)
		{
			Size = new Size(width, Size.Height);
		}

		public void Check(bool @checked)
		{
			_disabledEvents = true;
			checkBoxSelected.Checked = @checked;
			_disabledEvents = false;
		}

		public void TakeAction(ApplicationActionType type)
		{
			if (checkBoxSelected.Checked)
				DoTakeAction(type);
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

		private void ApplyStatus()
		{
			switch (Status)
			{
				case ApplicationStatusValue.Running:
					pictureBoxStatus.Image = Properties.Resources.running_16;
					break;
				case ApplicationStatusValue.Stopped:
					pictureBoxStatus.Image = Properties.Resources.stopped_16;
					break;
				default:
					pictureBoxStatus.Image = Properties.Resources.unknown_16;
					break;
			}
		}

		private void DoTakeAction(ApplicationActionType type)
		{
			RaiseActionTakenEvent(new ApplicationAction(type, Application));
		}

		#endregion
	}
}

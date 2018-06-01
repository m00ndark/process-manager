using System;
using System.Drawing;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManagerUI.Controls.Nodes.Support;
using ToolComponents.Core;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Controls.Nodes
{
	public partial class ProcessApplicationNode : UserControl, INode
	{
		private readonly Guid _id;
		private ProcessStatusValue _status;
		private string _message;

		public event EventHandler CheckedChanged;
		public event EventHandler<ActionEventArgs> ActionTaken;

		public ProcessApplicationNode(Application application, Guid groupID, Guid machineID)
		{
			InitializeComponent();
			Application = application;
			GroupID = groupID;
			MachineID = machineID;
			_id = MakeID(MachineID, GroupID, Application.ID);
			_status = ProcessStatusValue.Unknown;
			_message = null;
			//BackColor = Color.FromArgb(255, 192, 128);
		}

		#region Properties

		public Application Application { get; }
		public Guid GroupID { get; }
		public Guid MachineID { get; }
		
		public ProcessStatusValue Status
		{
			get { return _status; }
			set
			{
				_status = value;
				ApplyStatus();
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

		public Guid ID => _id;
		public CheckState CheckState => checkBoxSelected.CheckState;

		#endregion

		#region GUI event handlers

		private void CheckBoxSelected_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkBoxSelected.Checked)
				Settings.Client.P_CheckedNodes.Remove(ID);
			else if (!Settings.Client.P_CheckedNodes.Contains(ID))
				Settings.Client.P_CheckedNodes.Add(ID);

			RaiseCheckedChangedEvent();
		}

		private void LinkLabelStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Message = null;
			RaiseActionTakenEvent(new ProcessAction(ActionType.Start, Application));
		}

		private void LinkLabelStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Message = null;
			RaiseActionTakenEvent(new ProcessAction(ActionType.Stop, Application));
		}

		private void LinkLabelRestart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Message = null;
			RaiseActionTakenEvent(new ProcessAction(ActionType.Restart, Application));
		}

		#endregion

		#region Implementation of INode

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
			checkBoxSelected.Checked = @checked;
		}

		public void TakeAction(ActionType type)
		{
			if (!checkBoxSelected.Checked)
				return;

			Message = null;
			RaiseActionTakenEvent(new ProcessAction(type, Application));
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

		private static Guid MakeID(Guid machineID, Guid groupID, Guid applicationID)
		{
			return Cryptographer.CreateGuid(machineID.ToString() + groupID.ToString() + applicationID.ToString());
		}

		private void ApplyStatus()
		{
			switch (Status)
			{
				case ProcessStatusValue.Running:
					pictureBoxStatus.Image = Properties.Resources.running_16;
					break;
				case ProcessStatusValue.Stopped:
					pictureBoxStatus.Image = Properties.Resources.stopped_16;
					break;
				case ProcessStatusValue.ActionError:
					pictureBoxStatus.Image = Properties.Resources.action_error_16;
					break;
				default:
					pictureBoxStatus.Image = Properties.Resources.unknown_16;
					break;
			}
		}

		private void ApplyMessage()
		{
			toolTip.SetToolTip(pictureBoxStatus, _message);
		}

		#endregion

		public bool Matches(Guid machineID, Guid groupID, Guid applicationID)
		{
			return Matches(MakeID(machineID, groupID, applicationID));
		}

		public bool Matches(Guid id)
		{
			return ID == id;
		}
	}
}

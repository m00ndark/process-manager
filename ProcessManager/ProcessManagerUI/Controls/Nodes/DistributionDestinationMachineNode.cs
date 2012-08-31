﻿using System;
using System.Drawing;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Utilities;
using ProcessManagerUI.Controls.Nodes.Support;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Controls.Nodes
{
	public partial class DistributionDestinationMachineNode : UserControl, INode
	{
		private readonly Guid _id;
		private ApplicationStatusValue _status;

		public event EventHandler CheckedChanged;
		public event EventHandler<ApplicationActionEventArgs> ActionTaken;

		public DistributionDestinationMachineNode(Application application, Guid groupID, Guid machineID)
		{
			InitializeComponent();
			Application = application;
			GroupID = groupID;
			MachineID = machineID;
			_id = MakeID(MachineID, GroupID, Application.ID);
			_status = ApplicationStatusValue.Unknown;
			//BackColor = Color.FromArgb(255, 192, 128);
		}

		#region Properties

		public Application Application { get; private set; }
		public Guid GroupID { get; private set; }
		public Guid MachineID { get; private set; }

		public Guid ID { get { return _id; } }
		public CheckState CheckState { get { return checkBoxSelected.CheckState; } }

		#endregion

		#region GUI event handlers

		private void CheckBoxSelected_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkBoxSelected.Checked)
				Settings.Client.CP_CheckedNodes.Remove(ID);
			else if (!Settings.Client.CP_CheckedNodes.Contains(ID))
				Settings.Client.CP_CheckedNodes.Add(ID);

			RaiseCheckedChangedEvent();
		}

		private void LinkLabelStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			RaiseActionTakenEvent(new ApplicationAction(ApplicationActionType.Start, Application));
		}

		private void LinkLabelStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			RaiseActionTakenEvent(new ApplicationAction(ApplicationActionType.Stop, Application));
		}

		private void LinkLabelRestart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			RaiseActionTakenEvent(new ApplicationAction(ApplicationActionType.Restart, Application));
		}

		#endregion

		#region Implementation of IControlPanelNode

		public Size LayoutNode()
		{
			labelMachineName.Text = Application.Name;
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

		public void TakeAction(ApplicationActionType type)
		{
			if (checkBoxSelected.Checked)
				RaiseActionTakenEvent(new ApplicationAction(type, Application));
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

		private static Guid MakeID(Guid machineID, Guid groupID, Guid applicationID)
		{
			return Cryptographer.CreateGUID(machineID.ToString() + groupID.ToString() + applicationID.ToString());
		}

		#endregion

		public bool Matches(Guid machineID, Guid groupID, Guid applicationID)
		{
			return Matches(MakeID(machineID, groupID, applicationID));
		}

		public bool Matches(Guid id)
		{
			return (ID == id);
		}
	}
}

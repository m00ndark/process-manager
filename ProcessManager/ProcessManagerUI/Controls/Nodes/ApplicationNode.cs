using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProcessManager.DataObjects;
using ProcessManagerUI.Controls.Nodes.Support;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Controls.Nodes
{
	public partial class ApplicationNode : UserControl, IControlPanelNode
	{
		private ApplicationStatusValue _applicationStatusValue;
		private bool _disabledEvents;

		public event EventHandler CheckedChanged;

		public ApplicationNode(Application application)
		{
			InitializeComponent();
			Application = application;
			_applicationStatusValue = ApplicationStatusValue.Unknown;
			_disabledEvents = false;
			//BackColor = Color.FromArgb(255, 192, 128);
		}

		#region Properties

		public Application Application { get; private set; }
		
		public ApplicationStatusValue ApplicationStatusValue
		{
			get { return _applicationStatusValue; }
			set
			{
				_applicationStatusValue = value;
				ApplyApplicationStatus();
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
			DoStart();
		}

		private void LinkLabelStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DoStop();
		}

		private void LinkLabelRestart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DoRestart();
		}

		#endregion

		#region Implementation of IControlPanelNode

		public Size LayoutNode()
		{
			labelApplicationName.Text = Application.Name;
			Size = new Size(labelApplicationName.Location.X + labelApplicationName.Size.Width, Size.Height);
			ApplyApplicationStatus();
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

		public void Start()
		{
			if (checkBoxSelected.Checked)
				DoStart();
		}

		public void Stop()
		{
			if (checkBoxSelected.Checked)
				DoStop();
		}

		public void Restart()
		{
			if (checkBoxSelected.Checked)
				DoRestart();
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

		private void ApplyApplicationStatus()
		{
			switch (ApplicationStatusValue)
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

		private void DoStart()
		{
			throw new NotImplementedException();
		}

		private void DoStop()
		{
			throw new NotImplementedException();
		}

		private void DoRestart()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProcessManager.DataObjects;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Controls.Nodes
{
	public partial class ApplicationNode : UserControl, IControlPanelNode
	{
		private ApplicationStatusValue _applicationStatusValue;

		public ApplicationNode(Application application)
		{
			InitializeComponent();
			Application = application;
			_applicationStatusValue = ApplicationStatusValue.Unknown;
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

		#endregion

		public Size LayoutNode()
		{
			labelApplicationName.Text = Application.Name;
			Size = new Size(labelApplicationName.Location.X + labelApplicationName.Size.Width, Size.Height);
			ApplyApplicationStatus();
			return Size;
		}

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
	}
}

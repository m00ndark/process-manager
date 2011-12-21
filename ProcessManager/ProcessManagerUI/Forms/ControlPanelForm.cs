using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;

namespace ProcessManagerUI.Forms
{
	public partial class ControlPanelForm : Form, IProcessManagerEventHandler
	{
		public ControlPanelForm()
		{
			InitializeComponent();
		}

		#region GUI events handlers

		private void ControlPanelForm_Load(object sender, EventArgs e)
		{
			ProcessManagerServiceHandler processManagerServiceHandler = new ProcessManagerServiceHandler(this, new Machine());
		}

		#endregion

		#region Implementation of IProcessManagerEventHandler

		public void ProcessManagerServiceEventHandler_ApplicationStatusesChanged(object sender, ApplicationStatusesEventArgs e)
		{
			
		}

		#endregion
	}
}

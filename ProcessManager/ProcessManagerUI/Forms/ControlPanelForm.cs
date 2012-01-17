﻿using System;
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
using ProcessManagerUI.Utilities;

namespace ProcessManagerUI.Forms
{
	public partial class ControlPanelForm : Form, IProcessManagerEventHandler
	{
		private readonly IDictionary<Machine, ProcessManagerServiceHandler> _serviceHandlers;
		private DateTime _formClosedAt;

		public ControlPanelForm()
		{
			InitializeComponent();
			_serviceHandlers = new Dictionary<Machine, ProcessManagerServiceHandler>();
			_formClosedAt = DateTime.MinValue;
		}

		#region GUI events handlers

		#region Main form

		private void ControlPanelForm_Load(object sender, EventArgs e)
		{
			HideForm();
			Settings.Client.Load();
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerInitializationCompleted += ServiceConnectionHandler_ServiceHandlerInitializationCompleted;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged += ServiceConnectionHandler_ServiceHandlerConnectionChanged;
			foreach (Machine machine in Settings.Client.Machines.Where(machine => !ConnectionStore.ConnectionCreated(machine)))
			{
				MachineConnection connection = ConnectionStore.CreateConnection(this, machine);
				connection.ServiceHandler.Initialize();
			}
		}

		private void ControlPanelForm_Deactivate(object sender, EventArgs e)
		{
			if (Opacity == 1)
				HideForm();
		}

		private void LinkLabelOpenConfiguration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			new ConfigurationForm(this).Show();
		}

		#endregion

		#region Notify icon

		private void NotifyIcon_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			if (Opacity == 1)
				HideForm();
		}

		private void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			if (Opacity == 0 && _formClosedAt.AddMilliseconds(500) < DateTime.Now)
				ShowForm();
		}

		#endregion

		#region System tray context menu

		private void ToolStripMenuItemSystemTrayConfiguration_Click(object sender, EventArgs e)
		{
			new ConfigurationForm(this).Show();
		}

		private void ToolStripMenuItemSystemTrayExit_Click(object sender, EventArgs e)
		{
			Close();
			Environment.Exit(0);
		}

		#endregion

		#endregion

		#region Connection handler event handlers

		private void ServiceConnectionHandler_ServiceHandlerInitializationCompleted(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerInitializationCompleted), sender, e);
				return;
			}

			// do stuff
		}

		private void ServiceConnectionHandler_ServiceHandlerConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerConnectionChanged), sender, e);
				return;
			}

			// do stuff
		}

		#endregion

		#region Implementation of IProcessManagerEventHandler

		public void ProcessManagerServiceEventHandler_ApplicationStatusesChanged(object sender, ApplicationStatusesEventArgs e)
		{
			
		}

		#endregion

		#region Helpers

		private void ShowForm()
		{
			Location = new Point(Math.Min(MousePosition.X - Size.Width / 2, Screen.PrimaryScreen.WorkingArea.Width - Size.Width - 8),
				Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 8 /* (isWindowsSeven ? 8 : 0) */);
			Opacity = 1;
			Show();
            try { Program.SetForegroundWindow(Handle); } catch { ; }
		}

		private void HideForm()
		{
			Hide();
			Opacity = 0;
			_formClosedAt = DateTime.Now;
		}

		#endregion
	}
}

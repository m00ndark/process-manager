using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;
using ProcessManagerUI.Utilities;

namespace ProcessManagerUI.Forms
{
	public partial class FileSystemBrowserForm : Form
	{
		#region Mode enum

		[Flags]
		public enum Mode
		{
			File,
			Folder
		}

		#endregion

		private bool _machineAvailable;

		public FileSystemBrowserForm(Machine machine)
		{
			InitializeComponent();

			Machine = machine;
			Path = string.Empty;
			Description = "Select a file or folder...";
			BrowserMode = Mode.File | Mode.Folder;
			_machineAvailable = ConnectionStore.ConnectionCreated(Machine);
		}

		#region Properties

		public Machine Machine { get; private set; }
		public string Path { get; set; }
		public string Description { get; set; }
		public Mode BrowserMode { get; set; }

		#endregion

		#region GUI event handlers

		private void FileSystemBrowserForm_Load(object sender, EventArgs e)
		{
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged += ServiceConnectionHandler_ServiceHandlerConnectionChanged;

			Text = Text + " [" + Machine + "]";
			labelDescription.Text = Description;

			if (BrowserMode == Mode.Folder)
			{
				splitContainer.Panel2Collapsed = true;
				Size = new Size(400, 420);
			}
			else
				Size = new Size(600, 420);

			Task.Factory.StartNew(DisplayDrives);
		}

		#endregion

		#region Service handler event handlers

		private void ServiceConnectionHandler_ServiceHandlerConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerConnectionChanged), sender, e);
				return;
			}

			Machine machine = ConnectionStore.Connections.Values.Where(x => x.ServiceHandler == e.ServiceHandler).Select(x => x.Machine).FirstOrDefault();
			if (machine != null && Machine == machine)
			{
				if (e.Status == ProcessManagerServiceHandlerStatus.Disconnected)
				{
					_machineAvailable = false;
					EnableControls(false);
					Messenger.ShowWarning("Connection lost", "The connection to the targeted machine was lost.");
				}
				else if (e.Status == ProcessManagerServiceHandlerStatus.Connected)
				{
					_machineAvailable = true;
					EnableControls();
				}
			}
		}

		#endregion

		#region Helpers

		private void EnableControls(bool enable = true)
		{
			splitContainer.Enabled = (enable && _machineAvailable);
			buttonOK.Enabled = (enable && _machineAvailable && listView.SelectedItems.Count == 1);
		}

		private delegate void DisplayDrivesDelegate();

		private void DisplayDrives()
		{
			if (InvokeRequired)
			{
				Invoke(new DisplayDrivesDelegate(DisplayDrives));
				return;
			}

			if (!_machineAvailable)
				return;

			Cursor = Cursors.WaitCursor; // ??

			IEnumerable<FileSystemDrive> fileSystemDrives = ConnectionStore.Connections[Machine].ServiceHandler.Service.GetFileSystemDrives().Select(x => x.FromDTO());
			treeView.Nodes.Clear();
			treeView.Nodes.AddRange(fileSystemDrives.Select(drive =>
				new TreeNode((!string.IsNullOrEmpty(drive.Label) ? drive.Label : drive.GetTypeDescription()) + " (" + drive.Name + ")") { Tag = drive }).ToArray());

			Cursor = Cursors.Default; // ??
		}

		#endregion
	}
}

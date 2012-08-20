using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;
using ProcessManagerUI.Properties;
using ProcessManagerUI.Support;
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
		private readonly IDictionary<IFileSystemEntry, IFileSystemEntry> _entryTree; // < child, parent >
		private readonly IDictionary<IFileSystemEntry, FileSystemTreeNode> _entryNodes;

		public FileSystemBrowserForm(Machine machine)
		{
			InitializeComponent();

			Machine = machine;
			SelectedPath = string.Empty;
			Description = "Select a file or folder...";
			BrowserMode = Mode.File | Mode.Folder;
			_machineAvailable = ConnectionStore.ConnectionCreated(Machine);
			_entryTree = new Dictionary<IFileSystemEntry, IFileSystemEntry>();
			_entryNodes = new Dictionary<IFileSystemEntry, FileSystemTreeNode>();

			imageList.Images.Add("Folder", Resources.folder_16);
			imageList.Images.Add(FileSystemDriveType.RemovableDisk.ToString(), Resources.drive_removable_disk_16);
			imageList.Images.Add(FileSystemDriveType.LocalDisk.ToString(), Resources.drive_local_disk_16);
			imageList.Images.Add(FileSystemDriveType.NetworkDrive.ToString(), Resources.drive_network_16);
			imageList.Images.Add(FileSystemDriveType.CompactDisc.ToString(), Resources.drive_compact_disc_16);
		}

		#region Properties

		public Machine Machine { get; private set; }
		public string SelectedPath { get; set; }
		public string Description { get; set; }
		public Mode BrowserMode { get; set; }

		public bool FolderMode
		{
			get { return ((BrowserMode & Mode.File) == 0); }
		}

		#endregion

		#region GUI event handlers

		private void FileSystemBrowserForm_Load(object sender, EventArgs e)
		{
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged += ServiceConnectionHandler_ServiceHandlerConnectionChanged;

			Text = Text + " [" + Machine + "]";
			labelDescription.Text = Description;

			if (FolderMode)
			{
				splitContainer.Panel2Collapsed = true;
				Size = new Size(400, 420);
			}
			else
				Size = new Size(600, 420);

			EnableControls();

			Task.Factory.StartNew(DisplayDrives);
		}

		private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Nodes.Count == 1 && !(e.Node.Nodes[0] is FileSystemTreeNode))
			{
				Worker.WaitFor("Retieving folder content, please wait...", () => (e.Node.Nodes.Count != 1 || e.Node.Nodes[0] is FileSystemTreeNode));
			}

			Task.Factory.StartNew(() => RequestFolders(((FileSystemTreeNode) e.Node).ChildEntries));
		}

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			EnableControls();
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			if (!EntrySelected())
				return;

			if (FolderMode)
			{
				SelectedPath = BuildPath(((FileSystemTreeNode) treeView.SelectedNode).Entry);
			}
			else
			{

			}
			DialogResult = DialogResult.OK;
			Close();
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

		private bool EntrySelected()
		{
			return (FolderMode && treeView.SelectedNode != null || !FolderMode && listView.SelectedItems.Count > 0);
		}

		private void EnableControls(bool enable = true)
		{
			splitContainer.Enabled = (enable && _machineAvailable);
			buttonOK.Enabled = (enable && _machineAvailable && EntrySelected());
		}

		private void DisplayDrives()
		{
			if (!_machineAvailable)
				return;

			using (new WaitCursor())
			{
				List<FileSystemDrive> drives = ConnectionStore.Connections[Machine].ServiceHandler.Service
					.GetFileSystemDrives().Select(x => x.FromDTO()).OrderBy(x => x.Name).ToList();

				List<FileSystemTreeNode> treeNodes = drives.Select(drive =>
					new FileSystemTreeNode((!string.IsNullOrEmpty(drive.Label) ? drive.Label : drive.GetTypeDescription()) + " (" + drive.Name + ")")
						{
							ImageKey = drive.Type.ToString(),
							SelectedImageKey = drive.Type.ToString(),
							Entry = drive
						}).ToList();

				foreach (FileSystemTreeNode treeNode in treeNodes)
				{
					_entryNodes.Add(treeNode.Entry, treeNode);
					treeNode.Nodes.Add("dummy");
				}

				SetTreeViewNodes(treeNodes);
				Task.Factory.StartNew(() => RequestFolders(drives));
			}
		}

		private void RequestFolders(IEnumerable<IFileSystemEntry> entries)
		{
			foreach (IFileSystemEntry entry in entries.Where(x => x.IsFolder))
			{
				if (!_machineAvailable)
					return;

				List<FileSystemEntry> childEntries = ConnectionStore.Connections[Machine].ServiceHandler.Service
					.GetFileSystemEntries(BuildPath(entry)).Select(x => x.FromDTO()).OrderBy(x => x.Name).ToList();

				childEntries.ForEach(childEntry => _entryTree.Add(childEntry, entry));

				List<FileSystemTreeNode> childTreeNodes = childEntries
					.Where(childEntry => childEntry.IsFolder)
					.Select(childEntry => new FileSystemTreeNode(childEntry.Name) { Entry = childEntry })
					.ToList();

				foreach (FileSystemTreeNode childTreeNode in childTreeNodes)
				{
					_entryNodes.Add(childTreeNode.Entry, childTreeNode);
					childTreeNode.Nodes.Add("dummy");
				}

				FileSystemTreeNode treeNode = _entryNodes[entry];
				treeNode.ChildEntries = childEntries.Cast<IFileSystemEntry>().ToList();
				SetTreeNodes(treeNode, childTreeNodes);
			}
		}

		private string BuildPath(IFileSystemEntry entry)
		{
			string path = entry.Name;
			IFileSystemEntry parentEntry;
			while (_entryTree.TryGetValue(entry, out parentEntry))
			{
				path = Path.Combine(FixPath(parentEntry.Name), path);
				entry = parentEntry;
			}
			return FixPath(path);
		}

		private static string FixPath(string path)
		{
			return (path.EndsWith(":") ? path + @"\" : path);
		}

		private delegate void SetTreeViewNodesDelegate(IEnumerable<FileSystemTreeNode> nodes);

		private void SetTreeViewNodes(IEnumerable<FileSystemTreeNode> nodes)
		{
			if (InvokeRequired)
			{
				Invoke(new SetTreeViewNodesDelegate(SetTreeViewNodes), nodes);
				return;
			}

			treeView.Nodes.Clear();
			treeView.Nodes.AddRange(nodes.Cast<TreeNode>().ToArray());
		}

		private delegate void SetTreeNodesDelegate(TreeNode parentNode, IEnumerable<FileSystemTreeNode> nodes);

		private void SetTreeNodes(TreeNode parentNode, IEnumerable<FileSystemTreeNode> nodes)
		{
			if (InvokeRequired)
			{
				Invoke(new SetTreeNodesDelegate(SetTreeNodes), parentNode, nodes);
				return;
			}

			parentNode.Nodes.Clear();
			parentNode.Nodes.AddRange(nodes.Cast<TreeNode>().ToArray());
		}

		#endregion
	}
}

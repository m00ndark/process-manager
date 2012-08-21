using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
			File = 1,
			Folder = 2
		}

		#endregion

		private const string IMAGE_LIST_KEY_FOLDER = "Folder";

		private bool _machineAvailable;
		private readonly IDictionary<IFileSystemEntry, IFileSystemEntry> _entryTree; // < child, parent >
		private readonly IDictionary<IFileSystemEntry, FileSystemTreeNode> _entryNodes;
		private Queue<string> _pathExpansionQueue;
		private TreeNode _lastAutoExpandedNode;

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
			_pathExpansionQueue = null;
			_lastAutoExpandedNode = null;

			imageList.Images.Add(IMAGE_LIST_KEY_FOLDER, MakeIconImage(Resources.folder_16));
			imageList.Images.Add(FileSystemDriveType.RemovableDisk.ToString(), MakeIconImage(Resources.drive_removable_disk_16));
			imageList.Images.Add(FileSystemDriveType.LocalDisk.ToString(), MakeIconImage(Resources.drive_local_disk_16));
			imageList.Images.Add(FileSystemDriveType.NetworkDrive.ToString(), MakeIconImage(Resources.drive_network_16));
			imageList.Images.Add(FileSystemDriveType.CompactDisc.ToString(), MakeIconImage(Resources.drive_compact_disc_16));
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

			Text += " [" + Machine + "]";
			labelDescription.Text = Description;

			if (FolderMode)
			{
				splitContainer.Panel2Collapsed = true;
				Size = new Size(400, 420);
			}
			else
				Size = new Size(700, 420);

			EnableControls();

			if (!string.IsNullOrEmpty(SelectedPath))
				PreparePathExpansion(SelectedPath);

			Task.Factory.StartNew(DisplayDrives);
		}

		private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			PrepareExpand(e.Node);
		}

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (!FolderMode)
				DisplayFiles((FileSystemTreeNode) e.Node);

			EnableControls();
		}

		private void ListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			EnableControls();
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			if (!EntrySelected())
				return;

			SelectedPath = BuildPath(FolderMode ? ((FileSystemTreeNode) treeView.SelectedNode).Entry : ((FileSystemListViewItem) listView.SelectedItems[0]).Entry);
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

		private void PreparePathExpansion(string path)
		{
			if (!Path.IsPathRooted(path) || path[1] != ':')
				return;

			string[] pathSplit = path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

			if (pathSplit[0].Length != 2 || !pathSplit[0].EndsWith(":"))
				return;

			_pathExpansionQueue = new Queue<string>(pathSplit);
		}

		private void PrepareExpand(TreeNode node)
		{
			if (node.Nodes.Count == 1 && !(node.Nodes[0] is FileSystemTreeNode))
			{
				using (new WaitCursor())
				{
					Worker.WaitFor(() => (node.Nodes.Count != 1 || node.Nodes[0] is FileSystemTreeNode));
				}
			}

			Task.Factory.StartNew(() => RequestFolders(((FileSystemTreeNode) node).ChildEntries));
		}

		private void DisplayDrives()
		{
			if (!_machineAvailable)
				return;

			using (new WaitCursor())
			{
				List<FileSystemDrive> drives = ConnectionStore.Connections[Machine].ServiceHandler.Service
					.GetFileSystemDrives().Select(x => x.FromDTO())
					//.Where(x => treeView.Nodes.Cast<FileSystemTreeNode>().Select(y => y.Entry).All(y => y != x))
					.OrderBy(x => x.Name).ToList();

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

				FileSystemTreeNode treeNode = _entryNodes[entry];

				List<FileSystemEntry> childEntries = ConnectionStore.Connections[Machine].ServiceHandler.Service
					.GetFileSystemEntries(BuildPath(entry)).Select(x => x.FromDTO())
					//.Where(x => treeNode.ChildEntries.All(y => y != x))
					.OrderBy(x => x.Name).ToList();

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

				treeNode.ChildEntries = childEntries.Cast<IFileSystemEntry>().ToList();
				SetTreeNodes(treeNode, childTreeNodes);
			}

			AutoExpandPath();
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

		private void AutoExpandPath()
		{
			if (_pathExpansionQueue != null && _pathExpansionQueue.Count > 0)
			{
				TreeNodeCollection nodes = (_lastAutoExpandedNode == null ? treeView.Nodes : _lastAutoExpandedNode.Nodes);
				string pathPart = _pathExpansionQueue.Dequeue();
				_lastAutoExpandedNode = nodes.Cast<FileSystemTreeNode>().FirstOrDefault(node => node.Entry.Equals(pathPart));
				if (_lastAutoExpandedNode != null)
				{
					if (_pathExpansionQueue.Count == 0)
						SelectTreeNode(_lastAutoExpandedNode);
					else
						ExpandTreeNode(_lastAutoExpandedNode);
				}
				else
				{
					_pathExpansionQueue = null;
				}
			}
		}

		private void DisplayFiles(FileSystemTreeNode node)
		{
			using (new WaitCursor())
			{
				listView.Items.Clear();
				LoadFileIcons(node.ChildEntries);
				listView.Items.AddRange(node.ChildEntries
					.Where(x => x is FileSystemEntry)
					.Cast<FileSystemEntry>()
					.OrderByDescending(x => x.IsFolder)
					.ThenBy(x => x.Name)
					.Select(x => new FileSystemListViewItem(new[] { x.Name, MakeFileSize(x), MakeFileDate(x) })
						{
							ImageKey = (x.IsFolder ? IMAGE_LIST_KEY_FOLDER : Path.GetExtension(x.Name)),
							Entry = x
						})
					.Cast<ListViewItem>().ToArray());
			}
		}

		private static string MakeFileSize(FileSystemEntry entry)
		{
			if (entry.IsFolder) return string.Empty;
			const int KILO_BYTES = 1024;
			return string.Format("{0:#,##0} KB", decimal.Divide((entry.Bytes == 0 ? 0 : (entry.Bytes < KILO_BYTES ? KILO_BYTES : entry.Bytes)), KILO_BYTES));
		}

		private static string MakeFileDate(FileSystemEntry entry)
		{
			return entry.ModifiedDate.ToString("yyyy-MM-dd HH:mm");
		}

		private void LoadFileIcons(IEnumerable<IFileSystemEntry> entries)
		{
			Console.WriteLine(_entryNodes.Count);
			treeView.BeginUpdate();
			foreach (IFileSystemEntry entry in entries.Where(entry => !entry.IsFolder))
			{
				string key = Path.GetExtension(entry.Name);
				if (!imageList.Images.ContainsKey(key))
					imageList.Images.Add(key, MakeIconImage(ShellIcon.GetSmallIcon(entry.Name)));
			}
			treeView.EndUpdate();
		}

		private static Image MakeIconImage(Icon icon)
		{
			Bitmap image = new Bitmap(16, 20);
			using (Graphics graphics = Graphics.FromImage(image))
			{
				graphics.DrawIconUnstretched(icon, new Rectangle(0, 2, 16, 16));
			}
			return image;
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

		private delegate void ExpandTreeNodeDelegate(TreeNode node);

		private void ExpandTreeNode(TreeNode node)
		{
			if (InvokeRequired)
			{
				Invoke(new ExpandTreeNodeDelegate(ExpandTreeNode), node);
				return;
			}

			node.Expand();
		}

		private delegate void SelectTreeNodeDelegate(TreeNode node);

		private void SelectTreeNode(TreeNode node)
		{
			if (InvokeRequired)
			{
				Invoke(new SelectTreeNodeDelegate(SelectTreeNode), node);
				return;
			}

			treeView.SelectedNode = node;
			treeView.Focus();
		}

		#endregion
	}
}

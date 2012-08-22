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

		#region PrioritizedFileSystemEntry class

		private class PrioritizedFileSystemEntry
		{
			public PrioritizedFileSystemEntry(IFileSystemEntry entry, int priority)
			{
				Entry = entry;
				Priority = priority;
			}

			#region Properties

			public IFileSystemEntry Entry { get; set; }
			public int Priority { get; set; }

			#endregion
		}

		#endregion

		#region PrioritizedEntryQueue class

		private class PrioritizedEntryQueue
		{
			private static readonly object _lock = new object();
			private List<PrioritizedFileSystemEntry> _queuedEntries;

			public PrioritizedEntryQueue()
			{
				_queuedEntries = new List<PrioritizedFileSystemEntry>();
			}

			public void Add(IFileSystemEntry entry, int priority = 0)
			{
				if (!entry.IsFolder)
					return;

				lock (_lock)
				{
					_queuedEntries.Add(new PrioritizedFileSystemEntry(entry, priority));
					OrderQueue();
				}
			}
 
			public void Add(IEnumerable<IFileSystemEntry> entries)
			{
				lock (_lock)
				{
					_queuedEntries.AddRange(entries.Where(x => x.IsFolder).Where(x => _queuedEntries.All(y => x != y.Entry)).Select(x => new PrioritizedFileSystemEntry(x, 0)));
					OrderQueue();
				}
			}

			public IFileSystemEntry Take()
			{
				lock (_lock)
				{
					PrioritizedFileSystemEntry prioritizedEntry = _queuedEntries.FirstOrDefault();
					if (prioritizedEntry == null) return null;
					_queuedEntries = _queuedEntries.Skip(1).ToList();
					return prioritizedEntry.Entry;
				}
			}

			public void IncreasePriority(IFileSystemEntry entry, int priorityIncrement = 1)
			{
				lock (_lock)
				{
					PrioritizedFileSystemEntry prioritizedEntry = _queuedEntries.FirstOrDefault(x => x.Entry == entry);
					if (prioritizedEntry == null) return;
					prioritizedEntry.Priority += priorityIncrement;
					OrderQueue();
				}
			}

			private void OrderQueue()
			{
				lock (_lock)
					_queuedEntries = _queuedEntries.OrderByDescending(x => x.Priority).ThenBy(x => x.Entry.Name).ToList();
			}
		}

		#endregion

		private const string IMAGE_LIST_KEY_MACHINE = "Machine";
		private const string IMAGE_LIST_KEY_FOLDER = "Folder";

		private static readonly object _lock = new object();
		private bool _machineAvailable;
		private readonly IDictionary<IFileSystemEntry, IFileSystemEntry> _entryTree; // < child, parent >
		private readonly IDictionary<IFileSystemEntry, FileSystemTreeNode> _entryNodes;
		private readonly PrioritizedEntryQueue _entryQueue;
		private Queue<string> _pathExpansionQueue;
		private WaitCursor _pathExpansionWaitCursor;
		private FileSystemTreeNode _machineNode;
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
			_entryQueue = new PrioritizedEntryQueue();
			_pathExpansionQueue = null;
			_pathExpansionWaitCursor = null;
			_lastAutoExpandedNode = null;
			_machineNode = null;

			imageList.Images.Add(IMAGE_LIST_KEY_MACHINE, MakeIconImage(Resources.machine_16));
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

		public bool FileMode
		{
			get { return ((BrowserMode & Mode.Folder) == 0); }
		}

		#endregion

		#region GUI event handlers

		private void FileSystemBrowserForm_Load(object sender, EventArgs e)
		{
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged += ServiceConnectionHandler_ServiceHandlerConnectionChanged;

			labelDescription.Text = Description;
			_machineNode = new FileSystemTreeNode(Machine.ToString())
				{
					ImageKey = IMAGE_LIST_KEY_MACHINE,
					SelectedImageKey = IMAGE_LIST_KEY_MACHINE
				};
			treeView.Nodes.Add(_machineNode);

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

			Task.Factory.StartNew(() =>
				{
					DisplayDrives();
					ExpandTreeNode(_machineNode);
				});
		}

		private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			e.Cancel = PerformDependentWork(e.Node, () => ExpandTreeNode(e.Node));
		}

		private void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			FileSystemTreeNode node = (FileSystemTreeNode) e.Node;

			if (node.Entry != null)
				Task.Factory.StartNew(() => RequestFolders(node.ChildEntries));
		}

		private void TreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			e.Cancel = PerformDependentWork(e.Node, () => SelectTreeNode(e.Node));
		}

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			listView.Items.Clear();
			EnableControls();

			if (!FolderMode)
				Task.Factory.StartNew(() => DisplayFiles((FileSystemTreeNode) e.Node));
		}

		private void ListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			EnableControls();
		}

		private void ListView_DoubleClick(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count != 1)
				return;

			IFileSystemEntry entry = ((FileSystemListViewItem) listView.SelectedItems[0]).Entry;

			if (entry.IsFolder)
			{
				FileSystemTreeNode treeNode = _entryNodes[entry];
				Task.Factory.StartNew(() =>
					{
						ExpandTreeNode(treeNode.Parent);
						SelectTreeNode(treeNode);
					});
			}
			else
				buttonOK.PerformClick();
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
			return (FolderMode && treeView.SelectedNode != null
				|| FileMode && listView.SelectedItems.Count > 0 && !((FileSystemListViewItem) listView.SelectedItems[0]).Entry.IsFolder
				|| !FolderMode && !FileMode && listView.SelectedItems.Count > 0);
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

			if (_pathExpansionQueue.Count > 0)
				_pathExpansionWaitCursor = new WaitCursor(SetCursor);
		}

		private bool PerformDependentWork(TreeNode node, Action work = null)
		{
			bool cancel = !NodeHasChildren(node);

			if (cancel)
				_entryQueue.IncreasePriority(((FileSystemTreeNode) node).Entry);

			Task.Factory.StartNew(() =>
				{
					if (cancel)
						using (new WaitCursor(SetCursor))
							Worker.WaitFor(() => NodeHasChildren(node));

					if (work != null)
						work();
				});

			return cancel;
		}

		private static bool NodeHasChildren(TreeNode node)
		{
			return (node.Nodes.Count != 1 || node.Nodes[0] is FileSystemTreeNode);
		}

		private void DisplayDrives()
		{
			if (!_machineAvailable)
				return;

			using (new WaitCursor(SetCursor))
			{
				List<FileSystemDrive> drives = ConnectionStore.Connections[Machine].ServiceHandler.Service
					.GetFileSystemDrives().Select(x => x.FromDTO())
					.OrderBy(x => x.Name).ToList();

				List<FileSystemTreeNode> treeNodes = drives.Select(drive =>
					new FileSystemTreeNode(MakeDriveLabel(drive))
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

				_machineNode.ChildEntries = drives.Cast<IFileSystemEntry>().ToList();
				SetTreeViewNodes(_machineNode, treeNodes);
				Task.Factory.StartNew(() => RequestFolders(drives));
			}
		}

		private void RequestFolders(IEnumerable<IFileSystemEntry> entries)
		{
			_entryQueue.Add(entries);

			AutoExpandPath();

			IFileSystemEntry entry;
			while ((entry = _entryQueue.Take()) != null)
			{
				if (!_machineAvailable)
					return;

				FileSystemTreeNode treeNode = _entryNodes[entry];

				if (NodeHasChildren(treeNode))
					continue;

				List<FileSystemEntry> childEntries = ConnectionStore.Connections[Machine].ServiceHandler.Service
					.GetFileSystemEntries(BuildPath(entry)).Select(x => x.FromDTO())
					.OrderBy(x => x.Name).ToList();

				childEntries.ForEach(childEntry => _entryTree.Add(childEntry, entry));

				List<FileSystemTreeNode> childTreeNodes = childEntries
					.Where(childEntry => childEntry.IsFolder)
					.Select(childEntry => new FileSystemTreeNode(childEntry.Name)
						{
							ImageKey = IMAGE_LIST_KEY_FOLDER,
							SelectedImageKey = IMAGE_LIST_KEY_FOLDER,
							Entry = childEntry
						})
					.ToList();

				foreach (FileSystemTreeNode childTreeNode in childTreeNodes)
				{
					_entryNodes.Add(childTreeNode.Entry, childTreeNode);
					childTreeNode.Nodes.Add("dummy");
				}

				treeNode.ChildEntries = childEntries.Cast<IFileSystemEntry>().ToList();
				SetTreeViewNodes(treeNode, childTreeNodes);
			}

			//AutoExpandPath();
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
			if (_pathExpansionQueue == null || _pathExpansionQueue.Count <= 0)
				return;

			lock (_lock)
			{
				if (_pathExpansionQueue == null || _pathExpansionQueue.Count <= 0)
					return;

				string pathPart = _pathExpansionQueue.Dequeue();
				if (!FolderMode && _pathExpansionQueue.Count == 0)
				{
					FileSystemListViewItem targetItem = GetListViewItems().FirstOrDefault(item => item.Entry.Equals(pathPart));
					if (targetItem != null)
						SelectListViewItem(targetItem);
				}
				else
				{
					TreeNodeCollection nodes = (_lastAutoExpandedNode == null ? _machineNode.Nodes : _lastAutoExpandedNode.Nodes);
					_lastAutoExpandedNode = nodes.Cast<FileSystemTreeNode>().FirstOrDefault(node => node.Entry.Equals(pathPart));
					if (_lastAutoExpandedNode != null)
					{
						if (_pathExpansionQueue.Count == (FolderMode ? 0 : 1))
							SelectTreeNode(_lastAutoExpandedNode);
						else
							ExpandTreeNode(_lastAutoExpandedNode);
					}
					else
					{
						_pathExpansionQueue = null;
					}
				}

				if (_pathExpansionQueue == null || _pathExpansionQueue.Count == 0)
					_pathExpansionWaitCursor.Dispose();
			}
		}

		private static string MakeDriveLabel(FileSystemDrive drive)
		{
			return (!string.IsNullOrEmpty(drive.Label) ? drive.Label : drive.GetTypeDescription()) + " (" + drive.Name + ")";
		}

		private void DisplayFiles(FileSystemTreeNode node)
		{
			using (new WaitCursor(SetCursor))
			{
				if (node == _machineNode)
				{
					SetListViewItems(node.ChildEntries
						.Where(x => x is FileSystemDrive)
						.Cast<FileSystemDrive>()
						.OrderBy(x => x.Name)
						.Select(x => new FileSystemListViewItem(new[] { MakeDriveLabel(x) })
							{
								ImageKey = x.Type.ToString(),
								Entry = x
							}));
				}
				else
				{
					LoadFileIcons(node.ChildEntries);
					SetListViewItems(node.ChildEntries
						.Where(x => x is FileSystemEntry)
						.Cast<FileSystemEntry>()
						.OrderByDescending(x => x.IsFolder)
						.ThenBy(x => x.Name)
						.Select(x => new FileSystemListViewItem(new[] { x.Name, MakeFileSize(x), MakeFileDate(x) })
							{
								ImageKey = (x.IsFolder ? IMAGE_LIST_KEY_FOLDER : Path.GetExtension(x.Name)),
								Entry = x
							}));
				}
			}

			AutoExpandPath();
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
			IDictionary<string, Image> images = new Dictionary<string, Image>();
			foreach (IFileSystemEntry entry in entries.Where(entry => !entry.IsFolder))
			{
				string key = Path.GetExtension(entry.Name);
				if (key != null && !imageList.Images.ContainsKey(key) && !images.ContainsKey(key))
					images.Add(key, MakeIconImage(ShellIcon.GetSmallIcon(entry.Name)));
			}
			AddToImageList(images);
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

		private delegate void AddToImageListDelegate(IDictionary<string, Image> images);

		private void AddToImageList(IDictionary<string, Image> images)
		{
			if (treeView.InvokeRequired)
			{
				treeView.Invoke(new AddToImageListDelegate(AddToImageList), images);
				return;
			}

			treeView.BeginUpdate();
			foreach (string key in images.Keys)
			{
				imageList.Images.Add(key, images[key]);
			}
			treeView.EndUpdate();
		}

		private delegate void SetTreeViewNodesDelegate(TreeNode parentNode, IEnumerable<FileSystemTreeNode> nodes);

		private void SetTreeViewNodes(TreeNode parentNode, IEnumerable<FileSystemTreeNode> nodes)
		{
			if (treeView.InvokeRequired)
			{
				treeView.Invoke(new SetTreeViewNodesDelegate(SetTreeViewNodes), parentNode, nodes);
				return;
			}

			parentNode.Nodes.Clear();
			parentNode.Nodes.AddRange(nodes.Cast<TreeNode>().ToArray());
		}

		private delegate void ExpandTreeNodeDelegate(TreeNode node);

		private void ExpandTreeNode(TreeNode node)
		{
			if (treeView.InvokeRequired)
			{
				treeView.Invoke(new ExpandTreeNodeDelegate(ExpandTreeNode), node);
				return;
			}

			node.EnsureVisible();
			node.Expand();
		}

		private delegate void SelectTreeNodeDelegate(TreeNode node);

		private void SelectTreeNode(TreeNode node)
		{
			if (treeView.InvokeRequired)
			{
				treeView.Invoke(new SelectTreeNodeDelegate(SelectTreeNode), node);
				return;
			}

			node.EnsureVisible();
			treeView.SelectedNode = node;
			treeView.Focus();
		}

		private delegate List<FileSystemListViewItem> GetListViewItemsDelegate();

		private List<FileSystemListViewItem> GetListViewItems()
		{
			if (listView.InvokeRequired)
				return (List<FileSystemListViewItem>) listView.Invoke(new GetListViewItemsDelegate(GetListViewItems));

			return listView.Items.Cast<FileSystemListViewItem>().ToList();
		}

		private delegate void SetListViewItemsDelegate(IEnumerable<FileSystemListViewItem> items);

		private void SetListViewItems(IEnumerable<FileSystemListViewItem> items)
		{
			if (listView.InvokeRequired)
			{
				listView.Invoke(new SetListViewItemsDelegate(SetListViewItems), items);
				return;
			}

			listView.Items.Clear();
			listView.Items.AddRange(items.Cast<ListViewItem>().ToArray());
		}

		private delegate void SelectListViewItemDelegate(ListViewItem item);

		private void SelectListViewItem(ListViewItem item)
		{
			if (listView.InvokeRequired)
			{
				listView.Invoke(new SelectListViewItemDelegate(SelectListViewItem), item);
				return;
			}

			item.EnsureVisible();
			item.Selected = true;
			listView.Focus();
		}

		private delegate void SetCursorDelegate(Cursor cursor);

		private void SetCursor(Cursor cursor)
		{
			if (InvokeRequired)
			{
				Invoke(new SetCursorDelegate(SetCursor), cursor);
				return;
			}

			treeView.Cursor = cursor;
			listView.Cursor = cursor;
		}

		#endregion
	}
}

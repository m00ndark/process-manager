using System.Collections.Generic;
using System.Windows.Forms;
using ProcessManager.DataObjects;

namespace ProcessManagerUI.Support
{
	public class FileSystemTreeNode : TreeNode
	{
		public FileSystemTreeNode() : base()
		{
			ChildEntries = new List<IFileSystemEntry>();
		}

		public FileSystemTreeNode(string text) : base(text)
		{
			ChildEntries = new List<IFileSystemEntry>();
		}

		#region Properties

		public IFileSystemEntry Entry { get; set; }
		public List<IFileSystemEntry> ChildEntries { get; set; }

		#endregion
	}
}

using System.Windows.Forms;
using ProcessManager.DataObjects;

namespace ProcessManagerUI.Support
{
	public class FileSystemListViewItem : ListViewItem
	{
		public FileSystemListViewItem() : base() {}
		public FileSystemListViewItem(string[] items) : base(items) {}
		public FileSystemListViewItem(string[] items, string imageKey) : base(items, imageKey) {}

		#region Properties

		public IFileSystemEntry Entry { get; set; }

		#endregion
	}
}

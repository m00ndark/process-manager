using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProcessManagerUI.Utilities
{
	public class Picker
	{
		public static void ShowMenu<T>(Control control, IEnumerable<T> items, Action<T> pickHandler)
		{
			ShowMenu(control.Parent.PointToScreen(new Point(control.Location.X, control.Location.Y + control.Size.Height)), items, pickHandler);
		}

		public static void ShowMenu<T>(Point position, IEnumerable<T> items, Action<T> pickHandler)
		{
			if (items == null || !items.Any()) return;

			ContextMenuStrip contextMenu = new ContextMenuStrip();
			ToolStripDropDownClosedEventHandler contextMenuClosedEventHandler = null;
			ToolStripItemClickedEventHandler menuItemClickedEventHandler = null;
			contextMenuClosedEventHandler = (sender, e) =>
				{
					contextMenu.Closed -= contextMenuClosedEventHandler;
					contextMenu.ItemClicked -= menuItemClickedEventHandler;
				};
			menuItemClickedEventHandler = (sender, e) =>
				{
					contextMenu.Closed -= contextMenuClosedEventHandler;
					contextMenu.ItemClicked -= menuItemClickedEventHandler;
					contextMenu.Close();
					pickHandler((T) e.ClickedItem.Tag);
				};
			contextMenu.Closed += contextMenuClosedEventHandler;
			contextMenu.ItemClicked += menuItemClickedEventHandler;
			items.OrderBy(item => item.ToString()).ToList().ForEach(item => contextMenu.Items.Add(new ToolStripMenuItem(item.ToString()) { Tag = item }));
			contextMenu.Show(position);
		}
	}
}

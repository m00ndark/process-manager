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
			ContextMenuStrip contextMenu = new ContextMenuStrip();
			EventHandler menuItemClickEventHandler = null;
			menuItemClickEventHandler = (sender, e) =>
			{
				contextMenu.Items.Cast<ToolStripItem>().ToList().ForEach(item => item.Click -= menuItemClickEventHandler);
				pickHandler((T) ((ToolStripMenuItem) sender).Tag);
			};
			foreach (T item in items)
			{
				ToolStripMenuItem menuItem = new ToolStripMenuItem(item.ToString()) { Tag = item };
				menuItem.Click += menuItemClickEventHandler;
				contextMenu.Items.Add(menuItem);
			}
			contextMenu.Show(position);
		}
	}
}

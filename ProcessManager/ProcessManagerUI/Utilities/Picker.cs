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
			if (items == null || !items.Any())
				return;

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

		public static void ShowMenu<T1, T2>(Control control, IEnumerable<T1> rootItems, IEnumerable<T2> items, Action<T1, T2> pickHandler)
		{
			ShowMenu(control.Parent.PointToScreen(new Point(control.Location.X, control.Location.Y + control.Size.Height)), rootItems, items, pickHandler);
		}

		public static void ShowMenu<T1, T2>(Point position, IEnumerable<T1> rootItems, IEnumerable<T2> items, Action<T1, T2> pickHandler)
		{
			if (rootItems == null || !rootItems.Any()) return;
			if (items == null || !items.Any()) return;

			ContextMenuStrip contextMenu = new ContextMenuStrip();
			foreach (T1 rootItem in rootItems)
			{
				ToolStripMenuItem rootMenuItem = new ToolStripMenuItem(rootItem.ToString()) { Tag = rootItem };
				contextMenu.Items.Add(rootMenuItem);
				items.OrderBy(item => item.ToString()).ToList().ForEach(item => rootMenuItem.DropDownItems.Add(new ToolStripMenuItem(item.ToString()) { Tag = new Tuple<T1, T2>(rootItem, item) }));
			}

			ToolStripDropDownClosedEventHandler contextMenuClosedEventHandler = null;
			ToolStripItemClickedEventHandler menuItemClickedEventHandler = null;
			contextMenuClosedEventHandler = (sender, e) =>
				{
					contextMenu.Closed -= contextMenuClosedEventHandler;
					contextMenu.Items.Cast<ToolStripMenuItem>().ToList().ForEach(x => x.DropDownItemClicked -= menuItemClickedEventHandler);
				};
			menuItemClickedEventHandler = (sender, e) =>
				{
					contextMenu.Closed -= contextMenuClosedEventHandler;
					contextMenu.Items.Cast<ToolStripMenuItem>().ToList().ForEach(x => x.DropDownItemClicked -= menuItemClickedEventHandler);
					contextMenu.Close();
					Tuple<T1, T2> tag = (Tuple<T1, T2>) e.ClickedItem.Tag;
					pickHandler(tag.Item1, tag.Item2);
				};
			contextMenu.Closed += contextMenuClosedEventHandler;
			contextMenu.Items.Cast<ToolStripMenuItem>().ToList().ForEach(x => x.DropDownItemClicked += menuItemClickedEventHandler);
			contextMenu.Show(position);
		}

		public static void ShowMultiSelectMenu<T>(Control control, IEnumerable<Tuple<T, bool>> items, Action<List<T>> pickHandler)
		{
			ShowMultiSelectMenu(control.Parent.PointToScreen(new Point(control.Location.X, control.Location.Y + control.Size.Height)), items, pickHandler);
		}

		public static void ShowMultiSelectMenu<T>(Point position, IEnumerable<Tuple<T, bool>> items, Action<List<T>> pickHandler)
		{
			if (items == null || !items.Any())
				return;

			ContextMenuStrip contextMenu = new ContextMenuStrip();
			ToolStripDropDownClosedEventHandler contextMenuClosedEventHandler = null;
			ToolStripDropDownClosingEventHandler contextMenuClosingEventHandler = null;
			ToolStripItemClickedEventHandler menuItemClickedEventHandler = null;
			contextMenuClosedEventHandler = (sender, e) =>
				{
					contextMenu.Closed -= contextMenuClosedEventHandler;
					contextMenu.Closing -= contextMenuClosingEventHandler;
					contextMenu.ItemClicked -= menuItemClickedEventHandler;
				};
			contextMenuClosingEventHandler = (sender, e) =>
				{
					if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
					{
						e.Cancel = true;
						((ToolStripDropDownMenu) sender).Invalidate();
					}
					else
					{
						List<T> selectedItems = ((ToolStripDropDownMenu) sender).Items.Cast<ToolStripMenuItem>().Where(x => x.Checked).Select(x => (T) x.Tag).ToList();
						pickHandler(selectedItems);
					}
				};
			menuItemClickedEventHandler = (sender, e) =>
				{
					ToolStripMenuItem clickedItem = (ToolStripMenuItem) e.ClickedItem;
					clickedItem.Checked = !clickedItem.Checked;
				};
			contextMenu.Closed += contextMenuClosedEventHandler;
			contextMenu.Closing += contextMenuClosingEventHandler;
			contextMenu.ItemClicked += menuItemClickedEventHandler;
			items.OrderBy(item => item.Item1.ToString()).ToList().ForEach(item =>
				contextMenu.Items.Add(new ToolStripMenuItem(item.Item1.ToString())
					{
						Tag = item.Item1,
						Checked = item.Item2
					}));
			contextMenu.Show(position);
		}
	}
}

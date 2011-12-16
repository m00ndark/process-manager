using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProcessManagerUI.Forms
{
	public partial class ConfigurationForm : Form
	{
		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		private static extern int SetWindowTheme(IntPtr hWnd, string appName, string partList);

		public ConfigurationForm()
		{
			InitializeComponent();
			SetWindowTheme(treeView1.Handle, "explorer", null);
		}

		private void ConfigurationForm_Load(object sender, EventArgs e)
		{
			treeView1.ExpandAll();
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(
				new Pen(Color.FromArgb(130, 135, 144)),
				e.ClipRectangle.Left,
				e.ClipRectangle.Top,
				e.ClipRectangle.Width - 1,
				e.ClipRectangle.Height - 1);
		}
	}
}

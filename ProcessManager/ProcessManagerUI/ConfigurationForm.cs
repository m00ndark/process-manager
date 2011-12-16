using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ProcessManagerUI
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

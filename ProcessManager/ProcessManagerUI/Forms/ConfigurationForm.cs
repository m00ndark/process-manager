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
			SetWindowTheme(treeViewConfiguration.Handle, "explorer", null);
		}

		private void ConfigurationForm_Load(object sender, EventArgs e)
		{
			treeViewConfiguration.ExpandAll();
		}
	}
}

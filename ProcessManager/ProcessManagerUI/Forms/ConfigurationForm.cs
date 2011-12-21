using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProcessManagerUI.Forms
{
	public partial class ConfigurationForm : Form
	{

		public ConfigurationForm()
		{
			InitializeComponent();
		}

		private void ConfigurationForm_Load(object sender, EventArgs e)
		{
			treeViewConfiguration.ExpandAll();
		}
	}
}

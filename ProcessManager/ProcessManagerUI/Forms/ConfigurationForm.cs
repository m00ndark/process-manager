using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProcessManagerUI.Forms
{
	public partial class ConfigurationForm : Form
	{
		private Panel _currentPanel;

		public ConfigurationForm()
		{
			InitializeComponent();
			_currentPanel = null;
		}

		private void ConfigurationForm_Load(object sender, EventArgs e)
		{
			TreeNode setupNode = new TreeNode("Setup");
			TreeNode groupsNode = new TreeNode("Groups") { Tag = panelGroups };
			TreeNode applicationsNode = new TreeNode("Applications") { Tag = panelApplications };
			TreeNode pluginsNode = new TreeNode("Plugins") { Tag = panelPlugins };
			treeViewConfiguration.Nodes.Add(setupNode);
			treeViewConfiguration.Nodes.Add(pluginsNode);
			setupNode.Nodes.Add(groupsNode);
			setupNode.Nodes.Add(applicationsNode);
			treeViewConfiguration.ExpandAll();
			treeViewConfiguration.SelectedNode = groupsNode;
		}

		private void treeViewConfiguration_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (_currentPanel != null)
				_currentPanel.Visible = false;

			_currentPanel = (Panel) e.Node.Tag;

			if (_currentPanel != null)
				_currentPanel.Visible = true;
		}
	}
}

using System;
using System.Drawing;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManagerUI.Support;
using ProcessManagerUI.Utilities;

namespace ProcessManagerUI.Forms
{
	public partial class ConfigurationForm : Form
	{
		private Panel _currentPanel;
		private bool _configurationChanged;

		public ConfigurationForm()
		{
			InitializeComponent();
			_currentPanel = null;
			_configurationChanged = true;
		}

		#region GUI event handlers

		private void ConfigurationForm_Load(object sender, EventArgs e)
		{
			Settings.Client.Load();
			Machine localhost = new Machine("localhost");
			if (!Settings.Client.Machines.Contains(localhost))
			{
				Settings.Client.Machines.Add(localhost);
				Settings.Client.Save(ClientSettingsType.Machines);
			}
			foreach (Machine machine in Settings.Client.Machines)
			{
				int index = comboBoxMachines.Items.Add(new ComboBoxItem(machine.HostName, machine));
				if (machine.HostName == Settings.Client.CFG_SelectedHostName)
					comboBoxMachines.SelectedIndex = index;
			}
			if (comboBoxMachines.SelectedIndex < 0)
				comboBoxMachines.SelectedIndex = 0;

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

		private void comboBoxMachines_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxMachines.SelectedIndex > -1)
			{
				Settings.Client.CFG_SelectedHostName = ((Machine) ((ComboBoxItem) comboBoxMachines.SelectedItem).Tag).HostName;
				// todo: retrieve and update with selected machine's configuration
			} 
		}

		private void treeViewConfiguration_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (_currentPanel != null)
				_currentPanel.Visible = false;

			_currentPanel = (Panel) e.Node.Tag;

			if (_currentPanel != null)
				_currentPanel.Visible = true;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			SaveConfiguration();
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			if (_configurationChanged)
				if (Messenger.ShowWarningQuestion("Configuration has been changed", "Would you like to discard any changes?") == DialogResult.No)
					return;
			Close();
		}

		private void buttonApply_Click(object sender, EventArgs e)
		{
			SaveConfiguration();
		}

		#endregion

		#region Helpers

		private void SaveConfiguration()
		{
			if (_configurationChanged)
			{
				Settings.Client.Save();
				// todo: save server side config
			}
		}

		#endregion
	}
}

using System;
using System.Drawing;
using System.Linq;
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
		private bool _hasUnsavedConfiguration;

		public ConfigurationForm()
		{
			InitializeComponent();
			_currentPanel = null;
			_hasUnsavedConfiguration = false;
		}

		#region GUI event handlers

		private void ConfigurationForm_Load(object sender, EventArgs e)
		{
			Settings.Client.Load();
			PopulateMachinesComboBox();
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

		private void ComboBoxMachines_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxMachines.SelectedIndex > -1)
			{
				Settings.Client.CFG_SelectedHostName = ((Machine) comboBoxMachines.SelectedItem).HostName;
				// todo: retrieve and update with selected machine's configuration
			}
		}

		private void ButtonMachines_Click(object sender, EventArgs e)
		{
			MachinesForm machinesForm = new MachinesForm((Machine) comboBoxMachines.SelectedItem);
			machinesForm.ShowDialog(this);
			if (machinesForm.MachinesChanged)
			{
				PopulateMachinesComboBox(false);
			}
		}

		private void TreeViewConfiguration_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (_currentPanel != null)
				_currentPanel.Visible = false;

			_currentPanel = (Panel) e.Node.Tag;

			if (_currentPanel != null)
				_currentPanel.Visible = true;
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			SaveConfiguration();
			Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			if (_hasUnsavedConfiguration)
				if (Messenger.ShowWarningQuestion("Configuration has been changed", "Would you like to discard any changes?") == DialogResult.No)
					return;
			Close();
		}

		private void ButtonApply_Click(object sender, EventArgs e)
		{
			SaveConfiguration();
		}

		#endregion

		#region Helpers

		private void PopulateMachinesComboBox(bool selectDefault = true)
		{
			Machine localhost = new Machine(Settings.Constants.LOCALHOST);
			if (!Settings.Client.Machines.Contains(localhost))
			{
				Settings.Client.Machines.Insert(0, localhost);
				Settings.Client.Save(ClientSettingsType.Machines);
			}

			Machine selectedMachine = new Machine(Settings.Client.CFG_SelectedHostName);
			if (!Settings.Client.Machines.Contains(selectedMachine))
				selectedMachine = localhost;

			comboBoxMachines.Items.Clear();
			foreach (Machine machine in Settings.Client.Machines)
			{
				int index = comboBoxMachines.Items.Add(machine);
				if (machine == selectedMachine)
					comboBoxMachines.SelectedIndex = index;
			}

			if (selectDefault && comboBoxMachines.SelectedIndex < 0)
				comboBoxMachines.SelectedIndex = 0;
		}

		private void SaveConfiguration()
		{
			if (_hasUnsavedConfiguration)
			{
				Settings.Client.Save();
				// todo: save server side config
				_hasUnsavedConfiguration = false;
			}
		}

		#endregion
	}
}

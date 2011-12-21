namespace ProcessManagerUI.Forms
{
	partial class ConfigurationForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Groups");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Applications");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Setup", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Plugins");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
			this.comboBoxMachines = new System.Windows.Forms.ComboBox();
			this.buttonMachines = new System.Windows.Forms.Button();
			this.panelGroups = new ProcessManagerUI.Controls.BackgroundPanel();
			this.buttonCopyGroupApplications = new System.Windows.Forms.Button();
			this.buttonRemoveGroupApplication = new System.Windows.Forms.Button();
			this.buttonAddGroupApplication = new System.Windows.Forms.Button();
			this.listViewGroupApplications = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderApplicationMappings = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.labelGroupApplications = new System.Windows.Forms.Label();
			this.buttonBrowseGroupPath = new System.Windows.Forms.Button();
			this.labeledDividerGroup = new ProcessManagerUI.Controls.LabeledDivider();
			this.labeledDividerGroupApplicationMappings = new ProcessManagerUI.Controls.LabeledDivider();
			this.textBoxGroupPath = new System.Windows.Forms.TextBox();
			this.labelGroupPath = new System.Windows.Forms.Label();
			this.textBoxGroupName = new System.Windows.Forms.TextBox();
			this.labelGroupName = new System.Windows.Forms.Label();
			this.buttonRemoveGroup = new System.Windows.Forms.Button();
			this.buttonAddGroup = new System.Windows.Forms.Button();
			this.listViewGroups = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderGroups = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonApply = new System.Windows.Forms.Button();
			this.panelPlugins = new ProcessManagerUI.Controls.BackgroundPanel();
			this.checkBoxPluginEnabled = new System.Windows.Forms.CheckBox();
			this.labelPluginDescriptionValue = new System.Windows.Forms.Label();
			this.labelPluginNameValue = new System.Windows.Forms.Label();
			this.buttonConfigurePlugin = new System.Windows.Forms.Button();
			this.labeledDividerPlugin = new ProcessManagerUI.Controls.LabeledDivider();
			this.labelPluginDescription = new System.Windows.Forms.Label();
			this.labelPluginName = new System.Windows.Forms.Label();
			this.listViewPlugins = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderPlugins = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panelApplications = new ProcessManagerUI.Controls.BackgroundPanel();
			this.textBoxApplicationArguments = new System.Windows.Forms.TextBox();
			this.labelApplicationArguments = new System.Windows.Forms.Label();
			this.buttonBrowseApplicationRelativePath = new System.Windows.Forms.Button();
			this.labeledDividerApplication = new ProcessManagerUI.Controls.LabeledDivider();
			this.textBoxApplicationRelativePath = new System.Windows.Forms.TextBox();
			this.labelApplicationRelativePath = new System.Windows.Forms.Label();
			this.textBoxApplicationName = new System.Windows.Forms.TextBox();
			this.labelApplicationName = new System.Windows.Forms.Label();
			this.buttonRemoveApplication = new System.Windows.Forms.Button();
			this.buttonAddApplication = new System.Windows.Forms.Button();
			this.listViewApplications = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderApplications = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.treeViewConfiguration = new ProcessManagerUI.Controls.TreeView();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.panelGroups.SuspendLayout();
			this.panelPlugins.SuspendLayout();
			this.panelApplications.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxMachines
			// 
			this.comboBoxMachines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxMachines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMachines.FormattingEnabled = true;
			this.comboBoxMachines.Location = new System.Drawing.Point(12, 12);
			this.comboBoxMachines.Name = "comboBoxMachines";
			this.comboBoxMachines.Size = new System.Drawing.Size(653, 21);
			this.comboBoxMachines.TabIndex = 0;
			// 
			// buttonMachines
			// 
			this.buttonMachines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonMachines.Location = new System.Drawing.Point(671, 11);
			this.buttonMachines.Name = "buttonMachines";
			this.buttonMachines.Size = new System.Drawing.Size(92, 23);
			this.buttonMachines.TabIndex = 1;
			this.buttonMachines.Text = "Machines...";
			this.buttonMachines.UseVisualStyleBackColor = true;
			// 
			// panelGroups
			// 
			this.panelGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelGroups.BackColor = System.Drawing.SystemColors.Window;
			this.panelGroups.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
			this.panelGroups.Controls.Add(this.buttonCopyGroupApplications);
			this.panelGroups.Controls.Add(this.buttonRemoveGroupApplication);
			this.panelGroups.Controls.Add(this.buttonAddGroupApplication);
			this.panelGroups.Controls.Add(this.listViewGroupApplications);
			this.panelGroups.Controls.Add(this.labelGroupApplications);
			this.panelGroups.Controls.Add(this.buttonBrowseGroupPath);
			this.panelGroups.Controls.Add(this.labeledDividerGroup);
			this.panelGroups.Controls.Add(this.labeledDividerGroupApplicationMappings);
			this.panelGroups.Controls.Add(this.textBoxGroupPath);
			this.panelGroups.Controls.Add(this.labelGroupPath);
			this.panelGroups.Controls.Add(this.textBoxGroupName);
			this.panelGroups.Controls.Add(this.labelGroupName);
			this.panelGroups.Controls.Add(this.buttonRemoveGroup);
			this.panelGroups.Controls.Add(this.buttonAddGroup);
			this.panelGroups.Controls.Add(this.listViewGroups);
			this.panelGroups.Location = new System.Drawing.Point(174, 39);
			this.panelGroups.Name = "panelGroups";
			this.panelGroups.Padding = new System.Windows.Forms.Padding(5);
			this.panelGroups.Size = new System.Drawing.Size(589, 361);
			this.panelGroups.TabIndex = 3;
			// 
			// buttonCopyGroupApplications
			// 
			this.buttonCopyGroupApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCopyGroupApplications.Image = global::ProcessManagerUI.Properties.Resources.copy_16;
			this.buttonCopyGroupApplications.Location = new System.Drawing.Point(537, 186);
			this.buttonCopyGroupApplications.Name = "buttonCopyGroupApplications";
			this.buttonCopyGroupApplications.Size = new System.Drawing.Size(38, 24);
			this.buttonCopyGroupApplications.TabIndex = 16;
			this.buttonCopyGroupApplications.UseVisualStyleBackColor = true;
			// 
			// buttonRemoveGroupApplication
			// 
			this.buttonRemoveGroupApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRemoveGroupApplication.Image = global::ProcessManagerUI.Properties.Resources.remove_16;
			this.buttonRemoveGroupApplication.Location = new System.Drawing.Point(537, 156);
			this.buttonRemoveGroupApplication.Name = "buttonRemoveGroupApplication";
			this.buttonRemoveGroupApplication.Size = new System.Drawing.Size(38, 24);
			this.buttonRemoveGroupApplication.TabIndex = 15;
			this.buttonRemoveGroupApplication.UseVisualStyleBackColor = true;
			// 
			// buttonAddGroupApplication
			// 
			this.buttonAddGroupApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddGroupApplication.Image = global::ProcessManagerUI.Properties.Resources.add_16;
			this.buttonAddGroupApplication.Location = new System.Drawing.Point(537, 126);
			this.buttonAddGroupApplication.Name = "buttonAddGroupApplication";
			this.buttonAddGroupApplication.Size = new System.Drawing.Size(38, 24);
			this.buttonAddGroupApplication.TabIndex = 14;
			this.buttonAddGroupApplication.UseVisualStyleBackColor = true;
			// 
			// listViewGroupApplications
			// 
			this.listViewGroupApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewGroupApplications.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderApplicationMappings});
			this.listViewGroupApplications.FullRowSelect = true;
			this.listViewGroupApplications.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewGroupApplications.HideSelection = false;
			this.listViewGroupApplications.Location = new System.Drawing.Point(244, 126);
			this.listViewGroupApplications.MultiSelect = false;
			this.listViewGroupApplications.Name = "listViewGroupApplications";
			this.listViewGroupApplications.Size = new System.Drawing.Size(287, 225);
			this.listViewGroupApplications.TabIndex = 13;
			this.listViewGroupApplications.UseCompatibleStateImageBehavior = false;
			this.listViewGroupApplications.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderApplicationMappings
			// 
			this.columnHeaderApplicationMappings.Text = "ApplicationMappings";
			this.columnHeaderApplicationMappings.Width = 268;
			// 
			// labelGroupApplications
			// 
			this.labelGroupApplications.AutoSize = true;
			this.labelGroupApplications.Location = new System.Drawing.Point(164, 129);
			this.labelGroupApplications.Name = "labelGroupApplications";
			this.labelGroupApplications.Size = new System.Drawing.Size(74, 13);
			this.labelGroupApplications.TabIndex = 12;
			this.labelGroupApplications.Text = "Applications:";
			// 
			// buttonBrowseGroupPath
			// 
			this.buttonBrowseGroupPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseGroupPath.Location = new System.Drawing.Point(537, 58);
			this.buttonBrowseGroupPath.Name = "buttonBrowseGroupPath";
			this.buttonBrowseGroupPath.Size = new System.Drawing.Size(38, 23);
			this.buttonBrowseGroupPath.TabIndex = 11;
			this.buttonBrowseGroupPath.Text = "...";
			this.buttonBrowseGroupPath.UseVisualStyleBackColor = true;
			// 
			// labeledDividerGroup
			// 
			this.labeledDividerGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerGroup.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(170)))));
			this.labeledDividerGroup.Location = new System.Drawing.Point(144, 8);
			this.labeledDividerGroup.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerGroup.Name = "labeledDividerGroup";
			this.labeledDividerGroup.Size = new System.Drawing.Size(437, 15);
			this.labeledDividerGroup.TabIndex = 10;
			this.labeledDividerGroup.Text = "Group";
			// 
			// labeledDividerGroupApplicationMappings
			// 
			this.labeledDividerGroupApplicationMappings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerGroupApplicationMappings.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerGroupApplicationMappings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(170)))));
			this.labeledDividerGroupApplicationMappings.Location = new System.Drawing.Point(147, 103);
			this.labeledDividerGroupApplicationMappings.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerGroupApplicationMappings.Name = "labeledDividerGroupApplicationMappings";
			this.labeledDividerGroupApplicationMappings.Size = new System.Drawing.Size(434, 15);
			this.labeledDividerGroupApplicationMappings.TabIndex = 9;
			this.labeledDividerGroupApplicationMappings.Text = "Mappings";
			// 
			// textBoxGroupPath
			// 
			this.textBoxGroupPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxGroupPath.Location = new System.Drawing.Point(244, 59);
			this.textBoxGroupPath.Name = "textBoxGroupPath";
			this.textBoxGroupPath.Size = new System.Drawing.Size(287, 22);
			this.textBoxGroupPath.TabIndex = 8;
			// 
			// labelGroupPath
			// 
			this.labelGroupPath.AutoSize = true;
			this.labelGroupPath.Location = new System.Drawing.Point(164, 62);
			this.labelGroupPath.Name = "labelGroupPath";
			this.labelGroupPath.Size = new System.Drawing.Size(33, 13);
			this.labelGroupPath.TabIndex = 7;
			this.labelGroupPath.Text = "Path:";
			// 
			// textBoxGroupName
			// 
			this.textBoxGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxGroupName.Location = new System.Drawing.Point(244, 31);
			this.textBoxGroupName.Name = "textBoxGroupName";
			this.textBoxGroupName.Size = new System.Drawing.Size(287, 22);
			this.textBoxGroupName.TabIndex = 6;
			// 
			// labelGroupName
			// 
			this.labelGroupName.AutoSize = true;
			this.labelGroupName.Location = new System.Drawing.Point(164, 34);
			this.labelGroupName.Name = "labelGroupName";
			this.labelGroupName.Size = new System.Drawing.Size(39, 13);
			this.labelGroupName.TabIndex = 5;
			this.labelGroupName.Text = "Name:";
			// 
			// buttonRemoveGroup
			// 
			this.buttonRemoveGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemoveGroup.Location = new System.Drawing.Point(71, 330);
			this.buttonRemoveGroup.Name = "buttonRemoveGroup";
			this.buttonRemoveGroup.Size = new System.Drawing.Size(58, 23);
			this.buttonRemoveGroup.TabIndex = 2;
			this.buttonRemoveGroup.Text = "Remove";
			this.buttonRemoveGroup.UseVisualStyleBackColor = true;
			// 
			// buttonAddGroup
			// 
			this.buttonAddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddGroup.Location = new System.Drawing.Point(7, 330);
			this.buttonAddGroup.Name = "buttonAddGroup";
			this.buttonAddGroup.Size = new System.Drawing.Size(58, 23);
			this.buttonAddGroup.TabIndex = 1;
			this.buttonAddGroup.Text = "Add";
			this.buttonAddGroup.UseVisualStyleBackColor = true;
			// 
			// listViewGroups
			// 
			this.listViewGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderGroups});
			this.listViewGroups.FullRowSelect = true;
			this.listViewGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewGroups.HideSelection = false;
			this.listViewGroups.Location = new System.Drawing.Point(8, 8);
			this.listViewGroups.MultiSelect = false;
			this.listViewGroups.Name = "listViewGroups";
			this.listViewGroups.Size = new System.Drawing.Size(120, 316);
			this.listViewGroups.TabIndex = 0;
			this.listViewGroups.UseCompatibleStateImageBehavior = false;
			this.listViewGroups.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderGroups
			// 
			this.columnHeaderGroups.Text = "Groups";
			this.columnHeaderGroups.Width = 100;
			// 
			// buttonApply
			// 
			this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonApply.Location = new System.Drawing.Point(671, 406);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(92, 23);
			this.buttonApply.TabIndex = 17;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			// 
			// panelPlugins
			// 
			this.panelPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPlugins.BackColor = System.Drawing.SystemColors.Window;
			this.panelPlugins.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
			this.panelPlugins.Controls.Add(this.checkBoxPluginEnabled);
			this.panelPlugins.Controls.Add(this.labelPluginDescriptionValue);
			this.panelPlugins.Controls.Add(this.labelPluginNameValue);
			this.panelPlugins.Controls.Add(this.buttonConfigurePlugin);
			this.panelPlugins.Controls.Add(this.labeledDividerPlugin);
			this.panelPlugins.Controls.Add(this.labelPluginDescription);
			this.panelPlugins.Controls.Add(this.labelPluginName);
			this.panelPlugins.Controls.Add(this.listViewPlugins);
			this.panelPlugins.Location = new System.Drawing.Point(174, 39);
			this.panelPlugins.Name = "panelPlugins";
			this.panelPlugins.Padding = new System.Windows.Forms.Padding(5);
			this.panelPlugins.Size = new System.Drawing.Size(589, 361);
			this.panelPlugins.TabIndex = 18;
			// 
			// checkBoxPluginEnabled
			// 
			this.checkBoxPluginEnabled.AutoSize = true;
			this.checkBoxPluginEnabled.Location = new System.Drawing.Point(147, 334);
			this.checkBoxPluginEnabled.Name = "checkBoxPluginEnabled";
			this.checkBoxPluginEnabled.Size = new System.Drawing.Size(68, 17);
			this.checkBoxPluginEnabled.TabIndex = 14;
			this.checkBoxPluginEnabled.Text = "Enabled";
			this.checkBoxPluginEnabled.UseVisualStyleBackColor = true;
			// 
			// labelPluginDescriptionValue
			// 
			this.labelPluginDescriptionValue.Location = new System.Drawing.Point(239, 62);
			this.labelPluginDescriptionValue.Name = "labelPluginDescriptionValue";
			this.labelPluginDescriptionValue.Size = new System.Drawing.Size(336, 262);
			this.labelPluginDescriptionValue.TabIndex = 13;
			this.labelPluginDescriptionValue.Text = "[description]";
			// 
			// labelPluginNameValue
			// 
			this.labelPluginNameValue.AutoSize = true;
			this.labelPluginNameValue.Location = new System.Drawing.Point(239, 34);
			this.labelPluginNameValue.Name = "labelPluginNameValue";
			this.labelPluginNameValue.Size = new System.Drawing.Size(41, 13);
			this.labelPluginNameValue.TabIndex = 12;
			this.labelPluginNameValue.Text = "[name]";
			// 
			// buttonConfigurePlugin
			// 
			this.buttonConfigurePlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonConfigurePlugin.Location = new System.Drawing.Point(489, 330);
			this.buttonConfigurePlugin.Name = "buttonConfigurePlugin";
			this.buttonConfigurePlugin.Size = new System.Drawing.Size(92, 23);
			this.buttonConfigurePlugin.TabIndex = 11;
			this.buttonConfigurePlugin.Text = "Configure...";
			this.buttonConfigurePlugin.UseVisualStyleBackColor = true;
			// 
			// labeledDividerPlugin
			// 
			this.labeledDividerPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerPlugin.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerPlugin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(170)))));
			this.labeledDividerPlugin.Location = new System.Drawing.Point(144, 8);
			this.labeledDividerPlugin.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerPlugin.Name = "labeledDividerPlugin";
			this.labeledDividerPlugin.Size = new System.Drawing.Size(437, 15);
			this.labeledDividerPlugin.TabIndex = 10;
			this.labeledDividerPlugin.Text = "Plugin";
			// 
			// labelPluginDescription
			// 
			this.labelPluginDescription.AutoSize = true;
			this.labelPluginDescription.Location = new System.Drawing.Point(164, 62);
			this.labelPluginDescription.Name = "labelPluginDescription";
			this.labelPluginDescription.Size = new System.Drawing.Size(69, 13);
			this.labelPluginDescription.TabIndex = 7;
			this.labelPluginDescription.Text = "Description:";
			// 
			// labelPluginName
			// 
			this.labelPluginName.AutoSize = true;
			this.labelPluginName.Location = new System.Drawing.Point(164, 34);
			this.labelPluginName.Name = "labelPluginName";
			this.labelPluginName.Size = new System.Drawing.Size(39, 13);
			this.labelPluginName.TabIndex = 5;
			this.labelPluginName.Text = "Name:";
			// 
			// listViewPlugins
			// 
			this.listViewPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPlugins});
			this.listViewPlugins.FullRowSelect = true;
			this.listViewPlugins.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewPlugins.HideSelection = false;
			this.listViewPlugins.Location = new System.Drawing.Point(8, 8);
			this.listViewPlugins.MultiSelect = false;
			this.listViewPlugins.Name = "listViewPlugins";
			this.listViewPlugins.Size = new System.Drawing.Size(120, 345);
			this.listViewPlugins.TabIndex = 0;
			this.listViewPlugins.UseCompatibleStateImageBehavior = false;
			this.listViewPlugins.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderPlugins
			// 
			this.columnHeaderPlugins.Text = "Plugins";
			this.columnHeaderPlugins.Width = 100;
			// 
			// panelApplications
			// 
			this.panelApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelApplications.BackColor = System.Drawing.SystemColors.Window;
			this.panelApplications.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
			this.panelApplications.Controls.Add(this.textBoxApplicationArguments);
			this.panelApplications.Controls.Add(this.labelApplicationArguments);
			this.panelApplications.Controls.Add(this.buttonBrowseApplicationRelativePath);
			this.panelApplications.Controls.Add(this.labeledDividerApplication);
			this.panelApplications.Controls.Add(this.textBoxApplicationRelativePath);
			this.panelApplications.Controls.Add(this.labelApplicationRelativePath);
			this.panelApplications.Controls.Add(this.textBoxApplicationName);
			this.panelApplications.Controls.Add(this.labelApplicationName);
			this.panelApplications.Controls.Add(this.buttonRemoveApplication);
			this.panelApplications.Controls.Add(this.buttonAddApplication);
			this.panelApplications.Controls.Add(this.listViewApplications);
			this.panelApplications.Location = new System.Drawing.Point(174, 39);
			this.panelApplications.Name = "panelApplications";
			this.panelApplications.Padding = new System.Windows.Forms.Padding(5);
			this.panelApplications.Size = new System.Drawing.Size(589, 361);
			this.panelApplications.TabIndex = 17;
			// 
			// textBoxApplicationArguments
			// 
			this.textBoxApplicationArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxApplicationArguments.Location = new System.Drawing.Point(246, 87);
			this.textBoxApplicationArguments.Name = "textBoxApplicationArguments";
			this.textBoxApplicationArguments.Size = new System.Drawing.Size(284, 22);
			this.textBoxApplicationArguments.TabIndex = 13;
			// 
			// labelApplicationArguments
			// 
			this.labelApplicationArguments.AutoSize = true;
			this.labelApplicationArguments.Location = new System.Drawing.Point(164, 90);
			this.labelApplicationArguments.Name = "labelApplicationArguments";
			this.labelApplicationArguments.Size = new System.Drawing.Size(66, 13);
			this.labelApplicationArguments.TabIndex = 12;
			this.labelApplicationArguments.Text = "Arguments:";
			// 
			// buttonBrowseApplicationRelativePath
			// 
			this.buttonBrowseApplicationRelativePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseApplicationRelativePath.Location = new System.Drawing.Point(537, 58);
			this.buttonBrowseApplicationRelativePath.Name = "buttonBrowseApplicationRelativePath";
			this.buttonBrowseApplicationRelativePath.Size = new System.Drawing.Size(38, 23);
			this.buttonBrowseApplicationRelativePath.TabIndex = 11;
			this.buttonBrowseApplicationRelativePath.Text = "...";
			this.buttonBrowseApplicationRelativePath.UseVisualStyleBackColor = true;
			// 
			// labeledDividerApplication
			// 
			this.labeledDividerApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerApplication.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerApplication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(170)))));
			this.labeledDividerApplication.Location = new System.Drawing.Point(144, 8);
			this.labeledDividerApplication.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerApplication.Name = "labeledDividerApplication";
			this.labeledDividerApplication.Size = new System.Drawing.Size(437, 15);
			this.labeledDividerApplication.TabIndex = 10;
			this.labeledDividerApplication.Text = "Application";
			// 
			// textBoxApplicationRelativePath
			// 
			this.textBoxApplicationRelativePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxApplicationRelativePath.Location = new System.Drawing.Point(246, 59);
			this.textBoxApplicationRelativePath.Name = "textBoxApplicationRelativePath";
			this.textBoxApplicationRelativePath.Size = new System.Drawing.Size(284, 22);
			this.textBoxApplicationRelativePath.TabIndex = 8;
			// 
			// labelApplicationRelativePath
			// 
			this.labelApplicationRelativePath.AutoSize = true;
			this.labelApplicationRelativePath.Location = new System.Drawing.Point(164, 62);
			this.labelApplicationRelativePath.Name = "labelApplicationRelativePath";
			this.labelApplicationRelativePath.Size = new System.Drawing.Size(77, 13);
			this.labelApplicationRelativePath.TabIndex = 7;
			this.labelApplicationRelativePath.Text = "Relative path:";
			// 
			// textBoxApplicationName
			// 
			this.textBoxApplicationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxApplicationName.Location = new System.Drawing.Point(247, 31);
			this.textBoxApplicationName.Name = "textBoxApplicationName";
			this.textBoxApplicationName.Size = new System.Drawing.Size(284, 22);
			this.textBoxApplicationName.TabIndex = 6;
			// 
			// labelApplicationName
			// 
			this.labelApplicationName.AutoSize = true;
			this.labelApplicationName.Location = new System.Drawing.Point(164, 34);
			this.labelApplicationName.Name = "labelApplicationName";
			this.labelApplicationName.Size = new System.Drawing.Size(39, 13);
			this.labelApplicationName.TabIndex = 5;
			this.labelApplicationName.Text = "Name:";
			// 
			// buttonRemoveApplication
			// 
			this.buttonRemoveApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemoveApplication.Location = new System.Drawing.Point(71, 330);
			this.buttonRemoveApplication.Name = "buttonRemoveApplication";
			this.buttonRemoveApplication.Size = new System.Drawing.Size(58, 23);
			this.buttonRemoveApplication.TabIndex = 2;
			this.buttonRemoveApplication.Text = "Remove";
			this.buttonRemoveApplication.UseVisualStyleBackColor = true;
			// 
			// buttonAddApplication
			// 
			this.buttonAddApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddApplication.Location = new System.Drawing.Point(7, 330);
			this.buttonAddApplication.Name = "buttonAddApplication";
			this.buttonAddApplication.Size = new System.Drawing.Size(58, 23);
			this.buttonAddApplication.TabIndex = 1;
			this.buttonAddApplication.Text = "Add";
			this.buttonAddApplication.UseVisualStyleBackColor = true;
			// 
			// listViewApplications
			// 
			this.listViewApplications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewApplications.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderApplications});
			this.listViewApplications.FullRowSelect = true;
			this.listViewApplications.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewApplications.HideSelection = false;
			this.listViewApplications.Location = new System.Drawing.Point(8, 8);
			this.listViewApplications.MultiSelect = false;
			this.listViewApplications.Name = "listViewApplications";
			this.listViewApplications.Size = new System.Drawing.Size(120, 316);
			this.listViewApplications.TabIndex = 0;
			this.listViewApplications.UseCompatibleStateImageBehavior = false;
			this.listViewApplications.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderApplications
			// 
			this.columnHeaderApplications.Text = "Applications";
			this.columnHeaderApplications.Width = 100;
			// 
			// treeViewConfiguration
			// 
			this.treeViewConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeViewConfiguration.FullRowSelect = true;
			this.treeViewConfiguration.HideSelection = false;
			this.treeViewConfiguration.HotTracking = true;
			this.treeViewConfiguration.Location = new System.Drawing.Point(12, 39);
			this.treeViewConfiguration.Name = "treeViewConfiguration";
			treeNode1.Name = "Groups";
			treeNode1.Text = "Groups";
			treeNode2.Name = "Applications";
			treeNode2.Text = "Applications";
			treeNode3.Name = "Setup";
			treeNode3.Text = "Setup";
			treeNode4.Name = "Plugins";
			treeNode4.Text = "Plugins";
			this.treeViewConfiguration.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
			this.treeViewConfiguration.ShowLines = false;
			this.treeViewConfiguration.Size = new System.Drawing.Size(156, 361);
			this.treeViewConfiguration.TabIndex = 2;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(573, 406);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(92, 23);
			this.buttonCancel.TabIndex = 19;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(475, 406);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(92, 23);
			this.buttonOK.TabIndex = 20;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			// 
			// ConfigurationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(775, 441);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonApply);
			this.Controls.Add(this.panelGroups);
			this.Controls.Add(this.panelPlugins);
			this.Controls.Add(this.panelApplications);
			this.Controls.Add(this.treeViewConfiguration);
			this.Controls.Add(this.buttonMachines);
			this.Controls.Add(this.comboBoxMachines);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigurationForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Process Manager Configuration";
			this.Load += new System.EventHandler(this.ConfigurationForm_Load);
			this.panelGroups.ResumeLayout(false);
			this.panelGroups.PerformLayout();
			this.panelPlugins.ResumeLayout(false);
			this.panelPlugins.PerformLayout();
			this.panelApplications.ResumeLayout(false);
			this.panelApplications.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxMachines;
		private System.Windows.Forms.Button buttonMachines;
		private Controls.TreeView treeViewConfiguration;
		private Controls.BackgroundPanel panelGroups;
		private System.Windows.Forms.Button buttonRemoveGroup;
		private System.Windows.Forms.Button buttonAddGroup;
		private Controls.ListView listViewGroups;
		private System.Windows.Forms.TextBox textBoxGroupPath;
		private System.Windows.Forms.Label labelGroupPath;
		private System.Windows.Forms.TextBox textBoxGroupName;
		private System.Windows.Forms.Label labelGroupName;
		private Controls.LabeledDivider labeledDividerGroupApplicationMappings;
		private Controls.LabeledDivider labeledDividerGroup;
		private System.Windows.Forms.Label labelGroupApplications;
		private System.Windows.Forms.Button buttonBrowseGroupPath;
		private System.Windows.Forms.Button buttonCopyGroupApplications;
		private System.Windows.Forms.Button buttonRemoveGroupApplication;
		private System.Windows.Forms.Button buttonAddGroupApplication;
		private Controls.ListView listViewGroupApplications;
		private System.Windows.Forms.ColumnHeader columnHeaderGroups;
		private System.Windows.Forms.ColumnHeader columnHeaderApplicationMappings;
		private Controls.BackgroundPanel panelApplications;
		private System.Windows.Forms.TextBox textBoxApplicationArguments;
		private System.Windows.Forms.Label labelApplicationArguments;
		private System.Windows.Forms.Button buttonBrowseApplicationRelativePath;
		private Controls.LabeledDivider labeledDividerApplication;
		private System.Windows.Forms.TextBox textBoxApplicationRelativePath;
		private System.Windows.Forms.Label labelApplicationRelativePath;
		private System.Windows.Forms.TextBox textBoxApplicationName;
		private System.Windows.Forms.Label labelApplicationName;
		private System.Windows.Forms.Button buttonRemoveApplication;
		private System.Windows.Forms.Button buttonAddApplication;
		private Controls.ListView listViewApplications;
		private System.Windows.Forms.ColumnHeader columnHeaderApplications;
		private Controls.BackgroundPanel panelPlugins;
		private System.Windows.Forms.Label labelPluginDescriptionValue;
		private System.Windows.Forms.Label labelPluginNameValue;
		private System.Windows.Forms.Button buttonConfigurePlugin;
		private Controls.LabeledDivider labeledDividerPlugin;
		private System.Windows.Forms.Label labelPluginDescription;
		private System.Windows.Forms.Label labelPluginName;
		private Controls.ListView listViewPlugins;
		private System.Windows.Forms.ColumnHeader columnHeaderPlugins;
		private System.Windows.Forms.CheckBox checkBoxPluginEnabled;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;

	}
}


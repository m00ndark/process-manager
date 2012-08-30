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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
			this.comboBoxMachines = new System.Windows.Forms.ComboBox();
			this.buttonMachines = new System.Windows.Forms.Button();
			this.buttonApply = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.labelNothingToShow = new System.Windows.Forms.Label();
			this.labelMachineNotAvailable = new System.Windows.Forms.Label();
			this.panelGroups = new ProcessManagerUI.Controls.BackgroundPanel();
			this.panelGroup = new System.Windows.Forms.Panel();
			this.labeledDividerGroup = new ProcessManagerUI.Controls.LabeledDivider();
			this.buttonCopyGroupApplications = new System.Windows.Forms.Button();
			this.labelGroupName = new System.Windows.Forms.Label();
			this.buttonRemoveGroupApplication = new System.Windows.Forms.Button();
			this.textBoxGroupName = new System.Windows.Forms.TextBox();
			this.buttonAddGroupApplication = new System.Windows.Forms.Button();
			this.labelGroupPath = new System.Windows.Forms.Label();
			this.listViewGroupApplications = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderApplicationMappings = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.textBoxGroupPath = new System.Windows.Forms.TextBox();
			this.labelGroupApplications = new System.Windows.Forms.Label();
			this.labeledDividerGroupApplicationMappings = new ProcessManagerUI.Controls.LabeledDivider();
			this.buttonBrowseGroupPath = new System.Windows.Forms.Button();
			this.labelNoGroupSelected = new System.Windows.Forms.Label();
			this.buttonRemoveGroup = new System.Windows.Forms.Button();
			this.buttonAddGroup = new System.Windows.Forms.Button();
			this.listViewGroups = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderGroups = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panelPlugins = new ProcessManagerUI.Controls.BackgroundPanel();
			this.panelPlugin = new System.Windows.Forms.Panel();
			this.checkBoxPluginEnabled = new System.Windows.Forms.CheckBox();
			this.labeledDividerPlugin = new ProcessManagerUI.Controls.LabeledDivider();
			this.labelPluginDescriptionValue = new System.Windows.Forms.Label();
			this.labelPluginName = new System.Windows.Forms.Label();
			this.labelPluginNameValue = new System.Windows.Forms.Label();
			this.labelPluginDescription = new System.Windows.Forms.Label();
			this.buttonConfigurePlugin = new System.Windows.Forms.Button();
			this.labelNoPluginSelected = new System.Windows.Forms.Label();
			this.listViewPlugins = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderPlugins = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panelApplications = new ProcessManagerUI.Controls.BackgroundPanel();
			this.panelApplication = new System.Windows.Forms.Panel();
			this.labeledDividerDistribution = new ProcessManagerUI.Controls.LabeledDivider();
			this.labelDistributionSourcesCount = new System.Windows.Forms.Label();
			this.buttonEditDistributionSources = new System.Windows.Forms.Button();
			this.labelDistributionSources = new System.Windows.Forms.Label();
			this.labeledDividerApplication = new ProcessManagerUI.Controls.LabeledDivider();
			this.textBoxApplicationArguments = new System.Windows.Forms.TextBox();
			this.labelApplicationName = new System.Windows.Forms.Label();
			this.labelApplicationArguments = new System.Windows.Forms.Label();
			this.textBoxApplicationName = new System.Windows.Forms.TextBox();
			this.buttonBrowseApplicationRelativePath = new System.Windows.Forms.Button();
			this.labelApplicationRelativePath = new System.Windows.Forms.Label();
			this.textBoxApplicationRelativePath = new System.Windows.Forms.TextBox();
			this.labelNoApplicationSelected = new System.Windows.Forms.Label();
			this.buttonRemoveApplication = new System.Windows.Forms.Button();
			this.buttonAddApplication = new System.Windows.Forms.Button();
			this.listViewApplications = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderApplications = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.treeViewConfiguration = new ProcessManagerUI.Controls.TreeView();
			this.panelGroups.SuspendLayout();
			this.panelGroup.SuspendLayout();
			this.panelPlugins.SuspendLayout();
			this.panelPlugin.SuspendLayout();
			this.panelApplications.SuspendLayout();
			this.panelApplication.SuspendLayout();
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
			this.comboBoxMachines.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMachines_SelectedIndexChanged);
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
			this.buttonMachines.Click += new System.EventHandler(this.ButtonMachines_Click);
			// 
			// buttonApply
			// 
			this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonApply.Enabled = false;
			this.buttonApply.Location = new System.Drawing.Point(671, 407);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(92, 23);
			this.buttonApply.TabIndex = 17;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(573, 407);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(92, 23);
			this.buttonCancel.TabIndex = 19;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(475, 407);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(92, 23);
			this.buttonOK.TabIndex = 20;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
			// 
			// labelNothingToShow
			// 
			this.labelNothingToShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelNothingToShow.Location = new System.Drawing.Point(174, 39);
			this.labelNothingToShow.Name = "labelNothingToShow";
			this.labelNothingToShow.Size = new System.Drawing.Size(588, 362);
			this.labelNothingToShow.TabIndex = 21;
			this.labelNothingToShow.Text = "There are no items to show in this view";
			this.labelNothingToShow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelMachineNotAvailable
			// 
			this.labelMachineNotAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelMachineNotAvailable.Location = new System.Drawing.Point(12, 39);
			this.labelMachineNotAvailable.Name = "labelMachineNotAvailable";
			this.labelMachineNotAvailable.Size = new System.Drawing.Size(750, 362);
			this.labelMachineNotAvailable.TabIndex = 22;
			this.labelMachineNotAvailable.Text = "The selected machine is not available";
			this.labelMachineNotAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelMachineNotAvailable.Visible = false;
			// 
			// panelGroups
			// 
			this.panelGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelGroups.BackColor = System.Drawing.SystemColors.Window;
			this.panelGroups.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
			this.panelGroups.Controls.Add(this.panelGroup);
			this.panelGroups.Controls.Add(this.labelNoGroupSelected);
			this.panelGroups.Controls.Add(this.buttonRemoveGroup);
			this.panelGroups.Controls.Add(this.buttonAddGroup);
			this.panelGroups.Controls.Add(this.listViewGroups);
			this.panelGroups.Location = new System.Drawing.Point(174, 39);
			this.panelGroups.Name = "panelGroups";
			this.panelGroups.Padding = new System.Windows.Forms.Padding(5);
			this.panelGroups.Size = new System.Drawing.Size(588, 362);
			this.panelGroups.TabIndex = 3;
			this.panelGroups.Visible = false;
			// 
			// panelGroup
			// 
			this.panelGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelGroup.Controls.Add(this.labeledDividerGroup);
			this.panelGroup.Controls.Add(this.buttonCopyGroupApplications);
			this.panelGroup.Controls.Add(this.labelGroupName);
			this.panelGroup.Controls.Add(this.buttonRemoveGroupApplication);
			this.panelGroup.Controls.Add(this.textBoxGroupName);
			this.panelGroup.Controls.Add(this.buttonAddGroupApplication);
			this.panelGroup.Controls.Add(this.labelGroupPath);
			this.panelGroup.Controls.Add(this.listViewGroupApplications);
			this.panelGroup.Controls.Add(this.textBoxGroupPath);
			this.panelGroup.Controls.Add(this.labelGroupApplications);
			this.panelGroup.Controls.Add(this.labeledDividerGroupApplicationMappings);
			this.panelGroup.Controls.Add(this.buttonBrowseGroupPath);
			this.panelGroup.Location = new System.Drawing.Point(144, 8);
			this.panelGroup.Margin = new System.Windows.Forms.Padding(0);
			this.panelGroup.Name = "panelGroup";
			this.panelGroup.Size = new System.Drawing.Size(436, 345);
			this.panelGroup.TabIndex = 17;
			this.panelGroup.Visible = false;
			// 
			// labeledDividerGroup
			// 
			this.labeledDividerGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerGroup.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerGroup.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labeledDividerGroup.Location = new System.Drawing.Point(0, 0);
			this.labeledDividerGroup.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerGroup.Name = "labeledDividerGroup";
			this.labeledDividerGroup.Size = new System.Drawing.Size(436, 15);
			this.labeledDividerGroup.TabIndex = 10;
			this.labeledDividerGroup.Text = "Group";
			// 
			// buttonCopyGroupApplications
			// 
			this.buttonCopyGroupApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCopyGroupApplications.Image = global::ProcessManagerUI.Properties.Resources.copy_16;
			this.buttonCopyGroupApplications.Location = new System.Drawing.Point(392, 178);
			this.buttonCopyGroupApplications.Name = "buttonCopyGroupApplications";
			this.buttonCopyGroupApplications.Size = new System.Drawing.Size(38, 24);
			this.buttonCopyGroupApplications.TabIndex = 16;
			this.buttonCopyGroupApplications.UseVisualStyleBackColor = true;
			this.buttonCopyGroupApplications.Click += new System.EventHandler(this.ButtonCopyGroupApplications_Click);
			// 
			// labelGroupName
			// 
			this.labelGroupName.AutoSize = true;
			this.labelGroupName.Location = new System.Drawing.Point(20, 26);
			this.labelGroupName.Name = "labelGroupName";
			this.labelGroupName.Size = new System.Drawing.Size(39, 13);
			this.labelGroupName.TabIndex = 5;
			this.labelGroupName.Text = "Name:";
			// 
			// buttonRemoveGroupApplication
			// 
			this.buttonRemoveGroupApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRemoveGroupApplication.Image = global::ProcessManagerUI.Properties.Resources.remove_16;
			this.buttonRemoveGroupApplication.Location = new System.Drawing.Point(392, 148);
			this.buttonRemoveGroupApplication.Name = "buttonRemoveGroupApplication";
			this.buttonRemoveGroupApplication.Size = new System.Drawing.Size(38, 24);
			this.buttonRemoveGroupApplication.TabIndex = 15;
			this.buttonRemoveGroupApplication.UseVisualStyleBackColor = true;
			this.buttonRemoveGroupApplication.Click += new System.EventHandler(this.ButtonRemoveGroupApplication_Click);
			// 
			// textBoxGroupName
			// 
			this.textBoxGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxGroupName.Location = new System.Drawing.Point(100, 23);
			this.textBoxGroupName.Name = "textBoxGroupName";
			this.textBoxGroupName.Size = new System.Drawing.Size(286, 22);
			this.textBoxGroupName.TabIndex = 6;
			this.textBoxGroupName.TextChanged += new System.EventHandler(this.TextBoxGroupName_TextChanged);
			this.textBoxGroupName.Leave += new System.EventHandler(this.TextBoxGroupName_Leave);
			// 
			// buttonAddGroupApplication
			// 
			this.buttonAddGroupApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddGroupApplication.Image = global::ProcessManagerUI.Properties.Resources.add_16;
			this.buttonAddGroupApplication.Location = new System.Drawing.Point(392, 118);
			this.buttonAddGroupApplication.Name = "buttonAddGroupApplication";
			this.buttonAddGroupApplication.Size = new System.Drawing.Size(38, 24);
			this.buttonAddGroupApplication.TabIndex = 14;
			this.buttonAddGroupApplication.UseVisualStyleBackColor = true;
			this.buttonAddGroupApplication.Click += new System.EventHandler(this.ButtonAddGroupApplication_Click);
			// 
			// labelGroupPath
			// 
			this.labelGroupPath.AutoSize = true;
			this.labelGroupPath.Location = new System.Drawing.Point(20, 54);
			this.labelGroupPath.Name = "labelGroupPath";
			this.labelGroupPath.Size = new System.Drawing.Size(33, 13);
			this.labelGroupPath.TabIndex = 7;
			this.labelGroupPath.Text = "Path:";
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
			this.listViewGroupApplications.Location = new System.Drawing.Point(100, 119);
			this.listViewGroupApplications.MultiSelect = false;
			this.listViewGroupApplications.Name = "listViewGroupApplications";
			this.listViewGroupApplications.Size = new System.Drawing.Size(286, 226);
			this.listViewGroupApplications.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewGroupApplications.TabIndex = 13;
			this.listViewGroupApplications.UseCompatibleStateImageBehavior = false;
			this.listViewGroupApplications.View = System.Windows.Forms.View.Details;
			this.listViewGroupApplications.SelectedIndexChanged += new System.EventHandler(this.ListViewGroupApplications_SelectedIndexChanged);
			// 
			// columnHeaderApplicationMappings
			// 
			this.columnHeaderApplicationMappings.Text = "ApplicationMappings";
			this.columnHeaderApplicationMappings.Width = 265;
			// 
			// textBoxGroupPath
			// 
			this.textBoxGroupPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxGroupPath.Location = new System.Drawing.Point(100, 51);
			this.textBoxGroupPath.Name = "textBoxGroupPath";
			this.textBoxGroupPath.Size = new System.Drawing.Size(286, 22);
			this.textBoxGroupPath.TabIndex = 8;
			this.textBoxGroupPath.TextChanged += new System.EventHandler(this.TextBoxGroupPath_TextChanged);
			this.textBoxGroupPath.MouseLeave += new System.EventHandler(this.TextBoxGroupPath_MouseLeave);
			// 
			// labelGroupApplications
			// 
			this.labelGroupApplications.AutoSize = true;
			this.labelGroupApplications.Location = new System.Drawing.Point(20, 122);
			this.labelGroupApplications.Name = "labelGroupApplications";
			this.labelGroupApplications.Size = new System.Drawing.Size(74, 13);
			this.labelGroupApplications.TabIndex = 12;
			this.labelGroupApplications.Text = "Applications:";
			// 
			// labeledDividerGroupApplicationMappings
			// 
			this.labeledDividerGroupApplicationMappings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerGroupApplicationMappings.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerGroupApplicationMappings.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labeledDividerGroupApplicationMappings.Location = new System.Drawing.Point(0, 95);
			this.labeledDividerGroupApplicationMappings.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerGroupApplicationMappings.Name = "labeledDividerGroupApplicationMappings";
			this.labeledDividerGroupApplicationMappings.Size = new System.Drawing.Size(436, 15);
			this.labeledDividerGroupApplicationMappings.TabIndex = 9;
			this.labeledDividerGroupApplicationMappings.Text = "Mappings";
			// 
			// buttonBrowseGroupPath
			// 
			this.buttonBrowseGroupPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseGroupPath.Location = new System.Drawing.Point(392, 50);
			this.buttonBrowseGroupPath.Name = "buttonBrowseGroupPath";
			this.buttonBrowseGroupPath.Size = new System.Drawing.Size(38, 23);
			this.buttonBrowseGroupPath.TabIndex = 11;
			this.buttonBrowseGroupPath.Text = "...";
			this.buttonBrowseGroupPath.UseVisualStyleBackColor = true;
			this.buttonBrowseGroupPath.Click += new System.EventHandler(this.ButtonBrowseGroupPath_Click);
			// 
			// labelNoGroupSelected
			// 
			this.labelNoGroupSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelNoGroupSelected.Location = new System.Drawing.Point(144, 8);
			this.labelNoGroupSelected.Name = "labelNoGroupSelected";
			this.labelNoGroupSelected.Size = new System.Drawing.Size(436, 345);
			this.labelNoGroupSelected.TabIndex = 22;
			this.labelNoGroupSelected.Text = "No group selected";
			this.labelNoGroupSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonRemoveGroup
			// 
			this.buttonRemoveGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemoveGroup.Location = new System.Drawing.Point(71, 331);
			this.buttonRemoveGroup.Name = "buttonRemoveGroup";
			this.buttonRemoveGroup.Size = new System.Drawing.Size(58, 23);
			this.buttonRemoveGroup.TabIndex = 2;
			this.buttonRemoveGroup.Text = "Remove";
			this.buttonRemoveGroup.UseVisualStyleBackColor = true;
			this.buttonRemoveGroup.Click += new System.EventHandler(this.ButtonRemoveGroup_Click);
			// 
			// buttonAddGroup
			// 
			this.buttonAddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddGroup.Location = new System.Drawing.Point(7, 331);
			this.buttonAddGroup.Name = "buttonAddGroup";
			this.buttonAddGroup.Size = new System.Drawing.Size(58, 23);
			this.buttonAddGroup.TabIndex = 1;
			this.buttonAddGroup.Text = "Add";
			this.buttonAddGroup.UseVisualStyleBackColor = true;
			this.buttonAddGroup.Click += new System.EventHandler(this.ButtonAddGroup_Click);
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
			this.listViewGroups.Size = new System.Drawing.Size(120, 317);
			this.listViewGroups.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewGroups.TabIndex = 0;
			this.listViewGroups.UseCompatibleStateImageBehavior = false;
			this.listViewGroups.View = System.Windows.Forms.View.Details;
			this.listViewGroups.SelectedIndexChanged += new System.EventHandler(this.ListViewGroups_SelectedIndexChanged);
			// 
			// columnHeaderGroups
			// 
			this.columnHeaderGroups.Text = "Groups";
			this.columnHeaderGroups.Width = 99;
			// 
			// panelPlugins
			// 
			this.panelPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPlugins.BackColor = System.Drawing.SystemColors.Window;
			this.panelPlugins.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
			this.panelPlugins.Controls.Add(this.panelPlugin);
			this.panelPlugins.Controls.Add(this.labelNoPluginSelected);
			this.panelPlugins.Controls.Add(this.listViewPlugins);
			this.panelPlugins.Location = new System.Drawing.Point(174, 39);
			this.panelPlugins.Name = "panelPlugins";
			this.panelPlugins.Padding = new System.Windows.Forms.Padding(5);
			this.panelPlugins.Size = new System.Drawing.Size(588, 362);
			this.panelPlugins.TabIndex = 18;
			this.panelPlugins.Visible = false;
			// 
			// panelPlugin
			// 
			this.panelPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPlugin.Controls.Add(this.checkBoxPluginEnabled);
			this.panelPlugin.Controls.Add(this.labeledDividerPlugin);
			this.panelPlugin.Controls.Add(this.labelPluginDescriptionValue);
			this.panelPlugin.Controls.Add(this.labelPluginName);
			this.panelPlugin.Controls.Add(this.labelPluginNameValue);
			this.panelPlugin.Controls.Add(this.labelPluginDescription);
			this.panelPlugin.Controls.Add(this.buttonConfigurePlugin);
			this.panelPlugin.Location = new System.Drawing.Point(144, 8);
			this.panelPlugin.Margin = new System.Windows.Forms.Padding(0);
			this.panelPlugin.Name = "panelPlugin";
			this.panelPlugin.Size = new System.Drawing.Size(436, 345);
			this.panelPlugin.TabIndex = 15;
			this.panelPlugin.Visible = false;
			// 
			// checkBoxPluginEnabled
			// 
			this.checkBoxPluginEnabled.AutoSize = true;
			this.checkBoxPluginEnabled.Location = new System.Drawing.Point(3, 327);
			this.checkBoxPluginEnabled.Name = "checkBoxPluginEnabled";
			this.checkBoxPluginEnabled.Size = new System.Drawing.Size(68, 17);
			this.checkBoxPluginEnabled.TabIndex = 14;
			this.checkBoxPluginEnabled.Text = "Enabled";
			this.checkBoxPluginEnabled.UseVisualStyleBackColor = true;
			// 
			// labeledDividerPlugin
			// 
			this.labeledDividerPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerPlugin.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerPlugin.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labeledDividerPlugin.Location = new System.Drawing.Point(0, 0);
			this.labeledDividerPlugin.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerPlugin.Name = "labeledDividerPlugin";
			this.labeledDividerPlugin.Size = new System.Drawing.Size(436, 15);
			this.labeledDividerPlugin.TabIndex = 10;
			this.labeledDividerPlugin.Text = "Plugin";
			// 
			// labelPluginDescriptionValue
			// 
			this.labelPluginDescriptionValue.Location = new System.Drawing.Point(95, 54);
			this.labelPluginDescriptionValue.Name = "labelPluginDescriptionValue";
			this.labelPluginDescriptionValue.Size = new System.Drawing.Size(336, 262);
			this.labelPluginDescriptionValue.TabIndex = 13;
			this.labelPluginDescriptionValue.Text = "[description]";
			// 
			// labelPluginName
			// 
			this.labelPluginName.AutoSize = true;
			this.labelPluginName.Location = new System.Drawing.Point(20, 26);
			this.labelPluginName.Name = "labelPluginName";
			this.labelPluginName.Size = new System.Drawing.Size(39, 13);
			this.labelPluginName.TabIndex = 5;
			this.labelPluginName.Text = "Name:";
			// 
			// labelPluginNameValue
			// 
			this.labelPluginNameValue.AutoSize = true;
			this.labelPluginNameValue.Location = new System.Drawing.Point(95, 26);
			this.labelPluginNameValue.Name = "labelPluginNameValue";
			this.labelPluginNameValue.Size = new System.Drawing.Size(41, 13);
			this.labelPluginNameValue.TabIndex = 12;
			this.labelPluginNameValue.Text = "[name]";
			// 
			// labelPluginDescription
			// 
			this.labelPluginDescription.AutoSize = true;
			this.labelPluginDescription.Location = new System.Drawing.Point(20, 54);
			this.labelPluginDescription.Name = "labelPluginDescription";
			this.labelPluginDescription.Size = new System.Drawing.Size(69, 13);
			this.labelPluginDescription.TabIndex = 7;
			this.labelPluginDescription.Text = "Description:";
			// 
			// buttonConfigurePlugin
			// 
			this.buttonConfigurePlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonConfigurePlugin.Location = new System.Drawing.Point(345, 323);
			this.buttonConfigurePlugin.Name = "buttonConfigurePlugin";
			this.buttonConfigurePlugin.Size = new System.Drawing.Size(92, 23);
			this.buttonConfigurePlugin.TabIndex = 11;
			this.buttonConfigurePlugin.Text = "Configure...";
			this.buttonConfigurePlugin.UseVisualStyleBackColor = true;
			// 
			// labelNoPluginSelected
			// 
			this.labelNoPluginSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelNoPluginSelected.Location = new System.Drawing.Point(144, 8);
			this.labelNoPluginSelected.Name = "labelNoPluginSelected";
			this.labelNoPluginSelected.Size = new System.Drawing.Size(436, 345);
			this.labelNoPluginSelected.TabIndex = 24;
			this.labelNoPluginSelected.Text = "No plugin selected";
			this.labelNoPluginSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			this.listViewPlugins.Size = new System.Drawing.Size(120, 346);
			this.listViewPlugins.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewPlugins.TabIndex = 0;
			this.listViewPlugins.UseCompatibleStateImageBehavior = false;
			this.listViewPlugins.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderPlugins
			// 
			this.columnHeaderPlugins.Text = "Plugins";
			this.columnHeaderPlugins.Width = 99;
			// 
			// panelApplications
			// 
			this.panelApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelApplications.BackColor = System.Drawing.SystemColors.Window;
			this.panelApplications.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
			this.panelApplications.Controls.Add(this.panelApplication);
			this.panelApplications.Controls.Add(this.labelNoApplicationSelected);
			this.panelApplications.Controls.Add(this.buttonRemoveApplication);
			this.panelApplications.Controls.Add(this.buttonAddApplication);
			this.panelApplications.Controls.Add(this.listViewApplications);
			this.panelApplications.Location = new System.Drawing.Point(174, 39);
			this.panelApplications.Name = "panelApplications";
			this.panelApplications.Padding = new System.Windows.Forms.Padding(5);
			this.panelApplications.Size = new System.Drawing.Size(588, 362);
			this.panelApplications.TabIndex = 17;
			this.panelApplications.Visible = false;
			// 
			// panelApplication
			// 
			this.panelApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelApplication.Controls.Add(this.labeledDividerDistribution);
			this.panelApplication.Controls.Add(this.labelDistributionSourcesCount);
			this.panelApplication.Controls.Add(this.buttonEditDistributionSources);
			this.panelApplication.Controls.Add(this.labelDistributionSources);
			this.panelApplication.Controls.Add(this.labeledDividerApplication);
			this.panelApplication.Controls.Add(this.textBoxApplicationArguments);
			this.panelApplication.Controls.Add(this.labelApplicationName);
			this.panelApplication.Controls.Add(this.labelApplicationArguments);
			this.panelApplication.Controls.Add(this.textBoxApplicationName);
			this.panelApplication.Controls.Add(this.buttonBrowseApplicationRelativePath);
			this.panelApplication.Controls.Add(this.labelApplicationRelativePath);
			this.panelApplication.Controls.Add(this.textBoxApplicationRelativePath);
			this.panelApplication.Location = new System.Drawing.Point(144, 8);
			this.panelApplication.Margin = new System.Windows.Forms.Padding(0);
			this.panelApplication.Name = "panelApplication";
			this.panelApplication.Size = new System.Drawing.Size(436, 345);
			this.panelApplication.TabIndex = 14;
			this.panelApplication.Visible = false;
			// 
			// labeledDividerDistribution
			// 
			this.labeledDividerDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerDistribution.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerDistribution.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labeledDividerDistribution.Location = new System.Drawing.Point(0, 124);
			this.labeledDividerDistribution.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerDistribution.Name = "labeledDividerDistribution";
			this.labeledDividerDistribution.Size = new System.Drawing.Size(436, 15);
			this.labeledDividerDistribution.TabIndex = 23;
			this.labeledDividerDistribution.Text = "Distribution";
			// 
			// labelDistributionSourcesCount
			// 
			this.labelDistributionSourcesCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDistributionSourcesCount.ForeColor = System.Drawing.Color.Gray;
			this.labelDistributionSourcesCount.Location = new System.Drawing.Point(161, 151);
			this.labelDistributionSourcesCount.Name = "labelDistributionSourcesCount";
			this.labelDistributionSourcesCount.Size = new System.Drawing.Size(223, 13);
			this.labelDistributionSourcesCount.TabIndex = 22;
			this.labelDistributionSourcesCount.Text = "[source count]";
			this.labelDistributionSourcesCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// buttonEditDistributionSources
			// 
			this.buttonEditDistributionSources.Location = new System.Drawing.Point(103, 146);
			this.buttonEditDistributionSources.Name = "buttonEditDistributionSources";
			this.buttonEditDistributionSources.Size = new System.Drawing.Size(58, 23);
			this.buttonEditDistributionSources.TabIndex = 21;
			this.buttonEditDistributionSources.Text = "Edit...";
			this.buttonEditDistributionSources.UseVisualStyleBackColor = true;
			this.buttonEditDistributionSources.Click += new System.EventHandler(this.ButtonEditDistributionSources_Click);
			// 
			// labelDistributionSources
			// 
			this.labelDistributionSources.AutoSize = true;
			this.labelDistributionSources.Location = new System.Drawing.Point(20, 151);
			this.labelDistributionSources.Name = "labelDistributionSources";
			this.labelDistributionSources.Size = new System.Drawing.Size(50, 13);
			this.labelDistributionSources.TabIndex = 20;
			this.labelDistributionSources.Text = "Sources:";
			// 
			// labeledDividerApplication
			// 
			this.labeledDividerApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerApplication.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerApplication.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labeledDividerApplication.Location = new System.Drawing.Point(0, 0);
			this.labeledDividerApplication.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerApplication.Name = "labeledDividerApplication";
			this.labeledDividerApplication.Size = new System.Drawing.Size(436, 15);
			this.labeledDividerApplication.TabIndex = 10;
			this.labeledDividerApplication.Text = "Application";
			// 
			// textBoxApplicationArguments
			// 
			this.textBoxApplicationArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxApplicationArguments.Location = new System.Drawing.Point(102, 79);
			this.textBoxApplicationArguments.Name = "textBoxApplicationArguments";
			this.textBoxApplicationArguments.Size = new System.Drawing.Size(283, 22);
			this.textBoxApplicationArguments.TabIndex = 13;
			this.textBoxApplicationArguments.TextChanged += new System.EventHandler(this.TextBoxApplicationArguments_TextChanged);
			this.textBoxApplicationArguments.Leave += new System.EventHandler(this.TextBoxApplicationArguments_Leave);
			// 
			// labelApplicationName
			// 
			this.labelApplicationName.AutoSize = true;
			this.labelApplicationName.Location = new System.Drawing.Point(20, 26);
			this.labelApplicationName.Name = "labelApplicationName";
			this.labelApplicationName.Size = new System.Drawing.Size(39, 13);
			this.labelApplicationName.TabIndex = 5;
			this.labelApplicationName.Text = "Name:";
			// 
			// labelApplicationArguments
			// 
			this.labelApplicationArguments.AutoSize = true;
			this.labelApplicationArguments.Location = new System.Drawing.Point(20, 82);
			this.labelApplicationArguments.Name = "labelApplicationArguments";
			this.labelApplicationArguments.Size = new System.Drawing.Size(66, 13);
			this.labelApplicationArguments.TabIndex = 12;
			this.labelApplicationArguments.Text = "Arguments:";
			// 
			// textBoxApplicationName
			// 
			this.textBoxApplicationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxApplicationName.Location = new System.Drawing.Point(103, 23);
			this.textBoxApplicationName.Name = "textBoxApplicationName";
			this.textBoxApplicationName.Size = new System.Drawing.Size(283, 22);
			this.textBoxApplicationName.TabIndex = 6;
			this.textBoxApplicationName.TextChanged += new System.EventHandler(this.TextBoxApplicationName_TextChanged);
			this.textBoxApplicationName.Leave += new System.EventHandler(this.TextBoxApplicationName_Leave);
			// 
			// buttonBrowseApplicationRelativePath
			// 
			this.buttonBrowseApplicationRelativePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseApplicationRelativePath.Location = new System.Drawing.Point(392, 50);
			this.buttonBrowseApplicationRelativePath.Name = "buttonBrowseApplicationRelativePath";
			this.buttonBrowseApplicationRelativePath.Size = new System.Drawing.Size(38, 23);
			this.buttonBrowseApplicationRelativePath.TabIndex = 11;
			this.buttonBrowseApplicationRelativePath.Text = "...";
			this.buttonBrowseApplicationRelativePath.UseVisualStyleBackColor = true;
			this.buttonBrowseApplicationRelativePath.Click += new System.EventHandler(this.ButtonBrowseApplicationRelativePath_Click);
			// 
			// labelApplicationRelativePath
			// 
			this.labelApplicationRelativePath.AutoSize = true;
			this.labelApplicationRelativePath.Location = new System.Drawing.Point(20, 54);
			this.labelApplicationRelativePath.Name = "labelApplicationRelativePath";
			this.labelApplicationRelativePath.Size = new System.Drawing.Size(77, 13);
			this.labelApplicationRelativePath.TabIndex = 7;
			this.labelApplicationRelativePath.Text = "Relative path:";
			// 
			// textBoxApplicationRelativePath
			// 
			this.textBoxApplicationRelativePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxApplicationRelativePath.Location = new System.Drawing.Point(102, 51);
			this.textBoxApplicationRelativePath.Name = "textBoxApplicationRelativePath";
			this.textBoxApplicationRelativePath.Size = new System.Drawing.Size(283, 22);
			this.textBoxApplicationRelativePath.TabIndex = 8;
			this.textBoxApplicationRelativePath.TextChanged += new System.EventHandler(this.TextBoxApplicationRelativePath_TextChanged);
			this.textBoxApplicationRelativePath.Leave += new System.EventHandler(this.TextBoxApplicationRelativePath_Leave);
			// 
			// labelNoApplicationSelected
			// 
			this.labelNoApplicationSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelNoApplicationSelected.Location = new System.Drawing.Point(144, 8);
			this.labelNoApplicationSelected.Name = "labelNoApplicationSelected";
			this.labelNoApplicationSelected.Size = new System.Drawing.Size(436, 345);
			this.labelNoApplicationSelected.TabIndex = 23;
			this.labelNoApplicationSelected.Text = "No application selected";
			this.labelNoApplicationSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonRemoveApplication
			// 
			this.buttonRemoveApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemoveApplication.Location = new System.Drawing.Point(71, 331);
			this.buttonRemoveApplication.Name = "buttonRemoveApplication";
			this.buttonRemoveApplication.Size = new System.Drawing.Size(58, 23);
			this.buttonRemoveApplication.TabIndex = 2;
			this.buttonRemoveApplication.Text = "Remove";
			this.buttonRemoveApplication.UseVisualStyleBackColor = true;
			this.buttonRemoveApplication.Click += new System.EventHandler(this.ButtonRemoveApplication_Click);
			// 
			// buttonAddApplication
			// 
			this.buttonAddApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddApplication.Location = new System.Drawing.Point(7, 331);
			this.buttonAddApplication.Name = "buttonAddApplication";
			this.buttonAddApplication.Size = new System.Drawing.Size(58, 23);
			this.buttonAddApplication.TabIndex = 1;
			this.buttonAddApplication.Text = "Add";
			this.buttonAddApplication.UseVisualStyleBackColor = true;
			this.buttonAddApplication.Click += new System.EventHandler(this.ButtonAddApplication_Click);
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
			this.listViewApplications.Size = new System.Drawing.Size(120, 317);
			this.listViewApplications.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewApplications.TabIndex = 0;
			this.listViewApplications.UseCompatibleStateImageBehavior = false;
			this.listViewApplications.View = System.Windows.Forms.View.Details;
			this.listViewApplications.SelectedIndexChanged += new System.EventHandler(this.ListViewApplications_SelectedIndexChanged);
			// 
			// columnHeaderApplications
			// 
			this.columnHeaderApplications.Text = "Applications";
			this.columnHeaderApplications.Width = 99;
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
			this.treeViewConfiguration.ShowLines = false;
			this.treeViewConfiguration.Size = new System.Drawing.Size(156, 362);
			this.treeViewConfiguration.TabIndex = 2;
			this.treeViewConfiguration.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewConfiguration_AfterSelect);
			// 
			// ConfigurationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(774, 442);
			this.Controls.Add(this.panelApplications);
			this.Controls.Add(this.panelGroups);
			this.Controls.Add(this.panelPlugins);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.labelNothingToShow);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonApply);
			this.Controls.Add(this.treeViewConfiguration);
			this.Controls.Add(this.buttonMachines);
			this.Controls.Add(this.comboBoxMachines);
			this.Controls.Add(this.labelMachineNotAvailable);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(790, 480);
			this.Name = "ConfigurationForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Process Manager Configuration";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationForm_FormClosing);
			this.Load += new System.EventHandler(this.ConfigurationForm_Load);
			this.panelGroups.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			this.panelPlugins.ResumeLayout(false);
			this.panelPlugin.ResumeLayout(false);
			this.panelPlugin.PerformLayout();
			this.panelApplications.ResumeLayout(false);
			this.panelApplication.ResumeLayout(false);
			this.panelApplication.PerformLayout();
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
		private System.Windows.Forms.Panel panelGroup;
		private System.Windows.Forms.Panel panelApplication;
		private System.Windows.Forms.Panel panelPlugin;
		private System.Windows.Forms.Label labelNothingToShow;
		private System.Windows.Forms.Label labelNoGroupSelected;
		private System.Windows.Forms.Label labelNoPluginSelected;
		private System.Windows.Forms.Label labelNoApplicationSelected;
		private System.Windows.Forms.Label labelMachineNotAvailable;
		private Controls.LabeledDivider labeledDividerDistribution;
		private System.Windows.Forms.Label labelDistributionSourcesCount;
		private System.Windows.Forms.Button buttonEditDistributionSources;
		private System.Windows.Forms.Label labelDistributionSources;

	}
}


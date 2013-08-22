namespace ProcessManagerUI.Forms
{
	partial class ControlPanelForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPanelForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStripSystemTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemSystemTrayConfiguration = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorSystemTray1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemSystemTrayExit = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanelControlPanel = new System.Windows.Forms.TableLayoutPanel();
			this.panelControlPanelApplications = new System.Windows.Forms.Panel();
			this.labelControlPanelUnavailable = new System.Windows.Forms.Label();
			this.linkLabelControlPanelExpandAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelControlPanelCollapseAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelControlPanelStopAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelControlPanelStartAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelControlPanelRestartAll = new System.Windows.Forms.LinkLabel();
			this.labelControlPanelAllSelected = new System.Windows.Forms.Label();
			this.flowLayoutPanelControlPanelApplications = new System.Windows.Forms.FlowLayoutPanel();
			this.panelControlPanelGroupByAndFilter = new System.Windows.Forms.Panel();
			this.tableLayoutPanelControlPanelFilter = new System.Windows.Forms.TableLayoutPanel();
			this.comboBoxControlPanelMachineFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxControlPanelApplicationFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxControlPanelGroupFilter = new System.Windows.Forms.ComboBox();
			this.labelControlPanelFilter = new System.Windows.Forms.Label();
			this.comboBoxControlPanelGroupBy = new System.Windows.Forms.ComboBox();
			this.labelControlPanelGroupBy = new System.Windows.Forms.Label();
			this.panelGlass = new System.Windows.Forms.Panel();
			this.tabControlSection = new System.Windows.Forms.TabControl();
			this.tabPageControlPanel = new System.Windows.Forms.TabPage();
			this.tabPageDistribution = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
			this.horizontalPanel = new ProcessManagerUI.Controls.HorizontalPanel();
			this.linkLabelOpenConfiguration = new System.Windows.Forms.LinkLabel();
			this.panelTabPageArea = new System.Windows.Forms.Panel();
			this.tableLayoutPanelDistribution = new System.Windows.Forms.TableLayoutPanel();
			this.panelDistributionApplications = new System.Windows.Forms.Panel();
			this.labelDistributionUnavailable = new System.Windows.Forms.Label();
			this.linkLabelDistributionExpandAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelDistributionCollapseAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelDistributionStopAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelDistributionStartAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelDistributionRestartAll = new System.Windows.Forms.LinkLabel();
			this.labelDistributionAllSelected = new System.Windows.Forms.Label();
			this.flowLayoutPanelDistributionApplications = new System.Windows.Forms.FlowLayoutPanel();
			this.panelDistributionGroupByAndFilter = new System.Windows.Forms.Panel();
			this.tableLayoutPanelDistributionFilter = new System.Windows.Forms.TableLayoutPanel();
			this.comboBoxDistributionMachineFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxDistributionApplicationFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxDistributionGroupFilter = new System.Windows.Forms.ComboBox();
			this.labelDistributionFilter = new System.Windows.Forms.Label();
			this.comboBoxDistributionGroupBy = new System.Windows.Forms.ComboBox();
			this.labelDistributionGroupBy = new System.Windows.Forms.Label();
			this.contextMenuStripSystemTray.SuspendLayout();
			this.tableLayoutPanelControlPanel.SuspendLayout();
			this.panelControlPanelApplications.SuspendLayout();
			this.panelControlPanelGroupByAndFilter.SuspendLayout();
			this.tableLayoutPanelControlPanelFilter.SuspendLayout();
			this.panelGlass.SuspendLayout();
			this.tabControlSection.SuspendLayout();
			this.tableLayoutPanelMain.SuspendLayout();
			this.horizontalPanel.SuspendLayout();
			this.panelTabPageArea.SuspendLayout();
			this.tableLayoutPanelDistribution.SuspendLayout();
			this.panelDistributionApplications.SuspendLayout();
			this.panelDistributionGroupByAndFilter.SuspendLayout();
			this.tableLayoutPanelDistributionFilter.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStripSystemTray;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Process Manager";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDown);
			this.notifyIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseUp);
			// 
			// contextMenuStripSystemTray
			// 
			this.contextMenuStripSystemTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSystemTrayConfiguration,
            this.toolStripSeparatorSystemTray1,
            this.toolStripMenuItemSystemTrayExit});
			this.contextMenuStripSystemTray.Name = "contextMenuStripSystemTray";
			this.contextMenuStripSystemTray.Size = new System.Drawing.Size(158, 54);
			// 
			// toolStripMenuItemSystemTrayConfiguration
			// 
			this.toolStripMenuItemSystemTrayConfiguration.Name = "toolStripMenuItemSystemTrayConfiguration";
			this.toolStripMenuItemSystemTrayConfiguration.Size = new System.Drawing.Size(157, 22);
			this.toolStripMenuItemSystemTrayConfiguration.Text = "Configuration...";
			this.toolStripMenuItemSystemTrayConfiguration.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayConfiguration_Click);
			// 
			// toolStripSeparatorSystemTray1
			// 
			this.toolStripSeparatorSystemTray1.Name = "toolStripSeparatorSystemTray1";
			this.toolStripSeparatorSystemTray1.Size = new System.Drawing.Size(154, 6);
			// 
			// toolStripMenuItemSystemTrayExit
			// 
			this.toolStripMenuItemSystemTrayExit.Name = "toolStripMenuItemSystemTrayExit";
			this.toolStripMenuItemSystemTrayExit.Size = new System.Drawing.Size(157, 22);
			this.toolStripMenuItemSystemTrayExit.Text = "Exit";
			this.toolStripMenuItemSystemTrayExit.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayExit_Click);
			// 
			// tableLayoutPanelControlPanel
			// 
			this.tableLayoutPanelControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelControlPanel.ColumnCount = 1;
			this.tableLayoutPanelControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelControlPanel.Controls.Add(this.panelControlPanelApplications, 0, 1);
			this.tableLayoutPanelControlPanel.Controls.Add(this.panelControlPanelGroupByAndFilter, 0, 0);
			this.tableLayoutPanelControlPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelControlPanel.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelControlPanel.Name = "tableLayoutPanelControlPanel";
			this.tableLayoutPanelControlPanel.RowCount = 2;
			this.tableLayoutPanelControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
			this.tableLayoutPanelControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelControlPanel.Size = new System.Drawing.Size(400, 235);
			this.tableLayoutPanelControlPanel.TabIndex = 2;
			this.tableLayoutPanelControlPanel.Visible = false;
			// 
			// panelControlPanelApplications
			// 
			this.panelControlPanelApplications.Controls.Add(this.labelControlPanelUnavailable);
			this.panelControlPanelApplications.Controls.Add(this.linkLabelControlPanelExpandAll);
			this.panelControlPanelApplications.Controls.Add(this.linkLabelControlPanelCollapseAll);
			this.panelControlPanelApplications.Controls.Add(this.linkLabelControlPanelStopAll);
			this.panelControlPanelApplications.Controls.Add(this.linkLabelControlPanelStartAll);
			this.panelControlPanelApplications.Controls.Add(this.linkLabelControlPanelRestartAll);
			this.panelControlPanelApplications.Controls.Add(this.labelControlPanelAllSelected);
			this.panelControlPanelApplications.Controls.Add(this.flowLayoutPanelControlPanelApplications);
			this.panelControlPanelApplications.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControlPanelApplications.Location = new System.Drawing.Point(0, 63);
			this.panelControlPanelApplications.Margin = new System.Windows.Forms.Padding(0);
			this.panelControlPanelApplications.Name = "panelControlPanelApplications";
			this.panelControlPanelApplications.Size = new System.Drawing.Size(400, 172);
			this.panelControlPanelApplications.TabIndex = 2;
			// 
			// labelControlPanelUnavailable
			// 
			this.labelControlPanelUnavailable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlPanelUnavailable.Location = new System.Drawing.Point(10, 10);
			this.labelControlPanelUnavailable.Margin = new System.Windows.Forms.Padding(10);
			this.labelControlPanelUnavailable.Name = "labelControlPanelUnavailable";
			this.labelControlPanelUnavailable.Size = new System.Drawing.Size(380, 152);
			this.labelControlPanelUnavailable.TabIndex = 0;
			this.labelControlPanelUnavailable.Text = "No applications available";
			this.labelControlPanelUnavailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// linkLabelControlPanelExpandAll
			// 
			this.linkLabelControlPanelExpandAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelExpandAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelControlPanelExpandAll.AutoSize = true;
			this.linkLabelControlPanelExpandAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelControlPanelExpandAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelControlPanelExpandAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelExpandAll.Location = new System.Drawing.Point(261, 10);
			this.linkLabelControlPanelExpandAll.Name = "linkLabelControlPanelExpandAll";
			this.linkLabelControlPanelExpandAll.Size = new System.Drawing.Size(61, 13);
			this.linkLabelControlPanelExpandAll.TabIndex = 16;
			this.linkLabelControlPanelExpandAll.TabStop = true;
			this.linkLabelControlPanelExpandAll.Text = "Expand All";
			this.linkLabelControlPanelExpandAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelControlPanelExpandAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelExpandAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelControlPanelExpandAll_LinkClicked);
			// 
			// linkLabelControlPanelCollapseAll
			// 
			this.linkLabelControlPanelCollapseAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelCollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelControlPanelCollapseAll.AutoSize = true;
			this.linkLabelControlPanelCollapseAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelControlPanelCollapseAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelControlPanelCollapseAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelCollapseAll.Location = new System.Drawing.Point(323, 10);
			this.linkLabelControlPanelCollapseAll.Name = "linkLabelControlPanelCollapseAll";
			this.linkLabelControlPanelCollapseAll.Size = new System.Drawing.Size(67, 13);
			this.linkLabelControlPanelCollapseAll.TabIndex = 15;
			this.linkLabelControlPanelCollapseAll.TabStop = true;
			this.linkLabelControlPanelCollapseAll.Text = "Collapse All";
			this.linkLabelControlPanelCollapseAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelControlPanelCollapseAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelCollapseAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelControlPanelCollapseAll_LinkClicked);
			// 
			// linkLabelControlPanelStopAll
			// 
			this.linkLabelControlPanelStopAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelStopAll.AutoSize = true;
			this.linkLabelControlPanelStopAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelControlPanelStopAll.Enabled = false;
			this.linkLabelControlPanelStopAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelControlPanelStopAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelStopAll.Location = new System.Drawing.Point(109, 10);
			this.linkLabelControlPanelStopAll.Name = "linkLabelControlPanelStopAll";
			this.linkLabelControlPanelStopAll.Size = new System.Drawing.Size(31, 13);
			this.linkLabelControlPanelStopAll.TabIndex = 13;
			this.linkLabelControlPanelStopAll.TabStop = true;
			this.linkLabelControlPanelStopAll.Text = "Stop";
			this.linkLabelControlPanelStopAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelControlPanelStopAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelStopAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelControlPanelStopAll_LinkClicked);
			// 
			// linkLabelControlPanelStartAll
			// 
			this.linkLabelControlPanelStartAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelStartAll.AutoSize = true;
			this.linkLabelControlPanelStartAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelControlPanelStartAll.Enabled = false;
			this.linkLabelControlPanelStartAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelControlPanelStartAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelStartAll.Location = new System.Drawing.Point(79, 10);
			this.linkLabelControlPanelStartAll.Name = "linkLabelControlPanelStartAll";
			this.linkLabelControlPanelStartAll.Size = new System.Drawing.Size(31, 13);
			this.linkLabelControlPanelStartAll.TabIndex = 12;
			this.linkLabelControlPanelStartAll.TabStop = true;
			this.linkLabelControlPanelStartAll.Text = "Start";
			this.linkLabelControlPanelStartAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelControlPanelStartAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelStartAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelControlPanelStartAll_LinkClicked);
			// 
			// linkLabelControlPanelRestartAll
			// 
			this.linkLabelControlPanelRestartAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelRestartAll.AutoSize = true;
			this.linkLabelControlPanelRestartAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelControlPanelRestartAll.Enabled = false;
			this.linkLabelControlPanelRestartAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelControlPanelRestartAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelRestartAll.Location = new System.Drawing.Point(139, 10);
			this.linkLabelControlPanelRestartAll.Name = "linkLabelControlPanelRestartAll";
			this.linkLabelControlPanelRestartAll.Size = new System.Drawing.Size(43, 13);
			this.linkLabelControlPanelRestartAll.TabIndex = 14;
			this.linkLabelControlPanelRestartAll.TabStop = true;
			this.linkLabelControlPanelRestartAll.Text = "Restart";
			this.linkLabelControlPanelRestartAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelControlPanelRestartAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelControlPanelRestartAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelControlPanelRestartAll_LinkClicked);
			// 
			// labelControlPanelAllSelected
			// 
			this.labelControlPanelAllSelected.AutoSize = true;
			this.labelControlPanelAllSelected.Location = new System.Drawing.Point(7, 10);
			this.labelControlPanelAllSelected.Name = "labelControlPanelAllSelected";
			this.labelControlPanelAllSelected.Size = new System.Drawing.Size(69, 13);
			this.labelControlPanelAllSelected.TabIndex = 2;
			this.labelControlPanelAllSelected.Text = "All Selected:";
			// 
			// flowLayoutPanelControlPanelApplications
			// 
			this.flowLayoutPanelControlPanelApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelControlPanelApplications.Location = new System.Drawing.Point(10, 30);
			this.flowLayoutPanelControlPanelApplications.Margin = new System.Windows.Forms.Padding(10);
			this.flowLayoutPanelControlPanelApplications.Name = "flowLayoutPanelControlPanelApplications";
			this.flowLayoutPanelControlPanelApplications.Size = new System.Drawing.Size(380, 132);
			this.flowLayoutPanelControlPanelApplications.TabIndex = 1;
			// 
			// panelControlPanelGroupByAndFilter
			// 
			this.panelControlPanelGroupByAndFilter.Controls.Add(this.tableLayoutPanelControlPanelFilter);
			this.panelControlPanelGroupByAndFilter.Controls.Add(this.labelControlPanelFilter);
			this.panelControlPanelGroupByAndFilter.Controls.Add(this.comboBoxControlPanelGroupBy);
			this.panelControlPanelGroupByAndFilter.Controls.Add(this.labelControlPanelGroupBy);
			this.panelControlPanelGroupByAndFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControlPanelGroupByAndFilter.Location = new System.Drawing.Point(0, 0);
			this.panelControlPanelGroupByAndFilter.Margin = new System.Windows.Forms.Padding(0);
			this.panelControlPanelGroupByAndFilter.Name = "panelControlPanelGroupByAndFilter";
			this.panelControlPanelGroupByAndFilter.Padding = new System.Windows.Forms.Padding(10);
			this.panelControlPanelGroupByAndFilter.Size = new System.Drawing.Size(400, 63);
			this.panelControlPanelGroupByAndFilter.TabIndex = 3;
			// 
			// tableLayoutPanelControlPanelFilter
			// 
			this.tableLayoutPanelControlPanelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelControlPanelFilter.ColumnCount = 5;
			this.tableLayoutPanelControlPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
			this.tableLayoutPanelControlPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelControlPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanelControlPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelControlPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanelControlPanelFilter.Controls.Add(this.comboBoxControlPanelMachineFilter, 0, 0);
			this.tableLayoutPanelControlPanelFilter.Controls.Add(this.comboBoxControlPanelApplicationFilter, 4, 0);
			this.tableLayoutPanelControlPanelFilter.Controls.Add(this.comboBoxControlPanelGroupFilter, 2, 0);
			this.tableLayoutPanelControlPanelFilter.Location = new System.Drawing.Point(71, 37);
			this.tableLayoutPanelControlPanelFilter.Name = "tableLayoutPanelControlPanelFilter";
			this.tableLayoutPanelControlPanelFilter.RowCount = 1;
			this.tableLayoutPanelControlPanelFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelControlPanelFilter.Size = new System.Drawing.Size(319, 21);
			this.tableLayoutPanelControlPanelFilter.TabIndex = 6;
			// 
			// comboBoxControlPanelMachineFilter
			// 
			this.comboBoxControlPanelMachineFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxControlPanelMachineFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxControlPanelMachineFilter.FormattingEnabled = true;
			this.comboBoxControlPanelMachineFilter.Location = new System.Drawing.Point(0, 0);
			this.comboBoxControlPanelMachineFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxControlPanelMachineFilter.Name = "comboBoxControlPanelMachineFilter";
			this.comboBoxControlPanelMachineFilter.Size = new System.Drawing.Size(102, 21);
			this.comboBoxControlPanelMachineFilter.TabIndex = 3;
			this.comboBoxControlPanelMachineFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxControlPanelMachineFilter_SelectedIndexChanged);
			// 
			// comboBoxControlPanelApplicationFilter
			// 
			this.comboBoxControlPanelApplicationFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxControlPanelApplicationFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxControlPanelApplicationFilter.FormattingEnabled = true;
			this.comboBoxControlPanelApplicationFilter.Location = new System.Drawing.Point(216, 0);
			this.comboBoxControlPanelApplicationFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxControlPanelApplicationFilter.Name = "comboBoxControlPanelApplicationFilter";
			this.comboBoxControlPanelApplicationFilter.Size = new System.Drawing.Size(103, 21);
			this.comboBoxControlPanelApplicationFilter.TabIndex = 5;
			this.comboBoxControlPanelApplicationFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxControlPanelApplicationFilter_SelectedIndexChanged);
			// 
			// comboBoxControlPanelGroupFilter
			// 
			this.comboBoxControlPanelGroupFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxControlPanelGroupFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxControlPanelGroupFilter.FormattingEnabled = true;
			this.comboBoxControlPanelGroupFilter.Location = new System.Drawing.Point(108, 0);
			this.comboBoxControlPanelGroupFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxControlPanelGroupFilter.Name = "comboBoxControlPanelGroupFilter";
			this.comboBoxControlPanelGroupFilter.Size = new System.Drawing.Size(102, 21);
			this.comboBoxControlPanelGroupFilter.TabIndex = 4;
			this.comboBoxControlPanelGroupFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxControlPanelGroupFilter_SelectedIndexChanged);
			// 
			// labelControlPanelFilter
			// 
			this.labelControlPanelFilter.AutoSize = true;
			this.labelControlPanelFilter.Location = new System.Drawing.Point(7, 40);
			this.labelControlPanelFilter.Name = "labelControlPanelFilter";
			this.labelControlPanelFilter.Size = new System.Drawing.Size(36, 13);
			this.labelControlPanelFilter.TabIndex = 2;
			this.labelControlPanelFilter.Text = "Filter:";
			// 
			// comboBoxControlPanelGroupBy
			// 
			this.comboBoxControlPanelGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxControlPanelGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxControlPanelGroupBy.FormattingEnabled = true;
			this.comboBoxControlPanelGroupBy.Location = new System.Drawing.Point(71, 10);
			this.comboBoxControlPanelGroupBy.Name = "comboBoxControlPanelGroupBy";
			this.comboBoxControlPanelGroupBy.Size = new System.Drawing.Size(319, 21);
			this.comboBoxControlPanelGroupBy.TabIndex = 1;
			this.comboBoxControlPanelGroupBy.SelectedIndexChanged += new System.EventHandler(this.ComboBoxControlPanelGroupBy_SelectedIndexChanged);
			// 
			// labelControlPanelGroupBy
			// 
			this.labelControlPanelGroupBy.AutoSize = true;
			this.labelControlPanelGroupBy.Location = new System.Drawing.Point(7, 13);
			this.labelControlPanelGroupBy.Name = "labelControlPanelGroupBy";
			this.labelControlPanelGroupBy.Size = new System.Drawing.Size(58, 13);
			this.labelControlPanelGroupBy.TabIndex = 0;
			this.labelControlPanelGroupBy.Text = "Group by:";
			// 
			// panelGlass
			// 
			this.panelGlass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelGlass.Controls.Add(this.tabControlSection);
			this.panelGlass.Location = new System.Drawing.Point(0, 0);
			this.panelGlass.Name = "panelGlass";
			this.panelGlass.Size = new System.Drawing.Size(416, 21);
			this.panelGlass.TabIndex = 3;
			// 
			// tabControlSection
			// 
			this.tabControlSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlSection.Controls.Add(this.tabPageControlPanel);
			this.tabControlSection.Controls.Add(this.tabPageDistribution);
			this.tabControlSection.Location = new System.Drawing.Point(3, 0);
			this.tabControlSection.Name = "tabControlSection";
			this.tabControlSection.SelectedIndex = 0;
			this.tabControlSection.Size = new System.Drawing.Size(410, 30);
			this.tabControlSection.TabIndex = 0;
			this.tabControlSection.SelectedIndexChanged += new System.EventHandler(this.TabControlSection_SelectedIndexChanged);
			// 
			// tabPageControlPanel
			// 
			this.tabPageControlPanel.Location = new System.Drawing.Point(4, 22);
			this.tabPageControlPanel.Name = "tabPageControlPanel";
			this.tabPageControlPanel.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageControlPanel.Size = new System.Drawing.Size(402, 4);
			this.tabPageControlPanel.TabIndex = 0;
			this.tabPageControlPanel.Text = "Control Panel";
			this.tabPageControlPanel.UseVisualStyleBackColor = true;
			// 
			// tabPageDistribution
			// 
			this.tabPageDistribution.Location = new System.Drawing.Point(4, 22);
			this.tabPageDistribution.Name = "tabPageDistribution";
			this.tabPageDistribution.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDistribution.Size = new System.Drawing.Size(402, 4);
			this.tabPageDistribution.TabIndex = 1;
			this.tabPageDistribution.Text = "Distribution";
			this.tabPageDistribution.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelMain
			// 
			this.tableLayoutPanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelMain.ColumnCount = 1;
			this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMain.Controls.Add(this.horizontalPanel, 0, 1);
			this.tableLayoutPanelMain.Controls.Add(this.panelTabPageArea, 0, 0);
			this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 22);
			this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
			this.tableLayoutPanelMain.RowCount = 2;
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
			this.tableLayoutPanelMain.Size = new System.Drawing.Size(400, 278);
			this.tableLayoutPanelMain.TabIndex = 4;
			// 
			// horizontalPanel
			// 
			this.horizontalPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalPanel.AutoModifyAddedControls = false;
			this.horizontalPanel.BackColor = System.Drawing.Color.Transparent;
			this.horizontalPanel.Controls.Add(this.linkLabelOpenConfiguration);
			this.horizontalPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.horizontalPanel.Location = new System.Drawing.Point(0, 235);
			this.horizontalPanel.Margin = new System.Windows.Forms.Padding(0);
			this.horizontalPanel.Name = "horizontalPanel";
			this.horizontalPanel.Size = new System.Drawing.Size(400, 43);
			this.horizontalPanel.TabIndex = 1;
			// 
			// linkLabelOpenConfiguration
			// 
			this.linkLabelOpenConfiguration.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelOpenConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabelOpenConfiguration.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkLabelOpenConfiguration.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelOpenConfiguration.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelOpenConfiguration.Location = new System.Drawing.Point(0, 0);
			this.linkLabelOpenConfiguration.Name = "linkLabelOpenConfiguration";
			this.linkLabelOpenConfiguration.Size = new System.Drawing.Size(400, 43);
			this.linkLabelOpenConfiguration.TabIndex = 0;
			this.linkLabelOpenConfiguration.TabStop = true;
			this.linkLabelOpenConfiguration.Text = "Open Configuration";
			this.linkLabelOpenConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabelOpenConfiguration.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelOpenConfiguration_LinkClicked);
			// 
			// panelTabPageArea
			// 
			this.panelTabPageArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelTabPageArea.Controls.Add(this.tableLayoutPanelDistribution);
			this.panelTabPageArea.Controls.Add(this.tableLayoutPanelControlPanel);
			this.panelTabPageArea.Location = new System.Drawing.Point(0, 0);
			this.panelTabPageArea.Margin = new System.Windows.Forms.Padding(0);
			this.panelTabPageArea.Name = "panelTabPageArea";
			this.panelTabPageArea.Size = new System.Drawing.Size(400, 235);
			this.panelTabPageArea.TabIndex = 2;
			// 
			// tableLayoutPanelDistribution
			// 
			this.tableLayoutPanelDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelDistribution.ColumnCount = 1;
			this.tableLayoutPanelDistribution.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelDistribution.Controls.Add(this.panelDistributionApplications, 0, 1);
			this.tableLayoutPanelDistribution.Controls.Add(this.panelDistributionGroupByAndFilter, 0, 0);
			this.tableLayoutPanelDistribution.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelDistribution.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelDistribution.Name = "tableLayoutPanelDistribution";
			this.tableLayoutPanelDistribution.RowCount = 2;
			this.tableLayoutPanelDistribution.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
			this.tableLayoutPanelDistribution.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelDistribution.Size = new System.Drawing.Size(400, 235);
			this.tableLayoutPanelDistribution.TabIndex = 3;
			this.tableLayoutPanelDistribution.Visible = false;
			// 
			// panelDistributionApplications
			// 
			this.panelDistributionApplications.Controls.Add(this.labelDistributionUnavailable);
			this.panelDistributionApplications.Controls.Add(this.linkLabelDistributionExpandAll);
			this.panelDistributionApplications.Controls.Add(this.linkLabelDistributionCollapseAll);
			this.panelDistributionApplications.Controls.Add(this.linkLabelDistributionStopAll);
			this.panelDistributionApplications.Controls.Add(this.linkLabelDistributionStartAll);
			this.panelDistributionApplications.Controls.Add(this.linkLabelDistributionRestartAll);
			this.panelDistributionApplications.Controls.Add(this.labelDistributionAllSelected);
			this.panelDistributionApplications.Controls.Add(this.flowLayoutPanelDistributionApplications);
			this.panelDistributionApplications.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelDistributionApplications.Location = new System.Drawing.Point(0, 63);
			this.panelDistributionApplications.Margin = new System.Windows.Forms.Padding(0);
			this.panelDistributionApplications.Name = "panelDistributionApplications";
			this.panelDistributionApplications.Size = new System.Drawing.Size(400, 172);
			this.panelDistributionApplications.TabIndex = 5;
			// 
			// labelDistributionUnavailable
			// 
			this.labelDistributionUnavailable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDistributionUnavailable.Location = new System.Drawing.Point(10, 10);
			this.labelDistributionUnavailable.Margin = new System.Windows.Forms.Padding(10);
			this.labelDistributionUnavailable.Name = "labelDistributionUnavailable";
			this.labelDistributionUnavailable.Size = new System.Drawing.Size(380, 152);
			this.labelDistributionUnavailable.TabIndex = 0;
			this.labelDistributionUnavailable.Text = "No applications available";
			this.labelDistributionUnavailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// linkLabelDistributionExpandAll
			// 
			this.linkLabelDistributionExpandAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionExpandAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelDistributionExpandAll.AutoSize = true;
			this.linkLabelDistributionExpandAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelDistributionExpandAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelDistributionExpandAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionExpandAll.Location = new System.Drawing.Point(261, 10);
			this.linkLabelDistributionExpandAll.Name = "linkLabelDistributionExpandAll";
			this.linkLabelDistributionExpandAll.Size = new System.Drawing.Size(61, 13);
			this.linkLabelDistributionExpandAll.TabIndex = 16;
			this.linkLabelDistributionExpandAll.TabStop = true;
			this.linkLabelDistributionExpandAll.Text = "Expand All";
			this.linkLabelDistributionExpandAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelDistributionExpandAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			// 
			// linkLabelDistributionCollapseAll
			// 
			this.linkLabelDistributionCollapseAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionCollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelDistributionCollapseAll.AutoSize = true;
			this.linkLabelDistributionCollapseAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelDistributionCollapseAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelDistributionCollapseAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionCollapseAll.Location = new System.Drawing.Point(323, 10);
			this.linkLabelDistributionCollapseAll.Name = "linkLabelDistributionCollapseAll";
			this.linkLabelDistributionCollapseAll.Size = new System.Drawing.Size(67, 13);
			this.linkLabelDistributionCollapseAll.TabIndex = 15;
			this.linkLabelDistributionCollapseAll.TabStop = true;
			this.linkLabelDistributionCollapseAll.Text = "Collapse All";
			this.linkLabelDistributionCollapseAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelDistributionCollapseAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			// 
			// linkLabelDistributionStopAll
			// 
			this.linkLabelDistributionStopAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionStopAll.AutoSize = true;
			this.linkLabelDistributionStopAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelDistributionStopAll.Enabled = false;
			this.linkLabelDistributionStopAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelDistributionStopAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionStopAll.Location = new System.Drawing.Point(109, 10);
			this.linkLabelDistributionStopAll.Name = "linkLabelDistributionStopAll";
			this.linkLabelDistributionStopAll.Size = new System.Drawing.Size(31, 13);
			this.linkLabelDistributionStopAll.TabIndex = 13;
			this.linkLabelDistributionStopAll.TabStop = true;
			this.linkLabelDistributionStopAll.Text = "Stop";
			this.linkLabelDistributionStopAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelDistributionStopAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			// 
			// linkLabelDistributionStartAll
			// 
			this.linkLabelDistributionStartAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionStartAll.AutoSize = true;
			this.linkLabelDistributionStartAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelDistributionStartAll.Enabled = false;
			this.linkLabelDistributionStartAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelDistributionStartAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionStartAll.Location = new System.Drawing.Point(79, 10);
			this.linkLabelDistributionStartAll.Name = "linkLabelDistributionStartAll";
			this.linkLabelDistributionStartAll.Size = new System.Drawing.Size(31, 13);
			this.linkLabelDistributionStartAll.TabIndex = 12;
			this.linkLabelDistributionStartAll.TabStop = true;
			this.linkLabelDistributionStartAll.Text = "Start";
			this.linkLabelDistributionStartAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelDistributionStartAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			// 
			// linkLabelDistributionRestartAll
			// 
			this.linkLabelDistributionRestartAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionRestartAll.AutoSize = true;
			this.linkLabelDistributionRestartAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelDistributionRestartAll.Enabled = false;
			this.linkLabelDistributionRestartAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelDistributionRestartAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionRestartAll.Location = new System.Drawing.Point(139, 10);
			this.linkLabelDistributionRestartAll.Name = "linkLabelDistributionRestartAll";
			this.linkLabelDistributionRestartAll.Size = new System.Drawing.Size(43, 13);
			this.linkLabelDistributionRestartAll.TabIndex = 14;
			this.linkLabelDistributionRestartAll.TabStop = true;
			this.linkLabelDistributionRestartAll.Text = "Restart";
			this.linkLabelDistributionRestartAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelDistributionRestartAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			// 
			// labelDistributionAllSelected
			// 
			this.labelDistributionAllSelected.AutoSize = true;
			this.labelDistributionAllSelected.Location = new System.Drawing.Point(7, 10);
			this.labelDistributionAllSelected.Name = "labelDistributionAllSelected";
			this.labelDistributionAllSelected.Size = new System.Drawing.Size(69, 13);
			this.labelDistributionAllSelected.TabIndex = 2;
			this.labelDistributionAllSelected.Text = "All Selected:";
			// 
			// flowLayoutPanelDistributionApplications
			// 
			this.flowLayoutPanelDistributionApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelDistributionApplications.Location = new System.Drawing.Point(10, 30);
			this.flowLayoutPanelDistributionApplications.Margin = new System.Windows.Forms.Padding(10);
			this.flowLayoutPanelDistributionApplications.Name = "flowLayoutPanelDistributionApplications";
			this.flowLayoutPanelDistributionApplications.Size = new System.Drawing.Size(380, 132);
			this.flowLayoutPanelDistributionApplications.TabIndex = 1;
			// 
			// panelDistributionGroupByAndFilter
			// 
			this.panelDistributionGroupByAndFilter.Controls.Add(this.tableLayoutPanelDistributionFilter);
			this.panelDistributionGroupByAndFilter.Controls.Add(this.labelDistributionFilter);
			this.panelDistributionGroupByAndFilter.Controls.Add(this.comboBoxDistributionGroupBy);
			this.panelDistributionGroupByAndFilter.Controls.Add(this.labelDistributionGroupBy);
			this.panelDistributionGroupByAndFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelDistributionGroupByAndFilter.Location = new System.Drawing.Point(0, 0);
			this.panelDistributionGroupByAndFilter.Margin = new System.Windows.Forms.Padding(0);
			this.panelDistributionGroupByAndFilter.Name = "panelDistributionGroupByAndFilter";
			this.panelDistributionGroupByAndFilter.Padding = new System.Windows.Forms.Padding(10);
			this.panelDistributionGroupByAndFilter.Size = new System.Drawing.Size(400, 63);
			this.panelDistributionGroupByAndFilter.TabIndex = 4;
			// 
			// tableLayoutPanelDistributionFilter
			// 
			this.tableLayoutPanelDistributionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelDistributionFilter.ColumnCount = 5;
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanelDistributionFilter.Controls.Add(this.comboBoxDistributionMachineFilter, 0, 0);
			this.tableLayoutPanelDistributionFilter.Controls.Add(this.comboBoxDistributionApplicationFilter, 4, 0);
			this.tableLayoutPanelDistributionFilter.Controls.Add(this.comboBoxDistributionGroupFilter, 2, 0);
			this.tableLayoutPanelDistributionFilter.Location = new System.Drawing.Point(71, 37);
			this.tableLayoutPanelDistributionFilter.Name = "tableLayoutPanelDistributionFilter";
			this.tableLayoutPanelDistributionFilter.RowCount = 1;
			this.tableLayoutPanelDistributionFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelDistributionFilter.Size = new System.Drawing.Size(319, 21);
			this.tableLayoutPanelDistributionFilter.TabIndex = 6;
			// 
			// comboBoxDistributionMachineFilter
			// 
			this.comboBoxDistributionMachineFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxDistributionMachineFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDistributionMachineFilter.FormattingEnabled = true;
			this.comboBoxDistributionMachineFilter.Location = new System.Drawing.Point(0, 0);
			this.comboBoxDistributionMachineFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxDistributionMachineFilter.Name = "comboBoxDistributionMachineFilter";
			this.comboBoxDistributionMachineFilter.Size = new System.Drawing.Size(102, 21);
			this.comboBoxDistributionMachineFilter.TabIndex = 3;
			// 
			// comboBoxDistributionApplicationFilter
			// 
			this.comboBoxDistributionApplicationFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxDistributionApplicationFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDistributionApplicationFilter.FormattingEnabled = true;
			this.comboBoxDistributionApplicationFilter.Location = new System.Drawing.Point(216, 0);
			this.comboBoxDistributionApplicationFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxDistributionApplicationFilter.Name = "comboBoxDistributionApplicationFilter";
			this.comboBoxDistributionApplicationFilter.Size = new System.Drawing.Size(103, 21);
			this.comboBoxDistributionApplicationFilter.TabIndex = 5;
			// 
			// comboBoxDistributionGroupFilter
			// 
			this.comboBoxDistributionGroupFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxDistributionGroupFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDistributionGroupFilter.FormattingEnabled = true;
			this.comboBoxDistributionGroupFilter.Location = new System.Drawing.Point(108, 0);
			this.comboBoxDistributionGroupFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxDistributionGroupFilter.Name = "comboBoxDistributionGroupFilter";
			this.comboBoxDistributionGroupFilter.Size = new System.Drawing.Size(102, 21);
			this.comboBoxDistributionGroupFilter.TabIndex = 4;
			// 
			// labelDistributionFilter
			// 
			this.labelDistributionFilter.AutoSize = true;
			this.labelDistributionFilter.Location = new System.Drawing.Point(7, 40);
			this.labelDistributionFilter.Name = "labelDistributionFilter";
			this.labelDistributionFilter.Size = new System.Drawing.Size(36, 13);
			this.labelDistributionFilter.TabIndex = 2;
			this.labelDistributionFilter.Text = "Filter:";
			// 
			// comboBoxDistributionGroupBy
			// 
			this.comboBoxDistributionGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxDistributionGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDistributionGroupBy.FormattingEnabled = true;
			this.comboBoxDistributionGroupBy.Location = new System.Drawing.Point(71, 10);
			this.comboBoxDistributionGroupBy.Name = "comboBoxDistributionGroupBy";
			this.comboBoxDistributionGroupBy.Size = new System.Drawing.Size(319, 21);
			this.comboBoxDistributionGroupBy.TabIndex = 1;
			// 
			// labelDistributionGroupBy
			// 
			this.labelDistributionGroupBy.AutoSize = true;
			this.labelDistributionGroupBy.Location = new System.Drawing.Point(7, 13);
			this.labelDistributionGroupBy.Name = "labelDistributionGroupBy";
			this.labelDistributionGroupBy.Size = new System.Drawing.Size(58, 13);
			this.labelDistributionGroupBy.TabIndex = 0;
			this.labelDistributionGroupBy.Text = "Group by:";
			// 
			// ControlPanelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(400, 300);
			this.ControlBox = false;
			this.Controls.Add(this.panelGlass);
			this.Controls.Add(this.tableLayoutPanelMain);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ControlPanelForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Deactivate += new System.EventHandler(this.ControlPanelForm_Deactivate);
			this.Load += new System.EventHandler(this.ControlPanelForm_Load);
			this.contextMenuStripSystemTray.ResumeLayout(false);
			this.tableLayoutPanelControlPanel.ResumeLayout(false);
			this.panelControlPanelApplications.ResumeLayout(false);
			this.panelControlPanelApplications.PerformLayout();
			this.panelControlPanelGroupByAndFilter.ResumeLayout(false);
			this.panelControlPanelGroupByAndFilter.PerformLayout();
			this.tableLayoutPanelControlPanelFilter.ResumeLayout(false);
			this.panelGlass.ResumeLayout(false);
			this.tabControlSection.ResumeLayout(false);
			this.tableLayoutPanelMain.ResumeLayout(false);
			this.horizontalPanel.ResumeLayout(false);
			this.panelTabPageArea.ResumeLayout(false);
			this.tableLayoutPanelDistribution.ResumeLayout(false);
			this.panelDistributionApplications.ResumeLayout(false);
			this.panelDistributionApplications.PerformLayout();
			this.panelDistributionGroupByAndFilter.ResumeLayout(false);
			this.panelDistributionGroupByAndFilter.PerformLayout();
			this.tableLayoutPanelDistributionFilter.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemTray;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayConfiguration;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSystemTray1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayExit;
		private Controls.HorizontalPanel horizontalPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelControlPanel;
		private System.Windows.Forms.LinkLabel linkLabelOpenConfiguration;
		private System.Windows.Forms.Panel panelControlPanelApplications;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelControlPanelApplications;
		private System.Windows.Forms.Label labelControlPanelUnavailable;
		private System.Windows.Forms.Panel panelControlPanelGroupByAndFilter;
		private System.Windows.Forms.ComboBox comboBoxControlPanelApplicationFilter;
		private System.Windows.Forms.ComboBox comboBoxControlPanelGroupFilter;
		private System.Windows.Forms.ComboBox comboBoxControlPanelMachineFilter;
		private System.Windows.Forms.Label labelControlPanelFilter;
		private System.Windows.Forms.ComboBox comboBoxControlPanelGroupBy;
		private System.Windows.Forms.Label labelControlPanelGroupBy;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelControlPanelFilter;
		private System.Windows.Forms.Label labelControlPanelAllSelected;
		private System.Windows.Forms.LinkLabel linkLabelControlPanelCollapseAll;
		private System.Windows.Forms.LinkLabel linkLabelControlPanelStopAll;
		private System.Windows.Forms.LinkLabel linkLabelControlPanelStartAll;
		private System.Windows.Forms.LinkLabel linkLabelControlPanelRestartAll;
		private System.Windows.Forms.LinkLabel linkLabelControlPanelExpandAll;
		private System.Windows.Forms.Panel panelGlass;
		private System.Windows.Forms.TabControl tabControlSection;
		private System.Windows.Forms.TabPage tabPageControlPanel;
		private System.Windows.Forms.TabPage tabPageDistribution;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
		private System.Windows.Forms.Panel panelTabPageArea;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDistribution;
		private System.Windows.Forms.Panel panelDistributionGroupByAndFilter;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDistributionFilter;
		private System.Windows.Forms.ComboBox comboBoxDistributionMachineFilter;
		private System.Windows.Forms.ComboBox comboBoxDistributionApplicationFilter;
		private System.Windows.Forms.ComboBox comboBoxDistributionGroupFilter;
		private System.Windows.Forms.Label labelDistributionFilter;
		private System.Windows.Forms.ComboBox comboBoxDistributionGroupBy;
		private System.Windows.Forms.Label labelDistributionGroupBy;
		private System.Windows.Forms.Panel panelDistributionApplications;
		private System.Windows.Forms.Label labelDistributionUnavailable;
		private System.Windows.Forms.LinkLabel linkLabelDistributionExpandAll;
		private System.Windows.Forms.LinkLabel linkLabelDistributionCollapseAll;
		private System.Windows.Forms.LinkLabel linkLabelDistributionStopAll;
		private System.Windows.Forms.LinkLabel linkLabelDistributionStartAll;
		private System.Windows.Forms.LinkLabel linkLabelDistributionRestartAll;
		private System.Windows.Forms.Label labelDistributionAllSelected;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDistributionApplications;
	}
}
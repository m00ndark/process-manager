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
			this.tableLayoutPanelProcess = new System.Windows.Forms.TableLayoutPanel();
			this.panelProcessApplications = new System.Windows.Forms.Panel();
			this.labelProcessUnavailable = new System.Windows.Forms.Label();
			this.linkLabelProcessExpandAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelProcessCollapseAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelProcessStopAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelProcessStartAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelProcessRestartAll = new System.Windows.Forms.LinkLabel();
			this.labelProcessAllSelected = new System.Windows.Forms.Label();
			this.flowLayoutPanelProcessApplications = new System.Windows.Forms.FlowLayoutPanel();
			this.panelProcessGroupByAndFilter = new System.Windows.Forms.Panel();
			this.tableLayoutPanelProcessFilter = new System.Windows.Forms.TableLayoutPanel();
			this.comboBoxProcessMachineFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxProcessApplicationFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxProcessGroupFilter = new System.Windows.Forms.ComboBox();
			this.labelProcessFilter = new System.Windows.Forms.Label();
			this.comboBoxProcessGroupBy = new System.Windows.Forms.ComboBox();
			this.labelProcessGroupBy = new System.Windows.Forms.Label();
			this.panelGlass = new System.Windows.Forms.Panel();
			this.tabControlSection = new System.Windows.Forms.TabControl();
			this.tabPageProcess = new System.Windows.Forms.TabPage();
			this.tabPageDistribution = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
			this.panelTabPageArea = new System.Windows.Forms.Panel();
			this.tableLayoutPanelDistribution = new System.Windows.Forms.TableLayoutPanel();
			this.panelDistributionApplications = new System.Windows.Forms.Panel();
			this.labelDistributionUnavailable = new System.Windows.Forms.Label();
			this.linkLabelDistributionExpandAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelDistributionCollapseAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelDistributionDistributeAll = new System.Windows.Forms.LinkLabel();
			this.labelDistributionAllSelected = new System.Windows.Forms.Label();
			this.flowLayoutPanelDistributionDestinations = new System.Windows.Forms.FlowLayoutPanel();
			this.panelDistributionGroupByAndFilter = new System.Windows.Forms.Panel();
			this.tableLayoutPanelDistributionFilter = new System.Windows.Forms.TableLayoutPanel();
			this.comboBoxDistributionMachineFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxDistributionApplicationFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxDistributionGroupFilter = new System.Windows.Forms.ComboBox();
			this.labelDistributionFilter = new System.Windows.Forms.Label();
			this.comboBoxDistributionGroupBy = new System.Windows.Forms.ComboBox();
			this.labelDistributionGroupBy = new System.Windows.Forms.Label();
			this.horizontalPanel = new ProcessManagerUI.Controls.HorizontalPanel();
			this.linkLabelOpenConfiguration = new System.Windows.Forms.LinkLabel();
			this.contextMenuStripSystemTray.SuspendLayout();
			this.tableLayoutPanelProcess.SuspendLayout();
			this.panelProcessApplications.SuspendLayout();
			this.panelProcessGroupByAndFilter.SuspendLayout();
			this.tableLayoutPanelProcessFilter.SuspendLayout();
			this.panelGlass.SuspendLayout();
			this.tabControlSection.SuspendLayout();
			this.tableLayoutPanelMain.SuspendLayout();
			this.panelTabPageArea.SuspendLayout();
			this.tableLayoutPanelDistribution.SuspendLayout();
			this.panelDistributionApplications.SuspendLayout();
			this.panelDistributionGroupByAndFilter.SuspendLayout();
			this.tableLayoutPanelDistributionFilter.SuspendLayout();
			this.horizontalPanel.SuspendLayout();
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
			// tableLayoutPanelProcess
			// 
			this.tableLayoutPanelProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelProcess.ColumnCount = 1;
			this.tableLayoutPanelProcess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelProcess.Controls.Add(this.panelProcessApplications, 0, 1);
			this.tableLayoutPanelProcess.Controls.Add(this.panelProcessGroupByAndFilter, 0, 0);
			this.tableLayoutPanelProcess.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelProcess.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelProcess.Name = "tableLayoutPanelProcess";
			this.tableLayoutPanelProcess.RowCount = 2;
			this.tableLayoutPanelProcess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
			this.tableLayoutPanelProcess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelProcess.Size = new System.Drawing.Size(400, 235);
			this.tableLayoutPanelProcess.TabIndex = 2;
			this.tableLayoutPanelProcess.Visible = false;
			// 
			// panelProcessApplications
			// 
			this.panelProcessApplications.Controls.Add(this.labelProcessUnavailable);
			this.panelProcessApplications.Controls.Add(this.linkLabelProcessExpandAll);
			this.panelProcessApplications.Controls.Add(this.linkLabelProcessCollapseAll);
			this.panelProcessApplications.Controls.Add(this.linkLabelProcessStopAll);
			this.panelProcessApplications.Controls.Add(this.linkLabelProcessStartAll);
			this.panelProcessApplications.Controls.Add(this.linkLabelProcessRestartAll);
			this.panelProcessApplications.Controls.Add(this.labelProcessAllSelected);
			this.panelProcessApplications.Controls.Add(this.flowLayoutPanelProcessApplications);
			this.panelProcessApplications.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelProcessApplications.Location = new System.Drawing.Point(0, 63);
			this.panelProcessApplications.Margin = new System.Windows.Forms.Padding(0);
			this.panelProcessApplications.Name = "panelProcessApplications";
			this.panelProcessApplications.Size = new System.Drawing.Size(400, 172);
			this.panelProcessApplications.TabIndex = 2;
			// 
			// labelProcessUnavailable
			// 
			this.labelProcessUnavailable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelProcessUnavailable.Location = new System.Drawing.Point(10, 10);
			this.labelProcessUnavailable.Margin = new System.Windows.Forms.Padding(10);
			this.labelProcessUnavailable.Name = "labelProcessUnavailable";
			this.labelProcessUnavailable.Size = new System.Drawing.Size(380, 152);
			this.labelProcessUnavailable.TabIndex = 0;
			this.labelProcessUnavailable.Text = "No applications available";
			this.labelProcessUnavailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// linkLabelProcessExpandAll
			// 
			this.linkLabelProcessExpandAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessExpandAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelProcessExpandAll.AutoSize = true;
			this.linkLabelProcessExpandAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelProcessExpandAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelProcessExpandAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessExpandAll.Location = new System.Drawing.Point(261, 10);
			this.linkLabelProcessExpandAll.Name = "linkLabelProcessExpandAll";
			this.linkLabelProcessExpandAll.Size = new System.Drawing.Size(61, 13);
			this.linkLabelProcessExpandAll.TabIndex = 16;
			this.linkLabelProcessExpandAll.TabStop = true;
			this.linkLabelProcessExpandAll.Text = "Expand All";
			this.linkLabelProcessExpandAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelProcessExpandAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessExpandAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelProcessExpandAll_LinkClicked);
			// 
			// linkLabelProcessCollapseAll
			// 
			this.linkLabelProcessCollapseAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessCollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelProcessCollapseAll.AutoSize = true;
			this.linkLabelProcessCollapseAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelProcessCollapseAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelProcessCollapseAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessCollapseAll.Location = new System.Drawing.Point(323, 10);
			this.linkLabelProcessCollapseAll.Name = "linkLabelProcessCollapseAll";
			this.linkLabelProcessCollapseAll.Size = new System.Drawing.Size(67, 13);
			this.linkLabelProcessCollapseAll.TabIndex = 15;
			this.linkLabelProcessCollapseAll.TabStop = true;
			this.linkLabelProcessCollapseAll.Text = "Collapse All";
			this.linkLabelProcessCollapseAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelProcessCollapseAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessCollapseAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelProcessCollapseAll_LinkClicked);
			// 
			// linkLabelProcessStopAll
			// 
			this.linkLabelProcessStopAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessStopAll.AutoSize = true;
			this.linkLabelProcessStopAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelProcessStopAll.Enabled = false;
			this.linkLabelProcessStopAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelProcessStopAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessStopAll.Location = new System.Drawing.Point(109, 10);
			this.linkLabelProcessStopAll.Name = "linkLabelProcessStopAll";
			this.linkLabelProcessStopAll.Size = new System.Drawing.Size(31, 13);
			this.linkLabelProcessStopAll.TabIndex = 13;
			this.linkLabelProcessStopAll.TabStop = true;
			this.linkLabelProcessStopAll.Text = "Stop";
			this.linkLabelProcessStopAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelProcessStopAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessStopAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelProcessStopAll_LinkClicked);
			// 
			// linkLabelProcessStartAll
			// 
			this.linkLabelProcessStartAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessStartAll.AutoSize = true;
			this.linkLabelProcessStartAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelProcessStartAll.Enabled = false;
			this.linkLabelProcessStartAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelProcessStartAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessStartAll.Location = new System.Drawing.Point(79, 10);
			this.linkLabelProcessStartAll.Name = "linkLabelProcessStartAll";
			this.linkLabelProcessStartAll.Size = new System.Drawing.Size(31, 13);
			this.linkLabelProcessStartAll.TabIndex = 12;
			this.linkLabelProcessStartAll.TabStop = true;
			this.linkLabelProcessStartAll.Text = "Start";
			this.linkLabelProcessStartAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelProcessStartAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessStartAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelProcessStartAll_LinkClicked);
			// 
			// linkLabelProcessRestartAll
			// 
			this.linkLabelProcessRestartAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessRestartAll.AutoSize = true;
			this.linkLabelProcessRestartAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelProcessRestartAll.Enabled = false;
			this.linkLabelProcessRestartAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelProcessRestartAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessRestartAll.Location = new System.Drawing.Point(139, 10);
			this.linkLabelProcessRestartAll.Name = "linkLabelProcessRestartAll";
			this.linkLabelProcessRestartAll.Size = new System.Drawing.Size(43, 13);
			this.linkLabelProcessRestartAll.TabIndex = 14;
			this.linkLabelProcessRestartAll.TabStop = true;
			this.linkLabelProcessRestartAll.Text = "Restart";
			this.linkLabelProcessRestartAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelProcessRestartAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelProcessRestartAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelProcessRestartAll_LinkClicked);
			// 
			// labelProcessAllSelected
			// 
			this.labelProcessAllSelected.AutoSize = true;
			this.labelProcessAllSelected.Location = new System.Drawing.Point(7, 10);
			this.labelProcessAllSelected.Name = "labelProcessAllSelected";
			this.labelProcessAllSelected.Size = new System.Drawing.Size(69, 13);
			this.labelProcessAllSelected.TabIndex = 2;
			this.labelProcessAllSelected.Text = "All Selected:";
			// 
			// flowLayoutPanelProcessApplications
			// 
			this.flowLayoutPanelProcessApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelProcessApplications.Location = new System.Drawing.Point(10, 30);
			this.flowLayoutPanelProcessApplications.Margin = new System.Windows.Forms.Padding(10);
			this.flowLayoutPanelProcessApplications.Name = "flowLayoutPanelProcessApplications";
			this.flowLayoutPanelProcessApplications.Size = new System.Drawing.Size(380, 132);
			this.flowLayoutPanelProcessApplications.TabIndex = 1;
			// 
			// panelProcessGroupByAndFilter
			// 
			this.panelProcessGroupByAndFilter.Controls.Add(this.tableLayoutPanelProcessFilter);
			this.panelProcessGroupByAndFilter.Controls.Add(this.labelProcessFilter);
			this.panelProcessGroupByAndFilter.Controls.Add(this.comboBoxProcessGroupBy);
			this.panelProcessGroupByAndFilter.Controls.Add(this.labelProcessGroupBy);
			this.panelProcessGroupByAndFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelProcessGroupByAndFilter.Location = new System.Drawing.Point(0, 0);
			this.panelProcessGroupByAndFilter.Margin = new System.Windows.Forms.Padding(0);
			this.panelProcessGroupByAndFilter.Name = "panelProcessGroupByAndFilter";
			this.panelProcessGroupByAndFilter.Padding = new System.Windows.Forms.Padding(10);
			this.panelProcessGroupByAndFilter.Size = new System.Drawing.Size(400, 63);
			this.panelProcessGroupByAndFilter.TabIndex = 3;
			// 
			// tableLayoutPanelProcessFilter
			// 
			this.tableLayoutPanelProcessFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelProcessFilter.ColumnCount = 5;
			this.tableLayoutPanelProcessFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
			this.tableLayoutPanelProcessFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelProcessFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanelProcessFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelProcessFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanelProcessFilter.Controls.Add(this.comboBoxProcessMachineFilter, 0, 0);
			this.tableLayoutPanelProcessFilter.Controls.Add(this.comboBoxProcessApplicationFilter, 4, 0);
			this.tableLayoutPanelProcessFilter.Controls.Add(this.comboBoxProcessGroupFilter, 2, 0);
			this.tableLayoutPanelProcessFilter.Location = new System.Drawing.Point(71, 37);
			this.tableLayoutPanelProcessFilter.Name = "tableLayoutPanelProcessFilter";
			this.tableLayoutPanelProcessFilter.RowCount = 1;
			this.tableLayoutPanelProcessFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelProcessFilter.Size = new System.Drawing.Size(319, 21);
			this.tableLayoutPanelProcessFilter.TabIndex = 6;
			// 
			// comboBoxProcessMachineFilter
			// 
			this.comboBoxProcessMachineFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxProcessMachineFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProcessMachineFilter.FormattingEnabled = true;
			this.comboBoxProcessMachineFilter.Location = new System.Drawing.Point(0, 0);
			this.comboBoxProcessMachineFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxProcessMachineFilter.Name = "comboBoxProcessMachineFilter";
			this.comboBoxProcessMachineFilter.Size = new System.Drawing.Size(102, 21);
			this.comboBoxProcessMachineFilter.TabIndex = 3;
			this.comboBoxProcessMachineFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProcessMachineFilter_SelectedIndexChanged);
			// 
			// comboBoxProcessApplicationFilter
			// 
			this.comboBoxProcessApplicationFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxProcessApplicationFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProcessApplicationFilter.FormattingEnabled = true;
			this.comboBoxProcessApplicationFilter.Location = new System.Drawing.Point(216, 0);
			this.comboBoxProcessApplicationFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxProcessApplicationFilter.Name = "comboBoxProcessApplicationFilter";
			this.comboBoxProcessApplicationFilter.Size = new System.Drawing.Size(103, 21);
			this.comboBoxProcessApplicationFilter.TabIndex = 5;
			this.comboBoxProcessApplicationFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProcessApplicationFilter_SelectedIndexChanged);
			// 
			// comboBoxProcessGroupFilter
			// 
			this.comboBoxProcessGroupFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxProcessGroupFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProcessGroupFilter.FormattingEnabled = true;
			this.comboBoxProcessGroupFilter.Location = new System.Drawing.Point(108, 0);
			this.comboBoxProcessGroupFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxProcessGroupFilter.Name = "comboBoxProcessGroupFilter";
			this.comboBoxProcessGroupFilter.Size = new System.Drawing.Size(102, 21);
			this.comboBoxProcessGroupFilter.TabIndex = 4;
			this.comboBoxProcessGroupFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProcessGroupFilter_SelectedIndexChanged);
			// 
			// labelProcessFilter
			// 
			this.labelProcessFilter.AutoSize = true;
			this.labelProcessFilter.Location = new System.Drawing.Point(7, 40);
			this.labelProcessFilter.Name = "labelProcessFilter";
			this.labelProcessFilter.Size = new System.Drawing.Size(36, 13);
			this.labelProcessFilter.TabIndex = 2;
			this.labelProcessFilter.Text = "Filter:";
			// 
			// comboBoxProcessGroupBy
			// 
			this.comboBoxProcessGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxProcessGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProcessGroupBy.FormattingEnabled = true;
			this.comboBoxProcessGroupBy.Location = new System.Drawing.Point(71, 10);
			this.comboBoxProcessGroupBy.Name = "comboBoxProcessGroupBy";
			this.comboBoxProcessGroupBy.Size = new System.Drawing.Size(319, 21);
			this.comboBoxProcessGroupBy.TabIndex = 1;
			this.comboBoxProcessGroupBy.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProcessGroupBy_SelectedIndexChanged);
			// 
			// labelProcessGroupBy
			// 
			this.labelProcessGroupBy.AutoSize = true;
			this.labelProcessGroupBy.Location = new System.Drawing.Point(7, 13);
			this.labelProcessGroupBy.Name = "labelProcessGroupBy";
			this.labelProcessGroupBy.Size = new System.Drawing.Size(58, 13);
			this.labelProcessGroupBy.TabIndex = 0;
			this.labelProcessGroupBy.Text = "Group by:";
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
			this.tabControlSection.Controls.Add(this.tabPageProcess);
			this.tabControlSection.Controls.Add(this.tabPageDistribution);
			this.tabControlSection.Location = new System.Drawing.Point(3, 0);
			this.tabControlSection.Name = "tabControlSection";
			this.tabControlSection.SelectedIndex = 0;
			this.tabControlSection.Size = new System.Drawing.Size(410, 30);
			this.tabControlSection.TabIndex = 0;
			this.tabControlSection.SelectedIndexChanged += new System.EventHandler(this.TabControlSection_SelectedIndexChanged);
			// 
			// tabPageProcess
			// 
			this.tabPageProcess.Location = new System.Drawing.Point(4, 22);
			this.tabPageProcess.Name = "tabPageProcess";
			this.tabPageProcess.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageProcess.Size = new System.Drawing.Size(402, 4);
			this.tabPageProcess.TabIndex = 0;
			this.tabPageProcess.Text = "Process";
			this.tabPageProcess.UseVisualStyleBackColor = true;
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
			// panelTabPageArea
			// 
			this.panelTabPageArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelTabPageArea.Controls.Add(this.tableLayoutPanelDistribution);
			this.panelTabPageArea.Controls.Add(this.tableLayoutPanelProcess);
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
			this.panelDistributionApplications.Controls.Add(this.linkLabelDistributionDistributeAll);
			this.panelDistributionApplications.Controls.Add(this.labelDistributionAllSelected);
			this.panelDistributionApplications.Controls.Add(this.flowLayoutPanelDistributionDestinations);
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
			// linkLabelDistributionDistributeAll
			// 
			this.linkLabelDistributionDistributeAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionDistributeAll.AutoSize = true;
			this.linkLabelDistributionDistributeAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelDistributionDistributeAll.Enabled = false;
			this.linkLabelDistributionDistributeAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelDistributionDistributeAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionDistributeAll.Location = new System.Drawing.Point(79, 10);
			this.linkLabelDistributionDistributeAll.Name = "linkLabelDistributionDistributeAll";
			this.linkLabelDistributionDistributeAll.Size = new System.Drawing.Size(58, 13);
			this.linkLabelDistributionDistributeAll.TabIndex = 12;
			this.linkLabelDistributionDistributeAll.TabStop = true;
			this.linkLabelDistributionDistributeAll.Text = "Distribute";
			this.linkLabelDistributionDistributeAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelDistributionDistributeAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
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
			// flowLayoutPanelDistributionDestinations
			// 
			this.flowLayoutPanelDistributionDestinations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelDistributionDestinations.Location = new System.Drawing.Point(10, 30);
			this.flowLayoutPanelDistributionDestinations.Margin = new System.Windows.Forms.Padding(10);
			this.flowLayoutPanelDistributionDestinations.Name = "flowLayoutPanelDistributionDestinations";
			this.flowLayoutPanelDistributionDestinations.Size = new System.Drawing.Size(380, 132);
			this.flowLayoutPanelDistributionDestinations.TabIndex = 1;
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
			this.tableLayoutPanelProcess.ResumeLayout(false);
			this.panelProcessApplications.ResumeLayout(false);
			this.panelProcessApplications.PerformLayout();
			this.panelProcessGroupByAndFilter.ResumeLayout(false);
			this.panelProcessGroupByAndFilter.PerformLayout();
			this.tableLayoutPanelProcessFilter.ResumeLayout(false);
			this.panelGlass.ResumeLayout(false);
			this.tabControlSection.ResumeLayout(false);
			this.tableLayoutPanelMain.ResumeLayout(false);
			this.panelTabPageArea.ResumeLayout(false);
			this.tableLayoutPanelDistribution.ResumeLayout(false);
			this.panelDistributionApplications.ResumeLayout(false);
			this.panelDistributionApplications.PerformLayout();
			this.panelDistributionGroupByAndFilter.ResumeLayout(false);
			this.panelDistributionGroupByAndFilter.PerformLayout();
			this.tableLayoutPanelDistributionFilter.ResumeLayout(false);
			this.horizontalPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemTray;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayConfiguration;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSystemTray1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayExit;
		private Controls.HorizontalPanel horizontalPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProcess;
		private System.Windows.Forms.LinkLabel linkLabelOpenConfiguration;
		private System.Windows.Forms.Panel panelProcessApplications;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProcessApplications;
		private System.Windows.Forms.Label labelProcessUnavailable;
		private System.Windows.Forms.Panel panelProcessGroupByAndFilter;
		private System.Windows.Forms.ComboBox comboBoxProcessApplicationFilter;
		private System.Windows.Forms.ComboBox comboBoxProcessGroupFilter;
		private System.Windows.Forms.ComboBox comboBoxProcessMachineFilter;
		private System.Windows.Forms.Label labelProcessFilter;
		private System.Windows.Forms.ComboBox comboBoxProcessGroupBy;
		private System.Windows.Forms.Label labelProcessGroupBy;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProcessFilter;
		private System.Windows.Forms.Label labelProcessAllSelected;
		private System.Windows.Forms.LinkLabel linkLabelProcessCollapseAll;
		private System.Windows.Forms.LinkLabel linkLabelProcessStopAll;
		private System.Windows.Forms.LinkLabel linkLabelProcessStartAll;
		private System.Windows.Forms.LinkLabel linkLabelProcessRestartAll;
		private System.Windows.Forms.LinkLabel linkLabelProcessExpandAll;
		private System.Windows.Forms.Panel panelGlass;
		private System.Windows.Forms.TabControl tabControlSection;
		private System.Windows.Forms.TabPage tabPageProcess;
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
		private System.Windows.Forms.LinkLabel linkLabelDistributionDistributeAll;
		private System.Windows.Forms.Label labelDistributionAllSelected;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDistributionDestinations;
	}
}
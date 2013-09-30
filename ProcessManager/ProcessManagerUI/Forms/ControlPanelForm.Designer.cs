using System.Drawing;

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
			this.toolStripMenuItemSystemTrayOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSystemTrayOptionsStartWithWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSystemTrayOptionsUserOwnsControlPanel = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevel = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelError = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug = new System.Windows.Forms.ToolStripMenuItem();
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
			this.panelScrollProcessApplications = new System.Windows.Forms.Panel();
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
			this.panelGlassTop = new System.Windows.Forms.Panel();
			this.pictureBoxClose = new System.Windows.Forms.PictureBox();
			this.tabControlSection = new System.Windows.Forms.TabControl();
			this.tabPageProcess = new System.Windows.Forms.TabPage();
			this.tabPageDistribution = new System.Windows.Forms.TabPage();
			this.tabPageMacro = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
			this.horizontalPanel = new ProcessManagerUI.Controls.HorizontalPanel();
			this.linkLabelOpenConfiguration = new System.Windows.Forms.LinkLabel();
			this.panelTabPageArea = new System.Windows.Forms.Panel();
			this.tableLayoutPanelMacro = new System.Windows.Forms.TableLayoutPanel();
			this.panelMacros = new System.Windows.Forms.Panel();
			this.labelMacroUnavailable = new System.Windows.Forms.Label();
			this.linkLabelMacroExpandAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelMacroCollapseAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelMacroPlayAll = new System.Windows.Forms.LinkLabel();
			this.labelMacroAllSelected = new System.Windows.Forms.Label();
			this.panelScrollMacros = new System.Windows.Forms.Panel();
			this.flowLayoutPanelMacros = new System.Windows.Forms.FlowLayoutPanel();
			this.tableLayoutPanelDistribution = new System.Windows.Forms.TableLayoutPanel();
			this.panelDistributionDestinations = new System.Windows.Forms.Panel();
			this.labelDistributionUnavailable = new System.Windows.Forms.Label();
			this.linkLabelDistributionExpandAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelDistributionCollapseAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelDistributionDistributeAll = new System.Windows.Forms.LinkLabel();
			this.labelDistributionAllSelected = new System.Windows.Forms.Label();
			this.panelScrollDistributionDestinations = new System.Windows.Forms.Panel();
			this.flowLayoutPanelDistributionDestinations = new System.Windows.Forms.FlowLayoutPanel();
			this.panelDistributionGroupByAndFilter = new System.Windows.Forms.Panel();
			this.tableLayoutPanelDistributionFilter = new System.Windows.Forms.TableLayoutPanel();
			this.comboBoxDistributionDestinationMachineFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxDistributionSourceMachineFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxDistributionApplicationFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxDistributionGroupFilter = new System.Windows.Forms.ComboBox();
			this.labelDistributionFilter = new System.Windows.Forms.Label();
			this.comboBoxDistributionGroupBy = new System.Windows.Forms.ComboBox();
			this.labelDistributionGroupBy = new System.Windows.Forms.Label();
			this.contextMenuStripSystemTray.SuspendLayout();
			this.tableLayoutPanelProcess.SuspendLayout();
			this.panelProcessApplications.SuspendLayout();
			this.panelScrollProcessApplications.SuspendLayout();
			this.panelProcessGroupByAndFilter.SuspendLayout();
			this.tableLayoutPanelProcessFilter.SuspendLayout();
			this.panelGlass.SuspendLayout();
			this.panelGlassTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
			this.tabControlSection.SuspendLayout();
			this.tableLayoutPanelMain.SuspendLayout();
			this.horizontalPanel.SuspendLayout();
			this.panelTabPageArea.SuspendLayout();
			this.tableLayoutPanelMacro.SuspendLayout();
			this.panelMacros.SuspendLayout();
			this.panelScrollMacros.SuspendLayout();
			this.tableLayoutPanelDistribution.SuspendLayout();
			this.panelDistributionDestinations.SuspendLayout();
			this.panelScrollDistributionDestinations.SuspendLayout();
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
            this.toolStripMenuItemSystemTrayOptions,
            this.toolStripSeparatorSystemTray1,
            this.toolStripMenuItemSystemTrayExit});
			this.contextMenuStripSystemTray.Name = "contextMenuStripSystemTray";
			this.contextMenuStripSystemTray.Size = new System.Drawing.Size(158, 76);
			// 
			// toolStripMenuItemSystemTrayConfiguration
			// 
			this.toolStripMenuItemSystemTrayConfiguration.Name = "toolStripMenuItemSystemTrayConfiguration";
			this.toolStripMenuItemSystemTrayConfiguration.Size = new System.Drawing.Size(157, 22);
			this.toolStripMenuItemSystemTrayConfiguration.Text = "Configuration...";
			this.toolStripMenuItemSystemTrayConfiguration.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayConfiguration_Click);
			// 
			// toolStripMenuItemSystemTrayOptions
			// 
			this.toolStripMenuItemSystemTrayOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSystemTrayOptionsStartWithWindows,
            this.toolStripMenuItemSystemTrayOptionsUserOwnsControlPanel,
            this.toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost,
            this.toolStripMenuItemSystemTrayOptionsMinimumLogLevel});
			this.toolStripMenuItemSystemTrayOptions.Name = "toolStripMenuItemSystemTrayOptions";
			this.toolStripMenuItemSystemTrayOptions.Size = new System.Drawing.Size(157, 22);
			this.toolStripMenuItemSystemTrayOptions.Text = "Options";
			// 
			// toolStripMenuItemSystemTrayOptionsStartWithWindows
			// 
			this.toolStripMenuItemSystemTrayOptionsStartWithWindows.Name = "toolStripMenuItemSystemTrayOptionsStartWithWindows";
			this.toolStripMenuItemSystemTrayOptionsStartWithWindows.Size = new System.Drawing.Size(229, 22);
			this.toolStripMenuItemSystemTrayOptionsStartWithWindows.Text = "Start With Windows";
			this.toolStripMenuItemSystemTrayOptionsStartWithWindows.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayOptionsStartWithWindows_Click);
			// 
			// toolStripMenuItemSystemTrayOptionsUserOwnsControlPanel
			// 
			this.toolStripMenuItemSystemTrayOptionsUserOwnsControlPanel.Name = "toolStripMenuItemSystemTrayOptionsUserOwnsControlPanel";
			this.toolStripMenuItemSystemTrayOptionsUserOwnsControlPanel.Size = new System.Drawing.Size(229, 22);
			this.toolStripMenuItemSystemTrayOptionsUserOwnsControlPanel.Text = "User Owns Control Panel";
			this.toolStripMenuItemSystemTrayOptionsUserOwnsControlPanel.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayOptionsUserOwnsControlPanel_Click);
			// 
			// toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost
			// 
			this.toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost.Name = "toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost";
			this.toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost.Size = new System.Drawing.Size(229, 22);
			this.toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost.Text = "Keep Control Panel Top Most";
			this.toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost_Click);
			// 
			// toolStripMenuItemSystemTrayOptionsMinimumLogLevel
			// 
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelError,
            this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning,
            this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow,
            this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose,
            this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug});
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevel.Name = "toolStripMenuItemSystemTrayOptionsMinimumLogLevel";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevel.Size = new System.Drawing.Size(229, 22);
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevel.Text = "Minimum Log Level";
			// 
			// toolStripMenuItemSystemTrayOptionsMinimumLogLevelError
			// 
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelError.Name = "toolStripMenuItemSystemTrayOptionsMinimumLogLevelError";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelError.Size = new System.Drawing.Size(119, 22);
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelError.Text = "Error";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelError.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayOptionsMinimumLogLevelError_Click);
			// 
			// toolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning
			// 
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning.Name = "toolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning.Size = new System.Drawing.Size(119, 22);
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning.Text = "Warning";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning_Click);
			// 
			// toolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow
			// 
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow.Name = "toolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow.Size = new System.Drawing.Size(119, 22);
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow.Text = "Flow";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow_Click);
			// 
			// toolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose
			// 
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose.Name = "toolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose.Size = new System.Drawing.Size(119, 22);
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose.Text = "Verbose";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose_Click);
			// 
			// toolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug
			// 
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug.Name = "toolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug.Size = new System.Drawing.Size(119, 22);
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug.Text = "Debug";
			this.toolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug.Click += new System.EventHandler(this.ToolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug_Click);
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
			this.tableLayoutPanelProcess.Size = new System.Drawing.Size(449, 235);
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
			this.panelProcessApplications.Controls.Add(this.panelScrollProcessApplications);
			this.panelProcessApplications.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelProcessApplications.Location = new System.Drawing.Point(0, 63);
			this.panelProcessApplications.Margin = new System.Windows.Forms.Padding(0);
			this.panelProcessApplications.Name = "panelProcessApplications";
			this.panelProcessApplications.Size = new System.Drawing.Size(449, 172);
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
			this.labelProcessUnavailable.Size = new System.Drawing.Size(429, 152);
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
			this.linkLabelProcessExpandAll.Location = new System.Drawing.Point(310, 10);
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
			this.linkLabelProcessCollapseAll.Location = new System.Drawing.Point(372, 10);
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
			// panelScrollProcessApplications
			// 
			this.panelScrollProcessApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelScrollProcessApplications.AutoScroll = true;
			this.panelScrollProcessApplications.Controls.Add(this.flowLayoutPanelProcessApplications);
			this.panelScrollProcessApplications.Location = new System.Drawing.Point(10, 30);
			this.panelScrollProcessApplications.Margin = new System.Windows.Forms.Padding(10);
			this.panelScrollProcessApplications.Name = "panelScrollProcessApplications";
			this.panelScrollProcessApplications.Size = new System.Drawing.Size(429, 132);
			this.panelScrollProcessApplications.TabIndex = 1;
			// 
			// flowLayoutPanelProcessApplications
			// 
			this.flowLayoutPanelProcessApplications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelProcessApplications.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanelProcessApplications.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanelProcessApplications.Name = "flowLayoutPanelProcessApplications";
			this.flowLayoutPanelProcessApplications.Size = new System.Drawing.Size(429, 132);
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
			this.panelProcessGroupByAndFilter.Size = new System.Drawing.Size(449, 63);
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
			this.tableLayoutPanelProcessFilter.Size = new System.Drawing.Size(368, 21);
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
			this.comboBoxProcessMachineFilter.Size = new System.Drawing.Size(118, 21);
			this.comboBoxProcessMachineFilter.TabIndex = 3;
			this.comboBoxProcessMachineFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProcessMachineFilter_SelectedIndexChanged);
			// 
			// comboBoxProcessApplicationFilter
			// 
			this.comboBoxProcessApplicationFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxProcessApplicationFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProcessApplicationFilter.FormattingEnabled = true;
			this.comboBoxProcessApplicationFilter.Location = new System.Drawing.Point(248, 0);
			this.comboBoxProcessApplicationFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxProcessApplicationFilter.Name = "comboBoxProcessApplicationFilter";
			this.comboBoxProcessApplicationFilter.Size = new System.Drawing.Size(120, 21);
			this.comboBoxProcessApplicationFilter.TabIndex = 5;
			this.comboBoxProcessApplicationFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProcessApplicationFilter_SelectedIndexChanged);
			// 
			// comboBoxProcessGroupFilter
			// 
			this.comboBoxProcessGroupFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxProcessGroupFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProcessGroupFilter.FormattingEnabled = true;
			this.comboBoxProcessGroupFilter.Location = new System.Drawing.Point(124, 0);
			this.comboBoxProcessGroupFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxProcessGroupFilter.Name = "comboBoxProcessGroupFilter";
			this.comboBoxProcessGroupFilter.Size = new System.Drawing.Size(118, 21);
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
			this.comboBoxProcessGroupBy.Size = new System.Drawing.Size(368, 21);
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
			this.panelGlass.Controls.Add(this.panelGlassTop);
			this.panelGlass.Controls.Add(this.tabControlSection);
			this.panelGlass.Location = new System.Drawing.Point(0, 0);
			this.panelGlass.Name = "panelGlass";
			this.panelGlass.Size = new System.Drawing.Size(465, 21);
			this.panelGlass.TabIndex = 3;
			// 
			// panelGlassTop
			// 
			this.panelGlassTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelGlassTop.Controls.Add(this.pictureBoxClose);
			this.panelGlassTop.Location = new System.Drawing.Point(179, 0);
			this.panelGlassTop.Name = "panelGlassTop";
			this.panelGlassTop.Size = new System.Drawing.Size(289, 21);
			this.panelGlassTop.TabIndex = 0;
			this.panelGlassTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelGlassTop_MouseDown);
			// 
			// pictureBoxClose
			// 
			this.pictureBoxClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxClose.Image = global::ProcessManagerUI.Properties.Resources.close_16_14;
			this.pictureBoxClose.Location = new System.Drawing.Point(253, 0);
			this.pictureBoxClose.Name = "pictureBoxClose";
			this.pictureBoxClose.Size = new System.Drawing.Size(16, 14);
			this.pictureBoxClose.TabIndex = 0;
			this.pictureBoxClose.TabStop = false;
			this.pictureBoxClose.Visible = false;
			this.pictureBoxClose.Click += new System.EventHandler(this.PictureBoxClose_Click);
			this.pictureBoxClose.MouseEnter += new System.EventHandler(this.PictureBoxClose_MouseEnter);
			this.pictureBoxClose.MouseLeave += new System.EventHandler(this.PictureBoxClose_MouseLeave);
			// 
			// tabControlSection
			// 
			this.tabControlSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlSection.Controls.Add(this.tabPageProcess);
			this.tabControlSection.Controls.Add(this.tabPageDistribution);
			this.tabControlSection.Controls.Add(this.tabPageMacro);
			this.tabControlSection.Location = new System.Drawing.Point(3, 0);
			this.tabControlSection.Name = "tabControlSection";
			this.tabControlSection.SelectedIndex = 0;
			this.tabControlSection.Size = new System.Drawing.Size(459, 30);
			this.tabControlSection.TabIndex = 0;
			this.tabControlSection.SelectedIndexChanged += new System.EventHandler(this.TabControlSection_SelectedIndexChanged);
			// 
			// tabPageProcess
			// 
			this.tabPageProcess.Location = new System.Drawing.Point(4, 22);
			this.tabPageProcess.Name = "tabPageProcess";
			this.tabPageProcess.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageProcess.Size = new System.Drawing.Size(451, 4);
			this.tabPageProcess.TabIndex = 0;
			this.tabPageProcess.Text = "Process";
			this.tabPageProcess.UseVisualStyleBackColor = true;
			// 
			// tabPageDistribution
			// 
			this.tabPageDistribution.Location = new System.Drawing.Point(4, 22);
			this.tabPageDistribution.Name = "tabPageDistribution";
			this.tabPageDistribution.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDistribution.Size = new System.Drawing.Size(451, 4);
			this.tabPageDistribution.TabIndex = 1;
			this.tabPageDistribution.Text = "Distribution";
			this.tabPageDistribution.UseVisualStyleBackColor = true;
			// 
			// tabPageMacro
			// 
			this.tabPageMacro.Location = new System.Drawing.Point(4, 22);
			this.tabPageMacro.Name = "tabPageMacro";
			this.tabPageMacro.Size = new System.Drawing.Size(451, 4);
			this.tabPageMacro.TabIndex = 2;
			this.tabPageMacro.Text = "Macro";
			this.tabPageMacro.UseVisualStyleBackColor = true;
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
			this.tableLayoutPanelMain.Size = new System.Drawing.Size(449, 278);
			this.tableLayoutPanelMain.TabIndex = 4;
			// 
			// horizontalPanel
			// 
			this.horizontalPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalPanel.AutoModifyAddedControls = false;
			this.horizontalPanel.BackColor = System.Drawing.Color.Transparent;
			this.horizontalPanel.Controls.Add(this.linkLabelOpenConfiguration);
			this.horizontalPanel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.horizontalPanel.Location = new System.Drawing.Point(0, 235);
			this.horizontalPanel.Margin = new System.Windows.Forms.Padding(0);
			this.horizontalPanel.Name = "horizontalPanel";
			this.horizontalPanel.Size = new System.Drawing.Size(449, 43);
			this.horizontalPanel.TabIndex = 1;
			// 
			// linkLabelOpenConfiguration
			// 
			this.linkLabelOpenConfiguration.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelOpenConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabelOpenConfiguration.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.linkLabelOpenConfiguration.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelOpenConfiguration.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelOpenConfiguration.Location = new System.Drawing.Point(0, 0);
			this.linkLabelOpenConfiguration.Name = "linkLabelOpenConfiguration";
			this.linkLabelOpenConfiguration.Size = new System.Drawing.Size(449, 43);
			this.linkLabelOpenConfiguration.TabIndex = 0;
			this.linkLabelOpenConfiguration.TabStop = true;
			this.linkLabelOpenConfiguration.Text = "Open Configuration";
			this.linkLabelOpenConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabelOpenConfiguration.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelOpenConfiguration.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelOpenConfiguration_LinkClicked);
			// 
			// panelTabPageArea
			// 
			this.panelTabPageArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelTabPageArea.Controls.Add(this.tableLayoutPanelMacro);
			this.panelTabPageArea.Controls.Add(this.tableLayoutPanelDistribution);
			this.panelTabPageArea.Controls.Add(this.tableLayoutPanelProcess);
			this.panelTabPageArea.Location = new System.Drawing.Point(0, 0);
			this.panelTabPageArea.Margin = new System.Windows.Forms.Padding(0);
			this.panelTabPageArea.Name = "panelTabPageArea";
			this.panelTabPageArea.Size = new System.Drawing.Size(449, 235);
			this.panelTabPageArea.TabIndex = 2;
			// 
			// tableLayoutPanelMacro
			// 
			this.tableLayoutPanelMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelMacro.ColumnCount = 1;
			this.tableLayoutPanelMacro.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMacro.Controls.Add(this.panelMacros, 0, 0);
			this.tableLayoutPanelMacro.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelMacro.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelMacro.Name = "tableLayoutPanelMacro";
			this.tableLayoutPanelMacro.RowCount = 1;
			this.tableLayoutPanelMacro.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMacro.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 235F));
			this.tableLayoutPanelMacro.Size = new System.Drawing.Size(449, 235);
			this.tableLayoutPanelMacro.TabIndex = 4;
			this.tableLayoutPanelMacro.Visible = false;
			// 
			// panelMacros
			// 
			this.panelMacros.Controls.Add(this.labelMacroUnavailable);
			this.panelMacros.Controls.Add(this.linkLabelMacroExpandAll);
			this.panelMacros.Controls.Add(this.linkLabelMacroCollapseAll);
			this.panelMacros.Controls.Add(this.linkLabelMacroPlayAll);
			this.panelMacros.Controls.Add(this.labelMacroAllSelected);
			this.panelMacros.Controls.Add(this.panelScrollMacros);
			this.panelMacros.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMacros.Location = new System.Drawing.Point(0, 0);
			this.panelMacros.Margin = new System.Windows.Forms.Padding(0);
			this.panelMacros.Name = "panelMacros";
			this.panelMacros.Size = new System.Drawing.Size(449, 235);
			this.panelMacros.TabIndex = 2;
			// 
			// labelMacroUnavailable
			// 
			this.labelMacroUnavailable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelMacroUnavailable.Location = new System.Drawing.Point(10, 10);
			this.labelMacroUnavailable.Margin = new System.Windows.Forms.Padding(10);
			this.labelMacroUnavailable.Name = "labelMacroUnavailable";
			this.labelMacroUnavailable.Size = new System.Drawing.Size(429, 215);
			this.labelMacroUnavailable.TabIndex = 0;
			this.labelMacroUnavailable.Text = "No macros available";
			this.labelMacroUnavailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// linkLabelMacroExpandAll
			// 
			this.linkLabelMacroExpandAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelMacroExpandAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelMacroExpandAll.AutoSize = true;
			this.linkLabelMacroExpandAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelMacroExpandAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelMacroExpandAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelMacroExpandAll.Location = new System.Drawing.Point(310, 10);
			this.linkLabelMacroExpandAll.Name = "linkLabelMacroExpandAll";
			this.linkLabelMacroExpandAll.Size = new System.Drawing.Size(61, 13);
			this.linkLabelMacroExpandAll.TabIndex = 20;
			this.linkLabelMacroExpandAll.TabStop = true;
			this.linkLabelMacroExpandAll.Text = "Expand All";
			this.linkLabelMacroExpandAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelMacroExpandAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelMacroExpandAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelMacroExpandAll_LinkClicked);
			// 
			// linkLabelMacroCollapseAll
			// 
			this.linkLabelMacroCollapseAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelMacroCollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelMacroCollapseAll.AutoSize = true;
			this.linkLabelMacroCollapseAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelMacroCollapseAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelMacroCollapseAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelMacroCollapseAll.Location = new System.Drawing.Point(372, 10);
			this.linkLabelMacroCollapseAll.Name = "linkLabelMacroCollapseAll";
			this.linkLabelMacroCollapseAll.Size = new System.Drawing.Size(67, 13);
			this.linkLabelMacroCollapseAll.TabIndex = 19;
			this.linkLabelMacroCollapseAll.TabStop = true;
			this.linkLabelMacroCollapseAll.Text = "Collapse All";
			this.linkLabelMacroCollapseAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelMacroCollapseAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelMacroCollapseAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelMacroCollapseAll_LinkClicked);
			// 
			// linkLabelMacroPlayAll
			// 
			this.linkLabelMacroPlayAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelMacroPlayAll.AutoSize = true;
			this.linkLabelMacroPlayAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelMacroPlayAll.Enabled = false;
			this.linkLabelMacroPlayAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelMacroPlayAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelMacroPlayAll.Location = new System.Drawing.Point(79, 10);
			this.linkLabelMacroPlayAll.Name = "linkLabelMacroPlayAll";
			this.linkLabelMacroPlayAll.Size = new System.Drawing.Size(27, 13);
			this.linkLabelMacroPlayAll.TabIndex = 18;
			this.linkLabelMacroPlayAll.TabStop = true;
			this.linkLabelMacroPlayAll.Text = "Play";
			this.linkLabelMacroPlayAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelMacroPlayAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelMacroPlayAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelMacroPlayAll_LinkClicked);
			// 
			// labelMacroAllSelected
			// 
			this.labelMacroAllSelected.AutoSize = true;
			this.labelMacroAllSelected.Location = new System.Drawing.Point(7, 10);
			this.labelMacroAllSelected.Name = "labelMacroAllSelected";
			this.labelMacroAllSelected.Size = new System.Drawing.Size(69, 13);
			this.labelMacroAllSelected.TabIndex = 17;
			this.labelMacroAllSelected.Text = "All Selected:";
			// 
			// panelScrollMacros
			// 
			this.panelScrollMacros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelScrollMacros.AutoScroll = true;
			this.panelScrollMacros.Controls.Add(this.flowLayoutPanelMacros);
			this.panelScrollMacros.Location = new System.Drawing.Point(10, 30);
			this.panelScrollMacros.Margin = new System.Windows.Forms.Padding(10);
			this.panelScrollMacros.Name = "panelScrollMacros";
			this.panelScrollMacros.Size = new System.Drawing.Size(429, 195);
			this.panelScrollMacros.TabIndex = 1;
			// 
			// flowLayoutPanelMacros
			// 
			this.flowLayoutPanelMacros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelMacros.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanelMacros.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanelMacros.Name = "flowLayoutPanelMacros";
			this.flowLayoutPanelMacros.Size = new System.Drawing.Size(429, 195);
			this.flowLayoutPanelMacros.TabIndex = 1;
			// 
			// tableLayoutPanelDistribution
			// 
			this.tableLayoutPanelDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelDistribution.ColumnCount = 1;
			this.tableLayoutPanelDistribution.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelDistribution.Controls.Add(this.panelDistributionDestinations, 0, 1);
			this.tableLayoutPanelDistribution.Controls.Add(this.panelDistributionGroupByAndFilter, 0, 0);
			this.tableLayoutPanelDistribution.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelDistribution.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelDistribution.Name = "tableLayoutPanelDistribution";
			this.tableLayoutPanelDistribution.RowCount = 2;
			this.tableLayoutPanelDistribution.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
			this.tableLayoutPanelDistribution.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelDistribution.Size = new System.Drawing.Size(449, 235);
			this.tableLayoutPanelDistribution.TabIndex = 3;
			this.tableLayoutPanelDistribution.Visible = false;
			// 
			// panelDistributionDestinations
			// 
			this.panelDistributionDestinations.Controls.Add(this.labelDistributionUnavailable);
			this.panelDistributionDestinations.Controls.Add(this.linkLabelDistributionExpandAll);
			this.panelDistributionDestinations.Controls.Add(this.linkLabelDistributionCollapseAll);
			this.panelDistributionDestinations.Controls.Add(this.linkLabelDistributionDistributeAll);
			this.panelDistributionDestinations.Controls.Add(this.labelDistributionAllSelected);
			this.panelDistributionDestinations.Controls.Add(this.panelScrollDistributionDestinations);
			this.panelDistributionDestinations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelDistributionDestinations.Location = new System.Drawing.Point(0, 63);
			this.panelDistributionDestinations.Margin = new System.Windows.Forms.Padding(0);
			this.panelDistributionDestinations.Name = "panelDistributionDestinations";
			this.panelDistributionDestinations.Size = new System.Drawing.Size(449, 172);
			this.panelDistributionDestinations.TabIndex = 5;
			// 
			// labelDistributionUnavailable
			// 
			this.labelDistributionUnavailable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDistributionUnavailable.Location = new System.Drawing.Point(10, 10);
			this.labelDistributionUnavailable.Margin = new System.Windows.Forms.Padding(10);
			this.labelDistributionUnavailable.Name = "labelDistributionUnavailable";
			this.labelDistributionUnavailable.Size = new System.Drawing.Size(429, 152);
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
			this.linkLabelDistributionExpandAll.Location = new System.Drawing.Point(310, 10);
			this.linkLabelDistributionExpandAll.Name = "linkLabelDistributionExpandAll";
			this.linkLabelDistributionExpandAll.Size = new System.Drawing.Size(61, 13);
			this.linkLabelDistributionExpandAll.TabIndex = 16;
			this.linkLabelDistributionExpandAll.TabStop = true;
			this.linkLabelDistributionExpandAll.Text = "Expand All";
			this.linkLabelDistributionExpandAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelDistributionExpandAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionExpandAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelDistributionExpandAll_LinkClicked);
			// 
			// linkLabelDistributionCollapseAll
			// 
			this.linkLabelDistributionCollapseAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionCollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelDistributionCollapseAll.AutoSize = true;
			this.linkLabelDistributionCollapseAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelDistributionCollapseAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelDistributionCollapseAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionCollapseAll.Location = new System.Drawing.Point(372, 10);
			this.linkLabelDistributionCollapseAll.Name = "linkLabelDistributionCollapseAll";
			this.linkLabelDistributionCollapseAll.Size = new System.Drawing.Size(67, 13);
			this.linkLabelDistributionCollapseAll.TabIndex = 15;
			this.linkLabelDistributionCollapseAll.TabStop = true;
			this.linkLabelDistributionCollapseAll.Text = "Collapse All";
			this.linkLabelDistributionCollapseAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelDistributionCollapseAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistributionCollapseAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelDistributionCollapseAll_LinkClicked);
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
			this.linkLabelDistributionDistributeAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelDistributionDistributeAll_LinkClicked);
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
			// panelScrollDistributionDestinations
			// 
			this.panelScrollDistributionDestinations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelScrollDistributionDestinations.AutoScroll = true;
			this.panelScrollDistributionDestinations.Controls.Add(this.flowLayoutPanelDistributionDestinations);
			this.panelScrollDistributionDestinations.Location = new System.Drawing.Point(10, 30);
			this.panelScrollDistributionDestinations.Margin = new System.Windows.Forms.Padding(10);
			this.panelScrollDistributionDestinations.Name = "panelScrollDistributionDestinations";
			this.panelScrollDistributionDestinations.Size = new System.Drawing.Size(429, 132);
			this.panelScrollDistributionDestinations.TabIndex = 1;
			// 
			// flowLayoutPanelDistributionDestinations
			// 
			this.flowLayoutPanelDistributionDestinations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelDistributionDestinations.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanelDistributionDestinations.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanelDistributionDestinations.Name = "flowLayoutPanelDistributionDestinations";
			this.flowLayoutPanelDistributionDestinations.Size = new System.Drawing.Size(429, 132);
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
			this.panelDistributionGroupByAndFilter.Size = new System.Drawing.Size(449, 63);
			this.panelDistributionGroupByAndFilter.TabIndex = 4;
			// 
			// tableLayoutPanelDistributionFilter
			// 
			this.tableLayoutPanelDistributionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelDistributionFilter.ColumnCount = 7;
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelDistributionFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanelDistributionFilter.Controls.Add(this.comboBoxDistributionDestinationMachineFilter, 6, 0);
			this.tableLayoutPanelDistributionFilter.Controls.Add(this.comboBoxDistributionSourceMachineFilter, 0, 0);
			this.tableLayoutPanelDistributionFilter.Controls.Add(this.comboBoxDistributionApplicationFilter, 4, 0);
			this.tableLayoutPanelDistributionFilter.Controls.Add(this.comboBoxDistributionGroupFilter, 2, 0);
			this.tableLayoutPanelDistributionFilter.Location = new System.Drawing.Point(71, 37);
			this.tableLayoutPanelDistributionFilter.Name = "tableLayoutPanelDistributionFilter";
			this.tableLayoutPanelDistributionFilter.RowCount = 1;
			this.tableLayoutPanelDistributionFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelDistributionFilter.Size = new System.Drawing.Size(368, 21);
			this.tableLayoutPanelDistributionFilter.TabIndex = 6;
			// 
			// comboBoxDistributionDestinationMachineFilter
			// 
			this.comboBoxDistributionDestinationMachineFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxDistributionDestinationMachineFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDistributionDestinationMachineFilter.FormattingEnabled = true;
			this.comboBoxDistributionDestinationMachineFilter.Location = new System.Drawing.Point(279, 0);
			this.comboBoxDistributionDestinationMachineFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxDistributionDestinationMachineFilter.Name = "comboBoxDistributionDestinationMachineFilter";
			this.comboBoxDistributionDestinationMachineFilter.Size = new System.Drawing.Size(89, 21);
			this.comboBoxDistributionDestinationMachineFilter.TabIndex = 4;
			this.comboBoxDistributionDestinationMachineFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDistributionDestinationMachineFilter_SelectedIndexChanged);
			// 
			// comboBoxDistributionSourceMachineFilter
			// 
			this.comboBoxDistributionSourceMachineFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxDistributionSourceMachineFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDistributionSourceMachineFilter.FormattingEnabled = true;
			this.comboBoxDistributionSourceMachineFilter.Location = new System.Drawing.Point(0, 0);
			this.comboBoxDistributionSourceMachineFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxDistributionSourceMachineFilter.Name = "comboBoxDistributionSourceMachineFilter";
			this.comboBoxDistributionSourceMachineFilter.Size = new System.Drawing.Size(87, 21);
			this.comboBoxDistributionSourceMachineFilter.TabIndex = 3;
			this.comboBoxDistributionSourceMachineFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDistributionSourceMachineFilter_SelectedIndexChanged);
			// 
			// comboBoxDistributionApplicationFilter
			// 
			this.comboBoxDistributionApplicationFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxDistributionApplicationFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDistributionApplicationFilter.FormattingEnabled = true;
			this.comboBoxDistributionApplicationFilter.Location = new System.Drawing.Point(186, 0);
			this.comboBoxDistributionApplicationFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxDistributionApplicationFilter.Name = "comboBoxDistributionApplicationFilter";
			this.comboBoxDistributionApplicationFilter.Size = new System.Drawing.Size(87, 21);
			this.comboBoxDistributionApplicationFilter.TabIndex = 5;
			this.comboBoxDistributionApplicationFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDistributionApplicationFilter_SelectedIndexChanged);
			// 
			// comboBoxDistributionGroupFilter
			// 
			this.comboBoxDistributionGroupFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxDistributionGroupFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDistributionGroupFilter.FormattingEnabled = true;
			this.comboBoxDistributionGroupFilter.Location = new System.Drawing.Point(93, 0);
			this.comboBoxDistributionGroupFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxDistributionGroupFilter.Name = "comboBoxDistributionGroupFilter";
			this.comboBoxDistributionGroupFilter.Size = new System.Drawing.Size(87, 21);
			this.comboBoxDistributionGroupFilter.TabIndex = 4;
			this.comboBoxDistributionGroupFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDistributionGroupFilter_SelectedIndexChanged);
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
			this.comboBoxDistributionGroupBy.Size = new System.Drawing.Size(368, 21);
			this.comboBoxDistributionGroupBy.TabIndex = 1;
			this.comboBoxDistributionGroupBy.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDistributionGroupBy_SelectedIndexChanged);
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
			this.ClientSize = new System.Drawing.Size(449, 300);
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
			this.Enter += new System.EventHandler(this.ControlPanelForm_Enter);
			this.Resize += new System.EventHandler(this.ControlPanelForm_Resize);
			this.contextMenuStripSystemTray.ResumeLayout(false);
			this.tableLayoutPanelProcess.ResumeLayout(false);
			this.panelProcessApplications.ResumeLayout(false);
			this.panelProcessApplications.PerformLayout();
			this.panelScrollProcessApplications.ResumeLayout(false);
			this.panelProcessGroupByAndFilter.ResumeLayout(false);
			this.panelProcessGroupByAndFilter.PerformLayout();
			this.tableLayoutPanelProcessFilter.ResumeLayout(false);
			this.panelGlass.ResumeLayout(false);
			this.panelGlassTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
			this.tabControlSection.ResumeLayout(false);
			this.tableLayoutPanelMain.ResumeLayout(false);
			this.horizontalPanel.ResumeLayout(false);
			this.panelTabPageArea.ResumeLayout(false);
			this.tableLayoutPanelMacro.ResumeLayout(false);
			this.panelMacros.ResumeLayout(false);
			this.panelMacros.PerformLayout();
			this.panelScrollMacros.ResumeLayout(false);
			this.tableLayoutPanelDistribution.ResumeLayout(false);
			this.panelDistributionDestinations.ResumeLayout(false);
			this.panelDistributionDestinations.PerformLayout();
			this.panelScrollDistributionDestinations.ResumeLayout(false);
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
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProcess;
		private System.Windows.Forms.LinkLabel linkLabelOpenConfiguration;
		private System.Windows.Forms.Panel panelProcessApplications;
		private System.Windows.Forms.Panel panelScrollProcessApplications;
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
		private System.Windows.Forms.ComboBox comboBoxDistributionSourceMachineFilter;
		private System.Windows.Forms.ComboBox comboBoxDistributionApplicationFilter;
		private System.Windows.Forms.ComboBox comboBoxDistributionGroupFilter;
		private System.Windows.Forms.Label labelDistributionFilter;
		private System.Windows.Forms.ComboBox comboBoxDistributionGroupBy;
		private System.Windows.Forms.Label labelDistributionGroupBy;
		private System.Windows.Forms.Panel panelDistributionDestinations;
		private System.Windows.Forms.Label labelDistributionUnavailable;
		private System.Windows.Forms.LinkLabel linkLabelDistributionExpandAll;
		private System.Windows.Forms.LinkLabel linkLabelDistributionCollapseAll;
		private System.Windows.Forms.LinkLabel linkLabelDistributionDistributeAll;
		private System.Windows.Forms.Label labelDistributionAllSelected;
		private System.Windows.Forms.Panel panelScrollDistributionDestinations;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDistributionDestinations;
		private System.Windows.Forms.ComboBox comboBoxDistributionDestinationMachineFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMacro;
        private System.Windows.Forms.Panel panelMacros;
        private System.Windows.Forms.Label labelMacroUnavailable;
		private System.Windows.Forms.Panel panelScrollMacros;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMacros;
        private System.Windows.Forms.TabPage tabPageMacro;
		private System.Windows.Forms.LinkLabel linkLabelMacroExpandAll;
		private System.Windows.Forms.LinkLabel linkLabelMacroCollapseAll;
		private System.Windows.Forms.LinkLabel linkLabelMacroPlayAll;
		private System.Windows.Forms.Label labelMacroAllSelected;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptions;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptionsStartWithWindows;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptionsUserOwnsControlPanel;
		private System.Windows.Forms.Panel panelGlassTop;
		private System.Windows.Forms.PictureBox pictureBoxClose;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptionsMinimumLogLevel;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptionsMinimumLogLevelError;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug;
	}
}
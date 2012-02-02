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
			this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
			this.horizontalPanel = new ProcessManagerUI.Controls.HorizontalPanel();
			this.linkLabelOpenConfiguration = new System.Windows.Forms.LinkLabel();
			this.panelApplications = new System.Windows.Forms.Panel();
			this.labelUnavailable = new System.Windows.Forms.Label();
			this.linkLabelExpandAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelCollapseAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelStopAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelStartAll = new System.Windows.Forms.LinkLabel();
			this.linkLabelRestartAll = new System.Windows.Forms.LinkLabel();
			this.labelAllSelected = new System.Windows.Forms.Label();
			this.flowLayoutPanelApplications = new System.Windows.Forms.FlowLayoutPanel();
			this.panelGroupByAndFilter = new System.Windows.Forms.Panel();
			this.tableLayoutPanelFilter = new System.Windows.Forms.TableLayoutPanel();
			this.comboBoxMachineFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxApplicationFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxGroupFilter = new System.Windows.Forms.ComboBox();
			this.labelFilter = new System.Windows.Forms.Label();
			this.comboBoxGroupBy = new System.Windows.Forms.ComboBox();
			this.labelGroupBy = new System.Windows.Forms.Label();
			this.contextMenuStripSystemTray.SuspendLayout();
			this.tableLayoutPanelMain.SuspendLayout();
			this.horizontalPanel.SuspendLayout();
			this.panelApplications.SuspendLayout();
			this.panelGroupByAndFilter.SuspendLayout();
			this.tableLayoutPanelFilter.SuspendLayout();
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
			// tableLayoutPanelMain
			// 
			this.tableLayoutPanelMain.ColumnCount = 1;
			this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMain.Controls.Add(this.horizontalPanel, 0, 2);
			this.tableLayoutPanelMain.Controls.Add(this.panelApplications, 0, 1);
			this.tableLayoutPanelMain.Controls.Add(this.panelGroupByAndFilter, 0, 0);
			this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
			this.tableLayoutPanelMain.RowCount = 3;
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
			this.tableLayoutPanelMain.Size = new System.Drawing.Size(400, 277);
			this.tableLayoutPanelMain.TabIndex = 2;
			// 
			// horizontalPanel
			// 
			this.horizontalPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalPanel.AutoModifyAddedControls = false;
			this.horizontalPanel.BackColor = System.Drawing.Color.Transparent;
			this.horizontalPanel.Controls.Add(this.linkLabelOpenConfiguration);
			this.horizontalPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.horizontalPanel.Location = new System.Drawing.Point(0, 234);
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
			// panelApplications
			// 
			this.panelApplications.Controls.Add(this.labelUnavailable);
			this.panelApplications.Controls.Add(this.linkLabelExpandAll);
			this.panelApplications.Controls.Add(this.linkLabelCollapseAll);
			this.panelApplications.Controls.Add(this.linkLabelStopAll);
			this.panelApplications.Controls.Add(this.linkLabelStartAll);
			this.panelApplications.Controls.Add(this.linkLabelRestartAll);
			this.panelApplications.Controls.Add(this.labelAllSelected);
			this.panelApplications.Controls.Add(this.flowLayoutPanelApplications);
			this.panelApplications.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelApplications.Location = new System.Drawing.Point(0, 63);
			this.panelApplications.Margin = new System.Windows.Forms.Padding(0);
			this.panelApplications.Name = "panelApplications";
			this.panelApplications.Size = new System.Drawing.Size(400, 171);
			this.panelApplications.TabIndex = 2;
			// 
			// labelUnavailable
			// 
			this.labelUnavailable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelUnavailable.Location = new System.Drawing.Point(10, 10);
			this.labelUnavailable.Margin = new System.Windows.Forms.Padding(10);
			this.labelUnavailable.Name = "labelUnavailable";
			this.labelUnavailable.Size = new System.Drawing.Size(380, 151);
			this.labelUnavailable.TabIndex = 0;
			this.labelUnavailable.Text = "No applications available";
			this.labelUnavailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// linkLabelExpandAll
			// 
			this.linkLabelExpandAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelExpandAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelExpandAll.AutoSize = true;
			this.linkLabelExpandAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelExpandAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelExpandAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelExpandAll.Location = new System.Drawing.Point(261, 10);
			this.linkLabelExpandAll.Name = "linkLabelExpandAll";
			this.linkLabelExpandAll.Size = new System.Drawing.Size(61, 13);
			this.linkLabelExpandAll.TabIndex = 16;
			this.linkLabelExpandAll.TabStop = true;
			this.linkLabelExpandAll.Text = "Expand All";
			this.linkLabelExpandAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelExpandAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelExpandAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelExpandAll_LinkClicked);
			// 
			// linkLabelCollapseAll
			// 
			this.linkLabelCollapseAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelCollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelCollapseAll.AutoSize = true;
			this.linkLabelCollapseAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelCollapseAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelCollapseAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelCollapseAll.Location = new System.Drawing.Point(323, 10);
			this.linkLabelCollapseAll.Name = "linkLabelCollapseAll";
			this.linkLabelCollapseAll.Size = new System.Drawing.Size(67, 13);
			this.linkLabelCollapseAll.TabIndex = 15;
			this.linkLabelCollapseAll.TabStop = true;
			this.linkLabelCollapseAll.Text = "Collapse All";
			this.linkLabelCollapseAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelCollapseAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelCollapseAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelCollapseAll_LinkClicked);
			// 
			// linkLabelStopAll
			// 
			this.linkLabelStopAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStopAll.AutoSize = true;
			this.linkLabelStopAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelStopAll.Enabled = false;
			this.linkLabelStopAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelStopAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStopAll.Location = new System.Drawing.Point(109, 10);
			this.linkLabelStopAll.Name = "linkLabelStopAll";
			this.linkLabelStopAll.Size = new System.Drawing.Size(31, 13);
			this.linkLabelStopAll.TabIndex = 13;
			this.linkLabelStopAll.TabStop = true;
			this.linkLabelStopAll.Text = "Stop";
			this.linkLabelStopAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelStopAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStopAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelStopAll_LinkClicked);
			// 
			// linkLabelStartAll
			// 
			this.linkLabelStartAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStartAll.AutoSize = true;
			this.linkLabelStartAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelStartAll.Enabled = false;
			this.linkLabelStartAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelStartAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStartAll.Location = new System.Drawing.Point(79, 10);
			this.linkLabelStartAll.Name = "linkLabelStartAll";
			this.linkLabelStartAll.Size = new System.Drawing.Size(31, 13);
			this.linkLabelStartAll.TabIndex = 12;
			this.linkLabelStartAll.TabStop = true;
			this.linkLabelStartAll.Text = "Start";
			this.linkLabelStartAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelStartAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStartAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelStartAll_LinkClicked);
			// 
			// linkLabelRestartAll
			// 
			this.linkLabelRestartAll.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelRestartAll.AutoSize = true;
			this.linkLabelRestartAll.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelRestartAll.Enabled = false;
			this.linkLabelRestartAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelRestartAll.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelRestartAll.Location = new System.Drawing.Point(139, 10);
			this.linkLabelRestartAll.Name = "linkLabelRestartAll";
			this.linkLabelRestartAll.Size = new System.Drawing.Size(43, 13);
			this.linkLabelRestartAll.TabIndex = 14;
			this.linkLabelRestartAll.TabStop = true;
			this.linkLabelRestartAll.Text = "Restart";
			this.linkLabelRestartAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelRestartAll.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelRestartAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelRestartAll_LinkClicked);
			// 
			// labelAllSelected
			// 
			this.labelAllSelected.AutoSize = true;
			this.labelAllSelected.Location = new System.Drawing.Point(7, 10);
			this.labelAllSelected.Name = "labelAllSelected";
			this.labelAllSelected.Size = new System.Drawing.Size(69, 13);
			this.labelAllSelected.TabIndex = 2;
			this.labelAllSelected.Text = "All Selected:";
			// 
			// flowLayoutPanelApplications
			// 
			this.flowLayoutPanelApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelApplications.Location = new System.Drawing.Point(10, 30);
			this.flowLayoutPanelApplications.Margin = new System.Windows.Forms.Padding(10);
			this.flowLayoutPanelApplications.Name = "flowLayoutPanelApplications";
			this.flowLayoutPanelApplications.Size = new System.Drawing.Size(380, 131);
			this.flowLayoutPanelApplications.TabIndex = 1;
			// 
			// panelGroupByAndFilter
			// 
			this.panelGroupByAndFilter.Controls.Add(this.tableLayoutPanelFilter);
			this.panelGroupByAndFilter.Controls.Add(this.labelFilter);
			this.panelGroupByAndFilter.Controls.Add(this.comboBoxGroupBy);
			this.panelGroupByAndFilter.Controls.Add(this.labelGroupBy);
			this.panelGroupByAndFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelGroupByAndFilter.Location = new System.Drawing.Point(0, 0);
			this.panelGroupByAndFilter.Margin = new System.Windows.Forms.Padding(0);
			this.panelGroupByAndFilter.Name = "panelGroupByAndFilter";
			this.panelGroupByAndFilter.Padding = new System.Windows.Forms.Padding(10);
			this.panelGroupByAndFilter.Size = new System.Drawing.Size(400, 63);
			this.panelGroupByAndFilter.TabIndex = 3;
			// 
			// tableLayoutPanelFilter
			// 
			this.tableLayoutPanelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelFilter.ColumnCount = 5;
			this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
			this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanelFilter.Controls.Add(this.comboBoxMachineFilter, 0, 0);
			this.tableLayoutPanelFilter.Controls.Add(this.comboBoxApplicationFilter, 4, 0);
			this.tableLayoutPanelFilter.Controls.Add(this.comboBoxGroupFilter, 2, 0);
			this.tableLayoutPanelFilter.Location = new System.Drawing.Point(71, 37);
			this.tableLayoutPanelFilter.Name = "tableLayoutPanelFilter";
			this.tableLayoutPanelFilter.RowCount = 1;
			this.tableLayoutPanelFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelFilter.Size = new System.Drawing.Size(319, 21);
			this.tableLayoutPanelFilter.TabIndex = 6;
			// 
			// comboBoxMachineFilter
			// 
			this.comboBoxMachineFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxMachineFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMachineFilter.FormattingEnabled = true;
			this.comboBoxMachineFilter.Location = new System.Drawing.Point(0, 0);
			this.comboBoxMachineFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxMachineFilter.Name = "comboBoxMachineFilter";
			this.comboBoxMachineFilter.Size = new System.Drawing.Size(102, 21);
			this.comboBoxMachineFilter.TabIndex = 3;
			this.comboBoxMachineFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMachineFilter_SelectedIndexChanged);
			// 
			// comboBoxApplicationFilter
			// 
			this.comboBoxApplicationFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxApplicationFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxApplicationFilter.FormattingEnabled = true;
			this.comboBoxApplicationFilter.Location = new System.Drawing.Point(216, 0);
			this.comboBoxApplicationFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxApplicationFilter.Name = "comboBoxApplicationFilter";
			this.comboBoxApplicationFilter.Size = new System.Drawing.Size(103, 21);
			this.comboBoxApplicationFilter.TabIndex = 5;
			this.comboBoxApplicationFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxApplicationFilter_SelectedIndexChanged);
			// 
			// comboBoxGroupFilter
			// 
			this.comboBoxGroupFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxGroupFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGroupFilter.FormattingEnabled = true;
			this.comboBoxGroupFilter.Location = new System.Drawing.Point(108, 0);
			this.comboBoxGroupFilter.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxGroupFilter.Name = "comboBoxGroupFilter";
			this.comboBoxGroupFilter.Size = new System.Drawing.Size(102, 21);
			this.comboBoxGroupFilter.TabIndex = 4;
			this.comboBoxGroupFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxGroupFilter_SelectedIndexChanged);
			// 
			// labelFilter
			// 
			this.labelFilter.AutoSize = true;
			this.labelFilter.Location = new System.Drawing.Point(7, 40);
			this.labelFilter.Name = "labelFilter";
			this.labelFilter.Size = new System.Drawing.Size(36, 13);
			this.labelFilter.TabIndex = 2;
			this.labelFilter.Text = "Filter:";
			// 
			// comboBoxGroupBy
			// 
			this.comboBoxGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGroupBy.FormattingEnabled = true;
			this.comboBoxGroupBy.Location = new System.Drawing.Point(71, 10);
			this.comboBoxGroupBy.Name = "comboBoxGroupBy";
			this.comboBoxGroupBy.Size = new System.Drawing.Size(319, 21);
			this.comboBoxGroupBy.TabIndex = 1;
			this.comboBoxGroupBy.SelectedIndexChanged += new System.EventHandler(this.ComboBoxGroupBy_SelectedIndexChanged);
			// 
			// labelGroupBy
			// 
			this.labelGroupBy.AutoSize = true;
			this.labelGroupBy.Location = new System.Drawing.Point(7, 13);
			this.labelGroupBy.Name = "labelGroupBy";
			this.labelGroupBy.Size = new System.Drawing.Size(58, 13);
			this.labelGroupBy.TabIndex = 0;
			this.labelGroupBy.Text = "Group by:";
			// 
			// ControlPanelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(400, 277);
			this.ControlBox = false;
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
			this.tableLayoutPanelMain.ResumeLayout(false);
			this.horizontalPanel.ResumeLayout(false);
			this.panelApplications.ResumeLayout(false);
			this.panelApplications.PerformLayout();
			this.panelGroupByAndFilter.ResumeLayout(false);
			this.panelGroupByAndFilter.PerformLayout();
			this.tableLayoutPanelFilter.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemTray;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayConfiguration;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSystemTray1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayExit;
		private Controls.HorizontalPanel horizontalPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
		private System.Windows.Forms.LinkLabel linkLabelOpenConfiguration;
		private System.Windows.Forms.Panel panelApplications;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelApplications;
		private System.Windows.Forms.Label labelUnavailable;
		private System.Windows.Forms.Panel panelGroupByAndFilter;
		private System.Windows.Forms.ComboBox comboBoxApplicationFilter;
		private System.Windows.Forms.ComboBox comboBoxGroupFilter;
		private System.Windows.Forms.ComboBox comboBoxMachineFilter;
		private System.Windows.Forms.Label labelFilter;
		private System.Windows.Forms.ComboBox comboBoxGroupBy;
		private System.Windows.Forms.Label labelGroupBy;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFilter;
		private System.Windows.Forms.Label labelAllSelected;
		private System.Windows.Forms.LinkLabel linkLabelCollapseAll;
		private System.Windows.Forms.LinkLabel linkLabelStopAll;
		private System.Windows.Forms.LinkLabel linkLabelStartAll;
		private System.Windows.Forms.LinkLabel linkLabelRestartAll;
		private System.Windows.Forms.LinkLabel linkLabelExpandAll;
	}
}
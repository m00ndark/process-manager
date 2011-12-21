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
			this.horizontalPanel = new ProcessManagerUI.Controls.HorizontalPanel();
			this.linkLabelOpenConfiguration = new System.Windows.Forms.LinkLabel();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.panelApplications = new System.Windows.Forms.Panel();
			this.labelUnavailable = new System.Windows.Forms.Label();
			this.flowLayoutPanelApplications = new System.Windows.Forms.FlowLayoutPanel();
			this.panelGroupByAndFilter = new System.Windows.Forms.Panel();
			this.labelGroupBy = new System.Windows.Forms.Label();
			this.comboBoxGroupBy = new System.Windows.Forms.ComboBox();
			this.comboBoxMachineFilter = new System.Windows.Forms.ComboBox();
			this.labelFilter = new System.Windows.Forms.Label();
			this.comboBoxGroupFilter = new System.Windows.Forms.ComboBox();
			this.comboBoxApplicationFilter = new System.Windows.Forms.ComboBox();
			this.contextMenuStripSystemTray.SuspendLayout();
			this.horizontalPanel.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.panelApplications.SuspendLayout();
			this.panelGroupByAndFilter.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStripSystemTray;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Process Manager";
			this.notifyIcon.Visible = true;
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
			// 
			// horizontalPanel
			// 
			this.horizontalPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalPanel.BackColor = System.Drawing.Color.Transparent;
			this.horizontalPanel.Controls.Add(this.linkLabelOpenConfiguration);
			this.horizontalPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.horizontalPanel.Location = new System.Drawing.Point(0, 236);
			this.horizontalPanel.Margin = new System.Windows.Forms.Padding(0);
			this.horizontalPanel.Name = "horizontalPanel";
			this.horizontalPanel.Size = new System.Drawing.Size(401, 43);
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
			this.linkLabelOpenConfiguration.Size = new System.Drawing.Size(401, 43);
			this.linkLabelOpenConfiguration.TabIndex = 0;
			this.linkLabelOpenConfiguration.TabStop = true;
			this.linkLabelOpenConfiguration.Text = "Open Configuration";
			this.linkLabelOpenConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.horizontalPanel, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.panelApplications, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.panelGroupByAndFilter, 0, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 3;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(401, 279);
			this.tableLayoutPanel.TabIndex = 2;
			// 
			// panelApplications
			// 
			this.panelApplications.Controls.Add(this.labelUnavailable);
			this.panelApplications.Controls.Add(this.flowLayoutPanelApplications);
			this.panelApplications.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelApplications.Location = new System.Drawing.Point(0, 74);
			this.panelApplications.Margin = new System.Windows.Forms.Padding(0);
			this.panelApplications.Name = "panelApplications";
			this.panelApplications.Size = new System.Drawing.Size(401, 162);
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
			this.labelUnavailable.Size = new System.Drawing.Size(381, 142);
			this.labelUnavailable.TabIndex = 0;
			this.labelUnavailable.Text = "No applications available";
			this.labelUnavailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// flowLayoutPanelApplications
			// 
			this.flowLayoutPanelApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelApplications.Location = new System.Drawing.Point(10, 10);
			this.flowLayoutPanelApplications.Margin = new System.Windows.Forms.Padding(10);
			this.flowLayoutPanelApplications.Name = "flowLayoutPanelApplications";
			this.flowLayoutPanelApplications.Size = new System.Drawing.Size(381, 142);
			this.flowLayoutPanelApplications.TabIndex = 1;
			// 
			// panelGroupByAndFilter
			// 
			this.panelGroupByAndFilter.Controls.Add(this.comboBoxApplicationFilter);
			this.panelGroupByAndFilter.Controls.Add(this.comboBoxGroupFilter);
			this.panelGroupByAndFilter.Controls.Add(this.comboBoxMachineFilter);
			this.panelGroupByAndFilter.Controls.Add(this.labelFilter);
			this.panelGroupByAndFilter.Controls.Add(this.comboBoxGroupBy);
			this.panelGroupByAndFilter.Controls.Add(this.labelGroupBy);
			this.panelGroupByAndFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelGroupByAndFilter.Location = new System.Drawing.Point(0, 0);
			this.panelGroupByAndFilter.Margin = new System.Windows.Forms.Padding(0);
			this.panelGroupByAndFilter.Name = "panelGroupByAndFilter";
			this.panelGroupByAndFilter.Padding = new System.Windows.Forms.Padding(10);
			this.panelGroupByAndFilter.Size = new System.Drawing.Size(401, 74);
			this.panelGroupByAndFilter.TabIndex = 3;
			// 
			// labelGroupBy
			// 
			this.labelGroupBy.AutoSize = true;
			this.labelGroupBy.Location = new System.Drawing.Point(13, 16);
			this.labelGroupBy.Name = "labelGroupBy";
			this.labelGroupBy.Size = new System.Drawing.Size(58, 13);
			this.labelGroupBy.TabIndex = 0;
			this.labelGroupBy.Text = "Group by:";
			// 
			// comboBoxGroupBy
			// 
			this.comboBoxGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGroupBy.FormattingEnabled = true;
			this.comboBoxGroupBy.Location = new System.Drawing.Point(77, 13);
			this.comboBoxGroupBy.Name = "comboBoxGroupBy";
			this.comboBoxGroupBy.Size = new System.Drawing.Size(311, 21);
			this.comboBoxGroupBy.TabIndex = 1;
			// 
			// comboBoxMachineFilter
			// 
			this.comboBoxMachineFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxMachineFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMachineFilter.FormattingEnabled = true;
			this.comboBoxMachineFilter.Location = new System.Drawing.Point(77, 40);
			this.comboBoxMachineFilter.Name = "comboBoxMachineFilter";
			this.comboBoxMachineFilter.Size = new System.Drawing.Size(111, 21);
			this.comboBoxMachineFilter.TabIndex = 3;
			// 
			// labelFilter
			// 
			this.labelFilter.AutoSize = true;
			this.labelFilter.Location = new System.Drawing.Point(13, 43);
			this.labelFilter.Name = "labelFilter";
			this.labelFilter.Size = new System.Drawing.Size(36, 13);
			this.labelFilter.TabIndex = 2;
			this.labelFilter.Text = "Filter:";
			// 
			// comboBoxGroupFilter
			// 
			this.comboBoxGroupFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxGroupFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGroupFilter.FormattingEnabled = true;
			this.comboBoxGroupFilter.Location = new System.Drawing.Point(194, 40);
			this.comboBoxGroupFilter.Name = "comboBoxGroupFilter";
			this.comboBoxGroupFilter.Size = new System.Drawing.Size(94, 21);
			this.comboBoxGroupFilter.TabIndex = 4;
			// 
			// comboBoxApplicationFilter
			// 
			this.comboBoxApplicationFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxApplicationFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxApplicationFilter.FormattingEnabled = true;
			this.comboBoxApplicationFilter.Location = new System.Drawing.Point(294, 40);
			this.comboBoxApplicationFilter.Name = "comboBoxApplicationFilter";
			this.comboBoxApplicationFilter.Size = new System.Drawing.Size(94, 21);
			this.comboBoxApplicationFilter.TabIndex = 5;
			// 
			// ControlPanelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(401, 279);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ControlPanelForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Load += new System.EventHandler(this.ControlPanelForm_Load);
			this.contextMenuStripSystemTray.ResumeLayout(false);
			this.horizontalPanel.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			this.panelApplications.ResumeLayout(false);
			this.panelGroupByAndFilter.ResumeLayout(false);
			this.panelGroupByAndFilter.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemTray;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayConfiguration;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSystemTray1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayExit;
		private Controls.HorizontalPanel horizontalPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
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
	}
}
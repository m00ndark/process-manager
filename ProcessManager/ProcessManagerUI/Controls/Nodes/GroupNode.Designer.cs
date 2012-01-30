namespace ProcessManagerUI.Controls.Nodes
{
	partial class GroupNode
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBoxExpandCollapse = new System.Windows.Forms.PictureBox();
			this.checkBoxSelected = new System.Windows.Forms.CheckBox();
			this.linkLabelStart = new System.Windows.Forms.LinkLabel();
			this.labelGroupName = new System.Windows.Forms.Label();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.panelControl = new System.Windows.Forms.Panel();
			this.linkLabelRestart = new System.Windows.Forms.LinkLabel();
			this.linkLabelStop = new System.Windows.Forms.LinkLabel();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.labeledDivider = new ProcessManagerUI.Controls.LabeledDivider();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpandCollapse)).BeginInit();
			this.tableLayoutPanel.SuspendLayout();
			this.panelControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBoxExpandCollapse
			// 
			this.pictureBoxExpandCollapse.Location = new System.Drawing.Point(3, 3);
			this.pictureBoxExpandCollapse.Name = "pictureBoxExpandCollapse";
			this.pictureBoxExpandCollapse.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxExpandCollapse.TabIndex = 0;
			this.pictureBoxExpandCollapse.TabStop = false;
			// 
			// checkBoxSelected
			// 
			this.checkBoxSelected.AutoSize = true;
			this.checkBoxSelected.Location = new System.Drawing.Point(25, 5);
			this.checkBoxSelected.Name = "checkBoxSelected";
			this.checkBoxSelected.Size = new System.Drawing.Size(15, 14);
			this.checkBoxSelected.TabIndex = 1;
			this.checkBoxSelected.ThreeState = true;
			this.checkBoxSelected.UseVisualStyleBackColor = true;
			// 
			// linkLabelStart
			// 
			this.linkLabelStart.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabelStart.AutoSize = true;
			this.linkLabelStart.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelStart.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelStart.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStart.Location = new System.Drawing.Point(3, 4);
			this.linkLabelStart.Name = "linkLabelStart";
			this.linkLabelStart.Size = new System.Drawing.Size(31, 15);
			this.linkLabelStart.TabIndex = 3;
			this.linkLabelStart.TabStop = true;
			this.linkLabelStart.Text = "Start";
			this.linkLabelStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelStart.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			// 
			// labelGroupName
			// 
			this.labelGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.labelGroupName.AutoSize = true;
			this.labelGroupName.Location = new System.Drawing.Point(0, 0);
			this.labelGroupName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelGroupName.Name = "labelGroupName";
			this.labelGroupName.Size = new System.Drawing.Size(55, 22);
			this.labelGroupName.TabIndex = 2;
			this.labelGroupName.Text = "<group>";
			this.labelGroupName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.labelGroupName, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.panelControl, 1, 0);
			this.tableLayoutPanel.Location = new System.Drawing.Point(44, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(306, 22);
			this.tableLayoutPanel.TabIndex = 3;
			// 
			// panelControl
			// 
			this.panelControl.Controls.Add(this.labeledDivider);
			this.panelControl.Controls.Add(this.linkLabelRestart);
			this.panelControl.Controls.Add(this.linkLabelStop);
			this.panelControl.Controls.Add(this.linkLabelStart);
			this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl.Location = new System.Drawing.Point(93, 0);
			this.panelControl.Margin = new System.Windows.Forms.Padding(0);
			this.panelControl.Name = "panelControl";
			this.panelControl.Size = new System.Drawing.Size(213, 22);
			this.panelControl.TabIndex = 3;
			// 
			// linkLabelRestart
			// 
			this.linkLabelRestart.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelRestart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabelRestart.AutoSize = true;
			this.linkLabelRestart.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelRestart.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelRestart.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelRestart.Location = new System.Drawing.Point(63, 4);
			this.linkLabelRestart.Name = "linkLabelRestart";
			this.linkLabelRestart.Size = new System.Drawing.Size(43, 15);
			this.linkLabelRestart.TabIndex = 5;
			this.linkLabelRestart.TabStop = true;
			this.linkLabelRestart.Text = "Restart";
			this.linkLabelRestart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelRestart.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			// 
			// linkLabelStop
			// 
			this.linkLabelStop.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabelStop.AutoSize = true;
			this.linkLabelStop.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelStop.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelStop.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStop.Location = new System.Drawing.Point(33, 4);
			this.linkLabelStop.Name = "linkLabelStop";
			this.linkLabelStop.Size = new System.Drawing.Size(31, 15);
			this.linkLabelStop.TabIndex = 4;
			this.linkLabelStop.TabStop = true;
			this.linkLabelStop.Text = "Stop";
			this.linkLabelStop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelStop.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel.Location = new System.Drawing.Point(25, 22);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(325, 97);
			this.flowLayoutPanel.TabIndex = 4;
			// 
			// labeledDivider
			// 
			this.labeledDivider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDivider.BackColor = System.Drawing.Color.Transparent;
			this.labeledDivider.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDivider.Location = new System.Drawing.Point(108, 4);
			this.labeledDivider.Name = "labeledDivider";
			this.labeledDivider.Size = new System.Drawing.Size(105, 15);
			this.labeledDivider.TabIndex = 7;
			this.labeledDivider.Text = "";
			// 
			// GroupNode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.flowLayoutPanel);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.checkBoxSelected);
			this.Controls.Add(this.pictureBoxExpandCollapse);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "GroupNode";
			this.Size = new System.Drawing.Size(350, 119);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpandCollapse)).EndInit();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.panelControl.ResumeLayout(false);
			this.panelControl.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxExpandCollapse;
		private System.Windows.Forms.CheckBox checkBoxSelected;
		private System.Windows.Forms.LinkLabel linkLabelStart;
		private System.Windows.Forms.Label labelGroupName;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Panel panelControl;
		private System.Windows.Forms.LinkLabel linkLabelRestart;
		private System.Windows.Forms.LinkLabel linkLabelStop;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private LabeledDivider labeledDivider;
	}
}

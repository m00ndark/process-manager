namespace ProcessManagerUI.Controls.Nodes.Support
{
    partial class BaseMacroRootNode
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
			this.checkBoxSelected = new System.Windows.Forms.CheckBox();
			this.linkLabelPlay = new System.Windows.Forms.LinkLabel();
			this.labelNodeName = new System.Windows.Forms.Label();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.panelNodeName = new System.Windows.Forms.Panel();
			this.panelControl = new System.Windows.Forms.Panel();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.pictureBoxExpandCollapse = new System.Windows.Forms.PictureBox();
			this.labeledDivider = new ProcessManagerUI.Controls.LabeledDivider();
			this.tableLayoutPanel.SuspendLayout();
			this.panelNodeName.SuspendLayout();
			this.panelControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpandCollapse)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBoxSelected
			// 
			this.checkBoxSelected.AutoSize = true;
			this.checkBoxSelected.Location = new System.Drawing.Point(25, 5);
			this.checkBoxSelected.Name = "checkBoxSelected";
			this.checkBoxSelected.Size = new System.Drawing.Size(15, 14);
			this.checkBoxSelected.TabIndex = 1;
			this.checkBoxSelected.UseVisualStyleBackColor = true;
			this.checkBoxSelected.CheckStateChanged += new System.EventHandler(this.CheckBoxSelected_CheckStateChanged);
			// 
			// linkLabelPlay
			// 
			this.linkLabelPlay.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabelPlay.AutoSize = true;
			this.linkLabelPlay.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelPlay.Enabled = false;
			this.linkLabelPlay.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelPlay.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelPlay.Location = new System.Drawing.Point(3, 4);
			this.linkLabelPlay.Name = "linkLabelPlay";
			this.linkLabelPlay.Size = new System.Drawing.Size(29, 15);
			this.linkLabelPlay.TabIndex = 3;
			this.linkLabelPlay.TabStop = true;
			this.linkLabelPlay.Text = "Play";
			this.linkLabelPlay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelPlay.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelPlay.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelPlay_LinkClicked);
			// 
			// labelNodeName
			// 
			this.labelNodeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.labelNodeName.AutoSize = true;
			this.labelNodeName.Location = new System.Drawing.Point(0, 4);
			this.labelNodeName.Margin = new System.Windows.Forms.Padding(0);
			this.labelNodeName.Name = "labelNodeName";
			this.labelNodeName.Size = new System.Drawing.Size(85, 15);
			this.labelNodeName.TabIndex = 2;
			this.labelNodeName.Text = "<node-name>";
			this.labelNodeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.panelNodeName, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.panelControl, 1, 0);
			this.tableLayoutPanel.Location = new System.Drawing.Point(44, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(306, 22);
			this.tableLayoutPanel.TabIndex = 3;
			// 
			// panelNodeName
			// 
			this.panelNodeName.Controls.Add(this.labelNodeName);
			this.panelNodeName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelNodeName.Location = new System.Drawing.Point(0, 0);
			this.panelNodeName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.panelNodeName.Name = "panelNodeName";
			this.panelNodeName.Size = new System.Drawing.Size(90, 22);
			this.panelNodeName.TabIndex = 5;
			// 
			// panelControl
			// 
			this.panelControl.Controls.Add(this.labeledDivider);
			this.panelControl.Controls.Add(this.linkLabelPlay);
			this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl.Location = new System.Drawing.Point(93, 0);
			this.panelControl.Margin = new System.Windows.Forms.Padding(0);
			this.panelControl.Name = "panelControl";
			this.panelControl.Size = new System.Drawing.Size(213, 22);
			this.panelControl.TabIndex = 3;
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel.Location = new System.Drawing.Point(25, 22);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(325, 78);
			this.flowLayoutPanel.TabIndex = 4;
			// 
			// pictureBoxExpandCollapse
			// 
			this.pictureBoxExpandCollapse.Location = new System.Drawing.Point(3, 3);
			this.pictureBoxExpandCollapse.Name = "pictureBoxExpandCollapse";
			this.pictureBoxExpandCollapse.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxExpandCollapse.TabIndex = 0;
			this.pictureBoxExpandCollapse.TabStop = false;
			this.pictureBoxExpandCollapse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxExpandCollapse_MouseDown);
			// 
			// labeledDivider
			// 
			this.labeledDivider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDivider.BackColor = System.Drawing.Color.Transparent;
			this.labeledDivider.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDivider.Location = new System.Drawing.Point(34, 4);
			this.labeledDivider.Name = "labeledDivider";
			this.labeledDivider.Size = new System.Drawing.Size(179, 15);
			this.labeledDivider.TabIndex = 6;
			this.labeledDivider.Text = "";
			// 
			// BaseMacroRootNode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.flowLayoutPanel);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.checkBoxSelected);
			this.Controls.Add(this.pictureBoxExpandCollapse);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "BaseMacroRootNode";
			this.Size = new System.Drawing.Size(350, 100);
			this.tableLayoutPanel.ResumeLayout(false);
			this.panelNodeName.ResumeLayout(false);
			this.panelNodeName.PerformLayout();
			this.panelControl.ResumeLayout(false);
			this.panelControl.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpandCollapse)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxExpandCollapse;
		private System.Windows.Forms.CheckBox checkBoxSelected;
		private System.Windows.Forms.LinkLabel linkLabelPlay;
		private System.Windows.Forms.Label labelNodeName;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Panel panelControl;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private LabeledDivider labeledDivider;
		private System.Windows.Forms.Panel panelNodeName;
	}
}

namespace ProcessManagerUI.Controls.Nodes.Support
{
	partial class BaseDistributionRootNode
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
			this.linkLabelDistribute = new System.Windows.Forms.LinkLabel();
			this.labelNodeName = new System.Windows.Forms.Label();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.panelMachineName = new System.Windows.Forms.Panel();
			this.panelControl = new System.Windows.Forms.Panel();
			this.labeledDivider = new ProcessManagerUI.Controls.LabeledDivider();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.pictureBoxExpandCollapse = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel.SuspendLayout();
			this.panelMachineName.SuspendLayout();
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
			// linkLabelDistribute
			// 
			this.linkLabelDistribute.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistribute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabelDistribute.AutoSize = true;
			this.linkLabelDistribute.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelDistribute.Enabled = false;
			this.linkLabelDistribute.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelDistribute.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistribute.Location = new System.Drawing.Point(44, 4);
			this.linkLabelDistribute.Name = "linkLabelDistribute";
			this.linkLabelDistribute.Size = new System.Drawing.Size(58, 13);
			this.linkLabelDistribute.TabIndex = 3;
			this.linkLabelDistribute.TabStop = true;
			this.linkLabelDistribute.Text = "Distribute";
			this.linkLabelDistribute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelDistribute.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistribute.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelDistribute_LinkClicked);
			// 
			// labelNodeName
			// 
			this.labelNodeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.labelNodeName.AutoSize = true;
			this.labelNodeName.Location = new System.Drawing.Point(0, 4);
			this.labelNodeName.Margin = new System.Windows.Forms.Padding(0);
			this.labelNodeName.Name = "labelNodeName";
			this.labelNodeName.Size = new System.Drawing.Size(82, 13);
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
			this.tableLayoutPanel.Controls.Add(this.panelMachineName, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.panelControl, 1, 0);
			this.tableLayoutPanel.Location = new System.Drawing.Point(108, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(242, 22);
			this.tableLayoutPanel.TabIndex = 3;
			// 
			// panelMachineName
			// 
			this.panelMachineName.Controls.Add(this.labelNodeName);
			this.panelMachineName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMachineName.Location = new System.Drawing.Point(0, 0);
			this.panelMachineName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.panelMachineName.Name = "panelMachineName";
			this.panelMachineName.Size = new System.Drawing.Size(90, 22);
			this.panelMachineName.TabIndex = 5;
			// 
			// panelControl
			// 
			this.panelControl.Controls.Add(this.labeledDivider);
			this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl.Location = new System.Drawing.Point(93, 0);
			this.panelControl.Margin = new System.Windows.Forms.Padding(0);
			this.panelControl.Name = "panelControl";
			this.panelControl.Size = new System.Drawing.Size(149, 22);
			this.panelControl.TabIndex = 3;
			// 
			// labeledDivider
			// 
			this.labeledDivider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDivider.BackColor = System.Drawing.Color.Transparent;
			this.labeledDivider.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDivider.Location = new System.Drawing.Point(0, 4);
			this.labeledDivider.Name = "labeledDivider";
			this.labeledDivider.Size = new System.Drawing.Size(149, 15);
			this.labeledDivider.TabIndex = 6;
			this.labeledDivider.Text = "";
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
			// BaseDistributionRootNode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.linkLabelDistribute);
			this.Controls.Add(this.flowLayoutPanel);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.checkBoxSelected);
			this.Controls.Add(this.pictureBoxExpandCollapse);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "BaseDistributionRootNode";
			this.Size = new System.Drawing.Size(350, 100);
			this.tableLayoutPanel.ResumeLayout(false);
			this.panelMachineName.ResumeLayout(false);
			this.panelMachineName.PerformLayout();
			this.panelControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpandCollapse)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxExpandCollapse;
		private System.Windows.Forms.CheckBox checkBoxSelected;
		private System.Windows.Forms.LinkLabel linkLabelDistribute;
		private System.Windows.Forms.Label labelNodeName;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Panel panelControl;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private LabeledDivider labeledDivider;
		private System.Windows.Forms.Panel panelMachineName;
	}
}

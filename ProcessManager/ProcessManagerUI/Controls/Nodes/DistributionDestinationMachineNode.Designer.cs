namespace ProcessManagerUI.Controls.Nodes
{
	partial class DistributionDestinationMachineNode
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
			this.linkLabelDistribute = new System.Windows.Forms.LinkLabel();
			this.labelMachineName = new System.Windows.Forms.Label();
			this.checkBoxSelected = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// linkLabelDistribute
			// 
			this.linkLabelDistribute.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistribute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabelDistribute.AutoSize = true;
			this.linkLabelDistribute.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelDistribute.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelDistribute.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistribute.Location = new System.Drawing.Point(23, 4);
			this.linkLabelDistribute.Name = "linkLabelDistribute";
			this.linkLabelDistribute.Size = new System.Drawing.Size(58, 15);
			this.linkLabelDistribute.TabIndex = 9;
			this.linkLabelDistribute.TabStop = true;
			this.linkLabelDistribute.Text = "Distribute";
			this.linkLabelDistribute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelDistribute.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelDistribute.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelDistribute_LinkClicked);
			// 
			// labelMachineName
			// 
			this.labelMachineName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.labelMachineName.AutoSize = true;
			this.labelMachineName.Location = new System.Drawing.Point(85, 4);
			this.labelMachineName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelMachineName.Name = "labelMachineName";
			this.labelMachineName.Size = new System.Drawing.Size(69, 15);
			this.labelMachineName.TabIndex = 8;
			this.labelMachineName.Text = "<machine>";
			this.labelMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkBoxSelected
			// 
			this.checkBoxSelected.AutoSize = true;
			this.checkBoxSelected.Location = new System.Drawing.Point(4, 5);
			this.checkBoxSelected.Name = "checkBoxSelected";
			this.checkBoxSelected.Size = new System.Drawing.Size(15, 14);
			this.checkBoxSelected.TabIndex = 7;
			this.checkBoxSelected.UseVisualStyleBackColor = true;
			this.checkBoxSelected.CheckedChanged += new System.EventHandler(this.CheckBoxSelected_CheckedChanged);
			// 
			// DistributionDestinationMachineNode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.linkLabelDistribute);
			this.Controls.Add(this.labelMachineName);
			this.Controls.Add(this.checkBoxSelected);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "DistributionDestinationMachineNode";
			this.Size = new System.Drawing.Size(350, 22);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel linkLabelDistribute;
		private System.Windows.Forms.Label labelMachineName;
		private System.Windows.Forms.CheckBox checkBoxSelected;
	}
}

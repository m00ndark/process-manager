namespace ProcessManagerUI.Controls.Nodes
{
    partial class MacroActionNode
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
			this.linkLabelPlay = new System.Windows.Forms.LinkLabel();
			this.labelActionName = new System.Windows.Forms.Label();
			this.pictureBoxStatus = new System.Windows.Forms.PictureBox();
			this.checkBoxSelected = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxStatus)).BeginInit();
			this.SuspendLayout();
			// 
			// linkLabelPlay
			// 
			this.linkLabelPlay.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabelPlay.AutoSize = true;
			this.linkLabelPlay.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelPlay.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelPlay.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelPlay.Location = new System.Drawing.Point(68, 4);
			this.linkLabelPlay.Name = "linkLabelPlay";
			this.linkLabelPlay.Size = new System.Drawing.Size(29, 15);
			this.linkLabelPlay.TabIndex = 9;
			this.linkLabelPlay.TabStop = true;
			this.linkLabelPlay.Text = "Play";
			this.linkLabelPlay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelPlay.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelPlay.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelPlay_LinkClicked);
			// 
			// labelActionName
			// 
			this.labelActionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.labelActionName.AutoSize = true;
			this.labelActionName.Location = new System.Drawing.Point(101, 4);
			this.labelActionName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelActionName.Name = "labelActionName";
			this.labelActionName.Size = new System.Drawing.Size(56, 15);
			this.labelActionName.TabIndex = 8;
			this.labelActionName.Text = "<action>";
			this.labelActionName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pictureBoxStatus
			// 
			this.pictureBoxStatus.Location = new System.Drawing.Point(46, 3);
			this.pictureBoxStatus.Name = "pictureBoxStatus";
			this.pictureBoxStatus.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxStatus.TabIndex = 12;
			this.pictureBoxStatus.TabStop = false;
			// 
			// checkBoxSelected
			// 
			this.checkBoxSelected.AutoSize = true;
			this.checkBoxSelected.Location = new System.Drawing.Point(25, 5);
			this.checkBoxSelected.Name = "checkBoxSelected";
			this.checkBoxSelected.Size = new System.Drawing.Size(15, 14);
			this.checkBoxSelected.TabIndex = 11;
			this.checkBoxSelected.UseVisualStyleBackColor = true;
			this.checkBoxSelected.CheckedChanged += new System.EventHandler(this.CheckBoxSelected_CheckedChanged);
			// 
			// MacroActionNode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pictureBoxStatus);
			this.Controls.Add(this.checkBoxSelected);
			this.Controls.Add(this.linkLabelPlay);
			this.Controls.Add(this.labelActionName);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "MacroActionNode";
			this.Size = new System.Drawing.Size(350, 22);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxStatus)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel linkLabelPlay;
        private System.Windows.Forms.Label labelActionName;
		private System.Windows.Forms.PictureBox pictureBoxStatus;
		private System.Windows.Forms.CheckBox checkBoxSelected;
	}
}

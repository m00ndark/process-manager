namespace ProcessManagerUI.Controls.Nodes
{
	partial class ProcessApplicationNode
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
			this.pictureBoxStatus = new System.Windows.Forms.PictureBox();
			this.linkLabelStop = new System.Windows.Forms.LinkLabel();
			this.linkLabelStart = new System.Windows.Forms.LinkLabel();
			this.labelApplicationName = new System.Windows.Forms.Label();
			this.checkBoxSelected = new System.Windows.Forms.CheckBox();
			this.linkLabelRestart = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxStatus)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBoxStatus
			// 
			this.pictureBoxStatus.Location = new System.Drawing.Point(46, 3);
			this.pictureBoxStatus.Name = "pictureBoxStatus";
			this.pictureBoxStatus.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxStatus.TabIndex = 6;
			this.pictureBoxStatus.TabStop = false;
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
			this.linkLabelStop.Location = new System.Drawing.Point(98, 4);
			this.linkLabelStop.Name = "linkLabelStop";
			this.linkLabelStop.Size = new System.Drawing.Size(31, 13);
			this.linkLabelStop.TabIndex = 10;
			this.linkLabelStop.TabStop = true;
			this.linkLabelStop.Text = "Stop";
			this.linkLabelStop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelStop.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStop.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelStop_LinkClicked);
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
			this.linkLabelStart.Location = new System.Drawing.Point(68, 4);
			this.linkLabelStart.Name = "linkLabelStart";
			this.linkLabelStart.Size = new System.Drawing.Size(31, 13);
			this.linkLabelStart.TabIndex = 9;
			this.linkLabelStart.TabStop = true;
			this.linkLabelStart.Text = "Start";
			this.linkLabelStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelStart.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelStart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelStart_LinkClicked);
			// 
			// labelApplicationName
			// 
			this.labelApplicationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.labelApplicationName.AutoSize = true;
			this.labelApplicationName.Location = new System.Drawing.Point(175, 4);
			this.labelApplicationName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelApplicationName.Name = "labelApplicationName";
			this.labelApplicationName.Size = new System.Drawing.Size(81, 13);
			this.labelApplicationName.TabIndex = 8;
			this.labelApplicationName.Text = "<application>";
			this.labelApplicationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkBoxSelected
			// 
			this.checkBoxSelected.AutoSize = true;
			this.checkBoxSelected.Location = new System.Drawing.Point(25, 5);
			this.checkBoxSelected.Name = "checkBoxSelected";
			this.checkBoxSelected.Size = new System.Drawing.Size(15, 14);
			this.checkBoxSelected.TabIndex = 7;
			this.checkBoxSelected.UseVisualStyleBackColor = true;
			this.checkBoxSelected.CheckedChanged += new System.EventHandler(this.CheckBoxSelected_CheckedChanged);
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
			this.linkLabelRestart.Location = new System.Drawing.Point(128, 4);
			this.linkLabelRestart.Name = "linkLabelRestart";
			this.linkLabelRestart.Size = new System.Drawing.Size(43, 13);
			this.linkLabelRestart.TabIndex = 11;
			this.linkLabelRestart.TabStop = true;
			this.linkLabelRestart.Text = "Restart";
			this.linkLabelRestart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelRestart.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelRestart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelRestart_LinkClicked);
			// 
			// ProcessApplicationNode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pictureBoxStatus);
			this.Controls.Add(this.linkLabelStop);
			this.Controls.Add(this.linkLabelStart);
			this.Controls.Add(this.labelApplicationName);
			this.Controls.Add(this.checkBoxSelected);
			this.Controls.Add(this.linkLabelRestart);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ProcessApplicationNode";
			this.Size = new System.Drawing.Size(350, 22);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxStatus)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxStatus;
		private System.Windows.Forms.LinkLabel linkLabelStop;
		private System.Windows.Forms.LinkLabel linkLabelStart;
		private System.Windows.Forms.Label labelApplicationName;
		private System.Windows.Forms.CheckBox checkBoxSelected;
		private System.Windows.Forms.LinkLabel linkLabelRestart;
	}
}

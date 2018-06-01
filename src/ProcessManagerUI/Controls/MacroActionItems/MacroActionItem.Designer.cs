namespace ProcessManagerUI.Controls.MacroActionItems
{
	partial class MacroActionItem
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
			this.labelSeparator = new System.Windows.Forms.Label();
			this.panelAction = new System.Windows.Forms.Panel();
			this.linkLabelActionType = new System.Windows.Forms.LinkLabel();
			this.buttonMoveUp = new System.Windows.Forms.Button();
			this.buttonMoveDown = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelSeparator
			// 
			this.labelSeparator.Location = new System.Drawing.Point(139, 0);
			this.labelSeparator.Name = "labelSeparator";
			this.labelSeparator.Size = new System.Drawing.Size(10, 21);
			this.labelSeparator.TabIndex = 5;
			this.labelSeparator.Text = ">";
			this.labelSeparator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelSeparator.Visible = false;
			// 
			// panelAction
			// 
			this.panelAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelAction.Location = new System.Drawing.Point(149, 0);
			this.panelAction.Name = "panelAction";
			this.panelAction.Size = new System.Drawing.Size(432, 23);
			this.panelAction.TabIndex = 4;
			// 
			// linkLabelActionType
			// 
			this.linkLabelActionType.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelActionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabelActionType.AutoEllipsis = true;
			this.linkLabelActionType.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelActionType.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelActionType.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelActionType.Location = new System.Drawing.Point(71, 0);
			this.linkLabelActionType.Name = "linkLabelActionType";
			this.linkLabelActionType.Size = new System.Drawing.Size(62, 21);
			this.linkLabelActionType.TabIndex = 29;
			this.linkLabelActionType.TabStop = true;
			this.linkLabelActionType.Text = "Type...";
			this.linkLabelActionType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelActionType.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelActionType.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelActionType_LinkClicked);
			// 
			// buttonMoveUp
			// 
			this.buttonMoveUp.Image = global::ProcessManagerUI.Properties.Resources.arrow_up_7_4;
			this.buttonMoveUp.Location = new System.Drawing.Point(33, -1);
			this.buttonMoveUp.Name = "buttonMoveUp";
			this.buttonMoveUp.Size = new System.Drawing.Size(27, 12);
			this.buttonMoveUp.TabIndex = 30;
			this.buttonMoveUp.UseVisualStyleBackColor = true;
			this.buttonMoveUp.Click += new System.EventHandler(this.ButtonMoveUp_Click);
			// 
			// buttonMoveDown
			// 
			this.buttonMoveDown.Image = global::ProcessManagerUI.Properties.Resources.arrow_down_7_4;
			this.buttonMoveDown.Location = new System.Drawing.Point(33, 11);
			this.buttonMoveDown.Name = "buttonMoveDown";
			this.buttonMoveDown.Size = new System.Drawing.Size(27, 12);
			this.buttonMoveDown.TabIndex = 31;
			this.buttonMoveDown.UseVisualStyleBackColor = true;
			this.buttonMoveDown.Click += new System.EventHandler(this.ButtonMoveDown_Click);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Image = global::ProcessManagerUI.Properties.Resources.remove_16;
			this.buttonRemove.Location = new System.Drawing.Point(-1, -1);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(28, 24);
			this.buttonRemove.TabIndex = 28;
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.ButtonRemove_Click);
			// 
			// MacroActionItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.buttonMoveUp);
			this.Controls.Add(this.buttonMoveDown);
			this.Controls.Add(this.panelAction);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.labelSeparator);
			this.Controls.Add(this.linkLabelActionType);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "MacroActionItem";
			this.Size = new System.Drawing.Size(581, 25);
			this.Load += new System.EventHandler(this.MacroActionItem_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelSeparator;
		private System.Windows.Forms.Panel panelAction;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.LinkLabel linkLabelActionType;
		private System.Windows.Forms.Button buttonMoveUp;
		private System.Windows.Forms.Button buttonMoveDown;
	}
}

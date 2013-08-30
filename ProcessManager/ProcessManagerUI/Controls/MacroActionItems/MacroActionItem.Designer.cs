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
			this.labelDivider = new System.Windows.Forms.Label();
			this.panelAction = new System.Windows.Forms.Panel();
			this.comboBoxActionTypes = new System.Windows.Forms.ComboBox();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelDivider
			// 
			this.labelDivider.AutoSize = true;
			this.labelDivider.Location = new System.Drawing.Point(108, 4);
			this.labelDivider.Name = "labelDivider";
			this.labelDivider.Size = new System.Drawing.Size(10, 13);
			this.labelDivider.TabIndex = 5;
			this.labelDivider.Text = ":";
			this.labelDivider.Visible = false;
			// 
			// panelAction
			// 
			this.panelAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelAction.Location = new System.Drawing.Point(117, 0);
			this.panelAction.Name = "panelAction";
			this.panelAction.Size = new System.Drawing.Size(296, 21);
			this.panelAction.TabIndex = 4;
			// 
			// comboBoxActionTypes
			// 
			this.comboBoxActionTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxActionTypes.FormattingEnabled = true;
			this.comboBoxActionTypes.Location = new System.Drawing.Point(33, 0);
			this.comboBoxActionTypes.Name = "comboBoxActionTypes";
			this.comboBoxActionTypes.Size = new System.Drawing.Size(75, 21);
			this.comboBoxActionTypes.TabIndex = 3;
			this.comboBoxActionTypes.SelectedIndexChanged += new System.EventHandler(this.ComboBoxActionTypes_SelectedIndexChanged);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Image = global::ProcessManagerUI.Properties.Resources.remove_16;
			this.buttonRemove.Location = new System.Drawing.Point(-1, -1);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(28, 24);
			this.buttonRemove.TabIndex = 28;
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// MacroActionItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.panelAction);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.labelDivider);
			this.Controls.Add(this.comboBoxActionTypes);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "MacroActionItem";
			this.Size = new System.Drawing.Size(413, 25);
			this.Load += new System.EventHandler(this.MacroActionItem_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelDivider;
		private System.Windows.Forms.Panel panelAction;
		private System.Windows.Forms.ComboBox comboBoxActionTypes;
		private System.Windows.Forms.Button buttonRemove;
	}
}

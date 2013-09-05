namespace ProcessManagerUI.Controls.MacroActionItems
{
    partial class MacroProcessActionItem
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxMachine = new System.Windows.Forms.ComboBox();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxApplication = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel.ColumnCount = 5;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Controls.Add(this.comboBoxMachine, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.comboBoxGroup, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.comboBoxApplication, 4, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(301, 21);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // comboBoxMachine
            // 
            this.comboBoxMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMachine.FormattingEnabled = true;
            this.comboBoxMachine.Location = new System.Drawing.Point(0, 0);
            this.comboBoxMachine.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxMachine.Name = "comboBoxMachine";
            this.comboBoxMachine.Size = new System.Drawing.Size(96, 21);
            this.comboBoxMachine.TabIndex = 0;
            this.comboBoxMachine.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMachine_SelectedIndexChanged);
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(102, 0);
            this.comboBoxGroup.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(96, 21);
            this.comboBoxGroup.TabIndex = 1;
            this.comboBoxGroup.SelectedIndexChanged += new System.EventHandler(this.ComboBoxGroup_SelectedIndexChanged);
            // 
            // comboBoxApplication
            // 
            this.comboBoxApplication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxApplication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxApplication.FormattingEnabled = true;
            this.comboBoxApplication.Location = new System.Drawing.Point(204, 0);
            this.comboBoxApplication.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxApplication.Name = "comboBoxApplication";
            this.comboBoxApplication.Size = new System.Drawing.Size(97, 21);
            this.comboBoxApplication.TabIndex = 2;
            this.comboBoxApplication.SelectedIndexChanged += new System.EventHandler(this.ComboBoxApplication_SelectedIndexChanged);
            // 
            // MacroProcessActionItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MacroProcessActionItem";
            this.Size = new System.Drawing.Size(301, 21);
            this.Load += new System.EventHandler(this.MacroDistributionActionItem_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.ComboBox comboBoxMachine;
		private System.Windows.Forms.ComboBox comboBoxGroup;
        private System.Windows.Forms.ComboBox comboBoxApplication;
	}
}

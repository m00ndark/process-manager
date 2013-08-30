namespace ProcessManagerUI.Controls.MacroActionItems
{
	partial class MacroDistributionActionItem
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
			this.comboBoxSourceMachine = new System.Windows.Forms.ComboBox();
			this.comboBoxGroup = new System.Windows.Forms.ComboBox();
			this.comboBoxApplication = new System.Windows.Forms.ComboBox();
			this.comboBoxDestinationMachine = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel.ColumnCount = 7;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel.Controls.Add(this.comboBoxSourceMachine, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.comboBoxGroup, 2, 0);
			this.tableLayoutPanel.Controls.Add(this.comboBoxApplication, 4, 0);
			this.tableLayoutPanel.Controls.Add(this.comboBoxDestinationMachine, 6, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(301, 21);
			this.tableLayoutPanel.TabIndex = 2;
			// 
			// comboBoxSourceMachine
			// 
			this.comboBoxSourceMachine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxSourceMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSourceMachine.FormattingEnabled = true;
			this.comboBoxSourceMachine.Location = new System.Drawing.Point(0, 0);
			this.comboBoxSourceMachine.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxSourceMachine.Name = "comboBoxSourceMachine";
			this.comboBoxSourceMachine.Size = new System.Drawing.Size(70, 21);
			this.comboBoxSourceMachine.TabIndex = 0;
			this.comboBoxSourceMachine.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSourceMachine_SelectedIndexChanged);
			// 
			// comboBoxGroup
			// 
			this.comboBoxGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGroup.FormattingEnabled = true;
			this.comboBoxGroup.Location = new System.Drawing.Point(76, 0);
			this.comboBoxGroup.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxGroup.Name = "comboBoxGroup";
			this.comboBoxGroup.Size = new System.Drawing.Size(70, 21);
			this.comboBoxGroup.TabIndex = 1;
			this.comboBoxGroup.SelectedIndexChanged += new System.EventHandler(this.ComboBoxGroup_SelectedIndexChanged);
			// 
			// comboBoxApplication
			// 
			this.comboBoxApplication.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxApplication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxApplication.FormattingEnabled = true;
			this.comboBoxApplication.Location = new System.Drawing.Point(152, 0);
			this.comboBoxApplication.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxApplication.Name = "comboBoxApplication";
			this.comboBoxApplication.Size = new System.Drawing.Size(70, 21);
			this.comboBoxApplication.TabIndex = 2;
			this.comboBoxApplication.SelectedIndexChanged += new System.EventHandler(this.ComboBoxApplication_SelectedIndexChanged);
			// 
			// comboBoxDestinationMachine
			// 
			this.comboBoxDestinationMachine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxDestinationMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDestinationMachine.FormattingEnabled = true;
			this.comboBoxDestinationMachine.Location = new System.Drawing.Point(228, 0);
			this.comboBoxDestinationMachine.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxDestinationMachine.Name = "comboBoxDestinationMachine";
			this.comboBoxDestinationMachine.Size = new System.Drawing.Size(73, 21);
			this.comboBoxDestinationMachine.TabIndex = 3;
			this.comboBoxDestinationMachine.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDestinationMachine_SelectedIndexChanged);
			// 
			// MacroDistributionActionItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.tableLayoutPanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "MacroDistributionActionItem";
			this.Size = new System.Drawing.Size(301, 21);
			this.Load += new System.EventHandler(this.MacroDistributionActionItem_Load);
			this.tableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.ComboBox comboBoxSourceMachine;
		private System.Windows.Forms.ComboBox comboBoxGroup;
		private System.Windows.Forms.ComboBox comboBoxApplication;
		private System.Windows.Forms.ComboBox comboBoxDestinationMachine;
	}
}

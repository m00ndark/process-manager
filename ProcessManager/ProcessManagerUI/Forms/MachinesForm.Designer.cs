namespace ProcessManagerUI.Forms
{
	partial class MachinesForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listViewMachines = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderMachines = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonRemoveMachine = new System.Windows.Forms.Button();
			this.buttonAddMachine = new System.Windows.Forms.Button();
			this.panelMachines = new Controls.BackgroundPanel();
			this.buttonCopyMachineSetup = new System.Windows.Forms.Button();
			this.buttonValidateMachine = new System.Windows.Forms.Button();
			this.labeledDividerMachine = new ProcessManagerUI.Controls.LabeledDivider();
			this.textBoxMachineHostName = new System.Windows.Forms.TextBox();
			this.labelMachineHostName = new System.Windows.Forms.Label();
			this.panelMachines.SuspendLayout();
			this.SuspendLayout();
			// 
			// listViewMachines
			// 
			this.listViewMachines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewMachines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMachines});
			this.listViewMachines.FullRowSelect = true;
			this.listViewMachines.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewMachines.HideSelection = false;
			this.listViewMachines.Location = new System.Drawing.Point(12, 12);
			this.listViewMachines.MultiSelect = false;
			this.listViewMachines.Name = "listViewMachines";
			this.listViewMachines.Size = new System.Drawing.Size(120, 175);
			this.listViewMachines.TabIndex = 1;
			this.listViewMachines.UseCompatibleStateImageBehavior = false;
			this.listViewMachines.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderMachines
			// 
			this.columnHeaderMachines.Text = "Machines";
			this.columnHeaderMachines.Width = 100;
			// 
			// buttonRemoveMachine
			// 
			this.buttonRemoveMachine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemoveMachine.Location = new System.Drawing.Point(76, 193);
			this.buttonRemoveMachine.Name = "buttonRemoveMachine";
			this.buttonRemoveMachine.Size = new System.Drawing.Size(58, 23);
			this.buttonRemoveMachine.TabIndex = 4;
			this.buttonRemoveMachine.Text = "Remove";
			this.buttonRemoveMachine.UseVisualStyleBackColor = true;
			// 
			// buttonAddMachine
			// 
			this.buttonAddMachine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddMachine.Location = new System.Drawing.Point(12, 193);
			this.buttonAddMachine.Name = "buttonAddMachine";
			this.buttonAddMachine.Size = new System.Drawing.Size(58, 23);
			this.buttonAddMachine.TabIndex = 3;
			this.buttonAddMachine.Text = "Add";
			this.buttonAddMachine.UseVisualStyleBackColor = true;
			// 
			// panelMachines
			// 
			this.panelMachines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelMachines.BackColor = System.Drawing.SystemColors.Window;
			this.panelMachines.Controls.Add(this.buttonCopyMachineSetup);
			this.panelMachines.Controls.Add(this.buttonValidateMachine);
			this.panelMachines.Controls.Add(this.labeledDividerMachine);
			this.panelMachines.Controls.Add(this.textBoxMachineHostName);
			this.panelMachines.Controls.Add(this.labelMachineHostName);
			this.panelMachines.Location = new System.Drawing.Point(140, 12);
			this.panelMachines.Name = "panelMachines";
			this.panelMachines.Padding = new System.Windows.Forms.Padding(5);
			this.panelMachines.Size = new System.Drawing.Size(322, 204);
			this.panelMachines.TabIndex = 18;
			// 
			// buttonCopyMachineSetup
			// 
			this.buttonCopyMachineSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCopyMachineSetup.Location = new System.Drawing.Point(216, 113);
			this.buttonCopyMachineSetup.Name = "buttonCopyMachineSetup";
			this.buttonCopyMachineSetup.Size = new System.Drawing.Size(92, 23);
			this.buttonCopyMachineSetup.TabIndex = 12;
			this.buttonCopyMachineSetup.Text = "Copy Setup";
			this.buttonCopyMachineSetup.UseVisualStyleBackColor = true;
			// 
			// buttonValidateMachine
			// 
			this.buttonValidateMachine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonValidateMachine.Location = new System.Drawing.Point(216, 84);
			this.buttonValidateMachine.Name = "buttonValidateMachine";
			this.buttonValidateMachine.Size = new System.Drawing.Size(92, 23);
			this.buttonValidateMachine.TabIndex = 11;
			this.buttonValidateMachine.Text = "Validate";
			this.buttonValidateMachine.UseVisualStyleBackColor = true;
			// 
			// labeledDividerMachine
			// 
			this.labeledDividerMachine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerMachine.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerMachine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(170)))));
			this.labeledDividerMachine.Location = new System.Drawing.Point(8, 8);
			this.labeledDividerMachine.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerMachine.Name = "labeledDividerMachine";
			this.labeledDividerMachine.Size = new System.Drawing.Size(306, 15);
			this.labeledDividerMachine.TabIndex = 10;
			this.labeledDividerMachine.Text = "Machine";
			// 
			// textBoxMachineHostName
			// 
			this.textBoxMachineHostName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMachineHostName.Location = new System.Drawing.Point(95, 31);
			this.textBoxMachineHostName.Name = "textBoxMachineHostName";
			this.textBoxMachineHostName.Size = new System.Drawing.Size(213, 20);
			this.textBoxMachineHostName.TabIndex = 6;
			// 
			// labelMachineHostName
			// 
			this.labelMachineHostName.AutoSize = true;
			this.labelMachineHostName.Location = new System.Drawing.Point(28, 34);
			this.labelMachineHostName.Name = "labelMachineHostName";
			this.labelMachineHostName.Size = new System.Drawing.Size(61, 13);
			this.labelMachineHostName.TabIndex = 5;
			this.labelMachineHostName.Text = "Host name:";
			// 
			// MachinesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 228);
			this.Controls.Add(this.panelMachines);
			this.Controls.Add(this.buttonRemoveMachine);
			this.Controls.Add(this.buttonAddMachine);
			this.Controls.Add(this.listViewMachines);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MachinesForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Machines";
			this.panelMachines.ResumeLayout(false);
			this.panelMachines.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.ListView listViewMachines;
		private System.Windows.Forms.ColumnHeader columnHeaderMachines;
		private System.Windows.Forms.Button buttonRemoveMachine;
		private System.Windows.Forms.Button buttonAddMachine;
		private Controls.BackgroundPanel panelMachines;
		private System.Windows.Forms.Button buttonValidateMachine;
		private Controls.LabeledDivider labeledDividerMachine;
		private System.Windows.Forms.TextBox textBoxMachineHostName;
		private System.Windows.Forms.Label labelMachineHostName;
		private System.Windows.Forms.Button buttonCopyMachineSetup;
	}
}
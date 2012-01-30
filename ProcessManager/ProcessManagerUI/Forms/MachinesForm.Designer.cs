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
			this.buttonRemoveMachine = new System.Windows.Forms.Button();
			this.buttonAddMachine = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonApply = new System.Windows.Forms.Button();
			this.labelNoMachineSelected = new System.Windows.Forms.Label();
			this.panelMachine = new ProcessManagerUI.Controls.BackgroundPanel();
			this.buttonCopyMachineSetup = new System.Windows.Forms.Button();
			this.buttonValidateMachine = new System.Windows.Forms.Button();
			this.labeledDividerMachine = new ProcessManagerUI.Controls.LabeledDivider();
			this.textBoxMachineHostName = new System.Windows.Forms.TextBox();
			this.labelMachineHostName = new System.Windows.Forms.Label();
			this.listViewMachines = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderMachines = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panelMachine.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonRemoveMachine
			// 
			this.buttonRemoveMachine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemoveMachine.Location = new System.Drawing.Point(75, 198);
			this.buttonRemoveMachine.Name = "buttonRemoveMachine";
			this.buttonRemoveMachine.Size = new System.Drawing.Size(58, 23);
			this.buttonRemoveMachine.TabIndex = 4;
			this.buttonRemoveMachine.Text = "Remove";
			this.buttonRemoveMachine.UseVisualStyleBackColor = true;
			this.buttonRemoveMachine.Click += new System.EventHandler(this.ButtonRemoveMachine_Click);
			// 
			// buttonAddMachine
			// 
			this.buttonAddMachine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddMachine.Location = new System.Drawing.Point(12, 198);
			this.buttonAddMachine.Name = "buttonAddMachine";
			this.buttonAddMachine.Size = new System.Drawing.Size(58, 23);
			this.buttonAddMachine.TabIndex = 3;
			this.buttonAddMachine.Text = "Add";
			this.buttonAddMachine.UseVisualStyleBackColor = true;
			this.buttonAddMachine.Click += new System.EventHandler(this.ButtonAddMachine_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(185, 227);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(92, 23);
			this.buttonOK.TabIndex = 23;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(283, 227);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(92, 23);
			this.buttonCancel.TabIndex = 22;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// buttonApply
			// 
			this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonApply.Enabled = false;
			this.buttonApply.Location = new System.Drawing.Point(381, 227);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(92, 23);
			this.buttonApply.TabIndex = 21;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
			// 
			// labelNoMachineSelected
			// 
			this.labelNoMachineSelected.Location = new System.Drawing.Point(140, 12);
			this.labelNoMachineSelected.Name = "labelNoMachineSelected";
			this.labelNoMachineSelected.Size = new System.Drawing.Size(332, 209);
			this.labelNoMachineSelected.TabIndex = 24;
			this.labelNoMachineSelected.Text = "No machine selected";
			this.labelNoMachineSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelMachine
			// 
			this.panelMachine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelMachine.BackColor = System.Drawing.SystemColors.Window;
			this.panelMachine.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
			this.panelMachine.Controls.Add(this.buttonCopyMachineSetup);
			this.panelMachine.Controls.Add(this.buttonValidateMachine);
			this.panelMachine.Controls.Add(this.labeledDividerMachine);
			this.panelMachine.Controls.Add(this.textBoxMachineHostName);
			this.panelMachine.Controls.Add(this.labelMachineHostName);
			this.panelMachine.Location = new System.Drawing.Point(140, 12);
			this.panelMachine.Name = "panelMachine";
			this.panelMachine.Padding = new System.Windows.Forms.Padding(5);
			this.panelMachine.Size = new System.Drawing.Size(332, 209);
			this.panelMachine.TabIndex = 18;
			this.panelMachine.Visible = false;
			// 
			// buttonCopyMachineSetup
			// 
			this.buttonCopyMachineSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCopyMachineSetup.Location = new System.Drawing.Point(226, 113);
			this.buttonCopyMachineSetup.Name = "buttonCopyMachineSetup";
			this.buttonCopyMachineSetup.Size = new System.Drawing.Size(92, 23);
			this.buttonCopyMachineSetup.TabIndex = 12;
			this.buttonCopyMachineSetup.Text = "Copy Setup";
			this.buttonCopyMachineSetup.UseVisualStyleBackColor = true;
			this.buttonCopyMachineSetup.Click += new System.EventHandler(this.ButtonCopyMachineSetup_Click);
			// 
			// buttonValidateMachine
			// 
			this.buttonValidateMachine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonValidateMachine.Location = new System.Drawing.Point(226, 84);
			this.buttonValidateMachine.Name = "buttonValidateMachine";
			this.buttonValidateMachine.Size = new System.Drawing.Size(92, 23);
			this.buttonValidateMachine.TabIndex = 11;
			this.buttonValidateMachine.Text = "Validate";
			this.buttonValidateMachine.UseVisualStyleBackColor = true;
			this.buttonValidateMachine.Click += new System.EventHandler(this.ButtonValidateMachine_Click);
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
			this.labeledDividerMachine.Size = new System.Drawing.Size(316, 15);
			this.labeledDividerMachine.TabIndex = 10;
			this.labeledDividerMachine.Text = "Machine";
			// 
			// textBoxMachineHostName
			// 
			this.textBoxMachineHostName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMachineHostName.Location = new System.Drawing.Point(99, 31);
			this.textBoxMachineHostName.Name = "textBoxMachineHostName";
			this.textBoxMachineHostName.Size = new System.Drawing.Size(219, 22);
			this.textBoxMachineHostName.TabIndex = 6;
			this.textBoxMachineHostName.TextChanged += new System.EventHandler(this.TextBoxMachineHostName_TextChanged);
			this.textBoxMachineHostName.Leave += new System.EventHandler(this.TextBoxMachineHostName_Leave);
			// 
			// labelMachineHostName
			// 
			this.labelMachineHostName.AutoSize = true;
			this.labelMachineHostName.Location = new System.Drawing.Point(28, 34);
			this.labelMachineHostName.Name = "labelMachineHostName";
			this.labelMachineHostName.Size = new System.Drawing.Size(65, 13);
			this.labelMachineHostName.TabIndex = 5;
			this.labelMachineHostName.Text = "Host name:";
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
			this.listViewMachines.Size = new System.Drawing.Size(120, 180);
			this.listViewMachines.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewMachines.TabIndex = 1;
			this.listViewMachines.UseCompatibleStateImageBehavior = false;
			this.listViewMachines.View = System.Windows.Forms.View.Details;
			this.listViewMachines.SelectedIndexChanged += new System.EventHandler(this.ListViewMachines_SelectedIndexChanged);
			// 
			// columnHeaderMachines
			// 
			this.columnHeaderMachines.Text = "Machines";
			this.columnHeaderMachines.Width = 99;
			// 
			// MachinesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(484, 262);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonApply);
			this.Controls.Add(this.panelMachine);
			this.Controls.Add(this.buttonRemoveMachine);
			this.Controls.Add(this.buttonAddMachine);
			this.Controls.Add(this.listViewMachines);
			this.Controls.Add(this.labelNoMachineSelected);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(500, 300);
			this.Name = "MachinesForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Machines";
			this.Load += new System.EventHandler(this.MachinesForm_Load);
			this.panelMachine.ResumeLayout(false);
			this.panelMachine.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.ListView listViewMachines;
		private System.Windows.Forms.ColumnHeader columnHeaderMachines;
		private System.Windows.Forms.Button buttonRemoveMachine;
		private System.Windows.Forms.Button buttonAddMachine;
		private Controls.BackgroundPanel panelMachine;
		private System.Windows.Forms.Button buttonValidateMachine;
		private Controls.LabeledDivider labeledDividerMachine;
		private System.Windows.Forms.TextBox textBoxMachineHostName;
		private System.Windows.Forms.Label labelMachineHostName;
		private System.Windows.Forms.Button buttonCopyMachineSetup;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.Label labelNoMachineSelected;
	}
}
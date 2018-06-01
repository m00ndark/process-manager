namespace ProcessManagerUI.Forms
{
	partial class MacrosForm
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
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonApply = new System.Windows.Forms.Button();
			this.labelNoMacroSelected = new System.Windows.Forms.Label();
			this.panelMacro = new ProcessManagerUI.Controls.BackgroundPanel();
			this.buttonAddMacroAction = new System.Windows.Forms.Button();
			this.flowLayoutPanelMacroActions = new System.Windows.Forms.FlowLayoutPanel();
			this.labeledDividerMacroActions = new ProcessManagerUI.Controls.LabeledDivider();
			this.labeledDividerMacro = new ProcessManagerUI.Controls.LabeledDivider();
			this.labelMacroName = new System.Windows.Forms.Label();
			this.textBoxMacroName = new System.Windows.Forms.TextBox();
			this.buttonRemoveMacro = new System.Windows.Forms.Button();
			this.buttonAddMacro = new System.Windows.Forms.Button();
			this.listViewMacros = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderMacros = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonCopyMacro = new System.Windows.Forms.Button();
			this.panelMacro.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(563, 428);
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
			this.buttonCancel.Location = new System.Drawing.Point(661, 428);
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
			this.buttonApply.Location = new System.Drawing.Point(759, 428);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(92, 23);
			this.buttonApply.TabIndex = 21;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
			// 
			// labelNoMacroSelected
			// 
			this.labelNoMacroSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelNoMacroSelected.Location = new System.Drawing.Point(188, 12);
			this.labelNoMacroSelected.Name = "labelNoMacroSelected";
			this.labelNoMacroSelected.Size = new System.Drawing.Size(662, 410);
			this.labelNoMacroSelected.TabIndex = 24;
			this.labelNoMacroSelected.Text = "No macro selected";
			this.labelNoMacroSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelMacro
			// 
			this.panelMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelMacro.BackColor = System.Drawing.SystemColors.Window;
			this.panelMacro.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
			this.panelMacro.Controls.Add(this.buttonAddMacroAction);
			this.panelMacro.Controls.Add(this.flowLayoutPanelMacroActions);
			this.panelMacro.Controls.Add(this.labeledDividerMacroActions);
			this.panelMacro.Controls.Add(this.labeledDividerMacro);
			this.panelMacro.Controls.Add(this.labelMacroName);
			this.panelMacro.Controls.Add(this.textBoxMacroName);
			this.panelMacro.Location = new System.Drawing.Point(188, 12);
			this.panelMacro.Name = "panelMacro";
			this.panelMacro.Padding = new System.Windows.Forms.Padding(5);
			this.panelMacro.Size = new System.Drawing.Size(662, 410);
			this.panelMacro.TabIndex = 25;
			this.panelMacro.Visible = false;
			// 
			// buttonAddMacroAction
			// 
			this.buttonAddMacroAction.Image = global::ProcessManagerUI.Properties.Resources.add_16;
			this.buttonAddMacroAction.Location = new System.Drawing.Point(31, 97);
			this.buttonAddMacroAction.Name = "buttonAddMacroAction";
			this.buttonAddMacroAction.Size = new System.Drawing.Size(28, 24);
			this.buttonAddMacroAction.TabIndex = 26;
			this.buttonAddMacroAction.UseVisualStyleBackColor = true;
			this.buttonAddMacroAction.Click += new System.EventHandler(this.ButtonAddMacroAction_Click);
			// 
			// flowLayoutPanelMacroActions
			// 
			this.flowLayoutPanelMacroActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelMacroActions.AutoScroll = true;
			this.flowLayoutPanelMacroActions.Location = new System.Drawing.Point(73, 98);
			this.flowLayoutPanelMacroActions.Name = "flowLayoutPanelMacroActions";
			this.flowLayoutPanelMacroActions.Size = new System.Drawing.Size(581, 303);
			this.flowLayoutPanelMacroActions.TabIndex = 25;
			this.flowLayoutPanelMacroActions.Resize += new System.EventHandler(this.FlowLayoutPanelMacroActions_Resize);
			// 
			// labeledDividerMacroActions
			// 
			this.labeledDividerMacroActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerMacroActions.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerMacroActions.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labeledDividerMacroActions.Location = new System.Drawing.Point(8, 75);
			this.labeledDividerMacroActions.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerMacroActions.Name = "labeledDividerMacroActions";
			this.labeledDividerMacroActions.Size = new System.Drawing.Size(646, 15);
			this.labeledDividerMacroActions.TabIndex = 24;
			this.labeledDividerMacroActions.Text = "Actions";
			// 
			// labeledDividerMacro
			// 
			this.labeledDividerMacro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerMacro.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerMacro.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labeledDividerMacro.Location = new System.Drawing.Point(8, 8);
			this.labeledDividerMacro.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerMacro.Name = "labeledDividerMacro";
			this.labeledDividerMacro.Size = new System.Drawing.Size(646, 15);
			this.labeledDividerMacro.TabIndex = 10;
			this.labeledDividerMacro.Text = "Macro";
			// 
			// labelMacroName
			// 
			this.labelMacroName.AutoSize = true;
			this.labelMacroName.Location = new System.Drawing.Point(28, 34);
			this.labelMacroName.Name = "labelMacroName";
			this.labelMacroName.Size = new System.Drawing.Size(39, 13);
			this.labelMacroName.TabIndex = 11;
			this.labelMacroName.Text = "Name:";
			// 
			// textBoxMacroName
			// 
			this.textBoxMacroName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMacroName.Location = new System.Drawing.Point(77, 31);
			this.textBoxMacroName.Name = "textBoxMacroName";
			this.textBoxMacroName.Size = new System.Drawing.Size(557, 22);
			this.textBoxMacroName.TabIndex = 12;
			this.textBoxMacroName.TextChanged += new System.EventHandler(this.TextBoxMacroName_TextChanged);
			this.textBoxMacroName.Leave += new System.EventHandler(this.TextBoxMacroName_Leave);
			// 
			// buttonRemoveMacro
			// 
			this.buttonRemoveMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemoveMacro.Location = new System.Drawing.Point(127, 400);
			this.buttonRemoveMacro.Name = "buttonRemoveMacro";
			this.buttonRemoveMacro.Size = new System.Drawing.Size(55, 23);
			this.buttonRemoveMacro.TabIndex = 2;
			this.buttonRemoveMacro.Text = "Remove";
			this.buttonRemoveMacro.UseVisualStyleBackColor = true;
			this.buttonRemoveMacro.Click += new System.EventHandler(this.ButtonRemoveMacro_Click);
			// 
			// buttonAddMacro
			// 
			this.buttonAddMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddMacro.Location = new System.Drawing.Point(11, 400);
			this.buttonAddMacro.Name = "buttonAddMacro";
			this.buttonAddMacro.Size = new System.Drawing.Size(55, 23);
			this.buttonAddMacro.TabIndex = 1;
			this.buttonAddMacro.Text = "Add";
			this.buttonAddMacro.UseVisualStyleBackColor = true;
			this.buttonAddMacro.Click += new System.EventHandler(this.ButtonAddMacro_Click);
			// 
			// listViewMacros
			// 
			this.listViewMacros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewMacros.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMacros});
			this.listViewMacros.FullRowSelect = true;
			this.listViewMacros.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewMacros.HideSelection = false;
			this.listViewMacros.Location = new System.Drawing.Point(12, 12);
			this.listViewMacros.Name = "listViewMacros";
			this.listViewMacros.Size = new System.Drawing.Size(169, 382);
			this.listViewMacros.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewMacros.TabIndex = 0;
			this.listViewMacros.UseCompatibleStateImageBehavior = false;
			this.listViewMacros.View = System.Windows.Forms.View.Details;
			this.listViewMacros.SelectedIndexChanged += new System.EventHandler(this.ListViewMacros_SelectedIndexChanged);
			// 
			// columnHeaderMacros
			// 
			this.columnHeaderMacros.Text = "Applications";
			this.columnHeaderMacros.Width = 139;
			// 
			// buttonCopyMacro
			// 
			this.buttonCopyMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCopyMacro.Location = new System.Drawing.Point(69, 400);
			this.buttonCopyMacro.Name = "buttonCopyMacro";
			this.buttonCopyMacro.Size = new System.Drawing.Size(55, 23);
			this.buttonCopyMacro.TabIndex = 26;
			this.buttonCopyMacro.Text = "Copy";
			this.buttonCopyMacro.UseVisualStyleBackColor = true;
			this.buttonCopyMacro.Click += new System.EventHandler(this.ButtonCopyMacro_Click);
			// 
			// MacrosForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(862, 463);
			this.Controls.Add(this.buttonCopyMacro);
			this.Controls.Add(this.panelMacro);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonRemoveMacro);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonAddMacro);
			this.Controls.Add(this.buttonApply);
			this.Controls.Add(this.listViewMacros);
			this.Controls.Add(this.labelNoMacroSelected);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(500, 300);
			this.Name = "MacrosForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Macros";
			this.Load += new System.EventHandler(this.MachinesForm_Load);
			this.panelMacro.ResumeLayout(false);
			this.panelMacro.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.Label labelNoMacroSelected;
		private Controls.BackgroundPanel panelMacro;
		private System.Windows.Forms.Button buttonAddMacroAction;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMacroActions;
		private Controls.LabeledDivider labeledDividerMacroActions;
		private System.Windows.Forms.Label labelMacroName;
		private System.Windows.Forms.TextBox textBoxMacroName;
		private Controls.LabeledDivider labeledDividerMacro;
		private System.Windows.Forms.Button buttonRemoveMacro;
		private System.Windows.Forms.Button buttonAddMacro;
		private Controls.ListView listViewMacros;
		private System.Windows.Forms.ColumnHeader columnHeaderMacros;
		private System.Windows.Forms.Button buttonCopyMacro;
	}
}
namespace ProcessManagerUI.Forms
{
	partial class DistributionSourcesForm
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
			this.listViewSources = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderFilter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderRecursive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderInclusive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonRemoveSource = new System.Windows.Forms.Button();
			this.buttonAddSource = new System.Windows.Forms.Button();
			this.backgroundPanelSource = new ProcessManagerUI.Controls.BackgroundPanel();
			this.panelSource = new System.Windows.Forms.Panel();
			this.labeledDividerSource = new ProcessManagerUI.Controls.LabeledDivider();
			this.buttonBrowseSourcePath = new System.Windows.Forms.Button();
			this.labelPath = new System.Windows.Forms.Label();
			this.checkBoxInclusive = new System.Windows.Forms.CheckBox();
			this.textBoxPath = new System.Windows.Forms.TextBox();
			this.checkBoxRecursive = new System.Windows.Forms.CheckBox();
			this.labelFilter = new System.Windows.Forms.Label();
			this.textBoxFilter = new System.Windows.Forms.TextBox();
			this.labelNoSourceSelected = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.backgroundPanelSource.SuspendLayout();
			this.panelSource.SuspendLayout();
			this.SuspendLayout();
			// 
			// listViewSources
			// 
			this.listViewSources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewSources.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPath,
            this.columnHeaderFilter,
            this.columnHeaderRecursive,
            this.columnHeaderInclusive});
			this.listViewSources.FullRowSelect = true;
			this.listViewSources.HideSelection = false;
			this.listViewSources.Location = new System.Drawing.Point(12, 12);
			this.listViewSources.MultiSelect = false;
			this.listViewSources.Name = "listViewSources";
			this.listViewSources.Size = new System.Drawing.Size(586, 177);
			this.listViewSources.TabIndex = 0;
			this.listViewSources.UseCompatibleStateImageBehavior = false;
			this.listViewSources.View = System.Windows.Forms.View.Details;
			this.listViewSources.SelectedIndexChanged += new System.EventHandler(this.ListViewSources_SelectedIndexChanged);
			// 
			// columnHeaderPath
			// 
			this.columnHeaderPath.Text = "Path";
			this.columnHeaderPath.Width = 325;
			// 
			// columnHeaderFilter
			// 
			this.columnHeaderFilter.Text = "Filter";
			this.columnHeaderFilter.Width = 120;
			// 
			// columnHeaderRecursive
			// 
			this.columnHeaderRecursive.Text = "Recursive";
			// 
			// columnHeaderInclusive
			// 
			this.columnHeaderInclusive.Text = "Inclusive";
			// 
			// buttonRemoveSource
			// 
			this.buttonRemoveSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRemoveSource.Image = global::ProcessManagerUI.Properties.Resources.remove_16;
			this.buttonRemoveSource.Location = new System.Drawing.Point(604, 42);
			this.buttonRemoveSource.Name = "buttonRemoveSource";
			this.buttonRemoveSource.Size = new System.Drawing.Size(38, 24);
			this.buttonRemoveSource.TabIndex = 22;
			this.buttonRemoveSource.UseVisualStyleBackColor = true;
			this.buttonRemoveSource.Click += new System.EventHandler(this.ButtonRemoveSource_Click);
			// 
			// buttonAddSource
			// 
			this.buttonAddSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddSource.Image = global::ProcessManagerUI.Properties.Resources.add_16;
			this.buttonAddSource.Location = new System.Drawing.Point(604, 12);
			this.buttonAddSource.Name = "buttonAddSource";
			this.buttonAddSource.Size = new System.Drawing.Size(38, 24);
			this.buttonAddSource.TabIndex = 21;
			this.buttonAddSource.UseVisualStyleBackColor = true;
			this.buttonAddSource.Click += new System.EventHandler(this.ButtonAddSource_Click);
			// 
			// backgroundPanelSource
			// 
			this.backgroundPanelSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.backgroundPanelSource.BackColor = System.Drawing.SystemColors.Window;
			this.backgroundPanelSource.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
			this.backgroundPanelSource.Controls.Add(this.panelSource);
			this.backgroundPanelSource.Controls.Add(this.labelNoSourceSelected);
			this.backgroundPanelSource.Location = new System.Drawing.Point(12, 195);
			this.backgroundPanelSource.Name = "backgroundPanelSource";
			this.backgroundPanelSource.Size = new System.Drawing.Size(630, 115);
			this.backgroundPanelSource.TabIndex = 23;
			// 
			// panelSource
			// 
			this.panelSource.Controls.Add(this.labeledDividerSource);
			this.panelSource.Controls.Add(this.buttonBrowseSourcePath);
			this.panelSource.Controls.Add(this.labelPath);
			this.panelSource.Controls.Add(this.checkBoxInclusive);
			this.panelSource.Controls.Add(this.textBoxPath);
			this.panelSource.Controls.Add(this.checkBoxRecursive);
			this.panelSource.Controls.Add(this.labelFilter);
			this.panelSource.Controls.Add(this.textBoxFilter);
			this.panelSource.Location = new System.Drawing.Point(8, 8);
			this.panelSource.Name = "panelSource";
			this.panelSource.Size = new System.Drawing.Size(614, 99);
			this.panelSource.TabIndex = 26;
			this.panelSource.Visible = false;
			// 
			// labeledDividerSource
			// 
			this.labeledDividerSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerSource.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerSource.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labeledDividerSource.Location = new System.Drawing.Point(0, 0);
			this.labeledDividerSource.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerSource.Name = "labeledDividerSource";
			this.labeledDividerSource.Size = new System.Drawing.Size(614, 15);
			this.labeledDividerSource.TabIndex = 11;
			this.labeledDividerSource.Text = "Source";
			// 
			// buttonBrowseSourcePath
			// 
			this.buttonBrowseSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseSourcePath.Location = new System.Drawing.Point(570, 22);
			this.buttonBrowseSourcePath.Name = "buttonBrowseSourcePath";
			this.buttonBrowseSourcePath.Size = new System.Drawing.Size(38, 23);
			this.buttonBrowseSourcePath.TabIndex = 17;
			this.buttonBrowseSourcePath.Text = "...";
			this.buttonBrowseSourcePath.UseVisualStyleBackColor = true;
			this.buttonBrowseSourcePath.Click += new System.EventHandler(this.ButtonBrowseSourcePath_Click);
			// 
			// labelPath
			// 
			this.labelPath.AutoSize = true;
			this.labelPath.Location = new System.Drawing.Point(20, 26);
			this.labelPath.Name = "labelPath";
			this.labelPath.Size = new System.Drawing.Size(33, 13);
			this.labelPath.TabIndex = 0;
			this.labelPath.Text = "Path:";
			// 
			// checkBoxInclusive
			// 
			this.checkBoxInclusive.AutoSize = true;
			this.checkBoxInclusive.Location = new System.Drawing.Point(156, 79);
			this.checkBoxInclusive.Name = "checkBoxInclusive";
			this.checkBoxInclusive.Size = new System.Drawing.Size(70, 17);
			this.checkBoxInclusive.TabIndex = 13;
			this.checkBoxInclusive.Text = "Inclusive";
			this.checkBoxInclusive.UseVisualStyleBackColor = true;
			this.checkBoxInclusive.CheckedChanged += new System.EventHandler(this.CheckBoxInclusive_CheckedChanged);
			// 
			// textBoxPath
			// 
			this.textBoxPath.Location = new System.Drawing.Point(62, 23);
			this.textBoxPath.Name = "textBoxPath";
			this.textBoxPath.Size = new System.Drawing.Size(502, 22);
			this.textBoxPath.TabIndex = 1;
			this.textBoxPath.TextChanged += new System.EventHandler(this.TextBoxPath_TextChanged);
			this.textBoxPath.Enter += new System.EventHandler(this.TextBoxPath_Enter);
			this.textBoxPath.Leave += new System.EventHandler(this.TextBoxPath_Leave);
			// 
			// checkBoxRecursive
			// 
			this.checkBoxRecursive.AutoSize = true;
			this.checkBoxRecursive.Location = new System.Drawing.Point(62, 79);
			this.checkBoxRecursive.Name = "checkBoxRecursive";
			this.checkBoxRecursive.Size = new System.Drawing.Size(74, 17);
			this.checkBoxRecursive.TabIndex = 12;
			this.checkBoxRecursive.Text = "Recursive";
			this.checkBoxRecursive.UseVisualStyleBackColor = true;
			this.checkBoxRecursive.CheckedChanged += new System.EventHandler(this.CheckBoxRecursive_CheckedChanged);
			// 
			// labelFilter
			// 
			this.labelFilter.AutoSize = true;
			this.labelFilter.Location = new System.Drawing.Point(20, 54);
			this.labelFilter.Name = "labelFilter";
			this.labelFilter.Size = new System.Drawing.Size(36, 13);
			this.labelFilter.TabIndex = 2;
			this.labelFilter.Text = "Filter:";
			// 
			// textBoxFilter
			// 
			this.textBoxFilter.Location = new System.Drawing.Point(62, 51);
			this.textBoxFilter.Name = "textBoxFilter";
			this.textBoxFilter.Size = new System.Drawing.Size(502, 22);
			this.textBoxFilter.TabIndex = 3;
			this.textBoxFilter.TextChanged += new System.EventHandler(this.TextBoxFilter_TextChanged);
			this.textBoxFilter.Leave += new System.EventHandler(this.TextBoxFilter_Leave);
			// 
			// labelNoSourceSelected
			// 
			this.labelNoSourceSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelNoSourceSelected.Location = new System.Drawing.Point(8, 8);
			this.labelNoSourceSelected.Name = "labelNoSourceSelected";
			this.labelNoSourceSelected.Size = new System.Drawing.Size(614, 99);
			this.labelNoSourceSelected.TabIndex = 26;
			this.labelNoSourceSelected.Text = "No source selected";
			this.labelNoSourceSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(452, 316);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(92, 23);
			this.buttonOK.TabIndex = 25;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(550, 316);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(92, 23);
			this.buttonCancel.TabIndex = 24;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// DistributionSourcesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(654, 351);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.backgroundPanelSource);
			this.Controls.Add(this.buttonRemoveSource);
			this.Controls.Add(this.buttonAddSource);
			this.Controls.Add(this.listViewSources);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DistributionSourcesForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Distribution Sources";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DistributionSourcesForm_FormClosing);
			this.Load += new System.EventHandler(this.DistributionSourcesForm_Load);
			this.backgroundPanelSource.ResumeLayout(false);
			this.panelSource.ResumeLayout(false);
			this.panelSource.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.ListView listViewSources;
		private System.Windows.Forms.Button buttonRemoveSource;
		private System.Windows.Forms.Button buttonAddSource;
		private Controls.BackgroundPanel backgroundPanelSource;
		private System.Windows.Forms.ColumnHeader columnHeaderPath;
		private System.Windows.Forms.ColumnHeader columnHeaderFilter;
		private System.Windows.Forms.ColumnHeader columnHeaderRecursive;
		private System.Windows.Forms.ColumnHeader columnHeaderInclusive;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBoxFilter;
		private System.Windows.Forms.Label labelFilter;
		private System.Windows.Forms.TextBox textBoxPath;
		private System.Windows.Forms.Label labelPath;
		private Controls.LabeledDivider labeledDividerSource;
		private System.Windows.Forms.CheckBox checkBoxInclusive;
		private System.Windows.Forms.CheckBox checkBoxRecursive;
		private System.Windows.Forms.Button buttonBrowseSourcePath;
		private System.Windows.Forms.Panel panelSource;
		private System.Windows.Forms.Label labelNoSourceSelected;
	}
}
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
			this.buttonRemoveSource = new System.Windows.Forms.Button();
			this.buttonAddSource = new System.Windows.Forms.Button();
			this.panelSource = new ProcessManagerUI.Controls.BackgroundPanel();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.columnHeaderPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderFilter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderRecursive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderInclusive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.labelPath = new System.Windows.Forms.Label();
			this.textBoxPath = new System.Windows.Forms.TextBox();
			this.textBoxFilter = new System.Windows.Forms.TextBox();
			this.labelFilter = new System.Windows.Forms.Label();
			this.labeledDividerSource = new ProcessManagerUI.Controls.LabeledDivider();
			this.checkBoxRecursive = new System.Windows.Forms.CheckBox();
			this.checkBoxInclusive = new System.Windows.Forms.CheckBox();
			this.buttonBrowseSourcePath = new System.Windows.Forms.Button();
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
			this.listViewSources.Location = new System.Drawing.Point(12, 12);
			this.listViewSources.Name = "listViewSources";
			this.listViewSources.Size = new System.Drawing.Size(586, 177);
			this.listViewSources.TabIndex = 0;
			this.listViewSources.UseCompatibleStateImageBehavior = false;
			this.listViewSources.View = System.Windows.Forms.View.Details;
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
			// 
			// panelSource
			// 
			this.panelSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelSource.BackColor = System.Drawing.SystemColors.Window;
			this.panelSource.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
			this.panelSource.Controls.Add(this.buttonBrowseSourcePath);
			this.panelSource.Controls.Add(this.checkBoxInclusive);
			this.panelSource.Controls.Add(this.checkBoxRecursive);
			this.panelSource.Controls.Add(this.labeledDividerSource);
			this.panelSource.Controls.Add(this.textBoxFilter);
			this.panelSource.Controls.Add(this.labelFilter);
			this.panelSource.Controls.Add(this.textBoxPath);
			this.panelSource.Controls.Add(this.labelPath);
			this.panelSource.Location = new System.Drawing.Point(12, 195);
			this.panelSource.Name = "panelSource";
			this.panelSource.Padding = new System.Windows.Forms.Padding(5);
			this.panelSource.Size = new System.Drawing.Size(630, 115);
			this.panelSource.TabIndex = 23;
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
			// labelPath
			// 
			this.labelPath.AutoSize = true;
			this.labelPath.Location = new System.Drawing.Point(28, 34);
			this.labelPath.Name = "labelPath";
			this.labelPath.Size = new System.Drawing.Size(33, 13);
			this.labelPath.TabIndex = 0;
			this.labelPath.Text = "Path:";
			// 
			// textBoxPath
			// 
			this.textBoxPath.Location = new System.Drawing.Point(70, 31);
			this.textBoxPath.Name = "textBoxPath";
			this.textBoxPath.Size = new System.Drawing.Size(502, 22);
			this.textBoxPath.TabIndex = 1;
			// 
			// textBoxFilter
			// 
			this.textBoxFilter.Location = new System.Drawing.Point(70, 59);
			this.textBoxFilter.Name = "textBoxFilter";
			this.textBoxFilter.Size = new System.Drawing.Size(502, 22);
			this.textBoxFilter.TabIndex = 3;
			// 
			// labelFilter
			// 
			this.labelFilter.AutoSize = true;
			this.labelFilter.Location = new System.Drawing.Point(28, 62);
			this.labelFilter.Name = "labelFilter";
			this.labelFilter.Size = new System.Drawing.Size(36, 13);
			this.labelFilter.TabIndex = 2;
			this.labelFilter.Text = "Filter:";
			// 
			// labeledDividerSource
			// 
			this.labeledDividerSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDividerSource.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDividerSource.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labeledDividerSource.Location = new System.Drawing.Point(8, 8);
			this.labeledDividerSource.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDividerSource.Name = "labeledDividerSource";
			this.labeledDividerSource.Size = new System.Drawing.Size(614, 15);
			this.labeledDividerSource.TabIndex = 11;
			this.labeledDividerSource.Text = "Source";
			// 
			// checkBoxRecursive
			// 
			this.checkBoxRecursive.AutoSize = true;
			this.checkBoxRecursive.Location = new System.Drawing.Point(70, 87);
			this.checkBoxRecursive.Name = "checkBoxRecursive";
			this.checkBoxRecursive.Size = new System.Drawing.Size(74, 17);
			this.checkBoxRecursive.TabIndex = 12;
			this.checkBoxRecursive.Text = "Recursive";
			this.checkBoxRecursive.UseVisualStyleBackColor = true;
			// 
			// checkBoxInclusive
			// 
			this.checkBoxInclusive.AutoSize = true;
			this.checkBoxInclusive.Location = new System.Drawing.Point(164, 87);
			this.checkBoxInclusive.Name = "checkBoxInclusive";
			this.checkBoxInclusive.Size = new System.Drawing.Size(70, 17);
			this.checkBoxInclusive.TabIndex = 13;
			this.checkBoxInclusive.Text = "Inclusive";
			this.checkBoxInclusive.UseVisualStyleBackColor = true;
			// 
			// buttonBrowseSourcePath
			// 
			this.buttonBrowseSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseSourcePath.Location = new System.Drawing.Point(578, 30);
			this.buttonBrowseSourcePath.Name = "buttonBrowseSourcePath";
			this.buttonBrowseSourcePath.Size = new System.Drawing.Size(38, 23);
			this.buttonBrowseSourcePath.TabIndex = 17;
			this.buttonBrowseSourcePath.Text = "...";
			this.buttonBrowseSourcePath.UseVisualStyleBackColor = true;
			// 
			// DistributionSourcesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(654, 351);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.panelSource);
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
			this.panelSource.ResumeLayout(false);
			this.panelSource.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.ListView listViewSources;
		private System.Windows.Forms.Button buttonRemoveSource;
		private System.Windows.Forms.Button buttonAddSource;
		private Controls.BackgroundPanel panelSource;
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
	}
}
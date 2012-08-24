namespace ProcessManagerUI.Forms
{
	partial class FileSystemBrowserForm
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
			this.components = new System.ComponentModel.Container();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelDescription = new System.Windows.Forms.Label();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.treeView = new ProcessManagerUI.Controls.TreeView();
			this.listView = new ProcessManagerUI.Controls.ListView();
			this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderModifiedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(482, 347);
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
			this.buttonCancel.Location = new System.Drawing.Point(580, 347);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(92, 23);
			this.buttonCancel.TabIndex = 24;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// labelDescription
			// 
			this.labelDescription.AutoSize = true;
			this.labelDescription.Location = new System.Drawing.Point(12, 9);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(71, 13);
			this.labelDescription.TabIndex = 26;
			this.labelDescription.Text = "[description]";
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 20);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.Location = new System.Drawing.Point(12, 34);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.treeView);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.listView);
			this.splitContainer.Size = new System.Drawing.Size(660, 307);
			this.splitContainer.SplitterDistance = 225;
			this.splitContainer.TabIndex = 29;
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.FullRowSelect = true;
			this.treeView.HideSelection = false;
			this.treeView.HotTracking = true;
			this.treeView.ImageIndex = 0;
			this.treeView.ImageList = this.imageList;
			this.treeView.Indent = 10;
			this.treeView.ItemHeight = 20;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = 0;
			this.treeView.ShowLines = false;
			this.treeView.Size = new System.Drawing.Size(225, 307);
			this.treeView.TabIndex = 27;
			this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeExpand);
			this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterExpand);
			this.treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeSelect);
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
			// 
			// listView
			// 
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderSize,
            this.columnHeaderModifiedDate});
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.FullRowSelect = true;
			this.listView.HideSelection = false;
			this.listView.Location = new System.Drawing.Point(0, 0);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(431, 307);
			this.listView.SmallImageList = this.imageList;
			this.listView.TabIndex = 28;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
			this.listView.DoubleClick += new System.EventHandler(this.ListView_DoubleClick);
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Name";
			this.columnHeaderName.Width = 200;
			// 
			// columnHeaderSize
			// 
			this.columnHeaderSize.Text = "Size";
			this.columnHeaderSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderSize.Width = 90;
			// 
			// columnHeaderModifiedDate
			// 
			this.columnHeaderModifiedDate.Text = "Date modified";
			this.columnHeaderModifiedDate.Width = 120;
			// 
			// FileSystemBrowserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(684, 382);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FileSystemBrowserForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "File System Browser";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileSystemBrowserForm_FormClosing);
			this.Load += new System.EventHandler(this.FileSystemBrowserForm_Load);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelDescription;
		private Controls.TreeView treeView;
		private Controls.ListView listView;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ColumnHeader columnHeaderSize;
		private System.Windows.Forms.ColumnHeader columnHeaderModifiedDate;
	}
}
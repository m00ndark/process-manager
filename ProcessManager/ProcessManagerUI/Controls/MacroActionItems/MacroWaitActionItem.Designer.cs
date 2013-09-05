namespace ProcessManagerUI.Controls.MacroActionItems
{
    partial class MacroWaitActionItem
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
            this.comboBoxWaitForEvent = new System.Windows.Forms.ComboBox();
            this.panelTimeout = new System.Windows.Forms.Panel();
            this.labelTimeoutMilliseconds = new System.Windows.Forms.Label();
            this.numericUpDownTimeoutMilliseconds = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel.SuspendLayout();
            this.panelTimeout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeoutMilliseconds)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel.Controls.Add(this.comboBoxWaitForEvent, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.panelTimeout, 2, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(301, 21);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // comboBoxWaitForEvent
            // 
            this.comboBoxWaitForEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxWaitForEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWaitForEvent.FormattingEnabled = true;
            this.comboBoxWaitForEvent.Location = new System.Drawing.Point(0, 0);
            this.comboBoxWaitForEvent.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxWaitForEvent.Name = "comboBoxWaitForEvent";
            this.comboBoxWaitForEvent.Size = new System.Drawing.Size(191, 21);
            this.comboBoxWaitForEvent.TabIndex = 0;
            this.comboBoxWaitForEvent.SelectedIndexChanged += new System.EventHandler(this.ComboBoxWaitForEvent_SelectedIndexChanged);
            // 
            // panelTimeout
            // 
            this.panelTimeout.Controls.Add(this.labelTimeoutMilliseconds);
            this.panelTimeout.Controls.Add(this.numericUpDownTimeoutMilliseconds);
            this.panelTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTimeout.Location = new System.Drawing.Point(197, 0);
            this.panelTimeout.Margin = new System.Windows.Forms.Padding(0);
            this.panelTimeout.Name = "panelTimeout";
            this.panelTimeout.Size = new System.Drawing.Size(104, 21);
            this.panelTimeout.TabIndex = 1;
            this.panelTimeout.Visible = false;
            // 
            // labelTimeoutMilliseconds
            // 
            this.labelTimeoutMilliseconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTimeoutMilliseconds.AutoSize = true;
            this.labelTimeoutMilliseconds.Location = new System.Drawing.Point(87, 2);
            this.labelTimeoutMilliseconds.Name = "labelTimeoutMilliseconds";
            this.labelTimeoutMilliseconds.Size = new System.Drawing.Size(20, 13);
            this.labelTimeoutMilliseconds.TabIndex = 3;
            this.labelTimeoutMilliseconds.Text = "ms";
            // 
            // numericUpDownTimeoutMilliseconds
            // 
            this.numericUpDownTimeoutMilliseconds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownTimeoutMilliseconds.Location = new System.Drawing.Point(0, 0);
            this.numericUpDownTimeoutMilliseconds.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownTimeoutMilliseconds.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDownTimeoutMilliseconds.Name = "numericUpDownTimeoutMilliseconds";
            this.numericUpDownTimeoutMilliseconds.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownTimeoutMilliseconds.TabIndex = 2;
            this.numericUpDownTimeoutMilliseconds.ValueChanged += new System.EventHandler(this.NumericUpDownTimeoutMilliseconds_ValueChanged);
            // 
            // MacroWaitActionItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MacroWaitActionItem";
            this.Size = new System.Drawing.Size(301, 21);
            this.Load += new System.EventHandler(this.MacroDistributionActionItem_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.panelTimeout.ResumeLayout(false);
            this.panelTimeout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeoutMilliseconds)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ComboBox comboBoxWaitForEvent;
        private System.Windows.Forms.Panel panelTimeout;
        private System.Windows.Forms.Label labelTimeoutMilliseconds;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeoutMilliseconds;
	}
}

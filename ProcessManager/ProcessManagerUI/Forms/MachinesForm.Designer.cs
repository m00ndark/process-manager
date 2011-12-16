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
			this.listView4 = new ProcessManagerUI.Controls.ListView();
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panelApplications = new System.Windows.Forms.Panel();
			this.button11 = new System.Windows.Forms.Button();
			this.labeledDivider3 = new ProcessManagerUI.Controls.LabeledDivider();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.panelApplications.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView4
			// 
			this.listView4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listView4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
			this.listView4.FullRowSelect = true;
			this.listView4.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView4.HideSelection = false;
			this.listView4.Location = new System.Drawing.Point(12, 12);
			this.listView4.MultiSelect = false;
			this.listView4.Name = "listView4";
			this.listView4.Size = new System.Drawing.Size(120, 175);
			this.listView4.TabIndex = 1;
			this.listView4.UseCompatibleStateImageBehavior = false;
			this.listView4.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Groups";
			this.columnHeader4.Width = 100;
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.Location = new System.Drawing.Point(76, 193);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(58, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "Remove";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.Location = new System.Drawing.Point(12, 193);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(58, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "Add";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// panelApplications
			// 
			this.panelApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelApplications.BackColor = System.Drawing.SystemColors.Window;
			this.panelApplications.Controls.Add(this.button1);
			this.panelApplications.Controls.Add(this.button11);
			this.panelApplications.Controls.Add(this.labeledDivider3);
			this.panelApplications.Controls.Add(this.textBox4);
			this.panelApplications.Controls.Add(this.label6);
			this.panelApplications.Location = new System.Drawing.Point(140, 12);
			this.panelApplications.Name = "panelApplications";
			this.panelApplications.Padding = new System.Windows.Forms.Padding(5);
			this.panelApplications.Size = new System.Drawing.Size(322, 204);
			this.panelApplications.TabIndex = 18;
			// 
			// button11
			// 
			this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button11.Location = new System.Drawing.Point(216, 84);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(92, 23);
			this.button11.TabIndex = 11;
			this.button11.Text = "Validate";
			this.button11.UseVisualStyleBackColor = true;
			// 
			// labeledDivider3
			// 
			this.labeledDivider3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDivider3.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDivider3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(170)))));
			this.labeledDivider3.Location = new System.Drawing.Point(8, 8);
			this.labeledDivider3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDivider3.Name = "labeledDivider3";
			this.labeledDivider3.Size = new System.Drawing.Size(306, 15);
			this.labeledDivider3.TabIndex = 10;
			this.labeledDivider3.Text = "Machine";
			// 
			// textBox4
			// 
			this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox4.Location = new System.Drawing.Point(95, 31);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(213, 20);
			this.textBox4.TabIndex = 6;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(28, 34);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(61, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Host name:";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(216, 113);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 23);
			this.button1.TabIndex = 12;
			this.button1.Text = "Copy Setup";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// MachinesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 228);
			this.Controls.Add(this.panelApplications);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.listView4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MachinesForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Machines";
			this.panelApplications.ResumeLayout(false);
			this.panelApplications.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.ListView listView4;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Panel panelApplications;
		private System.Windows.Forms.Button button11;
		private Controls.LabeledDivider labeledDivider3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button1;
	}
}
namespace ProcessManagerUI
{
	partial class ConfigurationForm
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Groups");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Applications");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Setup", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Plugins");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.panelGroups = new System.Windows.Forms.Panel();
			this.button7 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.listView2 = new ProcessManagerUI.Controls.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.labeledDivider2 = new ProcessManagerUI.Controls.LabeledDivider();
			this.labeledDivider1 = new ProcessManagerUI.Controls.LabeledDivider();
			this.listView1 = new ProcessManagerUI.Controls.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.treeView1 = new ProcessManagerUI.Controls.TreeView();
			this.panelApplications = new System.Windows.Forms.Panel();
			this.button11 = new System.Windows.Forms.Button();
			this.labeledDivider3 = new ProcessManagerUI.Controls.LabeledDivider();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button12 = new System.Windows.Forms.Button();
			this.button13 = new System.Windows.Forms.Button();
			this.listView4 = new ProcessManagerUI.Controls.ListView();
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.panelPlugins = new System.Windows.Forms.Panel();
			this.button8 = new System.Windows.Forms.Button();
			this.labeledDivider4 = new ProcessManagerUI.Controls.LabeledDivider();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.listView3 = new ProcessManagerUI.Controls.ListView();
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label7 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.panelGroups.SuspendLayout();
			this.panelApplications.SuspendLayout();
			this.panelPlugins.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(12, 12);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(653, 21);
			this.comboBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(671, 11);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Machines...";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// panelGroups
			// 
			this.panelGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelGroups.BackColor = System.Drawing.SystemColors.Window;
			this.panelGroups.Controls.Add(this.button7);
			this.panelGroups.Controls.Add(this.button6);
			this.panelGroups.Controls.Add(this.button5);
			this.panelGroups.Controls.Add(this.listView2);
			this.panelGroups.Controls.Add(this.label3);
			this.panelGroups.Controls.Add(this.button4);
			this.panelGroups.Controls.Add(this.labeledDivider2);
			this.panelGroups.Controls.Add(this.labeledDivider1);
			this.panelGroups.Controls.Add(this.textBox2);
			this.panelGroups.Controls.Add(this.label2);
			this.panelGroups.Controls.Add(this.textBox1);
			this.panelGroups.Controls.Add(this.label1);
			this.panelGroups.Controls.Add(this.button3);
			this.panelGroups.Controls.Add(this.button2);
			this.panelGroups.Controls.Add(this.listView1);
			this.panelGroups.Location = new System.Drawing.Point(174, 39);
			this.panelGroups.Name = "panelGroups";
			this.panelGroups.Padding = new System.Windows.Forms.Padding(5);
			this.panelGroups.Size = new System.Drawing.Size(589, 361);
			this.panelGroups.TabIndex = 3;
			this.panelGroups.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// button7
			// 
			this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button7.Image = global::ProcessManagerUI.Properties.Resources.copy_16;
			this.button7.Location = new System.Drawing.Point(537, 186);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(38, 24);
			this.button7.TabIndex = 16;
			this.button7.UseVisualStyleBackColor = true;
			// 
			// button6
			// 
			this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button6.Image = global::ProcessManagerUI.Properties.Resources.remove_16;
			this.button6.Location = new System.Drawing.Point(537, 156);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(38, 24);
			this.button6.TabIndex = 15;
			this.button6.UseVisualStyleBackColor = true;
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button5.Image = global::ProcessManagerUI.Properties.Resources.add_16;
			this.button5.Location = new System.Drawing.Point(537, 126);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(38, 24);
			this.button5.TabIndex = 14;
			this.button5.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(163, 129);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Applications:";
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Location = new System.Drawing.Point(537, 58);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(38, 23);
			this.button4.TabIndex = 11;
			this.button4.Text = "...";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(243, 59);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(288, 22);
			this.textBox2.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(163, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Path:";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(243, 31);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(288, 22);
			this.textBox1.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(163, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Name:";
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.Location = new System.Drawing.Point(71, 330);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(58, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "Remove";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.Location = new System.Drawing.Point(7, 330);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(58, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Add";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// listView2
			// 
			this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
			this.listView2.FullRowSelect = true;
			this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView2.HideSelection = false;
			this.listView2.Location = new System.Drawing.Point(243, 126);
			this.listView2.MultiSelect = false;
			this.listView2.Name = "listView2";
			this.listView2.Size = new System.Drawing.Size(288, 227);
			this.listView2.TabIndex = 13;
			this.listView2.UseCompatibleStateImageBehavior = false;
			this.listView2.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Applications";
			this.columnHeader2.Width = 268;
			// 
			// labeledDivider2
			// 
			this.labeledDivider2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDivider2.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDivider2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(170)))));
			this.labeledDivider2.Location = new System.Drawing.Point(144, 8);
			this.labeledDivider2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDivider2.Name = "labeledDivider2";
			this.labeledDivider2.Size = new System.Drawing.Size(437, 15);
			this.labeledDivider2.TabIndex = 10;
			this.labeledDivider2.Text = "Group";
			// 
			// labeledDivider1
			// 
			this.labeledDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDivider1.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDivider1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(170)))));
			this.labeledDivider1.Location = new System.Drawing.Point(147, 103);
			this.labeledDivider1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDivider1.Name = "labeledDivider1";
			this.labeledDivider1.Size = new System.Drawing.Size(434, 15);
			this.labeledDivider1.TabIndex = 9;
			this.labeledDivider1.Text = "Mappings";
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listView1.FullRowSelect = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(8, 8);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(120, 316);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Groups";
			this.columnHeader1.Width = 100;
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeView1.FullRowSelect = true;
			this.treeView1.HideSelection = false;
			this.treeView1.HotTracking = true;
			this.treeView1.Location = new System.Drawing.Point(12, 39);
			this.treeView1.Name = "treeView1";
			treeNode1.Name = "Groups";
			treeNode1.Text = "Groups";
			treeNode2.Name = "Applications";
			treeNode2.Text = "Applications";
			treeNode3.Name = "Setup";
			treeNode3.Text = "Setup";
			treeNode4.Name = "Plugins";
			treeNode4.Text = "Plugins";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
			this.treeView1.ShowLines = false;
			this.treeView1.Size = new System.Drawing.Size(156, 361);
			this.treeView1.TabIndex = 2;
			// 
			// panelApplications
			// 
			this.panelApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelApplications.BackColor = System.Drawing.SystemColors.Window;
			this.panelApplications.Controls.Add(this.textBox5);
			this.panelApplications.Controls.Add(this.label4);
			this.panelApplications.Controls.Add(this.button11);
			this.panelApplications.Controls.Add(this.labeledDivider3);
			this.panelApplications.Controls.Add(this.textBox3);
			this.panelApplications.Controls.Add(this.label5);
			this.panelApplications.Controls.Add(this.textBox4);
			this.panelApplications.Controls.Add(this.label6);
			this.panelApplications.Controls.Add(this.button12);
			this.panelApplications.Controls.Add(this.button13);
			this.panelApplications.Controls.Add(this.listView4);
			this.panelApplications.Location = new System.Drawing.Point(174, 39);
			this.panelApplications.Name = "panelApplications";
			this.panelApplications.Padding = new System.Windows.Forms.Padding(5);
			this.panelApplications.Size = new System.Drawing.Size(589, 361);
			this.panelApplications.TabIndex = 17;
			// 
			// button11
			// 
			this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button11.Location = new System.Drawing.Point(537, 58);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(38, 23);
			this.button11.TabIndex = 11;
			this.button11.Text = "...";
			this.button11.UseVisualStyleBackColor = true;
			// 
			// labeledDivider3
			// 
			this.labeledDivider3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDivider3.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDivider3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(170)))));
			this.labeledDivider3.Location = new System.Drawing.Point(144, 8);
			this.labeledDivider3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDivider3.Name = "labeledDivider3";
			this.labeledDivider3.Size = new System.Drawing.Size(437, 15);
			this.labeledDivider3.TabIndex = 10;
			this.labeledDivider3.Text = "Application";
			// 
			// textBox3
			// 
			this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox3.Location = new System.Drawing.Point(246, 59);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(285, 22);
			this.textBox3.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(163, 62);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Relative path:";
			// 
			// textBox4
			// 
			this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox4.Location = new System.Drawing.Point(246, 31);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(285, 22);
			this.textBox4.TabIndex = 6;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(163, 34);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(39, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Name:";
			// 
			// button12
			// 
			this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button12.Location = new System.Drawing.Point(71, 330);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(58, 23);
			this.button12.TabIndex = 2;
			this.button12.Text = "Remove";
			this.button12.UseVisualStyleBackColor = true;
			// 
			// button13
			// 
			this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button13.Location = new System.Drawing.Point(7, 330);
			this.button13.Name = "button13";
			this.button13.Size = new System.Drawing.Size(58, 23);
			this.button13.TabIndex = 1;
			this.button13.Text = "Add";
			this.button13.UseVisualStyleBackColor = true;
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
			this.listView4.Location = new System.Drawing.Point(8, 8);
			this.listView4.MultiSelect = false;
			this.listView4.Name = "listView4";
			this.listView4.Size = new System.Drawing.Size(120, 316);
			this.listView4.TabIndex = 0;
			this.listView4.UseCompatibleStateImageBehavior = false;
			this.listView4.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Groups";
			this.columnHeader4.Width = 100;
			// 
			// textBox5
			// 
			this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox5.Location = new System.Drawing.Point(246, 87);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(285, 22);
			this.textBox5.TabIndex = 13;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(163, 90);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(66, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Arguments:";
			// 
			// panelPlugins
			// 
			this.panelPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPlugins.BackColor = System.Drawing.SystemColors.Window;
			this.panelPlugins.Controls.Add(this.checkBox1);
			this.panelPlugins.Controls.Add(this.label10);
			this.panelPlugins.Controls.Add(this.label7);
			this.panelPlugins.Controls.Add(this.button8);
			this.panelPlugins.Controls.Add(this.labeledDivider4);
			this.panelPlugins.Controls.Add(this.label8);
			this.panelPlugins.Controls.Add(this.label9);
			this.panelPlugins.Controls.Add(this.listView3);
			this.panelPlugins.Location = new System.Drawing.Point(174, 39);
			this.panelPlugins.Name = "panelPlugins";
			this.panelPlugins.Padding = new System.Windows.Forms.Padding(5);
			this.panelPlugins.Size = new System.Drawing.Size(589, 361);
			this.panelPlugins.TabIndex = 18;
			// 
			// button8
			// 
			this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button8.Location = new System.Drawing.Point(489, 330);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(92, 23);
			this.button8.TabIndex = 11;
			this.button8.Text = "Configure...";
			this.button8.UseVisualStyleBackColor = true;
			// 
			// labeledDivider4
			// 
			this.labeledDivider4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labeledDivider4.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.labeledDivider4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(170)))));
			this.labeledDivider4.Location = new System.Drawing.Point(144, 8);
			this.labeledDivider4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
			this.labeledDivider4.Name = "labeledDivider4";
			this.labeledDivider4.Size = new System.Drawing.Size(437, 15);
			this.labeledDivider4.TabIndex = 10;
			this.labeledDivider4.Text = "Plugin";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(163, 62);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(69, 13);
			this.label8.TabIndex = 7;
			this.label8.Text = "Description:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(163, 34);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(39, 13);
			this.label9.TabIndex = 5;
			this.label9.Text = "Name:";
			// 
			// listView3
			// 
			this.listView3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
			this.listView3.FullRowSelect = true;
			this.listView3.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView3.HideSelection = false;
			this.listView3.Location = new System.Drawing.Point(8, 8);
			this.listView3.MultiSelect = false;
			this.listView3.Name = "listView3";
			this.listView3.Size = new System.Drawing.Size(120, 345);
			this.listView3.TabIndex = 0;
			this.listView3.UseCompatibleStateImageBehavior = false;
			this.listView3.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Groups";
			this.columnHeader3.Width = 100;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(238, 34);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 13);
			this.label7.TabIndex = 12;
			this.label7.Text = "[name]";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(238, 62);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(71, 13);
			this.label10.TabIndex = 13;
			this.label10.Text = "[description]";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(147, 334);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(68, 17);
			this.checkBox1.TabIndex = 14;
			this.checkBox1.Text = "Enabled";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// ConfigurationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(775, 412);
			this.Controls.Add(this.panelApplications);
			this.Controls.Add(this.panelGroups);
			this.Controls.Add(this.panelPlugins);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboBox1);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigurationForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Process Manager Configuration";
			this.Load += new System.EventHandler(this.ConfigurationForm_Load);
			this.panelGroups.ResumeLayout(false);
			this.panelGroups.PerformLayout();
			this.panelApplications.ResumeLayout(false);
			this.panelApplications.PerformLayout();
			this.panelPlugins.ResumeLayout(false);
			this.panelPlugins.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button button1;
		private Controls.TreeView treeView1;
		private System.Windows.Forms.Panel panelGroups;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private Controls.ListView listView1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private Controls.LabeledDivider labeledDivider1;
		private Controls.LabeledDivider labeledDivider2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button5;
		private Controls.ListView listView2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Panel panelApplications;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button11;
		private Controls.LabeledDivider labeledDivider3;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button13;
		private Controls.ListView listView4;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Panel panelPlugins;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button8;
		private Controls.LabeledDivider labeledDivider4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private Controls.ListView listView3;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.CheckBox checkBox1;

	}
}


namespace RolePlayCharacterWF
{
    partial class RolePlayCharacterForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.emotionalAppraisalSelectionView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.emotionalAppraisalView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deleteEA = new System.Windows.Forms.Button();
            this.editEA = new System.Windows.Forms.Button();
            this.addNewEA = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.emotionalDecisionMakingAvailableView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.emotionalDecisionMakingView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deleteEDM = new System.Windows.Forms.Button();
            this.editEDM = new System.Windows.Forms.Button();
            this.addNewEDM = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.deleteGroupButton = new System.Windows.Forms.Button();
            this.editGroupButton = new System.Windows.Forms.Button();
            this.addNewGroupButton = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.GenerateNPC = new System.Windows.Forms.Button();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(613, 281);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.emotionalAppraisalSelectionView);
            this.tabPage1.Controls.Add(this.emotionalAppraisalView);
            this.tabPage1.Controls.Add(this.deleteEA);
            this.tabPage1.Controls.Add(this.editEA);
            this.tabPage1.Controls.Add(this.addNewEA);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(605, 255);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Emotional Appraisal";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // emotionalAppraisalSelectionView
            // 
            this.emotionalAppraisalSelectionView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.emotionalAppraisalSelectionView.FullRowSelect = true;
            this.emotionalAppraisalSelectionView.Location = new System.Drawing.Point(395, 37);
            this.emotionalAppraisalSelectionView.Name = "emotionalAppraisalSelectionView";
            this.emotionalAppraisalSelectionView.Size = new System.Drawing.Size(156, 147);
            this.emotionalAppraisalSelectionView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.emotionalAppraisalSelectionView.TabIndex = 7;
            this.emotionalAppraisalSelectionView.UseCompatibleStateImageBehavior = false;
            this.emotionalAppraisalSelectionView.View = System.Windows.Forms.View.Details;
            this.emotionalAppraisalSelectionView.SelectedIndexChanged += new System.EventHandler(this.emotionalAppraisalSelectionView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 150;
            // 
            // emotionalAppraisalView
            // 
            this.emotionalAppraisalView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader});
            this.emotionalAppraisalView.FullRowSelect = true;
            this.emotionalAppraisalView.Location = new System.Drawing.Point(45, 37);
            this.emotionalAppraisalView.Name = "emotionalAppraisalView";
            this.emotionalAppraisalView.Size = new System.Drawing.Size(154, 147);
            this.emotionalAppraisalView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.emotionalAppraisalView.TabIndex = 6;
            this.emotionalAppraisalView.UseCompatibleStateImageBehavior = false;
            this.emotionalAppraisalView.View = System.Windows.Forms.View.Details;
            this.emotionalAppraisalView.SelectedIndexChanged += new System.EventHandler(this.emotionalAppraisalView_SelectedIndexChanged);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 150;
            // 
            // deleteEA
            // 
            this.deleteEA.Location = new System.Drawing.Point(524, 200);
            this.deleteEA.Name = "deleteEA";
            this.deleteEA.Size = new System.Drawing.Size(75, 23);
            this.deleteEA.TabIndex = 2;
            this.deleteEA.Text = "Delete";
            this.deleteEA.UseVisualStyleBackColor = true;
            this.deleteEA.Click += new System.EventHandler(this.deleteEA_Click);
            // 
            // editEA
            // 
            this.editEA.Location = new System.Drawing.Point(432, 200);
            this.editEA.Name = "editEA";
            this.editEA.Size = new System.Drawing.Size(75, 23);
            this.editEA.TabIndex = 1;
            this.editEA.Text = "Edit";
            this.editEA.UseVisualStyleBackColor = true;
            this.editEA.Click += new System.EventHandler(this.editEA_Click);
            // 
            // addNewEA
            // 
            this.addNewEA.Location = new System.Drawing.Point(339, 200);
            this.addNewEA.Name = "addNewEA";
            this.addNewEA.Size = new System.Drawing.Size(75, 23);
            this.addNewEA.TabIndex = 0;
            this.addNewEA.Text = "New";
            this.addNewEA.UseVisualStyleBackColor = true;
            this.addNewEA.Click += new System.EventHandler(this.addNewEA_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.emotionalDecisionMakingAvailableView);
            this.tabPage2.Controls.Add(this.emotionalDecisionMakingView);
            this.tabPage2.Controls.Add(this.deleteEDM);
            this.tabPage2.Controls.Add(this.editEDM);
            this.tabPage2.Controls.Add(this.addNewEDM);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(605, 255);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Emotional Decision Making";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // emotionalDecisionMakingAvailableView
            // 
            this.emotionalDecisionMakingAvailableView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.emotionalDecisionMakingAvailableView.FullRowSelect = true;
            this.emotionalDecisionMakingAvailableView.Location = new System.Drawing.Point(374, 57);
            this.emotionalDecisionMakingAvailableView.Name = "emotionalDecisionMakingAvailableView";
            this.emotionalDecisionMakingAvailableView.Size = new System.Drawing.Size(156, 147);
            this.emotionalDecisionMakingAvailableView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.emotionalDecisionMakingAvailableView.TabIndex = 12;
            this.emotionalDecisionMakingAvailableView.UseCompatibleStateImageBehavior = false;
            this.emotionalDecisionMakingAvailableView.View = System.Windows.Forms.View.Details;
            this.emotionalDecisionMakingAvailableView.SelectedIndexChanged += new System.EventHandler(this.emotionalDecisionMakingAvailableView_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 150;
            // 
            // emotionalDecisionMakingView
            // 
            this.emotionalDecisionMakingView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.emotionalDecisionMakingView.FullRowSelect = true;
            this.emotionalDecisionMakingView.Location = new System.Drawing.Point(22, 57);
            this.emotionalDecisionMakingView.Name = "emotionalDecisionMakingView";
            this.emotionalDecisionMakingView.Size = new System.Drawing.Size(155, 147);
            this.emotionalDecisionMakingView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.emotionalDecisionMakingView.TabIndex = 11;
            this.emotionalDecisionMakingView.UseCompatibleStateImageBehavior = false;
            this.emotionalDecisionMakingView.View = System.Windows.Forms.View.Details;
            this.emotionalDecisionMakingView.SelectedIndexChanged += new System.EventHandler(this.emotionalDecisionMakingView_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 150;
            // 
            // deleteEDM
            // 
            this.deleteEDM.Location = new System.Drawing.Point(511, 210);
            this.deleteEDM.Name = "deleteEDM";
            this.deleteEDM.Size = new System.Drawing.Size(75, 23);
            this.deleteEDM.TabIndex = 10;
            this.deleteEDM.Text = "Delete";
            this.deleteEDM.UseVisualStyleBackColor = true;
            this.deleteEDM.Click += new System.EventHandler(this.deleteEDM_Click);
            // 
            // editEDM
            // 
            this.editEDM.Location = new System.Drawing.Point(408, 210);
            this.editEDM.Name = "editEDM";
            this.editEDM.Size = new System.Drawing.Size(75, 23);
            this.editEDM.TabIndex = 9;
            this.editEDM.Text = "Edit";
            this.editEDM.UseVisualStyleBackColor = true;
            this.editEDM.Click += new System.EventHandler(this.editEDM_Click);
            // 
            // addNewEDM
            // 
            this.addNewEDM.Location = new System.Drawing.Point(312, 210);
            this.addNewEDM.Name = "addNewEDM";
            this.addNewEDM.Size = new System.Drawing.Size(75, 23);
            this.addNewEDM.TabIndex = 8;
            this.addNewEDM.Text = "New";
            this.addNewEDM.UseVisualStyleBackColor = true;
            this.addNewEDM.Click += new System.EventHandler(this.addNewEDM_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Controls.Add(this.deleteGroupButton);
            this.tabPage3.Controls.Add(this.editGroupButton);
            this.tabPage3.Controls.Add(this.addNewGroupButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(605, 255);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Groups";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(281, 27);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(233, 150);
            this.dataGridView2.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(219, 150);
            this.dataGridView1.TabIndex = 3;
            // 
            // deleteGroupButton
            // 
            this.deleteGroupButton.Location = new System.Drawing.Point(439, 192);
            this.deleteGroupButton.Name = "deleteGroupButton";
            this.deleteGroupButton.Size = new System.Drawing.Size(75, 23);
            this.deleteGroupButton.TabIndex = 2;
            this.deleteGroupButton.Text = "Delete";
            this.deleteGroupButton.UseVisualStyleBackColor = true;
            this.deleteGroupButton.Click += new System.EventHandler(this.deleteGroupButton_Click);
            // 
            // editGroupButton
            // 
            this.editGroupButton.Location = new System.Drawing.Point(362, 192);
            this.editGroupButton.Name = "editGroupButton";
            this.editGroupButton.Size = new System.Drawing.Size(75, 23);
            this.editGroupButton.TabIndex = 1;
            this.editGroupButton.Text = "Edit";
            this.editGroupButton.UseVisualStyleBackColor = true;
            this.editGroupButton.Click += new System.EventHandler(this.editGroupButton_Click);
            // 
            // addNewGroupButton
            // 
            this.addNewGroupButton.Location = new System.Drawing.Point(281, 192);
            this.addNewGroupButton.Name = "addNewGroupButton";
            this.addNewGroupButton.Size = new System.Drawing.Size(75, 23);
            this.addNewGroupButton.TabIndex = 0;
            this.addNewGroupButton.Text = "New";
            this.addNewGroupButton.UseVisualStyleBackColor = true;
            this.addNewGroupButton.Click += new System.EventHandler(this.addNewGroupButton_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.listBox4);
            this.tabPage4.Controls.Add(this.listBox3);
            this.tabPage4.Controls.Add(this.listBox2);
            this.tabPage4.Controls.Add(this.listBox1);
            this.tabPage4.Controls.Add(this.GenerateNPC);
            this.tabPage4.Controls.Add(this.descriptionTextBox);
            this.tabPage4.Controls.Add(this.nameTextBox);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(605, 255);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Generate";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(525, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Groups";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Groups";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Emotional Decision Making";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Emotional Appraisal";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.Location = new System.Drawing.Point(479, 106);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(120, 95);
            this.listBox4.TabIndex = 8;
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(327, 106);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(120, 95);
            this.listBox3.TabIndex = 7;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(170, 106);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(132, 95);
            this.listBox2.TabIndex = 6;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 106);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(127, 95);
            this.listBox1.TabIndex = 5;
            // 
            // GenerateNPC
            // 
            this.GenerateNPC.Location = new System.Drawing.Point(233, 214);
            this.GenerateNPC.Name = "GenerateNPC";
            this.GenerateNPC.Size = new System.Drawing.Size(75, 23);
            this.GenerateNPC.TabIndex = 4;
            this.GenerateNPC.Text = "Generate";
            this.GenerateNPC.UseVisualStyleBackColor = true;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(345, 29);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(142, 20);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(99, 29);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(142, 20);
            this.nameTextBox.TabIndex = 2;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // RolePlayCharacter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(637, 305);
            this.Controls.Add(this.tabControl1);
            this.Name = "RolePlayCharacter";
            this.Text = "RolePlayCharacter";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button deleteEA;
        private System.Windows.Forms.Button editEA;
        private System.Windows.Forms.Button addNewEA;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button deleteGroupButton;
        private System.Windows.Forms.Button editGroupButton;
        private System.Windows.Forms.Button addNewGroupButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ListView emotionalAppraisalView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ListView emotionalAppraisalSelectionView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView emotionalDecisionMakingAvailableView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView emotionalDecisionMakingView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button deleteEDM;
        private System.Windows.Forms.Button editEDM;
        private System.Windows.Forms.Button addNewEDM;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button GenerateNPC;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}


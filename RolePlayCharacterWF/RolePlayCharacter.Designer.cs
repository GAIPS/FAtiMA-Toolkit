namespace RolePlayCharacterWF
{
    partial class RolePlayCharacter
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
            this.emotionalAppraisalSelected = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.emotionalAppraisalView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deleteEA = new System.Windows.Forms.Button();
            this.editEA = new System.Windows.Forms.Button();
            this.addNewEA = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.deleteGroupButton = new System.Windows.Forms.Button();
            this.editGroupButton = new System.Windows.Forms.Button();
            this.addNewGroupButton = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deleteEDM = new System.Windows.Forms.Button();
            this.editEDM = new System.Windows.Forms.Button();
            this.addNewEDM = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(528, 281);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.emotionalAppraisalSelected);
            this.tabPage1.Controls.Add(this.emotionalAppraisalView);
            this.tabPage1.Controls.Add(this.deleteEA);
            this.tabPage1.Controls.Add(this.editEA);
            this.tabPage1.Controls.Add(this.addNewEA);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(520, 255);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Emotional Appraisal";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // emotionalAppraisalSelected
            // 
            this.emotionalAppraisalSelected.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.emotionalAppraisalSelected.FullRowSelect = true;
            this.emotionalAppraisalSelected.Location = new System.Drawing.Point(321, 17);
            this.emotionalAppraisalSelected.Name = "emotionalAppraisalSelected";
            this.emotionalAppraisalSelected.Size = new System.Drawing.Size(156, 147);
            this.emotionalAppraisalSelected.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.emotionalAppraisalSelected.TabIndex = 7;
            this.emotionalAppraisalSelected.UseCompatibleStateImageBehavior = false;
            this.emotionalAppraisalSelected.View = System.Windows.Forms.View.Details;
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
            this.emotionalAppraisalView.Location = new System.Drawing.Point(6, 17);
            this.emotionalAppraisalView.Name = "emotionalAppraisalView";
            this.emotionalAppraisalView.Size = new System.Drawing.Size(154, 147);
            this.emotionalAppraisalView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.emotionalAppraisalView.TabIndex = 6;
            this.emotionalAppraisalView.UseCompatibleStateImageBehavior = false;
            this.emotionalAppraisalView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 150;
            // 
            // deleteEA
            // 
            this.deleteEA.Location = new System.Drawing.Point(434, 179);
            this.deleteEA.Name = "deleteEA";
            this.deleteEA.Size = new System.Drawing.Size(75, 23);
            this.deleteEA.TabIndex = 2;
            this.deleteEA.Text = "Delete";
            this.deleteEA.UseVisualStyleBackColor = true;
            // 
            // editEA
            // 
            this.editEA.Location = new System.Drawing.Point(353, 179);
            this.editEA.Name = "editEA";
            this.editEA.Size = new System.Drawing.Size(75, 23);
            this.editEA.TabIndex = 1;
            this.editEA.Text = "Edit";
            this.editEA.UseVisualStyleBackColor = true;
            this.editEA.Click += new System.EventHandler(this.editEA_Click);
            // 
            // addNewEA
            // 
            this.addNewEA.Location = new System.Drawing.Point(272, 179);
            this.addNewEA.Name = "addNewEA";
            this.addNewEA.Size = new System.Drawing.Size(75, 23);
            this.addNewEA.TabIndex = 0;
            this.addNewEA.Text = "New";
            this.addNewEA.UseVisualStyleBackColor = true;
            this.addNewEA.Click += new System.EventHandler(this.addNewEA_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView2);
            this.tabPage2.Controls.Add(this.listView3);
            this.tabPage2.Controls.Add(this.deleteEDM);
            this.tabPage2.Controls.Add(this.editEDM);
            this.tabPage2.Controls.Add(this.addNewEDM);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(520, 255);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Emotional Decision Making";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.tabPage3.Size = new System.Drawing.Size(520, 255);
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
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView2.FullRowSelect = true;
            this.listView2.Location = new System.Drawing.Point(327, 24);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(156, 147);
            this.listView2.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView2.TabIndex = 12;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 150;
            // 
            // listView3
            // 
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listView3.FullRowSelect = true;
            this.listView3.Location = new System.Drawing.Point(12, 24);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(155, 147);
            this.listView3.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView3.TabIndex = 11;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 150;
            // 
            // deleteEDM
            // 
            this.deleteEDM.Location = new System.Drawing.Point(440, 186);
            this.deleteEDM.Name = "deleteEDM";
            this.deleteEDM.Size = new System.Drawing.Size(75, 23);
            this.deleteEDM.TabIndex = 10;
            this.deleteEDM.Text = "Delete";
            this.deleteEDM.UseVisualStyleBackColor = true;
            // 
            // editEDM
            // 
            this.editEDM.Location = new System.Drawing.Point(359, 186);
            this.editEDM.Name = "editEDM";
            this.editEDM.Size = new System.Drawing.Size(75, 23);
            this.editEDM.TabIndex = 9;
            this.editEDM.Text = "Edit";
            this.editEDM.UseVisualStyleBackColor = true;
            // 
            // addNewEDM
            // 
            this.addNewEDM.Location = new System.Drawing.Point(278, 186);
            this.addNewEDM.Name = "addNewEDM";
            this.addNewEDM.Size = new System.Drawing.Size(75, 23);
            this.addNewEDM.TabIndex = 8;
            this.addNewEDM.Text = "New";
            this.addNewEDM.UseVisualStyleBackColor = true;
            // 
            // RolePlayCharacter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(546, 305);
            this.Controls.Add(this.tabControl1);
            this.Name = "RolePlayCharacter";
            this.Text = "RolePlayCharacter";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.ListView emotionalAppraisalSelected;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button deleteEDM;
        private System.Windows.Forms.Button editEDM;
        private System.Windows.Forms.Button addNewEDM;
    }
}


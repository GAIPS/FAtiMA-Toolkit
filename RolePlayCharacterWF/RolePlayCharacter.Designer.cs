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
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.deleteEA = new System.Windows.Forms.Button();
            this.editEA = new System.Windows.Forms.Button();
            this.addNewEA = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.tabPage1.Controls.Add(this.listBox2);
            this.tabPage1.Controls.Add(this.listBox1);
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
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(6, 26);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(235, 147);
            this.listBox2.TabIndex = 4;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(274, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(235, 147);
            this.listBox1.TabIndex = 3;
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
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(560, 255);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Groups";
            this.tabPage3.UseVisualStyleBackColor = true;
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
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
    }
}


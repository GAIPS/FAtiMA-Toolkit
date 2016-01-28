namespace AuthorialAgentsWF
{
    partial class AddEditScenario
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
            this.deleteNPC = new System.Windows.Forms.Button();
            this.editNPC = new System.Windows.Forms.Button();
            this.createNewNPC = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(510, 278);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.deleteNPC);
            this.tabPage1.Controls.Add(this.editNPC);
            this.tabPage1.Controls.Add(this.createNewNPC);
            this.tabPage1.Controls.Add(this.listBox2);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(502, 252);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "NPC\'s";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // deleteNPC
            // 
            this.deleteNPC.Location = new System.Drawing.Point(421, 191);
            this.deleteNPC.Name = "deleteNPC";
            this.deleteNPC.Size = new System.Drawing.Size(75, 23);
            this.deleteNPC.TabIndex = 4;
            this.deleteNPC.Text = "Delete";
            this.deleteNPC.UseVisualStyleBackColor = true;
            this.deleteNPC.Click += new System.EventHandler(this.deleteNPC_Click);
            // 
            // editNPC
            // 
            this.editNPC.Location = new System.Drawing.Point(340, 191);
            this.editNPC.Name = "editNPC";
            this.editNPC.Size = new System.Drawing.Size(75, 23);
            this.editNPC.TabIndex = 3;
            this.editNPC.Text = "Edit";
            this.editNPC.UseVisualStyleBackColor = true;
            this.editNPC.Click += new System.EventHandler(this.editNPC_Click);
            // 
            // createNewNPC
            // 
            this.createNewNPC.Location = new System.Drawing.Point(259, 191);
            this.createNewNPC.Name = "createNewNPC";
            this.createNewNPC.Size = new System.Drawing.Size(75, 23);
            this.createNewNPC.TabIndex = 2;
            this.createNewNPC.Text = "New";
            this.createNewNPC.UseVisualStyleBackColor = true;
            this.createNewNPC.Click += new System.EventHandler(this.createNewNPC_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(259, 24);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(237, 147);
            this.listBox2.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(221, 147);
            this.listBox1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(502, 252);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Actions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // AddEditScenario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 302);
            this.Controls.Add(this.tabControl1);
            this.Name = "AddEditScenario";
            this.Text = "AddEditScenario";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button createNewNPC;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button deleteNPC;
        private System.Windows.Forms.Button editNPC;
    }
}
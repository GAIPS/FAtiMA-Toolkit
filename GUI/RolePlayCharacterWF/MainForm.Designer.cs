namespace RolePlayCharacterWF
{
    partial class MainForm
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxCharacterName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonEditEmotionalAppraisal = new System.Windows.Forms.Button();
            this.buttonSetEmotionalAppraisalSource = new System.Windows.Forms.Button();
            this.textBoxEmotionalAppraisalSource = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonEditEmotionalDecisionMaking = new System.Windows.Forms.Button();
            this.buttonSetEmotionalDecisionMakingSource = new System.Windows.Forms.Button();
            this.textBoxEmotionalDecisionMakingSource = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCharacterBody = new System.Windows.Forms.TextBox();
            this.mainMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(396, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsStripMenuItem
            // 
            this.saveAsStripMenuItem.Name = "saveAsStripMenuItem";
            this.saveAsStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsStripMenuItem.Text = "Save &As...";
            this.saveAsStripMenuItem.Click += new System.EventHandler(this.saveAsStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // textBoxCharacterName
            // 
            this.textBoxCharacterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCharacterName.Location = new System.Drawing.Point(119, 45);
            this.textBoxCharacterName.Name = "textBoxCharacterName";
            this.textBoxCharacterName.Size = new System.Drawing.Size(264, 20);
            this.textBoxCharacterName.TabIndex = 3;
            this.textBoxCharacterName.TextChanged += new System.EventHandler(this.textBoxCharacterName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Character Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonEditEmotionalAppraisal);
            this.groupBox1.Controls.Add(this.buttonSetEmotionalAppraisalSource);
            this.groupBox1.Controls.Add(this.textBoxEmotionalAppraisalSource);
            this.groupBox1.Location = new System.Drawing.Point(12, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 66);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Emotional Appraisal";
            // 
            // buttonEditEmotionalAppraisal
            // 
            this.buttonEditEmotionalAppraisal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditEmotionalAppraisal.Location = new System.Drawing.Point(304, 28);
            this.buttonEditEmotionalAppraisal.Name = "buttonEditEmotionalAppraisal";
            this.buttonEditEmotionalAppraisal.Size = new System.Drawing.Size(61, 23);
            this.buttonEditEmotionalAppraisal.TabIndex = 13;
            this.buttonEditEmotionalAppraisal.Text = "Edit";
            this.buttonEditEmotionalAppraisal.UseVisualStyleBackColor = true;
            this.buttonEditEmotionalAppraisal.Click += new System.EventHandler(this.buttonEditEmotionalAppraisal_Click);
            // 
            // buttonSetEmotionalAppraisalSource
            // 
            this.buttonSetEmotionalAppraisalSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetEmotionalAppraisalSource.Location = new System.Drawing.Point(237, 28);
            this.buttonSetEmotionalAppraisalSource.Name = "buttonSetEmotionalAppraisalSource";
            this.buttonSetEmotionalAppraisalSource.Size = new System.Drawing.Size(61, 23);
            this.buttonSetEmotionalAppraisalSource.TabIndex = 12;
            this.buttonSetEmotionalAppraisalSource.Text = "Set";
            this.buttonSetEmotionalAppraisalSource.UseVisualStyleBackColor = true;
            this.buttonSetEmotionalAppraisalSource.Click += new System.EventHandler(this.buttonSetEmotionalAppraisalSource_Click);
            // 
            // textBoxEmotionalAppraisalSource
            // 
            this.textBoxEmotionalAppraisalSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEmotionalAppraisalSource.Location = new System.Drawing.Point(6, 30);
            this.textBoxEmotionalAppraisalSource.Name = "textBoxEmotionalAppraisalSource";
            this.textBoxEmotionalAppraisalSource.ReadOnly = true;
            this.textBoxEmotionalAppraisalSource.Size = new System.Drawing.Size(225, 20);
            this.textBoxEmotionalAppraisalSource.TabIndex = 11;
            this.textBoxEmotionalAppraisalSource.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonEditEmotionalDecisionMaking);
            this.groupBox2.Controls.Add(this.buttonSetEmotionalDecisionMakingSource);
            this.groupBox2.Controls.Add(this.textBoxEmotionalDecisionMakingSource);
            this.groupBox2.Location = new System.Drawing.Point(12, 218);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 66);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Emotional Decision Making";
            // 
            // buttonEditEmotionalDecisionMaking
            // 
            this.buttonEditEmotionalDecisionMaking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditEmotionalDecisionMaking.Location = new System.Drawing.Point(304, 26);
            this.buttonEditEmotionalDecisionMaking.Name = "buttonEditEmotionalDecisionMaking";
            this.buttonEditEmotionalDecisionMaking.Size = new System.Drawing.Size(61, 23);
            this.buttonEditEmotionalDecisionMaking.TabIndex = 15;
            this.buttonEditEmotionalDecisionMaking.Text = "Edit";
            this.buttonEditEmotionalDecisionMaking.UseVisualStyleBackColor = true;
            this.buttonEditEmotionalDecisionMaking.Click += new System.EventHandler(this.buttonEditEmotionalDecisionMaking_Click);
            // 
            // buttonSetEmotionalDecisionMakingSource
            // 
            this.buttonSetEmotionalDecisionMakingSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetEmotionalDecisionMakingSource.Location = new System.Drawing.Point(237, 26);
            this.buttonSetEmotionalDecisionMakingSource.Name = "buttonSetEmotionalDecisionMakingSource";
            this.buttonSetEmotionalDecisionMakingSource.Size = new System.Drawing.Size(61, 23);
            this.buttonSetEmotionalDecisionMakingSource.TabIndex = 14;
            this.buttonSetEmotionalDecisionMakingSource.Text = "Set";
            this.buttonSetEmotionalDecisionMakingSource.UseVisualStyleBackColor = true;
            this.buttonSetEmotionalDecisionMakingSource.Click += new System.EventHandler(this.buttonSetEmotionalDecisionMakingSource_Click);
            // 
            // textBoxEmotionalDecisionMakingSource
            // 
            this.textBoxEmotionalDecisionMakingSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEmotionalDecisionMakingSource.Location = new System.Drawing.Point(6, 28);
            this.textBoxEmotionalDecisionMakingSource.Name = "textBoxEmotionalDecisionMakingSource";
            this.textBoxEmotionalDecisionMakingSource.ReadOnly = true;
            this.textBoxEmotionalDecisionMakingSource.Size = new System.Drawing.Size(225, 20);
            this.textBoxEmotionalDecisionMakingSource.TabIndex = 13;
            this.textBoxEmotionalDecisionMakingSource.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Character Body:";
            // 
            // textBoxCharacterBody
            // 
            this.textBoxCharacterBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCharacterBody.Location = new System.Drawing.Point(119, 84);
            this.textBoxCharacterBody.Name = "textBoxCharacterBody";
            this.textBoxCharacterBody.Size = new System.Drawing.Size(264, 20);
            this.textBoxCharacterBody.TabIndex = 4;
            this.textBoxCharacterBody.TextChanged += new System.EventHandler(this.textBoxCharacterBody_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(396, 301);
            this.Controls.Add(this.textBoxCharacterBody);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxCharacterName);
            this.Controls.Add(this.mainMenu);
            this.MaximumSize = new System.Drawing.Size(600, 380);
            this.MinimumSize = new System.Drawing.Size(300, 340);
            this.Name = "MainForm";
            this.Text = "RolePlayCharacter";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxCharacterName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSetEmotionalAppraisalSource;
        private System.Windows.Forms.TextBox textBoxEmotionalAppraisalSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxEmotionalDecisionMakingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCharacterBody;
        private System.Windows.Forms.Button buttonEditEmotionalAppraisal;
        private System.Windows.Forms.Button buttonEditEmotionalDecisionMaking;
        private System.Windows.Forms.Button buttonSetEmotionalDecisionMakingSource;
    }
}


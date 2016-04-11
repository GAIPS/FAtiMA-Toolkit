namespace EmotionalDecisionMakingWF
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonEditAppraisalRule = new System.Windows.Forms.Button();
            this.buttonAddAppraisalRule = new System.Windows.Forms.Button();
            this.buttonRemoveAppraisalRule = new System.Windows.Forms.Button();
            this.dataGridViewAppraisalRules = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.comboBoxQuantifierType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonEditAppraisalRuleCondition = new System.Windows.Forms.Button();
            this.buttonAddAppraisalRuleCondition = new System.Windows.Forms.Button();
            this.buttonRemoveAppraisalRuleCondition = new System.Windows.Forms.Button();
            this.dataGridViewAppRuleConditions = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            this.mainMenu.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppraisalRules)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppRuleConditions)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(474, 24);
            this.mainMenu.TabIndex = 1;
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
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.buttonEditAppraisalRule);
            this.groupBox7.Controls.Add(this.buttonAddAppraisalRule);
            this.groupBox7.Controls.Add(this.buttonRemoveAppraisalRule);
            this.groupBox7.Controls.Add(this.dataGridViewAppraisalRules);
            this.groupBox7.Location = new System.Drawing.Point(12, 27);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(450, 240);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Reactions";
            // 
            // buttonEditAppraisalRule
            // 
            this.buttonEditAppraisalRule.Location = new System.Drawing.Point(66, 19);
            this.buttonEditAppraisalRule.Name = "buttonEditAppraisalRule";
            this.buttonEditAppraisalRule.Size = new System.Drawing.Size(70, 23);
            this.buttonEditAppraisalRule.TabIndex = 9;
            this.buttonEditAppraisalRule.Text = "Edit";
            this.buttonEditAppraisalRule.UseVisualStyleBackColor = true;
            // 
            // buttonAddAppraisalRule
            // 
            this.buttonAddAppraisalRule.Location = new System.Drawing.Point(6, 19);
            this.buttonAddAppraisalRule.Name = "buttonAddAppraisalRule";
            this.buttonAddAppraisalRule.Size = new System.Drawing.Size(54, 23);
            this.buttonAddAppraisalRule.TabIndex = 7;
            this.buttonAddAppraisalRule.Text = "Add";
            this.buttonAddAppraisalRule.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveAppraisalRule
            // 
            this.buttonRemoveAppraisalRule.Location = new System.Drawing.Point(142, 19);
            this.buttonRemoveAppraisalRule.Name = "buttonRemoveAppraisalRule";
            this.buttonRemoveAppraisalRule.Size = new System.Drawing.Size(70, 23);
            this.buttonRemoveAppraisalRule.TabIndex = 8;
            this.buttonRemoveAppraisalRule.Text = "Remove";
            this.buttonRemoveAppraisalRule.UseVisualStyleBackColor = true;
            // 
            // dataGridViewAppraisalRules
            // 
            this.dataGridViewAppraisalRules.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewAppraisalRules.AllowUserToAddRows = false;
            this.dataGridViewAppraisalRules.AllowUserToOrderColumns = true;
            this.dataGridViewAppraisalRules.AllowUserToResizeRows = false;
            this.dataGridViewAppraisalRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAppraisalRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAppraisalRules.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewAppraisalRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAppraisalRules.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewAppraisalRules.Location = new System.Drawing.Point(6, 54);
            this.dataGridViewAppraisalRules.Name = "dataGridViewAppraisalRules";
            this.dataGridViewAppraisalRules.ReadOnly = true;
            this.dataGridViewAppraisalRules.RowHeadersVisible = false;
            this.dataGridViewAppraisalRules.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAppraisalRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAppraisalRules.Size = new System.Drawing.Size(438, 180);
            this.dataGridViewAppraisalRules.TabIndex = 2;
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.comboBoxQuantifierType);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.buttonEditAppraisalRuleCondition);
            this.groupBox8.Controls.Add(this.buttonAddAppraisalRuleCondition);
            this.groupBox8.Controls.Add(this.buttonRemoveAppraisalRuleCondition);
            this.groupBox8.Controls.Add(this.dataGridViewAppRuleConditions);
            this.groupBox8.Location = new System.Drawing.Point(12, 273);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(451, 172);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Reaction Conditions";
            // 
            // comboBoxQuantifierType
            // 
            this.comboBoxQuantifierType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxQuantifierType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQuantifierType.FormattingEnabled = true;
            this.comboBoxQuantifierType.Location = new System.Drawing.Point(322, 19);
            this.comboBoxQuantifierType.Name = "comboBoxQuantifierType";
            this.comboBoxQuantifierType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxQuantifierType.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Quantifier Type:";
            // 
            // buttonEditAppraisalRuleCondition
            // 
            this.buttonEditAppraisalRuleCondition.Location = new System.Drawing.Point(66, 18);
            this.buttonEditAppraisalRuleCondition.Name = "buttonEditAppraisalRuleCondition";
            this.buttonEditAppraisalRuleCondition.Size = new System.Drawing.Size(70, 23);
            this.buttonEditAppraisalRuleCondition.TabIndex = 9;
            this.buttonEditAppraisalRuleCondition.Text = "Edit";
            this.buttonEditAppraisalRuleCondition.UseVisualStyleBackColor = true;
            // 
            // buttonAddAppraisalRuleCondition
            // 
            this.buttonAddAppraisalRuleCondition.Location = new System.Drawing.Point(6, 18);
            this.buttonAddAppraisalRuleCondition.Name = "buttonAddAppraisalRuleCondition";
            this.buttonAddAppraisalRuleCondition.Size = new System.Drawing.Size(54, 23);
            this.buttonAddAppraisalRuleCondition.TabIndex = 7;
            this.buttonAddAppraisalRuleCondition.Text = "Add";
            this.buttonAddAppraisalRuleCondition.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveAppraisalRuleCondition
            // 
            this.buttonRemoveAppraisalRuleCondition.Location = new System.Drawing.Point(142, 18);
            this.buttonRemoveAppraisalRuleCondition.Name = "buttonRemoveAppraisalRuleCondition";
            this.buttonRemoveAppraisalRuleCondition.Size = new System.Drawing.Size(70, 23);
            this.buttonRemoveAppraisalRuleCondition.TabIndex = 8;
            this.buttonRemoveAppraisalRuleCondition.Text = "Remove";
            this.buttonRemoveAppraisalRuleCondition.UseVisualStyleBackColor = true;
            // 
            // dataGridViewAppRuleConditions
            // 
            this.dataGridViewAppRuleConditions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewAppRuleConditions.AllowUserToAddRows = false;
            this.dataGridViewAppRuleConditions.AllowUserToOrderColumns = true;
            this.dataGridViewAppRuleConditions.AllowUserToResizeRows = false;
            this.dataGridViewAppRuleConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAppRuleConditions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAppRuleConditions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewAppRuleConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAppRuleConditions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewAppRuleConditions.Location = new System.Drawing.Point(6, 54);
            this.dataGridViewAppRuleConditions.Name = "dataGridViewAppRuleConditions";
            this.dataGridViewAppRuleConditions.ReadOnly = true;
            this.dataGridViewAppRuleConditions.RowHeadersVisible = false;
            this.dataGridViewAppRuleConditions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAppRuleConditions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAppRuleConditions.Size = new System.Drawing.Size(439, 112);
            this.dataGridViewAppRuleConditions.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.richTextBoxDescription);
            this.groupBox2.Location = new System.Drawing.Point(12, 451);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 71);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reaction Description";
            // 
            // richTextBoxDescription
            // 
            this.richTextBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxDescription.CausesValidation = false;
            this.richTextBoxDescription.Location = new System.Drawing.Point(9, 19);
            this.richTextBoxDescription.Multiline = false;
            this.richTextBoxDescription.Name = "richTextBoxDescription";
            this.richTextBoxDescription.Size = new System.Drawing.Size(435, 46);
            this.richTextBoxDescription.TabIndex = 0;
            this.richTextBoxDescription.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 534);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.mainMenu);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppraisalRules)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppRuleConditions)).EndInit();
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonEditAppraisalRule;
        private System.Windows.Forms.Button buttonAddAppraisalRule;
        private System.Windows.Forms.Button buttonRemoveAppraisalRule;
        private System.Windows.Forms.DataGridView dataGridViewAppraisalRules;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox comboBoxQuantifierType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonEditAppraisalRuleCondition;
        private System.Windows.Forms.Button buttonAddAppraisalRuleCondition;
        private System.Windows.Forms.Button buttonRemoveAppraisalRuleCondition;
        private System.Windows.Forms.DataGridView dataGridViewAppRuleConditions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBoxDescription;
    }
}


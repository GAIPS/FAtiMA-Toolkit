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
            this.buttonEditReaction = new System.Windows.Forms.Button();
            this.buttonAddReaction = new System.Windows.Forms.Button();
            this.buttonRemoveReaction = new System.Windows.Forms.Button();
            this.dataGridViewReactiveActions = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.comboBoxQuantifierType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonEditReactionCondition = new System.Windows.Forms.Button();
            this.buttonAddReactionCondition = new System.Windows.Forms.Button();
            this.buttonRemoveReactionCondition = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            this.dataGridViewReactionConditions = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mainMenu.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactiveActions)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactionConditions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(519, 24);
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
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsStripMenuItem
            // 
            this.saveAsStripMenuItem.Name = "saveAsStripMenuItem";
            this.saveAsStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsStripMenuItem.Text = "Save &As...";
            this.saveAsStripMenuItem.Click += new System.EventHandler(this.saveAsStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonEditReaction);
            this.groupBox7.Controls.Add(this.buttonAddReaction);
            this.groupBox7.Controls.Add(this.buttonRemoveReaction);
            this.groupBox7.Controls.Add(this.dataGridViewReactiveActions);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(495, 276);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Reactions";
            // 
            // buttonEditReaction
            // 
            this.buttonEditReaction.Location = new System.Drawing.Point(66, 19);
            this.buttonEditReaction.Name = "buttonEditReaction";
            this.buttonEditReaction.Size = new System.Drawing.Size(70, 23);
            this.buttonEditReaction.TabIndex = 9;
            this.buttonEditReaction.Text = "Edit";
            this.buttonEditReaction.UseVisualStyleBackColor = true;
            this.buttonEditReaction.Click += new System.EventHandler(this.buttonEditReaction_Click);
            // 
            // buttonAddReaction
            // 
            this.buttonAddReaction.Location = new System.Drawing.Point(6, 19);
            this.buttonAddReaction.Name = "buttonAddReaction";
            this.buttonAddReaction.Size = new System.Drawing.Size(54, 23);
            this.buttonAddReaction.TabIndex = 7;
            this.buttonAddReaction.Text = "Add";
            this.buttonAddReaction.UseVisualStyleBackColor = true;
            this.buttonAddReaction.Click += new System.EventHandler(this.buttonAddReaction_Click);
            // 
            // buttonRemoveReaction
            // 
            this.buttonRemoveReaction.Location = new System.Drawing.Point(142, 19);
            this.buttonRemoveReaction.Name = "buttonRemoveReaction";
            this.buttonRemoveReaction.Size = new System.Drawing.Size(70, 23);
            this.buttonRemoveReaction.TabIndex = 8;
            this.buttonRemoveReaction.Text = "Remove";
            this.buttonRemoveReaction.UseVisualStyleBackColor = true;
            this.buttonRemoveReaction.Click += new System.EventHandler(this.buttonRemoveReaction_Click);
            // 
            // dataGridViewReactiveActions
            // 
            this.dataGridViewReactiveActions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewReactiveActions.AllowUserToAddRows = false;
            this.dataGridViewReactiveActions.AllowUserToDeleteRows = false;
            this.dataGridViewReactiveActions.AllowUserToOrderColumns = true;
            this.dataGridViewReactiveActions.AllowUserToResizeRows = false;
            this.dataGridViewReactiveActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReactiveActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReactiveActions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewReactiveActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReactiveActions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewReactiveActions.Location = new System.Drawing.Point(6, 54);
            this.dataGridViewReactiveActions.Name = "dataGridViewReactiveActions";
            this.dataGridViewReactiveActions.ReadOnly = true;
            this.dataGridViewReactiveActions.RowHeadersVisible = false;
            this.dataGridViewReactiveActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewReactiveActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReactiveActions.Size = new System.Drawing.Size(483, 216);
            this.dataGridViewReactiveActions.TabIndex = 2;
            this.dataGridViewReactiveActions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewReactiveActions_CellMouseDoubleClick);
            this.dataGridViewReactiveActions.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReactiveActions_RowEnter);
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.comboBoxQuantifierType);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.buttonEditReactionCondition);
            this.groupBox8.Controls.Add(this.buttonAddReactionCondition);
            this.groupBox8.Controls.Add(this.buttonRemoveReactionCondition);
            this.groupBox8.Controls.Add(this.dataGridViewReactionConditions);
            this.groupBox8.Location = new System.Drawing.Point(0, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(495, 171);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Reaction Conditions";
            // 
            // comboBoxQuantifierType
            // 
            this.comboBoxQuantifierType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxQuantifierType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQuantifierType.FormattingEnabled = true;
            this.comboBoxQuantifierType.Location = new System.Drawing.Point(368, 18);
            this.comboBoxQuantifierType.Name = "comboBoxQuantifierType";
            this.comboBoxQuantifierType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxQuantifierType.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Quantifier Type:";
            // 
            // buttonEditReactionCondition
            // 
            this.buttonEditReactionCondition.Location = new System.Drawing.Point(66, 18);
            this.buttonEditReactionCondition.Name = "buttonEditReactionCondition";
            this.buttonEditReactionCondition.Size = new System.Drawing.Size(70, 23);
            this.buttonEditReactionCondition.TabIndex = 9;
            this.buttonEditReactionCondition.Text = "Edit";
            this.buttonEditReactionCondition.UseVisualStyleBackColor = true;
            this.buttonEditReactionCondition.Click += new System.EventHandler(this.buttonEditReactionCondition_Click);
            // 
            // buttonAddReactionCondition
            // 
            this.buttonAddReactionCondition.Location = new System.Drawing.Point(6, 18);
            this.buttonAddReactionCondition.Name = "buttonAddReactionCondition";
            this.buttonAddReactionCondition.Size = new System.Drawing.Size(54, 23);
            this.buttonAddReactionCondition.TabIndex = 7;
            this.buttonAddReactionCondition.Text = "Add";
            this.buttonAddReactionCondition.UseVisualStyleBackColor = true;
            this.buttonAddReactionCondition.Click += new System.EventHandler(this.buttonAddReactionCondition_Click);
            // 
            // buttonRemoveReactionCondition
            // 
            this.buttonRemoveReactionCondition.Location = new System.Drawing.Point(142, 18);
            this.buttonRemoveReactionCondition.Name = "buttonRemoveReactionCondition";
            this.buttonRemoveReactionCondition.Size = new System.Drawing.Size(70, 23);
            this.buttonRemoveReactionCondition.TabIndex = 8;
            this.buttonRemoveReactionCondition.Text = "Remove";
            this.buttonRemoveReactionCondition.UseVisualStyleBackColor = true;
            this.buttonRemoveReactionCondition.Click += new System.EventHandler(this.buttonRemoveReactionCondition_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.richTextBoxDescription);
            this.groupBox2.Location = new System.Drawing.Point(0, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(495, 89);
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
            this.richTextBoxDescription.Size = new System.Drawing.Size(480, 64);
            this.richTextBoxDescription.TabIndex = 0;
            this.richTextBoxDescription.Text = "";
            // 
            // dataGridViewReactionConditions
            // 
            this.dataGridViewReactionConditions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewReactionConditions.AllowUserToAddRows = false;
            this.dataGridViewReactionConditions.AllowUserToDeleteRows = false;
            this.dataGridViewReactionConditions.AllowUserToOrderColumns = true;
            this.dataGridViewReactionConditions.AllowUserToResizeRows = false;
            this.dataGridViewReactionConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReactionConditions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReactionConditions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewReactionConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReactionConditions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewReactionConditions.Location = new System.Drawing.Point(6, 54);
            this.dataGridViewReactionConditions.Name = "dataGridViewReactionConditions";
            this.dataGridViewReactionConditions.ReadOnly = true;
            this.dataGridViewReactionConditions.RowHeadersVisible = false;
            this.dataGridViewReactionConditions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewReactionConditions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReactionConditions.Size = new System.Drawing.Size(483, 111);
            this.dataGridViewReactionConditions.TabIndex = 2;
            this.dataGridViewReactionConditions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewReactionConditions_CellMouseDoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox8);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(495, 553);
            this.splitContainer1.SplitterDistance = 276;
            this.splitContainer1.TabIndex = 17;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 592);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainMenu);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactiveActions)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactionConditions)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.Button buttonEditReaction;
        private System.Windows.Forms.Button buttonAddReaction;
        private System.Windows.Forms.Button buttonRemoveReaction;
        private System.Windows.Forms.DataGridView dataGridViewReactiveActions;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox comboBoxQuantifierType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonEditReactionCondition;
        private System.Windows.Forms.Button buttonAddReactionCondition;
        private System.Windows.Forms.Button buttonRemoveReactionCondition;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBoxDescription;
        private System.Windows.Forms.DataGridView dataGridViewReactionConditions;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}


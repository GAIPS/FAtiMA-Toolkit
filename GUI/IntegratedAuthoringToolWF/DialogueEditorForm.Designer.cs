namespace IntegratedAuthoringToolWF
{
    partial class DialogueEditorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromtxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToSpeachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonAgentRemoveDialogAction = new System.Windows.Forms.Button();
            this.buttonAgentAddDialogAction = new System.Windows.Forms.Button();
            this.dataGridViewAgentDialogueActions = new System.Windows.Forms.DataGridView();
            this.buttonAgentEditDialogAction = new System.Windows.Forms.Button();
            this.buttonAgentDuplicateDialogueAction = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPlayerRemoveDialogueAction = new System.Windows.Forms.Button();
            this.buttonAddPlayerDialogueAction = new System.Windows.Forms.Button();
            this.dataGridViewPlayerDialogueActions = new System.Windows.Forms.DataGridView();
            this.buttonPlayerEditDialogueAction = new System.Windows.Forms.Button();
            this.buttonPlayerDuplicateDialogueAction = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mainMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAgentDialogueActions)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlayerDialogueActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(972, 24);
            this.mainMenu.TabIndex = 16;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.importFromtxtToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.importToolStripMenuItem.Text = "&Import from Excel";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.exportToolStripMenuItem.Text = "&Export to Excel";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // importFromtxtToolStripMenuItem
            // 
            this.importFromtxtToolStripMenuItem.Name = "importFromtxtToolStripMenuItem";
            this.importFromtxtToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.importFromtxtToolStripMenuItem.Text = "Import Script from .txt";
            this.importFromtxtToolStripMenuItem.Click += new System.EventHandler(this.importFromtxtToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textToSpeachToolStripMenuItem,
            this.validateToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // textToSpeachToolStripMenuItem
            // 
            this.textToSpeachToolStripMenuItem.Name = "textToSpeachToolStripMenuItem";
            this.textToSpeachToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.textToSpeachToolStripMenuItem.Text = "Text To Speech";
            this.textToSpeachToolStripMenuItem.Click += new System.EventHandler(this.textToSpeachToolStripMenuItem_Click);
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.validateToolStripMenuItem.Text = "Validate";
            this.validateToolStripMenuItem.Click += new System.EventHandler(this.validateToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonAgentDuplicateDialogueAction);
            this.groupBox2.Controls.Add(this.buttonAgentEditDialogAction);
            this.groupBox2.Controls.Add(this.dataGridViewAgentDialogueActions);
            this.groupBox2.Controls.Add(this.buttonAgentAddDialogAction);
            this.groupBox2.Controls.Add(this.buttonAgentRemoveDialogAction);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(948, 292);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Agent Dialogue Actions";
            // 
            // buttonAgentRemoveDialogAction
            // 
            this.buttonAgentRemoveDialogAction.Location = new System.Drawing.Point(202, 19);
            this.buttonAgentRemoveDialogAction.Name = "buttonAgentRemoveDialogAction";
            this.buttonAgentRemoveDialogAction.Size = new System.Drawing.Size(70, 23);
            this.buttonAgentRemoveDialogAction.TabIndex = 11;
            this.buttonAgentRemoveDialogAction.Text = "Remove";
            this.buttonAgentRemoveDialogAction.UseVisualStyleBackColor = true;
            this.buttonAgentRemoveDialogAction.Click += new System.EventHandler(this.buttonAgentRemoveDialogAction_Click);
            // 
            // buttonAgentAddDialogAction
            // 
            this.buttonAgentAddDialogAction.Location = new System.Drawing.Point(6, 19);
            this.buttonAgentAddDialogAction.Name = "buttonAgentAddDialogAction";
            this.buttonAgentAddDialogAction.Size = new System.Drawing.Size(54, 23);
            this.buttonAgentAddDialogAction.TabIndex = 10;
            this.buttonAgentAddDialogAction.Text = "Add";
            this.buttonAgentAddDialogAction.UseVisualStyleBackColor = true;
            this.buttonAgentAddDialogAction.Click += new System.EventHandler(this.buttonAgentAddDialogAction_Click);
            // 
            // dataGridViewAgentDialogueActions
            // 
            this.dataGridViewAgentDialogueActions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewAgentDialogueActions.AllowUserToAddRows = false;
            this.dataGridViewAgentDialogueActions.AllowUserToDeleteRows = false;
            this.dataGridViewAgentDialogueActions.AllowUserToOrderColumns = true;
            this.dataGridViewAgentDialogueActions.AllowUserToResizeRows = false;
            this.dataGridViewAgentDialogueActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAgentDialogueActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAgentDialogueActions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAgentDialogueActions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewAgentDialogueActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAgentDialogueActions.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewAgentDialogueActions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewAgentDialogueActions.Location = new System.Drawing.Point(6, 51);
            this.dataGridViewAgentDialogueActions.Name = "dataGridViewAgentDialogueActions";
            this.dataGridViewAgentDialogueActions.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAgentDialogueActions.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewAgentDialogueActions.RowHeadersVisible = false;
            this.dataGridViewAgentDialogueActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAgentDialogueActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAgentDialogueActions.Size = new System.Drawing.Size(936, 235);
            this.dataGridViewAgentDialogueActions.TabIndex = 13;
            // 
            // buttonAgentEditDialogAction
            // 
            this.buttonAgentEditDialogAction.Location = new System.Drawing.Point(66, 19);
            this.buttonAgentEditDialogAction.Name = "buttonAgentEditDialogAction";
            this.buttonAgentEditDialogAction.Size = new System.Drawing.Size(54, 23);
            this.buttonAgentEditDialogAction.TabIndex = 14;
            this.buttonAgentEditDialogAction.Text = "Edit";
            this.buttonAgentEditDialogAction.UseVisualStyleBackColor = true;
            this.buttonAgentEditDialogAction.Click += new System.EventHandler(this.buttonAgentEditDialogAction_Click);
            // 
            // buttonAgentDuplicateDialogueAction
            // 
            this.buttonAgentDuplicateDialogueAction.Location = new System.Drawing.Point(126, 19);
            this.buttonAgentDuplicateDialogueAction.Name = "buttonAgentDuplicateDialogueAction";
            this.buttonAgentDuplicateDialogueAction.Size = new System.Drawing.Size(70, 23);
            this.buttonAgentDuplicateDialogueAction.TabIndex = 16;
            this.buttonAgentDuplicateDialogueAction.Text = "Duplicate";
            this.buttonAgentDuplicateDialogueAction.UseVisualStyleBackColor = true;
            this.buttonAgentDuplicateDialogueAction.Click += new System.EventHandler(this.buttonAgentDuplicateDialogueAction_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPlayerDuplicateDialogueAction);
            this.groupBox1.Controls.Add(this.buttonPlayerEditDialogueAction);
            this.groupBox1.Controls.Add(this.dataGridViewPlayerDialogueActions);
            this.groupBox1.Controls.Add(this.buttonAddPlayerDialogueAction);
            this.groupBox1.Controls.Add(this.buttonPlayerRemoveDialogueAction);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(948, 293);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Player Dialogue Actions";
            // 
            // buttonPlayerRemoveDialogueAction
            // 
            this.buttonPlayerRemoveDialogueAction.Location = new System.Drawing.Point(202, 19);
            this.buttonPlayerRemoveDialogueAction.Name = "buttonPlayerRemoveDialogueAction";
            this.buttonPlayerRemoveDialogueAction.Size = new System.Drawing.Size(70, 23);
            this.buttonPlayerRemoveDialogueAction.TabIndex = 11;
            this.buttonPlayerRemoveDialogueAction.Text = "Remove";
            this.buttonPlayerRemoveDialogueAction.UseVisualStyleBackColor = true;
            this.buttonPlayerRemoveDialogueAction.Click += new System.EventHandler(this.buttonPlayerRemoveDialogueAction_Click);
            // 
            // buttonAddPlayerDialogueAction
            // 
            this.buttonAddPlayerDialogueAction.Location = new System.Drawing.Point(6, 19);
            this.buttonAddPlayerDialogueAction.Name = "buttonAddPlayerDialogueAction";
            this.buttonAddPlayerDialogueAction.Size = new System.Drawing.Size(54, 23);
            this.buttonAddPlayerDialogueAction.TabIndex = 10;
            this.buttonAddPlayerDialogueAction.Text = "Add";
            this.buttonAddPlayerDialogueAction.UseVisualStyleBackColor = true;
            this.buttonAddPlayerDialogueAction.Click += new System.EventHandler(this.buttonAddPlayerDialogueAction_Click);
            // 
            // dataGridViewPlayerDialogueActions
            // 
            this.dataGridViewPlayerDialogueActions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewPlayerDialogueActions.AllowUserToAddRows = false;
            this.dataGridViewPlayerDialogueActions.AllowUserToDeleteRows = false;
            this.dataGridViewPlayerDialogueActions.AllowUserToOrderColumns = true;
            this.dataGridViewPlayerDialogueActions.AllowUserToResizeRows = false;
            this.dataGridViewPlayerDialogueActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPlayerDialogueActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPlayerDialogueActions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPlayerDialogueActions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewPlayerDialogueActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPlayerDialogueActions.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewPlayerDialogueActions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewPlayerDialogueActions.Location = new System.Drawing.Point(6, 51);
            this.dataGridViewPlayerDialogueActions.Name = "dataGridViewPlayerDialogueActions";
            this.dataGridViewPlayerDialogueActions.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPlayerDialogueActions.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewPlayerDialogueActions.RowHeadersVisible = false;
            this.dataGridViewPlayerDialogueActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPlayerDialogueActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPlayerDialogueActions.Size = new System.Drawing.Size(936, 236);
            this.dataGridViewPlayerDialogueActions.TabIndex = 13;
            this.dataGridViewPlayerDialogueActions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPlayerDialogueActions_CellContentClick);
            // 
            // buttonPlayerEditDialogueAction
            // 
            this.buttonPlayerEditDialogueAction.Location = new System.Drawing.Point(66, 19);
            this.buttonPlayerEditDialogueAction.Name = "buttonPlayerEditDialogueAction";
            this.buttonPlayerEditDialogueAction.Size = new System.Drawing.Size(54, 23);
            this.buttonPlayerEditDialogueAction.TabIndex = 14;
            this.buttonPlayerEditDialogueAction.Text = "Edit";
            this.buttonPlayerEditDialogueAction.UseVisualStyleBackColor = true;
            this.buttonPlayerEditDialogueAction.Click += new System.EventHandler(this.buttonPlayerEditDialogueAction_Click);
            // 
            // buttonPlayerDuplicateDialogueAction
            // 
            this.buttonPlayerDuplicateDialogueAction.Location = new System.Drawing.Point(126, 19);
            this.buttonPlayerDuplicateDialogueAction.Name = "buttonPlayerDuplicateDialogueAction";
            this.buttonPlayerDuplicateDialogueAction.Size = new System.Drawing.Size(70, 23);
            this.buttonPlayerDuplicateDialogueAction.TabIndex = 15;
            this.buttonPlayerDuplicateDialogueAction.Text = "Duplicate";
            this.buttonPlayerDuplicateDialogueAction.UseVisualStyleBackColor = true;
            this.buttonPlayerDuplicateDialogueAction.Click += new System.EventHandler(this.buttonPlayerDuplicateDialogueAction_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(948, 589);
            this.splitContainer1.SplitterDistance = 293;
            this.splitContainer1.TabIndex = 17;
            // 
            // DialogueEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 628);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainMenu);
            this.Name = "DialogueEditorForm";
            this.Text = "Dialogue Editor";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAgentDialogueActions)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlayerDialogueActions)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromtxtToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem textToSpeachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonAgentDuplicateDialogueAction;
        private System.Windows.Forms.Button buttonAgentEditDialogAction;
        private System.Windows.Forms.DataGridView dataGridViewAgentDialogueActions;
        private System.Windows.Forms.Button buttonAgentAddDialogAction;
        private System.Windows.Forms.Button buttonAgentRemoveDialogAction;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonPlayerDuplicateDialogueAction;
        private System.Windows.Forms.Button buttonPlayerEditDialogueAction;
        private System.Windows.Forms.DataGridView dataGridViewPlayerDialogueActions;
        private System.Windows.Forms.Button buttonAddPlayerDialogueAction;
        private System.Windows.Forms.Button buttonPlayerRemoveDialogueAction;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
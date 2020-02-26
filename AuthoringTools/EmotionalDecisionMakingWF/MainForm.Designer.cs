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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonDuplicateReaction = new System.Windows.Forms.Button();
            this.buttonEditReaction = new System.Windows.Forms.Button();
            this.buttonAddReaction = new System.Windows.Forms.Button();
            this.buttonRemoveReaction = new System.Windows.Forms.Button();
            this.dataGridViewReactiveActions = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.conditionSetEditor = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactiveActions)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonDuplicateReaction);
            this.groupBox7.Controls.Add(this.buttonEditReaction);
            this.groupBox7.Controls.Add(this.buttonAddReaction);
            this.groupBox7.Controls.Add(this.buttonRemoveReaction);
            this.groupBox7.Controls.Add(this.dataGridViewReactiveActions);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(1021, 384);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Action Rules";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // buttonDuplicateReaction
            // 
            this.buttonDuplicateReaction.Location = new System.Drawing.Point(189, 23);
            this.buttonDuplicateReaction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDuplicateReaction.Name = "buttonDuplicateReaction";
            this.buttonDuplicateReaction.Size = new System.Drawing.Size(93, 28);
            this.buttonDuplicateReaction.TabIndex = 9;
            this.buttonDuplicateReaction.Text = "Duplicate";
            this.buttonDuplicateReaction.UseVisualStyleBackColor = true;
            this.buttonDuplicateReaction.Click += new System.EventHandler(this.buttonDuplicateReaction_Click);
            // 
            // buttonEditReaction
            // 
            this.buttonEditReaction.Location = new System.Drawing.Point(88, 23);
            this.buttonEditReaction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditReaction.Name = "buttonEditReaction";
            this.buttonEditReaction.Size = new System.Drawing.Size(93, 28);
            this.buttonEditReaction.TabIndex = 8;
            this.buttonEditReaction.Text = "Edit";
            this.buttonEditReaction.UseVisualStyleBackColor = true;
            this.buttonEditReaction.Click += new System.EventHandler(this.buttonEditReaction_Click);
            // 
            // buttonAddReaction
            // 
            this.buttonAddReaction.Location = new System.Drawing.Point(8, 23);
            this.buttonAddReaction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddReaction.Name = "buttonAddReaction";
            this.buttonAddReaction.Size = new System.Drawing.Size(72, 28);
            this.buttonAddReaction.TabIndex = 7;
            this.buttonAddReaction.Text = "Add";
            this.buttonAddReaction.UseVisualStyleBackColor = true;
            this.buttonAddReaction.Click += new System.EventHandler(this.buttonAddReaction_Click);
            // 
            // buttonRemoveReaction
            // 
            this.buttonRemoveReaction.Location = new System.Drawing.Point(291, 23);
            this.buttonRemoveReaction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveReaction.Name = "buttonRemoveReaction";
            this.buttonRemoveReaction.Size = new System.Drawing.Size(93, 28);
            this.buttonRemoveReaction.TabIndex = 10;
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
            this.dataGridViewReactiveActions.Location = new System.Drawing.Point(8, 66);
            this.dataGridViewReactiveActions.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewReactiveActions.Name = "dataGridViewReactiveActions";
            this.dataGridViewReactiveActions.ReadOnly = true;
            this.dataGridViewReactiveActions.RowHeadersVisible = false;
            this.dataGridViewReactiveActions.RowHeadersWidth = 51;
            this.dataGridViewReactiveActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewReactiveActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReactiveActions.Size = new System.Drawing.Size(1005, 310);
            this.dataGridViewReactiveActions.TabIndex = 2;
            this.dataGridViewReactiveActions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReactiveActions_CellContentClick);
            this.dataGridViewReactiveActions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewReactiveActions_CellMouseDoubleClick);
            this.dataGridViewReactiveActions.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReactiveActions_RowEnter);
            this.dataGridViewReactiveActions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewReactiveActions_KeyDown);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.conditionSetEditor);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(1021, 475);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Conditions";
            // 
            // conditionSetEditor
            // 
            this.conditionSetEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionSetEditor.Location = new System.Drawing.Point(4, 23);
            this.conditionSetEditor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conditionSetEditor.Name = "conditionSetEditor";
            this.conditionSetEditor.Size = new System.Drawing.Size(1013, 448);
            this.conditionSetEditor.TabIndex = 0;
            this.conditionSetEditor.View = null;
            this.conditionSetEditor.Load += new System.EventHandler(this.conditionSetEditor_Load);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 48);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
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
            this.splitContainer1.Size = new System.Drawing.Size(1021, 864);
            this.splitContainer1.SplitterDistance = 384;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 17;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1021, 38);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 34);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 729);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Emotional Decision Making";
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactiveActions)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonEditReaction;
        private System.Windows.Forms.Button buttonAddReaction;
        private System.Windows.Forms.Button buttonRemoveReaction;
        private System.Windows.Forms.DataGridView dataGridViewReactiveActions;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.SplitContainer splitContainer1;
		private GAIPS.AssetEditorTools.ConditionSetEditorControl conditionSetEditor;
        private System.Windows.Forms.Button buttonDuplicateReaction;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    }
}


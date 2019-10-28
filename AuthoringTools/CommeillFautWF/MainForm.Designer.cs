namespace CommeillFautWF
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
            this.components = new System.ComponentModel.Container();
            this.socialExchangeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.influenceRules = new System.Windows.Forms.Button();
            this.buttonDuplicateSE = new System.Windows.Forms.Button();
            this.buttonEditSE = new System.Windows.Forms.Button();
            this.buttonAddSE = new System.Windows.Forms.Button();
            this.buttonRemoveSE = new System.Windows.Forms.Button();
            this.gridSocialExchanges = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.conditionSetEditorControl = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            this.triggerRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSocialExchanges)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.triggerRulesBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // socialExchangeBindingSource
            // 
            this.socialExchangeBindingSource.DataSource = typeof(CommeillFaut.SocialExchange);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(833, 410);
            this.splitContainer1.SplitterDistance = 202;
            this.splitContainer1.TabIndex = 16;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.influenceRules);
            this.groupBox7.Controls.Add(this.buttonDuplicateSE);
            this.groupBox7.Controls.Add(this.buttonEditSE);
            this.groupBox7.Controls.Add(this.buttonAddSE);
            this.groupBox7.Controls.Add(this.buttonRemoveSE);
            this.groupBox7.Controls.Add(this.gridSocialExchanges);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Size = new System.Drawing.Size(833, 202);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Social Exchanges";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // influenceRules
            // 
            this.influenceRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.influenceRules.Location = new System.Drawing.Point(429, 24);
            this.influenceRules.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.influenceRules.Name = "influenceRules";
            this.influenceRules.Size = new System.Drawing.Size(369, 33);
            this.influenceRules.TabIndex = 11;
            this.influenceRules.Text = "Influence Rules Inspector";
            this.influenceRules.UseVisualStyleBackColor = true;
            this.influenceRules.Click += new System.EventHandler(this.influenceRules_Click);
            // 
            // buttonDuplicateSE
            // 
            this.buttonDuplicateSE.Location = new System.Drawing.Point(198, 24);
            this.buttonDuplicateSE.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDuplicateSE.Name = "buttonDuplicateSE";
            this.buttonDuplicateSE.Size = new System.Drawing.Size(89, 33);
            this.buttonDuplicateSE.TabIndex = 10;
            this.buttonDuplicateSE.Text = "Duplicate";
            this.buttonDuplicateSE.UseVisualStyleBackColor = true;
            this.buttonDuplicateSE.Click += new System.EventHandler(this.buttonDuplicateSE_Click);
            // 
            // buttonEditSE
            // 
            this.buttonEditSE.Location = new System.Drawing.Point(101, 24);
            this.buttonEditSE.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonEditSE.Name = "buttonEditSE";
            this.buttonEditSE.Size = new System.Drawing.Size(89, 33);
            this.buttonEditSE.TabIndex = 9;
            this.buttonEditSE.Text = "Edit";
            this.buttonEditSE.UseVisualStyleBackColor = true;
            this.buttonEditSE.Click += new System.EventHandler(this.buttonEditSE_Click);
            // 
            // buttonAddSE
            // 
            this.buttonAddSE.Location = new System.Drawing.Point(19, 24);
            this.buttonAddSE.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAddSE.Name = "buttonAddSE";
            this.buttonAddSE.Size = new System.Drawing.Size(74, 33);
            this.buttonAddSE.TabIndex = 7;
            this.buttonAddSE.Text = "Add";
            this.buttonAddSE.UseVisualStyleBackColor = true;
            this.buttonAddSE.Click += new System.EventHandler(this.buttonAddSE_Click);
            // 
            // buttonRemoveSE
            // 
            this.buttonRemoveSE.Location = new System.Drawing.Point(295, 24);
            this.buttonRemoveSE.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonRemoveSE.Name = "buttonRemoveSE";
            this.buttonRemoveSE.Size = new System.Drawing.Size(89, 33);
            this.buttonRemoveSE.TabIndex = 8;
            this.buttonRemoveSE.Text = "Remove";
            this.buttonRemoveSE.UseVisualStyleBackColor = true;
            this.buttonRemoveSE.Click += new System.EventHandler(this.buttonRemoveSE_Click);
            // 
            // gridSocialExchanges
            // 
            this.gridSocialExchanges.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.gridSocialExchanges.AllowUserToAddRows = false;
            this.gridSocialExchanges.AllowUserToDeleteRows = false;
            this.gridSocialExchanges.AllowUserToOrderColumns = true;
            this.gridSocialExchanges.AllowUserToResizeRows = false;
            this.gridSocialExchanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSocialExchanges.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridSocialExchanges.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridSocialExchanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSocialExchanges.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gridSocialExchanges.Location = new System.Drawing.Point(5, 69);
            this.gridSocialExchanges.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridSocialExchanges.Name = "gridSocialExchanges";
            this.gridSocialExchanges.ReadOnly = true;
            this.gridSocialExchanges.RowHeadersVisible = false;
            this.gridSocialExchanges.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSocialExchanges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSocialExchanges.Size = new System.Drawing.Size(823, 128);
            this.gridSocialExchanges.TabIndex = 2;
            this.gridSocialExchanges.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSocialExchanges_CellContentClick);
            this.gridSocialExchanges.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridSocialExchanges_CellMouseDoubleClick);
            this.gridSocialExchanges.SelectionChanged += new System.EventHandler(this.gridSocialExchanges_SelectionChanged);
            this.gridSocialExchanges.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridSocialExchanges_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.conditionSetEditorControl);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(833, 204);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Starting Conditions:";
            // 
            // conditionSetEditorControl
            // 
            this.conditionSetEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionSetEditorControl.Location = new System.Drawing.Point(4, 19);
            this.conditionSetEditorControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.conditionSetEditorControl.Name = "conditionSetEditorControl";
            this.conditionSetEditorControl.Size = new System.Drawing.Size(824, 183);
            this.conditionSetEditorControl.TabIndex = 3;
            this.conditionSetEditorControl.View = null;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filieToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(833, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filieToolStripMenuItem
            // 
            this.filieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.filieToolStripMenuItem.Name = "filieToolStripMenuItem";
            this.filieToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.filieToolStripMenuItem.Text = "File";
            this.filieToolStripMenuItem.Click += new System.EventHandler(this.filieToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveAsToolStripMenuItem.Text = "Save As ...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(833, 434);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSocialExchanges)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.triggerRulesBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource socialExchangeBindingSource;
        private System.Windows.Forms.BindingSource triggerRulesBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private GAIPS.AssetEditorTools.ConditionSetEditorControl conditionSetEditorControl;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonDuplicateSE;
        private System.Windows.Forms.Button buttonEditSE;
        private System.Windows.Forms.Button buttonAddSE;
        private System.Windows.Forms.Button buttonRemoveSE;
        private System.Windows.Forms.DataGridView gridSocialExchanges;
        private System.Windows.Forms.Button influenceRules;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    }
}


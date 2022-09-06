﻿namespace SocialImportanceWF
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonDuplicateAttRule = new System.Windows.Forms.Button();
            this.buttonEditAttRule = new System.Windows.Forms.Button();
            this.buttonAddAttRule = new System.Windows.Forms.Button();
            this.buttonRemoveAttRule = new System.Windows.Forms.Button();
            this.dataGridViewAttributionRules = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._attRuleConditionSetEditor = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttributionRules)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
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
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Size = new System.Drawing.Size(827, 515);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox7
            // 
            this.groupBox7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox7.Controls.Add(this.buttonDuplicateAttRule);
            this.groupBox7.Controls.Add(this.buttonEditAttRule);
            this.groupBox7.Controls.Add(this.buttonAddAttRule);
            this.groupBox7.Controls.Add(this.buttonRemoveAttRule);
            this.groupBox7.Controls.Add(this.dataGridViewAttributionRules);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(827, 248);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "SI Attribution Rules";
            // 
            // buttonDuplicateAttRule
            // 
            this.buttonDuplicateAttRule.Location = new System.Drawing.Point(189, 23);
            this.buttonDuplicateAttRule.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDuplicateAttRule.Name = "buttonDuplicateAttRule";
            this.buttonDuplicateAttRule.Size = new System.Drawing.Size(93, 28);
            this.buttonDuplicateAttRule.TabIndex = 9;
            this.buttonDuplicateAttRule.Text = "Duplicate";
            this.buttonDuplicateAttRule.UseVisualStyleBackColor = true;
            this.buttonDuplicateAttRule.Click += new System.EventHandler(this.buttonDuplicateAttRule_Click);
            // 
            // buttonEditAttRule
            // 
            this.buttonEditAttRule.Location = new System.Drawing.Point(88, 23);
            this.buttonEditAttRule.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditAttRule.Name = "buttonEditAttRule";
            this.buttonEditAttRule.Size = new System.Drawing.Size(93, 28);
            this.buttonEditAttRule.TabIndex = 8;
            this.buttonEditAttRule.Text = "Edit";
            this.buttonEditAttRule.UseVisualStyleBackColor = true;
            this.buttonEditAttRule.Click += new System.EventHandler(this.buttonEditAttRule_Click);
            // 
            // buttonAddAttRule
            // 
            this.buttonAddAttRule.Location = new System.Drawing.Point(8, 23);
            this.buttonAddAttRule.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddAttRule.Name = "buttonAddAttRule";
            this.buttonAddAttRule.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAddAttRule.Size = new System.Drawing.Size(72, 28);
            this.buttonAddAttRule.TabIndex = 7;
            this.buttonAddAttRule.Text = "Add";
            this.buttonAddAttRule.UseVisualStyleBackColor = true;
            this.buttonAddAttRule.Click += new System.EventHandler(this.buttonAddAttRule_Click);
            // 
            // buttonRemoveAttRule
            // 
            this.buttonRemoveAttRule.Location = new System.Drawing.Point(291, 23);
            this.buttonRemoveAttRule.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveAttRule.Name = "buttonRemoveAttRule";
            this.buttonRemoveAttRule.Size = new System.Drawing.Size(93, 28);
            this.buttonRemoveAttRule.TabIndex = 10;
            this.buttonRemoveAttRule.Text = "Remove";
            this.buttonRemoveAttRule.UseVisualStyleBackColor = true;
            this.buttonRemoveAttRule.Click += new System.EventHandler(this.buttonRemoveAttRule_Click);
            // 
            // dataGridViewAttributionRules
            // 
            this.dataGridViewAttributionRules.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewAttributionRules.AllowUserToAddRows = false;
            this.dataGridViewAttributionRules.AllowUserToDeleteRows = false;
            this.dataGridViewAttributionRules.AllowUserToOrderColumns = true;
            this.dataGridViewAttributionRules.AllowUserToResizeRows = false;
            this.dataGridViewAttributionRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAttributionRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAttributionRules.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewAttributionRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAttributionRules.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewAttributionRules.Location = new System.Drawing.Point(16, 59);
            this.dataGridViewAttributionRules.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewAttributionRules.Name = "dataGridViewAttributionRules";
            this.dataGridViewAttributionRules.ReadOnly = true;
            this.dataGridViewAttributionRules.RowHeadersVisible = false;
            this.dataGridViewAttributionRules.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAttributionRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAttributionRules.Size = new System.Drawing.Size(811, 181);
            this.dataGridViewAttributionRules.TabIndex = 11;
            this.dataGridViewAttributionRules.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAttributionRules_CellContentClick);
            this.dataGridViewAttributionRules.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewAttributionRules_CellMouseDoubleClick);
            this.dataGridViewAttributionRules.SelectionChanged += new System.EventHandler(this.dataGridViewAttributionRules_SelectionChanged);
            this.dataGridViewAttributionRules.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewAttributionRules_KeyDown);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this._attRuleConditionSetEditor);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(827, 265);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SI Rule Conditions";
            // 
            // _attRuleConditionSetEditor
            // 
            this._attRuleConditionSetEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this._attRuleConditionSetEditor.Location = new System.Drawing.Point(4, 19);
            this._attRuleConditionSetEditor.Margin = new System.Windows.Forms.Padding(5);
            this._attRuleConditionSetEditor.Name = "_attRuleConditionSetEditor";
            this._attRuleConditionSetEditor.Size = new System.Drawing.Size(819, 242);
            this._attRuleConditionSetEditor.TabIndex = 0;
            this._attRuleConditionSetEditor.View = null;
            this._attRuleConditionSetEditor.Load += new System.EventHandler(this._attRuleConditionSetEditor_Load);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(827, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 539);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttributionRules)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox4;
        private GAIPS.AssetEditorTools.ConditionSetEditorControl _attRuleConditionSetEditor;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonDuplicateAttRule;
        private System.Windows.Forms.Button buttonEditAttRule;
        private System.Windows.Forms.Button buttonAddAttRule;
        private System.Windows.Forms.Button buttonRemoveAttRule;
        private System.Windows.Forms.DataGridView dataGridViewAttributionRules;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    }
}
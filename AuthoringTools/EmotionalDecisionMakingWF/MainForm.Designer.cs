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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.emotionaAppraisalButton = new System.Windows.Forms.Button();
            this.buttonDuplicateReaction = new System.Windows.Forms.Button();
            this.buttonEditReaction = new System.Windows.Forms.Button();
            this.buttonAddReaction = new System.Windows.Forms.Button();
            this.buttonRemoveReaction = new System.Windows.Forms.Button();
            this.dataGridViewReactiveActions = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.debugGroup = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.testActionRuleResults = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.charactersComboBox = new System.Windows.Forms.ComboBox();
            this.testConditions = new System.Windows.Forms.Button();
            this.conditionSetEditor = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edmToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactiveActions)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.debugGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.emotionaAppraisalButton);
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
            this.groupBox7.Size = new System.Drawing.Size(827, 241);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Action Rules";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // emotionaAppraisalButton
            // 
            this.emotionaAppraisalButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emotionaAppraisalButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.emotionaAppraisalButton.Location = new System.Drawing.Point(625, 23);
            this.emotionaAppraisalButton.Margin = new System.Windows.Forms.Padding(4);
            this.emotionaAppraisalButton.Name = "emotionaAppraisalButton";
            this.emotionaAppraisalButton.Size = new System.Drawing.Size(179, 28);
            this.emotionaAppraisalButton.TabIndex = 11;
            this.emotionaAppraisalButton.Text = "Add Emotional Response";
            this.edmToolTip.SetToolTip(this.emotionaAppraisalButton, "Add an Appraisal Rule that matches the select action");
            this.emotionaAppraisalButton.UseVisualStyleBackColor = false;
            this.emotionaAppraisalButton.Click += new System.EventHandler(this.emotionaAppraisalButton_Click);
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
            this.edmToolTip.SetToolTip(this.buttonEditReaction, "Edit action rule");
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
            this.edmToolTip.SetToolTip(this.buttonAddReaction, "Add a new action");
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
            this.dataGridViewReactiveActions.Location = new System.Drawing.Point(0, 70);
            this.dataGridViewReactiveActions.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewReactiveActions.Name = "dataGridViewReactiveActions";
            this.dataGridViewReactiveActions.RowHeadersVisible = false;
            this.dataGridViewReactiveActions.RowHeadersWidth = 51;
            this.dataGridViewReactiveActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewReactiveActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReactiveActions.ShowCellToolTips = false;
            this.dataGridViewReactiveActions.Size = new System.Drawing.Size(811, 169);
            this.dataGridViewReactiveActions.TabIndex = 2;
            this.edmToolTip.SetToolTip(this.dataGridViewReactiveActions, resources.GetString("dataGridViewReactiveActions.ToolTip"));
            this.dataGridViewReactiveActions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReactiveActions_CellContentClick);
            this.dataGridViewReactiveActions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewReactiveActions_CellMouseDoubleClick);
            this.dataGridViewReactiveActions.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReactiveActions_RowEnter);
            this.dataGridViewReactiveActions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewReactiveActions_KeyDown);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.debugGroup);
            this.groupBox8.Controls.Add(this.conditionSetEditor);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(827, 318);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Conditions";
            // 
            // debugGroup
            // 
            this.debugGroup.Controls.Add(this.label2);
            this.debugGroup.Controls.Add(this.testActionRuleResults);
            this.debugGroup.Controls.Add(this.label1);
            this.debugGroup.Controls.Add(this.charactersComboBox);
            this.debugGroup.Controls.Add(this.testConditions);
            this.debugGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.debugGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.debugGroup.Location = new System.Drawing.Point(4, 260);
            this.debugGroup.Name = "debugGroup";
            this.debugGroup.Size = new System.Drawing.Size(819, 54);
            this.debugGroup.TabIndex = 5;
            this.debugGroup.TabStop = false;
            this.debugGroup.Text = "Debug";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(172, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Initiator:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // testActionRuleResults
            // 
            this.testActionRuleResults.AutoEllipsis = true;
            this.testActionRuleResults.BackColor = System.Drawing.SystemColors.ControlLight;
            this.testActionRuleResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.testActionRuleResults.Location = new System.Drawing.Point(441, 12);
            this.testActionRuleResults.Name = "testActionRuleResults";
            this.testActionRuleResults.Size = new System.Drawing.Size(370, 37);
            this.testActionRuleResults.TabIndex = 4;
            this.testActionRuleResults.Text = "Result:";
            this.testActionRuleResults.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(382, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 33);
            this.label1.TabIndex = 3;
            this.label1.Text = "Result:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.edmToolTip.SetToolTip(this.label1, "Substitution Set found by the unification algorithm. The following values were fo" +
        "und for each variable");
            // 
            // charactersComboBox
            // 
            this.charactersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.charactersComboBox.FormattingEnabled = true;
            this.charactersComboBox.Location = new System.Drawing.Point(243, 19);
            this.charactersComboBox.Name = "charactersComboBox";
            this.charactersComboBox.Size = new System.Drawing.Size(114, 24);
            this.charactersComboBox.TabIndex = 2;
            // 
            // testConditions
            // 
            this.testConditions.Location = new System.Drawing.Point(18, 21);
            this.testConditions.Name = "testConditions";
            this.testConditions.Size = new System.Drawing.Size(131, 24);
            this.testConditions.TabIndex = 1;
            this.testConditions.Text = "Test Action Rule";
            this.edmToolTip.SetToolTip(this.testConditions, "This section allows the author to check if the action rule and its conditions are" +
        " being validated by the unification algorithm and what type of substitutions are" +
        " happening behind the cortain ");
            this.testConditions.UseVisualStyleBackColor = true;
            this.testConditions.Click += new System.EventHandler(this.testConditions_Click);
            // 
            // conditionSetEditor
            // 
            this.conditionSetEditor.Location = new System.Drawing.Point(4, 18);
            this.conditionSetEditor.Margin = new System.Windows.Forms.Padding(4);
            this.conditionSetEditor.Name = "conditionSetEditor";
            this.conditionSetEditor.Size = new System.Drawing.Size(819, 243);
            this.conditionSetEditor.TabIndex = 0;
            this.edmToolTip.SetToolTip(this.conditionSetEditor, "Conditions to add to each action");
            this.conditionSetEditor.View = null;
            this.conditionSetEditor.Load += new System.EventHandler(this.conditionSetEditor_Load);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
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
            this.splitContainer1.Size = new System.Drawing.Size(827, 564);
            this.splitContainer1.SplitterDistance = 241;
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
            this.menuStrip1.Size = new System.Drawing.Size(827, 24);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // edmToolTip
            // 
            this.edmToolTip.AutomaticDelay = 1000;
            this.edmToolTip.ShowAlways = true;
            this.edmToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.edmToolTip.ToolTipTitle = "EDM Tooltip";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 588);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Emotional Decision Making";
            this.edmToolTip.SetToolTip(this, "Emotional Decision Making component is resposible for the handling of the actions" +
        "");
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactiveActions)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.debugGroup.ResumeLayout(false);
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
        private System.Windows.Forms.Button buttonRemoveReaction;
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
        public System.Windows.Forms.DataGridView dataGridViewReactiveActions;
        public System.Windows.Forms.ToolTip edmToolTip;
        public System.Windows.Forms.Button buttonAddReaction;
        public System.Windows.Forms.Button emotionaAppraisalButton;
        private System.Windows.Forms.Button testConditions;
        private System.Windows.Forms.ComboBox charactersComboBox;
        private System.Windows.Forms.Label testActionRuleResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox debugGroup;
        private System.Windows.Forms.Label label2;
    }
}


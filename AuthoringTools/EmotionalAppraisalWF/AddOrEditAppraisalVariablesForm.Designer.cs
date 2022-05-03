namespace EmotionalAppraisalWF
{
    partial class AddOrEditAppraisalVariablesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fatimaLinkLabel = new System.Windows.Forms.LinkLabel();
            this.EmotionsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonEditAppraisalRule = new System.Windows.Forms.Button();
            this.buttonAddAppraisalRule = new System.Windows.Forms.Button();
            this.buttonRemoveAppraisalRule = new System.Windows.Forms.Button();
            this.dataGridViewAppraisalVariables = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.appraisalVariableToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emotionToAppraisalVariableConverterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCCModelDiagramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppraisalVariables)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox7.Controls.Add(this.groupBox1);
            this.groupBox7.Controls.Add(this.buttonEditAppraisalRule);
            this.groupBox7.Controls.Add(this.buttonAddAppraisalRule);
            this.groupBox7.Controls.Add(this.buttonRemoveAppraisalRule);
            this.groupBox7.Controls.Add(this.dataGridViewAppraisalVariables);
            this.groupBox7.Controls.Add(this.menuStrip1);
            this.groupBox7.Location = new System.Drawing.Point(2, 2);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(588, 316);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.fatimaLinkLabel);
            this.groupBox1.Controls.Add(this.EmotionsLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 236);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(574, 80);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Emotion Simulator";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Emotions:";
            // 
            // fatimaLinkLabel
            // 
            this.fatimaLinkLabel.AutoSize = true;
            this.fatimaLinkLabel.LinkColor = System.Drawing.Color.SteelBlue;
            this.fatimaLinkLabel.Location = new System.Drawing.Point(514, 62);
            this.fatimaLinkLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fatimaLinkLabel.Name = "fatimaLinkLabel";
            this.fatimaLinkLabel.Size = new System.Drawing.Size(61, 13);
            this.fatimaLinkLabel.TabIndex = 20;
            this.fatimaLinkLabel.TabStop = true;
            this.fatimaLinkLabel.Text = "Learn More";
            this.fatimaLinkLabel.VisitedLinkColor = System.Drawing.Color.SteelBlue;
            this.fatimaLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.fatimaLinkLabel_LinkClicked);
            // 
            // EmotionsLabel
            // 
            this.EmotionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmotionsLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.EmotionsLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmotionsLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.EmotionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmotionsLabel.Location = new System.Drawing.Point(75, 41);
            this.EmotionsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.EmotionsLabel.Name = "EmotionsLabel";
            this.EmotionsLabel.Size = new System.Drawing.Size(394, 26);
            this.EmotionsLabel.TabIndex = 1;
            this.EmotionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(499, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "The appraisal variables above might lead to the following emotions, some depend o" +
    "n their targets:";
            // 
            // buttonEditAppraisalRule
            // 
            this.buttonEditAppraisalRule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditAppraisalRule.Location = new System.Drawing.Point(88, 23);
            this.buttonEditAppraisalRule.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditAppraisalRule.Name = "buttonEditAppraisalRule";
            this.buttonEditAppraisalRule.Size = new System.Drawing.Size(93, 28);
            this.buttonEditAppraisalRule.TabIndex = 9;
            this.buttonEditAppraisalRule.Text = "Edit";
            this.buttonEditAppraisalRule.UseVisualStyleBackColor = true;
            this.buttonEditAppraisalRule.Click += new System.EventHandler(this.buttonEditAppraisalRule_Click);
            // 
            // buttonAddAppraisalRule
            // 
            this.buttonAddAppraisalRule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddAppraisalRule.Location = new System.Drawing.Point(8, 23);
            this.buttonAddAppraisalRule.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddAppraisalRule.Name = "buttonAddAppraisalRule";
            this.buttonAddAppraisalRule.Size = new System.Drawing.Size(72, 28);
            this.buttonAddAppraisalRule.TabIndex = 7;
            this.buttonAddAppraisalRule.Text = "Add";
            this.buttonAddAppraisalRule.UseVisualStyleBackColor = true;
            this.buttonAddAppraisalRule.Click += new System.EventHandler(this.buttonAddAppraisalRule_Click);
            // 
            // buttonRemoveAppraisalRule
            // 
            this.buttonRemoveAppraisalRule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemoveAppraisalRule.Location = new System.Drawing.Point(188, 23);
            this.buttonRemoveAppraisalRule.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveAppraisalRule.Name = "buttonRemoveAppraisalRule";
            this.buttonRemoveAppraisalRule.Size = new System.Drawing.Size(93, 28);
            this.buttonRemoveAppraisalRule.TabIndex = 8;
            this.buttonRemoveAppraisalRule.Text = "Remove";
            this.buttonRemoveAppraisalRule.UseVisualStyleBackColor = true;
            this.buttonRemoveAppraisalRule.Click += new System.EventHandler(this.buttonRemoveAppraisalRule_Click);
            // 
            // dataGridViewAppraisalVariables
            // 
            this.dataGridViewAppraisalVariables.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewAppraisalVariables.AllowUserToAddRows = false;
            this.dataGridViewAppraisalVariables.AllowUserToDeleteRows = false;
            this.dataGridViewAppraisalVariables.AllowUserToOrderColumns = true;
            this.dataGridViewAppraisalVariables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAppraisalVariables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAppraisalVariables.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAppraisalVariables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewAppraisalVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAppraisalVariables.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewAppraisalVariables.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewAppraisalVariables.Location = new System.Drawing.Point(8, 66);
            this.dataGridViewAppraisalVariables.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewAppraisalVariables.Name = "dataGridViewAppraisalVariables";
            this.dataGridViewAppraisalVariables.ReadOnly = true;
            this.dataGridViewAppraisalVariables.RowHeadersVisible = false;
            this.dataGridViewAppraisalVariables.RowHeadersWidth = 51;
            this.dataGridViewAppraisalVariables.RowTemplate.DividerHeight = 1;
            this.dataGridViewAppraisalVariables.RowTemplate.ReadOnly = true;
            this.dataGridViewAppraisalVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAppraisalVariables.Size = new System.Drawing.Size(580, 163);
            this.dataGridViewAppraisalVariables.TabIndex = 2;
            this.dataGridViewAppraisalVariables.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAppraisalVariables_CellContentClick);
            this.dataGridViewAppraisalVariables.SelectionChanged += new System.EventHandler(this.dataGridViewAppraisalVariables_CellContentClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appraisalVariableToolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(379, 23);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(209, 32);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // appraisalVariableToolsToolStripMenuItem
            // 
            this.appraisalVariableToolsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLight;
            this.appraisalVariableToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emotionToAppraisalVariableConverterToolStripMenuItem,
            this.oCCModelDiagramToolStripMenuItem});
            this.appraisalVariableToolsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appraisalVariableToolsToolStripMenuItem.Image = global::EmotionalAppraisalWF.Properties.Resources.icons8_repository_40;
            this.appraisalVariableToolsToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.appraisalVariableToolsToolStripMenuItem.Name = "appraisalVariableToolsToolStripMenuItem";
            this.appraisalVariableToolsToolStripMenuItem.Size = new System.Drawing.Size(191, 28);
            this.appraisalVariableToolsToolStripMenuItem.Text = "Appraisal Variable Helpers";
            // 
            // emotionToAppraisalVariableConverterToolStripMenuItem
            // 
            this.emotionToAppraisalVariableConverterToolStripMenuItem.Image = global::EmotionalAppraisalWF.Properties.Resources.icons8_navigation_pane_40;
            this.emotionToAppraisalVariableConverterToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.emotionToAppraisalVariableConverterToolStripMenuItem.Name = "emotionToAppraisalVariableConverterToolStripMenuItem";
            this.emotionToAppraisalVariableConverterToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.emotionToAppraisalVariableConverterToolStripMenuItem.Text = "Emotion to Appraisal Variable Converter";
            this.emotionToAppraisalVariableConverterToolStripMenuItem.Click += new System.EventHandler(this.emotionToAppraisalVariableConverterToolStripMenuItem_Click);
            // 
            // oCCModelDiagramToolStripMenuItem
            // 
            this.oCCModelDiagramToolStripMenuItem.Image = global::EmotionalAppraisalWF.Properties.Resources.icons8_control_panel_40;
            this.oCCModelDiagramToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.oCCModelDiagramToolStripMenuItem.Name = "oCCModelDiagramToolStripMenuItem";
            this.oCCModelDiagramToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.oCCModelDiagramToolStripMenuItem.Text = "OCC Model Diagram";
            this.oCCModelDiagramToolStripMenuItem.Click += new System.EventHandler(this.oCCModelDiagramToolStripMenuItem_Click);
            // 
            // AddOrEditAppraisalVariablesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 328);
            this.Controls.Add(this.groupBox7);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(754, 495);
            this.MinimumSize = new System.Drawing.Size(612, 367);
            this.Name = "AddOrEditAppraisalVariablesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Appraisal Variables";
            this.groupBox7.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppraisalVariables)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonEditAppraisalRule;
        private System.Windows.Forms.Button buttonAddAppraisalRule;
        private System.Windows.Forms.Button buttonRemoveAppraisalRule;
        private System.Windows.Forms.DataGridView dataGridViewAppraisalVariables;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel fatimaLinkLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label EmotionsLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem appraisalVariableToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emotionToAppraisalVariableConverterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oCCModelDiagramToolStripMenuItem;
    }
}
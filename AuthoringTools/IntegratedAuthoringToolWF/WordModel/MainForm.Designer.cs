namespace WorldModelWF
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
            this.buttonEditAttRule = new System.Windows.Forms.Button();
            this.buttonAddEventTemplate = new System.Windows.Forms.Button();
            this.buttonRemoveAttRule = new System.Windows.Forms.Button();
            this.dataGridViewEventTemplates = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.addEffectDTO = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridViewEffects = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.duplicateAction = new System.Windows.Forms.Button();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEventTemplates)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEffects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox7.Controls.Add(this.duplicateAction);
            this.groupBox7.Controls.Add(this.buttonEditAttRule);
            this.groupBox7.Controls.Add(this.buttonAddEventTemplate);
            this.groupBox7.Controls.Add(this.buttonRemoveAttRule);
            this.groupBox7.Controls.Add(this.dataGridViewEventTemplates);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Font = new System.Drawing.Font("Arial", 9.75F);
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox7.Size = new System.Drawing.Size(923, 250);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Action";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // buttonEditAttRule
            // 
            this.buttonEditAttRule.Location = new System.Drawing.Point(88, 26);
            this.buttonEditAttRule.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonEditAttRule.Name = "buttonEditAttRule";
            this.buttonEditAttRule.Size = new System.Drawing.Size(93, 31);
            this.buttonEditAttRule.TabIndex = 8;
            this.buttonEditAttRule.Text = "Edit";
            this.buttonEditAttRule.UseVisualStyleBackColor = true;
            this.buttonEditAttRule.Click += new System.EventHandler(this.buttonEditAttRule_Click);
            // 
            // buttonAddEventTemplate
            // 
            this.buttonAddEventTemplate.Font = new System.Drawing.Font("Arial", 9.75F);
            this.buttonAddEventTemplate.Location = new System.Drawing.Point(8, 26);
            this.buttonAddEventTemplate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAddEventTemplate.Name = "buttonAddEventTemplate";
            this.buttonAddEventTemplate.Size = new System.Drawing.Size(72, 31);
            this.buttonAddEventTemplate.TabIndex = 7;
            this.buttonAddEventTemplate.Text = "Add";
            this.buttonAddEventTemplate.UseVisualStyleBackColor = true;
            this.buttonAddEventTemplate.Click += new System.EventHandler(this.buttonAddEvent_Click);
            // 
            // buttonRemoveAttRule
            // 
            this.buttonRemoveAttRule.Location = new System.Drawing.Point(290, 26);
            this.buttonRemoveAttRule.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonRemoveAttRule.Name = "buttonRemoveAttRule";
            this.buttonRemoveAttRule.Size = new System.Drawing.Size(93, 31);
            this.buttonRemoveAttRule.TabIndex = 10;
            this.buttonRemoveAttRule.Text = "Remove";
            this.buttonRemoveAttRule.UseVisualStyleBackColor = true;
            this.buttonRemoveAttRule.Click += new System.EventHandler(this.buttonRemoveAttRule_Click);
            // 
            // dataGridViewEventTemplates
            // 
            this.dataGridViewEventTemplates.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewEventTemplates.AllowUserToAddRows = false;
            this.dataGridViewEventTemplates.AllowUserToDeleteRows = false;
            this.dataGridViewEventTemplates.AllowUserToOrderColumns = true;
            this.dataGridViewEventTemplates.AllowUserToResizeRows = false;
            this.dataGridViewEventTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEventTemplates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEventTemplates.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewEventTemplates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEventTemplates.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewEventTemplates.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewEventTemplates.Location = new System.Drawing.Point(8, 65);
            this.dataGridViewEventTemplates.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewEventTemplates.Name = "dataGridViewEventTemplates";
            this.dataGridViewEventTemplates.ReadOnly = true;
            this.dataGridViewEventTemplates.RowHeadersVisible = false;
            this.dataGridViewEventTemplates.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEventTemplates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEventTemplates.Size = new System.Drawing.Size(907, 177);
            this.dataGridViewEventTemplates.TabIndex = 11;
            this.dataGridViewEventTemplates.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEventTemplates_CellContentClick);
            this.dataGridViewEventTemplates.SelectionChanged += new System.EventHandler(this.dataGridViewEventTemplates_SelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.addEffectDTO);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.dataGridViewEffects);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(923, 245);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Effects";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 31);
            this.button1.TabIndex = 9;
            this.button1.Text = "Duplicate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(88, 26);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 31);
            this.button2.TabIndex = 8;
            this.button2.Text = "Edit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // addEffectDTO
            // 
            this.addEffectDTO.Location = new System.Drawing.Point(8, 26);
            this.addEffectDTO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addEffectDTO.Name = "addEffectDTO";
            this.addEffectDTO.Size = new System.Drawing.Size(72, 31);
            this.addEffectDTO.TabIndex = 7;
            this.addEffectDTO.Text = "Add";
            this.addEffectDTO.UseVisualStyleBackColor = true;
            this.addEffectDTO.Click += new System.EventHandler(this.addEffectDTO_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(291, 26);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 31);
            this.button4.TabIndex = 10;
            this.button4.Text = "Remove";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridViewEffects
            // 
            this.dataGridViewEffects.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewEffects.AllowUserToAddRows = false;
            this.dataGridViewEffects.AllowUserToDeleteRows = false;
            this.dataGridViewEffects.AllowUserToOrderColumns = true;
            this.dataGridViewEffects.AllowUserToResizeRows = false;
            this.dataGridViewEffects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEffects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEffects.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewEffects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEffects.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewEffects.Location = new System.Drawing.Point(8, 75);
            this.dataGridViewEffects.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewEffects.Name = "dataGridViewEffects";
            this.dataGridViewEffects.ReadOnly = true;
            this.dataGridViewEffects.RowHeadersVisible = false;
            this.dataGridViewEffects.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEffects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEffects.Size = new System.Drawing.Size(907, 155);
            this.dataGridViewEffects.TabIndex = 11;
            this.dataGridViewEffects.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEffects_CellContentClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(923, 500);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 7;
            // 
            // duplicateAction
            // 
            this.duplicateAction.Location = new System.Drawing.Point(189, 26);
            this.duplicateAction.Margin = new System.Windows.Forms.Padding(4);
            this.duplicateAction.Name = "duplicateAction";
            this.duplicateAction.Size = new System.Drawing.Size(93, 31);
            this.duplicateAction.TabIndex = 12;
            this.duplicateAction.Text = "Duplicate";
            this.duplicateAction.UseVisualStyleBackColor = true;
            this.duplicateAction.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 528);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "World Model Asset";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEventTemplates)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEffects)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonEditAttRule;
        private System.Windows.Forms.Button buttonAddEventTemplate;
        private System.Windows.Forms.Button buttonRemoveAttRule;
        private System.Windows.Forms.DataGridView dataGridViewEventTemplates;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button addEffectDTO;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridViewEffects;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button duplicateAction;
    }
}


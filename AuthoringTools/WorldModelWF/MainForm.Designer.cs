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
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEventTemplates)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEffects)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox7.Controls.Add(this.buttonEditAttRule);
            this.groupBox7.Controls.Add(this.buttonAddEventTemplate);
            this.groupBox7.Controls.Add(this.buttonRemoveAttRule);
            this.groupBox7.Controls.Add(this.dataGridViewEventTemplates);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(13, 32);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(910, 241);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Event Template";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // buttonEditAttRule
            // 
            this.buttonEditAttRule.Location = new System.Drawing.Point(88, 26);
            this.buttonEditAttRule.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditAttRule.Name = "buttonEditAttRule";
            this.buttonEditAttRule.Size = new System.Drawing.Size(93, 32);
            this.buttonEditAttRule.TabIndex = 8;
            this.buttonEditAttRule.Text = "Edit";
            this.buttonEditAttRule.UseVisualStyleBackColor = true;
            this.buttonEditAttRule.Click += new System.EventHandler(this.buttonEditAttRule_Click);
            // 
            // buttonAddEventTemplate
            // 
            this.buttonAddEventTemplate.Location = new System.Drawing.Point(8, 26);
            this.buttonAddEventTemplate.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddEventTemplate.Name = "buttonAddEventTemplate";
            this.buttonAddEventTemplate.Size = new System.Drawing.Size(72, 32);
            this.buttonAddEventTemplate.TabIndex = 7;
            this.buttonAddEventTemplate.Text = "Add";
            this.buttonAddEventTemplate.UseVisualStyleBackColor = true;
            this.buttonAddEventTemplate.Click += new System.EventHandler(this.buttonAddEvent_Click);
            // 
            // buttonRemoveAttRule
            // 
            this.buttonRemoveAttRule.Location = new System.Drawing.Point(189, 24);
            this.buttonRemoveAttRule.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveAttRule.Name = "buttonRemoveAttRule";
            this.buttonRemoveAttRule.Size = new System.Drawing.Size(93, 32);
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
            this.dataGridViewEventTemplates.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewEventTemplates.Location = new System.Drawing.Point(8, 75);
            this.dataGridViewEventTemplates.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewEventTemplates.Name = "dataGridViewEventTemplates";
            this.dataGridViewEventTemplates.ReadOnly = true;
            this.dataGridViewEventTemplates.RowHeadersVisible = false;
            this.dataGridViewEventTemplates.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEventTemplates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEventTemplates.Size = new System.Drawing.Size(894, 158);
            this.dataGridViewEventTemplates.TabIndex = 11;
            this.dataGridViewEventTemplates.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEventTemplates_CellContentClick);
            this.dataGridViewEventTemplates.SelectionChanged += new System.EventHandler(this.dataGridViewEventTemplates_SelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.addEffectDTO);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.dataGridViewEffects);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 281);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(910, 241);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Property Value";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 32);
            this.button1.TabIndex = 9;
            this.button1.Text = "Duplicate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(88, 26);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 32);
            this.button2.TabIndex = 8;
            this.button2.Text = "Edit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // addEffectDTO
            // 
            this.addEffectDTO.Location = new System.Drawing.Point(8, 26);
            this.addEffectDTO.Margin = new System.Windows.Forms.Padding(4);
            this.addEffectDTO.Name = "addEffectDTO";
            this.addEffectDTO.Size = new System.Drawing.Size(72, 32);
            this.addEffectDTO.TabIndex = 7;
            this.addEffectDTO.Text = "Add";
            this.addEffectDTO.UseVisualStyleBackColor = true;
            this.addEffectDTO.Click += new System.EventHandler(this.addEffectDTO_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(291, 26);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 32);
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
            this.dataGridViewEffects.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewEffects.Name = "dataGridViewEffects";
            this.dataGridViewEffects.ReadOnly = true;
            this.dataGridViewEffects.RowHeadersVisible = false;
            this.dataGridViewEffects.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEffects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEffects.Size = new System.Drawing.Size(894, 150);
            this.dataGridViewEffects.TabIndex = 11;
            this.dataGridViewEffects.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEffects_CellContentClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 528);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox7);
            this.Name = "MainForm";
            this.Text = "Wolrd Model Asset";
            this.Controls.SetChildIndex(this.groupBox7, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEventTemplates)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEffects)).EndInit();
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
    }
}


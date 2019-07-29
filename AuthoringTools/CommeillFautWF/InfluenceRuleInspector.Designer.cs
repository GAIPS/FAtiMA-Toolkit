namespace CommeillFautWF
{
    partial class InfluenceRuleInspector
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDuplicateInfluenceRule = new System.Windows.Forms.Button();
            this.buttonEditInfluenceRule = new System.Windows.Forms.Button();
            this.buttonAddInfluenceRule = new System.Windows.Forms.Button();
            this.buttonRemoveInfluenceRule = new System.Windows.Forms.Button();
            this.gridInfluenceRules = new System.Windows.Forms.DataGridView();
            this.Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridInfluenceRules)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Social Exchange Name:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(223, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "DefaultName";
            // 
            // buttonDuplicateInfluenceRule
            // 
            this.buttonDuplicateInfluenceRule.Location = new System.Drawing.Point(202, 64);
            this.buttonDuplicateInfluenceRule.Name = "buttonDuplicateInfluenceRule";
            this.buttonDuplicateInfluenceRule.Size = new System.Drawing.Size(91, 34);
            this.buttonDuplicateInfluenceRule.TabIndex = 15;
            this.buttonDuplicateInfluenceRule.Text = "Duplicate";
            this.buttonDuplicateInfluenceRule.UseVisualStyleBackColor = true;
            this.buttonDuplicateInfluenceRule.Click += new System.EventHandler(this.buttonDuplicateInfluenceRule_Click);
            // 
            // buttonEditInfluenceRule
            // 
            this.buttonEditInfluenceRule.Location = new System.Drawing.Point(102, 64);
            this.buttonEditInfluenceRule.Name = "buttonEditInfluenceRule";
            this.buttonEditInfluenceRule.Size = new System.Drawing.Size(91, 34);
            this.buttonEditInfluenceRule.TabIndex = 14;
            this.buttonEditInfluenceRule.Text = "Edit";
            this.buttonEditInfluenceRule.UseVisualStyleBackColor = true;
            this.buttonEditInfluenceRule.Click += new System.EventHandler(this.buttonEditInfluenceRule_Click);
            // 
            // buttonAddInfluenceRule
            // 
            this.buttonAddInfluenceRule.Location = new System.Drawing.Point(22, 64);
            this.buttonAddInfluenceRule.Name = "buttonAddInfluenceRule";
            this.buttonAddInfluenceRule.Size = new System.Drawing.Size(71, 34);
            this.buttonAddInfluenceRule.TabIndex = 12;
            this.buttonAddInfluenceRule.Text = "Add";
            this.buttonAddInfluenceRule.UseVisualStyleBackColor = true;
            this.buttonAddInfluenceRule.Click += new System.EventHandler(this.buttonAddInfluenceRule_Click);
            // 
            // buttonRemoveInfluenceRule
            // 
            this.buttonRemoveInfluenceRule.Location = new System.Drawing.Point(304, 64);
            this.buttonRemoveInfluenceRule.Name = "buttonRemoveInfluenceRule";
            this.buttonRemoveInfluenceRule.Size = new System.Drawing.Size(91, 34);
            this.buttonRemoveInfluenceRule.TabIndex = 13;
            this.buttonRemoveInfluenceRule.Text = "Remove";
            this.buttonRemoveInfluenceRule.UseVisualStyleBackColor = true;
            this.buttonRemoveInfluenceRule.Click += new System.EventHandler(this.buttonRemoveInfluenceRule_Click);
            // 
            // gridInfluenceRules
            // 
            this.gridInfluenceRules.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.gridInfluenceRules.AllowUserToAddRows = false;
            this.gridInfluenceRules.AllowUserToDeleteRows = false;
            this.gridInfluenceRules.AllowUserToOrderColumns = true;
            this.gridInfluenceRules.AllowUserToResizeRows = false;
            this.gridInfluenceRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridInfluenceRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridInfluenceRules.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridInfluenceRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridInfluenceRules.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gridInfluenceRules.Location = new System.Drawing.Point(22, 113);
            this.gridInfluenceRules.Name = "gridInfluenceRules";
            this.gridInfluenceRules.ReadOnly = true;
            this.gridInfluenceRules.RowHeadersVisible = false;
            this.gridInfluenceRules.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridInfluenceRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridInfluenceRules.Size = new System.Drawing.Size(544, 166);
            this.gridInfluenceRules.TabIndex = 11;
            this.gridInfluenceRules.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridInfluenceRules_CellMouseDoubleClick);
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.Location = new System.Drawing.Point(215, 295);
            this.Close.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(167, 32);
            this.Close.TabIndex = 16;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // InfluenceRuleInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 338);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.buttonDuplicateInfluenceRule);
            this.Controls.Add(this.buttonEditInfluenceRule);
            this.Controls.Add(this.buttonAddInfluenceRule);
            this.Controls.Add(this.buttonRemoveInfluenceRule);
            this.Controls.Add(this.gridInfluenceRules);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "InfluenceRuleInspector";
            this.Text = "InfluenceRuleInspector";
            this.Load += new System.EventHandler(this.InfluenceRuleInspector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridInfluenceRules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonDuplicateInfluenceRule;
        private System.Windows.Forms.Button buttonEditInfluenceRule;
        private System.Windows.Forms.Button buttonAddInfluenceRule;
        private System.Windows.Forms.Button buttonRemoveInfluenceRule;
        private System.Windows.Forms.DataGridView gridInfluenceRules;
        private System.Windows.Forms.Button Close;
    }
}
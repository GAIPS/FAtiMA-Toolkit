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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.actionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.intentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EditSE = new System.Windows.Forms.Button();
            this.RemoveSE = new System.Windows.Forms.Button();
            this.AddSE = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.triggerRulesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.triggerRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EditTriggerRules = new System.Windows.Forms.Button();
            this.RemoveTriggerRules = new System.Windows.Forms.Button();
            this.AddTriggerRule = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.triggerRulesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // socialExchangeBindingSource
            // 
            this.socialExchangeBindingSource.DataSource = typeof(CommeillFaut.SocialExchange);
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Location = new System.Drawing.Point(12, 40);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(859, 455);
            this.TabControl.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.EditSE);
            this.tabPage1.Controls.Add(this.RemoveSE);
            this.tabPage1.Controls.Add(this.AddSE);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(851, 422);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Social Exchange";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.actionNameDataGridViewTextBoxColumn,
            this.intentDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.socialExchangeBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(23, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(822, 341);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // actionNameDataGridViewTextBoxColumn
            // 
            this.actionNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.actionNameDataGridViewTextBoxColumn.DataPropertyName = "ActionName";
            this.actionNameDataGridViewTextBoxColumn.FillWeight = 73.09644F;
            this.actionNameDataGridViewTextBoxColumn.HeaderText = "ActionName";
            this.actionNameDataGridViewTextBoxColumn.Name = "actionNameDataGridViewTextBoxColumn";
            this.actionNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // intentDataGridViewTextBoxColumn
            // 
            this.intentDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.intentDataGridViewTextBoxColumn.DataPropertyName = "Intent";
            this.intentDataGridViewTextBoxColumn.FillWeight = 126.9036F;
            this.intentDataGridViewTextBoxColumn.HeaderText = "Intent";
            this.intentDataGridViewTextBoxColumn.Name = "intentDataGridViewTextBoxColumn";
            // 
            // EditSE
            // 
            this.EditSE.Location = new System.Drawing.Point(144, 17);
            this.EditSE.Name = "EditSE";
            this.EditSE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EditSE.Size = new System.Drawing.Size(101, 39);
            this.EditSE.TabIndex = 14;
            this.EditSE.Text = "Edit";
            this.EditSE.UseVisualStyleBackColor = true;
            this.EditSE.Click += new System.EventHandler(this.EditClick);
            // 
            // RemoveSE
            // 
            this.RemoveSE.Location = new System.Drawing.Point(260, 17);
            this.RemoveSE.Name = "RemoveSE";
            this.RemoveSE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RemoveSE.Size = new System.Drawing.Size(106, 39);
            this.RemoveSE.TabIndex = 13;
            this.RemoveSE.Text = "Remove";
            this.RemoveSE.UseVisualStyleBackColor = true;
            this.RemoveSE.Click += new System.EventHandler(this.RemoveClick);
            // 
            // AddSE
            // 
            this.AddSE.Location = new System.Drawing.Point(23, 17);
            this.AddSE.Name = "AddSE";
            this.AddSE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.AddSE.Size = new System.Drawing.Size(101, 39);
            this.AddSE.TabIndex = 12;
            this.AddSE.Text = "Add";
            this.AddSE.UseVisualStyleBackColor = true;
            this.AddSE.Click += new System.EventHandler(this.AddClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Controls.Add(this.EditTriggerRules);
            this.tabPage2.Controls.Add(this.RemoveTriggerRules);
            this.tabPage2.Controls.Add(this.AddTriggerRule);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1060, 354);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Trigger Rules";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.triggerRulesDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.triggerRulesBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(19, 80);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(1002, 268);
            this.dataGridView2.TabIndex = 15;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // triggerRulesDataGridViewTextBoxColumn
            // 
            this.triggerRulesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.triggerRulesDataGridViewTextBoxColumn.DataPropertyName = "_triggerRules";
            this.triggerRulesDataGridViewTextBoxColumn.HeaderText = "Trigger Rules";
            this.triggerRulesDataGridViewTextBoxColumn.Name = "triggerRulesDataGridViewTextBoxColumn";
            this.triggerRulesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // triggerRulesBindingSource
            // 
            this.triggerRulesBindingSource.DataSource = typeof(CommeillFaut.TriggerRules);
            // 
            // EditTriggerRules
            // 
            this.EditTriggerRules.Location = new System.Drawing.Point(206, 21);
            this.EditTriggerRules.Name = "EditTriggerRules";
            this.EditTriggerRules.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EditTriggerRules.Size = new System.Drawing.Size(101, 32);
            this.EditTriggerRules.TabIndex = 18;
            this.EditTriggerRules.Text = "Edit";
            this.EditTriggerRules.UseVisualStyleBackColor = true;
            this.EditTriggerRules.Click += new System.EventHandler(this.EditTriggerRule_Click);
            // 
            // RemoveTriggerRules
            // 
            this.RemoveTriggerRules.Location = new System.Drawing.Point(322, 21);
            this.RemoveTriggerRules.Name = "RemoveTriggerRules";
            this.RemoveTriggerRules.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RemoveTriggerRules.Size = new System.Drawing.Size(106, 32);
            this.RemoveTriggerRules.TabIndex = 17;
            this.RemoveTriggerRules.Text = "Remove";
            this.RemoveTriggerRules.UseVisualStyleBackColor = true;
            this.RemoveTriggerRules.Click += new System.EventHandler(this.DeleteTriggerRule_Click);
            // 
            // AddTriggerRule
            // 
            this.AddTriggerRule.Location = new System.Drawing.Point(85, 21);
            this.AddTriggerRule.Name = "AddTriggerRule";
            this.AddTriggerRule.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.AddTriggerRule.Size = new System.Drawing.Size(101, 32);
            this.AddTriggerRule.TabIndex = 16;
            this.AddTriggerRule.Text = "Add";
            this.AddTriggerRule.UseVisualStyleBackColor = true;
            this.AddTriggerRule.Click += new System.EventHandler(this.AddTriggerRule_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(871, 507);
            this.Controls.Add(this.TabControl);
            this.Name = "MainForm";
            this.Text = "Comme ill Faut";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.SetChildIndex(this.TabControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.triggerRulesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource socialExchangeBindingSource;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button EditSE;
        private System.Windows.Forms.Button RemoveSE;
        private System.Windows.Forms.Button AddSE;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingSource triggerRulesBindingSource;
        private System.Windows.Forms.Button EditTriggerRules;
        private System.Windows.Forms.Button RemoveTriggerRules;
        private System.Windows.Forms.Button AddTriggerRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerRulesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn intentDataGridViewTextBoxColumn;
    }
}


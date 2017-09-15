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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.triggerRulesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.triggerRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EditTriggerRules = new System.Windows.Forms.Button();
            this.RemoveTriggerRules = new System.Windows.Forms.Button();
            this.AddTriggerRule = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.genericPropertyDataGridControler1 = new GAIPS.AssetEditorTools.GenericPropertyDataGridControler();
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.triggerRulesBindingSource)).BeginInit();
            this.tabPage1.SuspendLayout();
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Controls.Add(this.EditTriggerRules);
            this.tabPage2.Controls.Add(this.RemoveTriggerRules);
            this.tabPage2.Controls.Add(this.AddTriggerRule);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(851, 422);
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
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.genericPropertyDataGridControler1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(851, 422);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Social Exchange";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // genericPropertyDataGridControler1
            // 
            this.genericPropertyDataGridControler1.AllowMuliSelect = false;
            this.genericPropertyDataGridControler1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genericPropertyDataGridControler1.Location = new System.Drawing.Point(-4, 3);
            this.genericPropertyDataGridControler1.Margin = new System.Windows.Forms.Padding(0);
            this.genericPropertyDataGridControler1.Name = "genericPropertyDataGridControler1";
            this.genericPropertyDataGridControler1.Size = new System.Drawing.Size(855, 358);
            this.genericPropertyDataGridControler1.TabIndex = 0;
            this.genericPropertyDataGridControler1.Load += new System.EventHandler(this.genericPropertyDataGridControler1_Load_1);
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
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.triggerRulesBindingSource)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource socialExchangeBindingSource;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingSource triggerRulesBindingSource;
        private System.Windows.Forms.Button EditTriggerRules;
        private System.Windows.Forms.Button RemoveTriggerRules;
        private System.Windows.Forms.Button AddTriggerRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerRulesDataGridViewTextBoxColumn;
        private System.Windows.Forms.TabPage tabPage1;
        private GAIPS.AssetEditorTools.GenericPropertyDataGridControler genericPropertyDataGridControler1;
    }
}


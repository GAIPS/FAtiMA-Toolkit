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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.AddSE = new System.Windows.Forms.Button();
            this.RemoveSE = new System.Windows.Forms.Button();
            this.EditSE = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.intentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Location = new System.Drawing.Point(12, 40);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1068, 387);
            this.TabControl.TabIndex = 15;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1060, 357);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Trigger Rules";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.EditSE);
            this.tabPage1.Controls.Add(this.RemoveSE);
            this.tabPage1.Controls.Add(this.AddSE);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1060, 357);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Social Exchange";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // AddSE
            // 
            this.AddSE.Location = new System.Drawing.Point(23, 17);
            this.AddSE.Name = "AddSE";
            this.AddSE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.AddSE.Size = new System.Drawing.Size(101, 32);
            this.AddSE.TabIndex = 12;
            this.AddSE.Text = "Add";
            this.AddSE.UseVisualStyleBackColor = true;
            this.AddSE.Click += new System.EventHandler(this.AddClick);
            // 
            // RemoveSE
            // 
            this.RemoveSE.Location = new System.Drawing.Point(260, 17);
            this.RemoveSE.Name = "RemoveSE";
            this.RemoveSE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RemoveSE.Size = new System.Drawing.Size(106, 32);
            this.RemoveSE.TabIndex = 13;
            this.RemoveSE.Text = "Remove";
            this.RemoveSE.UseVisualStyleBackColor = true;
            this.RemoveSE.Click += new System.EventHandler(this.RemoveClick);
            // 
            // EditSE
            // 
            this.EditSE.Location = new System.Drawing.Point(144, 17);
            this.EditSE.Name = "EditSE";
            this.EditSE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EditSE.Size = new System.Drawing.Size(101, 32);
            this.EditSE.TabIndex = 14;
            this.EditSE.Text = "Edit";
            this.EditSE.UseVisualStyleBackColor = true;
            this.EditSE.Click += new System.EventHandler(this.EditClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.actionNameDataGridViewTextBoxColumn,
            this.intentDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.socialExchangeBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(-32, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1894, 1171);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // intentDataGridViewTextBoxColumn
            // 
            this.intentDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.intentDataGridViewTextBoxColumn.DataPropertyName = "Intent";
            this.intentDataGridViewTextBoxColumn.HeaderText = "Intent";
            this.intentDataGridViewTextBoxColumn.Name = "intentDataGridViewTextBoxColumn";
            // 
            // actionNameDataGridViewTextBoxColumn
            // 
            this.actionNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.actionNameDataGridViewTextBoxColumn.DataPropertyName = "ActionName";
            this.actionNameDataGridViewTextBoxColumn.HeaderText = "ActionName";
            this.actionNameDataGridViewTextBoxColumn.Name = "actionNameDataGridViewTextBoxColumn";
            this.actionNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1385, 479);
            this.Controls.Add(this.TabControl);
            this.Name = "MainForm";
            this.Text = "Comme ill Faut";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.SetChildIndex(this.TabControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource socialExchangeBindingSource;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn intentDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button EditSE;
        private System.Windows.Forms.Button RemoveSE;
        private System.Windows.Forms.Button AddSE;
        private System.Windows.Forms.TabPage tabPage2;
    }
}


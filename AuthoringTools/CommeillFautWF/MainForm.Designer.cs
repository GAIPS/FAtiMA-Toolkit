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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.genericPropertyDataGridControler1 = new GAIPS.AssetEditorTools.GenericPropertyDataGridControler();
            this.triggerRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.conditionSetEditorControl1 = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.triggerRulesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.TabControl.Location = new System.Drawing.Point(12, 38);
            this.TabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(869, 669);
            this.TabControl.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(861, 637);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Social Exchange";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(3, 365);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(847, 268);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Starting Conditions:";
            // 
            // genericPropertyDataGridControler1
            // 
            this.genericPropertyDataGridControler1.AllowMuliSelect = true;
            this.genericPropertyDataGridControler1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.genericPropertyDataGridControler1.Location = new System.Drawing.Point(0, 0);
            this.genericPropertyDataGridControler1.Margin = new System.Windows.Forms.Padding(0);
            this.genericPropertyDataGridControler1.Name = "genericPropertyDataGridControler1";
            this.genericPropertyDataGridControler1.Size = new System.Drawing.Size(855, 316);
            this.genericPropertyDataGridControler1.TabIndex = 0;
            this.genericPropertyDataGridControler1.Load += new System.EventHandler(this.genericPropertyDataGridControler1_Load_1);
            this.genericPropertyDataGridControler1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.genericPropertyDataGridControler1_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.genericPropertyDataGridControler1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(855, 633);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.conditionSetEditorControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(855, 313);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Starting Conditions:";
            // 
            // conditionSetEditorControl1
            // 
            this.conditionSetEditorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionSetEditorControl1.Location = new System.Drawing.Point(7, 26);
            this.conditionSetEditorControl1.Margin = new System.Windows.Forms.Padding(4);
            this.conditionSetEditorControl1.Name = "conditionSetEditorControl1";
            this.conditionSetEditorControl1.Size = new System.Drawing.Size(840, 281);
            this.conditionSetEditorControl1.TabIndex = 3;
            this.conditionSetEditorControl1.View = null;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(881, 718);
            this.Controls.Add(this.TabControl);
            this.EditorName = "Comme Ill Faut";
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.SetChildIndex(this.TabControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.triggerRulesBindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource socialExchangeBindingSource;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.BindingSource triggerRulesBindingSource;
        private System.Windows.Forms.TabPage tabPage1;
        private GAIPS.AssetEditorTools.GenericPropertyDataGridControler genericPropertyDataGridControler1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private GAIPS.AssetEditorTools.ConditionSetEditorControl conditionSetEditorControl1;
    }
}


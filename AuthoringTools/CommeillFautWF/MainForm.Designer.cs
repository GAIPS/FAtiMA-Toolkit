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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonDuplicateSE = new System.Windows.Forms.Button();
            this.buttonEditSE = new System.Windows.Forms.Button();
            this.buttonAddSE = new System.Windows.Forms.Button();
            this.buttonRemoveSE = new System.Windows.Forms.Button();
            this.gridSocialExchanges = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.conditionSetEditorControl = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.triggerRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSocialExchanges)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.TabControl.Location = new System.Drawing.Point(12, 38);
            this.TabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(786, 669);
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
            this.tabPage1.Size = new System.Drawing.Size(778, 637);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Social Exchange";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(772, 633);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 16;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonDuplicateSE);
            this.groupBox7.Controls.Add(this.buttonEditSE);
            this.groupBox7.Controls.Add(this.buttonAddSE);
            this.groupBox7.Controls.Add(this.buttonRemoveSE);
            this.groupBox7.Controls.Add(this.gridSocialExchanges);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(772, 316);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            // 
            // buttonDuplicateSE
            // 
            this.buttonDuplicateSE.Location = new System.Drawing.Point(189, 23);
            this.buttonDuplicateSE.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDuplicateSE.Name = "buttonDuplicateSE";
            this.buttonDuplicateSE.Size = new System.Drawing.Size(93, 28);
            this.buttonDuplicateSE.TabIndex = 10;
            this.buttonDuplicateSE.Text = "Duplicate";
            this.buttonDuplicateSE.UseVisualStyleBackColor = true;
            this.buttonDuplicateSE.Click += new System.EventHandler(this.buttonDuplicateSE_Click);
            // 
            // buttonEditSE
            // 
            this.buttonEditSE.Location = new System.Drawing.Point(88, 23);
            this.buttonEditSE.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditSE.Name = "buttonEditSE";
            this.buttonEditSE.Size = new System.Drawing.Size(93, 28);
            this.buttonEditSE.TabIndex = 9;
            this.buttonEditSE.Text = "Edit";
            this.buttonEditSE.UseVisualStyleBackColor = true;
            this.buttonEditSE.Click += new System.EventHandler(this.buttonEditSE_Click);
            // 
            // buttonAddSE
            // 
            this.buttonAddSE.Location = new System.Drawing.Point(8, 23);
            this.buttonAddSE.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddSE.Name = "buttonAddSE";
            this.buttonAddSE.Size = new System.Drawing.Size(72, 28);
            this.buttonAddSE.TabIndex = 7;
            this.buttonAddSE.Text = "Add";
            this.buttonAddSE.UseVisualStyleBackColor = true;
            this.buttonAddSE.Click += new System.EventHandler(this.buttonAddSE_Click);
            // 
            // buttonRemoveSE
            // 
            this.buttonRemoveSE.Location = new System.Drawing.Point(291, 23);
            this.buttonRemoveSE.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveSE.Name = "buttonRemoveSE";
            this.buttonRemoveSE.Size = new System.Drawing.Size(93, 28);
            this.buttonRemoveSE.TabIndex = 8;
            this.buttonRemoveSE.Text = "Remove";
            this.buttonRemoveSE.UseVisualStyleBackColor = true;
            this.buttonRemoveSE.Click += new System.EventHandler(this.buttonRemoveSE_Click);
            // 
            // gridSocialExchanges
            // 
            this.gridSocialExchanges.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.gridSocialExchanges.AllowUserToAddRows = false;
            this.gridSocialExchanges.AllowUserToDeleteRows = false;
            this.gridSocialExchanges.AllowUserToOrderColumns = true;
            this.gridSocialExchanges.AllowUserToResizeRows = false;
            this.gridSocialExchanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSocialExchanges.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridSocialExchanges.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridSocialExchanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSocialExchanges.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gridSocialExchanges.Location = new System.Drawing.Point(8, 66);
            this.gridSocialExchanges.Margin = new System.Windows.Forms.Padding(4);
            this.gridSocialExchanges.Name = "gridSocialExchanges";
            this.gridSocialExchanges.ReadOnly = true;
            this.gridSocialExchanges.RowHeadersVisible = false;
            this.gridSocialExchanges.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSocialExchanges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSocialExchanges.Size = new System.Drawing.Size(756, 242);
            this.gridSocialExchanges.TabIndex = 2;
            this.gridSocialExchanges.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridSocialExchanges_CellMouseDoubleClick);
            this.gridSocialExchanges.SelectionChanged += new System.EventHandler(this.gridSocialExchanges_SelectionChanged);
            this.gridSocialExchanges.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridSocialExchanges_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.conditionSetEditorControl);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(772, 313);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conditions:";
            // 
            // conditionSetEditorControl
            // 
            this.conditionSetEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionSetEditorControl.Location = new System.Drawing.Point(7, 26);
            this.conditionSetEditorControl.Margin = new System.Windows.Forms.Padding(4);
            this.conditionSetEditorControl.Name = "conditionSetEditorControl";
            this.conditionSetEditorControl.Size = new System.Drawing.Size(757, 281);
            this.conditionSetEditorControl.TabIndex = 3;
            this.conditionSetEditorControl.View = null;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(3, 365);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(764, 268);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Starting Conditions:";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(798, 718);
            this.Controls.Add(this.TabControl);
            this.EditorName = "Comme Ill Faut";
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "";
            this.Controls.SetChildIndex(this.TabControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.socialExchangeBindingSource)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSocialExchanges)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.triggerRulesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource socialExchangeBindingSource;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.BindingSource triggerRulesBindingSource;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private GAIPS.AssetEditorTools.ConditionSetEditorControl conditionSetEditorControl;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonDuplicateSE;
        private System.Windows.Forms.Button buttonEditSE;
        private System.Windows.Forms.Button buttonAddSE;
        private System.Windows.Forms.Button buttonRemoveSE;
        private System.Windows.Forms.DataGridView gridSocialExchanges;
    }
}


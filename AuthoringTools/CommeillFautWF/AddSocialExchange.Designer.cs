namespace CommeillFautWF
{
    partial class AddSocialExchange
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.Button();
            this.moveName = new System.Windows.Forms.RichTextBox();
            this.influenceRuleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.conditionSetEditorControl1 = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            this.genericPropertyDataGridControler1 = new GAIPS.AssetEditorTools.GenericPropertyDataGridControler();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.IntentTextBox = new System.Windows.Forms.RichTextBox();
            this.ConsequenceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.influenceRuleBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Social Exchange Name:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "uhm";
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(335, 583);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(131, 50);
            this.NameBox.TabIndex = 3;
            this.NameBox.Text = "Add";
            this.NameBox.UseVisualStyleBackColor = true;
            this.NameBox.Click += new System.EventHandler(this.NameBox_Click);
            // 
            // moveName
            // 
            this.moveName.Location = new System.Drawing.Point(186, 16);
            this.moveName.Name = "moveName";
            this.moveName.Size = new System.Drawing.Size(160, 25);
            this.moveName.TabIndex = 4;
            this.moveName.Text = "";
            // 
            // influenceRuleBindingSource
            // 
            this.influenceRuleBindingSource.DataSource = typeof(CommeillFaut.InfluenceRule);
            this.influenceRuleBindingSource.CurrentChanged += new System.EventHandler(this.influenceRuleBindingSource_CurrentChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Location = new System.Drawing.Point(6, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(746, 390);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Affected Variables";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ConsequenceName,
            this.Result,
            this.Variable});
            this.dataGridView1.Location = new System.Drawing.Point(18, 78);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(722, 306);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(138, 39);
            this.button3.Name = "button3";
            this.button3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button3.Size = new System.Drawing.Size(87, 26);
            this.button3.TabIndex = 6;
            this.button3.Text = "Edit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(231, 39);
            this.button2.Name = "button2";
            this.button2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button2.Size = new System.Drawing.Size(92, 26);
            this.button2.TabIndex = 5;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(45, 39);
            this.button6.Name = "button6";
            this.button6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button6.Size = new System.Drawing.Size(87, 26);
            this.button6.TabIndex = 4;
            this.button6.Text = "Add";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(26, 111);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(766, 453);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.conditionSetEditorControl1);
            this.tabPage1.Controls.Add(this.genericPropertyDataGridControler1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(758, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Starting Conditions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // conditionSetEditorControl1
            // 
            this.conditionSetEditorControl1.Location = new System.Drawing.Point(4, 208);
            this.conditionSetEditorControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conditionSetEditorControl1.Name = "conditionSetEditorControl1";
            this.conditionSetEditorControl1.Size = new System.Drawing.Size(664, 194);
            this.conditionSetEditorControl1.TabIndex = 3;
            this.conditionSetEditorControl1.View = null;
            // 
            // genericPropertyDataGridControler1
            // 
            this.genericPropertyDataGridControler1.AllowMuliSelect = false;
            this.genericPropertyDataGridControler1.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.influenceRuleBindingSource, "RuleName", true));
            this.genericPropertyDataGridControler1.Location = new System.Drawing.Point(0, 3);
            this.genericPropertyDataGridControler1.Margin = new System.Windows.Forms.Padding(0);
            this.genericPropertyDataGridControler1.Name = "genericPropertyDataGridControler1";
            this.genericPropertyDataGridControler1.Size = new System.Drawing.Size(672, 201);
            this.genericPropertyDataGridControler1.TabIndex = 4;
            this.genericPropertyDataGridControler1.Load += new System.EventHandler(this.genericPropertyDataGridControler1_Load);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(758, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Consequences";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Description";
            // 
            // IntentTextBox
            // 
            this.IntentTextBox.Location = new System.Drawing.Point(186, 62);
            this.IntentTextBox.Name = "IntentTextBox";
            this.IntentTextBox.Size = new System.Drawing.Size(378, 31);
            this.IntentTextBox.TabIndex = 12;
            this.IntentTextBox.Text = "";
            this.IntentTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // ConsequenceName
            // 
            this.ConsequenceName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ConsequenceName.HeaderText = "Consequence Name";
            this.ConsequenceName.Name = "ConsequenceName";
            // 
            // Result
            // 
            this.Result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Result.FillWeight = 150F;
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            // 
            // Variable
            // 
            this.Variable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Variable.FillWeight = 150F;
            this.Variable.HeaderText = "Variable";
            this.Variable.Name = "Variable";
            // 
            // AddSocialExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 645);
            this.Controls.Add(this.IntentTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.moveName);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label1);
            this.Name = "AddSocialExchange";
            this.Text = "Add Social Exchange";
            this.Load += new System.EventHandler(this.AddSocialExchange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.influenceRuleBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button NameBox;
        private System.Windows.Forms.RichTextBox moveName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.BindingSource influenceRuleBindingSource;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private GAIPS.AssetEditorTools.ConditionSetEditorControl conditionSetEditorControl1;
        private GAIPS.AssetEditorTools.GenericPropertyDataGridControler genericPropertyDataGridControler1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox IntentTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConsequenceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
    }
}
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
            this.label2 = new System.Windows.Forms.Label();
            this.IntentTextBox = new System.Windows.Forms.RichTextBox();
            this.conditionSetEditorControl1 = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.influenceRuleBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.NameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.NameBox.Location = new System.Drawing.Point(334, 349);
            this.NameBox.MinimumSize = new System.Drawing.Size(80, 40);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(118, 44);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Description";
            // 
            // IntentTextBox
            // 
            this.IntentTextBox.Location = new System.Drawing.Point(186, 54);
            this.IntentTextBox.Name = "IntentTextBox";
            this.IntentTextBox.Size = new System.Drawing.Size(361, 28);
            this.IntentTextBox.TabIndex = 12;
            this.IntentTextBox.Text = "";
            this.IntentTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // conditionSetEditorControl1
            // 
            this.conditionSetEditorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionSetEditorControl1.Location = new System.Drawing.Point(7, 26);
            this.conditionSetEditorControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conditionSetEditorControl1.Name = "conditionSetEditorControl1";
            this.conditionSetEditorControl1.Size = new System.Drawing.Size(772, 210);
            this.conditionSetEditorControl1.TabIndex = 3;
            this.conditionSetEditorControl1.View = null;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.conditionSetEditorControl1);
            this.groupBox1.Location = new System.Drawing.Point(26, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(786, 243);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Starting Conditions:";
            // 
            // AddSocialExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 409);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.IntentTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.moveName);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label1);
            this.Name = "AddSocialExchange";
            this.Text = "Add Social Exchange";
            this.Load += new System.EventHandler(this.AddSocialExchange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.influenceRuleBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button NameBox;
        private System.Windows.Forms.RichTextBox moveName;
        private System.Windows.Forms.BindingSource influenceRuleBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox IntentTextBox;
        private GAIPS.AssetEditorTools.ConditionSetEditorControl conditionSetEditorControl1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
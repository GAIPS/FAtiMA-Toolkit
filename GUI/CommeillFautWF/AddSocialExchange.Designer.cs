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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.Button();
            this.moveName = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IntentTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.InstantiationTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
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
            this.NameBox.Location = new System.Drawing.Point(91, 408);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(84, 34);
            this.NameBox.TabIndex = 3;
            this.NameBox.Text = "Add";
            this.NameBox.UseVisualStyleBackColor = true;
            this.NameBox.Click += new System.EventHandler(this.NameBox_Click);
            // 
            // moveName
            // 
            this.moveName.Location = new System.Drawing.Point(102, 34);
            this.moveName.Name = "moveName";
            this.moveName.Size = new System.Drawing.Size(150, 24);
            this.moveName.TabIndex = 4;
            this.moveName.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Intent";
            // 
            // IntentTextBox
            // 
            this.IntentTextBox.Location = new System.Drawing.Point(102, 75);
            this.IntentTextBox.Name = "IntentTextBox";
            this.IntentTextBox.Size = new System.Drawing.Size(150, 44);
            this.IntentTextBox.TabIndex = 6;
            this.IntentTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Instantiation";
            // 
            // InstantiationTextBox
            // 
            this.InstantiationTextBox.Location = new System.Drawing.Point(102, 125);
            this.InstantiationTextBox.Name = "InstantiationTextBox";
            this.InstantiationTextBox.Size = new System.Drawing.Size(150, 31);
            this.InstantiationTextBox.TabIndex = 8;
            this.InstantiationTextBox.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(23, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 192);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Influence Rules";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(4, 86);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(224, 100);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(22, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(182, 36);
            this.button2.TabIndex = 0;
            this.button2.Text = "Add/Edit Influence Rule";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddSocialExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 454);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.InstantiationTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IntentTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.moveName);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label1);
            this.Name = "AddSocialExchange";
            this.Text = "AddSocialExchange";
            this.Load += new System.EventHandler(this.AddSocialExchange_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button NameBox;
        private System.Windows.Forms.RichTextBox moveName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox IntentTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox InstantiationTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
    }
}
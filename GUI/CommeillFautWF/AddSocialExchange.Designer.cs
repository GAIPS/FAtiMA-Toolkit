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
            this.CloseButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.Button();
            this.wfNameFieldBox1 = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.moveName = new System.Windows.Forms.RichTextBox();
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
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(61, 211);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(164, 37);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(179, 28);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(84, 34);
            this.NameBox.TabIndex = 3;
            this.NameBox.Text = "Add";
            this.NameBox.UseVisualStyleBackColor = true;
            this.NameBox.Click += new System.EventHandler(this.button2_Click);
            // 
            // wfNameFieldBox1
            // 
            this.wfNameFieldBox1.AllowProperty = true;
            this.wfNameFieldBox1.AllowUniversal = true;
            this.wfNameFieldBox1.AllowVariable = true;
            this.wfNameFieldBox1.Location = new System.Drawing.Point(0, 0);
            this.wfNameFieldBox1.Name = "wfNameFieldBox1";
            this.wfNameFieldBox1.Size = new System.Drawing.Size(100, 22);
            this.wfNameFieldBox1.TabIndex = 0;
            // 
            // moveName
            // 
            this.moveName.Location = new System.Drawing.Point(75, 37);
            this.moveName.Name = "moveName";
            this.moveName.Size = new System.Drawing.Size(85, 24);
            this.moveName.TabIndex = 4;
            this.moveName.Text = "";
            this.moveName.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // AddSocialExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.moveName);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label1);
            this.Name = "AddSocialExchange";
            this.Text = "AddSocialExchange";
            this.Load += new System.EventHandler(this.AddSocialExchange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox wfNameFieldBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button NameBox;
        private System.Windows.Forms.RichTextBox moveName;
    }
}
namespace SimToolWF
{
    partial class SimTool
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
            this.emotionLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.moodLabel = new System.Windows.Forms.Label();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "EMOTION:";
            // 
            // emotionLabel
            // 
            this.emotionLabel.AutoSize = true;
            this.emotionLabel.Location = new System.Drawing.Point(399, 208);
            this.emotionLabel.Name = "emotionLabel";
            this.emotionLabel.Size = new System.Drawing.Size(44, 13);
            this.emotionLabel.TabIndex = 1;
            this.emotionLabel.Text = "emotion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "MOOD";
            // 
            // moodLabel
            // 
            this.moodLabel.AutoSize = true;
            this.moodLabel.Location = new System.Drawing.Point(399, 239);
            this.moodLabel.Name = "moodLabel";
            this.moodLabel.Size = new System.Drawing.Size(33, 13);
            this.moodLabel.TabIndex = 3;
            this.moodLabel.Text = "mood";
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(155, 48);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(75, 23);
            this.loadFileButton.TabIndex = 4;
            this.loadFileButton.Text = "Load File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // fileTextBox
            // 
            this.fileTextBox.Location = new System.Drawing.Point(12, 50);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(113, 20);
            this.fileTextBox.TabIndex = 5;
            // 
            // SimTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 262);
            this.Controls.Add(this.fileTextBox);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.moodLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.emotionLabel);
            this.Controls.Add(this.label1);
            this.Name = "SimTool";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label emotionLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label moodLabel;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.TextBox fileTextBox;
    }
}


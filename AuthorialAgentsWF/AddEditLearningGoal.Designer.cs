namespace AuthorialAgentsWF
{
    partial class AddEditLearningGoal
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
            this.learningGoalSaveButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // learningGoalSaveButton
            // 
            this.learningGoalSaveButton.Location = new System.Drawing.Point(96, 199);
            this.learningGoalSaveButton.Name = "learningGoalSaveButton";
            this.learningGoalSaveButton.Size = new System.Drawing.Size(75, 23);
            this.learningGoalSaveButton.TabIndex = 0;
            this.learningGoalSaveButton.Text = "Save";
            this.learningGoalSaveButton.UseVisualStyleBackColor = true;
            this.learningGoalSaveButton.Click += new System.EventHandler(this.learningGoalSaveButton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(96, 37);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(92, 121);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(100, 20);
            this.valueTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Value";
            // 
            // AddEditLearningGoal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.valueTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.learningGoalSaveButton);
            this.Name = "AddEditLearningGoal";
            this.Text = "AddEditLearningGoal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button learningGoalSaveButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
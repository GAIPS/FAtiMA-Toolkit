namespace RolePlayCharacterWF
{
    partial class AddOrEditEmotionForm
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
            this.addOrEditButton = new System.Windows.Forms.Button();
            this.comboBoxEmotionType = new System.Windows.Forms.ComboBox();
            this.textBoxCauseId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxIntensity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.targetBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Type:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // addOrEditButton
            // 
            this.addOrEditButton.Location = new System.Drawing.Point(167, 176);
            this.addOrEditButton.Margin = new System.Windows.Forms.Padding(4);
            this.addOrEditButton.Name = "addOrEditButton";
            this.addOrEditButton.Size = new System.Drawing.Size(116, 30);
            this.addOrEditButton.TabIndex = 20;
            this.addOrEditButton.Text = "Add";
            this.addOrEditButton.UseVisualStyleBackColor = true;
            this.addOrEditButton.Click += new System.EventHandler(this.addOrEditButton_Click);
            // 
            // comboBoxEmotionType
            // 
            this.comboBoxEmotionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmotionType.FormattingEnabled = true;
            this.comboBoxEmotionType.Location = new System.Drawing.Point(43, 65);
            this.comboBoxEmotionType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxEmotionType.Name = "comboBoxEmotionType";
            this.comboBoxEmotionType.Size = new System.Drawing.Size(116, 28);
            this.comboBoxEmotionType.TabIndex = 19;
            this.comboBoxEmotionType.SelectedIndexChanged += new System.EventHandler(this.beliefVisibilityComboBox_SelectedIndexChanged_1);
            // 
            // textBoxCauseId
            // 
            this.textBoxCauseId.Location = new System.Drawing.Point(287, 65);
            this.textBoxCauseId.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCauseId.Name = "textBoxCauseId";
            this.textBoxCauseId.Size = new System.Drawing.Size(97, 26);
            this.textBoxCauseId.TabIndex = 17;
            this.textBoxCauseId.TextChanged += new System.EventHandler(this.beliefNameTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Cause Id:";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Intensity:";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // comboBoxIntensity
            // 
            this.comboBoxIntensity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIntensity.FormattingEnabled = true;
            this.comboBoxIntensity.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxIntensity.Location = new System.Drawing.Point(187, 65);
            this.comboBoxIntensity.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxIntensity.Name = "comboBoxIntensity";
            this.comboBoxIntensity.Size = new System.Drawing.Size(64, 28);
            this.comboBoxIntensity.TabIndex = 21;
            this.comboBoxIntensity.SelectedIndexChanged += new System.EventHandler(this.comboBoxIntensity_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 125);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "Target:";
            // 
            // targetBox
            // 
            this.targetBox.Location = new System.Drawing.Point(142, 122);
            this.targetBox.Margin = new System.Windows.Forms.Padding(4);
            this.targetBox.Name = "targetBox";
            this.targetBox.Size = new System.Drawing.Size(160, 26);
            this.targetBox.TabIndex = 23;
            // 
            // AddOrEditEmotionForm
            // 
            this.AcceptButton = this.addOrEditButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(420, 228);
            this.Controls.Add(this.targetBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxIntensity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addOrEditButton);
            this.Controls.Add(this.comboBoxEmotionType);
            this.Controls.Add(this.textBoxCauseId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "AddOrEditEmotionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Emotion";
            this.Load += new System.EventHandler(this.AddOrEditBeliefForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddOrEditEmotionForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addOrEditButton;
        private System.Windows.Forms.ComboBox comboBoxEmotionType;
        private System.Windows.Forms.TextBox textBoxCauseId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxIntensity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox targetBox;
    }
}
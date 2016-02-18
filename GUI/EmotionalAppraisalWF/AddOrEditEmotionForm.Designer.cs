namespace EmotionalAppraisalWF
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.addOrEditButton = new System.Windows.Forms.Button();
            this.comboBoxEmotionType = new System.Windows.Forms.ComboBox();
            this.textBoxCauseId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addBeliefErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.comboBoxIntensity = new System.Windows.Forms.ComboBox();
            this.emotionalAppraisalAssetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Type:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // addOrEditButton
            // 
            this.addOrEditButton.Location = new System.Drawing.Point(131, 94);
            this.addOrEditButton.Name = "addOrEditButton";
            this.addOrEditButton.Size = new System.Drawing.Size(75, 23);
            this.addOrEditButton.TabIndex = 20;
            this.addOrEditButton.Text = "Add";
            this.addOrEditButton.UseVisualStyleBackColor = true;
            this.addOrEditButton.Click += new System.EventHandler(this.addOrEditButton_Click);
            // 
            // comboBoxEmotionType
            // 
            this.comboBoxEmotionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmotionType.FormattingEnabled = true;
            this.comboBoxEmotionType.Location = new System.Drawing.Point(40, 53);
            this.comboBoxEmotionType.Name = "comboBoxEmotionType";
            this.comboBoxEmotionType.Size = new System.Drawing.Size(88, 21);
            this.comboBoxEmotionType.TabIndex = 19;
            this.comboBoxEmotionType.SelectedIndexChanged += new System.EventHandler(this.beliefVisibilityComboBox_SelectedIndexChanged_1);
            // 
            // textBoxCauseId
            // 
            this.textBoxCauseId.Location = new System.Drawing.Point(216, 53);
            this.textBoxCauseId.Name = "textBoxCauseId";
            this.textBoxCauseId.Size = new System.Drawing.Size(74, 20);
            this.textBoxCauseId.TabIndex = 17;
            this.textBoxCauseId.TextChanged += new System.EventHandler(this.beliefNameTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Cause Id:";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Intensity:";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // addBeliefErrorProvider
            // 
            this.addBeliefErrorProvider.ContainerControl = this;
            // 
            // comboBoxIntensity
            // 
            this.comboBoxIntensity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIntensity.FormattingEnabled = true;
            this.comboBoxIntensity.Items.AddRange(new object[] {
            "-10",
            "-9",
            "-8",
            "-7",
            "-6",
            "-5",
            "-4",
            "-3",
            "-2",
            "-1",
            "0",
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
            this.comboBoxIntensity.Location = new System.Drawing.Point(146, 53);
            this.comboBoxIntensity.Name = "comboBoxIntensity";
            this.comboBoxIntensity.Size = new System.Drawing.Size(49, 21);
            this.comboBoxIntensity.TabIndex = 21;
            this.comboBoxIntensity.SelectedIndexChanged += new System.EventHandler(this.comboBoxIntensity_SelectedIndexChanged);
            // 
            // emotionalAppraisalAssetBindingSource
            // 
            this.emotionalAppraisalAssetBindingSource.DataSource = typeof(EmotionalAppraisal.EmotionalAppraisalAsset);
            // 
            // AddOrEditEmotionForm
            // 
            this.AcceptButton = this.addOrEditButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(336, 139);
            this.Controls.Add(this.comboBoxIntensity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addOrEditButton);
            this.Controls.Add(this.comboBoxEmotionType);
            this.Controls.Add(this.textBoxCauseId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddOrEditEmotionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Emotion";
            this.Load += new System.EventHandler(this.AddOrEditBeliefForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource emotionalAppraisalAssetBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addOrEditButton;
        private System.Windows.Forms.ComboBox comboBoxEmotionType;
        private System.Windows.Forms.TextBox textBoxCauseId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider addBeliefErrorProvider;
        private System.Windows.Forms.ComboBox comboBoxIntensity;
    }
}
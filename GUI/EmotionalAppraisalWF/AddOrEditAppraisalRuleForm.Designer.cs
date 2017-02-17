namespace EmotionalAppraisalWF
{
    partial class AddOrEditAppraisalRuleForm
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
            this.addOrEditButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addBeliefErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.comboBoxPraiseworthiness = new System.Windows.Forms.ComboBox();
            this.comboBoxDesirability = new System.Windows.Forms.ComboBox();
            this.emotionalAppraisalAssetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxEventType = new System.Windows.Forms.ComboBox();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.textBoxObject = new System.Windows.Forms.TextBox();
            this.labelObject = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // addOrEditButton
            // 
            this.addOrEditButton.Location = new System.Drawing.Point(123, 337);
            this.addOrEditButton.Name = "addOrEditButton";
            this.addOrEditButton.Size = new System.Drawing.Size(75, 23);
            this.addOrEditButton.TabIndex = 20;
            this.addOrEditButton.Text = "Add";
            this.addOrEditButton.UseVisualStyleBackColor = true;
            this.addOrEditButton.Click += new System.EventHandler(this.addOrEditButton_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 295);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Praiseworthiness:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Desirability:";
            // 
            // addBeliefErrorProvider
            // 
            this.addBeliefErrorProvider.ContainerControl = this;
            // 
            // comboBoxPraiseworthiness
            // 
            this.comboBoxPraiseworthiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPraiseworthiness.FormattingEnabled = true;
            this.comboBoxPraiseworthiness.Items.AddRange(new object[] {
            "10",
            "9",
            "8",
            "7",
            "6",
            "5",
            "4",
            "3",
            "2",
            "1",
            "0",
            "-1",
            "-2",
            "-3",
            "-4",
            "-5",
            "-6",
            "-7",
            "-8",
            "-9",
            "-10"});
            this.comboBoxPraiseworthiness.Location = new System.Drawing.Point(244, 291);
            this.comboBoxPraiseworthiness.Name = "comboBoxPraiseworthiness";
            this.comboBoxPraiseworthiness.Size = new System.Drawing.Size(44, 21);
            this.comboBoxPraiseworthiness.TabIndex = 19;
            // 
            // comboBoxDesirability
            // 
            this.comboBoxDesirability.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDesirability.FormattingEnabled = true;
            this.comboBoxDesirability.Items.AddRange(new object[] {
            "10",
            "9",
            "8",
            "7",
            "6",
            "5",
            "4",
            "3",
            "2",
            "1",
            "0",
            "-1",
            "-2",
            "-3",
            "-4",
            "-5",
            "-6",
            "-7",
            "-8",
            "-9",
            "-10"});
            this.comboBoxDesirability.Location = new System.Drawing.Point(94, 291);
            this.comboBoxDesirability.Name = "comboBoxDesirability";
            this.comboBoxDesirability.Size = new System.Drawing.Size(46, 21);
            this.comboBoxDesirability.TabIndex = 18;
            // 
            // emotionalAppraisalAssetBindingSource
            // 
            this.emotionalAppraisalAssetBindingSource.DataSource = typeof(EmotionalAppraisal.EmotionalAppraisalAsset);
            // 
            // comboBoxEventType
            // 
            this.comboBoxEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEventType.FormattingEnabled = true;
            this.comboBoxEventType.Location = new System.Drawing.Point(32, 49);
            this.comboBoxEventType.Name = "comboBoxEventType";
            this.comboBoxEventType.Size = new System.Drawing.Size(257, 21);
            this.comboBoxEventType.TabIndex = 32;
            this.comboBoxEventType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEventType_SelectedIndexChanged);
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.Location = new System.Drawing.Point(32, 250);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.Size = new System.Drawing.Size(257, 20);
            this.textBoxTarget.TabIndex = 39;
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(32, 224);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(101, 13);
            this.labelTarget.TabIndex = 38;
            this.labelTarget.Text = "Target | New Value:";
            // 
            // textBoxObject
            // 
            this.textBoxObject.Location = new System.Drawing.Point(32, 183);
            this.textBoxObject.Name = "textBoxObject";
            this.textBoxObject.Size = new System.Drawing.Size(257, 20);
            this.textBoxObject.TabIndex = 37;
            // 
            // labelObject
            // 
            this.labelObject.AutoSize = true;
            this.labelObject.Location = new System.Drawing.Point(32, 157);
            this.labelObject.Name = "labelObject";
            this.labelObject.Size = new System.Drawing.Size(87, 13);
            this.labelObject.TabIndex = 36;
            this.labelObject.Text = "Action | Property:";
            this.labelObject.Click += new System.EventHandler(this.labelObject_Click);
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Location = new System.Drawing.Point(32, 119);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(257, 20);
            this.textBoxSubject.TabIndex = 35;
            this.textBoxSubject.TextChanged += new System.EventHandler(this.textBoxSubject_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Subject:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Type:";
            // 
            // AddOrEditAppraisalRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(328, 382);
            this.Controls.Add(this.comboBoxEventType);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.textBoxObject);
            this.Controls.Add(this.labelObject);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxDesirability);
            this.Controls.Add(this.addOrEditButton);
            this.Controls.Add(this.comboBoxPraiseworthiness);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddOrEditAppraisalRuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Appraisal Rule";
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource emotionalAppraisalAssetBindingSource;
        private System.Windows.Forms.Button addOrEditButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider addBeliefErrorProvider;
        private System.Windows.Forms.ComboBox comboBoxDesirability;
        private System.Windows.Forms.ComboBox comboBoxPraiseworthiness;
        private System.Windows.Forms.ComboBox comboBoxEventType;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.TextBox textBoxObject;
        private System.Windows.Forms.Label labelObject;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}
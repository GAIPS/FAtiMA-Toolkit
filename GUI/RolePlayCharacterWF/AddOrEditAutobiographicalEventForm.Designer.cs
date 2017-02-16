namespace RolePlayCharacterWF
{
    partial class AddOrEditAutobiographicalEventForm
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
            this.addBeliefErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.textBoxObject = new System.Windows.Forms.TextBox();
            this.labelObject = new System.Windows.Forms.Label();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.comboBoxEventType = new System.Windows.Forms.ComboBox();
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
            this.addOrEditButton.Location = new System.Drawing.Point(111, 376);
            this.addOrEditButton.Name = "addOrEditButton";
            this.addOrEditButton.Size = new System.Drawing.Size(75, 23);
            this.addOrEditButton.TabIndex = 40;
            this.addOrEditButton.Text = "Add";
            this.addOrEditButton.UseVisualStyleBackColor = true;
            this.addOrEditButton.Click += new System.EventHandler(this.addOrEditButton_Click_1);
            // 
            // addBeliefErrorProvider
            // 
            this.addBeliefErrorProvider.ContainerControl = this;
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(40, 324);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(227, 20);
            this.textBoxTime.TabIndex = 30;
            this.textBoxTime.TextChanged += new System.EventHandler(this.textBoxTime_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Timestamp:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Subject:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Location = new System.Drawing.Point(40, 128);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(227, 20);
            this.textBoxSubject.TabIndex = 25;
            this.textBoxSubject.TextChanged += new System.EventHandler(this.textBoxSubject_TextChanged);
            // 
            // textBoxObject
            // 
            this.textBoxObject.Location = new System.Drawing.Point(40, 192);
            this.textBoxObject.Name = "textBoxObject";
            this.textBoxObject.Size = new System.Drawing.Size(227, 20);
            this.textBoxObject.TabIndex = 27;
            this.textBoxObject.TextChanged += new System.EventHandler(this.textBoxObject_TextChanged);
            // 
            // labelObject
            // 
            this.labelObject.AutoSize = true;
            this.labelObject.Location = new System.Drawing.Point(40, 166);
            this.labelObject.Name = "labelObject";
            this.labelObject.Size = new System.Drawing.Size(40, 13);
            this.labelObject.TabIndex = 26;
            this.labelObject.Text = "Action:";
            this.labelObject.Click += new System.EventHandler(this.labelObject_Click);
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.Location = new System.Drawing.Point(40, 258);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.Size = new System.Drawing.Size(227, 20);
            this.textBoxTarget.TabIndex = 29;
            this.textBoxTarget.TextChanged += new System.EventHandler(this.textBoxTarget_TextChanged);
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(40, 233);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(41, 13);
            this.labelTarget.TabIndex = 28;
            this.labelTarget.Text = "Target:";
            this.labelTarget.Click += new System.EventHandler(this.labelTarget_Click);
            // 
            // comboBoxEventType
            // 
            this.comboBoxEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEventType.FormattingEnabled = true;
            this.comboBoxEventType.Location = new System.Drawing.Point(40, 58);
            this.comboBoxEventType.Name = "comboBoxEventType";
            this.comboBoxEventType.Size = new System.Drawing.Size(224, 21);
            this.comboBoxEventType.TabIndex = 15;
            this.comboBoxEventType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEventType_SelectedIndexChanged);
            this.comboBoxEventType.SelectedValueChanged += new System.EventHandler(this.comboBoxEventType_SelectedValueChanged);
            // 
            // emotionalAppraisalAssetBindingSource
            // 
            this.emotionalAppraisalAssetBindingSource.DataSource = typeof(EmotionalAppraisal.EmotionalAppraisalAsset);
            // 
            // AddOrEditAutobiographicalEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(296, 431);
            this.Controls.Add(this.comboBoxEventType);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.textBoxObject);
            this.Controls.Add(this.labelObject);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addOrEditButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddOrEditAutobiographicalEventForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Event Record";
            this.Load += new System.EventHandler(this.AddOrEditAutobiographicalEventForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource emotionalAppraisalAssetBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addOrEditButton;
        private System.Windows.Forms.ErrorProvider addBeliefErrorProvider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.ComboBox comboBoxEventType;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.TextBox textBoxObject;
        private System.Windows.Forms.Label labelObject;
    }
}
namespace WorldModelWF
{
    partial class AddOrEditActionTemplateForm
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
            this.addBeliefErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.emotionalAppraisalAssetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelTarget = new System.Windows.Forms.Label();
            this.labelObject = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSubject = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxObject = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxTarget = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.label1 = new System.Windows.Forms.Label();
            this.priorityFieldBox = new GAIPS.AssetEditorTools.TypedTextBoxes.Int32FieldBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // addOrEditButton
            // 
            this.addOrEditButton.Location = new System.Drawing.Point(91, 268);
            this.addOrEditButton.Margin = new System.Windows.Forms.Padding(4);
            this.addOrEditButton.Name = "addOrEditButton";
            this.addOrEditButton.Size = new System.Drawing.Size(186, 28);
            this.addOrEditButton.TabIndex = 50;
            this.addOrEditButton.Text = "Add World Model Rule";
            this.addOrEditButton.UseVisualStyleBackColor = true;
            this.addOrEditButton.Click += new System.EventHandler(this.addOrEditButton_Click_1);
            // 
            // addBeliefErrorProvider
            // 
            this.addBeliefErrorProvider.ContainerControl = this;
            // 
            // emotionalAppraisalAssetBindingSource
            // 
            this.emotionalAppraisalAssetBindingSource.DataSource = typeof(EmotionalAppraisal.EmotionalAppraisalAsset);
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(18, 154);
            this.labelTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(91, 16);
            this.labelTarget.TabIndex = 38;
            this.labelTarget.Text = "Action Target:";
            // 
            // labelObject
            // 
            this.labelObject.AutoSize = true;
            this.labelObject.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelObject.Location = new System.Drawing.Point(19, 27);
            this.labelObject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObject.Name = "labelObject";
            this.labelObject.Size = new System.Drawing.Size(109, 16);
            this.labelObject.TabIndex = 36;
            this.labelObject.Text = "Action Template:";
            this.labelObject.Click += new System.EventHandler(this.labelObject_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 93);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "Action Subject:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.AllowComposedName = true;
            this.textBoxSubject.AllowLiteral = true;
            this.textBoxSubject.AllowNil = true;
            this.textBoxSubject.AllowUniversal = true;
            this.textBoxSubject.AllowUniversalLiteral = true;
            this.textBoxSubject.AllowVariable = true;
            this.textBoxSubject.Location = new System.Drawing.Point(22, 112);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.OnlyIntOrVariable = false;
            this.textBoxSubject.Size = new System.Drawing.Size(307, 22);
            this.textBoxSubject.TabIndex = 47;
            // 
            // textBoxObject
            // 
            this.textBoxObject.AllowComposedName = true;
            this.textBoxObject.AllowLiteral = true;
            this.textBoxObject.AllowNil = true;
            this.textBoxObject.AllowUniversal = true;
            this.textBoxObject.AllowUniversalLiteral = true;
            this.textBoxObject.AllowVariable = true;
            this.textBoxObject.Location = new System.Drawing.Point(21, 46);
            this.textBoxObject.Name = "textBoxObject";
            this.textBoxObject.OnlyIntOrVariable = false;
            this.textBoxObject.Size = new System.Drawing.Size(308, 22);
            this.textBoxObject.TabIndex = 45;
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.AllowComposedName = true;
            this.textBoxTarget.AllowLiteral = true;
            this.textBoxTarget.AllowNil = true;
            this.textBoxTarget.AllowUniversal = true;
            this.textBoxTarget.AllowUniversalLiteral = true;
            this.textBoxTarget.AllowVariable = true;
            this.textBoxTarget.Location = new System.Drawing.Point(22, 173);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.OnlyIntOrVariable = false;
            this.textBoxTarget.Size = new System.Drawing.Size(307, 22);
            this.textBoxTarget.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 222);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 51;
            this.label1.Text = "Rule Priority:";
            // 
            // priorityFieldBox
            // 
            this.priorityFieldBox.Location = new System.Drawing.Point(114, 219);
            this.priorityFieldBox.Name = "priorityFieldBox";
            this.priorityFieldBox.Size = new System.Drawing.Size(110, 22);
            this.priorityFieldBox.TabIndex = 49;
            this.priorityFieldBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.priorityFieldBox.TextChanged += new System.EventHandler(this.priorityFieldBox_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label7.Location = new System.Drawing.Point(142, 27);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(187, 16);
            this.label7.TabIndex = 82;
            this.label7.Text = "Eat, GoTo, Speak( * , [ns], * , *)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(221, 93);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 83;
            this.label2.Text = "John, [subject], * ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(230, 154);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 84;
            this.label3.Text = "John, [target], * ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddOrEditActionTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(366, 309);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.priorityFieldBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.textBoxObject);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.labelObject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addOrEditButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "AddOrEditActionTemplateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add World Model Rule";
            this.Load += new System.EventHandler(this.AddOrEditAppraisalRuleForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddOrEditAppraisalRuleForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource emotionalAppraisalAssetBindingSource;
        private System.Windows.Forms.Button addOrEditButton;
        private System.Windows.Forms.ErrorProvider addBeliefErrorProvider;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.Label labelObject;
        private System.Windows.Forms.Label label4;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxSubject;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxObject;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxTarget;
        private GAIPS.AssetEditorTools.TypedTextBoxes.Int32FieldBox priorityFieldBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
    }
}
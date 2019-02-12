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
            this.addBeliefErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.emotionalAppraisalAssetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxEventType = new System.Windows.Forms.ComboBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.labelObject = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSubject = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxObject = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxTarget = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // addOrEditButton
            // 
            this.addOrEditButton.Location = new System.Drawing.Point(161, 369);
            this.addOrEditButton.Margin = new System.Windows.Forms.Padding(4);
            this.addOrEditButton.Name = "addOrEditButton";
            this.addOrEditButton.Size = new System.Drawing.Size(100, 28);
            this.addOrEditButton.TabIndex = 20;
            this.addOrEditButton.Text = "Add";
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
            // comboBoxEventType
            // 
            this.comboBoxEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEventType.FormattingEnabled = true;
            this.comboBoxEventType.Location = new System.Drawing.Point(43, 60);
            this.comboBoxEventType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxEventType.Name = "comboBoxEventType";
            this.comboBoxEventType.Size = new System.Drawing.Size(343, 28);
            this.comboBoxEventType.TabIndex = 32;
            this.comboBoxEventType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEventType_SelectedIndexChanged);
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(43, 276);
            this.labelTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(156, 20);
            this.labelTarget.TabIndex = 38;
            this.labelTarget.Text = "Target | New Value:";
            // 
            // labelObject
            // 
            this.labelObject.AutoSize = true;
            this.labelObject.Location = new System.Drawing.Point(43, 194);
            this.labelObject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObject.Name = "labelObject";
            this.labelObject.Size = new System.Drawing.Size(138, 20);
            this.labelObject.TabIndex = 36;
            this.labelObject.Text = "Action | Property:";
            this.labelObject.Click += new System.EventHandler(this.labelObject_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 114);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.TabIndex = 34;
            this.label4.Text = "Subject:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 20);
            this.label6.TabIndex = 31;
            this.label6.Text = "Type:";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.AllowComposedName = true;
            this.textBoxSubject.AllowLiteral = true;
            this.textBoxSubject.AllowNil = true;
            this.textBoxSubject.AllowUniversal = true;
            this.textBoxSubject.AllowVariable = true;
            this.textBoxSubject.Location = new System.Drawing.Point(43, 145);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.OnlyIntOrVariable = false;
            this.textBoxSubject.Size = new System.Drawing.Size(343, 26);
            this.textBoxSubject.TabIndex = 44;
            // 
            // textBoxObject
            // 
            this.textBoxObject.AllowComposedName = true;
            this.textBoxObject.AllowLiteral = true;
            this.textBoxObject.AllowNil = true;
            this.textBoxObject.AllowUniversal = true;
            this.textBoxObject.AllowVariable = true;
            this.textBoxObject.Location = new System.Drawing.Point(43, 230);
            this.textBoxObject.Name = "textBoxObject";
            this.textBoxObject.OnlyIntOrVariable = false;
            this.textBoxObject.Size = new System.Drawing.Size(344, 26);
            this.textBoxObject.TabIndex = 45;
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.AllowComposedName = true;
            this.textBoxTarget.AllowLiteral = true;
            this.textBoxTarget.AllowNil = true;
            this.textBoxTarget.AllowUniversal = true;
            this.textBoxTarget.AllowVariable = true;
            this.textBoxTarget.Location = new System.Drawing.Point(43, 312);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.OnlyIntOrVariable = false;
            this.textBoxTarget.Size = new System.Drawing.Size(344, 26);
            this.textBoxTarget.TabIndex = 46;
            // 
            // AddOrEditAppraisalRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(427, 429);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.textBoxObject);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.comboBoxEventType);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.labelObject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.addOrEditButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "AddOrEditAppraisalRuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Appraisal Rule";
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
        private System.Windows.Forms.ComboBox comboBoxEventType;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.Label labelObject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxSubject;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxObject;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxTarget;
    }
}
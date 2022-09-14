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
            this.labelSubject = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSubject = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxObject = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxTarget = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTargetHelper = new System.Windows.Forms.Label();
            this.labelObjectHelper = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // addOrEditButton
            // 
            this.addOrEditButton.Location = new System.Drawing.Point(110, 356);
            this.addOrEditButton.Margin = new System.Windows.Forms.Padding(4);
            this.addOrEditButton.Name = "addOrEditButton";
            this.addOrEditButton.Size = new System.Drawing.Size(184, 28);
            this.addOrEditButton.TabIndex = 20;
            this.addOrEditButton.Text = "Add Appraisal Rule";
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
            this.comboBoxEventType.Size = new System.Drawing.Size(343, 24);
            this.comboBoxEventType.TabIndex = 32;
            this.comboBoxEventType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEventType_SelectedIndexChanged);
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(43, 267);
            this.labelTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(125, 16);
            this.labelTarget.TabIndex = 38;
            this.labelTarget.Text = "Target | New Value:";
            // 
            // labelObject
            // 
            this.labelObject.AutoSize = true;
            this.labelObject.Location = new System.Drawing.Point(42, 183);
            this.labelObject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObject.Name = "labelObject";
            this.labelObject.Size = new System.Drawing.Size(108, 16);
            this.labelObject.TabIndex = 36;
            this.labelObject.Text = "Action | Property:";
            this.labelObject.Click += new System.EventHandler(this.labelObject_Click);
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(43, 107);
            this.labelSubject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(56, 16);
            this.labelSubject.TabIndex = 34;
            this.labelSubject.Text = "Subject:";
            this.labelSubject.Click += new System.EventHandler(this.label4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 16);
            this.label6.TabIndex = 31;
            this.label6.Text = "Action Type:";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.AllowComposedName = true;
            this.textBoxSubject.AllowLiteral = true;
            this.textBoxSubject.AllowNil = true;
            this.textBoxSubject.AllowUniversal = true;
            this.textBoxSubject.AllowUniversalLiteral = true;
            this.textBoxSubject.AllowVariable = true;
            this.textBoxSubject.Location = new System.Drawing.Point(43, 138);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.OnlyIntOrVariable = false;
            this.textBoxSubject.Size = new System.Drawing.Size(343, 22);
            this.textBoxSubject.TabIndex = 44;
            // 
            // textBoxObject
            // 
            this.textBoxObject.AllowComposedName = true;
            this.textBoxObject.AllowLiteral = true;
            this.textBoxObject.AllowNil = true;
            this.textBoxObject.AllowUniversal = true;
            this.textBoxObject.AllowUniversalLiteral = true;
            this.textBoxObject.AllowVariable = true;
            this.textBoxObject.Location = new System.Drawing.Point(42, 219);
            this.textBoxObject.Name = "textBoxObject";
            this.textBoxObject.OnlyIntOrVariable = false;
            this.textBoxObject.Size = new System.Drawing.Size(344, 22);
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
            this.textBoxTarget.Location = new System.Drawing.Point(43, 303);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.OnlyIntOrVariable = false;
            this.textBoxTarget.Size = new System.Drawing.Size(344, 22);
            this.textBoxTarget.TabIndex = 46;
            this.textBoxTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(278, 119);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 84;
            this.label2.Text = "John, [subject], * ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTargetHelper
            // 
            this.labelTargetHelper.AutoSize = true;
            this.labelTargetHelper.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelTargetHelper.Location = new System.Drawing.Point(324, 284);
            this.labelTargetHelper.Name = "labelTargetHelper";
            this.labelTargetHelper.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelTargetHelper.Size = new System.Drawing.Size(62, 16);
            this.labelTargetHelper.TabIndex = 85;
            this.labelTargetHelper.Text = "5, [value]";
            this.labelTargetHelper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTargetHelper.Click += new System.EventHandler(this.labelTargetHelper_Click);
            // 
            // labelObjectHelper
            // 
            this.labelObjectHelper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelObjectHelper.AutoSize = true;
            this.labelObjectHelper.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelObjectHelper.Location = new System.Drawing.Point(199, 200);
            this.labelObjectHelper.Name = "labelObjectHelper";
            this.labelObjectHelper.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelObjectHelper.Size = new System.Drawing.Size(187, 16);
            this.labelObjectHelper.TabIndex = 86;
            this.labelObjectHelper.Text = "Eat, GoTo, Speak( * , [ns], * , *)";
            this.labelObjectHelper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddOrEditAppraisalRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(405, 397);
            this.Controls.Add(this.labelObjectHelper);
            this.Controls.Add(this.labelTargetHelper);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.textBoxObject);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.comboBoxEventType);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.labelObject);
            this.Controls.Add(this.labelSubject);
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
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.Label labelObject;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox comboBoxEventType;
        public GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxSubject;
        public GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxTarget;
        public GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxObject;
        private System.Windows.Forms.Label labelTargetHelper;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelObjectHelper;
    }
}
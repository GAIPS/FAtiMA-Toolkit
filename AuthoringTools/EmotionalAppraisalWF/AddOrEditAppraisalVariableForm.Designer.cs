namespace EmotionalAppraisalWF
{
    partial class AddOrEditAppraisalVariableForm
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
            this.appraisalVariableName = new System.Windows.Forms.ComboBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.appraisalVariableValueTextBox = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.appraisalVariableTarget = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // addOrEditButton
            // 
            this.addOrEditButton.Location = new System.Drawing.Point(151, 253);
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
            // appraisalVariableName
            // 
            this.appraisalVariableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appraisalVariableName.FormattingEnabled = true;
            this.appraisalVariableName.Location = new System.Drawing.Point(43, 60);
            this.appraisalVariableName.Margin = new System.Windows.Forms.Padding(4);
            this.appraisalVariableName.Name = "appraisalVariableName";
            this.appraisalVariableName.Size = new System.Drawing.Size(343, 24);
            this.appraisalVariableName.TabIndex = 32;
            this.appraisalVariableName.SelectionChangeCommitted += new System.EventHandler(this.comboBoxEventType_SelectedIndexChanged);
            this.appraisalVariableName.DropDownClosed += new System.EventHandler(this.comboBoxEventType_SelectedIndexChanged);
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(39, 178);
            this.labelTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(51, 16);
            this.labelTarget.TabIndex = 38;
            this.labelTarget.Text = "Target:";
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(39, 107);
            this.valueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(46, 16);
            this.valueLabel.TabIndex = 34;
            this.valueLabel.Text = "Value:";
            this.valueLabel.Click += new System.EventHandler(this.label4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 31;
            this.label6.Text = "Name:";
            // 
            // appraisalVariableValueTextBox
            // 
            this.appraisalVariableValueTextBox.AllowComposedName = true;
            this.appraisalVariableValueTextBox.AllowLiteral = true;
            this.appraisalVariableValueTextBox.AllowNil = true;
            this.appraisalVariableValueTextBox.AllowUniversal = true;
            this.appraisalVariableValueTextBox.AllowVariable = true;
            this.appraisalVariableValueTextBox.Location = new System.Drawing.Point(43, 130);
            this.appraisalVariableValueTextBox.Name = "appraisalVariableValueTextBox";
            this.appraisalVariableValueTextBox.OnlyIntOrVariable = false;
            this.appraisalVariableValueTextBox.Size = new System.Drawing.Size(343, 22);
            this.appraisalVariableValueTextBox.TabIndex = 44;
            this.appraisalVariableValueTextBox.TextChanged += new System.EventHandler(this.appraisalVariableValueTextBox_TextChanged);
            // 
            // appraisalVariableTarget
            // 
            this.appraisalVariableTarget.AllowComposedName = true;
            this.appraisalVariableTarget.AllowLiteral = true;
            this.appraisalVariableTarget.AllowNil = true;
            this.appraisalVariableTarget.AllowUniversal = true;
            this.appraisalVariableTarget.AllowVariable = true;
            this.appraisalVariableTarget.Location = new System.Drawing.Point(42, 201);
            this.appraisalVariableTarget.Name = "appraisalVariableTarget";
            this.appraisalVariableTarget.OnlyIntOrVariable = false;
            this.appraisalVariableTarget.Size = new System.Drawing.Size(344, 22);
            this.appraisalVariableTarget.TabIndex = 46;
            // 
            // AddOrEditAppraisalVariableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(427, 315);
            this.Controls.Add(this.appraisalVariableTarget);
            this.Controls.Add(this.appraisalVariableValueTextBox);
            this.Controls.Add(this.appraisalVariableName);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.addOrEditButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "AddOrEditAppraisalVariableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Appraisal Variable";
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
        private System.Windows.Forms.ComboBox appraisalVariableName;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Label label6;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox appraisalVariableValueTextBox;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox appraisalVariableTarget;
    }
}
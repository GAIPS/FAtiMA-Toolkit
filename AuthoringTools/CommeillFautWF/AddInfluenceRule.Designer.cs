namespace CommeillFautWF
{
    partial class AddInfluenceRule
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
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.valueFieldBox = new GAIPS.AssetEditorTools.TypedTextBoxes.Int32FieldBox();
            this.conditionSetEditorControl = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            this.modeNameField = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.Mode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 18);
            this.label2.TabIndex = 23;
            this.label2.Text = "Value:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAdd.Location = new System.Drawing.Point(206, 337);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAdd.MinimumSize = new System.Drawing.Size(80, 39);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(195, 39);
            this.buttonAdd.TabIndex = 22;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // valueFieldBox
            // 
            this.valueFieldBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueFieldBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueFieldBox.Location = new System.Drawing.Point(84, 33);
            this.valueFieldBox.Name = "valueFieldBox";
            this.valueFieldBox.Size = new System.Drawing.Size(66, 24);
            this.valueFieldBox.TabIndex = 25;
            // 
            // conditionSetEditorControl
            // 
            this.conditionSetEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionSetEditorControl.Location = new System.Drawing.Point(33, 123);
            this.conditionSetEditorControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conditionSetEditorControl.Name = "conditionSetEditorControl";
            this.conditionSetEditorControl.Size = new System.Drawing.Size(535, 199);
            this.conditionSetEditorControl.TabIndex = 26;
            this.conditionSetEditorControl.View = null;
            this.conditionSetEditorControl.Load += new System.EventHandler(this.conditionSetEditorControl1_Load);
            // 
            // modeNameField
            // 
            this.modeNameField.AllowComposedName = true;
            this.modeNameField.AllowLiteral = true;
            this.modeNameField.AllowNil = true;
            this.modeNameField.AllowUniversal = true;
            this.modeNameField.AllowVariable = true;
            this.modeNameField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modeNameField.Location = new System.Drawing.Point(83, 82);
            this.modeNameField.Name = "modeNameField";
            this.modeNameField.OnlyIntOrVariable = false;
            this.modeNameField.Size = new System.Drawing.Size(218, 22);
            this.modeNameField.TabIndex = 27;
            // 
            // Mode
            // 
            this.Mode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Mode.AutoSize = true;
            this.Mode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mode.Location = new System.Drawing.Point(27, 82);
            this.Mode.Name = "Mode";
            this.Mode.Size = new System.Drawing.Size(50, 18);
            this.Mode.TabIndex = 28;
            this.Mode.Text = "Mode:";
            // 
            // AddInfluenceRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 387);
            this.Controls.Add(this.Mode);
            this.Controls.Add(this.modeNameField);
            this.Controls.Add(this.conditionSetEditorControl);
            this.Controls.Add(this.valueFieldBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonAdd);
            this.Name = "AddInfluenceRule";
            this.Text = "AddInfluenceRule";
            this.Load += new System.EventHandler(this.AddInfluenceRule_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAdd;
        private GAIPS.AssetEditorTools.TypedTextBoxes.Int32FieldBox valueFieldBox;
        private GAIPS.AssetEditorTools.ConditionSetEditorControl conditionSetEditorControl;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox modeNameField;
        private System.Windows.Forms.Label Mode;
    }
}
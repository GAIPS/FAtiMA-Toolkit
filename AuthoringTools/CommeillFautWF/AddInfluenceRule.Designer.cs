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
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label2.Location = new System.Drawing.Point(26, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Value:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.buttonAdd.Location = new System.Drawing.Point(211, 316);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonAdd.MinimumSize = new System.Drawing.Size(70, 37);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(170, 37);
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
            this.valueFieldBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.valueFieldBox.Location = new System.Drawing.Point(73, 31);
            this.valueFieldBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.valueFieldBox.Name = "valueFieldBox";
            this.valueFieldBox.Size = new System.Drawing.Size(119, 22);
            this.valueFieldBox.TabIndex = 25;
            // 
            // conditionSetEditorControl
            // 
            this.conditionSetEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionSetEditorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.conditionSetEditorControl.Location = new System.Drawing.Point(26, 115);
            this.conditionSetEditorControl.Name = "conditionSetEditorControl";
            this.conditionSetEditorControl.Size = new System.Drawing.Size(558, 187);
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
            this.modeNameField.AllowUniversalLiteral = true;
            this.modeNameField.AllowVariable = true;
            this.modeNameField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modeNameField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.modeNameField.Location = new System.Drawing.Point(72, 77);
            this.modeNameField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.modeNameField.Name = "modeNameField";
            this.modeNameField.OnlyIntOrVariable = false;
            this.modeNameField.Size = new System.Drawing.Size(252, 22);
            this.modeNameField.TabIndex = 27;
            // 
            // Mode
            // 
            this.Mode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Mode.AutoSize = true;
            this.Mode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Mode.Location = new System.Drawing.Point(23, 77);
            this.Mode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Mode.Name = "Mode";
            this.Mode.Size = new System.Drawing.Size(46, 16);
            this.Mode.TabIndex = 28;
            this.Mode.Text = "Mode:";
            // 
            // AddInfluenceRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 364);
            this.Controls.Add(this.Mode);
            this.Controls.Add(this.modeNameField);
            this.Controls.Add(this.conditionSetEditorControl);
            this.Controls.Add(this.valueFieldBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonAdd);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
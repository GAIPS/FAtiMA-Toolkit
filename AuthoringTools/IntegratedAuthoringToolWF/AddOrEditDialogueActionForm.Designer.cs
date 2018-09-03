namespace IntegratedAuthoringToolWF
{
    partial class AddOrEditDialogueActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddOrEditDialogueActionForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxUtterance = new System.Windows.Forms.TextBox();
            this.buttonAddOrUpdate = new System.Windows.Forms.Button();
            this.textBoxStyle = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxMeaning = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxNextState = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxCurrentState = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current State:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(415, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Next State:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Meaning:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(415, 97);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Style:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 148);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Utterance:";
            // 
            // textBoxUtterance
            // 
            this.textBoxUtterance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUtterance.Location = new System.Drawing.Point(144, 144);
            this.textBoxUtterance.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBoxUtterance.Name = "textBoxUtterance";
            this.textBoxUtterance.Size = new System.Drawing.Size(607, 26);
            this.textBoxUtterance.TabIndex = 17;
            // 
            // buttonAddOrUpdate
            // 
            this.buttonAddOrUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddOrUpdate.Location = new System.Drawing.Point(345, 203);
            this.buttonAddOrUpdate.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonAddOrUpdate.Name = "buttonAddOrUpdate";
            this.buttonAddOrUpdate.Size = new System.Drawing.Size(101, 28);
            this.buttonAddOrUpdate.TabIndex = 19;
            this.buttonAddOrUpdate.Text = "Add";
            this.buttonAddOrUpdate.UseVisualStyleBackColor = true;
            this.buttonAddOrUpdate.Click += new System.EventHandler(this.buttonAddOrUpdate_Click);
            // 
            // textBoxStyle
            // 
            this.textBoxStyle.AllowComposedName = true;
            this.textBoxStyle.AllowLiteral = true;
            this.textBoxStyle.AllowNil = true;
            this.textBoxStyle.AllowUniversal = true;
            this.textBoxStyle.AllowVariable = true;
            this.textBoxStyle.Location = new System.Drawing.Point(503, 95);
            this.textBoxStyle.Name = "textBoxStyle";
            this.textBoxStyle.OnlyIntOrVariable = false;
            this.textBoxStyle.Size = new System.Drawing.Size(248, 26);
            this.textBoxStyle.TabIndex = 16;
            // 
            // textBoxMeaning
            // 
            this.textBoxMeaning.AllowComposedName = true;
            this.textBoxMeaning.AllowLiteral = true;
            this.textBoxMeaning.AllowNil = true;
            this.textBoxMeaning.AllowUniversal = true;
            this.textBoxMeaning.AllowVariable = true;
            this.textBoxMeaning.Location = new System.Drawing.Point(144, 95);
            this.textBoxMeaning.Name = "textBoxMeaning";
            this.textBoxMeaning.OnlyIntOrVariable = false;
            this.textBoxMeaning.Size = new System.Drawing.Size(229, 26);
            this.textBoxMeaning.TabIndex = 15;
            // 
            // textBoxNextState
            // 
            this.textBoxNextState.AllowComposedName = true;
            this.textBoxNextState.AllowLiteral = true;
            this.textBoxNextState.AllowNil = true;
            this.textBoxNextState.AllowUniversal = true;
            this.textBoxNextState.AllowVariable = true;
            this.textBoxNextState.Location = new System.Drawing.Point(503, 47);
            this.textBoxNextState.Name = "textBoxNextState";
            this.textBoxNextState.OnlyIntOrVariable = false;
            this.textBoxNextState.Size = new System.Drawing.Size(248, 26);
            this.textBoxNextState.TabIndex = 14;
            // 
            // textBoxCurrentState
            // 
            this.textBoxCurrentState.AllowComposedName = true;
            this.textBoxCurrentState.AllowLiteral = true;
            this.textBoxCurrentState.AllowNil = true;
            this.textBoxCurrentState.AllowUniversal = true;
            this.textBoxCurrentState.AllowVariable = true;
            this.textBoxCurrentState.Location = new System.Drawing.Point(144, 47);
            this.textBoxCurrentState.Name = "textBoxCurrentState";
            this.textBoxCurrentState.OnlyIntOrVariable = false;
            this.textBoxCurrentState.Size = new System.Drawing.Size(229, 26);
            this.textBoxCurrentState.TabIndex = 13;
            // 
            // AddOrEditDialogueActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 255);
            this.Controls.Add(this.textBoxStyle);
            this.Controls.Add(this.textBoxMeaning);
            this.Controls.Add(this.textBoxNextState);
            this.Controls.Add(this.textBoxCurrentState);
            this.Controls.Add(this.buttonAddOrUpdate);
            this.Controls.Add(this.textBoxUtterance);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximumSize = new System.Drawing.Size(1061, 302);
            this.MinimumSize = new System.Drawing.Size(18, 302);
            this.Name = "AddOrEditDialogueActionForm";
            this.Text = "Add Dialogue Action";
            this.Load += new System.EventHandler(this.AddOrEditDialogueActionForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddOrEditDialogueActionForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxUtterance;
        private System.Windows.Forms.Button buttonAddOrUpdate;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxCurrentState;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxNextState;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxMeaning;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxStyle;
    }
}
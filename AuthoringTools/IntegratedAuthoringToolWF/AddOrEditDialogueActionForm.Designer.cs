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
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current State:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(305, 47);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Next State:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 101);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Meaning:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 98);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Style:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 163);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Utterance:";
            // 
            // textBoxUtterance
            // 
            this.textBoxUtterance.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxUtterance.Location = new System.Drawing.Point(108, 163);
            this.textBoxUtterance.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBoxUtterance.Name = "textBoxUtterance";
            this.textBoxUtterance.Size = new System.Drawing.Size(485, 22);
            this.textBoxUtterance.TabIndex = 17;
            // 
            // buttonAddOrUpdate
            // 
            this.buttonAddOrUpdate.Location = new System.Drawing.Point(245, 193);
            this.buttonAddOrUpdate.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonAddOrUpdate.MaximumSize = new System.Drawing.Size(159, 42);
            this.buttonAddOrUpdate.MinimumSize = new System.Drawing.Size(159, 42);
            this.buttonAddOrUpdate.Name = "buttonAddOrUpdate";
            this.buttonAddOrUpdate.Size = new System.Drawing.Size(159, 42);
            this.buttonAddOrUpdate.TabIndex = 19;
            this.buttonAddOrUpdate.Text = "Add Dialogue Action";
            this.buttonAddOrUpdate.UseVisualStyleBackColor = true;
            this.buttonAddOrUpdate.Click += new System.EventHandler(this.buttonAddOrUpdate_Click);
            // 
            // textBoxStyle
            // 
            this.textBoxStyle.AllowComposedName = true;
            this.textBoxStyle.AllowLiteral = true;
            this.textBoxStyle.AllowNil = true;
            this.textBoxStyle.AllowUniversal = true;
            this.textBoxStyle.AllowUniversalLiteral = true;
            this.textBoxStyle.AllowVariable = true;
            this.textBoxStyle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxStyle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxStyle.Location = new System.Drawing.Point(385, 98);
            this.textBoxStyle.Name = "textBoxStyle";
            this.textBoxStyle.OnlyIntOrVariable = false;
            this.textBoxStyle.Size = new System.Drawing.Size(208, 22);
            this.textBoxStyle.TabIndex = 16;
            // 
            // textBoxMeaning
            // 
            this.textBoxMeaning.AllowComposedName = true;
            this.textBoxMeaning.AllowLiteral = true;
            this.textBoxMeaning.AllowNil = true;
            this.textBoxMeaning.AllowUniversal = true;
            this.textBoxMeaning.AllowUniversalLiteral = true;
            this.textBoxMeaning.AllowVariable = true;
            this.textBoxMeaning.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxMeaning.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxMeaning.Location = new System.Drawing.Point(108, 98);
            this.textBoxMeaning.Name = "textBoxMeaning";
            this.textBoxMeaning.OnlyIntOrVariable = false;
            this.textBoxMeaning.Size = new System.Drawing.Size(174, 22);
            this.textBoxMeaning.TabIndex = 15;
            // 
            // textBoxNextState
            // 
            this.textBoxNextState.AllowComposedName = true;
            this.textBoxNextState.AllowLiteral = true;
            this.textBoxNextState.AllowNil = true;
            this.textBoxNextState.AllowUniversal = true;
            this.textBoxNextState.AllowUniversalLiteral = true;
            this.textBoxNextState.AllowVariable = true;
            this.textBoxNextState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxNextState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxNextState.Location = new System.Drawing.Point(385, 44);
            this.textBoxNextState.Name = "textBoxNextState";
            this.textBoxNextState.OnlyIntOrVariable = false;
            this.textBoxNextState.Size = new System.Drawing.Size(208, 22);
            this.textBoxNextState.TabIndex = 14;
            // 
            // textBoxCurrentState
            // 
            this.textBoxCurrentState.AllowComposedName = true;
            this.textBoxCurrentState.AllowLiteral = true;
            this.textBoxCurrentState.AllowNil = true;
            this.textBoxCurrentState.AllowUniversal = true;
            this.textBoxCurrentState.AllowUniversalLiteral = true;
            this.textBoxCurrentState.AllowVariable = true;
            this.textBoxCurrentState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxCurrentState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxCurrentState.Location = new System.Drawing.Point(108, 44);
            this.textBoxCurrentState.Name = "textBoxCurrentState";
            this.textBoxCurrentState.OnlyIntOrVariable = false;
            this.textBoxCurrentState.Size = new System.Drawing.Size(174, 22);
            this.textBoxCurrentState.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Location = new System.Drawing.Point(171, 25);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Start, S1, Greeting";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label7.Location = new System.Drawing.Point(194, 79);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(91, 16);
            this.label7.TabIndex = 21;
            this.label7.Text = "Polite, Hungry";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label8.Location = new System.Drawing.Point(334, 143);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(258, 16);
            this.label8.TabIndex = 22;
            this.label8.Text = "Hello, how are you? | I love [[Likes(food)]] !";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label9.Location = new System.Drawing.Point(480, 79);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(116, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "Aggressive, Rude";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label10.Location = new System.Drawing.Point(465, 25);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(131, 16);
            this.label10.TabIndex = 24;
            this.label10.Text = "Compliment, S2, End";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddOrEditDialogueActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 248);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
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
            this.MaximumSize = new System.Drawing.Size(687, 287);
            this.MinimumSize = new System.Drawing.Size(687, 287);
            this.Name = "AddOrEditDialogueActionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}
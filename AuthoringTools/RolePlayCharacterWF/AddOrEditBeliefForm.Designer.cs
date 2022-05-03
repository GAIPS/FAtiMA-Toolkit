namespace RolePlayCharacterWF
{
    partial class AddOrEditBeliefForm
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
            this.emotionalAppraisalAssetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.addOrEditBeliefButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addBeliefErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelCertainty = new System.Windows.Forms.Label();
            this.certaintyTextBox = new GAIPS.AssetEditorTools.TypedTextBoxes.FloatFieldBox();
            this.perspectiveTextBox = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.beliefNameTextBox = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.beliefValueTextBox = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // emotionalAppraisalAssetBindingSource
            // 
            this.emotionalAppraisalAssetBindingSource.DataSource = typeof(EmotionalAppraisal.EmotionalAppraisalAsset);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Belief Name:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // addOrEditBeliefButton
            // 
            this.addOrEditBeliefButton.Location = new System.Drawing.Point(98, 229);
            this.addOrEditBeliefButton.Margin = new System.Windows.Forms.Padding(4);
            this.addOrEditBeliefButton.Name = "addOrEditBeliefButton";
            this.addOrEditBeliefButton.Size = new System.Drawing.Size(149, 30);
            this.addOrEditBeliefButton.TabIndex = 9;
            this.addOrEditBeliefButton.Text = "Add Belief";
            this.addOrEditBeliefButton.UseVisualStyleBackColor = true;
            this.addOrEditBeliefButton.Click += new System.EventHandler(this.addOrEditBeliefButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 132);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Perspective:";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Value:";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // addBeliefErrorProvider
            // 
            this.addBeliefErrorProvider.ContainerControl = this;
            // 
            // labelCertainty
            // 
            this.labelCertainty.AutoSize = true;
            this.labelCertainty.Location = new System.Drawing.Point(48, 181);
            this.labelCertainty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCertainty.Name = "labelCertainty";
            this.labelCertainty.Size = new System.Drawing.Size(63, 16);
            this.labelCertainty.TabIndex = 22;
            this.labelCertainty.Text = "Certainty:";
            // 
            // certaintyTextBox
            // 
            this.certaintyTextBox.HasBounds = false;
            this.certaintyTextBox.Location = new System.Drawing.Point(117, 181);
            this.certaintyTextBox.MaxValue = 0F;
            this.certaintyTextBox.MinValue = 0F;
            this.certaintyTextBox.Name = "certaintyTextBox";
            this.certaintyTextBox.Size = new System.Drawing.Size(192, 22);
            this.certaintyTextBox.TabIndex = 8;
            this.certaintyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.certaintyTextBox.TextChanged += new System.EventHandler(this.floatFieldBox1_TextChanged_1);
            // 
            // perspectiveTextBox
            // 
            this.perspectiveTextBox.AllowComposedName = true;
            this.perspectiveTextBox.AllowLiteral = true;
            this.perspectiveTextBox.AllowNil = true;
            this.perspectiveTextBox.AllowUniversal = true;
            this.perspectiveTextBox.AllowUniversalLiteral = true;
            this.perspectiveTextBox.AllowVariable = true;
            this.perspectiveTextBox.Location = new System.Drawing.Point(117, 132);
            this.perspectiveTextBox.Name = "perspectiveTextBox";
            this.perspectiveTextBox.OnlyIntOrVariable = false;
            this.perspectiveTextBox.Size = new System.Drawing.Size(192, 22);
            this.perspectiveTextBox.TabIndex = 7;
            this.perspectiveTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.perspectiveTextBox.TextChanged += new System.EventHandler(this.perspectiveTextBox_TextChanged);
            // 
            // beliefNameTextBox
            // 
            this.beliefNameTextBox.AllowComposedName = true;
            this.beliefNameTextBox.AllowLiteral = true;
            this.beliefNameTextBox.AllowNil = true;
            this.beliefNameTextBox.AllowUniversal = true;
            this.beliefNameTextBox.AllowUniversalLiteral = true;
            this.beliefNameTextBox.AllowVariable = true;
            this.beliefNameTextBox.Location = new System.Drawing.Point(118, 36);
            this.beliefNameTextBox.Name = "beliefNameTextBox";
            this.beliefNameTextBox.OnlyIntOrVariable = false;
            this.beliefNameTextBox.Size = new System.Drawing.Size(192, 22);
            this.beliefNameTextBox.TabIndex = 5;
            this.beliefNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.beliefNameTextBox.TextChanged += new System.EventHandler(this.beliefNameTextBox_TextChanged_1);
            // 
            // beliefValueTextBox
            // 
            this.beliefValueTextBox.AllowComposedName = true;
            this.beliefValueTextBox.AllowLiteral = true;
            this.beliefValueTextBox.AllowNil = true;
            this.beliefValueTextBox.AllowUniversal = true;
            this.beliefValueTextBox.AllowUniversalLiteral = true;
            this.beliefValueTextBox.AllowVariable = true;
            this.beliefValueTextBox.Location = new System.Drawing.Point(118, 85);
            this.beliefValueTextBox.Name = "beliefValueTextBox";
            this.beliefValueTextBox.OnlyIntOrVariable = false;
            this.beliefValueTextBox.Size = new System.Drawing.Size(192, 22);
            this.beliefValueTextBox.TabIndex = 6;
            this.beliefValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label4.Location = new System.Drawing.Point(153, 18);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(157, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "Has(Money), Loves(Apples)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label5.Location = new System.Drawing.Point(223, 67);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 24;
            this.label5.Text = "5 , True, Sarah";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label6.Location = new System.Drawing.Point(240, 114);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(70, 15);
            this.label6.TabIndex = 25;
            this.label6.Text = "SELF, John";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label7.Location = new System.Drawing.Point(277, 163);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(33, 15);
            this.label7.TabIndex = 26;
            this.label7.Text = "(0-1)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // AddOrEditBeliefForm
            // 
            this.AcceptButton = this.addOrEditBeliefButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(335, 282);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.certaintyTextBox);
            this.Controls.Add(this.perspectiveTextBox);
            this.Controls.Add(this.beliefNameTextBox);
            this.Controls.Add(this.beliefValueTextBox);
            this.Controls.Add(this.labelCertainty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addOrEditBeliefButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "AddOrEditBeliefForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Belief";
            this.Load += new System.EventHandler(this.AddOrEditBeliefForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddOrEditBeliefForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource emotionalAppraisalAssetBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addOrEditBeliefButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider addBeliefErrorProvider;
        private System.Windows.Forms.Label labelCertainty;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox beliefValueTextBox;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox perspectiveTextBox;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox beliefNameTextBox;
        private GAIPS.AssetEditorTools.TypedTextBoxes.FloatFieldBox certaintyTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}
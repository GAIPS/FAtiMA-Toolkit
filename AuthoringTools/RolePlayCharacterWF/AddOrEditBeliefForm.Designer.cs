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
            this.label1.Location = new System.Drawing.Point(53, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Name:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // addOrEditBeliefButton
            // 
            this.addOrEditBeliefButton.Location = new System.Drawing.Point(177, 314);
            this.addOrEditBeliefButton.Margin = new System.Windows.Forms.Padding(4);
            this.addOrEditBeliefButton.Name = "addOrEditBeliefButton";
            this.addOrEditBeliefButton.Size = new System.Drawing.Size(100, 28);
            this.addOrEditBeliefButton.TabIndex = 9;
            this.addOrEditBeliefButton.Text = "Add";
            this.addOrEditBeliefButton.UseVisualStyleBackColor = true;
            this.addOrEditBeliefButton.Click += new System.EventHandler(this.addOrEditBeliefButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 169);
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
            this.label2.Location = new System.Drawing.Point(53, 103);
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
            this.labelCertainty.Location = new System.Drawing.Point(53, 236);
            this.labelCertainty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCertainty.Name = "labelCertainty";
            this.labelCertainty.Size = new System.Drawing.Size(63, 16);
            this.labelCertainty.TabIndex = 22;
            this.labelCertainty.Text = "Certainty:";
            // 
            // certaintyTextBox
            // 
            this.certaintyTextBox.HasBounds = false;
            this.certaintyTextBox.Location = new System.Drawing.Point(53, 267);
            this.certaintyTextBox.MaxValue = 0F;
            this.certaintyTextBox.MinValue = 0F;
            this.certaintyTextBox.Name = "certaintyTextBox";
            this.certaintyTextBox.Size = new System.Drawing.Size(345, 22);
            this.certaintyTextBox.TabIndex = 8;
            this.certaintyTextBox.TextChanged += new System.EventHandler(this.floatFieldBox1_TextChanged_1);
            // 
            // perspectiveTextBox
            // 
            this.perspectiveTextBox.AllowComposedName = true;
            this.perspectiveTextBox.AllowLiteral = true;
            this.perspectiveTextBox.AllowNil = true;
            this.perspectiveTextBox.AllowUniversal = true;
            this.perspectiveTextBox.AllowVariable = true;
            this.perspectiveTextBox.Location = new System.Drawing.Point(53, 198);
            this.perspectiveTextBox.Name = "perspectiveTextBox";
            this.perspectiveTextBox.OnlyIntOrVariable = false;
            this.perspectiveTextBox.Size = new System.Drawing.Size(345, 22);
            this.perspectiveTextBox.TabIndex = 7;
            this.perspectiveTextBox.TextChanged += new System.EventHandler(this.perspectiveTextBox_TextChanged);
            // 
            // beliefNameTextBox
            // 
            this.beliefNameTextBox.AllowComposedName = true;
            this.beliefNameTextBox.AllowLiteral = true;
            this.beliefNameTextBox.AllowNil = true;
            this.beliefNameTextBox.AllowUniversal = true;
            this.beliefNameTextBox.AllowVariable = true;
            this.beliefNameTextBox.Location = new System.Drawing.Point(53, 66);
            this.beliefNameTextBox.Name = "beliefNameTextBox";
            this.beliefNameTextBox.OnlyIntOrVariable = false;
            this.beliefNameTextBox.Size = new System.Drawing.Size(345, 22);
            this.beliefNameTextBox.TabIndex = 5;
            this.beliefNameTextBox.TextChanged += new System.EventHandler(this.beliefNameTextBox_TextChanged_1);
            // 
            // beliefValueTextBox
            // 
            this.beliefValueTextBox.AllowComposedName = true;
            this.beliefValueTextBox.AllowLiteral = true;
            this.beliefValueTextBox.AllowNil = true;
            this.beliefValueTextBox.AllowUniversal = true;
            this.beliefValueTextBox.AllowVariable = true;
            this.beliefValueTextBox.Location = new System.Drawing.Point(53, 129);
            this.beliefValueTextBox.Name = "beliefValueTextBox";
            this.beliefValueTextBox.OnlyIntOrVariable = false;
            this.beliefValueTextBox.Size = new System.Drawing.Size(345, 22);
            this.beliefValueTextBox.TabIndex = 6;
            // 
            // AddOrEditBeliefForm
            // 
            this.AcceptButton = this.addOrEditBeliefButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(453, 378);
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
    }
}
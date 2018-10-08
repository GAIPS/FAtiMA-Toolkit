namespace CommeillFautWF
{
    partial class AddSocialExchange
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.influenceRuleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.stepsTextBox = new System.Windows.Forms.TextBox();
            this.stepsLabel = new System.Windows.Forms.Label();
            this.wfNameTarget = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.nameTextBox = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            ((System.ComponentModel.ISupportInitialize)(this.influenceRuleBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Social Exchange Name:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "uhm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(31, 134);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(395, 26);
            this.textBoxDescription.TabIndex = 2;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAdd.Location = new System.Drawing.Point(183, 320);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAdd.MinimumSize = new System.Drawing.Size(80, 39);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(116, 39);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Target Variable:";
            // 
            // stepsTextBox
            // 
            this.stepsTextBox.Location = new System.Drawing.Point(31, 199);
            this.stepsTextBox.Name = "stepsTextBox";
            this.stepsTextBox.Size = new System.Drawing.Size(395, 26);
            this.stepsTextBox.TabIndex = 3;
            // 
            // stepsLabel
            // 
            this.stepsLabel.AutoSize = true;
            this.stepsLabel.Location = new System.Drawing.Point(28, 172);
            this.stepsLabel.Name = "stepsLabel";
            this.stepsLabel.Size = new System.Drawing.Size(57, 20);
            this.stepsLabel.TabIndex = 21;
            this.stepsLabel.Text = "Steps:";
            // 
            // wfNameTarget
            // 
            this.wfNameTarget.AllowComposedName = true;
            this.wfNameTarget.AllowLiteral = true;
            this.wfNameTarget.AllowNil = true;
            this.wfNameTarget.AllowUniversal = true;
            this.wfNameTarget.AllowVariable = true;
            this.wfNameTarget.Location = new System.Drawing.Point(206, 254);
            this.wfNameTarget.Name = "wfNameTarget";
            this.wfNameTarget.OnlyIntOrVariable = false;
            this.wfNameTarget.Size = new System.Drawing.Size(71, 26);
            this.wfNameTarget.TabIndex = 5;
            // 
            // nameTextBox
            // 
            this.nameTextBox.AllowComposedName = true;
            this.nameTextBox.AllowLiteral = true;
            this.nameTextBox.AllowNil = true;
            this.nameTextBox.AllowUniversal = true;
            this.nameTextBox.AllowVariable = true;
            this.nameTextBox.Location = new System.Drawing.Point(31, 62);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.OnlyIntOrVariable = false;
            this.nameTextBox.Size = new System.Drawing.Size(395, 26);
            this.nameTextBox.TabIndex = 1;
            // 
            // AddSocialExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 370);
            this.Controls.Add(this.stepsTextBox);
            this.Controls.Add(this.stepsLabel);
            this.Controls.Add(this.wfNameTarget);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AddSocialExchange";
            this.Text = "Add Social Exchange";
            this.Load += new System.EventHandler(this.AddSocialExchange_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddSocialExchange_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.influenceRuleBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource influenceRuleBindingSource;
        private System.Windows.Forms.Label label2;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox nameTextBox;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label4;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox wfNameTarget;
        private System.Windows.Forms.TextBox stepsTextBox;
        private System.Windows.Forms.Label stepsLabel;
    }
}
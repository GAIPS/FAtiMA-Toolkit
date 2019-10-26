namespace IntegratedAuthoringToolWF
{
    partial class AddCharacterForm
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
            this.wfNameFieldBoxCharacterName = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wfNameFieldBoxCharacterName
            // 
            this.wfNameFieldBoxCharacterName.AllowComposedName = true;
            this.wfNameFieldBoxCharacterName.AllowLiteral = true;
            this.wfNameFieldBoxCharacterName.AllowNil = true;
            this.wfNameFieldBoxCharacterName.AllowUniversal = true;
            this.wfNameFieldBoxCharacterName.AllowUniversalLiteral = true;
            this.wfNameFieldBoxCharacterName.AllowVariable = true;
            this.wfNameFieldBoxCharacterName.Location = new System.Drawing.Point(65, 23);
            this.wfNameFieldBoxCharacterName.Name = "wfNameFieldBoxCharacterName";
            this.wfNameFieldBoxCharacterName.OnlyIntOrVariable = false;
            this.wfNameFieldBoxCharacterName.Size = new System.Drawing.Size(257, 20);
            this.wfNameFieldBoxCharacterName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(118, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddCharacterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 102);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wfNameFieldBoxCharacterName);
            this.Name = "AddCharacterForm";
            this.Text = "Add New Character";
            this.Load += new System.EventHandler(this.AddCharacterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox wfNameFieldBoxCharacterName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}
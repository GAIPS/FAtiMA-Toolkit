namespace AuthoringToolWF
{
    partial class AddOccupationForm
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
            this.addOccupationButton = new System.Windows.Forms.Button();
            this.occupationToBeAdded = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addOccupationButton
            // 
            this.addOccupationButton.Location = new System.Drawing.Point(172, 31);
            this.addOccupationButton.Name = "addOccupationButton";
            this.addOccupationButton.Size = new System.Drawing.Size(75, 23);
            this.addOccupationButton.TabIndex = 0;
            this.addOccupationButton.Text = "Add";
            this.addOccupationButton.UseVisualStyleBackColor = true;
            this.addOccupationButton.Click += new System.EventHandler(this.addOccupationButton_Click);
            // 
            // occupationToBeAdded
            // 
            this.occupationToBeAdded.Location = new System.Drawing.Point(13, 34);
            this.occupationToBeAdded.Name = "occupationToBeAdded";
            this.occupationToBeAdded.Size = new System.Drawing.Size(139, 20);
            this.occupationToBeAdded.TabIndex = 1;
            // 
            // AddOccupationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 88);
            this.Controls.Add(this.occupationToBeAdded);
            this.Controls.Add(this.addOccupationButton);
            this.Name = "AddOccupationForm";
            this.Text = "AddOccupationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addOccupationButton;
        private System.Windows.Forms.TextBox occupationToBeAdded;
    }
}
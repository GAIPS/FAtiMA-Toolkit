namespace RolePlayCharacterWF
{
    partial class AddOrEditEmotionForm
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
            this.addOrEditButton = new System.Windows.Forms.Button();
            this.comboBoxEmotionType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxIntensity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.targetBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.eventComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Emotion Type:";
            this.toolTip1.SetToolTip(this.label1, "Type of Emotion according to the OCC Model");
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // addOrEditButton
            // 
            this.addOrEditButton.Location = new System.Drawing.Point(74, 185);
            this.addOrEditButton.Margin = new System.Windows.Forms.Padding(4);
            this.addOrEditButton.Name = "addOrEditButton";
            this.addOrEditButton.Size = new System.Drawing.Size(119, 30);
            this.addOrEditButton.TabIndex = 20;
            this.addOrEditButton.Text = "Add Emotion";
            this.addOrEditButton.UseVisualStyleBackColor = true;
            this.addOrEditButton.Click += new System.EventHandler(this.addOrEditButton_Click);
            // 
            // comboBoxEmotionType
            // 
            this.comboBoxEmotionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmotionType.FormattingEnabled = true;
            this.comboBoxEmotionType.Location = new System.Drawing.Point(149, 27);
            this.comboBoxEmotionType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxEmotionType.Name = "comboBoxEmotionType";
            this.comboBoxEmotionType.Size = new System.Drawing.Size(102, 24);
            this.comboBoxEmotionType.TabIndex = 19;
            this.toolTip1.SetToolTip(this.comboBoxEmotionType, "Type of Emotion according to the OCC Model");
            this.comboBoxEmotionType.SelectedIndexChanged += new System.EventHandler(this.beliefVisibilityComboBox_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Event Cause Id:";
            this.toolTip1.SetToolTip(this.label3, "In order to add an Emotion it is necessary to have an even record in the agent\'s " +
        "memoy, if there is no event please add one");
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Intensity:";
            this.toolTip1.SetToolTip(this.label2, "Intensity of an emotion, from 0  to 10");
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // comboBoxIntensity
            // 
            this.comboBoxIntensity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIntensity.FormattingEnabled = true;
            this.comboBoxIntensity.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxIntensity.Location = new System.Drawing.Point(149, 65);
            this.comboBoxIntensity.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxIntensity.Name = "comboBoxIntensity";
            this.comboBoxIntensity.Size = new System.Drawing.Size(102, 24);
            this.comboBoxIntensity.TabIndex = 21;
            this.toolTip1.SetToolTip(this.comboBoxIntensity, "Intensity of an emotion, from 0  to 10");
            this.comboBoxIntensity.SelectedIndexChanged += new System.EventHandler(this.comboBoxIntensity_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 143);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "Target:";
            this.toolTip1.SetToolTip(this.label4, "Target of the Emotion, it is reccomend it to keep it as SELF");
            // 
            // targetBox
            // 
            this.targetBox.Location = new System.Drawing.Point(149, 140);
            this.targetBox.Margin = new System.Windows.Forms.Padding(4);
            this.targetBox.Name = "targetBox";
            this.targetBox.Size = new System.Drawing.Size(102, 22);
            this.targetBox.TabIndex = 23;
            this.targetBox.Text = "SELF";
            this.targetBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.targetBox, "Target of the Emotion, it is reccomend it to keep it as SELF");
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipTitle = "Add Emotion:";
            // 
            // eventComboBox
            // 
            this.eventComboBox.DropDownHeight = 130;
            this.eventComboBox.FormattingEnabled = true;
            this.eventComboBox.IntegralHeight = false;
            this.eventComboBox.Location = new System.Drawing.Point(149, 102);
            this.eventComboBox.Name = "eventComboBox";
            this.eventComboBox.Size = new System.Drawing.Size(102, 24);
            this.eventComboBox.TabIndex = 24;
            // 
            // AddOrEditEmotionForm
            // 
            this.AcceptButton = this.addOrEditButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(285, 228);
            this.Controls.Add(this.eventComboBox);
            this.Controls.Add(this.targetBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxIntensity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addOrEditButton);
            this.Controls.Add(this.comboBoxEmotionType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "AddOrEditEmotionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Emotion";
            this.Load += new System.EventHandler(this.AddOrEditBeliefForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddOrEditEmotionForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addOrEditButton;
        private System.Windows.Forms.ComboBox comboBoxEmotionType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxIntensity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox targetBox;
        public System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox eventComboBox;
    }
}
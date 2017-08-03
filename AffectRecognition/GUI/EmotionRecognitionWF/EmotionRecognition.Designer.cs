namespace EmotionRecognitionWF
{
    partial class EmotionRecognition
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
            this.lblTextInput = new System.Windows.Forms.Label();
            this.txtTextInput = new System.Windows.Forms.TextBox();
            this.btTextInput = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgvTextInformation = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxFusionPolicy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgvSpeechInformation = new System.Windows.Forms.DataGridView();
            this.dtgvEDAInformation = new System.Windows.Forms.DataGridView();
            this.dtgFusedAffectiveInformation = new System.Windows.Forms.DataGridView();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.SoundInputTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlFacialRecognition = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btStart = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTextInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSpeechInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvEDAInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFusedAffectiveInformation)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTextInput
            // 
            this.lblTextInput.AutoSize = true;
            this.lblTextInput.Location = new System.Drawing.Point(18, 219);
            this.lblTextInput.Name = "lblTextInput";
            this.lblTextInput.Size = new System.Drawing.Size(55, 13);
            this.lblTextInput.TabIndex = 0;
            this.lblTextInput.Text = "Text Input";
            // 
            // txtTextInput
            // 
            this.txtTextInput.Location = new System.Drawing.Point(21, 235);
            this.txtTextInput.Name = "txtTextInput";
            this.txtTextInput.Size = new System.Drawing.Size(206, 20);
            this.txtTextInput.TabIndex = 1;
            // 
            // btTextInput
            // 
            this.btTextInput.Location = new System.Drawing.Point(72, 261);
            this.btTextInput.Name = "btTextInput";
            this.btTextInput.Size = new System.Drawing.Size(85, 35);
            this.btTextInput.TabIndex = 2;
            this.btTextInput.Text = "Send";
            this.btTextInput.UseVisualStyleBackColor = true;
            this.btTextInput.Click += new System.EventHandler(this.btTextInput_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btTextInput);
            this.panel1.Controls.Add(this.txtTextInput);
            this.panel1.Controls.Add(this.lblTextInput);
            this.panel1.Controls.Add(this.dtgvTextInformation);
            this.panel1.Location = new System.Drawing.Point(20, 492);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 316);
            this.panel1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Text Recognition";
            // 
            // dtgvTextInformation
            // 
            this.dtgvTextInformation.AllowUserToAddRows = false;
            this.dtgvTextInformation.AllowUserToDeleteRows = false;
            this.dtgvTextInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvTextInformation.Location = new System.Drawing.Point(21, 31);
            this.dtgvTextInformation.Name = "dtgvTextInformation";
            this.dtgvTextInformation.ReadOnly = true;
            this.dtgvTextInformation.Size = new System.Drawing.Size(206, 176);
            this.dtgvTextInformation.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Fusion Policy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Speech Recognition";
            // 
            // cboxFusionPolicy
            // 
            this.cboxFusionPolicy.FormattingEnabled = true;
            this.cboxFusionPolicy.Items.AddRange(new object[] {
            "Max",
            "Weighted Sum",
            "Kalman"});
            this.cboxFusionPolicy.Location = new System.Drawing.Point(14, 31);
            this.cboxFusionPolicy.Name = "cboxFusionPolicy";
            this.cboxFusionPolicy.Size = new System.Drawing.Size(169, 21);
            this.cboxFusionPolicy.TabIndex = 7;
            this.cboxFusionPolicy.SelectedIndexChanged += new System.EventHandler(this.cboxFusionPolicy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "EDA Recognition";
            // 
            // dtgvSpeechInformation
            // 
            this.dtgvSpeechInformation.AllowUserToAddRows = false;
            this.dtgvSpeechInformation.AllowUserToDeleteRows = false;
            this.dtgvSpeechInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvSpeechInformation.Location = new System.Drawing.Point(12, 32);
            this.dtgvSpeechInformation.Name = "dtgvSpeechInformation";
            this.dtgvSpeechInformation.ReadOnly = true;
            this.dtgvSpeechInformation.Size = new System.Drawing.Size(206, 176);
            this.dtgvSpeechInformation.TabIndex = 3;
            // 
            // dtgvEDAInformation
            // 
            this.dtgvEDAInformation.AllowUserToAddRows = false;
            this.dtgvEDAInformation.AllowUserToDeleteRows = false;
            this.dtgvEDAInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvEDAInformation.Location = new System.Drawing.Point(14, 32);
            this.dtgvEDAInformation.Name = "dtgvEDAInformation";
            this.dtgvEDAInformation.ReadOnly = true;
            this.dtgvEDAInformation.Size = new System.Drawing.Size(206, 176);
            this.dtgvEDAInformation.TabIndex = 1;
            // 
            // dtgFusedAffectiveInformation
            // 
            this.dtgFusedAffectiveInformation.AllowUserToAddRows = false;
            this.dtgFusedAffectiveInformation.AllowUserToDeleteRows = false;
            this.dtgFusedAffectiveInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgFusedAffectiveInformation.Location = new System.Drawing.Point(14, 58);
            this.dtgFusedAffectiveInformation.Name = "dtgFusedAffectiveInformation";
            this.dtgFusedAffectiveInformation.ReadOnly = true;
            this.dtgFusedAffectiveInformation.Size = new System.Drawing.Size(253, 238);
            this.dtgFusedAffectiveInformation.TabIndex = 0;
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Interval = 500;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // SoundInputTimer
            // 
            this.SoundInputTimer.Interval = 2000;
            this.SoundInputTimer.Tick += new System.EventHandler(this.SoundInputTimer_Tick);
            // 
            // pnlFacialRecognition
            // 
            this.pnlFacialRecognition.AccessibleDescription = "Facial Recognition";
            this.pnlFacialRecognition.AccessibleName = "Facial Recognition";
            this.pnlFacialRecognition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFacialRecognition.Location = new System.Drawing.Point(20, 12);
            this.pnlFacialRecognition.Name = "pnlFacialRecognition";
            this.pnlFacialRecognition.Size = new System.Drawing.Size(1165, 465);
            this.pnlFacialRecognition.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dtgFusedAffectiveInformation);
            this.panel2.Controls.Add(this.cboxFusionPolicy);
            this.panel2.Location = new System.Drawing.Point(818, 492);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(284, 316);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btStop);
            this.panel3.Controls.Add(this.btStart);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.dtgvSpeechInformation);
            this.panel3.Location = new System.Drawing.Point(297, 492);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(239, 316);
            this.panel3.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.dtgvEDAInformation);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(555, 492);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(239, 316);
            this.panel4.TabIndex = 10;
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(28, 261);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(85, 35);
            this.btStart.TabIndex = 10;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btStop
            // 
            this.btStop.Enabled = false;
            this.btStop.Location = new System.Drawing.Point(119, 261);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(85, 35);
            this.btStop.TabIndex = 11;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // EmotionRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 820);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlFacialRecognition);
            this.Controls.Add(this.panel1);
            this.Name = "EmotionRecognition";
            this.Text = "Emotion Recognition";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTextInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSpeechInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvEDAInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFusedAffectiveInformation)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTextInput;
        private System.Windows.Forms.TextBox txtTextInput;
        private System.Windows.Forms.Button btTextInput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dtgFusedAffectiveInformation;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.DataGridView dtgvEDAInformation;
        private System.Windows.Forms.DataGridView dtgvSpeechInformation;
        private System.Windows.Forms.DataGridView dtgvTextInformation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboxFusionPolicy;
        private System.Windows.Forms.Timer SoundInputTimer;
        private System.Windows.Forms.Panel pnlFacialRecognition;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Button btStart;
    }
}


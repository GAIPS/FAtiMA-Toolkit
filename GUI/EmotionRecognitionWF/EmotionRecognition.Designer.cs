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
            this.lblSoundInput = new System.Windows.Forms.Label();
            this.btRecord = new System.Windows.Forms.Button();
            this.btSendSound = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxFusionPolicy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgvSpeechInformation = new System.Windows.Forms.DataGridView();
            this.dtgvTextInformation = new System.Windows.Forms.DataGridView();
            this.dtgvEDAInformation = new System.Windows.Forms.DataGridView();
            this.dtgFusedAffectiveInformation = new System.Windows.Forms.DataGridView();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSpeechInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTextInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvEDAInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFusedAffectiveInformation)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTextInput
            // 
            this.lblTextInput.AutoSize = true;
            this.lblTextInput.Location = new System.Drawing.Point(27, 25);
            this.lblTextInput.Name = "lblTextInput";
            this.lblTextInput.Size = new System.Drawing.Size(55, 13);
            this.lblTextInput.TabIndex = 0;
            this.lblTextInput.Text = "Text Input";
            // 
            // txtTextInput
            // 
            this.txtTextInput.Location = new System.Drawing.Point(88, 22);
            this.txtTextInput.Name = "txtTextInput";
            this.txtTextInput.Size = new System.Drawing.Size(316, 20);
            this.txtTextInput.TabIndex = 1;
            // 
            // btTextInput
            // 
            this.btTextInput.Location = new System.Drawing.Point(410, 14);
            this.btTextInput.Name = "btTextInput";
            this.btTextInput.Size = new System.Drawing.Size(53, 35);
            this.btTextInput.TabIndex = 2;
            this.btTextInput.Text = "Send";
            this.btTextInput.UseVisualStyleBackColor = true;
            this.btTextInput.Click += new System.EventHandler(this.btTextInput_Click);
            // 
            // lblSoundInput
            // 
            this.lblSoundInput.AutoSize = true;
            this.lblSoundInput.Location = new System.Drawing.Point(27, 80);
            this.lblSoundInput.Name = "lblSoundInput";
            this.lblSoundInput.Size = new System.Drawing.Size(65, 13);
            this.lblSoundInput.TabIndex = 3;
            this.lblSoundInput.Text = "Sound Input";
            // 
            // btRecord
            // 
            this.btRecord.Location = new System.Drawing.Point(98, 69);
            this.btRecord.Name = "btRecord";
            this.btRecord.Size = new System.Drawing.Size(53, 35);
            this.btRecord.TabIndex = 4;
            this.btRecord.Text = "Record";
            this.btRecord.UseVisualStyleBackColor = true;
            this.btRecord.Click += new System.EventHandler(this.btRecord_Click);
            // 
            // btSendSound
            // 
            this.btSendSound.Location = new System.Drawing.Point(157, 69);
            this.btSendSound.Name = "btSendSound";
            this.btSendSound.Size = new System.Drawing.Size(91, 35);
            this.btSendSound.TabIndex = 5;
            this.btSendSound.Text = "Stop andSend";
            this.btSendSound.UseVisualStyleBackColor = true;
            this.btSendSound.Visible = false;
            this.btSendSound.Click += new System.EventHandler(this.btSendSound_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboxFusionPolicy);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtgvSpeechInformation);
            this.panel1.Controls.Add(this.dtgvTextInformation);
            this.panel1.Controls.Add(this.dtgvEDAInformation);
            this.panel1.Controls.Add(this.dtgFusedAffectiveInformation);
            this.panel1.Location = new System.Drawing.Point(27, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(923, 345);
            this.panel1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(675, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Fusion Policy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(454, 14);
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
            this.cboxFusionPolicy.Location = new System.Drawing.Point(678, 30);
            this.cboxFusionPolicy.Name = "cboxFusionPolicy";
            this.cboxFusionPolicy.Size = new System.Drawing.Size(169, 21);
            this.cboxFusionPolicy.TabIndex = 7;
            this.cboxFusionPolicy.SelectedIndexChanged += new System.EventHandler(this.cboxFusionPolicy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Text Recognition";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
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
            this.dtgvSpeechInformation.Location = new System.Drawing.Point(457, 30);
            this.dtgvSpeechInformation.Name = "dtgvSpeechInformation";
            this.dtgvSpeechInformation.ReadOnly = true;
            this.dtgvSpeechInformation.Size = new System.Drawing.Size(206, 209);
            this.dtgvSpeechInformation.TabIndex = 3;
            // 
            // dtgvTextInformation
            // 
            this.dtgvTextInformation.AllowUserToAddRows = false;
            this.dtgvTextInformation.AllowUserToDeleteRows = false;
            this.dtgvTextInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvTextInformation.Location = new System.Drawing.Point(236, 30);
            this.dtgvTextInformation.Name = "dtgvTextInformation";
            this.dtgvTextInformation.ReadOnly = true;
            this.dtgvTextInformation.Size = new System.Drawing.Size(206, 209);
            this.dtgvTextInformation.TabIndex = 2;
            // 
            // dtgvEDAInformation
            // 
            this.dtgvEDAInformation.AllowUserToAddRows = false;
            this.dtgvEDAInformation.AllowUserToDeleteRows = false;
            this.dtgvEDAInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvEDAInformation.Location = new System.Drawing.Point(15, 30);
            this.dtgvEDAInformation.Name = "dtgvEDAInformation";
            this.dtgvEDAInformation.ReadOnly = true;
            this.dtgvEDAInformation.Size = new System.Drawing.Size(206, 209);
            this.dtgvEDAInformation.TabIndex = 1;
            // 
            // dtgFusedAffectiveInformation
            // 
            this.dtgFusedAffectiveInformation.AllowUserToAddRows = false;
            this.dtgFusedAffectiveInformation.AllowUserToDeleteRows = false;
            this.dtgFusedAffectiveInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgFusedAffectiveInformation.Location = new System.Drawing.Point(678, 57);
            this.dtgFusedAffectiveInformation.Name = "dtgFusedAffectiveInformation";
            this.dtgFusedAffectiveInformation.ReadOnly = true;
            this.dtgFusedAffectiveInformation.Size = new System.Drawing.Size(206, 256);
            this.dtgFusedAffectiveInformation.TabIndex = 0;
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Interval = 500;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // EmotionRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 490);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btSendSound);
            this.Controls.Add(this.btRecord);
            this.Controls.Add(this.lblSoundInput);
            this.Controls.Add(this.btTextInput);
            this.Controls.Add(this.txtTextInput);
            this.Controls.Add(this.lblTextInput);
            this.Name = "EmotionRecognition";
            this.Text = "Emotion Recognition";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSpeechInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTextInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvEDAInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFusedAffectiveInformation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTextInput;
        private System.Windows.Forms.TextBox txtTextInput;
        private System.Windows.Forms.Button btTextInput;
        private System.Windows.Forms.Label lblSoundInput;
        private System.Windows.Forms.Button btRecord;
        private System.Windows.Forms.Button btSendSound;
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
    }
}


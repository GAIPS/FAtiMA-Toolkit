namespace EDARecognitionWF
{
    partial class EDARecognition
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
            this.lblConnection = new System.Windows.Forms.Label();
            this.txtConnection = new System.Windows.Forms.TextBox();
            this.txtMinSCL = new System.Windows.Forms.TextBox();
            this.lblMinSCL = new System.Windows.Forms.Label();
            this.grpSCL = new System.Windows.Forms.GroupBox();
            this.lblSCLMovingAverage = new System.Windows.Forms.Label();
            this.lblSCLZScore = new System.Windows.Forms.Label();
            this.txtSCLMovingAverage = new System.Windows.Forms.TextBox();
            this.lblSCLStandard = new System.Windows.Forms.Label();
            this.txtSCLZScore = new System.Windows.Forms.TextBox();
            this.txtSCLStandard = new System.Windows.Forms.TextBox();
            this.lblSCLValue = new System.Windows.Forms.Label();
            this.lblSMaxSCL = new System.Windows.Forms.Label();
            this.txtSCLValue = new System.Windows.Forms.TextBox();
            this.txtMaxSCL = new System.Windows.Forms.TextBox();
            this.grpSCR = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinSCR = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaxSCR = new System.Windows.Forms.TextBox();
            this.txtSCRMovingAverage = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSCRValue = new System.Windows.Forms.TextBox();
            this.txtSCRZScore = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSCRStandard = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.butRecordMinBaseline = new System.Windows.Forms.Button();
            this.butRecordMaxBaseline = new System.Windows.Forms.Button();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.lblLog = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.butLog = new System.Windows.Forms.Button();
            this.grpSCL.SuspendLayout();
            this.grpSCR.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblConnection
            // 
            this.lblConnection.AutoSize = true;
            this.lblConnection.Location = new System.Drawing.Point(12, 9);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(61, 13);
            this.lblConnection.TabIndex = 0;
            this.lblConnection.Text = "Connection";
            // 
            // txtConnection
            // 
            this.txtConnection.Enabled = false;
            this.txtConnection.Location = new System.Drawing.Point(79, 6);
            this.txtConnection.Name = "txtConnection";
            this.txtConnection.Size = new System.Drawing.Size(62, 20);
            this.txtConnection.TabIndex = 1;
            // 
            // txtMinSCL
            // 
            this.txtMinSCL.Enabled = false;
            this.txtMinSCL.Location = new System.Drawing.Point(83, 17);
            this.txtMinSCL.Name = "txtMinSCL";
            this.txtMinSCL.Size = new System.Drawing.Size(62, 20);
            this.txtMinSCL.TabIndex = 3;
            // 
            // lblMinSCL
            // 
            this.lblMinSCL.AutoSize = true;
            this.lblMinSCL.Location = new System.Drawing.Point(6, 20);
            this.lblMinSCL.Name = "lblMinSCL";
            this.lblMinSCL.Size = new System.Drawing.Size(24, 13);
            this.lblMinSCL.TabIndex = 2;
            this.lblMinSCL.Text = "Min";
            // 
            // grpSCL
            // 
            this.grpSCL.Controls.Add(this.lblSCLMovingAverage);
            this.grpSCL.Controls.Add(this.lblSCLZScore);
            this.grpSCL.Controls.Add(this.txtSCLMovingAverage);
            this.grpSCL.Controls.Add(this.lblSCLStandard);
            this.grpSCL.Controls.Add(this.txtSCLZScore);
            this.grpSCL.Controls.Add(this.txtSCLStandard);
            this.grpSCL.Controls.Add(this.lblSCLValue);
            this.grpSCL.Controls.Add(this.lblSMaxSCL);
            this.grpSCL.Controls.Add(this.txtSCLValue);
            this.grpSCL.Controls.Add(this.lblMinSCL);
            this.grpSCL.Controls.Add(this.txtMaxSCL);
            this.grpSCL.Controls.Add(this.txtMinSCL);
            this.grpSCL.Location = new System.Drawing.Point(12, 50);
            this.grpSCL.Name = "grpSCL";
            this.grpSCL.Size = new System.Drawing.Size(201, 173);
            this.grpSCL.TabIndex = 4;
            this.grpSCL.TabStop = false;
            this.grpSCL.Text = "Skin Conductance Level (Tonic)";
            // 
            // lblSCLMovingAverage
            // 
            this.lblSCLMovingAverage.AutoSize = true;
            this.lblSCLMovingAverage.Location = new System.Drawing.Point(6, 150);
            this.lblSCLMovingAverage.Name = "lblSCLMovingAverage";
            this.lblSCLMovingAverage.Size = new System.Drawing.Size(74, 13);
            this.lblSCLMovingAverage.TabIndex = 10;
            this.lblSCLMovingAverage.Text = "Mov. Average";
            // 
            // lblSCLZScore
            // 
            this.lblSCLZScore.AutoSize = true;
            this.lblSCLZScore.Location = new System.Drawing.Point(6, 124);
            this.lblSCLZScore.Name = "lblSCLZScore";
            this.lblSCLZScore.Size = new System.Drawing.Size(42, 13);
            this.lblSCLZScore.TabIndex = 8;
            this.lblSCLZScore.Text = "ZScore";
            // 
            // txtSCLMovingAverage
            // 
            this.txtSCLMovingAverage.Enabled = false;
            this.txtSCLMovingAverage.Location = new System.Drawing.Point(83, 147);
            this.txtSCLMovingAverage.Name = "txtSCLMovingAverage";
            this.txtSCLMovingAverage.Size = new System.Drawing.Size(62, 20);
            this.txtSCLMovingAverage.TabIndex = 11;
            // 
            // lblSCLStandard
            // 
            this.lblSCLStandard.AutoSize = true;
            this.lblSCLStandard.Location = new System.Drawing.Point(6, 98);
            this.lblSCLStandard.Name = "lblSCLStandard";
            this.lblSCLStandard.Size = new System.Drawing.Size(50, 13);
            this.lblSCLStandard.TabIndex = 8;
            this.lblSCLStandard.Text = "Standard";
            // 
            // txtSCLZScore
            // 
            this.txtSCLZScore.Enabled = false;
            this.txtSCLZScore.Location = new System.Drawing.Point(83, 121);
            this.txtSCLZScore.Name = "txtSCLZScore";
            this.txtSCLZScore.Size = new System.Drawing.Size(62, 20);
            this.txtSCLZScore.TabIndex = 9;
            // 
            // txtSCLStandard
            // 
            this.txtSCLStandard.Enabled = false;
            this.txtSCLStandard.Location = new System.Drawing.Point(83, 95);
            this.txtSCLStandard.Name = "txtSCLStandard";
            this.txtSCLStandard.Size = new System.Drawing.Size(62, 20);
            this.txtSCLStandard.TabIndex = 9;
            // 
            // lblSCLValue
            // 
            this.lblSCLValue.AutoSize = true;
            this.lblSCLValue.Location = new System.Drawing.Point(6, 72);
            this.lblSCLValue.Name = "lblSCLValue";
            this.lblSCLValue.Size = new System.Drawing.Size(34, 13);
            this.lblSCLValue.TabIndex = 6;
            this.lblSCLValue.Text = "Value";
            // 
            // lblSMaxSCL
            // 
            this.lblSMaxSCL.AutoSize = true;
            this.lblSMaxSCL.Location = new System.Drawing.Point(6, 46);
            this.lblSMaxSCL.Name = "lblSMaxSCL";
            this.lblSMaxSCL.Size = new System.Drawing.Size(27, 13);
            this.lblSMaxSCL.TabIndex = 6;
            this.lblSMaxSCL.Text = "Max";
            // 
            // txtSCLValue
            // 
            this.txtSCLValue.Enabled = false;
            this.txtSCLValue.Location = new System.Drawing.Point(83, 69);
            this.txtSCLValue.Name = "txtSCLValue";
            this.txtSCLValue.Size = new System.Drawing.Size(62, 20);
            this.txtSCLValue.TabIndex = 7;
            // 
            // txtMaxSCL
            // 
            this.txtMaxSCL.Enabled = false;
            this.txtMaxSCL.Location = new System.Drawing.Point(83, 43);
            this.txtMaxSCL.Name = "txtMaxSCL";
            this.txtMaxSCL.Size = new System.Drawing.Size(62, 20);
            this.txtMaxSCL.TabIndex = 7;
            // 
            // grpSCR
            // 
            this.grpSCR.Controls.Add(this.label1);
            this.grpSCR.Controls.Add(this.txtMinSCR);
            this.grpSCR.Controls.Add(this.label2);
            this.grpSCR.Controls.Add(this.txtMaxSCR);
            this.grpSCR.Controls.Add(this.txtSCRMovingAverage);
            this.grpSCR.Controls.Add(this.label6);
            this.grpSCR.Controls.Add(this.label3);
            this.grpSCR.Controls.Add(this.txtSCRValue);
            this.grpSCR.Controls.Add(this.txtSCRZScore);
            this.grpSCR.Controls.Add(this.label5);
            this.grpSCR.Controls.Add(this.txtSCRStandard);
            this.grpSCR.Controls.Add(this.label4);
            this.grpSCR.Location = new System.Drawing.Point(232, 50);
            this.grpSCR.Name = "grpSCR";
            this.grpSCR.Size = new System.Drawing.Size(201, 173);
            this.grpSCR.TabIndex = 5;
            this.grpSCR.TabStop = false;
            this.grpSCR.Text = "Skin Conductance Response (Phasic)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Mov. Average";
            // 
            // txtMinSCR
            // 
            this.txtMinSCR.Enabled = false;
            this.txtMinSCR.Location = new System.Drawing.Point(87, 17);
            this.txtMinSCR.Name = "txtMinSCR";
            this.txtMinSCR.Size = new System.Drawing.Size(62, 20);
            this.txtMinSCR.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "ZScore";
            // 
            // txtMaxSCR
            // 
            this.txtMaxSCR.Enabled = false;
            this.txtMaxSCR.Location = new System.Drawing.Point(87, 43);
            this.txtMaxSCR.Name = "txtMaxSCR";
            this.txtMaxSCR.Size = new System.Drawing.Size(62, 20);
            this.txtMaxSCR.TabIndex = 17;
            // 
            // txtSCRMovingAverage
            // 
            this.txtSCRMovingAverage.Enabled = false;
            this.txtSCRMovingAverage.Location = new System.Drawing.Point(87, 147);
            this.txtSCRMovingAverage.Name = "txtSCRMovingAverage";
            this.txtSCRMovingAverage.Size = new System.Drawing.Size(62, 20);
            this.txtSCRMovingAverage.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Min";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Standard";
            // 
            // txtSCRValue
            // 
            this.txtSCRValue.Enabled = false;
            this.txtSCRValue.Location = new System.Drawing.Point(87, 69);
            this.txtSCRValue.Name = "txtSCRValue";
            this.txtSCRValue.Size = new System.Drawing.Size(62, 20);
            this.txtSCRValue.TabIndex = 16;
            // 
            // txtSCRZScore
            // 
            this.txtSCRZScore.Enabled = false;
            this.txtSCRZScore.Location = new System.Drawing.Point(87, 121);
            this.txtSCRZScore.Name = "txtSCRZScore";
            this.txtSCRZScore.Size = new System.Drawing.Size(62, 20);
            this.txtSCRZScore.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Max";
            // 
            // txtSCRStandard
            // 
            this.txtSCRStandard.Enabled = false;
            this.txtSCRStandard.Location = new System.Drawing.Point(87, 95);
            this.txtSCRStandard.Name = "txtSCRStandard";
            this.txtSCRStandard.Size = new System.Drawing.Size(62, 20);
            this.txtSCRStandard.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Value";
            // 
            // butRecordMinBaseline
            // 
            this.butRecordMinBaseline.Enabled = false;
            this.butRecordMinBaseline.Location = new System.Drawing.Point(50, 311);
            this.butRecordMinBaseline.Name = "butRecordMinBaseline";
            this.butRecordMinBaseline.Size = new System.Drawing.Size(129, 29);
            this.butRecordMinBaseline.TabIndex = 6;
            this.butRecordMinBaseline.Text = "Record Min Baseline";
            this.butRecordMinBaseline.UseVisualStyleBackColor = true;
            this.butRecordMinBaseline.Click += new System.EventHandler(this.butRecordMinBaseline_Click);
            // 
            // butRecordMaxBaseline
            // 
            this.butRecordMaxBaseline.Enabled = false;
            this.butRecordMaxBaseline.Location = new System.Drawing.Point(267, 311);
            this.butRecordMaxBaseline.Name = "butRecordMaxBaseline";
            this.butRecordMaxBaseline.Size = new System.Drawing.Size(129, 29);
            this.butRecordMaxBaseline.TabIndex = 7;
            this.butRecordMaxBaseline.Text = "Record Max Baseline";
            this.butRecordMaxBaseline.UseVisualStyleBackColor = true;
            this.butRecordMaxBaseline.Click += new System.EventHandler(this.butRecordMaxBaseline_Click);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(51, 259);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(53, 13);
            this.lblLog.TabIndex = 8;
            this.lblLog.Text = "LogName";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(110, 256);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(89, 20);
            this.txtLog.TabIndex = 12;
            // 
            // butLog
            // 
            this.butLog.Location = new System.Drawing.Point(205, 250);
            this.butLog.Name = "butLog";
            this.butLog.Size = new System.Drawing.Size(95, 30);
            this.butLog.TabIndex = 13;
            this.butLog.Text = "Activate Logging";
            this.butLog.UseVisualStyleBackColor = true;
            this.butLog.Click += new System.EventHandler(this.butLog_Click);
            // 
            // EDARecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 373);
            this.Controls.Add(this.butLog);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.butRecordMaxBaseline);
            this.Controls.Add(this.butRecordMinBaseline);
            this.Controls.Add(this.grpSCR);
            this.Controls.Add(this.grpSCL);
            this.Controls.Add(this.txtConnection);
            this.Controls.Add(this.lblConnection);
            this.Name = "EDARecognition";
            this.Text = "EDARecognition";
            this.grpSCL.ResumeLayout(false);
            this.grpSCL.PerformLayout();
            this.grpSCR.ResumeLayout(false);
            this.grpSCR.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.TextBox txtConnection;
        private System.Windows.Forms.TextBox txtMinSCL;
        private System.Windows.Forms.Label lblMinSCL;
        private System.Windows.Forms.GroupBox grpSCL;
        private System.Windows.Forms.GroupBox grpSCR;
        private System.Windows.Forms.Label lblSCLMovingAverage;
        private System.Windows.Forms.Label lblSCLZScore;
        private System.Windows.Forms.TextBox txtSCLMovingAverage;
        private System.Windows.Forms.Label lblSCLStandard;
        private System.Windows.Forms.TextBox txtSCLZScore;
        private System.Windows.Forms.TextBox txtSCLStandard;
        private System.Windows.Forms.Label lblSCLValue;
        private System.Windows.Forms.Label lblSMaxSCL;
        private System.Windows.Forms.TextBox txtSCLValue;
        private System.Windows.Forms.TextBox txtMaxSCL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMinSCR;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaxSCR;
        private System.Windows.Forms.TextBox txtSCRMovingAverage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSCRValue;
        private System.Windows.Forms.TextBox txtSCRZScore;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSCRStandard;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button butRecordMinBaseline;
        private System.Windows.Forms.Button butRecordMaxBaseline;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button butLog;
    }
}


using System;
using System.Windows.Forms;
using EDARecognition;

namespace EDARecognitionWF
{
    public partial class EDARecognition : Form
    {
        public EDARecognitionAsset EDARecognitionAsset { get; private set; }

        public EDARecognition()
        {
            InitializeComponent();
            this.EDARecognitionAsset = new EDARecognitionAsset();
        }

        private void butRecordMinBaseline_Click(object sender, EventArgs e)
        {
            this.EDARecognitionAsset.RecordMinBaseline(10);
        }

        private void butRecordMaxBaseline_Click(object sender, EventArgs e)
        {
            this.EDARecognitionAsset.RecordMaxBaseline(10);
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            this.txtConnection.Text = this.EDARecognitionAsset.Connected.ToString();

            if (this.EDARecognitionAsset.Connected)
            {
                this.txtMinSCL.Text = this.EDARecognitionAsset.SCLMinBaseline.ToString();
                this.txtMaxSCL.Text = this.EDARecognitionAsset.SCLMaxBaseline.ToString();
                this.txtSCLValue.Text = this.EDARecognitionAsset.SkinConductanceLevel.ToString();
                this.txtSCLStandard.Text = this.EDARecognitionAsset.StandardSCL.ToString();
                this.txtSCLZScore.Text = this.EDARecognitionAsset.SCL_ZScore.ToString();
                this.txtSCLMovingAverage.Text = this.EDARecognitionAsset.SCL_MovingAverage.ToString();

                this.txtMinSCR.Text = this.EDARecognitionAsset.SCRMinBaseline.ToString();
                this.txtMaxSCR.Text = this.EDARecognitionAsset.SCRMaxBaseline.ToString();
                this.txtSCRValue.Text = this.EDARecognitionAsset.SkinConductanceResponse.ToString();
                this.txtSCRStandard.Text = this.EDARecognitionAsset.StandardSCR.ToString();
                this.txtSCRZScore.Text = this.EDARecognitionAsset.SCR_ZScore.ToString();
                this.txtSCRMovingAverage.Text = this.EDARecognitionAsset.SCR_MovingAverage.ToString();

                if (this.EDARecognitionAsset.IsRecordingBaseline)
                {
                    this.butRecordMaxBaseline.Enabled = false;
                    this.butRecordMinBaseline.Enabled = false;
                }
                else
                {
                    this.butRecordMaxBaseline.Enabled = true;
                    this.butRecordMinBaseline.Enabled = true;
                }
            }
        }

        private void butLog_Click(object sender, EventArgs e)
        {
            this.EDARecognitionAsset.ActivateLogging(this.txtLog.Text);
            MessageBox.Show("Logging is now active!");
        }
    }
}

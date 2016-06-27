using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RealTimeEmotionRecognition;
using RealTimeEmotionRecognition.FusionPolicies;
using EDARecognition;
using TextEmotionRecognition;
using SpeechEmotionRecognition;
using Equin.ApplicationFramework;
using System.Runtime.InteropServices;
using System.IO;

namespace EmotionRecognitionWF
{
    public partial class EmotionRecognition : Form
    {
        private RealTimeEmotionRecognitionAsset EmotionRecognitionAsset { get; set; }
        private TextEmotionRecognitionAsset TextEmotionRecognitionAsset { get; set; }
        private SpeechEmotionRecognitionAsset SpeechEmotionRecognitionAsset { get; set; }
        private EDARecognitionAsset EDARecognitionAsset { get; set; }

        private BindingListView<AffectiveInformation> FusedAffectiveInformation { get; }
        private BindingListView<AffectiveInformation> EDAInformation { get; }
        private BindingListView<AffectiveInformation> TextInformation { get; }
        private BindingListView<AffectiveInformation> SpeechInformation { get; }

        private KalmanFilterFusionPolicy KalmanFusionPolicy { get; set; }

        public EmotionRecognition()
        {
            InitializeComponent();

            this.EmotionRecognitionAsset = new RealTimeEmotionRecognitionAsset();
            this.TextEmotionRecognitionAsset = new TextEmotionRecognitionAsset { DecayWindow = 30 };
            this.SpeechEmotionRecognitionAsset = new SpeechEmotionRecognitionAsset() { DecayWindow = 30 };
            this.EDARecognitionAsset = new EDARecognitionAsset();

            this.EmotionRecognitionAsset.AddAffectRecognitionAsset(this.EDARecognitionAsset, 1.0f);
            this.EmotionRecognitionAsset.AddAffectRecognitionAsset(this.TextEmotionRecognitionAsset, 1.0f);
            this.EmotionRecognitionAsset.AddAffectRecognitionAsset(this.SpeechEmotionRecognitionAsset, 1.0f);

            this.FusedAffectiveInformation = new BindingListView<AffectiveInformation>(this.EmotionRecognitionAsset.GetSample().ToList());
            this.EDAInformation = new BindingListView<AffectiveInformation>(this.EDARecognitionAsset.GetSample().ToList());
            this.TextInformation = new BindingListView<AffectiveInformation>(this.TextEmotionRecognitionAsset.GetSample().ToList());
            this.SpeechInformation = new BindingListView<AffectiveInformation>(this.SpeechEmotionRecognitionAsset.GetSample().ToList());

            this.dtgFusedAffectiveInformation.DataSource = this.FusedAffectiveInformation;
            this.dtgvEDAInformation.DataSource = this.EDAInformation;
            this.dtgvTextInformation.DataSource = this.TextInformation;
            this.dtgvSpeechInformation.DataSource = this.SpeechInformation;

            this.cboxFusionPolicy.SelectedIndex = 0;
            this.EmotionRecognitionAsset.Policy = new MaxPolicy();

            this.KalmanFusionPolicy = new KalmanFilterFusionPolicy(this.EmotionRecognitionAsset.Classifiers);
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        private void btTextInput_Click(object sender, EventArgs e)
        {
            this.TextEmotionRecognitionAsset.ProcessText(this.txtTextInput.Text);
            this.txtTextInput.Clear();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            this.EmotionRecognitionAsset.UpdateSamples();

            this.FusedAffectiveInformation.DataSource = this.EmotionRecognitionAsset.GetSample().ToList();
            this.FusedAffectiveInformation.Refresh();

            this.EDAInformation.DataSource = this.EDARecognitionAsset.GetSample().ToList();
            this.EDAInformation.Refresh();

            this.TextInformation.DataSource = this.TextEmotionRecognitionAsset.GetSample().ToList();
            this.TextInformation.Refresh();

            this.SpeechInformation.DataSource = this.SpeechEmotionRecognitionAsset.GetSample().ToList();
            this.SpeechInformation.Refresh();
        }

        private void cboxFusionPolicy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cboxFusionPolicy.SelectedText.Equals("Max"))
            {
                this.EmotionRecognitionAsset.Policy = new MaxPolicy();
            }
            else if (this.cboxFusionPolicy.SelectedText.Equals("Max"))
            {
                this.EmotionRecognitionAsset.Policy = new WeightedFusionPolicy();
            }
            else
            {
                this.EmotionRecognitionAsset.Policy = this.KalmanFusionPolicy;
            }
        }

        private void btRecord_Click(object sender, EventArgs e)
        {
            record("open new Type waveaudio Alias recsound", "", 0, 0);
            record("record recsound", "", 0, 0);
            this.btRecord.Visible = false;
            this.btSendSound.Visible = true;
        }

        private void btSendSound_Click(object sender, EventArgs e)
        {
            record("save recsound mic.wav", "", 0, 0);
            record("close recsound", "", 0, 0);

            FileStream speechTestFile = File.Open("anger.wav", FileMode.Open);
            byte[] speech = new byte[speechTestFile.Length];
            speechTestFile.Read(speech, 0, speech.Length);
            speechTestFile.Close();

            this.btSendSound.Visible = false;
            this.btRecord.Visible = true;

            this.SpeechEmotionRecognitionAsset.ProcessSpeechAsync(speech);
        }
    }
}

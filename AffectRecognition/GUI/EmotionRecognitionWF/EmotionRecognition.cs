using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using EDARecognition;
using TextEmotionRecognition;
using SpeechEmotionRecognition;
using Equin.ApplicationFramework;
using System.Runtime.InteropServices;
using System.IO;
using FacialEmotionRecognition;

namespace EmotionRecognitionWF
{
    public partial class EmotionRecognition : Form
    {
        private Form1 FacialEmotionRecognitionForm;
        private RealTimeEmotionRecognitionAsset RealTimeEmotionRecognitionAsset { get; set; }
        private TextEmotionRecognitionComponent TextEmotionRecognitionAsset { get; set; }
        private SpeechEmotionRecognitionComponent SpeechEmotionRecognitionAsset { get; set; }
        private FacialEmotionRecognitionComponent FacialEmotionRecognitionAsset { get; set; }
        private EDARecognitionComponent EDARecognitionAsset { get; set; }

        private BindingListView<AffectiveInformation> FusedAffectiveInformation { get; }
        private BindingListView<AffectiveInformation> EDAInformation { get; }
        private BindingListView<AffectiveInformation> TextInformation { get; }
        private BindingListView<AffectiveInformation> SpeechInformation { get; }

        private Task CurrentSpeechRecognitionTask { get; set; }
        private KalmanFilterFusionPolicy KalmanFusionPolicy { get; set; }



        public EmotionRecognition()
        {
            InitializeComponent();

            this.RealTimeEmotionRecognitionAsset = new RealTimeEmotionRecognitionAsset();
            this.TextEmotionRecognitionAsset = new TextEmotionRecognitionComponent { DecayWindow = 10 };
            this.SpeechEmotionRecognitionAsset = new SpeechEmotionRecognitionComponent() { DecayWindow = 10 };
            this.EDARecognitionAsset = new EDARecognitionComponent();
            this.FacialEmotionRecognitionAsset = new FacialEmotionRecognitionComponent();

            this.RealTimeEmotionRecognitionAsset.AddAffectRecognitionAsset(this.EDARecognitionAsset, 1.0f);
            this.RealTimeEmotionRecognitionAsset.AddAffectRecognitionAsset(this.TextEmotionRecognitionAsset, 1.0f);
            this.RealTimeEmotionRecognitionAsset.AddAffectRecognitionAsset(this.SpeechEmotionRecognitionAsset, 1.0f);
            this.RealTimeEmotionRecognitionAsset.AddAffectRecognitionAsset(this.FacialEmotionRecognitionAsset, 1.0f);

            this.FusedAffectiveInformation = new BindingListView<AffectiveInformation>(this.RealTimeEmotionRecognitionAsset.GetSample().ToList());
            this.EDAInformation = new BindingListView<AffectiveInformation>(this.EDARecognitionAsset.GetSample().ToList());
            this.TextInformation = new BindingListView<AffectiveInformation>(this.TextEmotionRecognitionAsset.GetSample().ToList());
            this.SpeechInformation = new BindingListView<AffectiveInformation>(this.SpeechEmotionRecognitionAsset.GetSample().ToList());

            this.dtgFusedAffectiveInformation.DataSource = this.FusedAffectiveInformation;
            this.dtgvEDAInformation.DataSource = this.EDAInformation;
            this.dtgvTextInformation.DataSource = this.TextInformation;
            this.dtgvSpeechInformation.DataSource = this.SpeechInformation;

            this.cboxFusionPolicy.SelectedIndex = 0;
            this.RealTimeEmotionRecognitionAsset.Policy = new MaxPolicy();

            this.KalmanFusionPolicy = new KalmanFilterFusionPolicy(this.RealTimeEmotionRecognitionAsset.Classifiers);

            this.CurrentSpeechRecognitionTask = null;

            this.FacialEmotionRecognitionForm = new Form1(this.FacialEmotionRecognitionAsset.EDA);
            FormHelper.ShowFormInContainerControl(this.pnlFacialRecognition, this.FacialEmotionRecognitionForm);
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
            this.RealTimeEmotionRecognitionAsset.UpdateSamples();

            this.FusedAffectiveInformation.DataSource = this.RealTimeEmotionRecognitionAsset.GetSample().ToList();
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
            if(this.cboxFusionPolicy.SelectedIndex == 0)
            {
                this.RealTimeEmotionRecognitionAsset.Policy = new MaxPolicy();
            }
            else if (this.cboxFusionPolicy.SelectedIndex == 1)
            {
                this.RealTimeEmotionRecognitionAsset.Policy = new WeightedFusionPolicy();
            }
            else
            {
                this.RealTimeEmotionRecognitionAsset.Policy = this.KalmanFusionPolicy;
            }
        }

        private void btRecord_Click(object sender, EventArgs e)
        {
            //record("open new Type waveaudio Alias recsound", "", 0, 0);
            //record("set recsound time format ms bitspersample 16 channels 2 samplespersec 48000", "", 0, 0);
            //record("record recsound", "", 0, 0);
            //this.btRecord.Visible = false;
            //this.btSendSound.Visible = true;
        }

        private void btSendSound_Click(object sender, EventArgs e)
        {
            //record("save recsound mic.wav", "", 0, 0);
            //record("close recsound", "", 0, 0);

            

            //this.btSendSound.Visible = false;
            //this.btRecord.Visible = true;

            
        }

        private void SoundInputTimer_Tick(object sender, EventArgs e)
        {
            this.StopRecording();
            this.OpenAndProcessSpeech();
            this.StartRecording();
        }

        private void StartRecording()
        {
            record("open new Type waveaudio Alias recsound", "", 0, 0);
            record("set recsound format tag pcm time format ms bitspersample 16 channels 1 alignment 2 samplespersec 8000 bytespersec 16000 ", "", 0, 0);
            record("record recsound", "", 0, 0);
        }

        private void StopRecording()
        {
            record("save recsound mic.wav", "", 0, 0);
            record("close recsound", "", 0, 0);
        }

        private void OpenAndProcessSpeech()
        {
            if(this.CurrentSpeechRecognitionTask == null || this.CurrentSpeechRecognitionTask.IsCompleted)
            {
                FileStream speechTestFile = File.Open("mic.wav", FileMode.Open);
                byte[] speech = new byte[speechTestFile.Length];
                speechTestFile.Read(speech, 0, speech.Length);
                speechTestFile.Close();
                this.CurrentSpeechRecognitionTask = this.SpeechEmotionRecognitionAsset.ProcessSpeechAsync(speech);
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            this.btStart.Enabled = false;
            this.SoundInputTimer.Enabled = true;
            this.SoundInputTimer.Start();
            this.StartRecording();
            this.btStop.Enabled = true;
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            this.btStop.Enabled = false;
            this.SoundInputTimer.Stop();
            this.SoundInputTimer.Enabled = false;
            this.StopRecording();
            this.OpenAndProcessSpeech();
            this.btStart.Enabled = true;
            
        }
    }
}

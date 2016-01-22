using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using AutobiographicMemory.Interfaces;
using EmotionalAppraisal;
using GAIPS.Serialization;


namespace SimToolWF
{
    public enum INTERNAL_STATE
    {
        DEFAULT
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class SimTool : Form
    {

        private List<string> _dialogList = new List<string>();
        private EmotionalAppraisalAsset _emotionalAsset;
        private List<IEvent> _eventsToAppraise = new List<IEvent>();

        private string _perspective;
        private INTERNAL_STATE _internalState;
        private float _threshold;

        private string _mood, _emotion;

        private string _filePath;

        private bool _loadFromFile;

        public string Perspective
        {
            get { return _perspective; }

            set { _perspective = value; }
        }

        public INTERNAL_STATE InternalState
        {
            get { return _internalState; }

            set { _internalState = value; }
        }

        public float Threhsold
        {
            get { return _threshold; }

            set { _threshold = value; }
        }

        public SimTool()
        {
            InitializeComponent();

            if(_loadFromFile)
            {
                LoadAssetFromJSON();
            }
            else _emotionalAsset = new EmotionalAppraisalAsset(_perspective);
           // AgentDecisionMakingWF();
        }

        public void LoadAssetFromJSON()
        {
            Stream fileToDeserialize = File.OpenRead(_filePath);
            var serializer = new JSONSerializer();
            _emotionalAsset = (EmotionalAppraisalAsset)serializer.Deserialize(fileToDeserialize);
        }

        private void AgentDecisionMakingWF()
        {
            IActiveEmotion emotion = _emotionalAsset.EmotionalState.GetStrongestEmotion();

            if (_emotionalAsset.EmotionalState.Mood < _threshold)
            {
                switch (emotion.EmotionType)
                {
                    default:
                       break;
                }
            }

            ShowResult();
        }

        public void EnterDialogLine(string line, IEvent evt = null)
        {
            addDialogLine(string.Format(@"<i>{0}</i>", line), evt);
        }

        public void EnterDialogLine(string speaker, string line, IEvent evt = null)
        {
            addDialogLine(string.Format(@"<b>{0}</b>: {1}", speaker, line), evt);
        }

        private void addDialogLine(string line, IEvent evt)
        {
            if (evt != null)
                line += string.Format("\n<color=green>{0}</color>", evt.ToString());
            _dialogList.Add(line);
        }

        private void addEvent(SimEvent evt)
        {
            _eventsToAppraise.Add(evt);
        }

        public void ShowResult()
        {
            ShowEmotion();
            ShowMood();
        }

        public void SetMood(float newValue)
        {
            _emotionalAsset.SetMood(newValue);
        }

        public void SetState(INTERNAL_STATE newState)
        {
            _internalState = newState;
        }

        public void SetThreshold(float newValue)
        {
            _threshold = newValue;
        }

        public void ShowEmotion()
        {
            emotionLabel.Text = _emotionalAsset.EmotionalState.GetStrongestEmotion().ToString();
        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            _filePath = fileTextBox.Text;
            LoadAssetFromJSON();
        }

        public void ShowMood()
        {
            moodLabel.Text = _emotionalAsset.EmotionalState.Mood.ToString();
        }

    }
}

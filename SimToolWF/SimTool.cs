using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutobiographicMemory.Interfaces;
using EmotionalAppraisal;


namespace SimToolWF
{
    public enum INTERNAL_STATE
    {
        DEFAULT
    }

    public partial class SimTool : Form
    {

        private List<string> _dialogList = new List<string>();
        private EmotionalAppraisalAsset _emotionalAsset;
        private List<IEvent> _eventsToAppraise = new List<IEvent>();

        private string _perspective;
        private INTERNAL_STATE _internalState;
        private float _thresold;

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

        public float Thresold
        {
            get { return _thresold; }

            set { _thresold = value; }
        }

        public SimTool()
        {
            InitializeComponent();
            _emotionalAsset = new EmotionalAppraisalAsset(Perspective);
        }

        private void AgentDecisionMakingWF()
        {
            IActiveEmotion emotion = _emotionalAsset.EmotionalState.GetStrongestEmotion();

            switch(emotion.EmotionType)
            {
                default:
                    break;
            }
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

        public void ShowResult(string resultLine)
        {

        }

        public void SetMood(float newValue)
        {

        }

        public void SetState(INTERNAL_STATE newState)
        {
            _internalState = newState;
        }

        public void SetThresold(float newValue)
        {


        }
    }
}

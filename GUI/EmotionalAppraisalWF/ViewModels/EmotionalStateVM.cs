using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;

namespace EmotionalAppraisalWF.ViewModels
{
    public class EmotionalStateVM
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;
        private ulong _startTime;
        public BindingListView<EmotionDTO> Emotions {get;}
        
        public float Mood
        {
            get { return _emotionalAppraisalAsset.Mood; }
            set { _emotionalAppraisalAsset.Mood = value;}
        }

        public string Perspective
        {
            get { return _emotionalAppraisalAsset.Perspective; }
            set { _emotionalAppraisalAsset.Perspective = value; }
        }

        public IEnumerable<string> EmotionTypes { get { return _emotionalAppraisalAsset.EmotionTypes; } } 

        public ulong Start
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                _emotionalAppraisalAsset.Tick = value;
            }
        }

        public ulong Current{get; set;}

        public void Update()
        {
            this.Current++;
            this._emotionalAppraisalAsset.Update(1);
        }

        public void StopUpdate()
        {
            this.Current = this.Start;
            this._emotionalAppraisalAsset.Tick = _startTime;
        }

        public EmotionalStateVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
            _startTime = _emotionalAppraisalAsset.Tick;
            this.Current = this.Start;
            Emotions = new BindingListView<EmotionDTO>(ea.ActiveEmotions.ToList());
        }
    }
}

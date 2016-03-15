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
            get { return _emotionalAppraisalAsset.Tick; }
            set{ _emotionalAppraisalAsset.Tick = value;}
        }

        public EmotionalStateVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
            Emotions = new BindingListView<EmotionDTO>(ea.ActiveEmotions.ToList());
        }

        public void AddEmotion(EmotionDTO newEmotion)
        {
            var resultingEmotion = _emotionalAppraisalAsset.AddActiveEmotion(newEmotion);
            Emotions.DataSource.Add(resultingEmotion);
            Emotions.Refresh();
        }
    }
}

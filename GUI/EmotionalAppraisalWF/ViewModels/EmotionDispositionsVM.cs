using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;

namespace EmotionalAppraisalWF.ViewModels
{
    public class EmotionDispositionsVM
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        public IEnumerable<string> EmotionTypes { get { return _emotionalAppraisalAsset.EmotionTypes; } }

        public BindingListView<EmotionDispositionDTO> EmotionDispositions {get;}

        public int DefaultThreshold
        {
            get { return _emotionalAppraisalAsset.DefaultEmotionDisposition.Threshold; }
            set { ChangeDefaultEmotionDisposition(value, DefaultDecay); }
        }

        public int DefaultDecay
        {
            get { return _emotionalAppraisalAsset.DefaultEmotionDisposition.Decay; }
            set { ChangeDefaultEmotionDisposition(DefaultThreshold,value); }
        }

        public EmotionDispositionsVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
            this.EmotionDispositions = new BindingListView<EmotionDispositionDTO>(ea.EmotionDispositions.ToList());
        }

        private void ChangeDefaultEmotionDisposition(int threshold, int decay)
        {
            _emotionalAppraisalAsset.DefaultEmotionDisposition = new EmotionDispositionDTO
            {
                Decay = decay,
                Threshold = threshold,
                Emotion = "*"
            };
        }

    }
}

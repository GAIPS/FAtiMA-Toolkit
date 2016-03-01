using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal;
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


        public void AddEmotionDisposition(EmotionDispositionDTO disp)
        {
            _emotionalAppraisalAsset.AddEmotionDisposition(disp);
            EmotionDispositions.DataSource = _emotionalAppraisalAsset.EmotionDispositions.ToList();
            EmotionDispositions.Refresh();
        }

        public void UpdateEmotionDisposition(EmotionDispositionDTO oldDisp, EmotionDispositionDTO newDisp)
        {
            _emotionalAppraisalAsset.RemoveEmotionDisposition(oldDisp.Emotion);
            _emotionalAppraisalAsset.AddEmotionDisposition(newDisp);
            EmotionDispositions.DataSource = _emotionalAppraisalAsset.EmotionDispositions.ToList();
            EmotionDispositions.Refresh();
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

        public void RemoveDispositions(IList<EmotionDispositionDTO> dispositionsToRemove)
        {
            foreach (var emotionDispositionDto in dispositionsToRemove)
            {
                _emotionalAppraisalAsset.RemoveEmotionDisposition(emotionDispositionDto.Emotion);
            }
            EmotionDispositions.DataSource = _emotionalAppraisalAsset.EmotionDispositions.ToList();
            EmotionDispositions.Refresh();
        }
    }
}

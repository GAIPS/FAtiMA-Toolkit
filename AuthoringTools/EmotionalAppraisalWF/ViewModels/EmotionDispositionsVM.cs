using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;

namespace EmotionalAppraisalWF.ViewModels
{
    public class EmotionDispositionsVM
    {
	    private EmotionalAppraisalAsset _ea;
        public BindingListView<EmotionDispositionDTO> EmotionDispositions {get;}
	    private EmotionDispositionDTO _defaultEmotionalDisposition;

		public int DefaultThreshold
        {
            get { return _defaultEmotionalDisposition.Threshold; }
	        set
	        {
		        _defaultEmotionalDisposition.Threshold = value;
				UpdateDefaultEmotionDisposition();
	        }
        }

        public int DefaultDecay
        {
            get { return _defaultEmotionalDisposition.Decay; }
	        set
	        {
		        _defaultEmotionalDisposition.Decay = value;
				UpdateDefaultEmotionDisposition();
	        }
        }

        public EmotionDispositionsVM(EmotionalAppraisalAsset ea)
        {
            _ea = ea;
            this.EmotionDispositions = new BindingListView<EmotionDispositionDTO>(ea.EmotionDispositions.ToList());
	        _defaultEmotionalDisposition = ea.DefaultEmotionDisposition;
        }
		
        public void AddEmotionDisposition(EmotionDispositionDTO disp)
        {
            _ea.AddEmotionDisposition(disp);
            EmotionDispositions.DataSource = _ea.EmotionDispositions.ToList();
            EmotionDispositions.Refresh();
		}

        public void UpdateEmotionDisposition(EmotionDispositionDTO oldDisp, EmotionDispositionDTO newDisp)
        {
            _ea.RemoveEmotionDisposition(oldDisp.Emotion);
            _ea.AddEmotionDisposition(newDisp);
            EmotionDispositions.DataSource = _ea.EmotionDispositions.ToList();
            EmotionDispositions.Refresh();
		}

        private void UpdateDefaultEmotionDisposition()
        {
            _ea.DefaultEmotionDisposition = _defaultEmotionalDisposition;
        }

        public void RemoveDispositions(IList<EmotionDispositionDTO> dispositionsToRemove)
        {
            foreach (var emotionDispositionDto in dispositionsToRemove)
            {
                _ea.RemoveEmotionDisposition(emotionDispositionDto.Emotion);
            }
            EmotionDispositions.DataSource = _ea.EmotionDispositions.ToList();
            EmotionDispositions.Refresh();
		}
    }
}

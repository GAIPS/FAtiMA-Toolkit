using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;

namespace EmotionalAppraisalWF.ViewModels
{
    public class EmotionDispositionsVM
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        public BindingListView<EmotionDispositionDTO> EmotionDispositions {get;}

        public int DefaultThreshold { get; private set; }

        public int DefaultDecay { get; private set; }

        public EmotionDispositionsVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
            this.EmotionDispositions = new BindingListView<EmotionDispositionDTO>(ea.GetEmotionDispositions().ToList());
            this.DefaultDecay = ea.DefaultEmotionDispositionDecay;
            this.DefaultThreshold = ea.DefaultEmotionDispositionThreshold;
        }
        
       
    }
}

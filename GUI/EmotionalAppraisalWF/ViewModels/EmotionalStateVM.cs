using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;

namespace EmotionalAppraisalWF.ViewModels
{
    public class EmotionalStateVM
    {
	    private BaseEAForm _mainForm;
	    private EmotionalAppraisalAsset _emotionalAppraisalAsset => _mainForm.CurrentAsset;

        public BindingListView<EmotionDTO> Emotions {get;}
        
        public float Mood
        {
            get { return _emotionalAppraisalAsset.Mood; }
            set { _emotionalAppraisalAsset.Mood = value;}
        }

        public IEnumerable<string> EmotionTypes { get { return _emotionalAppraisalAsset.EmotionTypes; } } 

        public ulong Start
        {
            get { return _emotionalAppraisalAsset.Tick; }
            set{ _emotionalAppraisalAsset.Tick = value;}
        }

        public EmotionalStateVM(BaseEAForm form)
        {
	        _mainForm = form;
            Emotions = new BindingListView<EmotionDTO>(_emotionalAppraisalAsset.ActiveEmotions.ToList());
        }

	    public void AddEmotion(EmotionDTO newEmotion)
        {
            var resultingEmotion = _emotionalAppraisalAsset.AddActiveEmotion(newEmotion);
            Emotions.DataSource.Add(resultingEmotion);
            Emotions.Refresh();
			_mainForm.SetModified();
        }

        public void UpdateEmotion(EmotionDTO oldEmotion, EmotionDTO newEmotion)
        {
            _emotionalAppraisalAsset.RemoveEmotion(oldEmotion);
            _emotionalAppraisalAsset.AddActiveEmotion(newEmotion);
            Emotions.DataSource = _emotionalAppraisalAsset.ActiveEmotions.ToList();
            Emotions.Refresh();
			_mainForm.SetModified();
		}

        public void RemoveEmotions(IList<EmotionDTO> emotionsToRemove)
        {
            foreach (var emotion in emotionsToRemove)
            {
                _emotionalAppraisalAsset.RemoveEmotion(emotion);
            }
            Emotions.DataSource = _emotionalAppraisalAsset.ActiveEmotions.ToList();
            Emotions.Refresh();
			_mainForm.SetModified();
		}
    }
}

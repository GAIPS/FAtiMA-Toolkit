using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;
using RolePlayCharacter;

namespace RolePlayCharacterWF.ViewModels
{
    public class EmotionalStateVM
    {
	    private BaseRPCForm _mainForm;
	    private RolePlayCharacterAsset _rpcAsset => _mainForm.LoadedAsset;

        public BindingListView<EmotionDTO> Emotions {get;}
        
        public float Mood
        {
            get { return _rpcAsset.Mood; }
            set { _rpcAsset.Mood = value;}
        }

        public ulong Start
        {
            get { return _rpcAsset.Tick; }
	        set
	        {
                _rpcAsset.Tick = value;
		        _mainForm.SetModified();
	        }
        }

        public EmotionalStateVM(BaseRPCForm form)
        {
	        _mainForm = form;
            Emotions = new BindingListView<EmotionDTO>(_rpcAsset.GetAllActiveEmotions().ToList());
        }

	    public void AddEmotion(EmotionDTO newEmotion)
        {
            var resultingEmotion = _rpcAsset.AddActiveEmotion(newEmotion);
            Emotions.DataSource.Add(resultingEmotion);
            Emotions.Refresh();
			_mainForm.SetModified();
        }

        public void UpdateEmotion(EmotionDTO oldEmotion, EmotionDTO newEmotion)
        {
            _rpcAsset.RemoveEmotion(oldEmotion);
            _rpcAsset.AddActiveEmotion(newEmotion);
            Emotions.DataSource = _rpcAsset.GetAllActiveEmotions().ToList();
            Emotions.Refresh();
			_mainForm.SetModified();
		}

        public void RemoveEmotions(IList<EmotionDTO> emotionsToRemove)
        {
            foreach (var emotion in emotionsToRemove)
            {
                _rpcAsset.RemoveEmotion(emotion);
            }
            Emotions.DataSource = _rpcAsset.GetAllActiveEmotions().ToList();
            Emotions.Refresh();
			_mainForm.SetModified();
		}
    }
}

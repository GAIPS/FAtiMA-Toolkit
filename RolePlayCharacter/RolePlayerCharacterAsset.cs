using System;
using System.IO;
using GAIPS.Serialization;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using GAIPS.Rage;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace RolePlayCharacter
{
    [Serializable]
    public class RolePlayCharacterAsset : LoadableAsset<RolePlayCharacterAsset>, ICustomSerialization
    {
        #region RolePlayCharater Fields

	    private string _emotionalAppraisalAssetSource = null;
		private EmotionalAppraisalAsset _emotionalAppraisalAsset;
		private string _emotionalDecisionMakingAssetSource = null;
		private EmotionalDecisionMakingAsset _emotionalDecisionMakingAsset;

	    public ICharacterBody CharacterBody { get; private set; }
	    public string BodyName { get; set;}
        public string CharacterName { get; set; }

	    public string EmotionalAppraisalAssetSource {
		    get { return ToAbsolutePath(_emotionalAppraisalAssetSource); }
		    set { _emotionalAppraisalAssetSource = ToRelativePath(value); }
	    }
	    public string EmotionalDecisionMakingSource {
		    get { return ToAbsolutePath(_emotionalDecisionMakingAssetSource); }
		    set { _emotionalDecisionMakingAssetSource = ToRelativePath(value); }
	    }

        public float Mood { get { return _emotionalAppraisalAsset?.Mood ?? 0; } }

        public IEnumerable<IActiveEmotion> Emotions => _emotionalAppraisalAsset?.GetAllActiveEmotions();

        public string Perspective => _emotionalAppraisalAsset?.Perspective;

		#endregion

		protected override string OnAssetLoaded()
		{
			if (string.IsNullOrEmpty(EmotionalAppraisalAssetSource))
				_emotionalAppraisalAsset = new EmotionalAppraisalAsset("Agent");
			else
			{
				try
				{
					_emotionalAppraisalAsset = EmotionalAppraisalAsset.LoadFromFile(ToAbsolutePath(EmotionalAppraisalAssetSource));
				}
				catch (Exception)
				{
					return $"Unable to load the Emotional Appraisal Asset at \"{EmotionalAppraisalAssetSource}\". Check if the path is correct.";
				}
			}

			if (string.IsNullOrEmpty(EmotionalDecisionMakingSource))
				_emotionalDecisionMakingAsset = new EmotionalDecisionMakingAsset();
			else
			{
				try
				{
					_emotionalDecisionMakingAsset = EmotionalDecisionMakingAsset.LoadFromFile(ToAbsolutePath(EmotionalDecisionMakingSource));
				}
				catch (Exception)
				{
					return $"Unable to load the Emotional Decision Making Asset at \"{EmotionalDecisionMakingSource}\". Check if the path is correct.";
				}
			}

			_emotionalDecisionMakingAsset.RegisterEmotionalAppraisalAsset(_emotionalAppraisalAsset);
			return null;
		}

        public void RegisterCharacterBody(ICharacterBody body)
        {
			CharacterBody = body;
        }
        
        public IEnumerable<IAction> PerceptionActionLoop(IEnumerable<Name> events)
        {
            _emotionalAppraisalAsset.AppraiseEvents(events);
			_emotionalAppraisalAsset.Update();
			return _emotionalDecisionMakingAsset.Decide();
        }

		public IActiveEmotion GetStrongestActiveEmotion()
        {
			IEnumerable<IActiveEmotion> currentActiveEmotions = _emotionalAppraisalAsset.GetAllActiveEmotions();
	        return currentActiveEmotions.MaxValue(a => a.Intensity);
        }

	    public void Update()
        {
            _emotionalAppraisalAsset.Update();
        }
        
        #region Serialization

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("EmotionalAppraisalAssetSource", ToRelativePath(_emotionalAppraisalAssetSource));
            dataHolder.SetValue("EmotionalDecisionMakingAssetSource", ToRelativePath(_emotionalDecisionMakingAssetSource));
            dataHolder.SetValue("CharacterName", CharacterName);
            dataHolder.SetValue("BodyName", BodyName);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            EmotionalAppraisalAssetSource = dataHolder.GetValue<string>("EmotionalAppraisalAssetSource");
            EmotionalDecisionMakingSource = dataHolder.GetValue<string>("EmotionalDecisionMakingAssetSource");
            CharacterName = dataHolder.GetValue<string>("CharacterName");
            BodyName = dataHolder.GetValue<string>("BodyName");
        }
        #endregion
        
        public void SaveOutput(string filePath, string name)
        {
            if(_emotionalAppraisalAsset == null)
                return;

            var filepath = Path.Combine(filePath, name);
            using (var f = File.Open(filepath, FileMode.Create, FileAccess.Write))
            {
                var serializer = new JSONSerializer();
                serializer.Serialize(f, _emotionalAppraisalAsset);
            }
        }
    }
}

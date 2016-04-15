using System;
using System.IO;
using GAIPS.Serialization;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace RolePlayCharacter
{
    [Serializable]
    public class RolePlayCharacterAsset : ICustomSerialization
    {
        #region RolePlayCharater Fields
        [NonSerialized]
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        [NonSerialized]
        private EmotionalDecisionMakingAsset _emotionalDecisionMakingAsset;

	    [NonSerialized]
		private ICharacterBody _characterBody = null;
	    public ICharacterBody CharacterBody {
		    get { return _characterBody; }
	    }

	    public string BodyName { get; set;}

        public string CharacterName { get; set; }
        
        public string EmotionalAppraisalAssetSource { get; set; }

        public string EmotionalDecisionMakingSource { get; set; }

        public float Mood { get { return _emotionalAppraisalAsset == null ? 0 : _emotionalAppraisalAsset.Mood; } }

        public IEnumerable<IActiveEmotion> Emotions => _emotionalAppraisalAsset?.GetAllActiveEmotions();

        public string Perspective => _emotionalAppraisalAsset?.Perspective;

        #endregion

        public static RolePlayCharacterAsset LoadFromFile(string filename)
        {
            RolePlayCharacterAsset rpc;

            using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new JSONSerializer();
                rpc = serializer.Deserialize<RolePlayCharacterAsset>(f);
            }

            if (!string.IsNullOrEmpty(rpc.EmotionalAppraisalAssetSource))
            {
                rpc._emotionalAppraisalAsset = EmotionalAppraisalAsset.LoadFromFile(rpc.EmotionalAppraisalAssetSource);
                if (!string.IsNullOrEmpty(rpc.EmotionalDecisionMakingSource))
                {
                    rpc._emotionalDecisionMakingAsset = EmotionalDecisionMakingAsset.LoadFromFile(rpc.EmotionalDecisionMakingSource);
                    rpc._emotionalDecisionMakingAsset.RegisterEmotionalAppraisalAsset(rpc._emotionalAppraisalAsset);
                }
            }
            return rpc;
        }

        public void RegisterCharacterBody(ICharacterBody body)
        {
            _characterBody = body;
        }
        
        public IAction PerceptionActionLoop(IEnumerable<Name> events)
        {
            _emotionalAppraisalAsset.AppraiseEvents(events);
            return _emotionalDecisionMakingAsset.Decide().FirstOrDefault();
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
        public void SaveToFile(Stream file)
        {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this);
        }

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("EmotionalAppraisalAssetSource", EmotionalAppraisalAssetSource);
            dataHolder.SetValue("EmotionalDecisionMakingAssetSource", EmotionalDecisionMakingSource);
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

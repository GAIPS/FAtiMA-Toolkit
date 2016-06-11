using System;
using System.IO;
using GAIPS.Serialization;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using EmotionalAppraisal.DTOs;
using GAIPS.Rage;
using KnowledgeBase.WellFormedNames;
using SocialImportance;
using Utilities;

namespace RolePlayCharacter
{
    [Serializable]
    public sealed class RolePlayCharacterAsset : LoadableAsset<RolePlayCharacterAsset>//, ICustomSerialization
    {
		#region RolePlayCharater Fields

	    private string _emotionalAppraisalAssetSource = null;
		private string _emotionalDecisionMakingAssetSource = null;
		private string _socialImportanceAssetSource = null;

		[NonSerialized]
		private EmotionalAppraisalAsset _emotionalAppraisalAsset;
		[NonSerialized]
		private EmotionalDecisionMakingAsset _emotionalDecisionMakingAsset;
		[NonSerialized]
		private SocialImportanceAsset _socialImportanceAsset;
		[NonSerialized]
	    private ICharacterBody _characterBody;

		#endregion

		#region Public Interface

		public ICharacterBody CharacterBody => _characterBody;

	    public string BodyName { get; set; }
		public string CharacterName { get; set; }

		public string EmotionalAppraisalAssetSource
		{
			get { return ToAbsolutePath(_emotionalAppraisalAssetSource); }
			set { _emotionalAppraisalAssetSource = ToRelativePath(value); }
		}
		public string EmotionalDecisionMakingSource
		{
			get { return ToAbsolutePath(_emotionalDecisionMakingAssetSource); }
			set { _emotionalDecisionMakingAssetSource = ToRelativePath(value); }
		}
		public string SocialImportanceAssetSource
		{
			get { return ToAbsolutePath(_socialImportanceAssetSource); }
			set { _socialImportanceAssetSource = ToRelativePath(value); }
		}

		public float Mood { get { return _emotionalAppraisalAsset?.Mood ?? 0; } }

		public IEnumerable<IActiveEmotion> Emotions => _emotionalAppraisalAsset?.GetAllActiveEmotions();

		public string Perspective => _emotionalAppraisalAsset?.Perspective;

		public void RegisterCharacterBody(ICharacterBody body)
        {
			_characterBody = body;
        }

	    public void AddBelief(string propertyName, string value)
	    {
			_emotionalAppraisalAsset.AddOrUpdateBelief(new BeliefDTO() {Value = value,Name = propertyName, Perspective = Name.SELF_STRING});
	    }

	    public IAction PerceptionActionLoop(IEnumerable<string> eventStrings)
	    {
			_socialImportanceAsset.InvalidateCachedSI();
			_emotionalAppraisalAsset.AppraiseEvents(eventStrings);

		    var possibleActions = _emotionalDecisionMakingAsset.Decide();
		    var sociallyAcceptedActions = _socialImportanceAsset.FilterActions(Name.SELF_STRING, possibleActions);
		    var conferralAction = _socialImportanceAsset.DecideConferral(Name.SELF_STRING);
		    if (conferralAction != null)
			    sociallyAcceptedActions.Append(conferralAction);

		    return sociallyAcceptedActions.OrderByDescending(a => a.Utility).FirstOrDefault();
	    }

		public void Update()
        {
            _emotionalAppraisalAsset.Update();
        }

		public IActiveEmotion GetStrongestActiveEmotion()
		{
			IEnumerable<IActiveEmotion> currentActiveEmotions = _emotionalAppraisalAsset.GetAllActiveEmotions();
			return currentActiveEmotions.MaxValue(a => a.Intensity);
		}

		#endregion

		protected override string OnAssetLoaded()
		{
			//Load Emotional Appraisal Asset
			try
			{
				_emotionalAppraisalAsset = Loader(_emotionalAppraisalAssetSource, () => new EmotionalAppraisalAsset("Agent"));
			}
			catch (Exception)
			{
				return $"Unable to load the Emotional Appraisal Asset at \"{EmotionalAppraisalAssetSource}\". Check if the path is correct.";
			}

			//Load Emotional Decision Making Asset
			try
			{
				_emotionalDecisionMakingAsset = Loader(_emotionalDecisionMakingAssetSource, () => new EmotionalDecisionMakingAsset());
			}
			catch (Exception)
			{
				return $"Unable to load the Emotional Decision Making Asset at \"{EmotionalDecisionMakingSource}\". Check if the path is correct.";
			}
			_emotionalDecisionMakingAsset.RegisterEmotionalAppraisalAsset(_emotionalAppraisalAsset);

			//Load Social Importance Asset
			try
			{
				_socialImportanceAsset = Loader(_socialImportanceAssetSource, () => new SocialImportanceAsset());
			}
			catch (Exception)
			{
				return $"Unable to load the Emotional Decision Making Asset at \"{SocialImportanceAssetSource}\". Check if the path is correct.";
			}
			_socialImportanceAsset.BindEmotionalAppraisalAsset(_emotionalAppraisalAsset);

			return null;
		}

	    private T Loader<T>(string path, Func<T> generateDefault) where  T: LoadableAsset<T>
	    {
		    if (string.IsNullOrEmpty(path))
			    return generateDefault();

		    return LoadableAsset<T>.LoadFromFile(CurrentStorageProvider, ToAbsolutePath(path));
	    }

	    //#region Serialization

		//public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
  //      {
  //          dataHolder.SetValue("EmotionalAppraisalAssetSource", ToRelativePath(_emotionalAppraisalAssetSource));
  //          dataHolder.SetValue("EmotionalDecisionMakingAssetSource", ToRelativePath(_emotionalDecisionMakingAssetSource));
  //          dataHolder.SetValue("CharacterName", CharacterName);
  //          dataHolder.SetValue("BodyName", BodyName);
  //      }

  //      public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
  //      {
  //          EmotionalAppraisalAssetSource = dataHolder.GetValue<string>("EmotionalAppraisalAssetSource");
  //          EmotionalDecisionMakingSource = dataHolder.GetValue<string>("EmotionalDecisionMakingAssetSource");
  //          CharacterName = dataHolder.GetValue<string>("CharacterName");
  //          BodyName = dataHolder.GetValue<string>("BodyName");
  //      }
  //      #endregion
        
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

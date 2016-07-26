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
using SocialImportance;
using Utilities;
using WellFormedNames;

namespace RolePlayCharacter
{
    [Serializable]
    public sealed class RolePlayCharacterAsset : LoadableAsset<RolePlayCharacterAsset>//, ICustomSerialization
    {
		#region RolePlayCharater Fields

	    private string _emotionalAppraisalAssetSource = null;
		private string _emotionalDecisionMakingAssetSource = null;
		private string _socialImportanceAssetSource = null;

        private static IEnumerable<IAction> TakeBestActions(IEnumerable<IAction> enumerable)
        {
            float best = float.NegativeInfinity;
            foreach (var a in enumerable.OrderByDescending(a => a.Utility))
            {
                if (a.Utility < best)
                    break;

                yield return a;
                best = a.Utility;
            }
        }

        [NonSerialized]
		private EmotionalAppraisalAsset _emotionalAppraisalAsset;
		[NonSerialized]
		private EmotionalDecisionMakingAsset _emotionalDecisionMakingAsset;
		[NonSerialized]
		private SocialImportanceAsset _socialImportanceAsset;

	    private IAction _currentAction = null;

		#endregion

		#region Public Interface

        /// <summary>
        /// An identifier for the embodiment that is used by the character
        /// </summary>
        public string BodyName { get; set; }

        /// <summary>
        /// The name of the character
        /// </summary>
        public string CharacterName { get; set; }

        /// <summary>
        /// The source being used for the Emotional Appraisal Asset
        /// </summary>
        public string EmotionalAppraisalAssetSource
		{
			get { return ToAbsolutePath(_emotionalAppraisalAssetSource); }
			set { _emotionalAppraisalAssetSource = ToRelativePath(value); }
        }

        /// <summary>
        /// The source being used for the Emotional Decision Making Asset
        /// </summary>
        public string EmotionalDecisionMakingSource
		{
			get { return ToAbsolutePath(_emotionalDecisionMakingAssetSource); }
			set { _emotionalDecisionMakingAssetSource = ToRelativePath(value); }
		}

        /// <summary>
        /// The source being used for the Social Importance Asset
        /// </summary>
        public string SocialImportanceAssetSource
		{
			get { return ToAbsolutePath(_socialImportanceAssetSource); }
			set { _socialImportanceAssetSource = ToRelativePath(value); }
		}

		public float Mood { get { return _emotionalAppraisalAsset?.Mood ?? 0; } }

		public IEnumerable<IActiveEmotion> Emotions => _emotionalAppraisalAsset?.GetAllActiveEmotions();


		public Name Perspective => _emotionalAppraisalAsset?.Perspective;

		/// <summary>
        /// Adds or updates a logical belief to the character that consists of a property-value pair
        /// </summary>
        /// <param name="propertyName">A wellformed name representing a logical property (e.g. IsPerson(John))</param>
        /// <param name="value">The value of the property</param>
        public void AddBelief(string propertyName, string value)
	    {
			_emotionalAppraisalAsset.AddOrUpdateBelief(new BeliefDTO() {Value = value,Name = propertyName, Perspective = Name.SELF_STRING});
		}

	    /// <summary>
        /// Executes an iteration of the character's decision cycle.
        /// </summary>
        /// <param name="eventStrings">A list of new events that occurred since the last call to this method. Each event must be represented by a well formed name with the following format "EVENT([type], [subject], [param1], [param2])". 
        /// For illustration purposes here are some examples: EVENT(Property-Change, John, CurrentRole(Customer), False) ; EVENT(Action-Finished, John, Open, Box)</param>
        /// <returns>The action selected for execution or "null" otherwise</returns>
        public IAction PerceptionActionLoop(IEnumerable<string> eventStrings)
	    {
			_socialImportanceAsset.InvalidateCachedSI();
			_emotionalAppraisalAsset.AppraiseEvents(eventStrings);

		    if (_currentAction != null)
			    return null;

		    var possibleActions = _emotionalDecisionMakingAsset.Decide();
		    var sociallyAcceptedActions = _socialImportanceAsset.FilterActions(Name.SELF_STRING, possibleActions);
		    var conferralAction = _socialImportanceAsset.DecideConferral(Name.SELF_STRING);
		    if (conferralAction != null)
			    sociallyAcceptedActions.Append(conferralAction);

			_currentAction = TakeBestActions(sociallyAcceptedActions).Shuffle().FirstOrDefault();
		    if (_currentAction != null)
		    {
				var e = _currentAction.ToStartEventName(Name.SELF_SYMBOL);
				_emotionalAppraisalAsset.AppraiseEvents(new [] {e});
		    }

		    return _currentAction;
	    }


        /// <summary>
        /// Updates the character's internal state. Should be called once every game tick.
        /// </summary>
        public void Update()
        {
            _emotionalAppraisalAsset.Update();
        }

        /// <summary>
        /// Retrieves the character's strongest emotion if any.
        /// </summary>
		public IActiveEmotion GetStrongestActiveEmotion()
		{
			IEnumerable<IActiveEmotion> currentActiveEmotions = _emotionalAppraisalAsset.GetAllActiveEmotions();
			return currentActiveEmotions.MaxValue(a => a.Intensity);
		}

        /// <summary>
        /// Method used to inform the character that its current action is finished and a new action may be selected. It can also generate an emotion associated to finishing an action successfully.
        /// </summary>
	    public void ActionFinished(IAction action)
	    {
			if(_currentAction == null)
				throw new Exception("The RPC asset is not currently executing an action");

			if(!_currentAction.Equals(action))
				throw new ArgumentException("The given action mismatches the currently executing action.",nameof(action));

		    var e = _currentAction.ToFinishedEventName(Name.SELF_SYMBOL);
			_emotionalAppraisalAsset.AppraiseEvents(new[] { e });
		    _currentAction = null;
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

        /// <summary>
        /// Saves the current state of the asset into a file
        /// </summary>
        /// <param name="filePath">The path for the save file</param>
        /// <param name="name">The name of the save file</param>
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
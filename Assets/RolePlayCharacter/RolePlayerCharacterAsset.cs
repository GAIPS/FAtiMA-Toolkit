using System;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using AutobiographicMemory.DTOs;
using EmotionalAppraisal.DTOs;
using GAIPS.Rage;
using KnowledgeBase;
using SocialImportance;
using Utilities;
using WellFormedNames;

namespace RolePlayCharacter
{
    [Serializable]
    public sealed class RolePlayCharacterAsset : LoadableAsset<RolePlayCharacterAsset>//, ICustomSerialization
    {
		#region RolePlayCharater Fields

        private EmotionalAppraisalAsset _emotionalAppraisalAsset;
		private EmotionalDecisionMakingAsset _emotionalDecisionMakingAsset;
		private SocialImportanceAsset _socialImportanceAsset;

		[NonSerialized]
		private IAction _currentAction = null;

		#endregion

		#region Public Interface

		public RolePlayCharacterAsset(EmotionalAppraisalAsset ea, EmotionalDecisionMakingAsset edm = null, SocialImportanceAsset si = null)
		{
			if (ea == null)
				throw new ArgumentNullException(nameof(ea));

			if (edm == null)
				edm = new EmotionalDecisionMakingAsset();

			if (si == null)
				si = new SocialImportanceAsset();

			edm.RegisterEmotionalAppraisalAsset(ea);
			si.BindEmotionalAppraisalAsset(ea);

			_emotionalAppraisalAsset = ea;
			_emotionalDecisionMakingAsset = edm;
			_socialImportanceAsset = si;

			////Load Emotional Decision Making Asset
			//try
			//{
			//	_emotionalDecisionMakingAsset = Loader(_emotionalDecisionMakingAssetSource, () => new EmotionalDecisionMakingAsset());
			//}
			//catch (Exception e)
			//{
			//	AssetManager.Instance.Log(Severity.Critical, e.Message);
			//	return $"Unable to load the Emotional Decision Making Asset at \"{EmotionalDecisionMakingSource}\". Check if the path is correct.";
			//}
			//_emotionalDecisionMakingAsset.RegisterEmotionalAppraisalAsset(_emotionalAppraisalAsset);

			////Load Social Importance Asset
			//try
			//{
			//	_socialImportanceAsset = Loader(_socialImportanceAssetSource, () => new SocialImportanceAsset());
			//}
			//catch (Exception)
			//{
			//	return $"Unable to load the Emotional Decision Making Asset at \"{SocialImportanceAssetSource}\". Check if the path is correct.";
			//}
			//_socialImportanceAsset.BindEmotionalAppraisalAsset(_emotionalAppraisalAsset);
		}

		#region Emotional Appraisal Interface

		public float Mood => _emotionalAppraisalAsset?.Mood ?? 0;

	    public IEnumerable<IActiveEmotion> Emotions => _emotionalAppraisalAsset?.GetAllActiveEmotions();
		
		public Name Perspective => _emotionalAppraisalAsset?.Perspective;

		/// <summary>
        /// Adds or updates a logical belief to the character that consists of a property-value pair
        /// </summary>
        /// <param name="propertyName">A wellformed name representing a logical property (e.g. IsPerson(John))</param>
        /// <param name="value">The value of the property</param>
        [Obsolete]
        public void AddBelief(string propertyName, string value)
	    {
			_emotionalAppraisalAsset.AddOrUpdateBelief(new BeliefDTO() {Value = value,Name = propertyName, Perspective = Name.SELF_STRING});
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
		/// Returns all the associated information regarding an event
		/// </summary>
		/// <param name="eventId">The id of the event to retrieve</param>
		/// <returns>The dto containing the information of the retrieved event</returns>
		public EventDTO GetEventDetails(uint eventId)
		{
			return _emotionalAppraisalAsset.GetEventDetails(eventId);
		}

		public IEnumerable<BeliefDTO> GetAllBeliefs()
		{
			return _emotionalAppraisalAsset.GetAllBeliefs();
		}

		/// <summary>
		/// Return the value associated to a belief.
		/// </summary>
		/// <param name="beliefName">The name of the belief to return</param>
		/// <returns>The string value of the belief, or null if no belief exists.</returns>
		public string GetBeliefValue(string beliefName, string perspective = Name.SELF_STRING)
		{
			return _emotionalAppraisalAsset.GetBeliefValue(beliefName, perspective);
		}

		#endregion

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

		public IDynamicPropertiesRegister DynamicPropertiesRegistry => _emotionalAppraisalAsset.DynamicPropertiesRegistry;

	    #endregion

		protected override string OnAssetLoaded()
		{
			return null;
		}

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
    }
}
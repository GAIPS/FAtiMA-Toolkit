using EmotionalAppraisal.Interfaces;
using System;
using System.Collections.Generic;

namespace EmotionalAppraisal
{
	/// <summary>
	/// Interface for the agent's emotional state. It contains emotions, mood, and arousal.
	/// </summary>
	/// @author: Pedro Gonçalves
	/// @author: João Dias
	public interface IEmotionalState
	{
		/// <summary>
		/// Event dispatched whenever a new emotion is created.
		/// </summary>
		event Action<IEmotionalState, ActiveEmotion> OnEmotionCreated;

		/// <summary>
		/// Creates and Adds to the emotional state a new ActiveEmotion based on a received BaseEmotion.
		/// However, the ActiveEmotion will be created and added to the emotional state only if the final
		/// intensity for the emotion surpasses the threshold for the emotion type. 
		/// </summary>
		/// <param name="emotion">the BaseEmotion that creates the ActiveEmotion</param>
		/// <returns>the ActiveEmotion created if it was added to the EmotionalState.
		/// Otherwise, if the intensity of the emotion was not enough to be added to the EmotionalState, the method returns null</returns>
		ActiveEmotion AddEmotion(BaseEmotion emotion);

		/// <summary>
		/// Removes the given active emotion from the EmotionalState
		/// </summary>
		/// <param name="em">the active emotion to be removed</param>
		void RemoveEmotion(ActiveEmotion em);

		/// <summary>
		/// Creates a new ActiveEmotion based on a received BaseEmotion.
		/// However, the ActiveEmotion will be created only if the final intensity for 
		/// the emotion surpasses the threshold for the emotion type. Very similar to the 
		/// method AddEmotion, but this method DOES NOT ADD the emotion to the emotional state.
		/// It should only be used to determine the emotion that would be created.
		/// </summary>
		/// <param name="potEm">the BaseEmotion that creates the ActiveEmotion</param>
		/// <returns>the ActiveEmotion created. If the intensity of the emotion was not 
		/// enough to be created, the method returns null</returns>
		ActiveEmotion DetermineActiveEmotion(BaseEmotion potEm);

		/// <summary>
		/// Clears all the emotions in the EmotionalState
		/// </summary>
		void Clear();

		/// <summary>
		/// Searches for a given emotion in the EmotionalState
		/// </summary>
		/// <param name="emotionKey">a string that corresponds to a hashkey that represents the emotion in the EmotionalState</param>
		/// <returns>the found ActiveEmotion if it exists in the EmotionalState, null if the emotion doesn't exist anymore</returns>
		ActiveEmotion GetEmotion(string emotionKey);

		/// <summary>
		/// Searches for a given emotion in the EmotionalState
		/// </summary>
		/// <param name="emotionKey">a BaseEmotion that serves as a template to find the active emotion in the EmotionalState</param>
		/// <returns>the found ActiveEmotion if it exists in the EmotionalState, null if the emotion doesn't exist anymore</returns>
		ActiveEmotion GetEmotion(BaseEmotion emotion);

		/// <summary>
		/// Gets a set that contains all the keys for the emotions
		/// </summary>
		/// <returns>a set of all emotion keys contained in this EmotionalState</returns>
		IEnumerable<string> GetEmotionsKeys();

		/// <summary>
		/// Gets a set of all active emotions present in the emotional state
		/// </summary>
		IEnumerable<ActiveEmotion> GetAllEmotions();

		/// <summary>
		/// Gets a float value that represents the characters mood.
		/// 0 represents neutral mood, negative values represent negative mood,
		/// positive values represent positive mood (ranged [-10;10])
		/// </summary>
		float Mood{
			get;
		}
	
		/**
		 * Gets the current strongest emotion (the one with highest intensity)
		 * in the character's emotional state
		 * @return the strongest emotion or null if there is no emotion in the 
		 * 		   emotional state
		 */

		/// <summary>
		/// Gets the current strongest emotion (the one with highest intensity) in the character's emotional state
		/// </summary>
		/// <returns>the strongest emotion or null if there is no emotion in the emotional state</returns>
		ActiveEmotion GetStrongestEmotion();

		/// <summary>
		/// Gets the current strongest emotion (the one with highest intensity) in the character's emotional state, which was triggered by the received event 
		/// </summary>
		/// <param name="cause">the event that caused the emotion that we want to retrieve</param>
		/// <returns>the strongest emotion or null if there is no emotion in the emotional state</returns>
		ActiveEmotion GetStrongestEmotion(Cause cause);

		void AddEmotionDisposition(EmotionDisposition emotionDisposition);

		IEnumerable<EmotionDisposition> GetEmotionDispositions();

		EmotionDisposition getEmotionDisposition(string emotionName);
	}
}

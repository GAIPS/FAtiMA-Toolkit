using EmotionalAppraisal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAIPS.Serialization;
using Utilities;

namespace EmotionalAppraisal
{
	public partial class EmotionalAppraisalAsset
	{
		/// <summary>
		/// Actual implementation of the agent's emotional state.
		/// This ensures that only one instace of the emotional state exists per agent,
		/// because only the agent's correspondent EmotionalModule may create it.
		/// </summary>
		/// @author: Pedro Gonçalves
		/// @author: João Dias
		[Serializable]
		private class ConcreteEmotionalState : IEmotionalState, ICustomSerialization
		{
			private static readonly EmotionDisposition DEFAULT_EMOTIONAL_DISPOSITION = new EmotionDisposition("default", 1, 5);

			private Dictionary<string, ActiveEmotion> emotionPool;
			private Dictionary<string, EmotionDisposition> emotionDispositions;
			private Mood mood;

			public event Action<IEmotionalState, ActiveEmotion> OnEmotionCreated;

			public ConcreteEmotionalState()
			{
				this.emotionPool = new Dictionary<string, ActiveEmotion>();
				this.emotionDispositions = new Dictionary<string, EmotionDisposition>();
				this.mood = new Mood();
			}

			/// <summary>
			/// Clone constructor
			/// </summary>
			/// <param name="other">EmotionalState to clone</param>
			public ConcreteEmotionalState(ConcreteEmotionalState other)
			{
				this.emotionPool = new Dictionary<string, ActiveEmotion>(other.emotionPool);
				this.emotionDispositions = new Dictionary<string, EmotionDisposition>(other.emotionDispositions);
			}

			private float DeterminePotential(BaseEmotion emotion)
			{
				float potetial = emotion.Potential;
				float scale = (float)emotion.Valence;

				potetial += scale*(this.mood.MoodValue*EmotionalParameters.MoodInfluenceOnEmotion);
				return potetial < 0 ? 0 : potetial;
			}

			/// <summary>
			/// unique hash string calculation function
			/// </summary>
			/// <param name="emotion"></param>
			private static string calculateHashString(BaseEmotion emotion)
			{
				StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
				try
				{
					builder.Append(emotion.Cause.CauseName.ToString().ToUpper());
					using (var it = emotion.AppraisalVariables.GetEnumerator())
					{
						while (it.MoveNext())
						{
							builder.Append("-");
							builder.Append(it.Current);
						}
					}

					return builder.ToString();
				}
				finally
				{
					builder.Length = 0;
					ObjectPool<StringBuilder>.Recycle(builder);
				}
			}

			/// <summary>
			/// Creates and Adds to the emotional state a new ActiveEmotion based on 
			/// a received BaseEmotion. However, the ActiveEmotion will be created 
			/// and added to the emotional state only if the final intensity for 
			/// the emotion surpasses the threshold for the emotion type. 
			/// </summary>
			/// <param name="emotion">the BaseEmotion that creates the ActiveEmotion</param>
			/// <returns>the ActiveEmotion created if it was added to the EmotionalState.
			/// Otherwise, if the intensity of the emotion was not enough to be added to the EmotionalState, the method returns null</returns>
			public ActiveEmotion AddEmotion(BaseEmotion emotion)
			{
				if (emotion == null)
					return null;

				int decay;
				ActiveEmotion auxEmotion = null;
				bool reappraisal = false;

				EmotionDisposition disposition = GetEmotionDisposition(emotion.EmotionType);
				decay = disposition.Decay;

				ActiveEmotion previousEmotion;
				string hash = calculateHashString(emotion);
				if (emotionPool.TryGetValue(hash,out previousEmotion))
				{
					//if this test is true, it means that this is 100% a reappraisal of the same event
					//if not true, it is not a reappraisal, but the appraisal of a new event of the same
					//type
					if (previousEmotion.Cause.CauseTimestamp == emotion.Cause.CauseTimestamp)
						reappraisal = true;
					
					//in both cases we need to remove the old emotion. In the case of reappraisal it is obvious.
					//In the case of the appraisal of a similar event, we want to aggregate all the similar resulting 
					//emotions into only one emotion
					emotionPool.Remove(hash);
				}

				float potential = DeterminePotential(emotion);
				if (potential > disposition.Threshold)
				{
					auxEmotion = new ActiveEmotion(emotion, potential, disposition.Threshold, decay);
					emotionPool.Add(hash, auxEmotion);
					if (!reappraisal)
						this.mood.UpdateMood(auxEmotion);

					if (OnEmotionCreated != null)
						OnEmotionCreated(this, auxEmotion);
					/*
					TODO implement this code when developing episodic memory
					am.getMemory().getEpisodicMemory().AssociateEmotionToAction(am.getMemory(),
							auxEmotion,
							auxEmotion.GetCause());
					*/
				}


				return auxEmotion;
			}

			public EmotionDisposition GetEmotionDisposition(String emotionName)
			{
				EmotionDisposition disp;
				if (this.emotionDispositions.TryGetValue(emotionName, out disp))
					return disp;
				
				return DEFAULT_EMOTIONAL_DISPOSITION;
			}

			public ActiveEmotion DetermineActiveEmotion(BaseEmotion potEm)
			{
				EmotionDisposition emotionDisposition = GetEmotionDisposition(potEm.EmotionType);
				float potential = DeterminePotential(potEm);

				if (potential > emotionDisposition.Threshold)
					return new ActiveEmotion(potEm, potential, emotionDisposition.Threshold, emotionDisposition.Decay);

				return null;
			}

			/// <summary>
			/// Clears all the emotions in the EmotionalState
			/// </summary>
			public void Clear()
			{
				this.emotionPool.Clear();
			}

			/// <summary>
			/// Decays all emotions, mood and arousal according to the System Time
			/// </summary>
			public void Decay(float elapsedTime)
			{
				this.mood.DecayMood(elapsedTime);
				HashSet<string> toRemove = ObjectPool<HashSet<string>>.GetObject();
				using (var it = this.emotionPool.GetEnumerator())
				{
					while (it.MoveNext())
					{
						it.Current.Value.DecayEmotion(elapsedTime);
						if (!it.Current.Value.IsRelevant)
							toRemove.Add(it.Current.Key);
					}
				}
				foreach (var key in toRemove)
					this.emotionPool.Remove(key);

				toRemove.Clear();
				ObjectPool<HashSet<string>>.Recycle(toRemove);
			}

			/// <summary>
			/// Searches for a given emotion in the EmotionalState
			/// </summary>
			/// <param name="emotionKey">a string that corresponds to a hashkey that represents the emotion in the EmotionalState</param>
			/// <returns>the found ActiveEmotion if it exists in the EmotionalState, null if the emotion doesn't exist anymore</returns>
			public ActiveEmotion GetEmotion(String emotionKey)
			{
				ActiveEmotion emo;
				if (this.emotionPool.TryGetValue(emotionKey, out emo))
					return emo;
				return null;
			}

			/// <summary>
			/// Searches for a given emotion in the EmotionalState
			/// </summary>
			/// <param name="emotionKey">a BaseEmotion that serves as a template to find the active emotion in the EmotionalState</param>
			/// <returns>the found ActiveEmotion if it exists in the EmotionalState, null if the emotion doesn't exist anymore</returns>
			public ActiveEmotion GetEmotion(BaseEmotion emotion)
			{
				return GetEmotion(calculateHashString(emotion));
			}

			public void RemoveEmotion(ActiveEmotion em)
			{
				if (em != null)
					this.emotionPool.Remove(calculateHashString(em));
			}

			public IEnumerable<string> GetEmotionsKeys()
			{
				return this.emotionPool.Keys;
			}

			public IEnumerable<ActiveEmotion> GetAllEmotions()
			{
				return this.emotionPool.Values;
			}

			public float Mood
			{
				get
				{
					return this.mood.MoodValue;
				}
			}

			public ActiveEmotion GetStrongestEmotion()
			{
				return this.emotionPool.Values.MaxValue(emo => emo.Intensity);
			}

			public ActiveEmotion GetStrongestEmotion(IEvent cause)
			{
				return this.emotionPool.Values.Where(emo => cause.ToName().Match(emo.Cause.CauseName)).MaxValue(emo => emo.Intensity);
			}

			public void AddEmotionDisposition(EmotionDisposition emotionDisposition)
			{
				this.emotionDispositions.Add(emotionDisposition.Emotion, emotionDisposition);
			}

			public IEnumerable<EmotionDisposition> GetEmotionDispositions()
			{
				return this.emotionDispositions.Values;
			}

			public EmotionDisposition getEmotionDisposition(string emotionName)
			{
				EmotionDisposition disp;
				if (this.emotionDispositions.TryGetValue(emotionName, out disp))
					return disp;
				return DEFAULT_EMOTIONAL_DISPOSITION;
			}

			public override string ToString()
			{
				return string.Format("Mood: {0} Emotions: {1}",this.mood,this.emotionPool);
			}

			public void GetObjectData(ISerializationData dataHolder)
			{
				dataHolder.SetValue("EmotionalPool", emotionPool.Values.ToArray());
				dataHolder.SetValue("EmotionDispositions",emotionDispositions.Values.ToArray());
				dataHolder.SetValue("Mood",mood.MoodValue);
			}

			public void SetObjectData(ISerializationData dataHolder)
			{
				mood.SetMoodValue(dataHolder.GetValue<float>("Mood"));
				var dispositions = dataHolder.GetValue<EmotionDisposition[]>("EmotionDispositions");
				foreach (var disposition in dispositions)
					emotionDispositions.Add(disposition.Emotion,disposition);

				var emotions = dataHolder.GetValue<ActiveEmotion[]>("EmotionalPool");
				foreach (var emotion in emotions)
				{
					var hash = calculateHashString(emotion);
					emotionPool.Add(hash,emotion);
				}
			}
		}
	}
}

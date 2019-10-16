using AutobiographicMemory;
using EmotionalAppraisal.DTOs;
using SerializationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using WellFormedNames;

namespace EmotionalAppraisal
{
    [Serializable]
    public class ConcreteEmotionalState : IEmotionalState, ICustomSerialization
    {
        //private EmotionalAppraisalAsset m_parent = null;
        private Dictionary<string, ActiveEmotion> emotionPool;

        private Mood mood;
        private EmotionalAppraisalConfiguration appraisalConfiguration;
        private ulong tick;

        public ConcreteEmotionalState()
        {
            this.emotionPool = new Dictionary<string, ActiveEmotion>();

            this.mood = new Mood();
            this.appraisalConfiguration = new EmotionalAppraisalConfiguration();
        }

        private float DeterminePotential(IEmotion emotion)
        {
            float potetial = emotion.Potential;
            float scale = (float)emotion.Valence;

            potetial += scale * (this.mood.MoodValue * this.appraisalConfiguration.MoodInfluenceOnEmotionFactor);
            return potetial < 0 ? 0 : potetial;
        }

        public EmotionDTO AddActiveEmotion(EmotionDTO emotion, AM am)
        {
            var activeEmotion = new ActiveEmotion(emotion, am, 1, 1);

            if (emotionPool.ContainsKey(calculateHashString(activeEmotion)))
            {
                throw new ArgumentException("This given emotion is already related to given cause", nameof(emotion));
            }

            emotionPool.Add(calculateHashString(activeEmotion), activeEmotion);
            activeEmotion.GetCause(am).LinkEmotion(activeEmotion.EmotionType);
            return activeEmotion.ToDto(am);
        }

        public void RemoveEmotion(EmotionDTO emotion, AM am)
        {
            var activeEmotion = new ActiveEmotion(emotion, am, 1, 1);
            emotionPool.Remove(calculateHashString(activeEmotion));
        }

        /// <summary>
        /// unique hash string calculation function
        /// </summary>
        /// <param name="emotion"></param>
        private static string calculateHashString(IEmotion emotion)
        {
            StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
            try
            {
                var eventType = emotion.EventName.GetNTerm(1);
                //for property change events the cause of the emotion is just the property names
                //this means that if a new change to a property occcurs the previous emotion associated with it will be reappraised
                if (eventType.ToString().EqualsIgnoreCase(AMConsts.PROPERTY_CHANGE))
                {
                    builder.Append(emotion.EventName.GetNTerm(3));
                }
                else
                {
                    builder.Append(emotion.CauseId);
                }

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
        public IActiveEmotion AddEmotion(IEmotion emotion, AM am, EmotionDispositionDTO disposition, ulong _tick)
        {
            if (emotion == null)
                return null;
            tick = _tick;
            ActiveEmotion auxEmotion = null;
            bool reappraisal = false;

            var decay = disposition.Decay;

            ActiveEmotion previousEmotion;
            if (emotionPool.TryGetValue(calculateHashString(emotion), out previousEmotion))
            {
                //if this test is true, it means that this is 100% a reappraisal of the same event
                //if not true, it is not a reappraisal, but the appraisal of a new event of the same
                //type
                if (previousEmotion.CauseId == emotion.CauseId)
                    reappraisal = true;

                //in both cases we need to remove the old emotion. In the case of reappraisal it is obvious.
                //In the case of the appraisal of a similar event, we want to aggregate all the similar resulting
                //emotions into only one
                emotionPool.Remove(calculateHashString(emotion));
            }

            float potential = DeterminePotential(emotion);
            if (potential > disposition.Threshold)
            {
                auxEmotion = new ActiveEmotion(emotion, potential, disposition.Threshold, decay, tick);
                emotionPool.Add(calculateHashString(emotion), auxEmotion);
                if (!reappraisal)
                    this.mood.UpdateMood(auxEmotion, this.appraisalConfiguration, tick);

                auxEmotion.GetCause(am).LinkEmotion(auxEmotion.EmotionType);
            }

            return auxEmotion;
        }

        /// <summary>
        /// Clears all the emotions in the EmotionalState and reset the mood to 0
        /// </summary>
        public void Clear()
        {
            this.emotionPool.Clear();
            this.mood = new Mood();
        }

        /// <summary>
        /// Decays all emotions, mood and arousal
        /// </summary>
        public void Decay(ulong tick)
        {
            this.mood.DecayMood(this.appraisalConfiguration, tick);
            HashSet<string> toRemove = ObjectPool<HashSet<string>>.GetObject();
            using (var it = this.emotionPool.GetEnumerator())
            {
                while (it.MoveNext())
                {
                    it.Current.Value.DecayEmotion(this.appraisalConfiguration, tick);
                    if (!it.Current.Value.IsRelevant)
                    {
                        toRemove.Add(it.Current.Key);
                    }
                }
            }
            foreach (var key in toRemove)
                this.emotionPool.Remove(key);

            toRemove.Clear();
            ObjectPool<HashSet<string>>.Recycle(toRemove);
        }

        public IEnumerable<IActiveEmotion> GetEmotionsByType(string emotionType)
        {
            return emotionPool.Values.Where(emotion => string.Equals(emotion.EmotionType, emotionType, StringComparison.CurrentCultureIgnoreCase)).Cast<IActiveEmotion>();
        }

        /// <summary>
        /// Searches for a given emotion in the EmotionalState
        /// </summary>
        /// <param name="emotionKey">a string that corresponds to a hashkey that represents the emotion in the EmotionalState</param>
        /// <returns>the found ActiveEmotion if it exists in the EmotionalState, null if the emotion doesn't exist anymore</returns>
        public IActiveEmotion GetEmotion(string emotionKey)
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
        public IActiveEmotion GetEmotion(IEmotion emotion)
        {
            return GetEmotion(calculateHashString(emotion));
        }

        public void RemoveEmotion(IActiveEmotion em, AM am)
        {
            if (em != null)
                this.emotionPool.Remove(calculateHashString(em));
        }

        public IEnumerable<string> GetEmotionsKeys()
        {
            return this.emotionPool.Keys;
        }

        public IEnumerable<IActiveEmotion> GetAllEmotions()
        {
            return this.emotionPool.Values.Cast<IActiveEmotion>();
        }

        public float Mood
        {
            get
            {
                return this.mood.MoodValue;
            }
            set
            {
                this.mood.SetMoodValue(value, this.appraisalConfiguration);
            }
        }

        public IActiveEmotion GetStrongestEmotion()
        {
            return this.emotionPool.Values.MaxValue(emo => emo.Intensity);
        }

        public IActiveEmotion GetStrongestEmotion(Name cause, AM am)
        {
            var set = this.emotionPool.Values.Where(emo => emo.GetCause(am).EventName.Match(cause));
            return set.MaxValue(emo => emo.Intensity);
        }

        public override string ToString()
        {
            return $"Mood: {this.mood} Emotions: {this.emotionPool}";
        }

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("Mood", mood.MoodValue);
            dataHolder.SetValue("initialTick", mood.InitialTick);
            dataHolder.SetValue("EmotionalPool", emotionPool.Values.ToArray());
            dataHolder.SetValue("AppraisalConfiguration", this.appraisalConfiguration);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            if (emotionPool == null)
                emotionPool = new Dictionary<string, ActiveEmotion>();
            else
                emotionPool.Clear();

            if (mood == null)
                mood = new Mood();

            this.appraisalConfiguration = dataHolder.GetValue<EmotionalAppraisalConfiguration>("AppraisalConfiguration");
            if (this.appraisalConfiguration == null)
                this.appraisalConfiguration = new EmotionalAppraisalConfiguration();

            mood.SetMoodValue(dataHolder.GetValue<float>("Mood"), this.appraisalConfiguration);
            mood.SetTick0Value(dataHolder.GetValue<ulong>("initialTick"));
            context.PushContext();
            {
                context.Context = (ulong)0; //Tick

                var emotions = dataHolder.GetValue<ActiveEmotion[]>("EmotionalPool");
                foreach (var emotion in emotions)
                {
                    emotionPool.Add(calculateHashString(emotion), emotion);
                }
            }
            context.PopContext();
        }
    }
}
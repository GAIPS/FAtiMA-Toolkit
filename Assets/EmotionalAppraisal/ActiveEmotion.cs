using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutobiographicMemory;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisal.OCCModel;
using SerializationUtilities;
using WellFormedNames;
using Utilities;

namespace EmotionalAppraisal
{
	/// <summary>
	///  Represents an Emotion with intensity that is active in the agent's
	///  emotional state, i.e, the character is feeling the emotion.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves
	[Serializable]
	internal class ActiveEmotion : IActiveEmotion, ICustomSerialization
	{
		private float intensityATt0;
		private ulong tickATt0;
        
        public uint CauseId { get; private set; }

		public Name Target{ get; private set; }

        public Name EventName
        {
            get;
            private set;
        }

        public string EmotionType { get; private set; }

		public EmotionValence Valence { get; private set; }

		public IEnumerable<string> AppraisalVariables { get; private set; }

		public bool InfluenceMood { get; private set; }
                
        private int m_decay;
		public int Decay
		{
			get { return m_decay; }
			set { m_decay = value<0?0:(value>10?10:value);}
		}

		public int Threshold
		{
			get;
			set;
		}

		public float Intensity
		{
			get;
			private set;
		}

		public float Potential
		{
			get
			{
				return this.Intensity + this.Threshold;
			}
		}

		private void SetIntensity(float value, ulong tickStamp)
		{
			value -= Threshold;
			value = value < 0 ? 0 : (value > 10 ? 10 : value);
			this.intensityATt0 = this.Intensity = value;
			this.tickATt0 = tickStamp;
		}

		/// <summary>
		/// Creates a new ActiveEmotion
		/// </summary>
		/// <param name="emotion">the BaseEmotion that is the base for this ActiveEmotion</param>
		/// <param name="potential">the potential for the intensity of the emotion</param>
		/// <param name="threshold">the threshold for the specific emotion</param>
		/// <param name="decay">the decay rate for the specific emotion</param>
		public ActiveEmotion(IEmotion emotion, float potential, int threshold, int decay, ulong tickStamp)
		{
			this.EmotionType = emotion.EmotionType;
			this.Valence = emotion.Valence;
			this.AppraisalVariables = emotion.AppraisalVariables.ToArray();
			this.InfluenceMood = emotion.InfluenceMood;
			this.CauseId = emotion.CauseId;
			this.Target = emotion.Target;
            this.EventName = emotion.EventName;
            this.Threshold = threshold;
			this.Decay = decay;
			SetIntensity(potential,tickStamp);
		}

        
        public ActiveEmotion(EmotionDTO emotionDTO, AM am, int threshold, int decay)
	    {
	        var occType = OCCEmotionType.Parse(emotionDTO.Type);
            if(occType == null)
                throw new Exception("Unknown emotion type");
            this.EmotionType = occType.Name;
	        this.Valence = occType.Valence;
	        this.AppraisalVariables = occType.AppraisalVariables.ToArray();
	        this.InfluenceMood = occType.InfluencesMood;
	        this.CauseId = emotionDTO.CauseEventId;
            var causeEvent = am.RecallEvent(this.CauseId);
            this.EventName = causeEvent.EventName;
            this.Target = (Name)emotionDTO.Target; //TODO: handle direction correctly
	        this.Threshold = threshold;
	        this.Decay = decay;
	        this.Intensity = emotionDTO.Intensity;
        }

		/// <summary>
		/// Decays the emotion according to the system's time
		/// </summary>
		/// <returns>the intensity of the emotion after being decayed</returns>
		internal void DecayEmotion(EmotionalAppraisalConfiguration configuration, ulong tick)
		{
			var delta = tick - tickATt0;
			double lambda = Math.Log(configuration.HalfLifeDecayConstant) / configuration.EmotionalHalfLifeDecayTime;
			float decay = (float)Math.Exp(lambda * this.Decay * delta);
			Intensity = intensityATt0 * decay;
		}

		/// <summary>
		/// Reforces the intensity of the emotion by a given potential
		/// </summary>
		/// <param name="potential">the potential for the reinformcement of the emotion's intensity</param>
		public void ReforceEmotion(float potential)
		{
			this.Intensity = (float)Math.Log(Math.Exp(this.Potential) + Math.Exp(potential));
		}

		public bool IsRelevant
		{
			get
			{
				return this.Intensity > 0.1f;
			}
		}

		public override int GetHashCode()
		{
			return AppraisalVariables.Aggregate(CauseId.GetHashCode(), (h, s) => h ^ s.GetHashCode());
		}

		public override bool Equals(object obj)
		{
			var em = obj as IEmotion;
			if (em == null)
				return false;

			if (CauseId != em.CauseId)
				return false;

			return new HashSet<string>(AppraisalVariables).SetEquals(em.AppraisalVariables);
		}

		public IBaseEvent GetCause(AM am)
		{
			return am.RecallEvent(CauseId);
		}

		public string ToString(AM am)
		{
			StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
			builder.AppendFormat("{0}: {1}", EmotionType, am.RecallEvent(CauseId).EventName);
			if (this.Target != null)
				builder.AppendFormat(" {0}", this.Target);
            if (this.EventName != null)
                builder.AppendFormat(" {0}", this.EventName);
            if (this.Target != null)
                builder.AppendFormat(" {0}", this.Target);

            var result = builder.ToString();
			builder.Length = 0;
			ObjectPool<StringBuilder>.Recycle(builder);
			return result;
		}

	    public EmotionDTO ToDto(AM am)
	    {
	        return new EmotionDTO
	        {
                Type = this.EmotionType,
                Intensity = this.Intensity,
                Target = this.Target == null ? Name.NIL_STRING : this.Target.ToString(),
                CauseEventId =  this.CauseId, 
                CauseEventName = am.RecallEvent(this.CauseId).EventName.ToString(),
	        };
	    }

	
		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("Intensity", Intensity);
			dataHolder.SetValue("Decay", Decay);
			dataHolder.SetValue("Threshold", Threshold);
			dataHolder.SetValue("CauseId", CauseId);
			if (Target != null)
				dataHolder.SetValue("Direction", Target.ToString());
            if (EventName != null)
                dataHolder.SetValue("EventName", EventName.ToString());
            if (Target != null)
                dataHolder.SetValue("Target", Target);

            dataHolder.SetValue("EmotionType", EmotionType);
			dataHolder.SetValue("Valence", Valence);
			dataHolder.SetValue("AppraisalVariables", AppraisalVariables.ToArray());
			dataHolder.SetValue("InfluenceMood", InfluenceMood);
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			Decay = dataHolder.GetValue<int>("Decay");
			Threshold = dataHolder.GetValue<int>("Threshold");
			CauseId = dataHolder.GetValue<uint>("CauseId");
            var evtName = dataHolder.GetValue<string>("EventName");
            Target = (Name)dataHolder.GetValue<string>("Target");
            EventName = !string.IsNullOrEmpty(evtName) ? Name.BuildName(evtName) : null;
           
            EmotionType = dataHolder.GetValue<string>("EmotionType");
			Valence = dataHolder.GetValue<EmotionValence>("Valence");
			AppraisalVariables = dataHolder.GetValue<string[]>("AppraisalVariables");
			InfluenceMood = dataHolder.GetValue<bool>("InfluenceMood");
            this.intensityATt0 = this.Intensity = dataHolder.GetValue<float>("Intensity");
           
			if(!(context.Context is ulong))
				throw new Exception("Unable to deserialize Active Emotion. Invalid serialization context.");
			this.tickATt0 = (ulong)context.Context;
		}
	}
}

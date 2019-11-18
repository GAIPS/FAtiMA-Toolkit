using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutobiographicMemory;
using EmotionalAppraisal.DTOs;
using WellFormedNames;
using Utilities;
using System;

namespace EmotionalAppraisal
{
	/// <summary>
	/// Represents an emotion, which is an instance of a particular Emotion Type
	/// </summary>
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	public abstract class BaseEmotion : IEmotion
	{
		private float potentialValue = 0;

		public uint CauseId { get; protected set; }

		public Name Target
		{
			get;
			protected set;
		}

        public Name EventName
        {
            get;
            protected set;
        }

        public float Potential
		{
			get
			{
				return potentialValue;
			}
			protected set
			{
				potentialValue = value < 0 ? 0 : value;
			}
		}

		public string EmotionType
		{
			get;
			protected set;
		}

		public EmotionValence Valence
		{
			get;
			protected set;
		}

		public IEnumerable<string> AppraisalVariables
		{
			get;
			protected set;
		}

		public bool InfluenceMood
		{
			get;
			protected set;
		}


        /// <summary>
        /// Creates a new BasicEmotion
        /// </summary>
        /// <param name="type">the type of the Emotion</param>
        /// <param name="potential">the potential value for the intensity of the emotion</param>
        /// <param name="cause">the event that caused the emotion</param>
        /// <param name="direction">if the emotion is targeted to someone (ex: angry with Luke), this parameter specifies the target</param>
        protected BaseEmotion(string type, EmotionValence valence, IEnumerable<string> appraisalVariables, float potential, bool influencesMood, uint causeId, Name direction, Name eventName)
		{
			this.EmotionType = type;
			this.Valence = valence;
			this.AppraisalVariables = appraisalVariables;
			this.Potential = potential;

			this.CauseId = causeId;
			this.Target = direction;
            this.EventName = eventName;
			this.InfluenceMood = influencesMood;
            
		}

		protected BaseEmotion(string type, EmotionValence valence, IEnumerable<string> appraisalVariables, float potential, bool influencesMood, uint causeId, Name eventName) :
			this(type, valence, appraisalVariables, potential, influencesMood, causeId, null, eventName)
		{
		}
        
		/// <summary>
		/// Clone constructor
		/// </summary>
		/// <param name="other">the emotion to clone</param>
		protected BaseEmotion(BaseEmotion other)
		{
			this.EmotionType = other.EmotionType;
			this.Valence = other.Valence;
			this.AppraisalVariables = other.AppraisalVariables.ToArray();
			this.Potential = other.Potential;
			this.InfluenceMood = other.InfluenceMood;
			this.CauseId = other.CauseId;
			this.Target = other.Target;
            this.EventName = other.EventName;
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

		public void IncreatePotential(float delta)
		{
			this.Potential += delta;
			if (this.Potential < 0)
				this.Potential = 0;
		}

		public IBaseEvent GetCause(AM am)
		{
			return am.RecallEvent(CauseId);
		}

		public string ToString(AM am)
		{
			StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
			builder.AppendFormat("{0}: {1}", EmotionType,am.RecallEvent(CauseId).EventName);
			if (this.Target != null)
				builder.AppendFormat(" {0}", this.Target);
            if (this.EventName != null)
                builder.AppendFormat(" {0}", this.EventName);

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
                Intensity = this.Potential,
                Target = this.Target?.ToString(),
                CauseEventId = this.CauseId,
                CauseEventName = am.RecallEvent(this.CauseId).EventName.ToString()
            };
        }
    }
}

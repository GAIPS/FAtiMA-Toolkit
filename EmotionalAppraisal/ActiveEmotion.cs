using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutobiographicMemory;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisal.OCCModel;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;
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

		public Name Direction{ get; private set; }

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
			this.Direction = emotion.Direction;
            this.Threshold = threshold;
			this.Decay = decay;
			SetIntensity(potential,tickStamp);
		}


        //TODO: Discuss with Pedro this hierarchy. Problem: ActiveEmotion might be a bit too tied to OCCEmotion
	    public ActiveEmotion(EmotionDTO emotionDTO, int threshold, int decay)
	    {
	        var occType = OCCEmotionType.Parse(emotionDTO.Type);
            if(occType == null)
                throw new Exception("Unknown emotion type");
            this.EmotionType = occType.Name;
	        this.Valence = occType.Valence;
	        this.AppraisalVariables = occType.AppraisalVariables.ToArray();
	        this.InfluenceMood = occType.InfluencesMood;
	        this.CauseId = emotionDTO.CauseEventId;
	        this.Direction = null; //TODO: handle direction correctly
	        this.Threshold = threshold;
	        this.Decay = decay;
	        this.Intensity = emotionDTO.Intensity;
	    }

		/// <summary>
		/// Decays the emotion according to the system's time
		/// </summary>
		/// <returns>the intensity of the emotion after being decayed</returns>
		internal void DecayEmotion(EmotionalAppraisalAsset parent)
		{
			var delta = parent.Tick - tickATt0;
			double lambda = Math.Log(parent.HalfLifeDecayConstant) /parent.EmotionalHalfLifeDecayTime;
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
			if (this.Direction != null)
				builder.AppendFormat(" {0}", Direction);

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
                CauseEventId =  this.CauseId, 
                CauseEventName = am.RecallEvent(this.CauseId).EventName.ToString(),
	        };
	    }

		//public void GetObjectData(ISerializationData dataHolder)
		//{
		//	dataHolder.SetValue("Intensity", Intensity);
		//	dataHolder.SetValue("Decay", Decay);
		//	dataHolder.SetValue("Threshold", Threshold);
		//	dataHolder.SetValue("CauseId", CauseId);
		//	if (Direction != null)
		//		dataHolder.SetValue("Direction", Direction.ToString());
		//	dataHolder.SetValue("EmotionType", EmotionType);
		//	dataHolder.SetValue("Valence", Valence);
		//	dataHolder.SetValue("AppraisalVariables", AppraisalVariables.ToArray());
		//	dataHolder.SetValue("InfluenceMood", InfluenceMood);
		//}

		//public ActiveEmotion(ISerializationData dataHolder, ulong tickStamp)
		//{
		//	Decay = dataHolder.GetValue<int>("Decay");
		//	Threshold = dataHolder.GetValue<int>("Threshold");
		//	CauseId = dataHolder.GetValue<uint>("CauseId");
		//	var dir = dataHolder.GetValue<string>("Direction");
		//	Direction = !string.IsNullOrEmpty(dir) ? Name.BuildName(dir) : null;
		//	EmotionType = dataHolder.GetValue<string>("EmotionType");
		//	Valence = dataHolder.GetValue<EmotionValence>("Valence");
		//	AppraisalVariables = dataHolder.GetValue<string[]>("AppraisalVariables");
		//	InfluenceMood = dataHolder.GetValue<bool>("InfluenceMood");
		//	this.intensityATt0 = this.Intensity = dataHolder.GetValue<float>("Intensity");
  //          this.tickATt0 = tickStamp;
  //      }

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("Intensity", Intensity);
			dataHolder.SetValue("Decay", Decay);
			dataHolder.SetValue("Threshold", Threshold);
			dataHolder.SetValue("CauseId", CauseId);
			if (Direction != null)
				dataHolder.SetValue("Direction", Direction.ToString());
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
			var dir = dataHolder.GetValue<string>("Direction");
			Direction = !string.IsNullOrEmpty(dir) ? Name.BuildName(dir) : null;
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

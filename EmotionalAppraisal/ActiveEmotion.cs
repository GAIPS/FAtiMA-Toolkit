using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory.Interfaces;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisal
{
	/// <summary>
	///  Represents an Emotion with intensity that is active in the agent's
	///  emotional state, i.e, the character is feeling the emotion.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves
	public class ActiveEmotion : IEmotion, ICustomSerialization
	{
		private float intensityATt0;
		private float deltaTimeT0;

		public IEventRecord Cause { get; private set; }

		public Name Direction
		{
			get; private set; }

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

		private void SetIntencity(float value)
		{
			value -= Threshold;
			value = value < -10 ? -10 : (value > 10 ? 10 : value);
			this.intensityATt0 = this.Intensity = value;
			this.deltaTimeT0 = 0;
		}

		/// <summary>
		/// Creates a new ActiveEmotion
		/// </summary>
		/// <param name="emotion">the BaseEmotion that is the base for this ActiveEmotion</param>
		/// <param name="potential">the potential for the intensity of the emotion</param>
		/// <param name="threshold">the threshold for the specific emotion</param>
		/// <param name="decay">the decay rate for the specific emotion</param>
		public ActiveEmotion(IEmotion other, float potential, int threshold, int decay)
		{
			this.EmotionType = other.EmotionType;
			this.Valence = other.Valence;
			this.AppraisalVariables = other.AppraisalVariables.ToArray();
			this.InfluenceMood = other.InfluenceMood;
			this.Cause = other.Cause;
			this.Direction = other.Direction;

			this.Threshold = threshold;
			this.Decay = decay;
			SetIntencity(potential);
		}

		/// <summary>
		/// Decays the emotion according to the system's time
		/// </summary>
		/// <returns>the intensity of the emotion after being decayed</returns>
		internal void DecayEmotion(float elapsedTime, EmotionalAppraisalAsset parentAsset)
		{
			this.deltaTimeT0 += elapsedTime;
			double lambda = Constants.HalfLifeDecayConstant/parentAsset.EmotionalHalfLifeDecayTime;
			float decay = (float)Math.Exp(lambda * this.Decay * deltaTimeT0);
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

		public void GetObjectData(ISerializationData dataHolder)
		{
			dataHolder.SetValue("Intensity", Intensity);
			dataHolder.SetValue("Decay",Decay);
			dataHolder.SetValue("Threshold", Threshold);
			dataHolder.SetValue("Cause",Cause);
			if(Direction!=null)
				dataHolder.SetValue("Direction", Direction.ToString());
			dataHolder.SetValue("EmotionType",EmotionType);
			dataHolder.SetValue("Valence",Valence);
			dataHolder.SetValue("AppraisalVariables", AppraisalVariables.ToArray());
			dataHolder.SetValue("InfluenceMood",InfluenceMood);
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			Intensity = intensityATt0 = dataHolder.GetValue<float>("Intensity");
			deltaTimeT0 = 0;
			Decay = dataHolder.GetValue<int>("Decay");
			Threshold = dataHolder.GetValue<int>("Threshold");
			Cause = dataHolder.GetValue<IEventRecord>("Cause");
			var dir = dataHolder.GetValue<string>("Direction");
			Direction = !string.IsNullOrEmpty(dir) ? Name.BuildName(dir) : null;
			EmotionType = dataHolder.GetValue<string>("EmotionType");
			Valence = dataHolder.GetValue<EmotionValence>("Valence");
			AppraisalVariables = dataHolder.GetValue<string[]>("AppraisalVariables");
			InfluenceMood = dataHolder.GetValue<bool>("InfluenceMood");
		}
	}
}

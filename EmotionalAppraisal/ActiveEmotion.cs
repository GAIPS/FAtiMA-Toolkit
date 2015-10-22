using System;

namespace EmotionalAppraisal
{
	/// <summary>
	///  Represents an Emotion with intensity that is active in the agent's
	///  emotional state, i.e, the character is feeling the emotion.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves
	public class ActiveEmotion : BaseEmotion
	{
		private float intensityATt0;
		private float deltaTimeT0;

		public int Decay
		{
			get;
			set;
		}

		public int Threashold
		{
			get;
			set;
		}

		public float Intencity
		{
			get;
			private set;
		}

		public new float Potential
		{
			get
			{
				return this.Intencity + this.Threashold;
			}
		}

		public Type BaseEmotionType
		{
			get;
			protected set;
		}

		internal void SetIntencity(float value)
		{
			value -= Threashold;
			value = value < -10 ? -10 : (value > 10 ? 10 : value);
			this.intensityATt0 = this.Intencity = value;
			this.deltaTimeT0 = 0;
		}

		/// <summary>
		/// Creates a new ActiveEmotion
		/// </summary>
		/// <param name="emotion">the BaseEmotion that is the base for this ActiveEmotion</param>
		/// <param name="potential">the potential for the intensity of the emotion</param>
		/// <param name="threshold">the threshold for the specific emotion</param>
		/// <param name="decay">the decay rate for the specific emotion</param>
		public ActiveEmotion(BaseEmotion emotion, float potential, int threshold, int decay) : base(emotion)
		{
			this.Threashold = threshold;
			this.Decay = decay;
			SetIntencity(potential);
			this.BaseEmotionType = emotion.GetType();
		}

		/// <summary>
		/// Decays the emotion according to the system's time
		/// </summary>
		/// <returns>the intensity of the emotion after being decayed</returns>
		internal void DecayEmotion(float elapsedTime)
		{
			this.deltaTimeT0 += elapsedTime;
			float decay = (float)Math.Exp(-EmotionalParameters.EmotionDecayFactor * this.Decay * deltaTimeT0);
			Intencity = intensityATt0 * decay;
		}

		/// <summary>
		/// Reforces the intensity of the emotion by a given potential
		/// </summary>
		/// <param name="potential">the potential for the reinformcement of the emotion's intensity</param>
		public void ReforceEmotion(float potential)
		{
			this.Intencity = (float)Math.Log(Math.Exp(this.Potential) + Math.Exp(potential));
		}

		public bool IsRelevant
		{
			get
			{
				return this.Intencity > 0.1f;
			}
		}
	}
}

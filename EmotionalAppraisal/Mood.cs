using GAIPS.Serialization.Attributes;
using System;

namespace EmotionalAppraisal
{
	/// <summary>
	/// Class that represents a agent's mood.
	/// </summary>
	public class Mood
	{
		[SerializeField]
		private float intensityATt0;
		[SerializeField]
		private float deltaTimeT0;
		[SerializeField]
		private float intensity;

		/// <summary>
		/// value that represents mood.
		/// Mood is ranged between [-10;10], a negative value represents a bad mood,
		/// a positive value represents good mood and values near 0 represent a neutral mood
		/// </summary>
		public float MoodValue
		{
			get
			{
				return this.intensity;
			}
		}

		public void SetMoodValue(float value)
		{
			value = value < -10 ? -10 : (value > 10 ? 10 : value);
			if (Math.Abs(value) < EmotionalParameters.MinimumMoodValue)
				value = 0;

			this.intensityATt0 = this.intensity = value;
			this.deltaTimeT0 = 0;
		}

		public Mood()
		{
			SetMoodValue(0);
		}

		/// <summary>
		/// Decays the mood according to the agent's simulated time
		/// </summary>
		/// <returns>the mood's intensity after being decayed</returns>
		public void DecayMood(float elapsedTime)
		{
			if (this.intensityATt0 == 0)
			{
				this.intensity = 0;
				return;
			}

			this.deltaTimeT0 += elapsedTime;
			intensity = (float)(this.intensityATt0 * Math.Exp(-EmotionalParameters.MoodDecayFactor*deltaTimeT0));
			if(Math.Abs(this.intensity) < EmotionalParameters.MinimumMoodValue)
			{
				this.intensity = this.intensityATt0 = 0;
				this.deltaTimeT0 = 0;
			}
		}

		/// <summary>
		/// Updates the character's mood when a given emotion is "felt" by the character. 
		/// </summary>
		/// <param name="emotion">the ActiveEmotion that will influence the agent's current mood</param>
		public void UpdateMood(ActiveEmotion emotion)
		{
			if (!emotion.InfluenceMood)
				return;

			float scale = (float)emotion.Valence;
			SetMoodValue(this.intensity + scale * (emotion.Intencity * EmotionalParameters.EmotionInfluenceOnMood));
		}
	}
}

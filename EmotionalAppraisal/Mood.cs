using System;

namespace EmotionalAppraisal
{
	/// <summary>
	/// Class that represents a agent's mood.
	/// </summary>
	public class Mood
	{
		private float intensityATt0;
		private ITime timeInstance;
		private long t0;
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
			set
			{
				value = value < -10 ? -10 : (value > 10 ? 10 : value);
				if (Math.Abs(value) < EmotionalParameters.MinimumMoodValue)
					value = 0;

				this.intensityATt0 = this.intensity = value;
				this.t0 = this.timeInstance.SimulatedTime;
			}
		}

		public Mood(ITime time)
		{
			this.timeInstance = time;
			MoodValue = 0;
		}

		/// <summary>
		/// Decays the mood according to the agent's simulated time
		/// </summary>
		/// <returns>the mood's intensity after being decayed</returns>
		public float DecayMood(ITime time)
		{
			if (this.intensityATt0 == 0)
			{
				this.intensity = 0;
				return 0;
			}

			float deltaT = (time.SimulatedTime - this.t0) / 1000f;
			intensity = (float)(this.intensityATt0 * Math.Exp(-EmotionalParameters.MoodDecayFactor*deltaT));
			if(Math.Abs(this.intensity) < EmotionalParameters.MinimumMoodValue)
			{
				this.intensity = 0;
				this.intensityATt0 = 0;
				this.t0 = time.SimulatedTime;
			}
			return this.intensity;
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
			MoodValue = this.intensity + scale*(emotion.Intencity*EmotionalParameters.EmotionInfluenceOnMood);
		}
	}
}

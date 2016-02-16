using System;

namespace EmotionalAppraisal
{
	/// <summary>
	/// Class that represents a agent's mood.
	/// </summary>
	internal class Mood
	{
		private float _intensityATt0;
		private float _deltaTimeT0;
		private float _intensity;
	    
		/// <summary>
		/// value that represents mood.
		/// Mood is ranged between [-10;10], a negative value represents a bad mood,
		/// a positive value represents good mood and values near 0 represent a neutral mood
		/// </summary>
		public float MoodValue
		{
			get
			{
				return this._intensity;
			}
		}

		public void SetMoodValue(float value,EmotionalAppraisalAsset parent)
		{
			value = value < -10 ? -10 : (value > 10 ? 10 : value);
			if (Math.Abs(value) < parent.MinimumMoodValueForInfluencingEmotions)
				value = 0;

			this._intensityATt0 = this._intensity = value;
			this._deltaTimeT0 = 0;
		}

		internal Mood()
		{
			this._intensityATt0 = this._intensity = 0;
			this._deltaTimeT0 = 0;
		}

		/// <summary>
		/// Decays the mood according to the agent's simulated time
		/// </summary>
		/// <returns>the mood's intensity after being decayed</returns>
		public void DecayMood(float elapsedTime, EmotionalAppraisalAsset parent)
		{
			if (this._intensityATt0 == 0)
			{
				this._intensity = 0;
				return;
			}

			this._deltaTimeT0 += elapsedTime;
			double lambda = Math.Log(parent.HalfLifeDecayConstant)/parent.MoodHalfLifeDecayTime;
			_intensity = (float)(this._intensityATt0 * Math.Exp(lambda*_deltaTimeT0));
			if(Math.Abs(this._intensity) < parent.MinimumMoodValueForInfluencingEmotions)
			{
				this._intensity = this._intensityATt0 = 0;
				this._deltaTimeT0 = 0;
			}
		}

		/// <summary>
		/// Updates the character's mood when a given emotion is "felt" by the character. 
		/// </summary>
		/// <param name="emotion">the ActiveEmotion that will influence the agent's current mood</param>
		public void UpdateMood(ActiveEmotion emotion, EmotionalAppraisalAsset parent)
		{
			if (!emotion.InfluenceMood)
				return;

			float scale = (float)emotion.Valence;
			SetMoodValue(this._intensity + scale * (emotion.Intensity * parent.EmotionInfluenceOnMoodFactor),parent);
		}
	}
}

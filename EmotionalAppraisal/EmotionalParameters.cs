namespace EmotionalAppraisal
{
	public static class EmotionalParameters
	{	
		/// <summary>
		/// Constant value that defines how fast should a emotion decay over time.
		/// This value is adjusted so that the slowest decaying emotions takes
		/// aproximately 15 seconds to decay to half of the original intensity, 
		/// the fastest decaying emotions take 4 seconds, and the normal ones takes
		/// 7 seconds.
		/// </summary>
		public const float EmotionDecayFactor = 0.02f;
	
		/// <summary>
		/// Constant value that defines how fast should mood decay over time.
		/// This value is adjusted so that mood decays 3 times slower
		/// than the slowest decaying emotion in order to represent 
		/// a longer persistence and duration of mood over emotions.
		/// So, it takes aproximately 60 seconds for the mood to decay to half
		/// of the initial value.
		/// </summary>
		public const float MoodDecayFactor = 0.01f;

		/// <summary>
		/// Defines how strong is the influence of the emotion's intensity
		/// on the character's mood. Since we don't want the mood to be very
		/// volatile, we only take into account 30% of the emotion's intensity
		/// </summary>
		public const float EmotionInfluenceOnMood = 0.3f;
	
		/// <summary>
		/// Defines how strong is the influence of the current mood 
		/// in the intensity of the emotion. We don't want the influence
		/// of mood to be that great, so we only take into account 30% of 
		/// the mood's value
		/// </summary>
		public const float MoodInfluenceOnEmotion = 0.3f;
	
		/// <summary>
		/// Defines the minimum absolute value that mood must have,
		/// in order to be considered for influencing emotions. At the 
		/// moment, values of mood ranged in ]-0.5;0.5[ are considered
		/// to be neutral moods that do not infuence emotions
		/// </summary>
		public const float MinimumMoodValue = 0.5f;
	}
}

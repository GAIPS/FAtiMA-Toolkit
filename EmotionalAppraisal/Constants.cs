using System;

namespace EmotionalAppraisal
{
	public static class Constants
	{
		public const float MinimumDecayTime = 1;

		/// <summary>
		/// The half-life base decay for the exponential decay lambda calculation.
		/// To calculate the lambda, divide this constant by the required half-life time.
		/// </summary>
		public static readonly double HalfLifeDecayConstant = Math.Log(0.5);

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

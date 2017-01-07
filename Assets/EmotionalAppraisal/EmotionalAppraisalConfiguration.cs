using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmotionalAppraisal
{
    [Serializable]
    public class EmotionalAppraisalConfiguration
    {
        /// <summary>
        /// The half-life base decay for the exponential decay lambda calculation.
        /// To calculate the lambda, divide this constant by the required half-life time.
        /// </summary>
        /// @hideinitializer
        public double HalfLifeDecayConstant { get; private set; } = 0.5;

        /// <summary>
        /// Defines how strong is the influence of the emotion's intensity
        /// on the character's mood. Since we don't want the mood to be very
        /// volatile, we only take into account 30% of the emotion's intensity
        /// </summary>
        /// @hideinitializer
        public float EmotionInfluenceOnMoodFactor { get; private set; } = 0.3f;

        /// <summary>
        /// Defines how strong is the influence of the current mood 
        /// in the intensity of the emotion. We don't want the influence
        /// of mood to be that great, so we only take into account 30% of 
        /// the mood's value
        /// </summary>
        /// @hideinitializer
        public float MoodInfluenceOnEmotionFactor { get; private set; } = 0.3f;

        /// <summary>
        /// Defines the minimum absolute value that mood must have,
        /// in order to be considered for influencing emotions. At the 
        /// moment, values of mood ranged in ]-0.5;0.5[ are considered
        /// to be neutral moods that do not infuence emotions
        /// </summary>
        /// @hideinitializer
        public double MinimumMoodValueForInfluencingEmotions { get; private set; } = 0.5;

        /// <summary>
        /// Defines how fast a emotion decay over time.
        /// This value is the actual time it takes for an emotion to reach half of its initial intensity
        /// </summary>
        /// @hideinitializer
        public float EmotionalHalfLifeDecayTime { get; private set; } = 15;

        /// <summary>
        /// Defines how fast mood decay over time.
        /// This value is the actual time it takes the mood to reach half of its initial intensity
        /// </summary>
        /// @hideinitializer
        public float MoodHalfLifeDecayTime { get; private set; } = 60;
    }
}

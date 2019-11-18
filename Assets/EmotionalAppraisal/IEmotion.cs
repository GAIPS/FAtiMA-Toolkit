using System.Collections.Generic;
using AutobiographicMemory;
using EmotionalAppraisal.DTOs;
using WellFormedNames;

namespace EmotionalAppraisal
{
	/// <summary>
	/// Interface for every emotion of the Emotional Appraisal Asset
	/// </summary>
	public interface IEmotion
	{
		/// <summary>
		/// The id of the event that triggered this emotion
		/// </summary>
		uint CauseId { get; }

        Name EventName { get; }

        /// <summary>
        /// The target, if any, of the emotion.
        /// Ex. I am angry thowards John.
        /// Ie. The emotion is <b>Anger</b>. The direction is <b>John</b>
        /// </summary>
        Name Target { get; }

		/// <summary>
		/// The potential of this emotion.
		/// Emotion with higher potential, can result in more prominent active emotions
		/// </summary>
		float Potential { get; }

		/// <summary>
		/// The emotion type key
		/// </summary>
		string EmotionType { get; }

		/// <summary>
		/// The valence of this emotion.
		/// </summary>
		EmotionValence Valence { get; }

        IEnumerable<string> AppraisalVariables { get; }

		bool InfluenceMood { get; }

		string ToString(AM am);

		IBaseEvent GetCause(AM am);

	    EmotionDTO ToDto(AM am);
	}
}
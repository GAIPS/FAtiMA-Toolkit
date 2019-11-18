using System;

namespace EmotionalAppraisal.DTOs
{
    /// <summary>
    /// Data Type Object Class for the representation of an Emotion
    /// </summary>
    [Serializable]
    public class EmotionDTO
    {
		/// <summary>
		/// The emotion type key. Used to uniquely identify the emotion type.
		/// </summary>
		public string Type { get; set; }
		/// <summary>
		/// The current intencity of the emotion
		/// </summary>
		public float Intensity { get; set; }
        /// <summary>
		/// The target of the emotion
		/// </summary>
		public string Target { get; set; }
		/// <summary>
		/// The event that caused the expression of this emotion
		/// </summary>
		public uint CauseEventId { get; set; }
		/// <summary>
		/// The string representation of the event that caused this emotion
		/// </summary>
		public string CauseEventName { get; set; }
    }
}

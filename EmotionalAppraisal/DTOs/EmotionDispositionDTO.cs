namespace EmotionalAppraisal.DTOs
{
	/// <summary>
	/// Data Type Object Class for the representation of the Emotional Dispositions
	/// </summary>
	public class EmotionDispositionDTO 
	{
		/// <summary>
		/// The emotion type key. Used to uniquely identify the emotion type.
		/// </summary>
		public string Emotion { get; set; }
		/// <summary>
		/// The amount of decay the emotion is subjected to at each update.
		/// The higher the value, the faster the emotion disipates
		/// </summary>
        public int Decay { get; set; }
		/// <summary>
		/// The activation threshold for this emotion.
		/// Lower thresholds allow the emotion to be activated at lower intencities,
		/// while higher threshold do the oposite.
		/// </summary>
        public int Threshold { get; set; }
	}
}

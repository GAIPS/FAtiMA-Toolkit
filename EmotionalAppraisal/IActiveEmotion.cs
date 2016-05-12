namespace EmotionalAppraisal
{
	/// <summary>
	/// Interface for every active emotion of the Emotional Appraisal Asset
	/// </summary>
	public interface IActiveEmotion : IEmotion
	{
		/// <summary>
		/// The current intensity of the emotion.
		/// If it drops too much, the emotion will no longer be considered active
		/// </summary>
		float Intensity { get; }
	}
}
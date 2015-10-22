using GAIPS.Serialization.Attributes;
namespace EmotionalAppraisal
{
	/// <summary>
	/// Represents the character's emotional disposition (Emotional Threshold + Decay Rate) towards an Emotion Type.
	/// 
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	/// </summary>
	public class EmotionDisposition
	{
		[SerializeField]
		private string m_emotion;

		[SerializeField]
		private int m_decay;

		[SerializeField]
		private int m_threshold;

		public string Emotion
		{
			get { return m_emotion; }
		}

		public int Decay
		{
			get { return m_decay; }
		}

		public int Threshold
		{
			get { return m_threshold; }
		}

		public EmotionDisposition(string emotion, int decay, int threashold)
		{
			m_emotion = emotion;
			m_decay = decay;
			m_threshold = threashold;
		}

		public override string ToString()
		{
			return string.Format("Emotion: {0}, Threashold: {2}, Decay: {1}", m_emotion,m_threshold,m_decay);
		}
	}
}

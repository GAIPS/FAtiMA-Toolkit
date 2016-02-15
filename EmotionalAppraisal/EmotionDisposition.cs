using System;

namespace EmotionalAppraisal
{
	/// <summary>
	/// Represents the character's emotional disposition (Emotional Threshold + Decay Rate) towards an Emotion Type.
	/// 
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	/// </summary>
	[Serializable]
	public class EmotionDisposition
	{
		public string Emotion { get; private set; }

		public int Decay { get; private set; }

		public int Threshold { get; private set; }

        public EmotionDisposition(string emotion, int decay, int threshold)
        {
            Emotion = emotion;
            Decay = decay;
            Threshold = threshold;
        }

        public EmotionDisposition(EmotionDispositionDTO dispDto)
		{
		    Emotion = dispDto.Emotion;
			Decay = dispDto.Decay;
			Threshold = dispDto.Threshold;
		}

		public override string ToString()
		{
			return string.Format("Emotion: {0}, Threshold: {2}, Decay: {1}", Emotion,Threshold,Decay);
		}

	    public EmotionDispositionDTO ToDto()
	    {
	        return new EmotionDispositionDTO
	        {
	            Decay = this.Decay,
	            Emotion = this.Emotion,
	            Threshold = this.Threshold
	        };
	    }
	}
}

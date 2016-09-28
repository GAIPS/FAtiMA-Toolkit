namespace IntegratedAuthoringToolWF.TTSEngines
{
	public sealed class BakedTTS
	{
		public byte[] waveStreamData;
		public VisemeSpan[] visemes;
	}

	public struct VisemeSpan
	{
		public Viseme viseme;
		public double duration;
	}
}
using System.Collections.Generic;

namespace IntegratedAuthoringToolWF.TTSEngines
{
	public interface ITextToSpeechEngine
	{
		string Name { get; }

		IEnumerable<IVoice> GetAvailableVoices();
	}
}
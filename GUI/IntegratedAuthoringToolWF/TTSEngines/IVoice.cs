using System;
using System.Speech.Synthesis;

namespace IntegratedAuthoringToolWF.TTSEngines
{
	public interface IVoice
	{
		string Name { get; }
		VoiceGender Gender { get; }
		VoiceAge Age { get; }
		string Culture { get; }

		TextToSpeechEngine ParentEngine { get; }

		BakedTTS BakeTTS(string text, double rate, int pitch);
		VoicePlayer BuildPlayer(string text, double rate, int pitch);
	}
}
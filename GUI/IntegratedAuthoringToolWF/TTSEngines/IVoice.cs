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

		ITextToSpeechEngine ParentEngine { get; }

		bool IsSpeaking { get; }
		void Speak(string text, double playbackRate, int pitchShift, Action onFinished=null);
		void CancelSpeaking();

		BakedTTS BakeSpeechFiles(string text, double playbackRate, int pitchShift);
	}
}
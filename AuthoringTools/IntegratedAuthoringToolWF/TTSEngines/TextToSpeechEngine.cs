using System;
using System.Collections.Generic;
using System.Speech.Synthesis;

namespace IntegratedAuthoringToolWF.TTSEngines
{
	public abstract class TextToSpeechEngine
	{
		public abstract string Name { get; }

		public abstract IEnumerable<IVoice> GetAvailableVoices();

		public BakedTTS BakeTTS(IVoice voice, string text, double rate, int pitch)
		{
			if(voice.ParentEngine != this)
				throw new Exception("not parent engine");

			return BuildTTS(voice, text, rate, pitch);
		}

		public VoicePlayer BuildPlayer(IVoice voice, string text, double rate, int pitch)
		{
			var tts = BakeTTS(voice, text, rate, pitch);
			return tts == null ? null : new VoicePlayer(tts);
		}

		protected abstract BakedTTS BuildTTS(IVoice voice, string text, double rate, int pitch);

		protected abstract class BaseVoice : IVoice
		{
			public string Name { get; protected set; }
			public VoiceGender Gender { get; protected set; }
			public VoiceAge Age { get; protected set; }
			public string Culture { get; protected set; }

			public TextToSpeechEngine ParentEngine { get; }

			protected BaseVoice(TextToSpeechEngine parentEngine)
			{
				this.ParentEngine = parentEngine;
			}

			public BakedTTS BakeTTS(string text, double rate, int pitch)
			{
				return ParentEngine.BakeTTS(this, text, rate, pitch);
			}

			public VoicePlayer BuildPlayer(string text, double rate, int pitch)
			{
				return ParentEngine.BuildPlayer(this, text, rate, pitch);
			}
		}
	}
}
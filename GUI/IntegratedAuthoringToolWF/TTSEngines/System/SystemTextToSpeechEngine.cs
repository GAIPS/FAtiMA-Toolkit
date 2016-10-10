using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;

namespace IntegratedAuthoringToolWF.TTSEngines.System
{
	public sealed class SystemTextToSpeechEngine : TextToSpeechEngine
	{
		private SpeechSynthesizer _synthesizer;
		private PromptBuilder _builder = new PromptBuilder();
		private SystemVoice[] _voices;

		private SystemVoice _currentlyPlaying = null;
		private Prompt _current = null;

		public override string Name => "OS Native";

		public SystemTextToSpeechEngine()
		{
			_synthesizer = new SpeechSynthesizer();

			_voices = _synthesizer.GetInstalledVoices().Select(v => new SystemVoice(v.VoiceInfo,this)).ToArray();
		}

		public override IEnumerable<IVoice> GetAvailableVoices()
		{
			return _voices;
		}

		protected override BakedTTS BuildTTS(IVoice voice, string text, double rate, int pitch)
		{
			var _voice = ((SystemVoice) voice)._voice;

			List<VisemeSpan> visemes = new List<VisemeSpan>();
			EventHandler<VisemeReachedEventArgs> visemeHandler = (o, args) =>
			{
				var v = IntToViseme(args.Viseme);
				var d = args.Duration.TotalSeconds;
				visemes.Add(new VisemeSpan() { viseme = v, duration = d });
			};

			using (var m = new MemoryStream())
			{
				_synthesizer.VisemeReached += visemeHandler;
				UpdateSpeechData(_voice, text, rate, pitch);
				_synthesizer.SetOutputToWaveStream(m);
				_synthesizer.Speak(_builder);
				_synthesizer.SetOutputToDefaultAudioDevice();
				_synthesizer.VisemeReached -= visemeHandler;

				return visemes.Count == 0 ? null : new BakedTTS() { waveStreamData = m.ToArray(), visemes = visemes.ToArray() };
			}
		}

		private void UpdateSpeechData(VoiceInfo voice, string text, double rate, int pitch)
		{
			_builder.ClearContent();

			_builder.StartVoice(voice);
			var prosody = $"<prosody rate=\"{rate}\" pitch=\"{pitch}st\">";
			_builder.AppendSsmlMarkup(prosody);
			_builder.AppendText(text);
			_builder.AppendSsmlMarkup("</prosody>");
			_builder.EndVoice();
		}

		private static Viseme IntToViseme(int value)
		{
			switch (value)
			{
				case 0:
					return Viseme.Silence;
				case 1:
					return Viseme.AxAhUh;
				case 2:
					return Viseme.Aa;
				case 3:
					return Viseme.Ao;
				case 4:
					return Viseme.EyEhAe;
				case 5:
					return Viseme.Er;
				case 6:
					return Viseme.YIyIhIx;
				case 7:
					return Viseme.WUwU;
				case 8:
					return Viseme.Ow;
				case 9:
					return Viseme.Aw;
				case 10:
					return Viseme.Oy;
				case 11:
					return Viseme.Ay;
				case 12:
					return Viseme.H;
				case 13:
					return Viseme.R;
				case 14:
					return Viseme.L;
				case 15:
					return Viseme.SZTs;
				case 16:
					return Viseme.ShChJhZh;
				case 17:
					return Viseme.ThDh;
				case 18:
					return Viseme.FV;
				case 19:
					return Viseme.DTDxN;
				case 20:
					return Viseme.KGNg;
				case 21:
					return Viseme.PBM;
			}

			return Viseme.Unknown;
		}

		private class SystemVoice : BaseVoice
		{
			public readonly VoiceInfo _voice;

			public SystemVoice(VoiceInfo voice, TextToSpeechEngine parent):base(parent)
			{
				_voice = voice;
				Name = _voice.Name;
				Gender = _voice.Gender;
				Age = _voice.Age;
				Culture = _voice.Culture.Name;
			}
		}
	}
}
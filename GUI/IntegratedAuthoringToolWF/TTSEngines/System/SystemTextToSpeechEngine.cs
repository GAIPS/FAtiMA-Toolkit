using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;

namespace IntegratedAuthoringToolWF.TTSEngines.System
{
	public class SystemTextToSpeechEngine : ITextToSpeechEngine
	{
		private SpeechSynthesizer _synthesizer;
		private PromptBuilder _builder = new PromptBuilder();
		private SystemVoice[] _voices;

		private SystemVoice _currentlyPlaying = null;
		private Prompt _current = null;

		public string Name => "OS TTS";

		public SystemTextToSpeechEngine()
		{
			_synthesizer = new SpeechSynthesizer();

			_voices = _synthesizer.GetInstalledVoices().Select(v => new SystemVoice(v.VoiceInfo,this)).ToArray();
		}

		public IEnumerable<IVoice> GetAvailableVoices()
		{
			return _voices;
		}

		private void SpeakVoice(SystemVoice voice, string text, double rate, int pitch)
		{
			_currentlyPlaying?.CancelSpeaking();

			UpdateSpeechData(voice._voice, text,rate,pitch);
			_synthesizer.SetOutputToDefaultAudioDevice();
			_current = _synthesizer.SpeakAsync(_builder);
			_synthesizer.SpeakCompleted += SynthesizerOnSpeakCompleted;
			_currentlyPlaying = voice;
		}

		private void SynthesizerOnSpeakCompleted(object sender, SpeakCompletedEventArgs speakCompletedEventArgs)
		{
			_current = null;
			_synthesizer.SpeakCompleted -= SynthesizerOnSpeakCompleted;
			_currentlyPlaying.DispatchFinish();
			_currentlyPlaying = null;
		}

		private void CancelSpeak(SystemVoice voice)
		{
			if(_current==null)
				return;

			if(_currentlyPlaying!=voice)
				return;

			_synthesizer.SpeakCompleted -= SynthesizerOnSpeakCompleted;
			_synthesizer.SpeakAsyncCancel(_current);
			_current = null;
			_currentlyPlaying.DispatchFinish();
			_currentlyPlaying = null;
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

		private class SystemVoice : IVoice
		{
			public readonly VoiceInfo _voice;

			public SystemVoice(VoiceInfo voice, ITextToSpeechEngine parent)
			{
				_voice = voice;
				ParentEngine = parent;
			}

			public string Name => _voice.Name;
			public VoiceGender Gender => _voice.Gender;
			public VoiceAge Age => _voice.Age;
			public string Culture => _voice.Culture.Name;
			public ITextToSpeechEngine ParentEngine { get; }

			public bool IsSpeaking { get; private set; }
			private Action _callback;

			public void Speak(string text, double playbackRate, int pitchShift, Action onFinished)
			{
				if(IsSpeaking)
					CancelSpeaking();

				_callback = onFinished;
				((SystemTextToSpeechEngine)ParentEngine).SpeakVoice(this,text,playbackRate,pitchShift);
				IsSpeaking = true;
			}

			public void CancelSpeaking()
			{
				if(!IsSpeaking)
					return;

				((SystemTextToSpeechEngine)ParentEngine).CancelSpeak(this);
			}

			public void DispatchFinish()
			{
				IsSpeaking = false;
				_callback?.Invoke();
			}

			public BakedTTS BakeSpeechFiles(string text, double playbackRate, int pitchShift)
			{
				List<VisemeSpan> visemes = new List<VisemeSpan>();
				EventHandler<VisemeReachedEventArgs> visemeHandler = (o, args) =>
				{
					var v = IntToViseme(args.Viseme);
					var d = args.Duration.TotalSeconds;
					visemes.Add(new VisemeSpan() {viseme = v,duration = d});
				};

				var parent = (SystemTextToSpeechEngine) ParentEngine;
				var synt = parent._synthesizer;
				using (var m = new MemoryStream())
				{
					synt.VisemeReached += visemeHandler;
					parent.UpdateSpeechData(_voice, text, playbackRate, pitchShift);
					synt.SetOutputToWaveStream(m);
					synt.Speak(parent._builder);
					synt.SetOutputToDefaultAudioDevice();
					synt.VisemeReached -= visemeHandler;

					return visemes.Count == 0 ? null : new BakedTTS() {waveStreamData = m.GetBuffer(), visemes = visemes.ToArray()};
				}
			}

			private Viseme IntToViseme(int value)
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
		}

		//private void GenerateTTS(string path, DialogueStateActionDTO dto, IProgressBarControler ctrl)
		//{
		//	
		//	
		//}
	}
}
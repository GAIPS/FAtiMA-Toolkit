﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Speech.Synthesis;
using IntegratedAuthoringToolWF.Audio;
using NAudio.Wave;
using Utilities.Json;

namespace IntegratedAuthoringToolWF.TTSEngines.L2F
{
	public sealed class L2FSpeechEngine : TextToSpeechEngine
	{
		private const string HOST_URL = "http://www.l2f.inesc-id.pt/~pfialho";
		private const string TTS_URL = HOST_URL+ "/tts/frameworkProxy.php";
		private const string AUDIO_DL_PREFIX = HOST_URL+"/test/";

		private const float DELAY_BETWEEN_FRASES = 0.5f;

		public override string Name => "L2F";

		private L2FVoice[] _availableVoices;

		public L2FSpeechEngine()
		{
			_availableVoices = new[]
			{
				new L2FVoice("Vicent",VoiceGender.Male, VoiceAge.Adult,"pt",this),
				new L2FVoice("Viriato",VoiceGender.Male, VoiceAge.Adult,"pt",this),
				new L2FVoice("Violeta",VoiceGender.Female, VoiceAge.Adult,"pt",this)
			};
		}

		public override IEnumerable<IVoice> GetAvailableVoices()
		{
			return _availableVoices;
		}

		protected override BakedTTS BuildTTS(IVoice voice, string text, double rate, int pitch)
		{
			var tts = RequestTTS((L2FVoice)voice, text);
			if (tts == null)
				return null;

			var phones = ((JsonArray)tts["phones"]).Select(t => ((JsonString)t).String);
			var times = ((JsonArray)tts["times"]).Select(t => Convert.ToDouble(((JsonNumber)t).Value));
			var toProcess = phones.Zip(times, (s, d) => new { ph = s, duration = d }).ToArray();

			List<VisemeSpan> visemes = new List<VisemeSpan>();
			//Convert phones to visemes and adjust time to rate
			var mapper = LanguageResources.PT_PhonesToVisemes;
			Viseme lastViseme = Viseme.Silence;
			double totalVisemeTime = 0;
			foreach (var p in toProcess)
			{
				Viseme currentViseme;
				if (!mapper.TryGetValue(p.ph, out currentViseme))
					currentViseme = Viseme.Silence;

				if (lastViseme != currentViseme)
				{
					visemes.Add(new VisemeSpan() { duration = totalVisemeTime, viseme = lastViseme });
					lastViseme = currentViseme;
					totalVisemeTime = 0;
				}

				totalVisemeTime += p.duration / rate;
			}
			if (lastViseme != Viseme.Silence)
				visemes.Add(new VisemeSpan() { duration = totalVisemeTime, viseme = lastViseme });

			if (visemes.Count == 0)
				return null;

			var audioUrls = ((JsonArray)tts["url"]).Select(j => ((JsonString)j).String).ToArray();
			var v = BuildAudioStream(audioUrls, rate, pitch);

			const int MAX_SAMPLES = 1024;
			var bufferSize = MAX_SAMPLES * v.WaveFormat.Channels;
			var buffer = new float[bufferSize];

			using (var m = new MemoryStream())
			{
				using (var w = new WaveFileWriter(m, v.WaveFormat))
				{
					int readed;
					while ((readed = v.Read(buffer, 0, bufferSize)) > 0)
					{
						w.WriteSamples(buffer, 0, readed);
					}
					w.Flush();
					
					return new BakedTTS() { visemes = visemes.ToArray(), waveStreamData = m.ToArray() };
				}
			}
		}

		private JsonObject RequestTTS(L2FVoice voice, string text)
		{
			JsonObject[] results;
			using (var client = new HttpClient())
			{
				var values = new Dictionary<string, string>()
				{
					{"task", "echo"},
					{"lang", voice.Culture},
					{"v", voice.Name},
					{"q", text}
				};

				var content = new FormUrlEncodedContent(values);
				var response = client.PostAsync(TTS_URL, content).Result;
				if (!response.IsSuccessStatusCode)
					throw new Exception("fail");

				var str = response.Content.ReadAsStringAsync().Result;
				var data = (JsonObject)JsonParser.Parse(str);
				var array = (JsonArray)data["ttsresults"];
				if (array.Count == 0)
					return null;
				results = array.Cast<JsonObject>().ToArray();
			}

			if (results.Length == 1)
			{
				var r2 = results[0];
				r2["url"] = new JsonArray() { r2["url"] };
				return r2;
			}

			return results.Aggregate((j1, j2) =>
			{
				if(DELAY_BETWEEN_FRASES>0)
					((JsonArray)j1["phones"]).Add(new JsonString("@"));
				((JsonArray)j1["phones"]).AddRange((JsonArray)j2["phones"]);

				if (DELAY_BETWEEN_FRASES > 0)
					((JsonArray)j1["times"]).Add(new JsonNumber(DELAY_BETWEEN_FRASES));
				((JsonArray)j1["times"]).AddRange((JsonArray)j2["times"]);

				var token = j1["url"];
				var a = token as JsonArray;
				if (a == null)
				{
					a = new JsonArray {token};
					j1["url"] = a;
				}
				a.Add(j2["url"]);
				return j1;
			});
			
		}

		private ISampleProvider BuildAudioStream(string[] urls, double rate, int pitch)
		{
			ISampleProvider mixer;

			using (var client = new HttpClient())
			{
				mixer = new SampleJoiner(urls.Select(u =>
				{
					var url = AUDIO_DL_PREFIX + u;
					var oggDownloadRequest = client.GetAsync(url).Result;
					if (!oggDownloadRequest.IsSuccessStatusCode)
						throw new Exception("ogg download fail");

					var oggStream = oggDownloadRequest.Content.ReadAsStreamAsync().Result;
					return (WaveStream) new NAudio.Vorbis.VorbisWaveReader(oggStream);
				}));
			}

			return new AudioStreamModifier(mixer, rate, pitch);
		}

		private class L2FVoice : BaseVoice
		{
			private Action _onFinished;
			private WaveOutEvent _player;

			public L2FVoice(string name, VoiceGender gender, VoiceAge age, string culture, L2FSpeechEngine engine) : base(engine)
			{
				Name = name;
				Gender = gender;
				Age = age;
				Culture = culture;
			}

			//public bool IsSpeaking => _onFinished != null;

			//public void Speak(string text, double playbackRate, int pitchShift, Action onFinished = null)
			//{
			//	if(IsSpeaking)
			//		CancelSpeaking();

			//	_onFinished = onFinished;
			//	_player = _engine.PlayAudio(this,text,playbackRate,pitchShift);
			//	if(_player==null)
			//		OnFinishedDispatch();
			//}

			//public void CancelSpeaking()
			//{
			//	if(!IsSpeaking)
			//		return;

			//	_player.Stop();
			//	OnFinishedDispatch();
			//}

			//public BakedTTS BakeSpeechFiles(string text, double playbackRate, int pitchShift)
			//{
			//	return _engine.Bake(this, text, playbackRate, pitchShift);
			//}

			//public void OnFinishedDispatch()
			//{
			//	if (_player != null)
			//	{
			//		_player.Dispose();
			//		_player = null;
			//	}
			//	_onFinished?.Invoke();
			//	_onFinished = null;
			//}
		}
	}
}
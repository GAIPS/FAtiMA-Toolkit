using System;
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
		/*	_availableVoices = new[]
			{
				new L2FVoice("Vicent",VoiceGender.Male, VoiceAge.Adult,"pt",this),
				new L2FVoice("Viriato",VoiceGender.Male, VoiceAge.Adult,"pt",this),
				new L2FVoice("Violeta",VoiceGender.Female, VoiceAge.Adult,"pt",this)
			};*/
			
	
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

			//Create visemes
			List<VisemeSpan> visemes = new List<VisemeSpan>();
			//Convert phones to visemes and adjust time to rate
			var mapper = LanguageResources.PT_PhonesToVisemes;
			Viseme lastViseme = Viseme.Silence;
			double totalVisemeTime = 0;
			foreach (var r in tts)
			{
				var phones = ((JsonArray)r["phones"]).Select(t => ((JsonString)t).String);
				var time = ((JsonArray) r["times"]).Select(t => Convert.ToDouble(((JsonNumber) t).Value));

				var toProcess = phones.Zip(time, (s, d) => new { ph = s, entryTime = d }).ToArray();
				totalVisemeTime += toProcess[0].entryTime;///rate;
				lastViseme = Viseme.Silence;
				for (int i = 1; i < toProcess.Length-1; i++)
				{
					var p = toProcess[i];
					var nextTime = toProcess[i+1].entryTime;
					Viseme currentViseme;
					if (!mapper.TryGetValue(p.ph, out currentViseme))
						currentViseme = Viseme.Silence;

					if (lastViseme != currentViseme)
					{
						visemes.Add(new VisemeSpan() { duration = totalVisemeTime/rate, viseme = lastViseme });
						lastViseme = currentViseme;
						totalVisemeTime = 0;
					}
					var duration = nextTime - p.entryTime;
					totalVisemeTime += duration;
				}
			}
			if(totalVisemeTime>0)
				visemes.Add(new VisemeSpan() { duration = totalVisemeTime/rate, viseme = lastViseme });

			if (visemes.Count == 0)
				return null;

			//Create audio
			var audioUrls = tts.Select(r => ((JsonString)r["url"]).String).ToArray();
			var v = BuildAudioStream(audioUrls, rate, pitch);

			const int MAX_SAMPLES = 5120;
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

		private JsonObject[] RequestTTS(L2FVoice voice, string text)
		{
			// What happens if the server is down?
			using (var client = new HttpClient())
			{
				client.Timeout = TimeSpan.FromSeconds(15);
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
				if (str == "")
					return null;
				var data = (JsonObject)JsonParser.Parse(str);
				var array = (JsonArray)data["ttsresults"];
				if (array.Count == 0)
					return null;
				return array.Cast<JsonObject>().ToArray();
			}
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
			public L2FVoice(string name, VoiceGender gender, VoiceAge age, string culture, L2FSpeechEngine engine) : base(engine)
			{
				Name = name;
				Gender = gender;
				Age = age;
				Culture = culture;
			}
		}
	}
}
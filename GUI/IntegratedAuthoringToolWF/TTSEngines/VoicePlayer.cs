using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;

namespace IntegratedAuthoringToolWF.TTSEngines
{
	public class VoicePlayer
	{
		private BakedTTS _tts;
		private CancellationTokenSource _token;

		public VoicePlayer(BakedTTS tts)
		{
			_tts = tts;
		}

		public bool IsPlaying { get { return _token != null; } }

		public void Play(Action onFinished = null, Action<Viseme> onVisemeHit=null)
		{
			if(IsPlaying)
				Stop();

			_token = new CancellationTokenSource();
			PlayAsync(onFinished, onVisemeHit, _token.Token);
		}

		public void Stop()
		{
			if(!IsPlaying)
				return;

			_token.Cancel();
			_token = null;
		}

		private async Task PlayAsync(Action onFinished, Action<Viseme> onVisemeHit, CancellationToken token)
		{
			try
			{
				using (var m = new MemoryStream(_tts.waveStreamData))
				{
					using (var reader = new WaveFileReader(m))
					{
						using (var player = new WaveOut())
						{
							player.Init(reader);
							player.Play();

							while (player.PlaybackState == PlaybackState.Playing)
							{
								await Task.Delay(100, token);
								if (token.IsCancellationRequested)
									break;

								Console.WriteLine("...next");
							}
							player.Stop();
							onFinished?.Invoke();
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw e;
			}
		}
	}
}
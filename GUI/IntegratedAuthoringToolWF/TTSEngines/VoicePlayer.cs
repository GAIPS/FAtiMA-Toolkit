using System;
using System.IO;
using System.Linq;
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

							var visemes = _tts.visemes;

							int nextViseme = 0;
							double nextVisemeTime = 0;

							while (player.PlaybackState == PlaybackState.Playing)
							{
								if (onVisemeHit != null)
								{
									var s = reader.CurrentTime.TotalSeconds;
									if (s >= nextVisemeTime)
									{
										var v = visemes[nextViseme];
										nextViseme++;
										if (nextViseme >= visemes.Length)
											nextVisemeTime = double.PositiveInfinity;
										else
											nextVisemeTime += v.duration;

										onVisemeHit(v.viseme);
									}
								}

								await Task.Delay(1);
								if (token.IsCancellationRequested)
									break;

								//Console.WriteLine("...next");
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
using System;
using NAudio.Wave;
using SoundTouch;

namespace IntegratedAuthoringToolWF.Audio
{
	public class AudioStreamModifier : ISampleProvider
	{
		private const long readDurationMilliseconds = 100;

		private SoundTouch<float, double> _soundTouch;
		private ISampleProvider _sample;

		private readonly float[] sourceReadBuffer;
		private readonly float[] soundTouchReadBuffer;
		private readonly int channelCount;
		private bool reachedEndOfSource = false;

		public AudioStreamModifier(ISampleProvider sample, double rateMult, int pitchDelta)
		{
			_sample = sample;
			WaveFormat = _sample.WaveFormat;

			_soundTouch = new SoundTouch<float, double>();

			channelCount = sample.WaveFormat.Channels;
			_soundTouch.SetSampleRate(sample.WaveFormat.SampleRate);
			_soundTouch.SetChannels(channelCount);

			rateMult = (rateMult - 1)*50;
			_soundTouch.SetTempoChange(rateMult);
			_soundTouch.SetPitchSemiTones(pitchDelta*0.25f);
			_soundTouch.SetRateChange(1.0f);

			_soundTouch.SetSetting(SettingId.UseQuickseek, 1);
			_soundTouch.SetSetting(SettingId.UseAntiAliasFilter, 1);

			_soundTouch.SetSetting(SettingId.SequenceDurationMs, 40);
			_soundTouch.SetSetting(SettingId.SeekwindowDurationMs, 15);
			_soundTouch.SetSetting(SettingId.OverlapDurationMs, 8);

			sourceReadBuffer = new float[(WaveFormat.SampleRate * channelCount * readDurationMilliseconds) / 1000];
			soundTouchReadBuffer = new float[sourceReadBuffer.Length * 10]; // support down to 0.1 speed
		}

		public int Read(float[] buffer, int offset, int count)
		{
			int samplesRead = 0;
			while (samplesRead < count)
			{
				if (!reachedEndOfSource)
				{
					var readFromSource = _sample.Read(sourceReadBuffer, 0, sourceReadBuffer.Length);
					if (readFromSource > 0)
					{
						_soundTouch.PutSamples(sourceReadBuffer, readFromSource / channelCount);
					}
					else
					{
						reachedEndOfSource = true;
						// we've reached the end, tell SoundTouch we're done
						_soundTouch.Flush();
					}
				}
				var desiredSampleFrames = (count - samplesRead) / channelCount;
				var received = _soundTouch.ReceiveSamples(soundTouchReadBuffer, desiredSampleFrames) * channelCount;
				// use loop instead of Array.Copy due to WaveBuffer
				for (int n = 0; n < received; n++)
				{
					buffer[offset + samplesRead++] = soundTouchReadBuffer[n];
				}
				if (received == 0 && reachedEndOfSource) break;
			}
			return samplesRead;
		}

		
		public WaveFormat WaveFormat { get; }
	}
}
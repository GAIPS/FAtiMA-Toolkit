using System;
using NAudio.Wave;

namespace IntegratedAuthoringToolWF.Audio
{
	public class AutoDisposeAudioSample : ISampleProvider
	{
		private readonly ISampleProvider reader;
		private bool isDisposed;
		private Action _onDispose;

		public AutoDisposeAudioSample(ISampleProvider reader, Action onDispose)
		{
			this.reader = reader;
			this.WaveFormat = reader.WaveFormat;
			_onDispose = onDispose;
		}

		public int Read(float[] buffer, int offset, int count)
		{
			if (isDisposed)
				return 0;

			int read = reader.Read(buffer, offset, count);
			if (read == 0)
			{
				isDisposed = true;
				_onDispose?.Invoke();
			}
			return read;
		}

		public WaveFormat WaveFormat { get;}
	}
}
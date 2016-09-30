using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace IntegratedAuthoringToolWF.Audio
{
	public class SampleJoiner : ISampleProvider
	{
		private ISampleProvider[] _samples;
		private int _currentSample = 0;
		private int _currentRead = 0;
		public SampleJoiner(IEnumerable<WaveStream> samples)
		{
			var s = samples.ToArray();
			WaveFormat = s[0].WaveFormat;
			_samples = new ISampleProvider[s.Length];
			for (int i = 0; i < _samples.Length; i++)
			{
				var s2 = s[i];
				if (s2.WaveFormat.SampleRate != WaveFormat.SampleRate)
					s2 = new WaveFormatConversionStream(WaveFormat, s2);

				_samples[i] = new WaveToSampleProvider(s2);
			}
		}

		/// <summary>
		/// Fill the specified buffer with 32 bit floating point samples
		/// </summary>
		/// <param name="buffer">The buffer to fill with samples.</param><param name="offset">Offset into buffer</param><param name="count">The number of samples to read</param>
		/// <returns>
		/// the number of samples written to the buffer.
		/// </returns>
		public int Read(float[] buffer, int offset, int count)
		{
			if (_currentSample >= _samples.Length)
				return 0;

			var readed = _samples[_currentSample].Read(buffer, offset, count);
			if (readed == 0)
			{
				_currentSample++;
				if (_currentSample >= _samples.Length)
					return 0;

				readed = _samples[_currentSample].Read(buffer, offset, count);
			}

			return readed;
		}

		/// <summary>
		/// Gets the WaveFormat of this Sample Provider.
		/// </summary>
		/// <value>
		/// The wave format.
		/// </value>
		public WaveFormat WaveFormat { get; }
	}
}
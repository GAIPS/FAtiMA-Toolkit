using System;
using System.Collections.Generic;
using System.Linq;

namespace EDARecognition
{
    public class MovingSampleSet
    {
        public int WindowSize { get; private set; }
        public float MovingAverage { get; private set; }
        public float MovingStandardDeviation { get; private set; }
        public float LatestSample { get; private set; }
        public float MovingZScore { get; private set; }

        private List<float> Samples;

        public MovingSampleSet(int windowSize)
        {
            this.WindowSize = windowSize;
            this.Samples = new List<float>(this.WindowSize);
        }

        public void AddSample(float value)
        {
            if (this.Samples.Count == this.WindowSize)
            {
                this.Samples.RemoveAt(0);
            }
            this.Samples.Add(value);
            this.LatestSample = value;

            this.MovingAverage = this.Samples.Average();

            var aggregateDeviation = this.Samples.Aggregate((acum, s) => acum + (float)Math.Pow(s - this.MovingAverage, 2));
            this.MovingStandardDeviation = (float)Math.Sqrt(aggregateDeviation / this.Samples.Count);

            this.MovingZScore = (this.LatestSample - this.MovingAverage) / this.MovingStandardDeviation;
        }
    }
}

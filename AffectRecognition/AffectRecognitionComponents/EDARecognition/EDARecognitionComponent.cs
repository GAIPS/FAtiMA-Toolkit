using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System;
using System.IO;
using MultimodalEmotionDetection;

namespace EDARecognition
{
    public class EDARecognitionComponent : IAffectRecognitionComponent
    {
        private OpenSignalsSocket Socket { get; set; }

        public const float AVERAGE_SCL = 1.801015f;
        public const float STD_SCL = 0.509811f;

        public float SkinConductanceLevel { get; private set; }
        public float SkinConductanceResponse { get; private set; }

        public float SCLMinBaseline { get; private set; }
        public float SCLMaxBaseline { get; private set; }

        public float SCRMinBaseline { get; private set; }
        public float SCRMaxBaseline { get; private set; }

        public float StandardSCL { get { return (this.SkinConductanceLevel - this.SCLMinBaseline) / (this.SCLMaxBaseline - this.SCLMinBaseline); } }
        public float StandardSCR { get { return this.SkinConductanceResponse / this.SCRMaxBaseline; } }

        public float SCL_ZScore { get { return (this.SkinConductanceLevel - AVERAGE_SCL) / STD_SCL; } }
        public float SCR_ZScore { get { return this.SCRSamples.MovingZScore; } }

        public float SCL_MovingAverage { get { return this.SCLSamples.MovingAverage; } }
        public float SCR_MovingAverage { get { return this.SCRSamples.MovingAverage; } }

        public float Arousal { get { return this.SCL_ZScore * 0.2f + 0.5f; } }

        public bool Connected { get { return this.Socket.SocketReady; } }

        public bool IsRecordingBaseline { get { return this.baselineWatch.IsRunning; } }

        public bool LogActive { get; set; }

        public string Name
        {
            get
            {
                return "EDARecognition";
            }
        }

        private StreamWriter logWriter;
        private Stopwatch baselineWatch;
        private long baselineRecordingWindow;

        private List<float> SCLMinBaselineSamples;
        private List<float> SCLMaxBaselineSamples;
        private List<float> SCRMinBaselineSamples;
        private List<float> SCRMaxBaselineSamples;

        private bool minBaseline;

        private MovingSampleSet SCLSamples;
        private MovingSampleSet SCRSamples;


        public EDARecognitionComponent()
        {
            this.Socket = new OpenSignalsSocket(this);
            this.SCLSamples = new MovingSampleSet(600);
            this.SCRSamples = new MovingSampleSet(30);
            
            this.baselineWatch = new Stopwatch();
        }

        public void RecordMinBaseline(long timeInSeconds)
        {
            this.SCLMinBaselineSamples = new List<float>();
            this.SCRMinBaselineSamples = new List<float>();
            this.baselineWatch.Restart();
            this.baselineRecordingWindow = timeInSeconds;
            this.minBaseline = true;
        }

        public void RecordMaxBaseline(long timeInSeconds)
        {
            this.SCLMaxBaselineSamples = new List<float>();
            this.SCRMaxBaselineSamples = new List<float>();
            this.baselineWatch.Restart();
            this.baselineRecordingWindow = timeInSeconds;
            this.minBaseline = false;
        }

        public void ActivateLogging(string logFile)
        {
            this.LogActive = true;
            this.logWriter = File.CreateText("logs/" + logFile + ".txt");
            this.logWriter.WriteLine("Time\tSCLValue\tSCLStandardizedValue\tSCLZ-Score\tSCLMovingAverage\tSCRValue\tSCRStandardizedValue\tSCRZ-Score\tSCRMovingAverage");
             
        }

        private void Log()
        {
            if(this.logWriter != null)
            {
                
                this.logWriter.WriteLine(DateTime.Now + "\t" + this.SkinConductanceLevel + "\t" + this.StandardSCL + "\t" + this.SCL_ZScore + "\t" + this.SCL_MovingAverage
                    + "\t" + this.SkinConductanceResponse + "\t" + this.StandardSCR + "\t" + this.SCR_ZScore + "\t" + this.SCR_MovingAverage);
                this.logWriter.Flush();
            }
        }

        public void SetSkinConductanceLevel(float tonicLevel)
        {
            var logOfTonicLevel = (float)Math.Log(tonicLevel);
            this.SCLSamples.AddSample(logOfTonicLevel);

            this.SkinConductanceLevel = logOfTonicLevel;

            if (this.baselineWatch.IsRunning) 
            {
                if(this.baselineWatch.Elapsed.Seconds > this.baselineRecordingWindow)
                {
                    this.SetBaselineLevels(this.minBaseline);
                }
                else
                {
                    if (this.minBaseline)
                    {
                        this.SCLMinBaselineSamples.Add(logOfTonicLevel);
                    }
                    else
                    {
                        this.SCLMaxBaselineSamples.Add(logOfTonicLevel);
                    }
                }
            }

            if(this.LogActive)
            {
                this.Log();
            }
        }

        public void SetSkinConductanceRate(float phasicRate)
        {
            this.SCRSamples.AddSample(phasicRate);

            this.SkinConductanceResponse = phasicRate;

            if (this.baselineWatch.IsRunning)
            {
                if (this.baselineWatch.Elapsed.Seconds > this.baselineRecordingWindow)
                {
                    this.SetBaselineLevels(this.minBaseline);
                }
                else
                {
                    if(this.minBaseline)
                    {
                        this.SCRMinBaselineSamples.Add(phasicRate);
                    }
                    else
                    {
                        this.SCRMaxBaselineSamples.Add(phasicRate);
                    }
                }
            }
            
        }

        private void SetBaselineLevels(bool minBaseline)
        {
            this.baselineWatch.Stop();
            if(minBaseline)
            {
                if (this.SCLMinBaselineSamples.Count > 0)
                {
                    this.SCLMinBaseline = this.SCLMinBaselineSamples.Average();
                }
                else this.SCLMinBaseline = 0;

                if (this.SCRMinBaselineSamples.Count > 0)
                {
                    this.SCRMinBaseline = this.SCRMinBaselineSamples.Average();
                }
                else this.SCRMinBaseline = 0;
            }
            else
            {
                if (this.SCLMaxBaselineSamples.Count > 0)
                {
                    this.SCLMaxBaseline = this.SCLMaxBaselineSamples.Average();
                }
                else this.SCLMaxBaseline = 0;

                if (this.SCRMaxBaselineSamples.Count > 0)
                {
                    this.SCRMaxBaseline = this.SCRMaxBaselineSamples.Max();
                }
                else this.SCRMaxBaseline = 0;
            }
        }

        public IEnumerable<AffectiveInformation> GetSample()
        {
            var arousal = this.Arousal;
            if(arousal > 1)
            {
                arousal = 1;
            }
            else if (arousal < 0)
            {
                arousal = 0;
            }
            return new List<AffectiveInformation>
            {
                new AffectiveInformation { Name = "arousal", Score = arousal }
            };
        }

        public IEnumerable<string> GetRecognizedAffectiveVariables()
        {
            return new List<string> { "arousal" };
        }
    }
}

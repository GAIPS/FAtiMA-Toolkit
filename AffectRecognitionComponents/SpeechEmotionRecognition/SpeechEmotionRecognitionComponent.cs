using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using RealTimeEmotionRecognition;
using System.Collections.Generic;
using System.Linq;

namespace SpeechEmotionRecognition
{
    public class SpeechEmotionRecognitionComponent :  IAffectRecognitionComponent
    {
        //public List<Action<IEnumerable<AffectiveInformation>>> EmotionUpdateActions { get; private set; }

        public float DecayWindow { get; set; }
        private IEnumerable<AffectiveInformation> CurrentSample;

        private DateTime SampleTime { get; set; }

        public string Name
        {
            get
            {
                return "SpeechEmotionRecognition";
            }
        }

        public SpeechEmotionRecognitionComponent()
        {
            //this.EmotionUpdateActions = new List<Action<IEnumerable<AffectiveInformation>>>();
            this.DecayWindow = 5.0f;
            this.CurrentSample = new List<AffectiveInformation>();
            
        }

        public IEnumerable<AffectiveInformation> ProcessSpeech(byte[] speech)
        {
            string result;

            var request = this.CreateRequest(speech);

            try
            {
                WebResponse response = request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                result = reader.ReadToEnd();
                reader.Close();


                result = result.Remove(0, result.IndexOf("<result>") + 8);
                result = result.Remove(result.IndexOf("</result>"), result.Length - result.IndexOf("</result>") - 1);

                var finalResult = result.Split(':')[2].Split(' ')[0];

                if (finalResult.StartsWith("angry"))
                {
                    this.CurrentSample = new List<AffectiveInformation> { new AffectiveInformation { Name = "angry", Score = 1.0f } };
                }
                else
                {
                    this.CurrentSample = new List<AffectiveInformation> { new AffectiveInformation { Name = "angry", Score = 0.0f } };
                }

                this.SampleTime = DateTime.Now;

                //foreach(var action in this.EmotionUpdateActions)
                //{
                //    action.Invoke(output);
                //}

                return this.CurrentSample;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<AffectiveInformation>> ProcessSpeechAsync(byte[] speech)
        {
            string result;

            var request = this.CreateRequest(speech);

            try
            {
                WebResponse response = await request.GetResponseAsync();
                
                StreamReader reader = new StreamReader(response.GetResponseStream());
                result = reader.ReadToEnd();
                reader.Close();
                

                result = result.Remove(0, result.IndexOf("<result>") + 8);
                result = result.Remove(result.IndexOf("</result>"), result.Length - result.IndexOf("</result>") - 1);

                var finalResult = result.Split(':')[2].Split(' ')[0];

                if (finalResult.StartsWith("angry"))
                {
                    this.CurrentSample = new List<AffectiveInformation> { new AffectiveInformation { Name = "angry", Score = 1.0f } };
                }
                else
                {
                    this.CurrentSample = new List<AffectiveInformation> { new AffectiveInformation { Name = "angry", Score = 0.0f } };
                }

                this.SampleTime = DateTime.Now;

                //foreach (var action in this.EmotionUpdateActions)
                //{
                //    action.Invoke(output);
                //}

                return this.CurrentSample;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private HttpWebRequest CreateRequest(byte[] speech)
        {
            HttpWebRequest request = WebRequest.Create(new Uri("https://www.l2f.inesc-id.pt/spa/services/emotionDetection")) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.Headers["Authorization"] = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("joao.dias@gaips.inesc-id.pt:rage2016"));
            request.PreAuthenticate = true;

            StringBuilder builder = new StringBuilder();
            builder.Append("<root><audio>");
            builder.Append(System.Convert.ToBase64String(speech));
            builder.Append("</audio></root>");

            byte[] byteData = Encoding.UTF8.GetBytes(builder.ToString());

            request.ContentLength = byteData.Length;

            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
                postStream.Flush();
            }

            return request;
        }

        

        public async void Test()
        {
            IEnumerable<AffectiveInformation> result;

            FileStream speechTestFile = File.Open("D:\\Research\\EU Projects\\RAGE\\WP 2\\Gravacao.wav",FileMode.Open);
            byte[] speech = new byte[speechTestFile.Length];
            speechTestFile.Read(speech, 0, speech.Length);

            result = await this.ProcessSpeechAsync(speech);

            speechTestFile = File.Open("D:\\Research\\EU Projects\\RAGE\\WP 2\\anger.wav", FileMode.Open);
            speech = new byte[speechTestFile.Length];
            speechTestFile.Read(speech, 0, speech.Length);

            result = await this.ProcessSpeechAsync(speech);

            return;
        }

        public IEnumerable<AffectiveInformation> GetSample()
        {
            if (this.CurrentSample.Count() > 0 && (DateTime.Now - this.SampleTime).Seconds > this.DecayWindow)
            {
                this.CurrentSample = new List<AffectiveInformation>();
            }

            return this.CurrentSample;
        }

        public IEnumerable<string> GetRecognizedAffectiveVariables()
        {
            return new List<string> { "angry" };
        }
    }
}

using AssetManagerPackage;
using AssetPackage;
using RealTimeEmotionRecognition;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialEmotionRecognition
{
    public class FacialEmotionRecognitionComponent : IAffectRecognitionComponent
    {
        public EmotionDetectionAsset EDA { get; set; }

        string IAffectRecognitionComponent.Name
        {
            get
            {
                return "FacialEmotionRecognition";
            }
        }

        public FacialEmotionRecognitionComponent()
        {
            this.EDA = new EmotionDetectionAsset();
            var edaSettings = this.EDA.Settings as EmotionDetectionAssetSettings;

            this.EDA.Initialize(@".\", edaSettings.Database);

            this.EDA.ParseRules(File.ReadAllLines(edaSettings.Rules));

            edaSettings.SuppressSpikes = true;

            //foreach (String emotion in this.EDA.Emotions)
            //{
            //    Messages.subscribe(emotion, EmotionUpdateEventHandler);
            //}
        }

        public void ProcessImage(Image img)
        {
            bool facedetected = this.EDA.ProcessImage(img);

            if (facedetected)
            {
                if (this.EDA.ProcessFaces())
                {
                    //! Show Detection Results.
                    //
                    this.EDA.ProcessLandmarks();
                }
            }
        }


        public IEnumerable<string> GetRecognizedAffectiveVariables()
        {
            return this.EDA.Emotions.Select(em => ConvertEmotion(em));
        }

        public IEnumerable<AffectiveInformation> GetSample()
        {
            List<AffectiveInformation> sample = new List<AffectiveInformation>();

            foreach(var emotion in this.EDA.Emotions)
            {
                sample.Add(new AffectiveInformation() { Name = ConvertEmotion(emotion), Score = (float)this.EDA[0, emotion] });
            }

            return sample;
        }

        public static string ConvertEmotion(string emotion)
        {
            if(emotion.Equals("Anger"))
            {
                return "angry";
            }
            else if(emotion.Equals("Fear"))
            {
                return "scared";
            }
            else
            {
                return emotion.ToLower();
            }
        }
    }
}

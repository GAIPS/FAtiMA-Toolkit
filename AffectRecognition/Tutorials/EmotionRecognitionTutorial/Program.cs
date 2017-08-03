using AssetPackage;
using FacialEmotionRecognition;
using RealTimeEmotionRecognition;
using RealTimeEmotionRecognition.FusionPolicies;
using SpeechEmotionRecognition;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TextEmotionRecognition;

namespace EmotionRecognitionTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing and configuring recognition components...");
            RealTimeEmotionRecognitionAsset rtER = new RealTimeEmotionRecognitionAsset();
            TextEmotionRecognitionComponent textER = new TextEmotionRecognitionComponent() { DecayWindow = 10 };
            SpeechEmotionRecognitionComponent speechER = new SpeechEmotionRecognitionComponent() { DecayWindow = 10 };
            FacialEmotionRecognitionComponent facialER = new FacialEmotionRecognitionComponent();
            (facialER.EDA.Settings as EmotionDetectionAssetSettings).SuppressSpikes = false;
            
            rtER.AddAffectRecognitionAsset(textER, 1.0f);
            rtER.AddAffectRecognitionAsset(speechER, 1.0f);
            rtER.AddAffectRecognitionAsset(facialER, 1.0f);
           
            rtER.Policy = new KalmanFilterFusionPolicy(rtER.Classifiers);

            Console.WriteLine("Done!\n");

            //processing facial recognition from image
            Console.WriteLine("Processing Image Example...");
            Image exampleImage = Image.FromFile(@".\Kiavash1.jpg");
            facialER.ProcessImage(exampleImage);
            Console.WriteLine("Done!\n");

            //processing text modality
            Console.WriteLine("Processing Text Example...");
            textER.ProcessText("I'm so so happy. I'm hoping this is recognized as an happy statement.");
            Console.WriteLine("Done!\n");

            //processing sentiment analysis from speech (an anger example, sorry for the bad language!)
            Console.WriteLine("Processing Speech Example...");
            FileStream speechTestFile = File.Open(@".\anger.wav", FileMode.Open);
            byte[] speech = new byte[speechTestFile.Length];
            speechTestFile.Read(speech, 0, speech.Length);
            speechTestFile.Close();
            speechER.ProcessSpeech(speech);
            Console.WriteLine("Done!\n");
            
            //this method will create a new fused sample by combining samples from the registered classifiers (using the defined fusion policy)
            rtER.UpdateSamples();

            Console.WriteLine("Text Component Classification:");
            WriteSample(textER.GetSample());
            Console.WriteLine();

            Console.WriteLine("Facial Component Classification");
            WriteSample(facialER.GetSample());
            Console.WriteLine();

            Console.WriteLine("Speech Component Classification");
            WriteSample(speechER.GetSample());
            Console.WriteLine();

            Console.WriteLine("Fused Classification:");
            WriteSample(rtER.GetSample());
            Console.WriteLine();

            Console.WriteLine("Demo finished. Press any key to exit.");
            Console.ReadLine();
        }

        private static void WriteSample(IEnumerable<AffectiveInformation> sample)
        {
            foreach (var affectiveInfo in sample)
            {
                Console.WriteLine(affectiveInfo.Name + ": " + affectiveInfo.Score);
            }
        }
    }
}

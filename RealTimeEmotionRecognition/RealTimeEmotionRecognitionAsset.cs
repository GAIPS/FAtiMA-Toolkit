using AssetPackage;
using System.Collections.Generic;
using System;
using RealTimeEmotionRecognition.FusionPolicies;

namespace RealTimeEmotionRecognition
{
    public class RealTimeEmotionRecognitionAsset : BaseAsset
    {
        public IFusionPolicy Policy { get; set; }
        public List<Classifier> Classifiers { get; private set; }

        private List<MultiClassifierAffectiveInformation> AffectiveInformationData;

        public RealTimeEmotionRecognitionAsset()
        {
            this.Policy = new MaxPolicy();
            //this.UpdateRate = 1.0f; //1 Hz by default
            this.Classifiers = new List<Classifier>();
            this.AffectiveInformationData = new List<MultiClassifierAffectiveInformation>();
        }

        public void AddAffectRecognitionAsset(IAffectRecognitionAsset affectRecognitionAsset, float weight)
        {
            this.Classifiers.Add(new Classifier { Asset = affectRecognitionAsset, Weight = weight });
        }

        public void UpdateSamples()
        {
            var sampleData = new MultiClassifierAffectiveInformation { Time = DateTime.Now };

            foreach(var classifier in this.Classifiers)
            {
                sampleData.AddSample(classifier, classifier.Asset.GetSample());
            }

            sampleData.FuseClassifiers(this.Policy);

            this.AffectiveInformationData.Add(sampleData);
        }

        public IEnumerable<AffectiveInformation> GetSample()
        {
            if (this.AffectiveInformationData.Count == 0) return new List<AffectiveInformation>();

            return this.AffectiveInformationData[this.AffectiveInformationData.Count - 1].FusedAffectiveInformation;
        }
    }
}

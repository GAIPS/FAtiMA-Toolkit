using System.Collections.Generic;
using System;
using MultimodalEmotionDetection.FusionPolicies;

namespace MultimodalEmotionDetection
{
    public class MultiClassifierAffectiveInformation
    {
        public DateTime Time { get; set; }
        public Dictionary<string, List<AugmentedAffectiveInformation>> MCAffectiveInformationSet { get; private set; }
        public List<AffectiveInformation> FusedAffectiveInformation { get; private set; }
        
        public MultiClassifierAffectiveInformation()
        {
            this.MCAffectiveInformationSet = new Dictionary<string, List<AugmentedAffectiveInformation>>();
            this.FusedAffectiveInformation = new List<AffectiveInformation>();
            
        }

        public void AddSample(Classifier classifier, IEnumerable<AffectiveInformation> classifierSample)
        {
            AugmentedAffectiveInformation augmentedInfo;
            foreach(var ai in classifierSample)
            {
                augmentedInfo = new AugmentedAffectiveInformation(ai, classifier);

                if (!this.MCAffectiveInformationSet.ContainsKey(ai.Name))
                {
                    this.MCAffectiveInformationSet[ai.Name] = new List<AugmentedAffectiveInformation>();
                    
                }
                this.MCAffectiveInformationSet[ai.Name].Add(augmentedInfo);
            }
        }

        public IEnumerable<AffectiveInformation> FuseClassifiers(IFusionPolicy policy)
        {
            foreach(var affectiveVariable in this.MCAffectiveInformationSet.Values)
            {
                var fusedAffectiveVariable = policy.Fuse(affectiveVariable);
                if(fusedAffectiveVariable != null)
                {
                    this.FusedAffectiveInformation.Add(fusedAffectiveVariable);
                }
            }

            return this.FusedAffectiveInformation;
        }
    }
}

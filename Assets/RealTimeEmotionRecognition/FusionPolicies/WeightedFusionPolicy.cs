using System.Collections.Generic;
using System.Linq;

namespace RealTimeEmotionRecognition.FusionPolicies
{
    public class WeightedFusionPolicy : IFusionPolicy
    {
        public string PolicyName { get { return "Weighted"; } }

        public AffectiveInformation Fuse(IEnumerable<AugmentedAffectiveInformation> affectiveInformation)
        {
            float score = 0.0f;
            float totalWeight = 0.0f;

            if (affectiveInformation.Count() == 0)
            {
                return null;
            }

            AffectiveInformation result = new AffectiveInformation();
            result.Name = affectiveInformation.FirstOrDefault().Name;

            foreach(var ai in affectiveInformation)
            {
                score += ai.Classifier.Weight * ai.Score;
                totalWeight += ai.Classifier.Weight;
            }

            result.Score = score / totalWeight;

            return result;
        }
    }
}

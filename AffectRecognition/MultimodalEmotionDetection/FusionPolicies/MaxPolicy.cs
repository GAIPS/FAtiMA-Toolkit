using System.Collections.Generic;
using System.Linq;

namespace MultimodalEmotionDetection.FusionPolicies
{
    public class MaxPolicy : IFusionPolicy
    {
        public string PolicyName { get { return "Max"; } }

        public AffectiveInformation Fuse(IEnumerable<AugmentedAffectiveInformation> affectiveInformation)
        {
            if (affectiveInformation.Count() == 0)
            {
                return null;
            }

            AffectiveInformation result = new AffectiveInformation();
            result.Name = affectiveInformation.FirstOrDefault().Name;
            result.Score = affectiveInformation.Select(aff => aff.Score).Max();

            return result;
        }
    }
}

using System.Collections.Generic;

namespace MultimodalEmotionDetection.FusionPolicies
{
    public interface IFusionPolicy
    {
        string PolicyName { get; }
        AffectiveInformation Fuse(IEnumerable<AugmentedAffectiveInformation> affectiveInformation);
    }
}

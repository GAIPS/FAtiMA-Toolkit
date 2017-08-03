using System.Collections.Generic;

namespace RealTimeEmotionRecognition.FusionPolicies
{
    public interface IFusionPolicy
    {
        string PolicyName { get; }
        AffectiveInformation Fuse(IEnumerable<AugmentedAffectiveInformation> affectiveInformation);
    }
}

using System.Collections.Generic;

namespace RealTimeEmotionRecognition
{
    public interface IAffectRecognitionAsset 
    {
        string Name { get; }
        IEnumerable<AffectiveInformation> GetSample();
        IEnumerable<string> GetRecognizedAffectiveVariables();
    }
}

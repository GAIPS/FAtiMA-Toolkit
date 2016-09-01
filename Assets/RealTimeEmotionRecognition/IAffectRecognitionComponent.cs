using System.Collections.Generic;

namespace RealTimeEmotionRecognition
{
    public interface IAffectRecognitionComponent 
    {
        string Name { get; }
        IEnumerable<AffectiveInformation> GetSample();
        IEnumerable<string> GetRecognizedAffectiveVariables();
    }
}

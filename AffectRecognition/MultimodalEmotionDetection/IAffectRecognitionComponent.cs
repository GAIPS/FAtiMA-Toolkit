using System.Collections.Generic;

namespace MultimodalEmotionDetection
{
    public interface IAffectRecognitionComponent 
    {
        string Name { get; }
        IEnumerable<AffectiveInformation> GetSample();
        IEnumerable<string> GetRecognizedAffectiveVariables();
    }
}

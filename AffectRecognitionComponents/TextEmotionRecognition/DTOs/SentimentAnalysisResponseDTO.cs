using System;

namespace TextEmotionRecognition.DTOs
{
    [Serializable]
    public class SentimentAnalysisResponseDTO 
    {
        public ContentDTO[] Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMsg { get; set; }
        
    }
}

using System;

namespace TextEmotionRecognition.DTOs
{
    [Serializable]
    public class SentimentAnalysisRequestDTO
    {
        public string Text { get; set; }
        public string Lang { get; set; }
        public string LSA { get; set; }
        public string LDA { get; set; }
        public bool PostTagging { get; set; }
        public bool Dialogism { get; set; }
    }
}

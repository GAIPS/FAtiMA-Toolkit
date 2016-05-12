using System;

namespace TextEmotionRecognition.DTOs
{
    [Serializable]
    public class ContentDTO
    {
        public string Content { get; set; }
        public ValenceDTO[] Valences {get; set;}
        public ContentDTO[] InnerObjects { get; set; } 
    }
}

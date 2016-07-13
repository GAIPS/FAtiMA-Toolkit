using System;
namespace TextEmotionRecognition.DTOs
{
    [Serializable]
    public class ValenceDTO
    {
        public string Content { get; set; }
        public float Score { get; set; }
    }
}

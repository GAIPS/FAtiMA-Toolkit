using System.Collections.Generic;
using EmotionalAppraisal.DTOs;

namespace FAtiMAHTTPServer
{
    public class EmotionalStateDTO
    {
        public float Mood { get; set; }
        public List<EmotionDTO> Emotions { get; set; }
    }
}

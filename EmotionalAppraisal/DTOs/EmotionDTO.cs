using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmotionalAppraisal.DTOs
{
    public class EmotionDTO
    {
        public string Type { get; set; }
        public float Intensity { get; set; }
        public string CauseEvent { get; set; }
    }
}

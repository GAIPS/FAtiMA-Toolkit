using System;

namespace EmotionalAppraisal.DTOs
{
    [Serializable]
    public class GoalDTO
    {
        public string Name { get; set; }
        public float Significance { get; set; }
        public float Likelihood { get; set; }
    }
}

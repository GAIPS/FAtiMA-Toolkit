using EmotionalAppraisal.DTOs;
using System;
using WellFormedNames;

namespace EmotionalAppraisal
{
    [Serializable]
    public class Goal
    {
        public Name Name { get; set; }
        public float Significance { get; set; }
        public float Likelihood { get; set; }

        public Goal(GoalDTO goalDto)
        {
            this.Name = (Name)goalDto.Name;
            this.Significance = goalDto.Significance;
            this.Likelihood = goalDto.Likelihood;
        }

        public GoalDTO ToDto()
        {
            return new GoalDTO
            {
                Name = this.Name.ToString(),
                Significance = this.Significance,
                Likelihood = this.Likelihood
            };
        }
    }
}

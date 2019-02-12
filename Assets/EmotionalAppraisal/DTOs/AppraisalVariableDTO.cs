using System;
using WellFormedNames;

namespace EmotionalAppraisal.DTOs
{
    [Serializable]
    public class AppraisalVariableDTO
    {
        public string Name { get; set; }
        public Name  Value { get; set; }
        public Name Target { get; set; }
    }
}

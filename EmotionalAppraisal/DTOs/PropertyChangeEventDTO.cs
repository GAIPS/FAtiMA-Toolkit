using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmotionalAppraisal.DTOs
{
    public class PropertyChangeEventDTO : EventDTO
    {
        public string Subject { get; set; }
        public string Property { get; set; }
        public string NewValue { get; set; }
    }
}

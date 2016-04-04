using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmotionalAppraisal.DTOs
{
    public class ActionEventDTO : EventDTO
    {
        public string Subject { get; set; }
        public string Action { get; set; }
        public string Target { get; set; }
    }
}

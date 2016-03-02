using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmotionalAppraisal.DTOs
{
    public class EventDTO
    {
        public uint Id { get; set; }
        public string Event { get; set; }
        public ulong Time { get; set; }
    }
}

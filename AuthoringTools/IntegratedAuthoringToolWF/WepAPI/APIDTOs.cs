using EmotionalAppraisal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIWF
{
    public class CharacterDTO
    {
        public string Name { get; set; }
        public float Mood { get; set; }
        public IEnumerable<EmotionDTO> Emotions { get; set; }
        public ulong Tick { get; set; }
    }

    public class DecisionDTO
    {
        public string Action { get; set; }
        public string Target { get; set; }
        public string Utterance { get; set; }
        public float Utility { get; set; }
    }

    public class ExecuteRequestDTO
    {
        public string Subject { get; set; }

        public string Action { get; set; }

        public string Target { get; set; }
    }

}

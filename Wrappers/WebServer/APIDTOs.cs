using System;
using EmotionalAppraisal.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public class APIResourceDescriptionDTO
    {
        public string Resource { get; set; }
        public string URL { get; set; }
        public string Methods { get; set; }
    }

    public class CharacterDTO
    {
        public string Name { get; set; }
        public float Mood { get; set; }
        public IEnumerable<EmotionDTO> Emotions { get; set; }
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
    public class CreateScenarioRequestDTO
    {
        public string Scenario { get; set; }

        public string Assets { get; set; }
    }

}

using System;

namespace IntegratedAuthoringTool.DTOs
{
    [Serializable]
    public class DialogueStateActionDTO 
    {
        public Guid Id { get; set; }
        public string CurrentState { get; set; }
        public string NextState { get; set; }
        public string Meaning { get; set; }
        public string Style { get; set; }
        public string Utterance { get; set; }
    }
}

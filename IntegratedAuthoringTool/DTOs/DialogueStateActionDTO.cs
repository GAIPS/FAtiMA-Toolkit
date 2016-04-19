namespace IntegratedAuthoringTool.DTOs
{
    public class DialogueStateActionDTO
    {
        public string SpeakerType { get; set; }
        public string CurrentState { get; set; }
        public string Meaning { get; set; }
        public string NextState { get; set; }
        public string Utterance { get; set; }
    }
}

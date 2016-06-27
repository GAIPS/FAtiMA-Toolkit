using System;

namespace IntegratedAuthoringTool.DTOs
{
    [Serializable]
    public class DialogueStateActionDTO
    {
		[NonSerialized]
	    private Guid m_id;
		public Guid Id {
			get { return m_id; }
			set { m_id = value; }
		}
        public string CurrentState { get; set; }
        public string NextState { get; set; }
        public string Meaning { get; set; }
        public string Style { get; set; }
        public string Utterance { get; set; }
    }
}

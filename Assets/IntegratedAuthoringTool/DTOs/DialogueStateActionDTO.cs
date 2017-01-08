using System;

namespace IntegratedAuthoringTool.DTOs
{
    [Serializable]
    public class DialogueStateActionDTO
    {
		[NonSerialized]
	    private Guid _id;

		public Guid Id { get { return _id; } set { _id = value; } }
        public string CurrentState { get; set; }
        public string NextState { get; set; }
        public string[] Meaning { get; set; }
        public string[] Style { get; set; }
        public string Utterance { get; set; }
		public bool AutoFileName { get; set; }
		public string FileName { get; set; }
    }
}

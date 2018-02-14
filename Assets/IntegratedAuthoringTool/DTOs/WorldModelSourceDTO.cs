using System;

namespace IntegratedAuthoringTool.DTOs
{
    [Serializable]
    public class WorldModelSourceDTO
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string RelativePath { get; set; }
    }
}

using System;

namespace WorkingMemory
{
    [Serializable]
    public class WMEntryDTO
    {
        public string Name { get; set; }
		public string Value { get; set; }
    }
}

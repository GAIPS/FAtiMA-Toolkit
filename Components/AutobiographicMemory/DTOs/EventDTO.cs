namespace AutobiographicMemory.DTOs
{
    public class EventDTO
    {
        public uint Id { get; set; }
        public string Event { get; set; }
		public string Subject { get; set; }
		public ulong Time { get; set; }
    }
}

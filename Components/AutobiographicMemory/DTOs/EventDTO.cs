using System;

namespace AutobiographicMemory.DTOs
{
    /// <summary>
    /// Base Data Type Object Class for the representation of an Event
    /// </summary>
    [Serializable]
    public class EventDTO
    {
		/// <summary>
		/// The unique identifier of the event
		/// </summary>
        public uint Id { get; set; }
		/// <summary>
		/// The full string representation of this event
		/// </summary>
        public string Event { get; set; }
		/// <summary>
		/// The subject of the event (ie. Who is responsible responsible for this event)
		/// </summary>
		public string Subject { get; set; }
		/// <summary>
		/// The timestamp/tick in which this event was stored
		/// </summary>
		public ulong Time { get; set; }
    }
}

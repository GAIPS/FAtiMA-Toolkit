using System;

namespace AutobiographicMemory.DTOs
{
    /// <summary>
    /// Data Type Object Class for the representation of an Event referent to a property value change
    /// </summary>
    [Serializable]
    public class PropertyChangeEventDTO : EventDTO
    {
		/// <summary>
		/// The property that was modified.
		/// </summary>
        public string Property { get; set; }
		/// <summary>
		/// The new value that property has.
		/// </summary>
        public string NewValue { get; set; }
    }
}

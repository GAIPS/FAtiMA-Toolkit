namespace AutobiographicMemory.DTOs
{
    public class PropertyChangeEventDTO : EventDTO
    {
        public string Property { get; set; }
        public string NewValue { get; set; }
    }
}

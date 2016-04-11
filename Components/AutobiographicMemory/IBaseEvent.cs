using AutobiographicMemory.DTOs;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
{
	public interface IBaseEvent
	{
		Name EventType { get; }
		uint Id { get; }
		Name EventName { get; }
		ulong Timestamp { get; }
		Name Subject { get; }
		void LinkEmotion(string emotionType);

		EventDTO ToDTO();
	}
}
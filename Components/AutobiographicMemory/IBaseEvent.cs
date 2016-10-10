using System.Collections.Generic;
using AutobiographicMemory.DTOs;
using WellFormedNames;

namespace AutobiographicMemory
{
	public interface IBaseEvent
	{
		Name Type { get; }
		uint Id { get; }
		Name EventName { get; }
		ulong Timestamp { get; }
		Name Subject { get; }
		IEnumerable<string> LinkedEmotions { get; }

		void LinkEmotion(string emotionType);

		EventDTO ToDTO();
	}
}
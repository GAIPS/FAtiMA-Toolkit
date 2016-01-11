using System.Collections.Generic;

namespace AutobiographicMemory.Interfaces
{
	public interface IEventRecord : IEvent
	{
		uint Id { get; }
		IEnumerable<string> LinkedEmotions { get; }

		void LinkEmotion(string emotionType);
	}
}
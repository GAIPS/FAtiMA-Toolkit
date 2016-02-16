using System;
using System.Collections.Generic;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
{
	public interface IEventRecord
	{
		uint Id { get; }

		/// <summary>
		/// The type of the event
		/// </summary>
		string EventType { get; }

		/// <summary>
		/// How performed the action
		/// </summary>
		string Subject
		{
			get;
		}

		/// <summary>
		/// The target of the event
		/// </summary>
		string Target
		{
			get;
		}

		/// <summary>
		/// A WFN of the Action represented by this event
		/// </summary>
		Name Action { get; }

		/// <summary>
		/// The WFN representation of this event record
		/// </summary>
		Name EventName { get; }

		/// <summary>
		/// The time stamp in which the event occured
		/// </summary>
		DateTime Timestamp
		{
			get;
		}

		IEnumerable<string> LinkedEmotions { get; }
		void LinkEmotion(string emotionType);
	}
}
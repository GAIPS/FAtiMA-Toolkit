using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory.DTOs;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
{
	public sealed partial class AM
	{
		private abstract class BaseEvent: IBaseEvent
		{
			protected HashSet<string> m_linkedEmotions = new HashSet<string>();
			
			public uint Id { get; protected set; }

			public IEnumerable<string> LinkedEmotions
			{
				get
				{
					return m_linkedEmotions;
				}
			}

			public abstract Name EventType { get; }

			public Name Type { get; protected set; }

		    public Name Subject { get; protected set; }

			public ulong Timestamp { get; protected set; }

            public Name EventName { get; protected set; }

            protected BaseEvent(uint id, Name eventName, ulong timestamp)
			{
				Id = id;
	            Type = eventName.GetNTerm(1);
	            Subject = eventName.GetNTerm(2);
				Timestamp = timestamp;
				EventName = eventName;
			}

			public void LinkEmotion(string emotionType)
			{
				m_linkedEmotions.Add(emotionType);
			}

			public abstract EventDTO ToDTO();
		}
	}
}
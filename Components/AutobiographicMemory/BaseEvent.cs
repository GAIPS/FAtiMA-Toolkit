using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
{
	public sealed partial class AM
	{
		private class BaseEvent
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

			public string Type { get; protected set; }

		    public string Subject { get; protected set; }
            
		    public ulong Timestamp { get; protected set; }

            public Name EventName { get; protected set; }

            public BaseEvent(uint id, Name eventName, ulong timestamp)
			{
				Id = id;
			    Type = eventName.GetNTerm(1).ToString();
				Subject = eventName.GetNTerm(2).ToString();
				Timestamp = timestamp;
				EventName = eventName;
			}

			public void LinkEmotion(string emotionType)
			{
				m_linkedEmotions.Add(emotionType);
			}
		}
	}
}
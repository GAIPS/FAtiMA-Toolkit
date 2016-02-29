using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
{
	public sealed partial class AM
	{
		[Serializable]
		private class EventRecord : IEventRecord, ICustomSerialization
		{
			private HashSet<string> m_linkedEmotions = new HashSet<string>();
			
			public uint Id { get; private set; }

			public IEnumerable<string> LinkedEmotions
			{
				get
				{
					return m_linkedEmotions;
				}
			}

			public string EventType { get; private set; }

			public string Subject { get; private set; }

			public string Target { get; private set; }

			public ulong Timestamp { get; private set; }

			public Name EventObject{ get; private set; }

			public Name EventName { get; private set; }

			public EventRecord(uint id, Name eventName, ulong timestamp)
			{
				Id = id;
				EventType = eventName.GetNTerm(1).ToString();
				Subject = eventName.GetNTerm(2).ToString();
				EventObject = eventName.GetNTerm(3);

				var targetName = eventName.GetNTerm(4);
				Target = targetName == Name.NIL_SYMBOL ? null : targetName.ToString();
				Timestamp = timestamp;
				EventName = eventName;
			}

			public void LinkEmotion(string emotionType)
			{
				m_linkedEmotions.Add(emotionType);
			}

			public void GetObjectData(ISerializationData dataHolder)
			{
				dataHolder.SetValue("Id", Id);
				dataHolder.SetValue("EventType", EventType);
				dataHolder.SetValue("Subject", Subject);

				switch (EventType.ToUpperInvariant())
				{
					case "ACTION":
						dataHolder.SetValue("Action", EventObject);
						dataHolder.SetValue("Target", Target);
						break;
					case "PROPERTY-CHANGE":
						dataHolder.SetValue("Property", EventObject);
						dataHolder.SetValue("Value", Target);
						break;
					default:
						dataHolder.SetValue("EventObject", EventObject);
						dataHolder.SetValue("Target", Target);
						break;
				}

				dataHolder.SetValue("Timestamp", Timestamp);
				if (m_linkedEmotions.Count > 0)
				{
					dataHolder.SetValue("LinkedEmotions",m_linkedEmotions.ToArray());
				}
			}

			public void SetObjectData(ISerializationData dataHolder)
			{
				Id = dataHolder.GetValue<uint>("Id");
				EventType = dataHolder.GetValue<string>("EventType");
				Subject = dataHolder.GetValue<string>("Subject");

				switch (EventType.ToUpperInvariant())
				{
					case "ACTION":
						EventObject = dataHolder.GetValue<Name>("Action");
						Target = dataHolder.GetValue<string>("Target");
						break;
					case "PROPERTY-CHANGE":
						EventObject = dataHolder.GetValue<Name>("Property");
						Target = dataHolder.GetValue<string>("Value");
						break;
					default:
						EventObject = dataHolder.GetValue<Name>("EventObject");
						Target = dataHolder.GetValue<string>("Target");
						break;
				}

				Timestamp = dataHolder.GetValue<ulong>("Timestamp");

				if(m_linkedEmotions==null)
					m_linkedEmotions=new HashSet<string>();
				else
					m_linkedEmotions.Clear();
				var le = dataHolder.GetValue<string[]>("LinkedEmotions");
				if(le!=null && le.Length>0)
					m_linkedEmotions.UnionWith(le);

				EventName = Name.BuildName(EVT_NAME,(Name)EventType,(Name)Subject,EventObject,(Name)Target);
			}
		}
	}
}
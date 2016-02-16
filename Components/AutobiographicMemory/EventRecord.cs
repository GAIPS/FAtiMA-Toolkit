using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;
using GAIPS.Serialization;
using GAIPS.Serialization.SerializationGraph;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
{
	public sealed partial class AM
	{
		[Serializable]
		private class EventRecord : IEventRecord, ICustomSerialization
		{
			private HashSet<string> m_linkedEmotions = new HashSet<string>();
			/*
			public readonly Name EventName;
			public readonly SubstitutionSet CauseParameters;
			*/
			public EventRecord(uint id, Name eventName)
			{
				Id = id;
				EventType = eventName.GetNTerm(1).ToString();
				Subject = eventName.GetNTerm(2).ToString();
				Action = eventName.GetNTerm(3);

				var targetName = eventName.GetNTerm(4);
				Target = targetName == Name.NIL_SYMBOL ? null : targetName.ToString();
				Timestamp = DateTime.UtcNow;
				EventName = eventName;
			}

			public uint Id { get; private set; }

			public IEnumerable<string> LinkedEmotions
			{
				get
				{
					return m_linkedEmotions;
				}
			}

			public void LinkEmotion(string emotionType)
			{
				m_linkedEmotions.Add(emotionType);
			}

			public string EventType { get; private set; }

			public string Subject { get; private set; }

			public string Target { get; private set; }

			public DateTime Timestamp { get; private set; }

			public Name Action{ get; private set; }

			public Name EventName { get; private set; }

			public void GetObjectData(ISerializationData dataHolder)
			{
				dataHolder.SetValue("Id", Id);
				dataHolder.SetValue("EventType", EventType);
				dataHolder.SetValue("Subject", Subject);
				dataHolder.SetValue("Target", Target);
				dataHolder.SetValue("Action",Action);

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
				Target = dataHolder.GetValue<string>("Target");
				Action = dataHolder.GetValue<Name>("Action");
				Timestamp = dataHolder.GetValue<DateTime>("Timestamp");

				if(m_linkedEmotions==null)
					m_linkedEmotions=new HashSet<string>();
				else
					m_linkedEmotions.Clear();
				var le = dataHolder.GetValue<string[]>("LinkedEmotions");
				if(le!=null && le.Length>0)
					m_linkedEmotions.UnionWith(le);

				EventName = Name.BuildName(EVT_NAME,(Name)EventType,(Name)Subject,Action,(Name)Target);
			}
		}
	}
}
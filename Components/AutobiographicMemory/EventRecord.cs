using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory.Interfaces;
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
			private Dictionary<string, Name> m_parameters = null;
			private HashSet<string> m_linkedEmotions = new HashSet<string>();
			/*
			public readonly Name CauseName;
			public readonly SubstitutionSet CauseParameters;
			*/
			public EventRecord(uint id, IEvent evt, string perspective)
			{
				Id = id;
				Action = evt.Action;
				Subject = Name.ApplyPerspective(evt.Subject, perspective);
				Target = evt.Target == null ? null : Name.ApplyPerspective(evt.Target, perspective);
				Timestamp = evt.Timestamp;
				if (evt.Parameters != null && evt.Parameters.Any())
					m_parameters = evt.Parameters.ToDictionary(p => p.ParameterName, p => p.Value);

				CauseName = this.ToIdentifierName();
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

			public string Action { get; private set; }

			public string Subject { get; private set; }

			public string Target { get; private set; }

			public DateTime Timestamp { get; private set; }

			public IEnumerable<IEventParameter> Parameters
			{
				get
				{
					return m_parameters.Select(e => (IEventParameter)(new CauseParameter() { ParameterName = e.Key, Value = e.Value }));
				}
			}

			public Name this[string paramName]
			{
				get
				{
					if (m_parameters == null)
						return null;

					Name r;
					if (m_parameters.TryGetValue(paramName, out r))
						return r;
					return null;
				}
			}

			public bool HasParameters
			{
				get { return m_parameters != null; }
			}

			public Name CauseName { get; private set; }

			public SubstitutionSet BuildSubstitutions()
			{
				if (m_parameters == null)
					return null;

				var subs = m_parameters.Select(p => new Substitution(Name.BuildName("[" + p.Key + "]"), p.Value));
				return new SubstitutionSet(subs);
			}

			private class CauseParameter : IEventParameter
			{
				public CauseParameter()
				{
				}

				public CauseParameter(IEventParameter other)
				{
					ParameterName = other.ParameterName;
					Value = other.Value;
				}

				public string ParameterName { get; set; }

				public Name Value { get; set; }

				public object Clone()
				{
					return new CauseParameter(this);
				}
			}

			public void GetObjectData(ISerializationData dataHolder)
			{
				dataHolder.SetValue("Id", Id);
				dataHolder.SetValue("Action", Action);
				dataHolder.SetValue("Subject", Subject);
				dataHolder.SetValue("Target", Target);

				if (m_parameters != null)
				{
					var p = dataHolder.ParentGraph.CreateObjectData();
					foreach (var pair in m_parameters)
						p[pair.Key] = dataHolder.ParentGraph.BuildNode(pair.Value, typeof(Name));

					dataHolder.SetValueGraphNode("Parameters", p);
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
				Action = dataHolder.GetValue<string>("Action");
				Subject = dataHolder.GetValue<string>("Subject");
				Target = dataHolder.GetValue<string>("Target");
				Timestamp = dataHolder.GetValue<DateTime>("Timestamp");
				var p = dataHolder.GetValueGraphNode("Parameters");
				if (p != null)
					m_parameters = ((IObjectGraphNode)p).ToDictionary(f => f.FieldName, f => f.FieldNode.RebuildObject<Name>());

				if(m_linkedEmotions==null)
					m_linkedEmotions=new HashSet<string>();
				else
					m_linkedEmotions.Clear();
				var le = dataHolder.GetValue<string[]>("LinkedEmotions");
				if(le!=null && le.Length>0)
					m_linkedEmotions.UnionWith(le);

				CauseName = this.ToIdentifierName();
			}
		}
	}
}
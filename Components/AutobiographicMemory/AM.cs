using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory.Interfaces;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Collections;
using Utilities;

namespace AutobiographicMemory
{
	[Serializable]
	public sealed partial class AM : ICustomSerialization
	{
		//Indexes
		private uint m_eventGUIDCounter = 0;
		private Dictionary<uint, EventRecord> m_registry = new Dictionary<uint, EventRecord>();
		private NameSearchTree<List<uint>> m_typeIndexes = new NameSearchTree<List<uint>>();

		public void BindCalls(KB kb)
		{
			kb.RegistDynamicProperty(EVENT_PROPERTY_ID_TEMPLATE, EventIdPropertyCalculator);
			kb.RegistDynamicProperty(EVENT_PARAMETER_PROPERTY_TEMPLATE, EventParameterPropertyCalculator);
			kb.RegistDynamicProperty(EVENT_ELAPSED_TIME_PROPERTY_TEMPLATE, EventAgePropertyCalculator);
			kb.RegistDynamicProperty(LAST_EVENT_TEMPLATE,LastEventPropertyCalculator);
		}

		public IEventRecord RecordEvent(IEvent evt,string perspective)
		{
			var id = m_eventGUIDCounter++;
			var rec = new EventRecord(id,evt,perspective);
			AddRecord(rec);
			return rec;
		}

		public IEventRecord RecallEvent(uint eventId)
		{
			EventRecord r;
			if (m_registry.TryGetValue(eventId, out r))
				return r;
			return null;
		}

	    public IEnumerable<IEventRecord> RecallAllEvents()
	    {
	        return m_registry.Keys.Select(key => m_registry[key]).Cast<IEventRecord>().ToList();
	    }


	    private void AddRecord(EventRecord record)
		{
			m_registry.Add(record.Id,record);
			List<uint> ids;
			var name = record.CauseName;
			if (!m_typeIndexes.TryGetValue(name, out ids))
			{
				ids = new List<uint>();
				m_typeIndexes[name] = ids;
			}
			ids.Add(record.Id);
		}

		#region Dynamic Properties

		//Event
		private static readonly Name EVENT_TEMPLATE = Name.BuildName("EVENT([subject],[action],[target])");
		private static readonly Name EVENT_PROPERTY_ID_TEMPLATE = Name.BuildName((Name)"ID",EVENT_TEMPLATE);
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventIdPropertyCalculator(KB kb, IDictionary<string,Name> args, SubstitutionSet constraints)
		{
			Name subject,action,target;
			args.TryGetValue("subject", out subject);
			args.TryGetValue("action", out action);
			args.TryGetValue("target", out target);

			List<Pair<PrimitiveValue, SubstitutionSet>> results = new List<Pair<PrimitiveValue, SubstitutionSet>>();
			var key = Name.BuildName((Name)"Event", subject, action, target);
			foreach (var pair in m_typeIndexes.Unify(key, constraints))
			{
				foreach (var id in pair.Item1)
					results.Add(Tuples.Create((PrimitiveValue)id, new SubstitutionSet(pair.Item2)));
			}
			return results;
		}

		//EventParameter
		private static readonly Name EVENT_PARAMETER_PROPERTY_TEMPLATE = Name.BuildName("EventParameter([id],[paramName])");
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventParameterPropertyCalculator(KB kb, IDictionary<string, Name> args, SubstitutionSet constraints)
		{
			Name idName, paramName;
			args.TryGetValue("id",out idName);
			args.TryGetValue("paramName", out paramName);

			IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> result = Enumerable.Empty<Pair<PrimitiveValue, SubstitutionSet>>();
			if (idName.IsVariable)
			{
				foreach (var rec in m_registry.Values)
				{
					var newSub = new Substitution(idName, Name.BuildName(rec.Id));
					if(constraints.Conflicts(newSub))
						continue;

					var newConstraint = new SubstitutionSet(constraints);
					newConstraint.AddSubstitution(newSub);
					result = result.Union(GetParameter(rec, paramName, kb,newConstraint));
				}
			}
			else
			{
				foreach (var idPairs in kb.AskPossibleProperties(idName, constraints))
				{
					var idValue = idPairs.Item1;
					if (!idValue.TypeCode.IsUnsignedNumeric())
						continue;

					var rec = m_registry[idValue];
					if(rec==null)
						continue;

					result = result.Union(GetParameter(rec, paramName, kb,idPairs.Item2));
				}
			}

			return result;
		}

		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> GetParameter(EventRecord record, Name paramName, KB kb, SubstitutionSet constraints)
		{
			IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> result = Enumerable.Empty<Pair<PrimitiveValue, SubstitutionSet>>();
			if (paramName.IsVariable)
			{
				foreach (var p in record.Parameters)
				{
					var newSub = new Substitution(paramName, Name.BuildName(p.ParameterName));
					if (constraints.Conflicts(newSub))
						continue;

					var newConstraint = new SubstitutionSet(constraints);
					newConstraint.AddSubstitution(newSub);

					result = result.Union(kb.AskPossibleProperties(p.Value, newConstraint));
				}
			}
			else if(record.HasParameters)
			{
				var lookup = record.Parameters.ToDictionary(p => p.ParameterName, p => p.Value);
				foreach (var paramPairs in kb.AskPossibleProperties(paramName,constraints))
				{
					var paramValue = paramPairs.Item1;
					if (paramValue.TypeCode != TypeCode.String)
						continue;

					Name value;
					if (lookup.TryGetValue(paramValue, out value))
					{
						result = result.Union(kb.AskPossibleProperties(value, paramPairs.Item2));
					}
				}
			}
			return result;
		}

		//EventElapseTime
		private static readonly Name EVENT_ELAPSED_TIME_PROPERTY_TEMPLATE = Name.BuildName("EventElapsedTime([id])");
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventAgePropertyCalculator(KB kb, IDictionary<string,Name> args, SubstitutionSet constraints)
		{
			Name idName;
			if(!args.TryGetValue("id",out idName))
				yield break;

			if (idName.IsVariable)
			{
				foreach (var record in m_registry.Values)
				{
					var idSub = new Substitution(idName, Name.BuildName(record.Id));
					if (constraints.Conflicts(idSub))
						continue;

					var newSet = new SubstitutionSet(constraints);
					newSet.AddSubstitution(idSub);

					var value = (DateTime.UtcNow - record.Timestamp).TotalSeconds;
					yield return Tuples.Create((PrimitiveValue)value, newSet);
				}
				yield break;
			}

			foreach (var pair in kb.AskPossibleProperties(idName,constraints))
			{
				var idValue = pair.Item1;
				if(!idValue.TypeCode.IsUnsignedNumeric())
					continue;

				var record = m_registry[idValue];
				var value = (DateTime.UtcNow - record.Timestamp).TotalSeconds;
				yield return Tuples.Create((PrimitiveValue)value, pair.Item2);
			}
		}

		//LastEvent
		private static readonly Name LAST_EVENT_TEMPLATE = Name.BuildName((Name)"LastEvent", EVENT_TEMPLATE);
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> LastEventPropertyCalculator(KB kb, IDictionary<string, Name> args, SubstitutionSet constraints)
		{
			Name subject, action, target;
			args.TryGetValue("subject", out subject);
			args.TryGetValue("action", out action);
			args.TryGetValue("target", out target);

			var key = Name.BuildName((Name)"Event", subject, action, target);

			DateTime bestTime = DateTime.MinValue;
			Pair<PrimitiveValue, SubstitutionSet> best = null;
			foreach (var pair in m_typeIndexes.Unify(key, constraints))
			{
				var recentRecord = pair.Item1.Select(id => m_registry[id]).OrderByDescending(r => r.Timestamp).FirstOrDefault();
				if(recentRecord==null)
					continue;

				if (recentRecord.Timestamp > bestTime)
				{
					bestTime = recentRecord.Timestamp;
					best = Tuples.Create((PrimitiveValue)recentRecord.Id, pair.Item2);
				}
			}

			if (best == null)
				return Enumerable.Empty<Pair<PrimitiveValue, SubstitutionSet>>();

			return new[] {best};
		}

		#endregion

		public void GetObjectData(ISerializationData dataHolder)
		{
			dataHolder.SetValue("records", m_registry.Values.ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			m_eventGUIDCounter = 0;
			if (m_registry == null)
				m_registry = new Dictionary<uint, EventRecord>();
			else
				m_registry.Clear();

			if(m_typeIndexes==null)
				m_typeIndexes=new NameSearchTree<List<uint>>();
			else
				m_typeIndexes.Clear();

			var recs = dataHolder.GetValue<EventRecord[]>("records");
			if(recs==null)
				return;
			
			foreach (var r in recs)
			{
				if (m_eventGUIDCounter < r.Id)
					m_eventGUIDCounter = r.Id;

				AddRecord(r);
			}
			m_eventGUIDCounter++;
		}
	}
}
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
			kb.RegistDynamicProperty(EVENT_PROPERTY_ID_TEMPLATE, EventIdPropertyCalculator,new []{"subject","action","target"});
			kb.RegistDynamicProperty(EVENT_PARAMETER_PROPERTY_TEMPLATE, EventParameterPropertyCalculator,new []{"id","paramName"});
			kb.RegistDynamicProperty(EVENT_ELAPSED_TIME_PROPERTY_TEMPLATE, EventAgePropertyCalculator, new[] { "id" });
			kb.RegistDynamicProperty(LAST_EVENT_TEMPLATE, LastEventPropertyCalculator, new[] { "subject", "action", "target" });
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

		//Helpers
		private static IEnumerable<SubstitutionSet> AddSubstitutionToAllConstraints(IEnumerable<SubstitutionSet> constraints,
			Substitution newSub)
		{
			foreach (var c in constraints)
			{
				if(c.Conflicts(newSub))
					continue;

				var newConstraint = new SubstitutionSet(c);
				newConstraint.AddSubstitution(newSub);
				yield return newConstraint;
			}
		}

		//Event
		private static readonly Name EVENT_TEMPLATE = Name.BuildName("EVENT([subject],[action],[target])");
		private static readonly Name EVENT_PROPERTY_ID_TEMPLATE = Name.BuildName((Name)"ID",EVENT_TEMPLATE);
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventIdPropertyCalculator(KB kb, IDictionary<string,Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			Name subject = args["subject"];
			Name action = args["action"];
			Name target = args["target"];

			List<Pair<PrimitiveValue, SubstitutionSet>> results = new List<Pair<PrimitiveValue, SubstitutionSet>>();
			var key = Name.BuildName((Name)"Event", subject, action, target);
			foreach (var c in constraints)
			{
				foreach (var pair in m_typeIndexes.Unify(key, c))
				{
					foreach (var id in pair.Item1)
						results.Add(Tuples.Create((PrimitiveValue)id, new SubstitutionSet(pair.Item2)));
				}	
			}
			return results;
		}

		//EventParameter
		private static readonly Name EVENT_PARAMETER_PROPERTY_TEMPLATE = Name.BuildName("EventParameter([id],[paramName])");
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventParameterPropertyCalculator(KB kb, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			Name idName = args["id"];
			Name paramName = args["paramName"];

			IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> result = Enumerable.Empty<Pair<PrimitiveValue, SubstitutionSet>>();
			if (idName.IsVariable)
			{
				foreach (var rec in m_registry.Values)
				{
					var newSub = new Substitution(idName, Name.BuildName(rec.Id));
					var newConstraints = AddSubstitutionToAllConstraints(constraints, newSub);
					if(newConstraints.Any())
						result = result.Union(GetParameter(rec, paramName, kb, newConstraints));
				}
			}
			else
			{
				foreach (var idPairs in kb.AskPossibleProperties(idName, constraints))
				{
					var idValue = idPairs.Item1;
					if (idValue >= 0)
					{
						var rec = m_registry[idValue];
						if (rec == null)
							continue;

						result = result.Union(GetParameter(rec, paramName, kb, idPairs.Item2));
					}
				}
			}

			return result;
		}

		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> GetParameter(EventRecord record, Name paramName, KB kb, IEnumerable<SubstitutionSet> constraints)
		{
			if(!record.HasParameters)
				yield break;

			if (paramName.IsVariable)
			{
				foreach (var p in record.Parameters)
				{
					var newSub = new Substitution(paramName, Name.BuildName(p.ParameterName));
					var newContraints = AddSubstitutionToAllConstraints(constraints, newSub);
					if (newContraints.Any())
					{
						foreach (var pair in kb.AskPossibleProperties(p.Value, newContraints))
						{
							foreach (var s in pair.Item2)
								yield return Tuples.Create(pair.Item1, s);
						}
					}
				}
			}
			else
			{
				var lookup = record.Parameters.ToDictionary(p => p.ParameterName, p => p.Value);
				foreach (var paramPairs in kb.AskPossibleProperties(paramName,constraints))
				{
					var paramValue = paramPairs.Item1;
					if (paramValue.TypeCode != TypeCode.String)
						continue;

					Name value;
					if (!lookup.TryGetValue(paramValue, out value))
						continue;

					foreach (var pair in kb.AskPossibleProperties(value, paramPairs.Item2))
					{
						foreach (var s in pair.Item2)
							yield return Tuples.Create(pair.Item1, s);
					}
				}
			}
		}

		//EventElapseTime
		private static readonly Name EVENT_ELAPSED_TIME_PROPERTY_TEMPLATE = Name.BuildName("EventElapsedTime([id])");
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventAgePropertyCalculator(KB kb, IDictionary<string,Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			Name idName = args["id"];

			if (idName.IsVariable)
			{
				foreach (var record in m_registry.Values)
				{
					var idSub = new Substitution(idName, Name.BuildName(record.Id));
					foreach (var c in constraints)
					{
						if (c.Conflicts(idSub))
							continue;

						var newSet = new SubstitutionSet(c);
						newSet.AddSubstitution(idSub);

						var value = (DateTime.UtcNow - record.Timestamp).TotalSeconds;
						yield return Tuples.Create((PrimitiveValue)value, newSet);	
					}
				}
				yield break;
			}

			foreach (var pair in kb.AskPossibleProperties(idName,constraints))
			{
				var idValue = pair.Item1;
				if(!idValue.TypeCode.IsUnsignedNumeric())
					continue;

				var record = m_registry[idValue];
				var value = (PrimitiveValue)((DateTime.UtcNow - record.Timestamp).TotalSeconds);
				foreach (var c in pair.Item2)
					yield return Tuples.Create(value, c);
			}
		}

		//LastEvent
		private static readonly Name LAST_EVENT_TEMPLATE = Name.BuildName((Name)"LastEvent", EVENT_TEMPLATE);
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> LastEventPropertyCalculator(KB kb, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			Name subject = args["subject"];
			Name action = args["action"];
			Name target = args["target"];

			var key = Name.BuildName((Name)"Event", subject, action, target);

			DateTime bestTime = DateTime.MinValue;
			Pair<PrimitiveValue, SubstitutionSet> best = null;
			foreach (var pair in constraints.SelectMany(c => m_typeIndexes.Unify(key, c)))
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
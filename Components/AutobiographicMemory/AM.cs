using System;
using System.Collections.Generic;
using System.Linq;
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

		public ulong Tick { get; set; }

		public void BindCalls(KB kb)
		{
			kb.RegistDynamicProperty(EVENT_ID_PROPERTY_TEMPLATE, EventIdPropertyCalculator,new []{"type","subject","def","target"});
			kb.RegistDynamicProperty(EVENT_ELAPSED_TIME_PROPERTY_TEMPLATE, EventAgePropertyCalculator, new[] { "id" });
			kb.RegistDynamicProperty(LAST_EVENT_ID_PROPERTY_TEMPLATE, LastEventIdPropertyCalculator, new[] { "type", "subject", "def", "target" });
		}

		public IEventRecord RecordEvent(Name eventName, ulong timestamp)
		{
			AssertEventNameValidity(eventName);
			if (eventName.HasSelf())
				throw new Exception("Cannot record an event name containing \"Self\" keywords");

			var id = m_eventGUIDCounter++;
			var rec = new EventRecord(id, eventName,timestamp);
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
			var name = record.EventName;
			if (!m_typeIndexes.TryGetValue(name, out ids))
			{
				ids = new List<uint>();
				m_typeIndexes[name] = ids;
			}
			ids.Add(record.Id);
		}

		private static readonly Name EVT_NAME = (Name)"Event";

		public static void AssertEventNameValidity(Name name)
		{
			if (name.NumberOfTerms != 5)
				throw new Exception("A event name must contain 5 terms");

			if(!name.IsGrounded)
				throw new Exception("A event name cannot contain variables");

			if (name.GetNTerm(0) != EVT_NAME)
				throw new Exception("The first term of an event name must be \"Event\"");

			if (name.GetNTerm(1).IsComposed)
				throw new Exception("The second term of an event name cannot be a composed name.");

			if (name.GetNTerm(2).IsComposed)
				throw new Exception("The third term of an event name cannot be a composed name.");

			if (name.GetNTerm(4).IsComposed)
				throw new Exception("The fifth term of an event name cannot be a composed name.");
		}

		#region Dynamic Properties

		//Helpers
		//private static IEnumerable<SubstitutionSet> AddSubstitutionToAllConstraints(IEnumerable<SubstitutionSet> constraints,
		//	Substitution newSub)
		//{
		//	foreach (var c in constraints)
		//	{
		//		if(c.Conflicts(newSub))
		//			continue;

		//		var newConstraint = new SubstitutionSet(c);
		//		newConstraint.AddSubstitution(newSub);
		//		yield return newConstraint;
		//	}
		//}

		private static Name GetArgument(IDictionary<string, Name> args, string argName)
		{
			Name result;
			if (!args.TryGetValue(argName, out result))
				return Name.UNIVERSAL_SYMBOL;
			return result;
		}

		//Event
		private static readonly Name EVENT_ID_PROPERTY_TEMPLATE = Name.BuildName("EventId([type],[subject],[def],[target])");
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventIdPropertyCalculator(KB kb, Name perspective, IDictionary<string,Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			List<Pair<PrimitiveValue, SubstitutionSet>> results = new List<Pair<PrimitiveValue, SubstitutionSet>>();
			if (!perspective.Match(Name.SELF_SYMBOL))
				return results;

			Name type = GetArgument(args, "type");
			Name subject = GetArgument(args, "subject");
			Name def = GetArgument(args, "def");
			Name target = GetArgument(args,"target");
			
			var key = Name.BuildName(EVT_NAME, type, subject, def, target);
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

		//EventElapseTime
		private static readonly Name EVENT_ELAPSED_TIME_PROPERTY_TEMPLATE = Name.BuildName("EventElapsedTime([id])");
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventAgePropertyCalculator(KB kb, Name perspective, IDictionary<string,Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			if(!perspective.Match(Name.SELF_SYMBOL))
				yield break;

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

						var value = Tick - record.Timestamp;
						yield return Tuples.Create((PrimitiveValue)value, newSet);	
					}
				}
				yield break;
			}

			foreach (var pair in kb.AskPossibleProperties(idName,perspective,constraints))
			{
				var idValue = pair.Item1;
				if(!idValue.TypeCode.IsUnsignedNumeric())
					continue;

				var record = m_registry[idValue];
				var value = (PrimitiveValue) (Tick - record.Timestamp);
				foreach (var c in pair.Item2)
					yield return Tuples.Create(value, c);
			}
		}

		//LastEvent
		private static readonly Name LAST_EVENT_ID_PROPERTY_TEMPLATE = Name.BuildName("LastEventId([type],[subject],[def],[target])");
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> LastEventIdPropertyCalculator(KB kb, Name perspective, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			if(!perspective.Match(Name.SELF_SYMBOL))
				return Enumerable.Empty<Pair<PrimitiveValue, SubstitutionSet>>();

			Name type = GetArgument(args, "type");
			Name subject = GetArgument(args, "subject");
			Name def = GetArgument(args, "def");
			Name target = GetArgument(args, "target");

			var key = Name.BuildName(EVT_NAME, type, subject, def, target);

			ulong bestTime = 0;
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
			dataHolder.SetValue("Tick",Tick);
			dataHolder.SetValue("records", m_registry.Values.ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			Tick = dataHolder.GetValue<ulong>("Tick");

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
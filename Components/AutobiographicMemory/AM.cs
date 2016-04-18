using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory.DTOs;
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
		private Dictionary<uint, BaseEvent> m_registry = new Dictionary<uint, BaseEvent>();
		private NameSearchTree<List<uint>> m_typeIndexes = new NameSearchTree<List<uint>>();

		public ulong Tick { get; set; }

		public void BindCalls(KB kb)
		{
			kb.RegistDynamicProperty(EVENT_ID_PROPERTY_TEMPLATE, EventIdPropertyCalculator,new []{"type","subject","def","target"});
			kb.RegistDynamicProperty(EVENT_ELAPSED_TIME_PROPERTY_TEMPLATE, EventAgePropertyCalculator, new[] { "id" });
			kb.RegistDynamicProperty(LAST_EVENT_ID_PROPERTY_TEMPLATE, LastEventIdPropertyCalculator, new[] { "type", "subject", "def", "target" });
		}

		public IBaseEvent RecordEvent(EventDTO dto)
		{
			return RecordEvent(BuildEventNameFromDTO(dto), dto.Time);
		}

		public IBaseEvent RecordEvent(Name eventName, ulong timestamp)
		{
		    return this.SaveEventHelper(m_eventGUIDCounter++, eventName, timestamp);
		}

		public IBaseEvent UpdateEvent(EventDTO dto)
		{
			var evtName = BuildEventNameFromDTO(dto);
			return UpdateEvent(dto.Id, evtName, dto.Time);
		}

		public IBaseEvent UpdateEvent(uint eventId, Name eventName, ulong timestamp)
        {
            m_registry.Remove(eventId);
            return this.SaveEventHelper(eventId, eventName, timestamp);
        }

	    private BaseEvent SaveEventHelper(uint eventId, Name eventName, ulong timestamp)
	    {
            AssertEventNameValidity(eventName);
			if (eventName.HasSelf())
				throw new Exception("Cannot record an event name containing \"Self\" keywords");

			BaseEvent eventRecord;
            if (ActionEvent.IsActionEvent(eventName))
            {
                eventRecord = new ActionEvent(eventId, eventName, timestamp);
            }
			else if (PropertyChangeEvent.IsPropertyChangeEvent(eventName))
		    {
			    eventRecord = new PropertyChangeEvent(eventId, eventName, timestamp);
		    }else
				throw new Exception("Unknown Event Type");

			AddRecord(eventRecord);
		    return eventRecord;
	    }

		private Name BuildEventNameFromDTO(EventDTO evt)
		{
			var actionEvent = evt as ActionEventDTO;
			if (actionEvent != null)
			{
				return Name.BuildName(
					(Name)"Event",
					(Name)"Action",
					(Name)actionEvent.Subject,
					(Name)actionEvent.Action,
					(Name)actionEvent.Target);
			}

			var pcEvent = evt as PropertyChangeEventDTO;
			if (pcEvent != null)
			{
				return Name.BuildName(
				(Name)"Event",
				(Name)"Property-Change",
				(Name)pcEvent.Subject,
				(Name)pcEvent.Property,
				(Name)pcEvent.NewValue);
			}

			throw new Exception("Unknown Event DTO");
		}

		public IBaseEvent RecallEvent(uint eventId)
		{
			BaseEvent r;
		    if (m_registry.TryGetValue(eventId, out r))
		    {
			    return r;
		    }
	        return null;
		}

	    public IEnumerable<IBaseEvent> RecallAllEvents()
	    {
	        return m_registry.Keys.Select(key => m_registry[key]).Cast<IBaseEvent>().ToList();
	    }

	    public void ForgetEvent(uint eventId)
	    {
		    BaseEvent evt;
			if(!m_registry.TryGetValue(eventId,out evt))
				return;

			m_registry.Remove(eventId);
		    var evts = m_typeIndexes[evt.EventName];
		    evts.Remove(eventId);
		    if (evts.Count == 0)
				m_typeIndexes.Remove(evt.EventName);
	    }

	    private void AddRecord(BaseEvent record)
		{
			m_registry.Add(record.Id, record);
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

		public void SwapPerspective(Name oldPerspective, Name newPerspective)
		{
			foreach (var key in m_registry.Keys)
			{
				var evt = m_registry[key];
				m_registry[key] = evt.SwapPerspective(oldPerspective, newPerspective);
			}

			var newIndexes = new NameSearchTree<List<uint>>();
			foreach (var p in m_typeIndexes)
			{
				var k = p.Key.SwapPerspective(oldPerspective, newPerspective);
				newIndexes[k] = p.Value;
			}
			m_typeIndexes = newIndexes;
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

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("Tick",Tick);
			dataHolder.SetValue("records", m_registry.Values.ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			Tick = dataHolder.GetValue<ulong>("Tick");

			m_eventGUIDCounter = 0;
			if (m_registry == null)
				m_registry = new Dictionary<uint, BaseEvent>();
			else
				m_registry.Clear();

			if(m_typeIndexes==null)
				m_typeIndexes=new NameSearchTree<List<uint>>();
			else
				m_typeIndexes.Clear();

			var recs = dataHolder.GetValue<BaseEvent[]>("records");
			if (recs == null)
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
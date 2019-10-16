using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory.DTOs;
using SerializationUtilities;
using KnowledgeBase;
using Utilities;
using WellFormedNames;
using WellFormedNames.Collections;

namespace AutobiographicMemory
{
	//TODO improve LastEventId efficiency, by caching the last recorded events (cache should be dumped, if a new event is recorded with a greater timestamp that the ones in cache)

	[Serializable]
	public sealed partial class AM : ICustomSerialization
	{
		//Indexes
		private uint m_eventGUIDCounter = 0;
		private Dictionary<uint, BaseEvent> m_registry = new Dictionary<uint, BaseEvent>();
		private NameSearchTree<List<uint>> m_typeIndexes = new NameSearchTree<List<uint>>();

		public ulong Tick { get; set; }

		public void BindToRegistry(IDynamicPropertiesRegistry registry)
		{
            registry.RegistDynamicProperty(EVENT_ID_PROPERTY_NAME, "", EventIdPropertyCalculator);
			registry.RegistDynamicProperty(EVENT_ELAPSED_TIME_PROPERTY_NAME, "", EventAgePropertyCalculator);
			registry.RegistDynamicProperty(LAST_EVENT_ID_PROPERTY_NAME, "", LastEventIdPropertyCalculator);
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
			if (eventName.HasSelf() && !eventName.ToString().Contains("Property-Change"))
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
				var state = (actionEvent.ActionState == ActionState.Start) ? AMConsts.ACTION_START : AMConsts.ACTION_END;
				return Name.BuildName(
					(Name)AMConsts.EVENT,
					(Name)state,
					(Name)actionEvent.Subject,
					(Name)actionEvent.Action,
					(Name)actionEvent.Target);
			}

			var pcEvent = evt as PropertyChangeEventDTO;
			if (pcEvent != null)
			{
				return Name.BuildName(
				(Name)AMConsts.EVENT,
				(Name)AMConsts.PROPERTY_CHANGE,
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

		public static void AssertEventNameValidity(Name name)
		{
			if (name.NumberOfTerms != 5)
				throw new Exception("A event name must contain 5 terms");

			if(!name.IsGrounded)
				throw new Exception("A event name cannot contain variables");

			if (name.GetNTerm(0) != (Name)AMConsts.EVENT)
				throw new Exception("The first term of an event name must be "+ AMConsts.EVENT);

			if (name.GetNTerm(1).IsComposed)
				throw new Exception("The second term of an event name cannot be a composed name.");

			if (name.GetNTerm(2).IsComposed)
				throw new Exception("The third term of an event name cannot be a composed name.");

			if (name.GetNTerm(4).IsComposed && name.GetNTerm(1).ToString() != "Property-Change")
				throw new Exception("The fifth term of an event name cannot be a composed name.");
		}

		public void SwapPerspective(Name oldPerspective, Name newPerspective)
		{
			var currentKeys = m_registry.Keys.ToArray();
			foreach (var key in currentKeys)
			{
				var evt = m_registry[key];
				m_registry[key] = evt.SwapPerspective(oldPerspective, newPerspective);
			}

			var newIndexes = new NameSearchTree<List<uint>>();
			foreach (var p in m_typeIndexes)
			{
				var k = p.Key.SwapTerms(oldPerspective, newPerspective);
				newIndexes[k] = p.Value;
			}
			m_typeIndexes = newIndexes;
		}

		#region Dynamic Properties

		//Event
		private static readonly Name EVENT_ID_PROPERTY_NAME = Name.BuildName("EventId");
		private IEnumerable<DynamicPropertyResult> EventIdPropertyCalculator(IQueryContext context, Name type, Name subject, Name def, Name target)
		{
            
			var key = Name.BuildName((Name)AMConsts.EVENT, type, subject, def, target);
			foreach (var c in context.Constraints)
			{
                var unifiedSet = m_typeIndexes.Unify(key, c);
                foreach (var pair in unifiedSet)
				{
					foreach (var id in pair.Item1)
						yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(id)), new SubstitutionSet(pair.Item2));
				}

			    if (!unifiedSet.Any())
			    {
			        yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(-1)), c);
			    }
			}
		}

		//EventElapseTime
		private static readonly Name EVENT_ELAPSED_TIME_PROPERTY_NAME = Name.BuildName("EventElapsedTime");
		private IEnumerable<DynamicPropertyResult> EventAgePropertyCalculator(IQueryContext context, Name id)
		{
		
			if (id.IsVariable)
			{
				foreach (var record in m_registry.Values)
				{
					var idSub = new Substitution(id, new ComplexValue(Name.BuildName(record.Id)));
					foreach (var c in context.Constraints)
					{
						if (c.Conflicts(idSub))
							continue;

						var newSet = new SubstitutionSet(c);
						newSet.AddSubstitution(idSub);

						var value = Tick - record.Timestamp;
						yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(value)), newSet);
					}
				}
				yield break;
			}

			foreach (var pair in context.AskPossibleProperties(id))
			{
				uint idValue;
				if(!pair.Item1.Value.TryConvertToValue(out idValue))
					continue;

				var record = m_registry[idValue];
				var value = (Tick - record.Timestamp);
				foreach (var c in pair.Item2)
					yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(value)), c);
			}
		}

		//LastEvent
		private static readonly Name LAST_EVENT_ID_PROPERTY_NAME = Name.BuildName("LastEventId");
        private IEnumerable<DynamicPropertyResult> LastEventIdPropertyCalculator(IQueryContext context, Name type, Name subject, Name def, Name target)
        {

            ulong min = ulong.MinValue;


            var allEvents = m_registry.Values;

           
            foreach(var eve in allEvents)
            {
                if (eve.Timestamp >= min)
                    min = eve.Timestamp;
            }

            var lastEvents =  m_registry.Where(x => x.Value.Timestamp == min);



    
            var lastIndexes = new NameSearchTree<List<uint>>();

          //  Now we get a similar object as m_typeIndexes but only with the last events
            foreach (var ind in m_typeIndexes)
                foreach (var eve in lastEvents)
                    if (ind.Key.ToString() == eve.Value.EventName.ToString() && !lastIndexes.Contains(ind))
                        lastIndexes.Add(ind);



            // Now that we have the events of the last tick we can now ask the context 

            var key = Name.BuildName((Name)AMConsts.EVENT, type, subject, def, target);
                     foreach (var c in context.Constraints)
                    {
                         var unifiedSet = lastIndexes.Unify(key, c);
                         foreach (var pair in unifiedSet)
                         {
                             foreach (var id in pair.Item1)
                                 yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(id)), new SubstitutionSet(pair.Item2));
                         }


                        if (!unifiedSet.Any())
                        {
                            yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(-1)), c);
                        }
                     }
                     
           
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
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
	public sealed class AM : ICustomSerialization
	{
		//Indexes
		private uint m_eventGUIDCounter = 0;
		private Dictionary<uint, EventRecord> m_registry = new Dictionary<uint, EventRecord>();
		private NameSearchTree<List<uint>> m_typeIndexes = new NameSearchTree<List<uint>>();

		public void BindCalls(KB kb)
		{
			kb.RegistDynamicProperty(EVENT_PROPERTY_ID_TEMPLATE, EventIdPropertyCalculator);
			kb.RegistDynamicProperty(EVENT_PARAMETER_PROPERTY_TEMPLATE, EventParameterPropertyCalculator);
			kb.RegistDynamicProperty(EVENT_AGE_PROPERTY_TEMPLATE, EventAgePropertyCalculator);
		}

		public uint RecordEvent(Cause cause, string linkedEmotion)
		{
			var id = m_eventGUIDCounter++;
			var rec = new EventRecord(id,cause,linkedEmotion);
			AddRecord(rec);
			return id;
		}

		private void AddRecord(EventRecord record)
		{
			m_registry.Add(record.Id,record);
			List<uint> ids;
			if (!m_typeIndexes.TryGetValue(record.Cause.CauseName, out ids))
			{
				ids = new List<uint>();
				m_typeIndexes[record.Cause.CauseName] = ids;
			}
			ids.Add(record.Id);
		}

		#region Dynamic Properties

		//Event
		private static readonly Name EVENT_PROPERTY_ID_TEMPLATE = Name.BuildName("EVENT([subject],[action],[target])");
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventIdPropertyCalculator(KB kb, SubstitutionSet args, SubstitutionSet constraints)
		{
			var key = EVENT_PROPERTY_ID_TEMPLATE.MakeGround(args);
			return m_typeIndexes.Unify(key, constraints).SelectMany(p => p.Item1.Select(id => Tuples.Create((PrimitiveValue) id, p.Item2)));
		}

		//EventParameter
		private static readonly Name EVENT_PARAMETER_PROPERTY_TEMPLATE = Name.BuildName("EventParameter([id],[paramName])");
		private static readonly Name EVENT_PARAMETER_PROPERTY_ID_VARIABLE = Name.BuildName("[id]");
		private static readonly Name EVENT_PARAMETER_PROPERTY_PARAM_NAME_VARIABLE = Name.BuildName("[paramName]");
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventParameterPropertyCalculator(KB kb, SubstitutionSet args, SubstitutionSet constraints)
		{
			var idName = args[EVENT_PARAMETER_PROPERTY_ID_VARIABLE];
			var paramName = args[EVENT_PARAMETER_PROPERTY_PARAM_NAME_VARIABLE];

			if (idName.IsConstant)
			{
				var idValue = kb.AskProperty(idName,constraints);
				if (idValue == null || !idValue.GetTypeCode().IsUnsignedNumeric())
					yield break;

				var record = m_registry[idValue];

				if (paramName.IsConstant)
				{
					//Known event id and param name. Return param value.
					var paramValue = kb.AskProperty(paramName,constraints);
					if (paramValue == null || paramValue.GetTypeCode() != TypeCode.String)
						yield break;

					var valueName = record.Cause.CauseParameters[Name.BuildName("[" + paramValue + "]")];
					if (valueName == null)
						yield break;

					var value = kb.AskProperty(valueName,constraints);
					if (value == null)
						yield break;

					yield return Tuples.Create(value, constraints);
				}
				else
				{
					if(!paramName.IsVariable)
						throw new Exception("paramName argument needs to be a constant value or a variable");

					//Known event id and unknown param name. Return all parameters for that event
					foreach (var r in GetAllParameters(record, paramName, kb, constraints))
						yield return r;
				}
				yield break;
			}

			if (!idName.IsVariable)
				throw new Exception("id argument needs to be a constant value or a variable");

			if (paramName.IsConstant)
			{
				//Known param name and unknown event id. Return all events that have the know param.
				var paramValue = kb.AskProperty(paramName,constraints);
				if (paramValue == null || paramValue.GetTypeCode() != TypeCode.String)
					yield break;

				var searchVar = Name.BuildName("[" + paramValue + "]");
				foreach (var entry in m_registry.Values)
				{
					var valueName = entry.Cause.CauseParameters[searchVar];
					if (valueName == null)
						continue;

					var value = kb.AskProperty(valueName,constraints);
					if (value == null)
						continue;

					var newSub = new Substitution(idName,Name.BuildName(entry.Id));
					if(constraints.Conflicts(newSub))
						continue;

					var newSet = new SubstitutionSet(constraints);
					newSet.AddSubstitution(newSub);

					yield return Tuples.Create(value, newSet);
				}

				yield break;
			}

			if (!paramName.IsVariable)
				throw new Exception("paramName argument needs to be a constant value or a variable");

			//Unknown event id and param name. Return all parameters for all events
			foreach (var entry in m_registry.Values)
			{
				var idSub = new Substitution(idName, Name.BuildName(entry.Id));
				if (constraints.Conflicts(idSub))
					continue;

				var newSet = new SubstitutionSet(constraints);
				newSet.AddSubstitution(idSub);

				foreach (var result in GetAllParameters(entry, paramName, kb, newSet))
					yield return result;
			}
		}

		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> GetAllParameters(EventRecord record, Name paramVariable, KB kb,
			SubstitutionSet constraints)
		{
			foreach (var s in record.Cause.CauseParameters)
			{
				var value = kb.AskProperty(s.Value, constraints);
				if (value == null)
					continue;

				var newSub = new Substitution(paramVariable, VariableToSymbol(s.Variable));
				if (constraints.Conflicts(newSub))
					continue;

				var newSet = new SubstitutionSet(constraints);
				newSet.AddSubstitution(newSub);
				yield return Tuples.Create(value, newSet);
			}
		}

		private static Name VariableToSymbol(Name variable)
		{
			var str = variable.ToString();
			return Name.BuildName(str.Substring(1, str.Length - 2));
		}

		//EventAge
		private static readonly Name EVENT_AGE_PROPERTY_TEMPLATE = Name.BuildName("EventAge([id])");
		private static readonly Name EVENT_AGE_PROPERTY_ID_VARIABLE = Name.BuildName("[id]");
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EventAgePropertyCalculator(KB kb, SubstitutionSet args, SubstitutionSet constraints)
		{
			var idName = args[EVENT_AGE_PROPERTY_ID_VARIABLE];
			if (idName.IsConstant)
			{
				var idValue = kb.AskProperty(idName, constraints);
				if (idValue == null || !idValue.GetTypeCode().IsUnsignedNumeric())
					yield break;

				var record = m_registry[idValue];
				var value = (DateTime.UtcNow - record.Cause.CauseTimestamp).TotalSeconds;
				yield return Tuples.Create((PrimitiveValue) value, constraints);
				yield break;
			}

			if (!idName.IsVariable)
				throw new Exception("id argument needs to be a constant value or a variable");

			foreach (var record in m_registry.Values)
			{
				var idSub = new Substitution(idName, Name.BuildName(record.Id));
				if (constraints.Conflicts(idSub))
					continue;

				var newSet = new SubstitutionSet(constraints);
				newSet.AddSubstitution(idSub);

				var value = (DateTime.UtcNow - record.Cause.CauseTimestamp).TotalSeconds;
				yield return Tuples.Create((PrimitiveValue)value, newSet);
			}

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

		[Serializable]
		private class EventRecord
		{
			public readonly uint Id;
			public readonly Cause Cause;
			public readonly string EmotionType;

			public EventRecord(uint id, Cause cause, string emotionType)
			{
				Id = id;
				Cause = cause;
				EmotionType = emotionType;
			}
		}
	}
}
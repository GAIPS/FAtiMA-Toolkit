using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using KnowledgeBase.DTOs.Conditions;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	using VarDomain = Dictionary<Name, HashSet<Name>>;

	[Serializable]
	public sealed class ConditionSet : IEnumerable<Condition>, IConditionEvaluator, ICustomSerialization
	{
		public LogicalQuantifier Quantifier { get; private set; }

		[NonSerialized]
		private HashSet<Condition> m_conditions;

		public int Count {
			get { return m_conditions==null?0:m_conditions.Count; }
		}

		public ConditionSet() : this(LogicalQuantifier.Existential,null)
		{
		}

		public ConditionSet(IEnumerable<Condition> conditions) : this(LogicalQuantifier.Existential,conditions){}

		public ConditionSet(LogicalQuantifier quantifier, IEnumerable<Condition> conditions)
		{
			Quantifier = quantifier;
			if(conditions!=null)
				m_conditions = new HashSet<Condition>(conditions);
		}

		public ConditionSet(ConditionSetDTO dto) : this(dto.Quantifier,dto.Set?.Select(c => Condition.Parse(c.Condition)))
		{

		}

		public ConditionSet Add(Condition condition)
		{
			if (m_conditions!=null && m_conditions.Contains(condition))
				return this;

			if(m_conditions==null)
				return new ConditionSet(Quantifier, new []{ condition });

			return new ConditionSet(Quantifier, m_conditions.Prepend(condition));
		}

		public ConditionSet Remove(Condition condition)
		{
			if (m_conditions==null || !m_conditions.Contains(condition))
				return this;

			var c = new ConditionSet(Quantifier,m_conditions);
			if(m_conditions!=null)
				c.m_conditions.Remove(condition);
			return c;
		}

		public ConditionSet SetQuantifier(LogicalQuantifier quantifier)
		{
			if (quantifier == Quantifier)
				return this;

			return new ConditionSet(quantifier,m_conditions);
		}

		public IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			if (constraints == null || !constraints.Any())
				constraints = new[] {new SubstitutionSet()};

			if (m_conditions == null)
				return constraints;

			if(Quantifier == LogicalQuantifier.Existential)
				return ExistentialEvaluate(kb,perspective, constraints);

			return UniversalEvaluate(kb,perspective,constraints);
		}

		public bool Evaluate(KB kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			return UnifyEvaluate(kb,perspective, constraints).Any();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<Condition> GetEnumerator()
		{
			return m_conditions.GetEnumerator();
		}

		public override int GetHashCode()
		{
			const int EMPTY_HASH = 0x33b65725;
			return m_conditions.Aggregate(EMPTY_HASH, (current, c) => current ^ c.GetHashCode());
		}

		public override bool Equals(object obj)
		{
			ConditionSet evaluatorSet = obj as ConditionSet;
			if(evaluatorSet==null)
				return false;

			return m_conditions.SetEquals(evaluatorSet.m_conditions);
		}

		#region Evaluation Methods

		private IEnumerable<SubstitutionSet> ExistentialEvaluate(KB kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			List<SubstitutionSet> sets = new List<SubstitutionSet>();
			List<SubstitutionSet> aux = new List<SubstitutionSet>();
			sets.AddRange(constraints.Select(c => new SubstitutionSet(c)));
			foreach (var c in m_conditions)
			{
				aux.AddRange(c.UnifyEvaluate(kb,perspective, sets));
				Util.Swap(ref sets, ref aux);
				aux.Clear();
				if (sets.Count == 0)
					break;
			}

			return sets;
		}

		private IEnumerable<SubstitutionSet> UniversalEvaluate(KB kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			var lastDomain = BuildDomain(constraints);
			List<SubstitutionSet> sets = new List<SubstitutionSet>(constraints);
			List<SubstitutionSet> aux = new List<SubstitutionSet>();
			try
			{
				foreach (var c in m_conditions)
				{
					aux.AddRange(c.UnifyEvaluate(kb,perspective, sets));
					Util.Swap(ref sets, ref aux);
					aux.Clear();
					var newDomain = BuildDomain(sets);
					if (!CompareDomains(lastDomain, newDomain))
					{
						RecycleDomain(newDomain);
						return aux;
					}

					RecycleDomain(lastDomain);
					lastDomain = newDomain;
				}

				return sets;
			}
			finally
			{
				RecycleDomain(lastDomain);
			}
		}

		private bool CompareDomains(VarDomain oldDomain, VarDomain newDomain)
		{
			foreach (var o in oldDomain)
			{
				HashSet<Name> domain;
				if (!newDomain.TryGetValue(o.Key, out domain))
					return false;

				if (o.Value.Count != domain.Count)
					return false;
			}
			return true;
		}

		private VarDomain BuildDomain(IEnumerable<SubstitutionSet> constraints)
		{
			var domain = ObjectPool<Dictionary<Name, HashSet<Name>>>.GetObject();
			foreach (var sub in constraints.SelectMany(s => s))
			{
				HashSet<Name> set;
				if (!domain.TryGetValue(sub.Variable, out set))
				{
					set = ObjectPool<HashSet<Name>>.GetObject();
					domain[sub.Variable] = set;
				}

				set.Add(sub.Value);
			}
			return domain;
		}

		private void RecycleDomain(VarDomain domain)
		{
			foreach (var set in domain.Values)
			{
				set.Clear();
				ObjectPool<HashSet<Name>>.Recycle(set);
			}
			domain.Clear();
			ObjectPool<Dictionary<Name, HashSet<Name>>>.Recycle(domain);
		}

		#endregion

		public ConditionSetDTO ToDTO()
		{
			return new ConditionSetDTO() {Quantifier = this.Quantifier,Set = m_conditions.Select(c => c.ToDTO()).ToArray()};
		}

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			SerializationServices.PopulateWithFieldData(dataHolder,this,true,false);
			dataHolder.SetValue("Set", (m_conditions ?? Enumerable.Empty<Condition>()).ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			object o = this;
			SerializationServices.ExtractFromFieldData(dataHolder,ref o,true);
			var set = dataHolder.GetValue<Condition[]>("Set");
			if(set!=null)
				m_conditions = new HashSet<Condition>(set);
		}
	}
}
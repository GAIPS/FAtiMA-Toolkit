using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	using VarDomain = Dictionary<Name, HashSet<Name>>;

	[Serializable]
	public sealed class ConditionEvaluatorSet : IEnumerable<Condition>, IConditionEvaluator, ICustomSerialization
	{
		private HashSet<Condition> m_conditions;
		public LogicalQuantifier Quantifier { get; private set; }

		public int Count {
			get { return m_conditions==null?0:m_conditions.Count; }
		}

		public ConditionEvaluatorSet() : this(LogicalQuantifier.Existential,null)
		{
		}

		public ConditionEvaluatorSet(IEnumerable<Condition> conditions) : this(LogicalQuantifier.Existential,conditions){}

		public ConditionEvaluatorSet(LogicalQuantifier quantifier, IEnumerable<Condition> conditions)
		{
			Quantifier = quantifier;
			if(conditions!=null)
				m_conditions = new HashSet<Condition>(conditions);
		}

		public ConditionEvaluatorSet Add(Condition condition)
		{
			if (m_conditions!=null && m_conditions.Contains(condition))
				return this;

			if(m_conditions==null)
				return new ConditionEvaluatorSet(Quantifier, new []{ condition });

			return new ConditionEvaluatorSet(Quantifier, m_conditions.Prepend(condition));
		}

		public ConditionEvaluatorSet Remove(Condition condition)
		{
			if (m_conditions==null || !m_conditions.Contains(condition))
				return this;

			var c = new ConditionEvaluatorSet(Quantifier,m_conditions);
			if(m_conditions!=null)
				c.m_conditions.Remove(condition);
			return c;
		}

		public ConditionEvaluatorSet SetQuantifier(LogicalQuantifier quantifier)
		{
			if (quantifier == Quantifier)
				return this;

			return new ConditionEvaluatorSet(quantifier,m_conditions);
		}

		public IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, IEnumerable<SubstitutionSet> constraints)
		{
			if (constraints == null || !constraints.Any())
				constraints = new[] {new SubstitutionSet()};

			if(Quantifier == LogicalQuantifier.Existential)
				return ExistentialEvaluate(kb, constraints);

			return UniversalEvaluate(kb,constraints);
		}

		public bool Evaluate(KB kb, IEnumerable<SubstitutionSet> constraints)
		{
			return UnifyEvaluate(kb, constraints).Any();
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
			ConditionEvaluatorSet evaluatorSet = obj as ConditionEvaluatorSet;
			if(evaluatorSet==null)
				return false;

			return m_conditions.SetEquals(evaluatorSet.m_conditions);
		}

		#region Evaluation Methods

		private IEnumerable<SubstitutionSet> ExistentialEvaluate(KB kb, IEnumerable<SubstitutionSet> constraints)
		{
			List<SubstitutionSet> sets = new List<SubstitutionSet>();
			List<SubstitutionSet> aux = new List<SubstitutionSet>();
			sets.AddRange(constraints.Select(c => new SubstitutionSet(c)));
			foreach (var c in m_conditions)
			{
				aux.AddRange(c.UnifyEvaluate(kb, sets));
				Util.Swap(ref sets, ref aux);
				aux.Clear();
				if (sets.Count == 0)
					break;
			}

			return sets;
		}

		private IEnumerable<SubstitutionSet> UniversalEvaluate(KB kb, IEnumerable<SubstitutionSet> constraints)
		{
			var lastDomain = BuildDomain(constraints);
			List<SubstitutionSet> sets = new List<SubstitutionSet>(constraints);
			List<SubstitutionSet> aux = new List<SubstitutionSet>();
			try
			{
				foreach (var c in m_conditions)
				{
					aux.AddRange(c.UnifyEvaluate(kb, sets));
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

		public void GetObjectData(ISerializationData dataHolder)
		{
			dataHolder.SetValue("Quantifier",Quantifier);
			dataHolder.SetValue("Set",m_conditions.ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			Quantifier = dataHolder.GetValue<LogicalQuantifier>("Quantifier");
			var set = dataHolder.GetValue<Condition[]>("Set");
			if(set!=null)
				m_conditions = new HashSet<Condition>(set);
		}
	}
}
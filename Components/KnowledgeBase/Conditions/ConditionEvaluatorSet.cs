using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	[Serializable]
	public sealed class ConditionEvaluatorSet : IEnumerable<Condition>, IConditionEvaluator
	{
		private HashSet<Condition> m_conditions;
		public LogicalQuantifier Quantifier { get; }

		public int Count {
			get { return m_conditions.Count; }
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
			var lastDomain = constraints.SelectMany(set => set).GroupBy(s => s.Variable, s => s.Value).ToArray();
			List<SubstitutionSet> sets = new List<SubstitutionSet>(constraints);
			List<SubstitutionSet> aux = new List<SubstitutionSet>();
			foreach (var c in m_conditions)
			{
				aux.AddRange(c.UnifyEvaluate(kb, sets));
				Util.Swap(ref sets, ref aux);
				aux.Clear();
				var newDomain = sets.SelectMany(set => set).GroupBy(s => s.Variable, s => s.Value).ToArray();
				if (!CompareDomains(lastDomain, newDomain))
					return aux;

				lastDomain = newDomain;
			}

			return sets;
		}

		private bool CompareDomains(IGrouping<Name, Name>[] oldDomain, IGrouping<Name, Name>[] newDomain)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	[Serializable]
	public sealed class ConditionEvaluatorSet : HashSet<Condition>, IConditionEvaluator
	{
		public ConditionEvaluatorSet() : base()
		{
		}

		public ConditionEvaluatorSet(IEnumerable<Condition> conditions) : base(conditions)
		{
		}

		public IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, SubstitutionSet constraints)
		{
			List<SubstitutionSet> sets = new List<SubstitutionSet>();
			List<SubstitutionSet> aux = new List<SubstitutionSet>();
			sets.Add(constraints);
			using (var it = GetEnumerator())
			{
				while (it.MoveNext())
				{
					var condition = it.Current;
					foreach (var s in sets)
						aux.AddRange(condition.UnifyEvaluate(kb, s));
					Util.Swap(ref sets,ref aux);
					aux.Clear();
					if(sets.Count==0)
						break;
				}
			}
			return sets;
		}

		public bool Evaluate(KB kb, SubstitutionSet constraints)
		{
			return UnifyEvaluate(kb, constraints).Any();
		}

		public override int GetHashCode()
		{
			const int EMPTY_HASH = 0x33b65725;
			if (this.Count == 0)
				return EMPTY_HASH;
			return EMPTY_HASH ^ this.Select(c => c.GetHashCode()).Aggregate((h1, h2) => h1 ^ h2);
		}

		public override bool Equals(object obj)
		{
			ConditionEvaluatorSet evaluatorSet = obj as ConditionEvaluatorSet;
			if(evaluatorSet==null)
				return false;

			return SetEquals(evaluatorSet);
		}
	}
}
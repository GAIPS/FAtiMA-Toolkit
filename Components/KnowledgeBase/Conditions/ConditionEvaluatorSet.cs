using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	//todo remove hashset extention from this class.
	//todo this class should behave like a value set. any modification to it returns a copy of the object. (ie: the original object remains unchanged)
	[Serializable]
	public sealed class ConditionEvaluatorSet : HashSet<Condition>, IConditionEvaluator
	{
		public ConditionEvaluatorSet() : base()
		{
		}

		public ConditionEvaluatorSet(IEnumerable<Condition> conditions) : base(conditions)
		{
		}

		public IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, IEnumerable<SubstitutionSet> constraints)
		{
			if (constraints == null || !constraints.Any())
				constraints = new[] {new SubstitutionSet()};

			List<SubstitutionSet> sets = new List<SubstitutionSet>();
			List<SubstitutionSet> aux = new List<SubstitutionSet>();
			sets.AddRange(constraints.Select(c => new SubstitutionSet(c)));
			using (var it = GetEnumerator())
			{
				while (it.MoveNext())
				{
					var condition = it.Current;
					aux.AddRange(condition.UnifyEvaluate(kb, sets));
					Util.Swap(ref sets,ref aux);
					aux.Clear();
					if(sets.Count==0)
						break;
				}
			}
			return sets;
		}

		public bool Evaluate(KB kb, IEnumerable<SubstitutionSet> constraints)
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
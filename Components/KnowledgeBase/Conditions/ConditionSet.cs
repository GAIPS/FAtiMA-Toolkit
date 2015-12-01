using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase.Conditions
{
	[Serializable]
	public sealed class ConditionSet : HashSet<Condition>, ICondition
	{
		public ConditionSet() : base()
		{
		}

		public ConditionSet(IEnumerable<Condition> conditions) : base(conditions)
		{
		}

		public IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, SubstitutionSet constraints)
		{
			var it = GetEnumerator();
			if (!it.MoveNext())
				return new[] {new SubstitutionSet()};

			var result = it.Current.UnifyEvaluate(kb, constraints);

			while (it.MoveNext())
			{
				var a = it.Current;
				result = result.SelectMany(s => 
					a.UnifyEvaluate(kb, s));
			}
			return result;
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
			ConditionSet set = obj as ConditionSet;
			if(set==null)
				return false;

			return SetEquals(set);
		}
	}
}
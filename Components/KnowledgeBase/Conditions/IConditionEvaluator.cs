using System.Collections.Generic;
using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase.Conditions
{
	public interface IConditionEvaluator
	{
		IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, IEnumerable<SubstitutionSet> constraints);

		bool Evaluate(KB kb, IEnumerable<SubstitutionSet> constraints);
	}
}
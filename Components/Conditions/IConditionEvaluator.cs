using System.Collections.Generic;
using WellFormedNames;

namespace Conditions
{
	public interface IConditionEvaluator
	{
		IEnumerable<SubstitutionSet> UnifyEvaluate(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints);

		bool Evaluate(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints);
	}
}
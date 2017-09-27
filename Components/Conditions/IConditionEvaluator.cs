using System.Collections.Generic;
using WellFormedNames;

namespace Conditions
{
	public interface IConditionEvaluator
	{
		IEnumerable<SubstitutionSet> Unify(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints);
	}
}
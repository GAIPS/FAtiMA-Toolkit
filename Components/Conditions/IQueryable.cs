using System.Collections.Generic;
using Utilities;
using WellFormedNames;

namespace Conditions
{
	public interface IQueryable
	{
		IEnumerable<Pair<PrimitiveValue, IEnumerable<SubstitutionSet>>> AskPossibleProperties(Name property, Name perspective, IEnumerable<SubstitutionSet> constraints);
	}
}
using System.Collections.Generic;
using Utilities;

namespace WellFormedNames
{
	public interface IQueryable
	{
		Name Perspective { get; }
		IEnumerable<Pair<PrimitiveValue, IEnumerable<SubstitutionSet>>> AskPossibleProperties(Name property, Name perspective, IEnumerable<SubstitutionSet> constraints);
	}
}
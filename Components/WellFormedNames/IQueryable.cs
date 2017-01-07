using System.Collections.Generic;
using Utilities;

namespace WellFormedNames
{
	public interface IQueryable
	{
		Name Perspective { get; }
		IEnumerable<Pair<Name, IEnumerable<SubstitutionSet>>> AskPossibleProperties(Name property, Name perspective, IEnumerable<SubstitutionSet> constraints);
        Name AssertPerspective(Name perspective);
    }
}
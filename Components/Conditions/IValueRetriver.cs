using System.Collections.Generic;
using WellFormedNames;
using Utilities;

namespace Conditions
{
	public abstract partial class Condition
	{
		private interface IValueRetriever
		{
			IEnumerable<Pair<Name, SubstitutionSet>> Retrieve(IQueryable db, Name perpective, IEnumerable<SubstitutionSet> constraints);

			Name InnerName { get; }
			bool HasModifier { get; }
		}
	}
}
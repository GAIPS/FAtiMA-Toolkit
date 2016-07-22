using System.Collections.Generic;
using WellFormedNames;
using Utilities;

namespace Conditions
{
	public abstract partial class Condition
	{
		private interface IValueRetriver
		{
			IEnumerable<Pair<Name, SubstitutionSet>> Retrive(IQueryable db, Name perpective, IEnumerable<SubstitutionSet> constraints);

			Name InnerName { get; }
			bool HasModifier { get; }
		}
	}
}
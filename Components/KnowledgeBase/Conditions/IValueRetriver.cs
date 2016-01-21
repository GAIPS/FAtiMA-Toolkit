using System.Collections.Generic;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	public abstract partial class Condition
	{
		private interface IValueRetriver
		{
			IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> Retrive(KB kb, IEnumerable<SubstitutionSet> constraints);

			Name InnerName { get; }
			bool HasModifier { get; }
		}
	}
}
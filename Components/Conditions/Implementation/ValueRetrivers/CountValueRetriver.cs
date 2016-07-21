using System.Collections.Generic;
using System.Linq;
using WellFormedNames;
using Utilities;
using IQueryable = WellFormedNames.IQueryable;

namespace Conditions
{
	public abstract partial class Condition
	{
		private class CountValueRetriver : IValueRetriver
		{
			private readonly Name m_name;

			public CountValueRetriver(Name name)
			{
				m_name = name;
			}

			public IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> Retrive(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints)
			{
				PrimitiveValue count = constraints.Select(c => c[m_name]).Where(n => n != null).Distinct().Count();
				return constraints.Select(s => Tuples.Create(count, s));
			}

			public Name InnerName
			{
				get { return m_name; }
			}

			public bool HasModifier
			{
				get { return true; }
			}

			public override string ToString()
			{
				return "#" + m_name;
			}

			public override int GetHashCode()
			{
				return '#'.GetHashCode() ^ m_name.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				var c = obj as CountValueRetriver;
				if (c == null)
					return false;
				return m_name.Equals(c.m_name);
			}
		}
	}
}
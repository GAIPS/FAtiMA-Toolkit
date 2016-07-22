using System;
using System.Collections.Generic;
using WellFormedNames;

namespace Conditions
{
	public partial class Condition
	{
		private sealed class PrimitiveComparisonCondition : Condition
		{
			private readonly IValueRetriver m_retriver;
			private readonly Name m_value;
			private readonly ComparisonOperator m_operation;

			public PrimitiveComparisonCondition(IValueRetriver prop, Name value, ComparisonOperator op)
			{
				m_retriver = prop;
				m_value = value;
				m_operation = op;
			}

			protected override IEnumerable<SubstitutionSet> CheckActivation(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints)
			{
				foreach (var pair in m_retriver.Retrive(db,perspective, constraints))
				{
					if (CompareValues(pair.Item1, m_value, m_operation))
						yield return pair.Item2;
				}
			}

			public override string ToString()
			{
				return string.Format("{0} {1} {2}", m_retriver, OperatorRepresentation(m_operation), m_value);
			}

			public override bool Equals(object obj)
			{
				PrimitiveComparisonCondition o = obj as PrimitiveComparisonCondition;
				if (o == null)
					return false;

				return m_operation == o.m_operation && m_retriver.Equals(o.m_retriver) && m_value == o.m_value;
			}

			public override int GetHashCode()
			{
				return m_retriver.GetHashCode() ^ m_value.GetHashCode() ^ m_operation.GetHashCode();
			}
		}
	}
}
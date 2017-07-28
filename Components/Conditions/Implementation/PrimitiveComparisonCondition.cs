using System;
using System.Collections.Generic;
using System.Linq;
using WellFormedNames;
using IQueryable = WellFormedNames.IQueryable;

namespace Conditions
{
	public partial class Condition
	{
		private sealed class PrimitiveComparisonCondition : Condition
		{
			private readonly IValueRetriever m_retriver;
			private readonly Name m_value;
			private readonly ComparisonOperator m_operation;

			public PrimitiveComparisonCondition(IValueRetriever prop, Name value, ComparisonOperator op)
			{
				m_retriver = prop;
				m_value = value;
				m_operation = op;
			}

			protected override IEnumerable<SubstitutionSet> CheckActivation(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints)
			{

                Name realValue = db.AskPossibleProperties(m_value, perspective, null).First().Item1.Value;

				foreach (var pair in m_retriver.Retrieve(db,perspective, constraints))
				{
					if (CompareValues(pair.Item1.Value, realValue, m_operation))
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
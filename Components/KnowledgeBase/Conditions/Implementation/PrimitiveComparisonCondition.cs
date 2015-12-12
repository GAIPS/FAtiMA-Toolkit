using System.Collections.Generic;
using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase.Conditions
{
	public partial class Condition
	{
		public sealed class PrimitiveComparisonCondition : Condition
		{
			private readonly Name m_property;
			private readonly PrimitiveValue m_value;
			private readonly ComparisonOperator m_operation;

			public PrimitiveComparisonCondition(Name prop, PrimitiveValue value, ComparisonOperator op)
			{
				m_property = prop;
				m_value = value;
				m_operation = op;
			}

			public override IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, SubstitutionSet constraints)
			{
				if (constraints == null)
					constraints = new SubstitutionSet();

				var p = m_property.MakeGround(constraints);
				if (p.IsGrounded)
				{
					var pValue = kb.AskProperty(p);
					if (pValue != null)
					{
						if (CompareValues(pValue, m_value, m_operation))
							yield return constraints;
					}
				}
				else
				{
					foreach (var pair in kb.AskPossibleProperties(p, constraints))
					{
						if (CompareValues(pair.Item1, m_value, m_operation))
							yield return pair.Item2;
					}
				}
			}

			public override string ToString()
			{
				return string.Format("{0} {1} {2}", m_property, OperatorRepresentation(m_operation), m_value);
			}

			public override bool Equals(object obj)
			{
				PrimitiveComparisonCondition o = obj as PrimitiveComparisonCondition;
				if (o == null)
					return false;

				return m_operation == o.m_operation && m_property == o.m_property && m_value == o.m_value;
			}

			public override int GetHashCode()
			{
				return m_property.GetHashCode() ^ m_value.GetHashCode() ^ m_operation.GetHashCode();
			}
		}
	}
}
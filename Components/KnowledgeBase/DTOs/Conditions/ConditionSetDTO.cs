using KnowledgeBase.Conditions;

namespace KnowledgeBase.DTOs.Conditions
{
	public class ConditionSetDTO
	{
		public LogicalQuantifier Quantifier { get; set; }
		public ConditionDTO[] Set;
	}
}
using System;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using SocialImportance.DTOs;

namespace SocialImportance
{
	[Serializable]
	internal class AttributionRule
	{
		public Name Target { get;}
		public int Value { get; }
		public ConditionSet Conditions { get; }

		public AttributionRule(AttributionRuleDTO dto)
		{
			Target = (Name) dto.Target;
			Value = dto.Value;
			Conditions = new ConditionSet(dto.Conditions);
		}

		public AttributionRuleDTO ToDTO()
		{
			return new AttributionRuleDTO() {Target = Target.ToString(),Value = Value,Conditions = Conditions.ToDTO()};
		}
	}
}
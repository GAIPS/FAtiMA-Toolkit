using System;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using SocialImportance.DTOs;

namespace SocialImportance
{
	[Serializable]
	public class AttributionRule
	{
		public Name Target { get;}
		public float Value { get; }
		public ConditionSet Conditions { get; }

		public AttributionRule(AttributionRuleDTO dto)
		{
			Target = (Name) dto.Target;
			Value = dto.Value;
			Conditions = new ConditionSet(dto.Conditions);
		}
	}
}
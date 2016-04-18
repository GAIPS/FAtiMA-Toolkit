using System;
using KnowledgeBase.DTOs.Conditions;

namespace SocialImportance.DTOs
{
	[Serializable]
	public class AttributionRuleDTO
	{
		public string Target;
		public float Value;
		public ConditionSetDTO Conditions;
	}
}
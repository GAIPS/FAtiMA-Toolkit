using System;
using KnowledgeBase.DTOs.Conditions;

namespace SocialImportance.DTOs
{
	[Serializable]
	public class AttributionRuleDTO
	{
		public string Target;
		public int Value;
		public ConditionSetDTO Conditions;
	}
}
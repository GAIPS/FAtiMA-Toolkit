using System;
using KnowledgeBase.DTOs.Conditions;

namespace SocialImportance.DTOs
{
	/// <summary>
	/// Data Type Object Class for defining a Social Importance's Attribution Rule.
	/// </summary>
	/// <remarks>
	/// Attribution rules are used to define conditions that when validated
	/// through the asset's beliefs will attribute to the target a Social Importance Value.
	/// The total Social Importance Value of a target is given by the sum of all valid
	/// Attribution rules in the asset's definition.
	/// </remarks>
	[Serializable]
	public class AttributionRuleDTO
	{
		/// <summary>
		/// The condition variable that represents the target name in the rule condition set.
		/// </summary>
		public string Target;
		/// <summary>
		/// The value to be attributed to the target, if all conditions are valid.
		/// </summary>
		public int Value;
		/// <summary>
		/// The condition set used to validate this rule.
		/// </summary>
		public ConditionSetDTO Conditions;
	}
}
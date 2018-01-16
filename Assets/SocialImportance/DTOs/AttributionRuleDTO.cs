using System;
using Conditions.DTOs;
using WellFormedNames;

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
		public Guid Id { get; set; }

		/// <summary>
		/// The rule name/description string.
		/// Optional attribute of an Attribution Rule.
		/// </summary>
		public string Description { get; set; }

        /// <summary>
        /// The condition variable that represents the target name in the rule condition set.
        /// </summary>
        public Name Target { get; set; }

		/// <summary>
		/// The value to be attributed to the target, if all conditions are valid.
		/// </summary>
		public Name Value { get; set; }

		/// <summary>
		/// The condition set used to validate this rule.
		/// </summary>
		public ConditionSetDTO Conditions { get; set; }
	}
}
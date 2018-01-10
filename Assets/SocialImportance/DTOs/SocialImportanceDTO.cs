using System;

namespace SocialImportance.DTOs
{
	/// <summary>
	/// Data Type Object Class for defining a Social Importance Asset components.
	/// </summary>
	[Serializable]
	public class SocialImportanceDTO
	{
		/// <summary>
		/// The set of attribution rules used to calculate Social importance values to targets
		/// </summary>
		public AttributionRuleDTO[] AttributionRules;
	}
}
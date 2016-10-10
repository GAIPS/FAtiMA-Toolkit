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
		/// <summary>
		/// The set of Claims used to determine if a action is socially acceptable.
		/// </summary>
		public ClaimDTO[] Claims;
		/// <summary>
		/// The set of Conferrals we want the asset to executed.
		/// </summary>
		public ConferralDTO[] Conferral;
	}
}
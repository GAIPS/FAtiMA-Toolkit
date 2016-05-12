using System;

namespace SocialImportance.DTOs
{
	[Serializable]
	public class SocialImportanceDTO
	{
		public AttributionRuleDTO[] AttributionRules;
		public ClaimDTO[] Claims;
		public ConferralDTO[] Conferral;
	}
}
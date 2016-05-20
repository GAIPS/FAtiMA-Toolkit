using System;

namespace SocialImportance.DTOs
{
	/// <summary>
	/// Data Type Object Class for defining a Social Importance's Claim.
	/// </summary>
	/// <remarks>
	/// Claims are used to tell if actions are socialy acceptable.
	/// Socialy aceptable actions are ones that the Claim value don't exced
	/// the action's target Social Importance value.
	/// 
	/// This can be used to filter agent's possible actions remaining only the socially accepted ones,
	/// or determine if action targeting the agent is socialy accepted or not, acording to the agent's beliefs.
	/// </remarks>
	[Serializable]
	public class ClaimDTO
	{
		/// <summary>
		/// The action's name template used for action matching.
		/// </summary>
		public string ActionTemplate;
		/// <summary>
		/// The maximum Social Importance value the action's target can have,
		/// before the action is considered socially unacceptable.
		/// </summary>
		public uint ClaimSI;
	}
}
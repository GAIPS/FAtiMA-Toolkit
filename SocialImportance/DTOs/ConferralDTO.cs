using ActionLibrary.DTOs;

namespace SocialImportance.DTOs
{
	/// <summary>
	/// Data Type Object Class for defining a Social Importance's Conferral action.
	/// </summary>
	/// <remarks>
	/// Conferral actions are ones that an agent might want to execute, but only if
	/// the action's target's Social Importance value is bellow or equal to the
	/// Conferral's Social Importance.
	/// 
	/// If multiple conferrals are available for execution, the asset will only select
	/// the one with the highest social importance value.
	/// 
	/// Conferrals are bonded by Claims, like any other action, and as such even if a 
	/// conferral can be executed, if its target's social importance value it's above
	/// an action Claim, it will not execute.
	/// </remarks>
	/// <seealso cref="ClaimDTO"/>
	public class ConferralDTO : ActionDefinitionDTO
	{
		/// <summary>
		/// The Conferral's social importance value.
		/// </summary>
		public uint ConferralSI { get; set; }
	}
}
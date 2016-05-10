using ActionLibrary.DTOs;

namespace EmotionalDecisionMaking.DTOs
{
	/// <summary>
	/// Data Type Object Class for defining a reactive action.
	/// </summary>
	public class ReactionDTO : ActionDefinitionDTO
    {
		/// <summary>
		/// The cooldown time that must pass before reusing this same action,
		/// after using it once.
		/// </summary>
	    public float Cooldown { get; set; }
	}
}

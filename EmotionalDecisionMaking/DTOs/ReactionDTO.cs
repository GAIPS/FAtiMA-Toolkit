using ActionLibrary.DTOs;

namespace EmotionalDecisionMaking.DTOs
{
	/// <summary>
	/// Data Type Object Class for defining a reactive action.
	/// </summary>
	public class ReactionDTO : ActionDefinitionDTO
    {
		public float Priority { get; set; }
	}
}

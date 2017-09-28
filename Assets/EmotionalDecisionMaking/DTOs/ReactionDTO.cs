using ActionLibrary.DTOs;
using WellFormedNames;

namespace EmotionalDecisionMaking.DTOs
{
	/// <summary>
	/// Data Type Object Class for defining a reactive action.
	/// </summary>
	public class ReactionDTO : ActionDefinitionDTO
    {
		public string Priority { get; set; }
	}
}

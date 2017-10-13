using System;
using Conditions.DTOs;

namespace ActionLibrary.DTOs
{
	/// <summary>
	/// Data Type Object Class for defining an Action.
	/// </summary>
	public class ActionDefinitionDTO
	{
		/// <summary>
		/// The unique identifier of the action that this DTO is describing
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
		/// The action template, as a well formed string.
		/// </summary>
		/// <example>
		/// Attack([type],[strength])
		/// </example>
		public string Action { get; set; }
		/// <summary>
		/// The target of the action, if any.
		/// </summary>
		public string Target { get; set; }
        /// <summary>
		/// The priority of the action.
		/// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// The type of the action (e.g. verbal, nonverbal)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The set of conditions that must be true for this action execution.
        /// </summary>
        public ConditionSetDTO Conditions { get; set; }
	}
}
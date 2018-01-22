using System.Collections.Generic;
using WellFormedNames;

namespace ActionLibrary
{
	/// <summary>
	/// Interface used to represent an action execution request.
	/// </summary>
	public interface IAction
	{
        /// <summary>
        /// The name of the action's key (first literal of the action's name)
        /// </summary>
        Name Key { get; }

        /// <summary>
        /// The full name of the action to execute (key + parameters)
        /// </summary>
        Name Name { get; }

        /// <summary>
		/// The parameters values that the action needs in order for it to be executed.
		/// The parameter order is equal to the order of the variables in the action template,
		/// defined in the correspondent ActionDefinitionDTO.
		/// </summary>
		IList<Name> Parameters { get; }

        /// <summary>
        /// The target of the action, if applicable
        /// </summary>
        Name Target { get; }

		float Utility { get; }
	}
}
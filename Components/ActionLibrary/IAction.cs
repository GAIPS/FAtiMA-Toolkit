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
		/// The name of the action to execute
		/// </summary>
		Name ActionName { get; }
		/// <summary>
		/// The target of the action, if appliable
		/// </summary>
		Name Target { get; }
		/// <summary>
		/// The parameters values that the action needs in order for it to be executed.
		/// The parameter order is equal to the order of the variables in the action template,
		/// defined in the correspondent ActionDefinitionDTO.
		/// </summary>
		IList<Name> Parameters { get; }

        Name FullName { get; }

		float Utility { get; }
	}
}
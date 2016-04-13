using System.Collections.Generic;
using KnowledgeBase.WellFormedNames;

namespace ActionLibrary
{
	public interface IAction
	{
		Name ActionName { get; }
		Name Target { get; }
		IList<Name> Parameters { get; }
	}
}
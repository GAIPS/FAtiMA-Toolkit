using System.Collections.Generic;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	public interface IAction
	{
		Name ActionName { get; }
		Name Target { get; }
		IList<Name> Parameters { get; }
	}
}
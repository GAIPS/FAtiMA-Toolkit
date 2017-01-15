using System;
using EmotionalAppraisal;
using WellFormedNames;

namespace RolePlayCharacter
{
	[Serializable]
	internal class AgentEntry
	{
		public Name AgentId { get; private set; }
		public ConcreteEmotionalState EmotionalState { get; private set; }

		public AgentEntry(Name id)
		{
			AgentId = id;
			EmotionalState = new ConcreteEmotionalState();
		}
	}
}
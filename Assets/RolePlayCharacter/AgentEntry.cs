using System;
using EmotionalAppraisal;
using WellFormedNames;

namespace RolePlayCharacter
{
	[Serializable]
	internal class AgentEntry
	{
		public Name Name { get; private set; }
		public ConcreteEmotionalState EmotionalState { get; private set; }

		public AgentEntry(Name id)
		{
			Name = id;
			EmotionalState = new ConcreteEmotionalState();
		}
	}
}
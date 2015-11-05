using System;
using EmotionalAppraisal.Interfaces;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisal
{
	[Serializable]
	public class EmotionalCause
	{
		public readonly Name CauseName;
		public readonly DateTime CauseTimestamp;

		public EmotionalCause(Name causeName, DateTime timestamp)
		{
			this.CauseName = causeName;
			this.CauseTimestamp = timestamp;
		}
	}
}
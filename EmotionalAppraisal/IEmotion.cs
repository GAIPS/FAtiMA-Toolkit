using System.Collections.Generic;
using AutobiographicMemory.Interfaces;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisal
{
	public interface IEmotion
	{
		IEventRecord Cause { get; }

		Name Direction { get; }

		float Potential { get; }

		string EmotionType { get; }

		EmotionValence Valence { get; }

		IEnumerable<string> AppraisalVariables { get; }

		bool InfluenceMood { get; }
	}
}
using System.Collections.Generic;
using AutobiographicMemory;
using AutobiographicMemory.Interfaces;
using EmotionalAppraisal.DTOs;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisal
{
	public interface IEmotion
	{
		uint CauseId { get; }

		Name Direction { get; }

		float Potential { get; }

		string EmotionType { get; }

		EmotionValence Valence { get; }

		IEnumerable<string> AppraisalVariables { get; }

		bool InfluenceMood { get; }

		string ToString(AM am);

		IEventRecord GetCause(AM am);

	    EmotionDTO ToDto();
	}
}
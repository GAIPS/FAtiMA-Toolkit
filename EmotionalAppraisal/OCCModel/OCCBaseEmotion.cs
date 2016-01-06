using AutobiographicMemory;
using AutobiographicMemory.Interfaces;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisal.OCCModel
{
	public class OCCBaseEmotion : BaseEmotion
	{
		public OCCBaseEmotion(OCCEmotionType type, float potential, IEventRecord cause, Name direction)
			: base(type.Name,type.Valence,type.AppraisalVariables,potential,type.InfluencesMood,cause,direction)
		{
		}

		public OCCBaseEmotion(OCCEmotionType type, float potential, IEventRecord cause)
			: base(type.Name, type.Valence, type.AppraisalVariables, potential, type.InfluencesMood, cause)
		{
		}
	}
}

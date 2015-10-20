using EmotionalAppraisal;
using EmotionalAppraisal.Interfaces;
using WellFormedNames;

namespace EmotionalAppraisal.OCCModel
{
	public class OCCBaseEmotion : BaseEmotion
	{
		public OCCBaseEmotion(OCCEmotionType type, float potential, IEvent cause, Name direction)
			: base(type.Name,type.Valence,type.AppraisalVariables,potential,type.InfluencesMood,cause,direction)
		{
		}

		public OCCBaseEmotion(OCCEmotionType type, float potential, IEvent cause)
			: base(type.Name, type.Valence, type.AppraisalVariables, potential, type.InfluencesMood, cause)
		{
		}
	}
}

using WellFormedNames;

namespace EmotionalAppraisal.OCCModel
{
	public class OCCBaseEmotion : BaseEmotion
	{
		public OCCBaseEmotion(OCCEmotionType type, float potential, uint causeId, Name direction)
			: base(type.Name,type.Valence,type.AppraisalVariables,potential,type.InfluencesMood,causeId,direction)
		{
		}

		public OCCBaseEmotion(OCCEmotionType type, float potential, uint causeId)
			: base(type.Name, type.Valence, type.AppraisalVariables, potential, type.InfluencesMood, causeId)
		{
		}
	}
}

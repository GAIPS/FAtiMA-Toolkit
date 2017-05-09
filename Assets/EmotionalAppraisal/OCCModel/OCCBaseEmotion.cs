using WellFormedNames;

namespace EmotionalAppraisal.OCCModel
{
	public class OCCBaseEmotion : BaseEmotion
	{
		public OCCBaseEmotion(OCCEmotionType type, float potential, uint causeId, Name direction, Name eventName)
			: base(type.Name,type.Valence,type.AppraisalVariables,potential,type.InfluencesMood,causeId,direction, eventName)
		{
		}

		public OCCBaseEmotion(OCCEmotionType type, float potential, uint causeId, Name eventName)
			: base(type.Name, type.Valence, type.AppraisalVariables, potential, type.InfluencesMood, causeId, eventName)
		{
		}
	}
}

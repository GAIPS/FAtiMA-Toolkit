using EmotionalAppraisal;

namespace EmotionalAppraisal.Components
{
	public interface IEmotionProcessor
	{
		void EmotionActivation(EmotionalAppraisalAsset emotionalModule, ActiveEmotion emotion);
	}
}

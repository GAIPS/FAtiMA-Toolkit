using EmotionalAppraisal;

namespace EmotionalAppraisal.Components
{
	public interface IEmotionProcessor
	{
		void EmotionActivation(EmotionalAppraisalAsset emotionalModule, IActiveEmotion emotion);
	}
}

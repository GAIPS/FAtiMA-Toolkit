using EmotionalAppraisal;

namespace EmotionalAppraisal.Components
{
	public interface IEmotionProcessor
	{
		void EmotionActivation(EmotionalAppraisal emotionalModule, ActiveEmotion emotion);
	}
}

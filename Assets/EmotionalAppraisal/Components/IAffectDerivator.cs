using System.Collections.Generic;

namespace EmotionalAppraisal.Components
{
	public interface IAffectDerivator
	{
		IEnumerable<IEmotion> AffectDerivation(EmotionalAppraisalAsset emotionalModule, Dictionary<string, Goal> goals, IAppraisalFrame frame);
	}
}

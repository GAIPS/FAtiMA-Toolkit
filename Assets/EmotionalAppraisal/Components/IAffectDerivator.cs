using System.Collections.Generic;

namespace EmotionalAppraisal.Components
{
	public interface IAffectDerivator
	{
		short AffectDerivationWeight { get; }
		IEnumerable<IEmotion> AffectDerivation(EmotionalAppraisalAsset emotionalModule, Dictionary<string, Goal> goals, IAppraisalFrame frame);
		void InverseAffectDerivation(EmotionalAppraisalAsset emotionalModule, IEmotion emotion, IWritableAppraisalFrame frame);
	}
}

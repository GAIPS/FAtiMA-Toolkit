using EmotionalAppraisal;
using System.Collections.Generic;

namespace EmotionalAppraisal.Components
{
	public interface IAffectDerivator
	{
		short AffectDerivationWeight { get; }
		IEnumerable<BaseEmotion> AffectDerivation(EmotionalAppraisalAsset emotionalModule, IAppraisalFrame frame);
		void InverseAffectDerivation(EmotionalAppraisalAsset emotionalModule, BaseEmotion emotion, IWritableAppraisalFrame frame);
	}
}

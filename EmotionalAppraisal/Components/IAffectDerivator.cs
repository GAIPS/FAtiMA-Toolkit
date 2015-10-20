using EmotionalAppraisal;
using System.Collections.Generic;

namespace EmotionalAppraisal.Components
{
	public interface IAffectDerivator
	{
		short AffectDerivationWeight { get; }
		IEnumerable<BaseEmotion> AffectDerivation(EmotionalAppraisal emotionalModule, IAppraisalFrame frame);
		void InverseAffectDerivation(EmotionalAppraisal emotionalModule, BaseEmotion emotion, IWritableAppraisalFrame frame);
	}
}

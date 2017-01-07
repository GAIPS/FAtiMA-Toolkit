using AutobiographicMemory;
using KnowledgeBase;

namespace EmotionalAppraisal.Components
{
	public interface IAppraisalDerivator
	{
		short AppraisalWeight { get; }

		void Appraisal(KB emotionalModule, IBaseEvent e, IWritableAppraisalFrame frame);

		void InverseAppraisal(EmotionalAppraisalAsset emotionalModule, IAppraisalFrame frame);

		IAppraisalFrame Reappraisal(EmotionalAppraisalAsset emotionalModule);
	}
}

using AutobiographicMemory;

namespace EmotionalAppraisal.Components
{
	public interface IAppraisalDerivator
	{
		short AppraisalWeight { get; }

		void Appraisal(EmotionalAppraisalAsset emotionalModule, IBaseEvent e, IWritableAppraisalFrame frame);

		void InverseAppraisal(EmotionalAppraisalAsset emotionalModule, IAppraisalFrame frame);

		IAppraisalFrame Reappraisal(EmotionalAppraisalAsset emotionalModule);
	}
}

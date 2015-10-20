using EmotionalAppraisal;
using EmotionalAppraisal.Interfaces;

namespace EmotionalAppraisal.Components
{
	public interface IAppraisalDerivator
	{
		short AppraisalWeight { get; }

		void Appraisal(EmotionalAppraisalAsset emotionalModule, IEvent e, IWritableAppraisalFrame frame);

		void InverseAppraisal(EmotionalAppraisalAsset emotionalModule, IAppraisalFrame frame);

		IAppraisalFrame Reappraisal(EmotionalAppraisalAsset emotionalModule);
	}
}

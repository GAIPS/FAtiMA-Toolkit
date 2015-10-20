using EmotionalAppraisal;
using EmotionalAppraisal.Interfaces;

namespace EmotionalAppraisal.Components
{
	public interface IAppraisalDerivator
	{
		short AppraisalWeight { get; }

		void Appraisal(EmotionalAppraisal emotionalModule, IEvent e, IWritableAppraisalFrame frame);

		void InverseAppraisal(EmotionalAppraisal emotionalModule, IAppraisalFrame frame);

		IAppraisalFrame Reappraisal(EmotionalAppraisal emotionalModule);
	}
}

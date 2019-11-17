using AutobiographicMemory;
using KnowledgeBase;

namespace EmotionalAppraisal.Components
{
	public interface IAppraisalDerivator
	{
		short AppraisalWeight { get; }

		void Appraisal(KB emotionalModule, IBaseEvent e, IAppraisalFrame frame);
	}
}

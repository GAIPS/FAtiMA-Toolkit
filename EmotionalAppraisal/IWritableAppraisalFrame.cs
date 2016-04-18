namespace EmotionalAppraisal
{
	public interface IWritableAppraisalFrame : IAppraisalFrame
	{
		void SetAppraisalVariable(string appraisalVariableName, float value);
	}
}

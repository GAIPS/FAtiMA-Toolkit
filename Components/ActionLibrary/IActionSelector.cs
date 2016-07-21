using Conditions;

namespace ActionLibrary
{
	internal interface IActionSelector
	{
		void OnConditionsUpdated(BaseActionDefinition def, ConditionSet oldConditions);
	}
}
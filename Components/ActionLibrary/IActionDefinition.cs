using System;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace ActionLibrary
{
	public interface IActionDefinition: ICloneable
	{
		Guid Id { get; }
		ConditionSet ActivationConditions { get; }
		IAction GenerateAction(SubstitutionSet constraints);
	}
}
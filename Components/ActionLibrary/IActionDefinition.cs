using System;
using ActionLibrary.DTOs;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace ActionLibrary
{
	public interface IActionDefinition: ICloneable
	{
		ConditionSet ActivationConditions { get; }
		IAction GenerateAction(SubstitutionSet constraints);
	}
}
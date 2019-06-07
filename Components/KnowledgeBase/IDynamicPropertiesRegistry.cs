using System.Collections.Generic;
using KnowledgeBase.DTOs;
using Utilities;
using WellFormedNames;

namespace KnowledgeBase
{
	public delegate IEnumerable<DynamicPropertyResult> DynamicPropertyCalculator_T1(IQueryContext context, Name arg1);
	public delegate IEnumerable<DynamicPropertyResult> DynamicPropertyCalculator_T2(IQueryContext context, Name arg1, Name arg2);
	public delegate IEnumerable<DynamicPropertyResult> DynamicPropertyCalculator_T3(IQueryContext context, Name arg1, Name arg2, Name arg3);
	public delegate IEnumerable<DynamicPropertyResult> DynamicPropertyCalculator_T4(IQueryContext context, Name arg1, Name arg2, Name arg3, Name arg4);
    public delegate IEnumerable<DynamicPropertyResult> DynamicPropertyCalculator_T5(IQueryContext context, Name arg1, Name arg2, Name arg3, Name arg4, Name arg5);
	public interface IDynamicPropertiesRegistry
	{
		void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T1 surrogate);
		void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T2 surrogate);
		void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T3 surrogate);
		void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T4 surrogate);
        void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T5 surrogate);

		void UnregistDynamicProperty(Name propertyTemplate);

		IEnumerable<DynamicPropertyDTO> GetDynamicProperties();
	}

	public interface IQueryContext
	{
		IQueryable Queryable { get; }
		IEnumerable<SubstitutionSet> Constraints { get; }
		Name Perspective { get; }

		IEnumerable<Pair<ComplexValue, IEnumerable<SubstitutionSet>>> AskPossibleProperties(Name property);
	}

	public struct DynamicPropertyResult
	{
		public readonly ComplexValue Value;
		public readonly SubstitutionSet Constraints;

		public DynamicPropertyResult(ComplexValue value, SubstitutionSet constraint)
		{
			Value = value;
			Constraints = constraint;
		}
	}

}
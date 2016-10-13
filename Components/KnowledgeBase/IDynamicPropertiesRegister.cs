using System.Collections.Generic;
using Utilities;
using WellFormedNames;

namespace KnowledgeBase
{
	public delegate IEnumerable<Pair<Name, SubstitutionSet>> DynamicPropertyCalculator_T1(IQueryable kb, IEnumerable<SubstitutionSet> constraints, Name perspective, Name arg1);
	public delegate IEnumerable<Pair<Name, SubstitutionSet>> DynamicPropertyCalculator_T2(IQueryable kb, IEnumerable<SubstitutionSet> constraints, Name perspective, Name arg1, Name arg2);
	public delegate IEnumerable<Pair<Name, SubstitutionSet>> DynamicPropertyCalculator_T3(IQueryable kb, IEnumerable<SubstitutionSet> constraints, Name perspective, Name arg1, Name arg2, Name arg3);
	public delegate IEnumerable<Pair<Name, SubstitutionSet>> DynamicPropertyCalculator_T4(IQueryable kb, IEnumerable<SubstitutionSet> constraints, Name perspective, Name arg1, Name arg2, Name arg3, Name arg4);

	public interface IDynamicPropertiesRegister
	{
		void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T1 surrogate, string description = null);
		void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T2 surrogate, string description = null);
		void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T3 surrogate, string description = null);
		void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T4 surrogate, string description = null);

		void UnregistDynamicProperty(Name propertyTemplate);

		IEnumerable<DynamicPropertyEntry> GetDynamicProperties();
	}
}
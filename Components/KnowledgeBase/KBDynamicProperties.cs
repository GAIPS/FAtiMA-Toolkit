using System.Collections.Generic;
using System.Linq;
using WellFormedNames;

namespace KnowledgeBase
{
	public partial class KB : IDynamicPropertiesRegistry
	{
		private DynamicPropertyRegistry _registry;

		public void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T1 surrogate, string description = null)
		{
			_registry.RegistDynamicProperty(propertyName, surrogate, description);
		}

		public void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T2 surrogate, string description = null)
		{
			_registry.RegistDynamicProperty(propertyName, surrogate, description);
		}

		public void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T3 surrogate, string description = null)
		{
			_registry.RegistDynamicProperty(propertyName, surrogate, description);
		}

		public void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T4 surrogate, string description = null)
		{
			_registry.RegistDynamicProperty(propertyName,surrogate,description);
		}

		public void UnregistDynamicProperty(Name propertyTemplate)
		{
			_registry.UnregistDynamicProperty(propertyTemplate);
		}

		public IEnumerable<DynamicPropertyEntry> GetDynamicProperties()
		{
			return _registry.GetDynamicProperties();
		}

		private void CreateRegistry()
		{
			_registry = new DynamicPropertyRegistry(Test);
		}

		private bool Test(Name template)
		{
			return m_knowledgeStorage.Unify(template).Any();
		}
	}
}
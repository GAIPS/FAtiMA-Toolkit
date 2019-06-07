using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.DTOs;
using WellFormedNames;

namespace KnowledgeBase
{
	public partial class KB : IDynamicPropertiesRegistry
	{
		private DynamicPropertyRegistry _registry;

		public void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T1 surrogate)
		{
			_registry.RegistDynamicProperty(propertyName, description, surrogate);
		}

		public void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T2 surrogate)
		{
			_registry.RegistDynamicProperty(propertyName, description, surrogate);
		}

		public void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T3 surrogate)
		{
			_registry.RegistDynamicProperty(propertyName, description, surrogate);
		}

		public void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T4 surrogate)
		{
			_registry.RegistDynamicProperty(propertyName, description, surrogate);
		}

        public void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T5 surrogate)
		{
			_registry.RegistDynamicProperty(propertyName, description, surrogate);
		}

		public void UnregistDynamicProperty(Name propertyTemplate)
		{
			_registry.UnregistDynamicProperty(propertyTemplate);
		}

		public IEnumerable<DynamicPropertyDTO> GetDynamicProperties()
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
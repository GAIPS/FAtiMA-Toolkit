using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Utilities;
using WellFormedNames;

namespace KnowledgeBase
{
	public partial class KB
	{
		#region Dynamic Property Registry

		#region Old

		public void RegistDynamicProperty(Name propertyTemplate, DynamicPropertyCalculator surogate)
		{
			internal_RegistDynamicProperty(propertyTemplate,null, surogate);
		}

		public void RegistDynamicProperty(Name propertyTemplate, string description, DynamicPropertyCalculator surogate)
		{
			//Name[] args;
			//if (arguments == null)
			//	args = new Name[0];
			//else
			//	args = arguments.Distinct().Select(s => Name.BuildName("[" + s + "]")).ToArray();

			internal_RegistDynamicProperty(propertyTemplate,description,surogate);
		}

		#endregion
		/*
		public void RegistDynamicProperty(Name propertyName, LambdaExpression expression)
		{
			RegistDynamicProperty(propertyName, null, expression);
		}

		public void RegistDynamicProperty(Name propertyName, string description, LambdaExpression expression)
		{
			//var info = surogate.Method;
			throw new NotImplementedException();
		}
		*/
		#endregion

		private void internal_RegistDynamicProperty(Name propertyTemplate, string description, DynamicPropertyCalculator surogate)
		{
			if (surogate == null)
				throw new ArgumentNullException(nameof(surogate));

			if (propertyTemplate.IsGrounded)
				throw new ArgumentException("Grounded names cannot be used as dynamic properties", nameof(propertyTemplate));

			var r = m_dynamicProperties.Unify(propertyTemplate).FirstOrDefault();
			if (r != null)
			{
				throw new ArgumentException(
					$"The given template {propertyTemplate} will collide with already registed {propertyTemplate.MakeGround(r.Item2)} dynamic property", nameof(propertyTemplate));
			}

			if (m_knowledgeStorage.Unify(propertyTemplate).Any())
				throw new ArgumentException($"The given template {propertyTemplate} will collide with stored constant properties", nameof(propertyTemplate));

			m_dynamicProperties.Add(propertyTemplate, new DynamicKnowledgeEntry(surogate, description));
		}

		public void UnregistDynamicProperty(Name propertyTemplate)
		{
			if (!m_dynamicProperties.Remove(propertyTemplate))
				throw new Exception($"Unknown Dynamic Property {propertyTemplate}");
		}

		public IEnumerable<DynamicPropertyEntry> GetDynamicProperties()
		{
			return m_dynamicProperties.Select(p => new DynamicPropertyEntry() { PropertyTemplate = p.Key, Description = p.Value.description ?? "No Description" });
		}
	}
}
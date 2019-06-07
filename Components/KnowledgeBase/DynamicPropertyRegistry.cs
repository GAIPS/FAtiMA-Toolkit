using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KnowledgeBase.DTOs;
using Utilities;
using WellFormedNames;
using WellFormedNames.Collections;
using IQueryable = WellFormedNames.IQueryable;

namespace KnowledgeBase
{
	using BeliefPair = Pair<ComplexValue, IEnumerable<SubstitutionSet>>;

	public class DynamicPropertyRegistry : IDynamicPropertiesRegistry
	{
		private const string TMP_MARKER = "_arg";

		public delegate IEnumerable<DynamicPropertyResult> DynamicPropertyCalculator(IQueryContext context, IList<Name> args);

		private sealed class DynamicKnowledgeEntry
		{
			private readonly DynamicPropertyCalculator _surogate;
			private readonly Name[] _parameters;
			public readonly Type DeclaringType;
            public readonly string Description;

			private struct QueryContext : IQueryContext
			{
				public IQueryable Queryable { get; }
				public IEnumerable<SubstitutionSet> Constraints { get; }
				public Name Perspective { get; }

				public QueryContext(IQueryable kb, IEnumerable<SubstitutionSet> contraints, Name perspective)
				{
					Queryable = kb;
					Constraints = contraints;
					Perspective = perspective;
				}

				public IEnumerable<BeliefPair> AskPossibleProperties(Name property)
				{
					return Queryable.AskPossibleProperties(property, Perspective, Constraints);
				}
			}

			public DynamicKnowledgeEntry(DynamicPropertyCalculator surogate, Name[] parameters, string description, Type declaringType)
			{
				_surogate = surogate;
				_parameters = parameters;
                Description = description;
				DeclaringType = declaringType;
			}

			public IEnumerable<DynamicPropertyResult> Evaluate(IQueryable kb, Name perspective, SubstitutionSet args2, IEnumerable<SubstitutionSet> constraints)
			{
				var args = ObjectPool<List<Name>>.GetObject();
                try
				{
					args.AddRange(_parameters.Select(p => args2[p]).Select(v => v == null ? Name.UNIVERSAL_SYMBOL : v.RemoveBoundedVariables(TMP_MARKER)));
				
					return _surogate(new QueryContext(kb, constraints, perspective), args);
				}
				finally
				{
					args.Clear();
					ObjectPool<List<Name>>.Recycle(args);
				}
			}
		}

		public interface IDynamicPropertyMatch
		{
			IEnumerable<BeliefPair> Evaluate(IQueryable kb, Name perspective, IEnumerable<SubstitutionSet> constraints);
		}

		private sealed class DynamicPropertyMatch : IDynamicPropertyMatch
		{
			private DynamicKnowledgeEntry _entry;
			private SubstitutionSet _variable;

			public DynamicPropertyMatch(DynamicKnowledgeEntry entry, SubstitutionSet variables)
			{
				_entry = entry;
				_variable = variables;
			}

			public IEnumerable<BeliefPair> Evaluate(IQueryable kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
			{
				return _entry.Evaluate(kb, perspective, _variable, constraints).GroupBy(p => p.Value, p => p.Constraints).Select(g => Tuples.Create(g.Key,g.Distinct()));
			}
		}

		private NameSearchTree<DynamicKnowledgeEntry> m_dynamicProperties = new NameSearchTree<DynamicKnowledgeEntry>();

		private Func<Name, bool> _willCollideDelegate;

#region IDynamicPropertiesRegistry implementation

		public void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T1 surrogate)
		{
			if (surrogate == null)
				throw new ArgumentNullException(nameof(surrogate));

			internal_RegistDynamicProperty(propertyName, description, surrogate.Method,
				(context, args) => surrogate(context, args[0]));
		}

		public void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T2 surrogate)
		{
			if (surrogate == null)
				throw new ArgumentNullException(nameof(surrogate));

			internal_RegistDynamicProperty(propertyName, description, surrogate.Method,
				(context, args) => surrogate(context, args[0], args[1]));
		}

		public void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T3 surrogate)
		{
			if (surrogate == null)
				throw new ArgumentNullException(nameof(surrogate));

			internal_RegistDynamicProperty(propertyName, description, surrogate.Method,
				(context, args) => surrogate(context, args[0], args[1], args[2]));
		}

		public void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T4 surrogate)
		{
			if (surrogate == null)
				throw new ArgumentNullException(nameof(surrogate));

			internal_RegistDynamicProperty(propertyName, description, surrogate.Method,
				(context, args) => surrogate(context, args[0], args[1], args[2], args[3]));
		}

        	public void RegistDynamicProperty(Name propertyName, string description, DynamicPropertyCalculator_T5 surrogate)
		{
			if (surrogate == null)
				throw new ArgumentNullException(nameof(surrogate));

			internal_RegistDynamicProperty(propertyName, description, surrogate.Method,
				(context, args) => surrogate(context, args[0], args[1], args[2], args[3], args[4]));
		}


		public void UnregistDynamicProperty(Name propertyTemplate)
		{
			if (!m_dynamicProperties.Remove(propertyTemplate))
				throw new Exception($"Unknown Dynamic Property {propertyTemplate}");
		}

		public IEnumerable<DynamicPropertyDTO> GetDynamicProperties()
		{
			return m_dynamicProperties.Select(p => new DynamicPropertyDTO() { PropertyTemplate = p.Key.ToString(), DeclaringComponent = p.Value.DeclaringType.Name, Description = p.Value.Description});
		}

#endregion

		public DynamicPropertyRegistry(Func<Name, bool> willCollideDelegate)
		{
			_willCollideDelegate = willCollideDelegate;
		}

		private void internal_RegistDynamicProperty(Name propertyName, string description, MethodInfo surogate, DynamicPropertyCalculator converted)
		{
			if (!propertyName.IsPrimitive)
				throw new ArgumentException("The property name must be a primitive symbol.", nameof(propertyName));

			var p = surogate.GetParameters();
			var propertyParameters = p.Skip(1).Select(p2 => Name.BuildName("["+p2.Name+"]")).ToArray();
			var template = Name.BuildName(propertyParameters.Prepend(propertyName));

			var r = m_dynamicProperties.Unify(template).FirstOrDefault();
			if (r != null)
			{
				var t = r.Item1.DeclaringType;
				if (t == surogate.DeclaringType)
					return;

				throw new ArgumentException("There is already a registed property with the name " + propertyName +" that receives " + (p.Length - 3) + "parameters.");
			}

			if (_willCollideDelegate(template))
				throw new ArgumentException("There are already stored property values that will collide with the given dynamic property.");

            m_dynamicProperties.Add(template, new DynamicKnowledgeEntry(converted, propertyParameters, description, surogate.DeclaringType));
		}

		public int NumOfRegists { get { return m_dynamicProperties.Count; } }

		public void Clear()
		{
			m_dynamicProperties.Clear();
		}

		public IEnumerable<IDynamicPropertyMatch> Evaluate(Name property)
		{
			if (m_dynamicProperties.Count == 0)
				return Enumerable.Empty<IDynamicPropertyMatch>();

			Name tmpPropertyName = property.ReplaceUnboundVariables(TMP_MARKER);

			var d = m_dynamicProperties.Unify(tmpPropertyName).ToList();
			if (d.Count == 0)
				return Enumerable.Empty<IDynamicPropertyMatch>();

			return d.Select(r => (IDynamicPropertyMatch)new DynamicPropertyMatch(r.Item1,r.Item2));
		}

		public IEnumerable<BeliefPair> Evaluate2(IQueryable kb, Name property, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			if (m_dynamicProperties.Count == 0)
				yield break;

			Name tmpPropertyName = property.ReplaceUnboundVariables(TMP_MARKER);

			var d = m_dynamicProperties.Unify(tmpPropertyName).ToList();
			if (d.Count == 0)
				yield break;

			var results = d.SelectMany(p => p.Item1.Evaluate(kb, perspective, p.Item2, constraints).ToList());
            
			foreach (var g in results.GroupBy(p => p.Value, p => p.Constraints))
			{
				yield return Tuples.Create(g.Key, g.Distinct());
			}
		}

		public bool WillEvaluate(Name property)
		{
			return m_dynamicProperties.Unify(property).Any();
		}
	}
}
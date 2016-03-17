using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using GAIPS.Serialization.SerializationGraph;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Collections;
using Utilities;

namespace KnowledgeBase
{
	public delegate IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> DynamicPropertyCalculator(KB kb, Name perspective, IDictionary<string,Name> args, IEnumerable<SubstitutionSet> constraints);

	[Serializable]
	public class KB : ICustomSerialization
	{
		private sealed class KnowledgeEntry
		{
			private PrimitiveValue m_universal = null;
			private Dictionary<Name, PrimitiveValue> m_perspectives;
			//public readonly PrimitiveValue Value;
			//public readonly bool IsPersistent;

			//public KnowledgeEntry(PrimitiveValue value, bool isPersistent)
			//{
			//	this.Value = value;
			//	this.IsPersistent = isPersistent;
			//}

			public PrimitiveValue GetValueFor(Name perspective)
			{
				PrimitiveValue value;
				if ((m_perspectives != null) && m_perspectives.TryGetValue(perspective, out value))
					return value;

				return m_universal;
			}

			public void TellValueFor(Name perspective, PrimitiveValue value)
			{
				if (perspective.IsUniversal)
				{
					m_universal = value;
					return;
				}

				if (m_perspectives == null)
				{
					if(value==null)
						return;

					m_perspectives = new Dictionary<Name, PrimitiveValue>();
				}

				if (value == null)
					m_perspectives.Remove(perspective);
				else
					m_perspectives[perspective] = value;
			}

			public bool IsEmpty()
			{
				return (m_perspectives == null) && (m_universal == null);
			}

			public IEnumerable<KeyValuePair<Name, PrimitiveValue>> GetPerspectives()
			{
				if (m_perspectives != null)
				{
					foreach (var p in m_perspectives)
						yield return p;
				}

				if(m_universal!=null)
					yield return new KeyValuePair<Name, PrimitiveValue>(Name.UNIVERSAL_SYMBOL, m_universal);
			}
		}

		private sealed class DynamicKnowledgeEntry
		{
			public readonly DynamicPropertyCalculator surogate;
			public readonly Name[] arguments;
			
			public DynamicKnowledgeEntry(DynamicPropertyCalculator surogate, Name[] arguments)
			{
				this.surogate = surogate;
				this.arguments = arguments;
			}
		}

		private readonly NameSearchTree<KnowledgeEntry> m_knowledgeStorage;
		private readonly NameSearchTree<DynamicKnowledgeEntry> m_dynamicProperties;

		/// <summary>
		/// Indicates the default mapping of "SELF"
		/// </summary>
		public Name Perspective { get; set; }

		private KB()
		{
			m_knowledgeStorage = new NameSearchTree<KnowledgeEntry>();
			m_dynamicProperties = new NameSearchTree<DynamicKnowledgeEntry>();
		}

		public KB(Name perspective) : this()
		{
			Perspective = perspective;
			RegistNativeDynamicProperties(this);
		}

#region Native Dynamic Properties

		private static void RegistNativeDynamicProperties(KB kb)
		{
			kb.RegistDynamicProperty(COUNT_TEMPLATE, CountPropertyCalculator, new[] { "x" });
		}

		//Count
		private static readonly Name COUNT_TEMPLATE = Name.BuildName("Count([x])");
		private static IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> CountPropertyCalculator(KB kb, Name perspective, IDictionary<string,Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			var arg = args["x"];

			var set = kb.AskPossibleProperties(arg, perspective, constraints).ToList();
			PrimitiveValue count = set.Count;
			IEnumerable<SubstitutionSet> sets;
			if (set.Count == 0)
				sets = constraints;
			else
				sets = set.SelectMany(s => s.Item2).Distinct();
			
			foreach (var d in sets)
				yield return Tuples.Create(count, d);
		}

#endregion

		public void RegistDynamicProperty(Name propertyTemplate, DynamicPropertyCalculator surogate, IEnumerable<string> arguments)
		{
			if(surogate==null)
				throw new ArgumentNullException("surogate");

			if(propertyTemplate.IsGrounded)
				throw new ArgumentException("Grounded names cannot be used as dynamic properties", "propertyTemplate");

			var r = m_dynamicProperties.Unify(propertyTemplate).FirstOrDefault();
			if (r != null)
			{
				throw new ArgumentException(string.Format("The given template {0} will collide with already registed {1} dynamic property", propertyTemplate, propertyTemplate.MakeGround(r.Item2)), "propertyTemplate");
			}

			if(m_knowledgeStorage.Unify(propertyTemplate).Any())
				throw new ArgumentException(string.Format("The given template {0} will collide with stored constant properties", propertyTemplate), "propertyTemplate");

			Name[] args;
			if(arguments==null)
				args = new Name[0];
			else
				args = arguments.Distinct().Select(s => Name.BuildName("[" + s + "]")).ToArray();
			m_dynamicProperties.Add(propertyTemplate,new DynamicKnowledgeEntry(surogate,args));
		}

		public IEnumerable<Belief> GetAllBeliefs()
	    {
			foreach (var entry in m_knowledgeStorage)
			{
				foreach (var perspective in entry.Value.GetPerspectives())
				{
					yield return new Belief()
					{
						Name = entry.Key,
						Perspective = perspective.Key,
						Value = perspective.Value
					};
				}
			}
	    }

		public object AskProperty(Name property)
		{
			return AskProperty(property, Name.SELF_SYMBOL);
		}

		public object AskProperty(Name property, Name perspective)
		{
			if(!property.IsGrounded)
				throw new ArgumentException("The given Well Formed Name must be grounded","property");

			var results = AskPossibleProperties(property,perspective, null).Select(p => PrimitiveValue.Extract(p.Item1)).ToArray();
			if (results.Length == 0)
				return null;
			if (results.Length == 1)
				return results[0];
			return results;
		}

		public IEnumerable<Pair<PrimitiveValue, IEnumerable<SubstitutionSet>>> AskPossibleProperties(Name property, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			if (constraints == null)
				constraints = new[] { new SubstitutionSet() };

			if (property.IsPrimitive)
			{
				yield return Tuples.Create(property.GetPrimitiveValue(), constraints);
				yield break;
			}

			perspective = perspective.IsUniversal ? Perspective : perspective.RemovePerspective(Perspective);
			property = property.RemovePerspective(perspective);

			if (!property.IsVariable)
			{
				bool dynamicFound = false;
				foreach (var r in AskDynamicProperties(property,perspective, constraints))
				{
					dynamicFound = true;
					yield return r;
				}
				if (dynamicFound)
					yield break;
			}

			var group = constraints.GroupBy(property.MakeGround);

			foreach (var g in group)
			{
				if (g.Key.IsPrimitive)
				{
					yield return Tuples.Create(g.Key.GetPrimitiveValue(), (IEnumerable<SubstitutionSet>)g);
					continue;
				}

				SubstitutionSet set;
				var fact = g.Key.Unfold(out set);
				if (set != null)
					fact = FindConstantLeveledName(fact,perspective, set, false);
				if (fact == null)
					continue;

				var g2 = g.SelectMany(c => m_knowledgeStorage.Unify(fact, c)).GroupBy(r => r.Item1, r => r.Item2);
				foreach (var r in g2)
				{
					var value = r.Key.GetValueFor(perspective);
					if(value==null)
						continue;
					yield return Tuples.Create(value, r.Distinct());
				}
			}
		}

		private IEnumerable<Pair<PrimitiveValue, IEnumerable<SubstitutionSet>>> AskDynamicProperties(Name property, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			const string tmpMarker = "_arg";
			if (m_dynamicProperties.Count == 0)
				yield break;

			Name tmpPropertyName = property.ReplaceUnboundVariables(tmpMarker);

			var d = m_dynamicProperties.Unify(tmpPropertyName).ToList();
			if (d.Count == 0)
				yield break;

			var results = d.SelectMany(p =>
			{
				var args = ObjectPool<Dictionary<string, Name>>.GetObject();
				try
				{
					foreach (var s in p.Item2)
					{
						var paramName = s.Variable.ToString();
						paramName = paramName.Substring(1, paramName.Length - 2);
						args[paramName] = s.Value.RemoveBoundedVariables(tmpMarker);
						if (s.Value.IsVariable)
						{
							//Unify can mix parameter Name with it's value, if the value is a variable.
							//In this case, flip a duplicate of the argument entry
							paramName = s.Value.ToString();
							paramName = paramName.Substring(1, paramName.Length - 2);
							args[paramName] = s.Variable.RemoveBoundedVariables(tmpMarker);
						}
					}
					return p.Item1.surogate(this, perspective, args, constraints).ToList();
				}
				finally
				{
					args.Clear();
					ObjectPool<Dictionary<string, Name>>.Recycle(args);
				}
			});

			//var final = results.ToList();
			//if (final.Count == 0)
			//	yield break;

			foreach (var g in results.GroupBy(p => p.Item1, p => p.Item2))
			{
				yield return Tuples.Create(g.Key, g.Distinct());
			}
		}

	    public bool BeliefExists(Name name)
	    {
	        return m_knowledgeStorage.ContainsKey(name);
	    }

		/// <summary>
		/// Removes a predicate from the Semantic KB
		/// </summary>
		/// <param name="predicate">the predicate to be removed</param>
		public void Retract(Name predicate, Name perspective)
		{
			if(predicate.IsPrimitive)
				throw new ArgumentException("Unable to retract primitive value",nameof(predicate));

			if (!predicate.IsConstant)
				throw new ArgumentException("The given name is not constant. Only constant names can be retracted",nameof(predicate));

			if (m_dynamicProperties.Unify(predicate).Any())
				throw new ArgumentException("The given name cannot be retracted as it is a dynamic property.", nameof(predicate));

			if (perspective.IsUniversal)
			{
				if (HasSelf(predicate))
					throw new Exception($"Cannot remove a property containing SELF from the Universal context of the {nameof(KB)}");
			}

			predicate = predicate.RemovePerspective(perspective.RemovePerspective(Perspective));

			SubstitutionSet set;
			var fact = predicate.Unfold(out set);
			if (set != null)
			{
				fact = FindConstantLeveledName(fact,perspective, set,true);
			}

			KnowledgeEntry entry;
			if (m_knowledgeStorage.TryGetValue(fact, out entry))
			{
				entry.TellValueFor(perspective,null);
				if (entry.IsEmpty())
					m_knowledgeStorage.Remove(fact);
			}
		}

		/// <summary>
		/// This method provides a way to search for properties/predicates in the WorkingMemory 
		/// that match with a specified name with unbound variables.
		/// 
		/// In order to understand this method, let’s examine the following example. Suppose that 
		/// the memory only contains properties about two characters: Luke and John.
		/// Furthermore, it only stores two properties: their name and strength. So the KB will 
		/// only store the following objects:
		/// - Luke(Name) : Luke
		/// - Luke(Strength) : 8
		/// - John(Name) : John 
		/// - John(Strength) : 4
		/// 
		/// The next table shows the result of calling the method with several distinct names. 
		/// The function works by finding substitutions for the unbound variables, which make 
		/// the received name equal to the name of an object stored in memory.
		/// 
		/// Name			Substitutions returned
		/// Luke([x])		{{[x]/Name},{[x]/Strength}}
		/// [x](Strength)	{{[x]/John},{[x]/Luke}}
		/// [x]([y])		{{[x]/John,[y]/Name},{[x]/John,[y]/Strength},{[x]/Luke,[y]/Name},{[x]/Luke,[y]/Strength}}
		/// John(Name)	    {{}}
		/// John(Height)	null
		/// Paul([x])	    null
		/// 
		/// In the first example, there are two possible substitutions that make “Luke([x])”
		/// equal to the objects stored above. The third example has two unbound variables,
		/// so the returned set contains all possible combinations of variable attributions.
		/// 
		/// If this method receives a ground name, as seen on examples 4 and 5, it checks
		/// if the received name exists in memory. If so, a set with the empty substitution is
		/// returned, i.e. the empty substitution makes the received name equal to some object
		/// in memory. Otherwise, the function returns null, i.e. there is no substitution
		/// that applied to the name will make it equal to an object in memory. This same result
		/// is returned in the last example, since there is no object named Paul, and therefore no 
		/// substitution of [x] will match the received name with an existing object.
		/// </summary>
		/// <param name="name">a name (that correspond to a predicate or predicate)</param>
		/// <returns>a set of SubstitutionSets that make the received name to match predicates or properties that do exist in the KB</returns>
		public IEnumerable<SubstitutionSet> Unify(Name name, SubstitutionSet constraints = null)
		{
			return m_knowledgeStorage.Unify(name, constraints).Select(p => p.Item2);
		}

		public void Tell(Name property, PrimitiveValue value)
		{
			Tell(property,value,Name.SELF_SYMBOL);
		}

		public void Tell(Name property, PrimitiveValue value, Name perspective)
		{
			if (property.IsPrimitive)
				throw new Exception("The given name is a primitive. Primitive values cannot be changed.");	//TODO add a better exception

			if (!property.IsConstant)
				throw new Exception("The given name is not constant. Only constant names can be stored");	//TODO add a better exception

			if (m_dynamicProperties.Unify(property).Any())
				throw new ArgumentException("The given name will be objuscated by a dynamic property",nameof(property));

			if (perspective.IsUniversal)
			{
				if(HasSelf(property))
					throw new Exception($"Cannot add a property containing SELF to the Universal context of the {nameof(KB)}");
			}

			perspective = perspective.RemovePerspective(Perspective);
			property = property.RemovePerspective(perspective);

			SubstitutionSet set;
			var fact = property.Unfold(out set);
			if (set != null)
			{
				fact = FindConstantLeveledName(fact,perspective, set,true);
			}

			KnowledgeEntry entry;
			if (!m_knowledgeStorage.TryGetValue(fact, out entry))
			{
				entry = new KnowledgeEntry();
				m_knowledgeStorage[fact] = entry;
			}
			entry.TellValueFor(perspective,value);
		}

		//public void RemoveNonPersistent()
		//{
		//	var nonPersistentEntries = m_knowledgeStorage.Where(p => !p.Value.IsPersistent).Select(p => p.Key).ToList();
		//	foreach (var entry in nonPersistentEntries)
		//	{
		//		m_knowledgeStorage.Remove(entry);
		//	}
		//}

		public int NumOfEntries
		{
			get { return m_knowledgeStorage.Count; }
		}

		private Name FindConstantLeveledName(Name name, Name perspective, SubstitutionSet bindings, bool throwException)
		{
			var subs = new SubstitutionSet();
			foreach (var v in name.GetVariableList())
			{
				var value = bindings[v];
				if (value != null && !value.IsGrounded)
					value = FindConstantLeveledName(value,perspective, bindings, throwException);
				if (!throwException && value == null)
					return null;

				var r = AskPossibleProperties(value,perspective, new[] { bindings }).ToList();
				if (r.Count != 1)
				{
					if (throwException)
					{
						if (r.Count == 0)
							throw new Exception($"Knowledge Base could not find property for {value}");
						if (r.Count > 1)
							throw new Exception($"Knowledge Base found multiple valid values for {value}");
					}
					else
						return null;
				}

				var s = new Substitution(v, Name.BuildName(r[0].Item1));
				subs.AddSubstitution(s);
			}
			return name.MakeGround(subs);
		}

		private static bool HasSelf(Name name)
		{
			if (name.IsPrimitive)
				return name == Name.SELF_SYMBOL;

			return name.GetTerms().Any(HasSelf);
		}

		//private static Name RemovePerspective(Name property, Name ToMPerspective)
		//{
		//	if (ToMPerspective.IsUniversal)
		//	{

		//	}
		//	if (!HasSelf(property))
		//		return property;

		//	var p = property.RemovePerspective(ToMPerspective.ToString());
		//	return p;

		//	//if (ToMPerspective.Match(Name.SELF_SYMBOL))
		//	//	return property;

		//	//return property.ApplyPerspective(ToMPerspective.ToString());
		//}

		#region Serialization

		private string Perspective2String(Name perception)
		{
			if (perception.IsUniversal)
				return "universal";

			return perception.ApplyPerspective(this.Perspective).ToString();
		}

		private Name String2Perspective(string str)
		{
			if (string.Equals(str, "universal", StringComparison.InvariantCultureIgnoreCase))
				return Name.UNIVERSAL_SYMBOL;

			return ((Name)str).RemovePerspective(this.Perspective);
		}

		public void GetObjectData(ISerializationData dataHolder)
		{
			dataHolder.SetValue("Perspective",Perspective);
			var knowledge = dataHolder.ParentGraph.CreateObjectData();
			dataHolder.SetValueGraphNode("Knowledge",knowledge);
			//var dict = ObjectPool<Dictionary<string, IObjectGraphNode>>.GetObject();
			foreach (var entry in m_knowledgeStorage)
			{
				foreach (var perspective in entry.Value.GetPerspectives())
				{
					var key = Perspective2String(perspective.Key);
					
					IGraphNode node;
					if (!knowledge.TryGetField(key, out node))
					{
						node = dataHolder.ParentGraph.CreateObjectData();
						knowledge[key] = node;
					}

					((IObjectGraphNode)node)[entry.Key.ToString()] = dataHolder.ParentGraph.BuildNode(perspective.Value);
				}
			}
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			Perspective = dataHolder.GetValue<Name>("Perspective");
			var knowledge = dataHolder.GetValueGraphNode("Knowledge");
			var it = ((IObjectGraphNode) knowledge).GetEnumerator();
			while (it.MoveNext())
			{
				var perspective = String2Perspective(it.Current.FieldName);
				var holder = (IObjectGraphNode) it.Current.FieldNode;
				foreach (var field in holder)
				{
					var property = (Name) field.FieldName;
					var value = field.FieldNode.RebuildObject<PrimitiveValue>();
					if(value==null)
						continue;
					
					Tell(property,value,perspective);
				}
			}
		}

#endregion
	}
}
